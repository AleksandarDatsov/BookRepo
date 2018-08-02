using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookStore.Domain.Entities;
using BookStore.Domain.Infrastructure.UnitOfWorkPattern;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NLog;

namespace BookStore.Controllers
{
    public partial class AccountsController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string XsrfKey = "XsrfId";
        private readonly IUnitOfWork unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.unitOfWork = unitOfWork;
        }

        private AccountsController(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home", new { Username = Session["Username"].ToString()});
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                logger.Warn("'Login Action' ---- ModelState is Invalid");
                return View(model);
            }

            if (!IsThereSuchUsernameInDb(model.Username))
            {
                var user = unitOfWork.UsersRepository.SingleOrDefault(u => u.Username.Equals(model.Username));
                if (user.ConfirmedEmail)
                {
                    if (user.Password.Equals(model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, false);
                        this.Session["Username"] = user.Username.ToString();
                        return RedirectToAction(MVC.Home.Actions.Index());
                    }
                    else
                    {
                        this.ModelState.AddModelError("Password", "Password does not match");
                        return View(model);
                    }
                }
                else
                {
                    this.ModelState.AddModelError("ConfirmedEmail", "Email isn't comfirmed");
                    this.ViewBag.ShowDiv = true;
                    return View(model);
                }
            }
            else
            {
                this.ModelState.AddModelError("Username", "Invalid Username");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                this.ViewBag.ReturnUrl = returnUrl;
                this.ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        [AllowAnonymous]
        public virtual ActionResult ConfirmEmail(string Token, string email)
        {
            User user = this.unitOfWork.UsersRepository.SingleOrDefault(u => u.Email.Equals(email));
            if (user != null)
            {
                user.ConfirmedEmail = true;
                this.unitOfWork.Complete();
                return RedirectToAction("Login", "Accounts", new { username = user.Username });
            }
            else
            {
                return RedirectToAction("Confirm", "Accounts", new { Email = "" });
            }
        }

        [AllowAnonymous]
        public virtual ActionResult Confirm(string Email)
        {
            this.ViewBag.Email = Email; return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            var hasDbSuchEmail = false;
            var hasDbSuchUsername = false;

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            if (!IsThereSuchEmailInDb(model.Email))
            {
                hasDbSuchEmail = true;
                this.ModelState.AddModelError("Email", "password already exists");
            }

            if (!this.IsThereSuchUsernameInDb(model.Username))
            {
                hasDbSuchUsername = true;
                this.ModelState.AddModelError("Username", "username already exists");
            }

            if (hasDbSuchUsername || hasDbSuchEmail)
            {
                return this.View(model);
            }
            else
            {
                var newUser = new User(model.Username, model.Password, model.Age, model.Email);
                this.unitOfWork.UsersRepository.Add(newUser);
                this.unitOfWork.Complete();

                var user = new ApplicationUser() { UserName = model.Username };
                user.Email = model.Email;
                user.EmailConfirmed = false;
                var result = await this.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    this.ConfirmationEmail(user.UserName);
                }

                return RedirectToAction(MVC.Accounts.Actions.Login());
            }
        }

        public virtual ActionResult SendConfirmationEmail(string userName)
        {
            this.ConfirmationEmail(userName);
            return RedirectToAction(MVC.Accounts.Actions.Login());
        }

        [HttpPost]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Abandon();
            return RedirectToAction(MVC.Accounts.Actions.Login());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View(MVC.Accounts.Actions.Login());
                }

                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await this.SignInAsync(user, isPersistent: false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        [NonAction]
        private bool IsThereSuchEmailInDb(string Email)
        {
            var user = this.unitOfWork.UsersRepository.SingleOrDefault(u => u.Email.Equals(Email));
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [NonAction]
        private bool IsThereSuchUsernameInDb(string Username)
        {
            var user = this.unitOfWork.UsersRepository.SingleOrDefault(u => u.Username.Equals(Username));
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [NonAction]
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        [NonAction]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction(MVC.Home.Actions.Index());
            }
        }

        [NonAction]
        private void ConfirmationEmail(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = this.unitOfWork.UsersRepository.SingleOrDefault(u => u.Username.Equals(userName));

                if (user != null)
                {
                    MailMessage message = new MailMessage(
                        new MailAddress("**********", "Web Registration"),
                        new MailAddress(user.Email));
                    message.Subject = "Email confirmation";
                    message.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>",
                                                user.Username, Url.Action("ConfirmEmail", "Accounts", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                    message.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                    {
                        Credentials = new NetworkCredential("*************", "******"),
                        EnableSsl = true,
                        Port = 587
                    };

                    smtp.Send(message);
                }
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}
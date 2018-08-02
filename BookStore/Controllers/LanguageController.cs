using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public partial class LanguageController : Controller
    {
        public virtual ActionResult Index()
        {
            return this.View();
        }

        public virtual ActionResult Change(string lang)
        {
            if (lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            this.Response.Cookies.Add(cookie);

            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}
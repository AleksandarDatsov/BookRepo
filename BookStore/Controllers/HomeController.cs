using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookStore.Domain.Entities;
using BookStore.Domain.Enumerations;
using BookStore.Domain.Infrastructure.UnitOfWorkPattern;
using BookStore.Models.ViewModels;
using NLog;

namespace BookStore.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, Duration = 0)]
    public partial class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual ActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                Books = unitOfWork.BooksRepository.GetAll().OrderBy(b => b.BookName)
            };

            this.ViewBag.Authors = Author.GetAll();
            return View(homeViewModel);
        }

        [HttpGet]
        public virtual ActionResult AvailableBooks(HomeViewModel homeModel)
        {
            SearchResultViewModel model = new SearchResultViewModel();
            List<Book> books = unitOfWork.BooksRepository.GetAllBooksByAuthor(homeModel.SelectedAuthorId).ToList();
            if (homeModel.IsInStock)
            {
                books = books.Where(b => b.IsInStock == homeModel.IsInStock).ToList();
            }

            if (homeModel.BookName != null && homeModel.BookName != string.Empty)
            {
                books = books.Where(b => b.BookName.ToLower().Contains(homeModel.BookName.ToLower())).ToList();
            }

            books = books.OrderBy(b => b.BookName).ToList();
            if (books.Capacity > 0)
            {
                foreach (var book in books)
                {
                    model.Books.Add(new BookResultViewModel
                    {
                        BookName = book.BookName,
                        Id = book.Id,
                        ReleaseYear = book.ReleaseYear,
                        IsInStock = book.IsInStock,
                        NumbersInStock = book.NumbersInStock
                    });
                }
            }
            else
            {
                logger.Info("'AvailableBooks Action' ---- books.Capacity equals to zero");
            }

            return View(MVC.Home.Views._DataTableSearchResult, model);
        }

        public virtual ActionResult GeneratePdf()
        {
            return new Rotativa.UrlAsPdf(Request.UrlReferrer.ToString()) { FileName = "test.pdf" };
        }
    }
}
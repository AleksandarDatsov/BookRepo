using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookStore.Domain.Entities;
using BookStore.Domain.Enumerations;
using BookStore.Domain.Infrastructure.UnitOfWorkPattern;
using BookStore.Models;
using BookStore.Models.ViewModels;
using NLog;

namespace BookStore.Controllers
{
    [Authorize]
    public partial class BooksController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public readonly string _bookFormat = "dd/MM/yyyy";
        private readonly IUnitOfWork unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            BookViewModel model = new BookViewModel
            {
                ReleaseYear = DateTime.Now,
                IsInStock = true
            };

            this.ViewBag.Authors = Author.GetAll();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var selectedItem = Author.GetById(model.AuthorId);
                if (selectedItem != null)
                {
                    Book book = new Book(model.BookName,
                        model.AuthorId,
                        model.ReleaseYear.ToString(_bookFormat),
                        model.IsInStock,
                        model.NumbersInStock);

                    this.unitOfWork.BooksRepository.Add(book);
                    this.unitOfWork.Complete();
                }
                else
                {
                    logger.Warn("'Create Action' ---- The 'selectedItem' is null");
                }

                return RedirectToAction(MVC.Books.Actions.Create());
            }
        }
    }
}
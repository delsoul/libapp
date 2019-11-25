using libapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace libapp.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Электронная библиотека";

            var news = db.News.OrderBy(News => News.NewsId).Skip(Math.Max(0, db.News.Count() - 3));

            return View(news);
        }

        public ActionResult BookInfo(int? id)
        {
            ViewBag.Title = "Электронная библиотека";

            var book = db.Books.Where(b => b.Id == id).FirstOrDefault();

            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(book);
            }
        }

        public ActionResult News(int page = 1)
        {
            ViewBag.Title = "Электронная библиотека";

            int pageSize = 9; // количество выводимых книг
            IEnumerable<News> bookPerPages = db.News.OrderBy(x => x.NewsId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.News.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, News = bookPerPages };

            return View(ivm);
        }

        public ActionResult NewsInfo(int? id)
        {
            ViewBag.Title = "Электронная библиотека";

            var news = db.News.Where(n => n.NewsId == id).FirstOrDefault();

            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(news);
            }
        }
            
        public ActionResult ToReaders()
        {
            ViewBag.Title = "Электронная библиотека";

            return View();
        }

        public ActionResult Catalog(int page = 1)
        {
            ViewBag.Title = "Электронная библиотека";

            int pageSize = 9; // количество выводимых книг
            IEnumerable<Book> bookPerPages = db.Books.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Books.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Books = bookPerPages };

            return View(ivm);
        }

        public ActionResult Services()
        {
            ViewBag.Title = "Электронная библиотека";
            return View();
        }

        public ActionResult History()
        {
            ViewBag.Title = "Электронная библиотека";
            return View();
        }

        public ActionResult Contacts()
        {
            ViewBag.Title = "Электронная библиотека";
            return View();
        }

        public ActionResult Libnet()
        {
            ViewBag.Title = "Электронная библиотека";
            return View();
        }

        public ActionResult _BookSearch(string name)
        {
            var allbooks = db.Books.Where(a => a.Name.Contains(name)).ToList();
            if (allbooks.Count <= 0)
            {
                allbooks = db.Books.Where(a => a.Name.ToLower().Contains(name)).ToList();
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Name.ToUpper().Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Author.ToLower().Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Author.ToUpper().Contains(name)).ToList();
                };
                if(allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Genre.Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Genre.ToLower().Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Genre.ToUpper().Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Publisher.Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Publisher.ToLower().Contains(name)).ToList();
                };
                if (allbooks.Count <= 0)
                {
                    allbooks = db.Books.Where(a => a.Publisher.ToUpper().Contains(name)).ToList();
                };
            };
            return PartialView(allbooks);
        }
    }
}
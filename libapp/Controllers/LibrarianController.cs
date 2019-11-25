using libapp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace libapp.Controllers
{
    public class LibrarianController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "librarian")]
        public ActionResult Books(int page = 1)
        {
            ViewBag.Title = "Библиотекарь";

            int pageSize = 6; // количество выводимых книг
            IEnumerable<Book> bookPerPages = db.Books.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Books.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Books = bookPerPages };

            return View(ivm);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult AddBook()
        {
            ViewBag.Title = "Библиотекарь";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult AddBook(Book book, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                book.Picture = imageData;

                db.Books.Add(book);

                db.SaveChanges();

                return RedirectToAction("Books", "Librarian");
            }
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Books");
        }

        [Authorize(Roles = "librarian")]
        public ActionResult DeleteBook(int id)
        {
            ViewBag.Title = "Библиотекарь";

            Book b = new Book { Id = id };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Books");
        }

        [Authorize(Roles = "librarian")]
        public ActionResult News(int page = 1)
        {
            ViewBag.Title = "Библиотекарь";

            int pageSize = 6; // количество выводимых книг
            IEnumerable<News> newsPerPages = db.News.OrderBy(x => x.NewsId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.News.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, News = newsPerPages };

            return View(ivm);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult AddNews()
        {
            ViewBag.Title = "Библиотекарь";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult AddNews(News news, HttpPostedFileBase uploadImage)
        {
            ViewBag.Title = "Библиотекарь";

            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                news.Picture = imageData;

                db.News.Add(news);

                db.SaveChanges();

                return RedirectToAction("News", "Librarian");
            }
            return View(news);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult EditNews(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            News news = db.News.Find(id);
            if (news != null)
            {
                return View(news);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public ActionResult EditNews(News news)
        {
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("News");
        }

        [Authorize(Roles = "librarian")]
        public ActionResult DeleteNews(int id)
        {
            ViewBag.Title = "Библиотекарь";

            News n = new News { NewsId = id };
            db.Entry(n).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("News");
        }

        [Authorize(Roles = "user")]
        // пользователь бронирорует книгу
        public ActionResult Reservation(int id)
        {
            //переменная принимающая в качестве значения имя пользователя (только для зарегистрированных пользователей)
            var user = User.Identity.Name;

            //переменная принимающая в качестве значения метод поиска книги по id
            var book = db.Books.Find(id);

            //если "состояние" книги - свободна, то
            if (book.BookState == BookState.Free)
            {
                //установить состояние книги как "забронирована"
                book.BookState = BookState.Booked;

                //добавить в таблицу Bookings в БД новую запись
                db.Bookings.Add(new Booking
                {
                    Book = book,
                    User = user,
                    ReserveAt = DateTime.Now,
                });

                book.Client = user;

                //записать данные в БД
                db.SaveChanges();
            }

            //редирект на страницу "Catalog" или же просто "Каталог"
            return RedirectToAction("Catalog", "Home");
        }

        [Authorize(Roles = "user")]
        // пользователь или библиотекарь разбронирует книгу (отменяет бронь, простите за мой английский)
        public ActionResult ReReservation(int id)
        {
            //переменная принимающая в качестве значения имя пользователя (только для зарегистрированных пользователей)
            var user = User.Identity.Name;

            //переменная принимающая в качестве значения метод поиска книги по id
            var book = db.Books.Find(id);

            //если "состояние" книги "забронирована", то
            if (book.BookState == BookState.Booked)
            {
                //установить состояние книги как "свободна"
                book.BookState = BookState.Free;

                //переменная, принимающая в качестве значения метод LINQ для поска в БД книги с совпадающим id
                var booking = db.Bookings
                    .FirstOrDefault(b => b.Book.Id == id);

                // (до)запись в БД даты и времени окончания бронирования
                booking.AcceptedAt = DateTime.Now;

                book.Client = null;

                //обновление базы данных
                db.Entry(book).State = EntityState.Modified;

                //запись в БД изменений
                db.SaveChanges();
            }
            //редирект на страницу "Catalog" или же просто "Каталог"
            return RedirectToAction("Catalog", "Home");
        }

        [Authorize(Roles = "librarian")]
        //библиотекарь выдает книгу
        public ActionResult Issued(int id)
        {
            //переменная принимающая в качестве значения метод поиска книги по id
            var book = db.Books.Find(id);

            //если "состояние" книги "забронирована", то
            if (book.BookState == BookState.Booked)
            {
                //установаить состояние книги как "выдана"
                book.BookState = BookState.Issued;

                //обновление базы данных
                db.Entry(book).State = EntityState.Modified;

                //запись в БД изменений
                db.SaveChanges();
            }
            //редирект на страницу "Catalog" или же просто "Каталог"
            return RedirectToAction("Books", "Librarian");
        }

        [Authorize(Roles = "librarian")]
        //библиотекарь принимает книгу
        public ActionResult TakeBack(int id)
        {
            //переменная принимающая в качестве значения метод поиска книги по id
            var book = db.Books.Find(id);

            //если "состояние" книги "выдана", то
            if (book.BookState == BookState.Issued)
            {
                //установаить состояние книги как "свободна"
                book.BookState = BookState.Free;

                //переменная, принимающая в качестве значения метод LINQ для поска в БД книги с совпадающим id
                var Booking = db.Bookings
                    .FirstOrDefault(b => b.Book.Id == id);

                // (до)запись в БД даты и времени окончания бронирования
                Booking.AcceptedAt = DateTime.Now;

                book.Client = null;

                //обновление базы данных
                db.Entry(book).State = EntityState.Modified;

                //запись в БД изменений
                db.SaveChanges();
            }

            //редирект на страницу "Catalog" или же просто "Каталог"
            return RedirectToAction("Books", "Librarian");
        }
    }
}
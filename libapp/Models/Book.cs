using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace libapp.Models
{
    public class Book
    {
        // id книги
        public int Id { get; set; }
        // название книги
        public string Name { get; set; }
        // автор книги
        public string Author { get; set; }
        // описаниек книги
        public string Description { get; set; }
        //издатель книги
        public string Publisher { get; set; }
        // жанр книги
        public string Genre { get; set; }
        // забронирована/не забронирована
        public BookState BookState { get; set; }
        // обложка книги
        public byte[] Picture { get; set; } //пришлось выпилить обложки, т.к. при обновлении книги в БД изчезают данные картинки
        // клиент
        public string Client { get; set; }
    }

    public enum BookState
    {
        // книга свободна
        Free = 0,
        // книга забронирована
        Booked = 1,
        // книга выдана
        Issued = 2
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public string User { get; set; }
        public Book Book { get; set; }
        public DateTime ReserveAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
    }
}
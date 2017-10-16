using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BooksSerialization
{

    [Serializable]
    public class Book
    {
        public enum Genre
        {
            ScienceFiction,
            Horror,
            Romance,
            Fantasy,
            Computer
        }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("isbn")]
        public string Isbn { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public string GenreAsString
        {
            get
            {
                if (BookGenre == Genre.ScienceFiction)
                {
                    return "Science Fiction";
                }
                return BookGenre.ToString();
            }
            set
            {
                if (value == "Science Fiction")
                    BookGenre = Genre.ScienceFiction;
                else if (value == "Horror")
                {
                    BookGenre = Genre.Horror;
                }
                else if (value == "Romance")
                {
                    BookGenre = Genre.Romance;
                }
                else if (value == "Fantasy")
                {
                    BookGenre = Genre.Fantasy;
                }
                else if (value == "Computer")
                {
                    BookGenre = Genre.Computer;
                }
            }
        }

        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("publish_date", DataType = "date")]
        public DateTime PublishDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("registration_date",DataType = "date")]
        public DateTime RegistrationDate { get; set; }

        [XmlIgnore]
        public Genre BookGenre { get; set; }

        public Book()
        {
        }

        public Book(string isbn, string author, string title, 
            Genre genre, string publisher, 
            DateTime publishDate, string description, DateTime registrationDate)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            BookGenre = genre;
            Publisher = publisher;
            PublishDate = publishDate;
            Description = description;
            RegistrationDate = registrationDate;
        }
    }
}

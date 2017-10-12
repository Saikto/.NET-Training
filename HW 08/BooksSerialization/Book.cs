using System;
using System.Collections.Generic;
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
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("isbn", Order = 1)]
        public string Isbn { get; set; }

        [XmlElement("author", Order = 2)]
        public string Author { get; set; }

        [XmlElement("title", Order = 3)]
        public string Title { get; set; }

        [XmlElement("publisher", Order = 4)]
        public string Publisher { get; set; }

        [XmlElement("publish_date", DataType = "date", Order = 5)]
        public DateTime PublishDate { get; set; }

        [XmlEnum("genre")]
        public static Enum Genre;

        [XmlElement("description", Order = 7)]
        public string Description { get; set; }

        [XmlElement("registration_date",DataType = "date", Order = 8)]
        public DateTime RegistrationDate { get; set; }
        
        //{
        //    Computer,
        //    Fantasy,
        //    Romance,
        //    Horror,
        //    ScienceFiction
        //}

        public Book()
        {
        }

        public Book(string isbn, string author, string title, 
            Enum genre, string publisher, 
            DateTime publishDate, string description, DateTime registrationDate)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            Genre = genre;
            Publisher = publisher;
            PublishDate = publishDate;
            Description = description;
            RegistrationDate = registrationDate;
        }
    }
}

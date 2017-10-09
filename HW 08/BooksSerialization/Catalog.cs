using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BooksSerialization
{
    [Serializable, XmlRoot(ElementName = "catalog", Namespace = "http://library.by/catalog")]
    public class Catalog
    {
        [XmlAttribute("xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute("date")]
        public DateTime Date { get; set; }
        [XmlElement("book", typeof(Book))]
        public List<Book> BooksList { get; set; }

        public Catalog()
        {
        }

        public Catalog(string xmlns, DateTime date, List<Book> booksList)
        {
            Xmlns = xmlns;
            Date = date;
            BooksList = booksList;
        }
    }
}

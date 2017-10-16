using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using BooksSerialization;

namespace SerializationRunner
{
    class Runner
    {
        public static void SerializeCatalog(Catalog catalog, string path)
        {
            var serializer = new XmlSerializer(typeof(Catalog), new Type[] { typeof(Book) });
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                serializer.Serialize(fileStream, catalog);
                Console.WriteLine("Catalog successfully serialized.");
            }
        }

        public static Catalog DeserializeCatalog(string path)
        {
            var serializer = new XmlSerializer(typeof(Catalog), new Type[] {typeof(Book)});
            using (XmlReader reader = XmlReader.Create(path))
            {
                Catalog catalog = (Catalog)serializer.Deserialize(reader);
                Console.WriteLine("Catalog successfully deserialized.");
                return catalog;
            }
        }

        static void Main(string[] args)
        {
            string path = @"..\..\..\RD. HW - AT Lab. C#. 08 - Books.xml";
            string path1 = @"..\..\..\Books.xml";
            Catalog catalog = DeserializeCatalog(path);
            SerializeCatalog(catalog, path1);
        }
    }
}

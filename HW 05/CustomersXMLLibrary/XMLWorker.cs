using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomersXMLLibrary
{
    class XElementEqualityComparer : IEqualityComparer<XElement>
    {
        public bool Equals(XElement x1, XElement x2)
        {
            if (x1 == null && x2 == null)
                return true;
            else if (x1 == null | x2 == null)
                return false;
            else if (x1.Value == x2.Value)
                return true;
            else
                return false;
        }
        public int GetHashCode(XElement x)
        {
            int hCode = 0;
            foreach (var a in x.Value.ToCharArray())
            {
                hCode += (int) a;
            }
            return hCode;
        }
    }

    public static class XMLWorker
    {
        public static string path =
            @"E:\Study\VS Projects\.NET-Training\HW 05\CustomersXMLLibrary\RD. HW - AT Lab. C#. 05 - Customers.xml";

        public static void AddOrder(this XElement rootCustomer, string id, string orderdate, string total)
        {
            XElement newOrder = new XElement("order",
                new XElement("id", id),
                new XElement("orderdate", orderdate),
                new XElement("total", total));
            rootCustomer.Element("orders").Add(newOrder);
        }

        public static void AddCustomer(this XElement rootElement, string id, string postalcode, string phone)
        {
            XElement newCustomer = new XElement("customer",
                new XElement("id", id),
                new XElement("orders"));
            rootElement.Add(newCustomer);
        }

        public static List<XElement> OrdersSumMoreXList(this XDocument doc, double x)
        {
            var customers = doc
                .Element("customers")
                .Elements("customer")
                .Where(c => c.Descendants("total")
                .Sum(t => double.Parse(t.Value, CultureInfo.InvariantCulture)) > x).ToList();
            return customers;
        }

        public static List<XElement> OrderMoreXList(this XDocument doc, double x)
        {
            var customers = doc
                .Element("customers")
                .Elements("customer")
                .Where(c => c.Descendants("total")
                    .Any(t => double.Parse(t.Value, CultureInfo.InvariantCulture) > x)).ToList();
            return customers;
        }

        public static void GroupByCountry(this XDocument doc)
        {
            var customersByCountry = doc
                .Element("customers")
                .Elements("customer")
                .GroupBy(t => t.Element("country")).ToList();
        }

        public static Dictionary<string, XElement> CustomersWithStartDate(this XDocument doc)
        {
            var customersWithFirstOrderDate = doc
                .Element("customers")
                .Elements("customer").ToDictionary(t =>
                    t.Element("orders")
                    .Elements("order")
                    .First()
                    .Element("orderdae")
                    .Value);
            return customersWithFirstOrderDate;
        }

        public static string GetPostalCode(this XElement t)
        {
            if (t.Element("postalcode") != null)
                return t.Element("postalcode").Value;
            return "";
        }

        public static List<XElement> CustomersWrongData(this XDocument doc)
        {
            string pattern = "[0-9]+";
            Regex reg = new Regex(pattern);
            var customersWithWrongData = doc
                .Element("customers")
                .Elements("customer")
                .Where(t => !reg.IsMatch(t.GetPostalCode()) || (t.Descendants("region").ToList().Count) == 0 ||
                            !t.Element("phone").Value.StartsWith("("))
                            .ToList();
            return customersWithWrongData;
        }

        public static Dictionary<string, double> CityProfability(this XDocument doc)
        {
            Dictionary<string, double> CityOrdersAverage =new Dictionary<string, double>();
            List<XElement> cities = doc.Descendants("city").Distinct(new XElementEqualityComparer()).ToList();
            foreach (var city in cities)
            {
                var cityCustomers = doc
                    .Element("customers")
                    .Elements("customer")
                    .Where(t => t.Element("city").Value == city.Value).ToList();
                int ordersCountByCity = cityCustomers.Descendants("total").ToList().Count;
                double ordersSumByCity = cityCustomers.Descendants("total")
                    .Sum(t => double.Parse(t.Value, CultureInfo.InvariantCulture));
                double average = ordersSumByCity / ordersCountByCity;
                CityOrdersAverage.Add(city.Value, average);
            }
            return CityOrdersAverage;
        }
    }
}


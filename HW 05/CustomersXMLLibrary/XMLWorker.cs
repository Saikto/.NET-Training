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
        public static int GetYearFromXElement(this XElement orderdate)
        {
            DateTime date = DateTime.Parse(orderdate.Value);
            return date.Year;
        }

        public static int GetMonthFromXElement(this XElement orderdate)
        {
            DateTime date = DateTime.Parse(orderdate.Value);
            return date.Month;
        }

        //Adds order to selected XElement
        public static void AddOrder(this XElement rootCustomer, string id, string orderdate, string total)
        {
            XElement newOrder = new XElement("order",
                new XElement("id", id),
                new XElement("orderdate", orderdate),
                new XElement("total", total));
            rootCustomer.Element("orders").Add(newOrder);
        }

        //Adds customer to selected XElement
        public static void AddCustomer(this XElement rootElement, string id, string city, string postalcode, string phone)
        {
            XElement newCustomer = new XElement("customer",
                new XElement("id", id),
                new XElement("city", city),
                new XElement("postacode", postalcode),
                new XElement("phone", phone),
                new XElement("orders"));
            rootElement.Add(newCustomer);
        }

        //Returns list of customers containing the customers whose sum of all orders exceeds X
        public static List<XElement> OrdersSumMoreXList(this XDocument doc, double x)
        {
            var customers = doc
                .Element("customers")
                .Elements("customer")
                .Where(c => c.Descendants("total")
                .Sum(t => double.Parse(t.Value, CultureInfo.InvariantCulture)) > x).ToList();
            return customers;
        }

        //Returns list of customers containing the customers whose "total" value of at  least one order exceeds X
        public static List<XElement> OrderMoreXList(this XDocument doc, double x)
        {
            var customers = doc
                .Element("customers")
                .Elements("customer")
                .Where(c => c.Descendants("total")
                    .Any(t => double.Parse(t.Value, CultureInfo.InvariantCulture) > x)).ToList();
            return customers;
        }

        //Groups customer by country () [TODO]
        public static void GroupByCountry(this XDocument doc)
        {
            var customersByCountry = doc
                .Element("customers")
                .Elements("customer")
                .GroupBy(t => t.Element("country")).ToList();
        }

        //Returns dictionary in wich KEY is first order Date and value is customer XElement
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

        //Returns string value of postal code XElement
        public static string GetPostalCode(this XElement t)
        {
            if (t.Element("postalcode") != null)
                return t.Element("postalcode").Value;
            return "";
        }

        //Returns list of customers who have wrong postal code/phone number/no region
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

        //Returns dictionary in wich KEY is city name and value is city profability
        public static Dictionary<string, double> CityProfability(this XDocument doc)
        {
            Dictionary<string, double> cityOrdersAverage =new Dictionary<string, double>();
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
                cityOrdersAverage.Add(city.Value, average);
            }
            return cityOrdersAverage;
        }

        //Returns dictionary in wich KEY is city name and value is city intensity
        public static Dictionary<string, double> CityIntensity(this XDocument doc)
        {
            Dictionary<string, double> cityOrdersByCustomerAverage = new Dictionary<string, double>();
            List<XElement> cities = doc.Descendants("city").Distinct(new XElementEqualityComparer()).ToList();
            foreach (var city in cities)
            {
                var cityCustomers = doc
                    .Element("customers")
                    .Elements("customer")
                    .Where(t => t.Element("city").Value == city.Value).ToList();
                int ordersCountByCity = cityCustomers.Descendants("total").ToList().Count;
                int customersCountInCity = cityCustomers.Count;
                double average = (double)customersCountInCity / (double)ordersCountByCity;
                cityOrdersByCustomerAverage.Add(city.Value, average);
            }
            return cityOrdersByCustomerAverage;
        }


        //Returns dictionary in wich KEY is month number, VALUE is customers activity percent
        public static Dictionary<int, double> CustomersActivityByMonth(this XDocument doc)
        {
            double customersCount = doc.Element("customers")
                .Elements("customer").ToList().Count;
            Dictionary<int, double> customersActivity = new Dictionary<int, double>(12);
            for (int i = 1; i <= 12; i++)
            {
                var iMonthCustomers = doc.Element("customers")
                    .Elements("customer")
                    .Where(c => c.Descendants("orderdate").Any(t => t.GetMonthFromXElement() == i)).ToList();
                customersActivity.Add(i, iMonthCustomers.Count / customersCount * 100);
            }
            return customersActivity;
        }

        //Returns dictionary in wich KEY is year, VALUE is customers activity percent
        public static Dictionary<int, double> CustomersActivityByYear(this XDocument doc)
        {
            double customersCount = doc.Element("customers")
                .Elements("customer").ToList().Count;
            var listOfYears = doc.Element("customers")
                .Elements("customer")
                .Descendants("orderdate")
                .Select(t => t.GetYearFromXElement()).Distinct().OrderBy(t => t).ToList();
            Dictionary<int, double> customersActivity = new Dictionary<int, double>();
            foreach (int year in listOfYears)
            {
                var iYearCustomers = doc.Element("customers")
                    .Elements("customer")
                    .Where(c => c.Descendants("orderdate").Any(t => t.GetYearFromXElement() == year)).ToList();
                customersActivity.Add(year, iYearCustomers.Count / customersCount * 100);
            }
            return customersActivity;
        }

        //Returns dictionary in wich KEY is year-month pair, VALUE is customers activity percent
        public static Dictionary<string, double> CustomersActivityByYearAndMonth(this XDocument doc)
        {
            double customersCount = doc.Element("customers")
                .Elements("customer").ToList().Count;
            var listOfYears = doc.Element("customers")
                .Elements("customer")
                .Descendants("orderdate")
                .Select(t => t.GetYearFromXElement()).Distinct().OrderBy(t => t).ToList();
            Dictionary<string, double> customersActivity = new Dictionary<string, double>();
            foreach (int year in listOfYears)
            {
                var iYearCustomers = doc.Element("customers")
                    .Elements("customer")
                    .Where(c => c.Descendants("orderdate").Any(t => t.GetYearFromXElement() == year)).ToList();
                for (int i = 1; i <= 12; i++)
                {
                    var iMonthCustomers = iYearCustomers
                                            .Where(c => c.Descendants("orderdate").Any(t => t.GetMonthFromXElement() == i)).ToList();
                    customersActivity.Add(year.ToString() + "-" + i.ToString(), iMonthCustomers.Count / customersCount * 100);
                }
            }
            return customersActivity;
        }
    }
}


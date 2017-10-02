using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using System.Text;
using System.Threading.Tasks;
using CustomersXMLLibrary;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    [TestClass]
    public class CustomersTests
    {
        string id = "IGOR";
        string postalcode = "220000";
        string phone = "366-35-66";
        double x = 10000;
        string city = "Minsk";
       
        public static string path =
            @"E:\Study\VS Projects\.NET-Training\HW 05\CustomersXMLLibrary\RD. HW - AT Lab. C#. 05 - Customers.xml";
        //@"D:\Tasks\.NET-Training\HW 05\CustomersXMLLibrary\RD. HW - AT Lab. C#. 05 - Customers.xml";
        public static string path1 =
            @"E:\Study\VS Projects\.NET-Training\HW 05\CustomersXMLLibrary\EmptyCustomers.xml";
        //@"D:\Tasks\.NET-Training\HW 05\CustomersXMLLibrary\EmptyCustomers.xml";

        [TestMethod]
        public void TstGroupByCountry()
        {
            XDocument doc = XDocument.Load(path);
            XMLWorker.GroupByCountry(doc);
        }

        [TestMethod]
        public void TstOrderMoreXList()
        {
            double x = 10000;
            XDocument doc = XDocument.Load(path);
            int count = XMLWorker.OrderMoreXList(doc, x).Count;
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1998 - 04 - 03T00: 00:00", "10001");
            int count1 = XMLWorker.OrderMoreXList(doc, x).Count;
            Assert.AreEqual(count1 - 1, count);
        }

        [TestMethod]
        public void TstOrdersSumMoreXList()
        {
            double x = 1000;
            XDocument doc = XDocument.Load(path);
            int count = XMLWorker.OrdersSumMoreXList(doc, x).Count;
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1998 - 04 - 03T00: 00:00", "1001");
            int count1 = XMLWorker.OrdersSumMoreXList(doc, x).Count;
            Assert.AreEqual(count1 - 1, count);
        }

        [TestMethod]
        public void TstAddOrder()
        {
            XDocument doc = XDocument.Load(path);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            var ordersCount = doc
                .Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .Element("orders")
                .Elements("order").Count();

            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1998 - 04 - 03T00: 00:00", "1000.00");

            var ordersCount1 = doc
                .Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .Element("orders")
                .Elements("order").Count();
            Assert.AreEqual(ordersCount1 - 1, ordersCount);
        }

        [TestMethod]
        public void TstCustomersWrongData()
        {
            XDocument doc = XDocument.Load(path);
            int count = XMLWorker.CustomersWrongData(doc).Count;
            doc.Element("customers").AddCustomer(id, city, "220-000", "(029)366-35-66");
            doc.Element("customers").AddCustomer("VASIL", city, "220000", "840-66-90");
            doc.Element("customers").AddCustomer("VASIL", city, "220000", "(017)840-66-90");
            int count1 = XMLWorker.CustomersWrongData(doc).Count;
            Assert.AreEqual(count1 - 3, count);
        }

        [TestMethod]
        public void TstAddCustomer()
        {
            XDocument doc = XDocument.Load(path);
            var customersCount = doc
                .Element("customers")
                .Elements("customer").ToList().Count;
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            var customersCount1 = doc
                .Element("customers")
                .Elements("customer").ToList().Count;
            Assert.AreEqual(customersCount1 - 1, customersCount);
        }

        [TestMethod]
        public void TstCityProfability()
        {
            XDocument doc = XDocument.Load(path);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1998 - 04 - 03T00: 00:00", "1000");
            Dictionary<string, double> profability = new Dictionary<string, double>();
            profability = doc.CityProfability();
            double x = 0;
            if (profability.TryGetValue("Minsk", out x))
                Assert.AreEqual(1000, x);
            else
                Assert.Fail();
        }

        [TestMethod]
        public void TstCityIntensity()
        {
            XDocument doc = XDocument.Load(path);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1998 - 04 - 03T00: 00:00", "1000");
            Dictionary<string, double> intensity = new Dictionary<string, double>();
            intensity = doc.CityIntensity();
            double x = 0;
            if (intensity.TryGetValue("Minsk", out x))
                Assert.AreEqual(1, x);
            else
                Assert.Fail();
        }

        [TestMethod]
        public void TstCustomersActivityByMonth()
        {
            XDocument doc = XDocument.Load(path1);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "2000 - 04 - 03T00: 00:00", "1000");
            Dictionary<int, double> activity = new Dictionary<int, double>();
            activity = doc.CustomersActivityByMonth();
            activity.TryGetValue(4, out double x);
            Assert.AreEqual(100, x);
        }

        [TestMethod]
        public void TstCustomersActivityByYear()
        {
            XDocument doc = XDocument.Load(path1);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "1001 - 04 - 03T00: 00:00", "1000");
            Dictionary<int, double> activity = new Dictionary<int, double>();
            activity =  doc.CustomersActivityByYear();
            activity.TryGetValue(1001, out double x);
            Assert.AreEqual(100, x);
        }

        [TestMethod]
        public void TstCustomersActivityByYearAndMonth()
        {
            XDocument doc = XDocument.Load(path1);
            XMLWorker.CustomersActivityByYearAndMonth(doc);
            doc.Element("customers").AddCustomer(id, city, postalcode, phone);
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == id)
                .AddOrder("268", "2001 - 04 - 03T00: 00:00", "1000");
            Dictionary<string, double> activity = new Dictionary<string, double>();
            activity = doc.CustomersActivityByYearAndMonth();
            activity.TryGetValue("2001-4", out double x);
            Assert.AreEqual(100, x);
            doc.Element("customers").AddCustomer("VASIL", city, "220000", "840-66-90");
            doc.Element("customers")
                .Elements("customer")
                .Single(t => t.Element("id").Value == "VASIL")
                .AddOrder("269", "2002 - 04 - 03T00: 00:00", "2000");
            activity = doc.CustomersActivityByYearAndMonth();
            activity.TryGetValue("2002-4", out x);
            Assert.AreEqual(50, x);
        }
    }
}

﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestsLibrary;
using TestsLibrary.Enums;

namespace Tests.Task_3
{
    [TestFixture("single", "Win7FireFox")]
    [TestFixture("single", "Win10Chrome")]
    public class SetUp_Task3
    {
        private static readonly string FixtureName = "Task3";
        private static string _browser;
        protected static IWebDriver Driver;
        protected static WebDriverWait Wait;
        protected string Profile;
        protected string Environment;

        public SetUp_Task3()
        {
        }

        public SetUp_Task3(string profile, string environment)
        {
            this.Profile = profile;
            this.Environment = environment;
        }

        [OneTimeSetUp]
        public void TestsTask3Init()
        {
            _browser = Browsers.BrowserStack.ToString();
            Driver = WebDriverSelector.GetWebDriver(FixtureName, _browser, Profile, Environment);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TestsTask3Dispose()
        {
            Driver.Dispose();
        }
    }
}

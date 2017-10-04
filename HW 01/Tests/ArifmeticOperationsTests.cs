using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryProject;

namespace Tests
{
    [TestClass]
    public class ArifmeticOperationsTests
    {
        private int A { get; set; }
        private int B { get; set; }

        [TestMethod]
        public void TstNumbersSum()
        {
            A = 10;
            B = 20;
            Assert.AreEqual(A + B, ArifmeticOperations.NumbersSum(A, B));
        }
        [TestMethod]
        public void TstNumbersSubstraction()
        {
            A = 5;
            B = 2;
            Assert.AreEqual(A - B, ArifmeticOperations.NumbersSubtraction(A, B));
        }
        [TestMethod]
        public void TstNumbersDivision()
        {
            A = 20;
            B = 3;
            Assert.AreEqual(A / B, ArifmeticOperations.NumbersDivision(A, B)); //Demo mistake
        }
        [TestMethod]
        public void TstNumbersMultiplication()
        {
            A = 200;
            B = 100;
            Assert.AreEqual(A * B, ArifmeticOperations.NumbersMultiplication(A, B));
        }
    }
}

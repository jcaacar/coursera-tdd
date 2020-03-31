using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using camel = CourseraTDD.CamelCase;

namespace CourseraTDDTEST.CamelCase
{
    [TestFixture]
    public class CamelCaseTest
    {
        private camel.CamelCase camelCaseSolution;

        [OneTimeSetUp]
        public void Setup()
        {
            camelCaseSolution = new camel.CamelCase();
        }

        [Test]
        public void WithoutUpperSingleTest()
        {
            var result = camelCaseSolution.Convert("nome");

            var expected = new List<string> { "nome" };

            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void WithUpperSingleTest()
        {
            var result = camelCaseSolution.Convert("Nome");

            var expected = new List<string> { "nome" };
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithoutUpperCompoundTest()
        {
            var result = camelCaseSolution.Convert("nomeComposto");

            var expected = new List<string> { "nome", "composto" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithUpperCompoundTest()
        {
            var result = camelCaseSolution.Convert("NomeComposto");

            var expected = new List<string> { "nome", "composto" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymSingleTest()
        {
            var result = camelCaseSolution.Convert("CPF");

            var expected = new List<string> { "CPF" };
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymInTheEndTest()
        {
            var result = camelCaseSolution.Convert("numeroCPF");

            var expected = new List<string> { "numero", "CPF" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymInTheMiddleTest()
        {
            var result = camelCaseSolution.Convert("numeroCPFcontribuinte");

            var expected = new List<string> { "numero", "CPF", "contribuinte" };
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void NumbersInTheMiddleTest()
        {
            var result = camelCaseSolution.Convert("recupera10primeiros");

            var expected = new List<string> { "recupera", "10", "primeiros" };
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithNumberTest()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.Convert("10Primeiros"));
        }

        [Test]
        public void WithSpecialCharacerTest()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.Convert("nome#Composto"));
        }
    }
}
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using CourseraTDD;

namespace CourseraTDDTEST
{
    [TestFixture]
    public class CamelCaseTest
    {
        private CamelCaseImpl camelCaseSolution;

        [OneTimeSetUp]
        public void Setup()
        {
            camelCaseSolution = new CamelCaseImpl();
        }

        [Test]
        public void WithoutUpperSingleTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("nome");

            var expected = new List<string> { "nome" };

            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void WithUpperSingleTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("Nome");

            var expected = new List<string> { "nome" };
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithoutUpperCompoundTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("nomeComposto");

            var expected = new List<string> { "nome", "composto" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithUpperCompoundTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("NomeComposto");

            var expected = new List<string> { "nome", "composto" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymSingleTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("CPF");

            var expected = new List<string> { "CPF" };
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymInTheEndTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("numeroCPF");

            var expected = new List<string> { "numero", "CPF" };
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymInTheMiddleTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("numeroCPFcontribuinte");

            var expected = new List<string> { "numero", "CPF", "contribuinte" };
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void NumbersInTheMiddleTest()
        {
            var result = camelCaseSolution.ConverterCamelCase("recupera10primeiros");

            var expected = new List<string> { "recupera", "10", "primeiros" };
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void StartWithNumberTest()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.ConverterCamelCase("10Primeiros"));
        }

        [Test]
        public void WithSpecialCharacerTest()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.ConverterCamelCase("nome#Composto"));
        }
    }
}
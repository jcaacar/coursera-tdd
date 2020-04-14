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
        public void OnlyOneWordLowercase()
        {
            var result = camelCaseSolution.Convert("nome");

            var expected = new List<string> { "nome" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void OnlyOneWordWithFirstUppercase()
        {
            var result = camelCaseSolution.Convert("Nome");

            var expected = new List<string> { "nome" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void CompoundWithFirstLowercase()
        {
            var result = camelCaseSolution.Convert("nomeComposto");

            var expected = new List<string> { "nome", "composto" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void CompoundWithFirstUppercase()
        {
            var result = camelCaseSolution.Convert("NomeComposto");

            var expected = new List<string> { "nome", "composto" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void OnlyAcronym()
        {
            var result = camelCaseSolution.Convert("CPF");

            var expected = new List<string> { "CPF" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void EndWithAnAcronym()
        {
            var result = camelCaseSolution.Convert("numeroCPF");

            var expected = new List<string> { "numero", "CPF" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void AcronymInTheMiddle()
        {
            var result = camelCaseSolution.Convert("numeroCPFcontribuinte");

            var expected = new List<string> { "numero", "CPF", "contribuinte" };
            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void ManyAcronyms()
        {
            var result = camelCaseSolution.Convert("numeroCPFeRGcontribuintePF");

            var expected = new List<string> { "numero", "CPF", "e", "RG", "contribuinte", "PF" };
            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void NumbersInTheMiddle()
        {
            var result = camelCaseSolution.Convert("recupera10primeiros");

            var expected = new List<string> { "recupera", "10", "primeiros" };

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(expected.All(i => result.Contains(i)));
        }

        [Test]
        public void CantStartWithNumber()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.Convert("10Primeiros"));
        }

        [Test]
        public void CantSpecialCharacter()
        {
            Assert.Throws<ArgumentException>(() => camelCaseSolution.Convert("nome#Composto"));
        }
    }
}
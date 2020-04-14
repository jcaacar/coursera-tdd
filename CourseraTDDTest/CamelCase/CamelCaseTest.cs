using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using cc = CourseraTDD.CamelCase;

namespace CourseraTDDTEST.CamelCase
{
    [TestFixture]
    public class CamelCaseTest
    {
        private cc.CamelCase camelCase;

        [OneTimeSetUp]
        public void Setup()
        {
            camelCase = new cc.CamelCase();
        }

        [Test]
        public void OnlyOneWordLowercase()
        {
            var result = camelCase.Convert("nome");

            var expected = new List<string> { "nome" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void OnlyOneWordWithFirstUppercase()
        {
            var result = camelCase.Convert("Nome");

            var expected = new List<string> { "nome" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CompoundWithFirstLowercase()
        {
            var result = camelCase.Convert("nomeComposto");

            var expected = new List<string> { "nome", "composto" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CompoundWithFirstUppercase()
        {
            var result = camelCase.Convert("NomeComposto");

            var expected = new List<string> { "nome", "composto" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void OnlyAcronym()
        {
            var result = camelCase.Convert("CPF");

            var expected = new List<string> { "CPF" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EndWithAnAcronym()
        {
            var result = camelCase.Convert("numeroCPF");

            var expected = new List<string> { "numero", "CPF" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void AcronymInTheMiddle()
        {
            var result = camelCase.Convert("numeroCPFcontribuinte");

            var expected = new List<string> { "numero", "CPF", "contribuinte" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ManyAcronyms()
        {
            var result = camelCase.Convert("numeroCPFeRGcontribuintePF");

            var expected = new List<string> { "numero", "CPF", "e", "RG", "contribuinte", "PF" };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void NumbersInTheMiddle()
        {
            var result = camelCase.Convert("recupera10primeiros");

            var expected = new List<string> { "recupera", "10", "primeiros"};

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CantStartWithNumber()
        {
            Assert.Throws<ArgumentException>(() => camelCase.Convert("10Primeiros"));
        }

        [Test]
        public void CantSpecialCharacter()
        {
            Assert.Throws<ArgumentException>(() => camelCase.Convert("nome#Composto"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TDD_Katas_project.FizzBuzzKata
{
    [TestFixture]
    [Category("TheFizzBuzzKata")]
    public class TestFizzBuzz
    {
        [Test]
        [TestCaseSource("MuliplesOfThreeOnly")]
        public void ShouldPrintFizzIfNubmerCanBeDivideByThree(int mulipiOfThree)
        {
            var fizzBuzzGame = new FizzBuzzGame();

            var output = fizzBuzzGame.CheckNuber(mulipiOfThree);

            Assert.AreEqual("Fizz", output);
        }

        [Test]
        [TestCaseSource("MulipliesOfFiveOnly")]
        public void ShouldPrintBuzzIfNubmerCanBeDivideByFive(int mulipiOfFive)
        {
            var fizzBuzzGame = new FizzBuzzGame();

            var output = fizzBuzzGame.CheckNuber(mulipiOfFive);

            Assert.AreEqual("Buzz", output);
        }

        [Test]
        [TestCaseSource("MuliplesOfThreeAndFive")]
        public void ShouldPrintBuzzIfNubmerCanBeDivideByFiveAndThree(int muliplieOfThreeAndFive)
        {
            var fizzBuzzGame = new FizzBuzzGame();

            var output = fizzBuzzGame.CheckNuber(muliplieOfThreeAndFive);

            Assert.AreEqual("FizzBuzz", output);
        }

        [Test]
        [TestCaseSource("SomeOtherNumbers")]
        public void ShouldPrintNumberAsString(int someOrdinaryNumber)
        {
            var fizzBuzzGame = new FizzBuzzGame();

            var output = fizzBuzzGame.CheckNuber(someOrdinaryNumber);

            Assert.AreEqual(someOrdinaryNumber.ToString(), output);
        }
        
        public IEnumerable<int> MuliplesOfThreeOnly
        {

            get { return Enumerable.Range(1, 100).Where(x => x % 3 == 0 && x % 5 > 0); }

        }

        public IEnumerable<int> MulipliesOfFiveOnly
        {

            get { return Enumerable.Range(1, 100).Where(x => x % 5 == 0 && x % 3 > 0); }

        }
        public IEnumerable<int> MuliplesOfThreeAndFive
        {

            get { return Enumerable.Range(1, 100).Where(x => x % 5 == 0 && x % 3 == 0); }
        }

        public IEnumerable<int> SomeOtherNumbers
        {

            get { return Enumerable.Range(1, 100).Where(x => x % 5 > 0 && x % 3 > 0); }
        }
    }
}


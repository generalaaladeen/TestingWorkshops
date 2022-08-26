using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop.Test
{
    public class StringCalculatorTest
    {
        [Test]
        public void ShouldAddOperationWithEmptyParameterReturnZero()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add(string.Empty), Is.EqualTo(0));
        }

        [Test]
        [TestCase("1")]
        [TestCase("12")]
        [TestCase("3")]
        public void ShouldAddOperationWithParameterReturnSum(string value)
        {
            StringCalculator stringCalculator = new StringCalculator();


            Assert.That(stringCalculator.Add(value), Is.EqualTo(Convert.ToInt32(value)));
        }

        [TestCase("jdisaoidsnaoidinsa")]
        [TestCase("sadsadsa")]
        [TestCase("ffff")]
        public void ShouldAddOperationWithBrainlessParameterReturnException(string value)
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(() => stringCalculator.Add(value), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void ShouldAddOperationWithTwoPartValueInParametersReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add("1,2"), Is.EqualTo(3));
        }

        [Test]
        public void ShouldAddOperationWithThreePartValueInParametersReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add("1,2,3"), Is.EqualTo(6));
        }

        [Test]
        public void ShouldAddOperationWithManyPartValueInParametersReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add("1,2,3,4,5,6"), Is.EqualTo(21));
        }

        [Test]
        public void ShouldAddOperationWithNewLineOperatorInParameterReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add("1\n2"), Is.EqualTo(3));
        } 
        [Test]
        [TestCase("//;\n1;2")]
        [TestCase("//>\n1>2")]
        [TestCase("//@\n1@2")]
        public void ShouldAddOperationWithCustomSeparatorInParameterReturnSum(string value)
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.That(stringCalculator.Add(value), Is.EqualTo(3));
        }

        [Test]
        public void ShouldAddOperationWithNegativeInParameterValueReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();

            Assert.Multiple(() =>
            {
                Assert.That(() => stringCalculator.Add("-1"), Throws.Exception);
                Assert.That(() => stringCalculator.Add("-1"), Throws.Exception.With.Message.Contains("Negatives not allowed"));
                Assert.That(() => stringCalculator.Add("-1"), Throws.Exception.With.Message.Contains("-1"));
                Assert.That(() => stringCalculator.Add("-1,-2"), Throws.Exception.With.Message.Contains("-1,-2"));
            });
            
        } 
        
        [Test]
        public void ShouldAddOperationWithValueGreaterThan1000InParameterShouldSkipThisValue()
        {
            StringCalculator stringCalculator = new StringCalculator();
            Assert.Multiple(() =>
            {
                Assert.That(stringCalculator.Add("1,2,1000"), Is.EqualTo(3));
                Assert.That(stringCalculator.Add("1\n2\n1000"), Is.EqualTo(3));
                Assert.That(stringCalculator.Add("//@\n1@2@1000"), Is.EqualTo(3));
            });
            
        } 
        
        [Test]
        public void ShouldAddOperationWithManyDeclaredDelimetersReturnSum()
        {
            StringCalculator stringCalculator = new StringCalculator();
            Assert.Multiple(() =>
            {
                Assert.That(stringCalculator.Add("//[@][#]\n1@2#1000"), Is.EqualTo(3));
                Assert.That(stringCalculator.Add("//[@@@]\n1@@@2@@@1000"), Is.EqualTo(3));
                Assert.That(stringCalculator.Add("//[><]\n1><2><3"), Is.EqualTo(6));
            });
            
        }


       
    }
}

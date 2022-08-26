using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            int result = 0;

            if (string.IsNullOrEmpty(numbers))
                return result;

            IEnumerable<string> numberCollection = new List<string>();

            if (numbers.StartsWith("//"))
            {
                var delimitersCollection = GetDelimiters(numbers.Substring(2));
                int endOfSeparatorIndex = numbers.IndexOf('\n');

                var seprator = numbers.Substring(2, endOfSeparatorIndex).ToCharArray();
                if (delimitersCollection.Count() == 0)
                    numberCollection = numbers.Substring(endOfSeparatorIndex + 1).Split(seprator.FirstOrDefault());
                else
                    numberCollection = numbers.Substring(endOfSeparatorIndex + 1).Split(delimitersCollection.ToArray(), StringSplitOptions.None);
            }
            else
            {
                numberCollection = numbers.Split(',', '\n').ToList();
            }

            List<int> negativeNumbers = new List<int>();

            foreach (string number in numberCollection)
            {
                if (!int.TryParse(number, out int expectedResult))
                    throw new ArgumentException();

                if (expectedResult < 0)
                    negativeNumbers.Add(expectedResult);

                if (expectedResult < 1000)
                    result += expectedResult;
            }

            if (negativeNumbers.Count > 0)
                throw new Exception($"Negatives not allowed. {String.Join(",", negativeNumbers)}");

            return result;
        }

        private List<string> GetDelimiters(string value)
        {
            List<string> result = new();
            string stringValue = "";
            foreach (char @char in value)
            {
                if (@char == '[')
                    continue;

                if (@char == ']')
                {
                    result.Add(stringValue);
                    stringValue = "";
                    continue;
                }
                stringValue += @char;
            }

            return result;
        }

    }
}

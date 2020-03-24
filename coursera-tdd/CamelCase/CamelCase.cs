using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTDD.CamelCase
{
    public class CamelCase
    {
        public List<string> Convert(string original)
        {
            var result = new List<string>();

            if (char.IsDigit(original[0]))
                throw new ArgumentException("The string can't start with number.");

            var currentWord = original[0].ToString();

            for (int i = 1; i < original.Length; i++)
            {
                var c = original[i];

                if (!char.IsLetterOrDigit(c))
                    throw new ArgumentException("The string can't have symbols.");

                HandleCharToCurrentWord(c, result, ref currentWord);
            }

            if (currentWord.Any())
            {
                AddWordToResult(currentWord, result);
            }

            return result;
        }

        private void HandleCharToCurrentWord(char c, List<string> result, ref string currentWord)
        {
            var isUpper = char.IsUpper(c);
            var isDigit = char.IsDigit(c);

            var lastIsDigit = char.IsDigit(currentWord.Last());
            var lastIsUpperCase = char.IsUpper(currentWord.Last());

            if (!isUpper && !IsAcronym(currentWord) && (!isDigit && !IsOnlyDigit(currentWord)) || (isUpper && lastIsUpperCase) || isDigit && lastIsDigit)
            {
                currentWord += c;
            }
            else
            {
                AddWordToResult(currentWord, result);

                currentWord = c.ToString();
            }
        }

        private void AddWordToResult(string currentWord, List<string> result)
        {
            bool isAcronym = IsAcronym(currentWord);
            result.Add(isAcronym ? currentWord : currentWord.ToLower());
        }

        private bool IsAcronym(string str)
        {
            return str.Length > 1 && str.All(v => char.IsUpper(v));
        }

        private bool IsOnlyDigit(string str)
        {
            return str.All(v => char.IsDigit(v));
        }
    }

}
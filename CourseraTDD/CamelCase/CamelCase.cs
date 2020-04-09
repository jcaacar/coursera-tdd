using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTDD.CamelCase
{
    public class CamelCase
    {
        public List<string> Convert(string original)
        {
            ValidateStartWithoutNumber(original);
            
            var result = new List<string>();
            var currentWord = original[0].ToString();

            for (int i = 1; i < original.Length; i++)
            {
                var character = original[i];

                ValidadeLetterOrDigit(character);

                HandleCharToCurrentWord(character, result, ref currentWord);
            }

            if (currentWord.Any())
            {
                AddWordToResult(currentWord, result);
            }

            return result;
        }

        private void ValidateStartWithoutNumber(string original)
        {
            if (char.IsDigit(original[0]))
                throw new ArgumentException("The string can't start with number.");
        }

        private void ValidadeLetterOrDigit(char character)
        {
            if (!char.IsLetterOrDigit(character))
                throw new ArgumentException("The string can't have symbols.");
        }

        private void HandleCharToCurrentWord(char character, List<string> result, ref string currentWord)
        {
            var isUpper = char.IsUpper(character);
            var isDigit = char.IsDigit(character);

            var lastIsDigit = char.IsDigit(currentWord.Last());
            var lastIsUpperCase = char.IsUpper(currentWord.Last());

            var isUpperAndLastIsUpper = isUpper && lastIsUpperCase;
            var isDigitAndLastIsDigit = isDigit && lastIsDigit;

            var isNotUpperAndNotAcronmy = !isUpper && !IsAcronym(currentWord);
            var isNotDigitAndNotOnlyDigit = !isDigit && !IsOnlyDigit(currentWord);
            
            if ((isUpperAndLastIsUpper || isDigitAndLastIsDigit) || (isNotUpperAndNotAcronmy && isNotDigitAndNotOnlyDigit))
            {
                currentWord += character;
            }
            else
            {
                AddWordToResult(currentWord, result);

                currentWord = character.ToString();
            }
        }

        private void AddWordToResult(string currentWord, List<string> result)
        {
            bool isAcronym = IsAcronym(currentWord);
            result.Add(isAcronym ? currentWord : currentWord.ToLower());
        }

        private bool IsAcronym(string value)
        {
            return value.Length > 1 && value.All(c => char.IsUpper(c));
        }

        private bool IsOnlyDigit(string value)
        {
            return value.All(c => char.IsDigit(c));
        }
    }

}
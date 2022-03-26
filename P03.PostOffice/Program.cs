using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace P03.PostOffice
{
    class Word 
    {
        public Word(char fisrtLettter, int wordLength)
        {
            this.FirstLetter = fisrtLettter;
            this.WordLenght = wordLength;
        }
        public char FirstLetter { get; set; }

        public int WordLenght { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Word> wordsFirstLettersAndThierLength = new List<Word>();

            string[] parts = Console.ReadLine()
                .Split('|', StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string partOne = parts[0];
            string partTwo = parts[1];
            string partThree = parts[2];

            ExtractAllCapitalLettersFromFirstPart(partOne, wordsFirstLettersAndThierLength);
            ExtractAllWordsLengthsFromSecondtPart(partTwo, wordsFirstLettersAndThierLength);
            DisplayAllValidWordFromPArtThree(partThree, wordsFirstLettersAndThierLength);
        }

        static void ExtractAllCapitalLettersFromFirstPart(string partOne, List<Word> wordsFirstLettersAndThierLength)
        {
            string firstPartPattern = @"([#$%*&]{1})(?<letters>[A-Z]+)(\1)";

            Regex firsPartRegex = new Regex(firstPartPattern);

            Match matchLetters = firsPartRegex.Match(partOne);

            string letters = matchLetters.Groups["letters"].Value.ToString();

            foreach (char letter in letters)
            {
                Word currFirstLetter = new Word(letter, 0);
                wordsFirstLettersAndThierLength.Add(currFirstLetter);
            }
        }

        static void ExtractAllWordsLengthsFromSecondtPart(string partTwo, List<Word> wordsFirstLettersAndThierLength)
        {
            string secondPartPatter = @"(?<letterValue>[0-9]{2}):(?<wordLength>[0-9]{2})";

            Regex secondPartRegex = new Regex(secondPartPatter);

            MatchCollection matches = secondPartRegex.Matches(partTwo);

            foreach (Match match in matches)
            {
                char firstLetter = (char)(Convert.ToInt32(match.Groups["letterValue"].Value));

                if (wordsFirstLettersAndThierLength.Any(x => x.FirstLetter == firstLetter))
                {
                    int wordLength = Convert.ToInt32(match.Groups["wordLength"].Value);
                    int index = wordsFirstLettersAndThierLength.FindIndex(x => x.FirstLetter == firstLetter);
                   wordsFirstLettersAndThierLength[index].WordLenght = wordLength;
                } 
            }
        }

        static void DisplayAllValidWordFromPArtThree (string partThree, List<Word> wordsFirstLettersAndThierLength)
        {
            foreach (var wordInfo in wordsFirstLettersAndThierLength)
            {
                char firstLetter = wordInfo.FirstLetter;
                int wordLength = Convert.ToInt32(wordInfo.WordLenght);

                string partThreePattern = $"(^|\\s)([{firstLetter}][!-~]{{{wordLength}}}\\b)";
                Regex partThreeRegex = new Regex(partThreePattern);

                Match match = partThreeRegex.Match(partThree);

                if (match.Success)
                {
                    Console.WriteLine(match.Groups[2].Value);
                }
            }
        }
    }
}

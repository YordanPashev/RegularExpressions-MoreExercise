using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace P02.RageQuit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<char> uniqueSymbolsList = new List<char>();
            string pattern = @"((?<string>[ -\/:-~]+)(?<repNum>[0-9]+))";

            Regex regex = new Regex(pattern);

            string input = Console.ReadLine();

            MatchCollection matchCollectionOfStrings = regex.Matches(input);
            StringBuilder rageMessage = new StringBuilder();

            foreach (Match match in matchCollectionOfStrings)
            {
                string currString = match.Groups["string"].Value;
                int repNum = int.Parse(match.Groups["repNum"].Value);

                foreach (char ch in currString)
                {
                    if (repNum > 0)
                    {
                        AddUniqueSymbols(ch, uniqueSymbolsList);
                    }
                }

                string currRageString = GetRageString(currString, repNum);
                rageMessage.Append(currRageString);
            }

            Console.WriteLine($"Unique symbols used: {uniqueSymbolsList.Count}");
            Console.WriteLine(rageMessage);

        }
        static string GetRageString(string currString, int repNum)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= repNum; i++)
            {
                result.Append(currString.ToUpper());
            }

            return result.ToString();
        }

        static void AddUniqueSymbols(char ch, List<char> uniqueSymbolsList)
        {
            if (char.IsLetter(ch))
            {
                char currChar = Char.ToLower(ch);

                if (!uniqueSymbolsList.Contains(currChar))
                {
                    uniqueSymbolsList.Add(currChar);
                }
            }

            else
            {
                if (!uniqueSymbolsList.Contains(ch))
                    uniqueSymbolsList.Add(ch);
            }
        }
    }
}

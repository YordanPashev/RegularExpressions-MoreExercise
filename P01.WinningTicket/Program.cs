using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace P01.WinningTicket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] tickestsList = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            foreach (string ticket in tickestsList)
            {
                if (ticket.Length == 20)
                {
                    string[] ticketParts = SplitTicketsInTwoHalfs(ticket);
                    CheckForMatches(ticketParts, ticket);
                }

                else
                {
                    Console.WriteLine("invalid ticket");
                }
            }
        }

        static string[] SplitTicketsInTwoHalfs(string tickets)
        {
            string leftHalf = tickets.Substring(0, 10);
            string rigthHalf = tickets.Substring(10, 10);

            return new string[] { leftHalf, rigthHalf };
        }

        static void CheckForMatches(string[] ticketParts, string ticket)
        {
            string pattern = @"[@]{6,}|[#]{6,}|[$]{6,}|[\^]{6,}";
            Regex winningPattern = new Regex(pattern);

            string leftHalf = ticketParts[0];
            string rigthHalf = ticketParts[1];

            Match matchCollectionLeftHalf = winningPattern.Match(leftHalf);
            Match matchCollectionRightHalf = winningPattern.Match(rigthHalf);

            if (matchCollectionLeftHalf.Success && matchCollectionRightHalf.Success)
            {
                DisplayMatchresult(matchCollectionLeftHalf, matchCollectionRightHalf, ticket);
            }

            else
            {
                Console.WriteLine($"ticket \"{ ticket}\" - no match");
            }
        }

        static void DisplayMatchresult(Match matchCollectionLeftHalf, Match matCollectionRightHalf, string ticket)
        {
            string extractPattern = matchCollectionLeftHalf.Value.ToString();
            int winningSymbolCount = extractPattern.Length;
            char matchSymbol = extractPattern[0];

            if (matchCollectionLeftHalf.Length == 10 && matCollectionRightHalf.Length == 10)
            {
                Console.WriteLine($"ticket \"{ticket}\" - {10}{matchSymbol} Jackpot!");
            }

            else
            {
                if (matchCollectionLeftHalf.Length <= matCollectionRightHalf.Length)
                {
                    Console.WriteLine($"ticket \"{ticket}\" - {matchCollectionLeftHalf.Length}{matchSymbol}");
                }

                else
                { 
                    Console.WriteLine($"ticket \"{ticket}\" - {matCollectionRightHalf.Length}{matchSymbol}");
                }
            }
        }
    }
}

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P04.Santa_sSecretHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int decriptionValueKey = int.Parse(Console.ReadLine());

            string validInfoPatter = @"@(?<name>[A-Za-z]+)([^@\-!\:\>]*?)!(?<behavior>[G|N])!";
            Regex regex = new Regex(validInfoPatter);

            string currChildInfo;

            while ((currChildInfo = Console.ReadLine()) != "end")
            {
                string decryptedInfo = Decrypt(currChildInfo, decriptionValueKey);

                Match match = regex.Match(decryptedInfo);

                if (match.Success)
                {
                    string behavior = match.Groups["behavior"].Value;

                    if (behavior == "G")
                    {
                        string name = match.Groups["name"].Value;

                        Console.WriteLine(name);
                    }
                }
            }

            static string Decrypt(string input, int decriptionValueKey)
            {
                StringBuilder decryptedInfo = new StringBuilder();
                foreach (char symbol in input)
                {
                    decryptedInfo.Append((char)(symbol - decriptionValueKey));
                }

                return decryptedInfo.ToString();
            }
        }
    }
}

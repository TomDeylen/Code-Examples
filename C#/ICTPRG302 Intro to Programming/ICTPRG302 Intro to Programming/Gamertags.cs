using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ICTPRG302_Intro_to_Programming
{
    class Gamertags
    {
        private string[] gamerTagList = { };
        int amountToShow = 5;
        bool uppercase = true;

        public void LoadGamertags()
        {
            gamerTagList = File.ReadAllLines("../Gamertags.txt");
        }

        public void PrintAllGamerTags()
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("All GamerTags");
            Console.WriteLine("=======================");

            int lineNumber = 1;
            foreach (string s in gamerTagList)
            {
                string currentTag = s;
                if (uppercase == true)
                    currentTag = currentTag.ToUpper();
                Console.WriteLine(lineNumber.ToString() + ") " + currentTag);
                lineNumber = lineNumber + 1;
                if (amountToShow < lineNumber)
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void PrintGamertagsEndingWithNumber()
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("All GamerTags");
            Console.WriteLine("=======================");

            int lineNumber = 1;
            foreach (string s in gamerTagList)
            {
                if ((s.Length > 0) && Char.IsNumber(s, s.Length - 1))
                {
                    string currentTag = s;
                    if (uppercase == true)
                        currentTag = currentTag.ToUpper();
                    Console.WriteLine(lineNumber.ToString() + ") " + currentTag);
                    lineNumber = lineNumber + 1;
                    if (lineNumber == amountToShow)
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void PrintGamertagsNotStartingWithNumberOrLetter()
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("All GamerTags");
            Console.WriteLine("=======================");

            int lineNumber = 1;
            foreach (string s in gamerTagList)
            {
                if ((s.Length > 0) && Char.IsLetterOrDigit(s, 0) == false)
                {
                    string currentTag = s;
                    if (uppercase == true)
                        currentTag = currentTag.ToUpper();
                    Console.WriteLine(lineNumber.ToString() + ") " + currentTag);
                    lineNumber = lineNumber + 1;
                    if (lineNumber == amountToShow)
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

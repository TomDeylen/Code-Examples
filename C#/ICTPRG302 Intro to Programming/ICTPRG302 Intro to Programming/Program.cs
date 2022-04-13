using System;
using System.IO;

namespace ICTPRG302_Intro_to_Programming
{
	class Program
	{
		/// <summary>
		/// This is the starting location for the program.
		/// </summary>
		/// <param name="args">
		/// A list of strings passed in to the program
		/// from the command-line when it was started
		/// </param>
		static void Main(string[] args)
		{
            Gamertags gamertags = new Gamertags();
            gamertags.LoadGamertags();
            //gamertags.PrintAllGamerTags();
            gamertags.PrintAllGamerTags();
        }
	}
}

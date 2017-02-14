using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PlatformSpellCheck;

namespace OddSpell
{
	class Program
	{
		static void Main(string[] args)
		{

			// if there is a custom dictionary in %AppData%\Microsoft\Spelling\ 
			// it will be used automatically, 
			
			// todo - might be nice to be able to easily add a word to the custom dictionary
			// through spell check OddSpell
			// might also be nice to make the name more user friendly 
			try
			{
				using (var spelling = new SpellChecker())
				{
					var lineNo = 0;
					IEnumerable<string> lines = Enumerable.Empty<string>();

					if (Console.IsInputRedirected)
					{
						lines = ReadFromInput();
					}
					else if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]) && File.Exists(args[0]))
					{
						lines = File.ReadLines(args[0]);
					}
					else
					{
						System.Console.WriteLine(@"Usage: 

 <Your Input> | OddSpell
	OR
 OddSpell ""Path to File to Check""");
					}

					var query = from line in lines
									let currentLine = ++lineNo
									where !string.IsNullOrWhiteSpace(line)
									from error in spelling.Check(line)
									where error.RecommendedAction == RecommendedAction.GetSuggestions
									let misspelling = line.Substring((int)error.StartIndex, (int)error.Length)
									let suggestions = string.Join(", ", spelling.Suggestions(misspelling))
									select $"{currentLine}:{error.StartIndex + 1} {misspelling} -> {suggestions}";

					foreach (var el in query)
					{
						System.Console.WriteLine(el);
					}
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
		}

		private static IEnumerable<string> ReadFromInput()
		{
			using (var lstream = new StreamReader(Console.OpenStandardInput()))
			{
				var line = string.Empty;
				while ((line = lstream.ReadLine()) != null)
				{
					yield return line;
				}
			}
		}
	}
}

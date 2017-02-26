using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using PlatformSpellCheck;

namespace OddSpell
{
   class Program
   {
      static void Main(string[] args)
      {
         // custom dictionary in %AppData%\Microsoft\Spelling\en-US\default.dic
         if (args.Length > 0 && args[0].ToLower() == "add")
         {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Spelling\en-US\default.dic");
            File.AppendAllLines(path, args.Skip(1), Encoding.Unicode);
         }
         else
         {
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
SPELLCHECK
 <Your Input> | OddSpell
OR
 OddSpell ""Path to File to Check""
 
ADD WORDS
 OddSpell add WORD WORD...    Will add a command separated list of words to the default use dictionary.");
                  }

                  var query = from line in lines
                              let currentLine = ++lineNo
                              where !string.IsNullOrWhiteSpace(line)
                              from result in spelling.Check(line)
                              where result.RecommendedAction != RecommendedAction.None
                              let error = line.Substring((int)result.StartIndex, (int)result.Length)
                              let suggestions = GetSuggestions(result, spelling, error)
                              select $"{currentLine}:{result.StartIndex + 1} {error} -> {suggestions}";

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
      }

      private static string GetSuggestions(SpellingError result, SpellChecker spelling, string error)
      {
         var ret = string.Empty;
         switch (result.RecommendedAction)
         {
            case RecommendedAction.GetSuggestions:
               ret = string.Join(", ", spelling.Suggestions(error));
               break;
            case RecommendedAction.Replace:
               ret = result.RecommendedReplacement;
               break;
            case RecommendedAction.Delete:
               ret = "DELETE";
               break;
            case RecommendedAction.None:
            default:
               break;
         }



         return ret;
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

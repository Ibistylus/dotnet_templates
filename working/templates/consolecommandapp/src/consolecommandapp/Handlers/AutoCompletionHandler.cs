using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TemplateCommandLineApp.Handlers
{
    public class AutoCompletionHandler : IAutoCompleteHandler
    {
        private readonly string[] _additionalSeedStrings;

        public AutoCompletionHandler(string[] additionalSeedStrings)
        {
            _additionalSeedStrings = additionalSeedStrings;
        }

        public char[] Separators { get; set; } = {' ', '.', '/'};

        /// <summary>
        /// Gets suggestions for the command line
        /// </summary>
        /// <param name="text">Console strings typed to get suggestions</param>
        /// <param name="index"></param>
        /// <returns>String array of matches</returns>
        public string[] GetSuggestions(string text, int index)
        {
            var sug = new List<string>();
            try
            {
                sug.AddRange(new List<string> {"quit", "--help"});
                //Automatically and conveniently updates list with history
                sug.AddRange(ReadLine.GetHistory().ToImmutableHashSet().ToList());

                // NOTE: Add string collections to return suggestions.
                if (_additionalSeedStrings != null) sug.AddRange(_additionalSeedStrings);

                return sug.ToArray().Where(x => x.StartsWith(text)).ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return sug.ToArray();
        }
    }
}
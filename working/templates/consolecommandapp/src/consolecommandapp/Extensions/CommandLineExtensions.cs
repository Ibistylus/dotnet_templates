using System;
using System.Collections.Generic;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace TemplateCommandLineApp.Extensions
{
    public static class CommandLineExtensions
    {
        /// <summary>
        ///     Return an array of available commands. NOTE: This only works for attribute commands, not builder commands.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static string[] GetCommandNames(this CommandLineApplication app)
        {
            var names = new List<string>();

            foreach (var commandName in app.Commands) names.Add(commandName.Name);

            return names.ToArray();
        }


        /// <summary>
        ///     Takes a string and splits it into string array in preparation for CommandLindApplication
        ///     processing.
        /// </summary>
        /// <param name="commandLine">string to split</param>
        /// <returns>Args string array</returns>
        public static IEnumerable<string> SplitCommandLine(string commandLine)
        {
            var inQuotes = false;

            return commandLine.Split(c =>
                {
                    if (c == '\"')
                        inQuotes = !inQuotes;

                    return !inQuotes && c == ' ';
                })
                .Select(arg => TrimMatchingQuotes(arg.Trim(), '\"'))
                .Where(arg => !string.IsNullOrEmpty(arg));
        }

        public static IEnumerable<string> Split(this string str,
            Func<char, bool> controller)
        {
            var nextPiece = 0;

            for (var c = 0; c < str.Length; c++)
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }

            yield return str.Substring(nextPiece);
        }

        public static string TrimMatchingQuotes(this string input, char quote)
        {
            if (input.Length >= 2 &&
                input[0] == quote && input[input.Length - 1] == quote)
                return input.Substring(1, input.Length - 2);

            return input;
        }
    }
}
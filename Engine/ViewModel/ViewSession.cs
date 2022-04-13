using Engine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// TODO: create a class that is a List<string>, each index holding a line from filtered text based on the delimiter
// this will be used to display each line in the UI instead of Console.WriteLine()

namespace Engine.ViewModel
{
    public class ViewSession
    {
        public TextLog CurrentTextLog { get; set; }

        readonly string battleDelimiter = "::";
        readonly string npcDelimiter = "=:";
        readonly string playerDelimiterStart = ":'"; //start of name
        readonly string playerDelimiterEnd = "':"; //end of name

        public ViewSession()
        {
            CurrentTextLog = new TextLog();
            CurrentTextLog.FilePath = @"E:\ffxiv_logs_for_test\FINAL FANTASY XIV - A Realm Reborn\FFXIV_CHR004000174B48D772\log\00000000.log";
            CurrentTextLog.RawText = File.ReadAllText(CurrentTextLog.FilePath);
            CurrentTextLog.FilteredText = CleanRawText(CurrentTextLog.RawText);
        }

        public string CleanRawText(string text)
        {
            string pattern = "[^\\w\\s\\p{P}\\p{Sm}<>]+";
            string cleanText = Regex.Replace(text, pattern, String.Empty);
            return cleanText;
        }

        public void DisplayBattleLogs()
        {
            string[] battleSplit = CurrentTextLog.FilteredText.Split(
                battleDelimiter, 
                StringSplitOptions.RemoveEmptyEntries);

            int i = 0;
            foreach (string line in battleSplit)
            {
                i++;
                if (line.Contains("."))
                {
                    if (!line.EndsWith("."))
                    {
                        string trimmedLine = TrimCharactersAfterLastPeriod(line);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, npcDelimiter);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                        //Console.WriteLine("[BATTLE][{0}] {1}", i, trimmedLine);
                        //Trace.WriteLine(trimmedLine);
                        
                        //TODO: add trimmedLine to a list object (see TODO on top of page)
                    }
                }
                else if (line.Contains("!"))
                {
                    if (!line.EndsWith("!"))
                    {
                        string trimmedLine = TrimCharactersAfterLastExclamation(line);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, npcDelimiter);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                        //Trace.WriteLine(trimmedLine);

                        //TODO: add trimmedLine to a list object (see TODO on top of page)
                    }
                }
                else
                {
                    string trimmedLine = TrimDelimiterFrom(line, npcDelimiter);
                    trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                    trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                    //Trace.WriteLine(trimmedLine);

                    //TODO: add trimmedLine to a list object (see TODO on top of page)
                }

            }
        }

        private static string TrimCharactersAfterLastPeriod(string line)
        {
            int indexPeriod = line.LastIndexOf(".");
            if (indexPeriod != -1)
            {
                //Console.WriteLine(line.Substring(0, index+1));
                line = line.Substring(0, indexPeriod) + ".";
            }


            return line;
        }


        private static string TrimDelimiterFrom(string line, string delimiter)
        {
            int index = line.IndexOf(delimiter);
            if (index >= 0)
            {
                line = line.Substring(0, index);
            }
            return line;
        }

        private static string TrimCharactersAfterLastExclamation(string line)
        {
            int indexExclamation = line.LastIndexOf("!");

            //throws OutOfIndexException

            if (indexExclamation != -1)
            {
                //Console.WriteLine(line.Substring(0, indexExclamation+1));
                line = line.Substring(0, indexExclamation) + "!";
            }

            return line;
        }

    }
}

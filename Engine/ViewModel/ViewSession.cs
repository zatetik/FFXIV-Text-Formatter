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

        private readonly string battleDelimiter = "::";
        private readonly string npcDelimiter = "=:";
        private readonly string playerDelimiterStart = ":'"; //start of name
        private readonly string playerDelimiterEnd = "':"; //end of name


        private readonly char[] charsToTrim = new char[] { '0','1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                '#', '&', '>', '<','♂','♀',']','[','%','\\', ':','_','(','+','/', '@', '=', '*','~', '{', '}', '|' };

        // initialized in each option (battle, npc, player)
        public List<string> logLine;
        private string cleanedText;

        public ViewSession()
        {
            CurrentTextLog = new TextLog();
            CurrentTextLog.FilePath = @"E:\ffxiv_logs_for_test\FINAL FANTASY XIV - A Realm Reborn\FFXIV_CHR004000174B48D772\log\00000000.log";
            CurrentTextLog.RawText = File.ReadAllText(CurrentTextLog.FilePath);
            //CurrentTextLog.FilteredText = CleanRawText(CurrentTextLog.RawText);
            cleanedText = CleanRawText(CurrentTextLog.RawText);
            //DisplayBattleLogs();
            //DisplayNPCLogs();
            DisplayPlayerLogs();
        }

        public string CleanRawText(string text)
        {
            string pattern = "[^\\w\\s\\p{P}\\p{Sm}<>]+";
            string cleanText = Regex.Replace(text, pattern, String.Empty);
            return cleanText;
        }

        public void DisplayBattleLogs()
        {
            string[] battleSplit = cleanedText.Split(
                battleDelimiter, 
                StringSplitOptions.RemoveEmptyEntries);

            logLine = new List<string>();

            int i = 0;
            foreach (string line in battleSplit)
            {
                i++;
                if (line.Contains("."))
                {
                    if (!line.EndsWith("."))
                    {
                        string trimmedLine = TrimAllCharactersAfterLastExpression(".", line);
                        //string trimmedLine = TrimCharactersAfterLastPeriod(line);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, npcDelimiter);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                        //Console.WriteLine("[BATTLE][{0}] {1}", i, trimmedLine);
                        //Trace.WriteLine(trimmedLine);
                        
                        //logLine.Add(trimmedLine);

                        logLine.Add("[BATTLE] " + trimmedLine);
                        //TODO: add trimmedLine to a list object (see TODO on top of page)
                    }
                }
                else if (line.Contains("!"))
                {
                    if (!line.EndsWith("!"))
                    {
                        string trimmedLine = TrimAllCharactersAfterLastExpression("!", line);
                        //string trimmedLine = TrimCharactersAfterLastExclamation(line);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, npcDelimiter);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                        trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                        //Trace.WriteLine(trimmedLine);
                        //logLine.Add(trimmedLine);
                        logLine.Add("[BATTLE] " + trimmedLine);

                        //TODO: add trimmedLine to a list object (see TODO on top of page)
                    }
                }

                else
                {
                    string trimmedLine = TrimDelimiterFrom(line, npcDelimiter);
                    trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                    trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                    //Trace.WriteLine(trimmedLine);
                    //logLine.Add(trimmedLine);
                    logLine.Add("[BATTLE] " + trimmedLine);

                    //TODO: add trimmedLine to a list object (see TODO on top of page)
                }

            }

            //CurrentTextLog.FilteredText = cleanedText;
            CurrentTextLog.FilteredText = String.Join("\n", logLine);
            
        }

        void DisplayNPCLogs()
        {
            string[] npcSplit = cleanedText.Split(
                npcDelimiter,
                StringSplitOptions.RemoveEmptyEntries);

            logLine = new List<string>();
            int i = 0;
            foreach (string line in npcSplit)
            {
                i++;
                string trimmedLine = TrimAllCharactersAfterLastExpression(".", line);
                //string trimmedLine = TrimCharactersAfterLastPeriod(line);
                //trimmedLine = line.TrimEnd(charsToTrim);
                trimmedLine = TrimDelimiterFrom(trimmedLine, battleDelimiter);
                trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterEnd);
                trimmedLine = TrimDelimiterFrom(trimmedLine, playerDelimiterStart);
                logLine.Add("[NPC] " + trimmedLine);

            }
            CurrentTextLog.FilteredText = String.Join("\n", logLine);

        }

        void DisplayPlayerLogs()
        {
            string[] playerSplit = cleanedText.Split(
                playerDelimiterStart,
                StringSplitOptions.RemoveEmptyEntries);

            logLine = new List<string>();

            int i = 0;
            foreach (string line in playerSplit)
            {
                i++;
                //string trimmedLine = TrimCharactersAfterLastPeriod(line);
                string trimmedLine = line.TrimEnd(charsToTrim);
                trimmedLine = TrimDelimiterFrom(trimmedLine, npcDelimiter);
                trimmedLine = TrimDelimiterFrom(trimmedLine, battleDelimiter);
                logLine.Add("[PLAYER] " + trimmedLine);
            }
            CurrentTextLog.FilteredText = String.Join("\n", logLine);

        }


        private static string TrimAllCharactersAfterLastExpression(string expression, string line)
        {
            int indexPeriod = line.LastIndexOf(expression);
            if (indexPeriod != -1)
            {
                line = line.Substring(0, indexPeriod) + expression;
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

    }
}

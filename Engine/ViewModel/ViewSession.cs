using Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModel
{
    public class ViewSession
    {
        public TextLog CurrentTextLog { get; set; }

        public ViewSession()
        {
            CurrentTextLog = new TextLog();
            CurrentTextLog.FilePath = @"E:\ffxiv_logs_for_test\FINAL FANTASY XIV - A Realm Reborn\FFXIV_CHR004000174B48D772\log\00000000.log";
            CurrentTextLog.RawText = File.ReadAllText(CurrentTextLog.FilePath);
            CurrentTextLog.Test = "binding workd!";
            /*
            CurrentLog = new TextLog
            {
                // this should be user input later
                FilePath = @"E:\ffxiv_logs_for_test\FINAL FANTASY XIV - A Realm Reborn\FFXIV_CHR004000174B48D772\log\00000000.log",
                RawText = File.ReadAllText(CurrentLog.FilePath)
            };
            */
            
        }
    }
}

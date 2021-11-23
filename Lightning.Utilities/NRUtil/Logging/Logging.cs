using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    public static class Logging
    {
        public static string FileName { get; set; }
        public static void Log(string Text, string Component = null, MessageSeverity Severity = MessageSeverity.Message)
        {
#if DEBUG
            switch (Severity)
            {
                case MessageSeverity.Message:
                    LogText(Text, Component, Severity); 
                    return;
                case MessageSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    LogText(Text, Component, Severity);
                    return;
                case MessageSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    LogText(Text, Component, Severity);
                    return;
                // "BSOD" style for fatals
                case MessageSeverity.FatalError:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    LogText(Text, Component, Severity);
                    return;
            }
#else
            return;
#endif
        }

        public static void LogError(Error Err, string Component = null)
        {
#if DEBUG
            if (Component == null)
            {
                Log($"Error {Err.Id}: Severity {Err.Severity}: {Err.Name} ({Err.Description})", "Lightning Game Engine", Err.Severity);

                if (Err.BaseException != null)
                {
                    Log($"Base Exception: {Err}", "Lightning Game Engine");
                }

            }
            else
            {
                Log($"Error {Err.Id}: Severity {Err.Severity}: {Err.Name} ({Err.Description})", Component, Err.Severity);

                if (Err.BaseException != null)
                {
                    Log($"Base Exception: {Err}", "Lightning Game Engine");
                }
            }
#else
            return;
#endif
        }
        
        private static void LogText(string Text, string Component = null, MessageSeverity Severity = MessageSeverity.Message)
        {
#if DEBUG
            StringBuilder LogTextSB = new StringBuilder();

            string DateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // ISO date format
            // also a space
            LogTextSB.Append($"{DateString} ");
            
            if (Component == null) // todo: some NR stuff
            {
                LogTextSB.Append($"[NuRender]");
            }
            else
            {
                LogTextSB.Append($"[{Component}]");
            }

            LogTextSB.Append($" - {Text}");

            // Write the line we have built, and a newline, to the console.
            Console.WriteLine(LogTextSB.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
#else
            return;
#endif
        }
    }
}

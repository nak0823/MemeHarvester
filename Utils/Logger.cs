using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeHarvest.Utils
{
    public class Logger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message, bool writeToConsole)
        {
            string logEntry = $" {DateTime.Now.ToString("HH:mm:ss")} ~ {message}";

            if (writeToConsole)
                Colorful.Console.WriteLine(logEntry, Color.Aqua);

            try
            {
                using (StreamWriter writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Colorful.Console.WriteLine($" Error writing to log file: {ex.Message}", Color.Red);
            }
        }
    }
}

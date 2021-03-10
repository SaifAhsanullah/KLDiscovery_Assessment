using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KLDiscover_Assessment
{
    class FileWriter
    {
        public void WriteFile(string[] message, string outputPath)
        {
            using (StreamWriter file = File.AppendText(outputPath + "Output.csv"))
            {
                file.WriteLine($"{message[0]}" + "," + $"{message[1]}" + "," + $"{message[2]}");
                file.Flush();
            }
        }
    }
}

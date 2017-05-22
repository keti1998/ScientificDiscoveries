using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdzScientificDiscoveries
{
    static class Logger
    {//metod 
        public static void Log(string message)
        {
            using (FileStream fs = new FileStream("../../logging.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{DateTime.Now} - {message}");
                }
            }
        }
    }
}

using System;
using System.Configuration;
using System.IO;

namespace TextParser
{
    class Program
    {
        static void Main()
        {
            String inputFilePath = ConfigurationManager.AppSettings["InputFile"];

            using (StreamReader reader = File.OpenText(inputFilePath))
            {
                Parser parser = new Parser();
                parser.Parse(reader);
            }
            
            Console.ReadLine();
        }
    }
}

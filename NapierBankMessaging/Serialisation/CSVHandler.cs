using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NapierBankMessaging.Serialisation
{
    class CSVHandler
    {

        public static IDictionary<string, string> AbbreviationInput(string filePath)
        {

            IDictionary<string, string> AbbreviationDictionary = new Dictionary<string, string>();

            Regex CSVParser = new Regex(",");

            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] columns = CSVParser.Split(line);
                AbbreviationDictionary.Add(columns[0], columns[1]);
            }

            return AbbreviationDictionary;
        
        }
    }
}
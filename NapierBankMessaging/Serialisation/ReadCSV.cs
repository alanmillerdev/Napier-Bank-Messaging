﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NapierBankMessaging.Serialisation
{
    class ReadCSV
    {

        IDictionary<string, string> AbbreviationDictionary = new Dictionary<string, string>();

        public void CSVInput(string filePath)
        {

            Regex CSVParser = new Regex(",");

            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] columns = CSVParser.Split(line);
                AbbreviationDictionary.Add(columns[0], columns[1]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NapierBankMessaging.Serialisation
{
    class CSVHandler
    {

        //Abbreviation Input Method, used to create a dictionary that stores the Abbreviations used in the Abbriviation Check
        //During the Parse Process.
        public static IDictionary<string, string> AbbreviationInput(string filePath)
        {
            //Declares and Initalises the Abbreviation Dictionary.
            IDictionary<string, string> AbbreviationDictionary = new Dictionary<string, string>();

            //CSVParser holds a Regex used to allow commas within abbreviation outputs.
            Regex CSVParser = new Regex(",");

            //Declares and Initalises the lines string array that stores all of the lines read into the system from the file IO.
            string[] lines = System.IO.File.ReadAllLines(filePath);

            //For each entry in the array
            foreach (string line in lines)
            {
                //Split the line and store it
                string[] columns = CSVParser.Split(line);
                //Adds the column to the dictionary.
                AbbreviationDictionary.Add(columns[0], columns[1]);
            }

            //Returns the AbbreviationDictionary.
            return AbbreviationDictionary;
        
        }
    }
}
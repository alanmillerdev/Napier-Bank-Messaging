using System;
using System.IO;
using System.Text;

namespace NapierBankMessaging.Serialisation
{
    class TXTHandler
    {
        //TXTInput method, responsible for reading all the lines from a file with a passed in file path.
        public string [] TXTInput(string filePath)
        {

            //Holds all of the read in lines in a string array.
            string[] lines = System.IO.File.ReadAllLines(filePath);

            //Returns the passed in data.
            return lines;

        }
    }
}

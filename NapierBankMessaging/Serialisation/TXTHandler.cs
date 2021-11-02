using System;
using System.IO;
using System.Text;

namespace NapierBankMessaging.Serialisation
{
    class TXTHandler
    {
        public string [] TXTInput(string filePath)
        {

            string[] lines = System.IO.File.ReadAllLines(filePath);

            return lines;

        }
    }
}

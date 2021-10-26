using NapierBankMessaging.MessageTypes;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;

namespace NapierBankMessaging.Serialisation
{
    class JSONHandler
    {

        string dir = @"C:\Program Files\NapierBankMessaging";

        string applicationDataStorageLocation = @"C:\Program Files\NapierBankMessaging\NapierBankMessagingStorage.JSON";

        public string JSONInput(string filePath)
        {

            string jsonData = File.ReadAllText(filePath);

            return jsonData;

        }

        public List<Message> ReadApplicationData()
        {

            string jsonData = File.ReadAllText(applicationDataStorageLocation);

            List<Message> MessageList = JsonSerializer.Deserialize<List<Message>>(jsonData);

            return MessageList;

        }

        public void JSONOutput(List<Message> MessageList)
        {

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string jsonData = JsonSerializer.Serialize(MessageList);
            File.WriteAllText(applicationDataStorageLocation, jsonData);

        }
    }
}
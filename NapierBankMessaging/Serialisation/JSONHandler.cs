using NapierBankMessaging.MessageTypes;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace NapierBankMessaging.Serialisation
{
    public class JSONHandler
    {

        string applicationDataStorageLocation = @"C:\Users\alanm\Desktop\NapierBankMessaging\ApplicationData.JSON";

        public List<Message> ReadApplicationData()
        {

            string jsonData = File.ReadAllText(applicationDataStorageLocation);

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

            List<Message> MessageList = JsonConvert.DeserializeObject<List<Message>>(jsonData, settings);

            return MessageList;

        }
        
        public void JSONOutput(List<Message> MessageList)
        {

            string jsonData = string.Empty;

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

            jsonData = JsonConvert.SerializeObject(MessageList, settings);

            File.WriteAllText(applicationDataStorageLocation, jsonData);

        }
    }
}
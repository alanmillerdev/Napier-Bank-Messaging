using NapierBankMessaging.MessageTypes;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace NapierBankMessaging.Serialisation
{
    public class JSONHandler
    {

        string applicationDataStorageLocation = "ApplicationData.JSON";

        public List<Message> ReadApplicationData()
        {

            List<Message> MessageList = new List<Message>();

            try
            {
                string jsonData = File.ReadAllText(applicationDataStorageLocation);

                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

                MessageList = JsonConvert.DeserializeObject<List<Message>>(jsonData, settings);

            } catch (Exception e)
            {

            }

            return MessageList;

        }
        
        public void JSONOutput(List<Message> MessageList)
        {

            string jsonData = string.Empty;

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };

            jsonData = JsonConvert.SerializeObject(MessageList, settings);

            File.WriteAllText(applicationDataStorageLocation, jsonData);

        }
    }
}
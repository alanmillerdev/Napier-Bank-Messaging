using NapierBankMessaging.MessageTypes;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace NapierBankMessaging.Serialisation
{
    public class JSONHandler
    {

        //Declares and Initalises the applicationDataStorageLocation
        string applicationDataStorageLocation = "ApplicationData.JSON";

        //ReadApplicationData Method, this method is responsible for reading in data to the application from JSON.
        public List<Message> ReadApplicationData()
        {

            //Declares and Initialises MessageList which is responsible for holding the list of messages to be returned at the end of the process.
            List<Message> MessageList = new List<Message>();

            //Try, catch statement to catch any errors that can occur in the reading data process.
            try
            {
                //Declares and Initalises the jsonData variable that stores all of the read in data.
                string jsonData = File.ReadAllText(applicationDataStorageLocation);

                //Declares and Initalises settings variable that holds the settings for the JSON Serialiser. 
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

                //Deserialises the Message Objects into a MessageList.
                MessageList = JsonConvert.DeserializeObject<List<Message>>(jsonData, settings);

#pragma warning disable CS0168 // The variable 'e' is declared but never used
            } catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {

            }

            //Returns the message list.
            return MessageList;

        }
        
        //JSON Output Method, responsible for outputting the application data in JSON format.
        public void JSONOutput(List<Message> MessageList)
        {

            //Declares and Initalises jsonData variable as an empty string
            string jsonData = string.Empty;

            //Declares and Initalises settings variable that holds the settings for the JSON Serialiser. 
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };

            //sets the jsonData value to the serialised message list.
            jsonData = JsonConvert.SerializeObject(MessageList, settings);

            //Writes all of the text to the output file.
            File.WriteAllText(applicationDataStorageLocation, jsonData);

        }
    }
}
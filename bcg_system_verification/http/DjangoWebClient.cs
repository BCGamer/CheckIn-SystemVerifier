using System;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace bcg_system_verification
{
    class DjangoWebClient
    {
        public DjangoWebClient() 
        {
        }


        public bcgGETobject downloadServerData(string webAddress)
        {

            bcgGETobject values = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(webAddress);
                    values = JsonConvert.DeserializeObject<bcgGETobject>(response);
                }
            }
            catch (WebException ex)
            {
                // log the error
                Console.WriteLine("downloadServerData: Values failed to upload to server.\n");
                Console.WriteLine(ex.Message);
                throw;
            }

            return values;
        }

        public string uploadVerificationData(string webAddress, DjangoMessage message)
        {
            byte[] response = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    response = client.UploadValues(webAddress, message.get());
                }
            }
            catch (WebException ex)
            {
                // log the error
                Console.WriteLine("uploadVerificationData: Values failed to upload to server.\n");
                Console.WriteLine(ex.Message);
                throw;
            }

            return Encoding.Default.GetString(response);
        }

    }
    [DataContract]
    public class bcgGETobject
    {
        [DataMember]
        public string Uuid { get; set; }

        [DataMember]
        public string Ipaddress { get; set; }
    }

    class DjangoMessage
    {

        public DjangoMessage()
        {
            message = new NameValueCollection();
        }

        public void add(string key, string value)
        {
            message.Add(key, value);
        }

        public NameValueCollection get()
        {
            return message;
        }

        private NameValueCollection message;


    }
}

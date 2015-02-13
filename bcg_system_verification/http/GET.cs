using System;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace bcg_system_verification.http
{
    class get
    {
        public static void start()
        {
            //testGET();
            setVars();
            //bcgGET();
        }

        private static string getHTTP(string webServer)
        {
            string responseString = null;
            using (var client = new WebClient())
            {
                responseString = client.DownloadString(webServer);
            }
            return responseString;
        }

        private static void bcgGET()
        {
            string webServer = "http://checkin.bcgamer.lan/";
            string responseString = getHTTP(webServer);

            bcgGETobject getVARS = JsonConvert.DeserializeObject<bcgGETobject>(responseString);

            Globals.collection.Add("uuid", getVARS.uuid);
            Globals.collection.Add("ipaddress", getVARS.ipaddress);

            if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "uuid", getVARS.uuid);
            if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "ipaddress", getVARS.ipaddress);

        }

        [DataContract]
        public class bcgGETobject
        {
            [DataMember]
            public string uuid { get; set; }

            [DataMember]
            public string ipaddress { get; set; }
        }

        /********
         * TEST *
         ********/
        private static void testGET()
        {
            string webServer = "http://httpbin.org/get";
            string responseString = getHTTP(webServer);

            testGETobject test = JsonConvert.DeserializeObject<testGETobject>(responseString);

            Console.WriteLine(test.origin);
            Console.WriteLine(test.url);
        }

        [DataContract]
        public class testGETobject
        {
            // included in JSON
            [DataMember]
            public string origin { get; set; }

            [DataMember]
            public string url { get; set; }
        }

        private static void setVars()
        {
            Globals.collection.Add("uuid", "1234");
            Globals.collection.Add("ipaddress", "10.5.1.240");
        }
    }
}

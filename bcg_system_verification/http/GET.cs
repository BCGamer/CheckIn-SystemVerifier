using System;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace bcg_system_verification.http
{
    class get
    {
        public static void uuid_and_ip()
        {
            //testGet();
            Globals.collection.Add("uuid", "1234");
            Globals.collection.Add("ipaddress", "10.5.1.240");
        }

        private static void testGet()
        {
            string webServer = "http://httpbin.org/ip";
            string responseString = null;
            using (var client = new WebClient())
            {
                responseString = client.DownloadString(webServer);
            }

            Console.WriteLine(responseString);
        }
    }
}

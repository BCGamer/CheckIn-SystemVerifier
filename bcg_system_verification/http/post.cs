using System;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace bcg_system_verification.http
{
    class post
    {
        public static void final()
        {
            //testPost();
            //bcgPost();
        }

        private static string postHTTP(string webServer)
        {
            using (var client = new WebClient())
            {
                var response = client.UploadValues(webServer, Globals.collection);
                return Encoding.Default.GetString(response);
            }
        }

        private static void bcgPost()
        {
            string webServer = "http://checkin.bcgamer.lan/verification_response/";
            string responseString = postHTTP(webServer);

            Console.WriteLine(responseString);
        }

        private static void testPost()
        {
            string webServer = "https://posttestserver.com/post.php";
            string responseString = postHTTP(webServer);

            Console.WriteLine(responseString);
        }
    }
}

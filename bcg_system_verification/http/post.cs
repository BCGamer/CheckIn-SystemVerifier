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
        }

        private static void testPost()
        {
            string webServer = "https://posttestserver.com/post.php";
            string responseString = null;
            using (var client = new WebClient())
            {
                var response = client.UploadValues(webServer, Globals.collection);
                responseString = Encoding.Default.GetString(response);
            }

            Console.WriteLine(responseString);
        }
    }
}

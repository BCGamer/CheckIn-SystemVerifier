using System;
using System.Collections.Specialized;
using System.Text;

namespace bcg_system_verification
{
    class Globals
    {
        public static bool debugMode = new bool();
        public static NameValueCollection collection = new NameValueCollection();

        public static void init()
        {
            collection.Add("server", "10.5.1.14");
        }
    }
}
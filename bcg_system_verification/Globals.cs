using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace bcg_system_verification
{
    class Globals
    {
        public static bool debugMode = false;
        public static string scriptPath = Directory.GetCurrentDirectory();
        public static string server = "10.5.1.14";
        
        //Look at converting this to a NameValueCollection
        //http://msdn.microsoft.com/en-us/library/system.collections.specialized.namevaluecollection(v=vs.110).aspx
        public static string[,] Props = new string[6, 2];

        public static void init()
        {
            Props[0, 0] = "uuid";
            Props[1, 0] = "ipaddress";
            Props[2, 0] = "firewall";
            Props[3, 0] = "antivirus";
            Props[4, 0] = "dhcp";
            Props[5, 0] = "sfp";
        }
    }
}
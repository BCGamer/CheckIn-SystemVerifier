using System;
using System.Collections.Generic;
using System.Text;
using bcg_system_verification.verifiers;
using bcg_system_verification.http;

namespace bcg_system_verification
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            //remove me when done
            Globals.debugMode = true;

            if ( args.Length > 0 ){
                parseArgs(args);
            }
            
            Boolean status = new Boolean();
            String[,] Props = new String[6, 2];
            Props[0, 0] = "uuid";
            Props[1, 0] = "ipaddress";
            Props[2, 0] = "firewall";
            Props[3, 0] = "antivirus";
            Props[4, 0] = "dhcp";
            Props[5, 0] = "sfp";

            Props = initApp(Props);
            Props = verifyApp(Props);
            status = postApp(Props);

            //Debugging console
            if (Globals.debugMode) debugConsole(Props, status);
        }

        private static string[,] initApp(string[,] Props){
            //HTTP Get:
            GET_UUID httpGet = new GET_UUID();

            //initProps = httpGet.
            Props[0, 1] = "1234";
            Props[1, 1] = "10.5.1.240";

            return Props;
        }

        private static string[,] verifyApp(string[,] Props)
        {
            //Verify Operations
            network networkTest = new network();
            software softwareTest = new software();

            Props[2, 1] = networkTest.fwVerify();
            Props[3, 1] = softwareTest.avVerify();
            Props[4, 1] = networkTest.dhcpVerify(Props[1,1]);
            Props[5, 1] = networkTest.sfpVerify();

            return Props;
        }

        private static Boolean postApp(string[,] Props)
        {
            Boolean status = new Boolean();

            //HTTP Post:
            POST_RESPONSE httpPost = new POST_RESPONSE();

            return status;
        }

        private static void debugConsole(string[,] Props, bool status)
        {
            Console.WriteLine(Props[0, 0] + ": " + Props[0, 1]);
            Console.WriteLine(Props[1, 0] + ": " + Props[1, 1]);
            Console.WriteLine(Props[2, 0] + ": " + Props[2, 1]);
            Console.WriteLine(Props[3, 0] + ": " + Props[3, 1]);
            Console.WriteLine(Props[4, 0] + ": " + Props[4, 1]);
            Console.WriteLine(Props[5, 0] + ": " + Props[5, 1]);
            Console.WriteLine("success: " + status);

            //pause so we can see results
            Console.ReadLine();
        }

        public static void parseArgs(string[] args)
        {
            if (args[0] == "/debug")
            {
                Console.WriteLine("Mode = Debugging");
                Globals.debugMode = true;
            }
            else
            {
                Console.WriteLine("Bad");
            }
            
        }
    }
}

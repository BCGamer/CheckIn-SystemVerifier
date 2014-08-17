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

            Globals.init();
            App();
        }

        private static void App(){
            //Connect to web server to grab uuid and ip
            get.uuid_and_ip();

            Globals.Props[2, 1] = firewall.Verify();
            Globals.Props[3, 1] = antivirus.Verify();
            Globals.Props[4, 1] = dhcp.Verify();
            Globals.Props[5, 1] = sfp.Verify();

            //Debugging console
            if (Globals.debugMode) debugConsole();
            
        }

        private static void debugConsole()
        {
            Console.WriteLine("");
            Console.WriteLine("********************");
            Console.WriteLine("       Results      ");
            Console.WriteLine("********************");

            Console.WriteLine(Globals.Props[0, 0] + ": " + Globals.Props[0, 1]);
            Console.WriteLine(Globals.Props[1, 0] + ": " + Globals.Props[1, 1]);
            Console.WriteLine(Globals.Props[2, 0] + ": " + Globals.Props[2, 1]);
            Console.WriteLine(Globals.Props[3, 0] + ": " + Globals.Props[3, 1]);
            Console.WriteLine(Globals.Props[4, 0] + ": " + Globals.Props[4, 1]);
            Console.WriteLine(Globals.Props[5, 0] + ": " + Globals.Props[5, 1]);

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
        }
    }
}

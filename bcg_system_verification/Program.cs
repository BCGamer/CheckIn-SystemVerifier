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

            //Startup
            Globals.init();
            Arguments.check(args);

            //Primary App
            App();
        }

        private static void App(){
            //Connect to web server to grab uuid and ip
            get.uuid_and_ip();

            Globals.Props[2, 1] = firewall.Verify();
            Globals.Props[3, 1] = antivirus.Verify();
            Globals.Props[4, 1] = dhcp.Verify();
            Globals.Props[5, 1] = sfp.Verify();

            post.final();
            //Debugging console
            if (Globals.debugMode) Debug.results();

            
        }
    }
}

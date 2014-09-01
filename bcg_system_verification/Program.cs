using System;
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
            Debug.writeHeader("Mode Debugging");

            //Startup
            Globals.init();
            Arguments.check(args);

            //Primary App
            App();
        }

        private static void App(){
            //Connect to web server to grab uuid and ip
            get.uuid_and_ip();

            firewall.Verify();
            antivirus.Verify();
            network.Verify();

            post.final();
            //Debugging console
            if (Globals.debugMode) Debug.results();         
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.Win32;

namespace bcg_system_verification.verifiers
{
    class firewall
    {
        public static string Verify()
        {
            if (Globals.debugMode)
            {
                Console.WriteLine("");
                Console.WriteLine("*******************");
                Console.WriteLine(" firewall.verify() ");
                Console.WriteLine("*******************");
            }
            string windowsFW = checkForWindowsFW();
            string otherFW = checkForOtherFW();
            if (windowsFW == "good" || otherFW == "good" ){
                if (Globals.debugMode) Console.WriteLine("Returning GOOD status firewall.Verify()");
                return "good";
            }else{
                if (Globals.debugMode) Console.WriteLine("Returning BAD status from firewall.Verify()");
                return "bad";
            }

        }

        private static string checkForOtherFW()
        {
            /*
             * Query Security Center for external firewalls
             * Needs testing - on laptop with McAfee
            */
            
            //Windows XP uses "SecurityCenter" namespace
            //Windows Vista and up uses "SecurityCenter2" namespace
            string WMINameSpace = System.Environment.OSVersion.Version.Major > 5 ? "SecurityCenter2" : "SecurityCenter";
            ManagementScope Scope = new ManagementScope("root\\" + WMINameSpace);
            ObjectQuery Query = new ObjectQuery("SELECT * FROM FirewallProduct");
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            foreach (ManagementObject mo in moSearch.Get())
            {
                if (Globals.debugMode) Console.WriteLine("Name: " + mo["displayName"]);
                Console.WriteLine("{0,-35} {1,-40}", "Firewall Name", mo["displayName"]);
                if (System.Environment.OSVersion.Version.Major < 6) //is XP ?
                {
                    if (Globals.debugMode) Console.WriteLine("Enabled: " + mo["enabled"]);
                    Console.WriteLine("{0,-35} {1,-40}", "Enabled", mo["enabled"]);
                }
                else
                {
                    if (Globals.debugMode) Console.WriteLine("ProductState: " + mo["productState"]);
                    Console.WriteLine("{0,-35} {1,-40}", "State", mo["productState"]);
                }
                if (Globals.debugMode) Console.WriteLine("");
            }

            if (Globals.debugMode) Console.WriteLine("Returning BAD status from other firewall");
            return "bad";
        }

        private static string checkForWindowsFW()
        {
            /*
             * Query registry for Windows Firewall 
            */
            string systemRoot = "HKEY_LOCAL_MACHINE";
            string keyPath = "\\SYSTEM\\CurrentControlSet\\Services\\SharedAccess\\Parameters\\FirewallPolicy\\";
            string stdkeyName = systemRoot + keyPath + "StandardProfile";
            string pubkeyName = systemRoot + keyPath + "PublicProfile";
            string valueName = "EnableFirewall";

            //Looks at the registry values for Private/Standard and Private networks
            object stdFwStatus = Registry.GetValue(stdkeyName, valueName, "0");
            object pubFwStatus = Registry.GetValue(pubkeyName, valueName, "0");

            if (Globals.debugMode)
            {
                Console.WriteLine("Standard Firewall: " + stdFwStatus);
                Console.WriteLine("Public Firewall: " + pubFwStatus);
            }

            if (stdFwStatus.ToString() == "1" && pubFwStatus.ToString() == "1")
            {
                if (Globals.debugMode) Console.WriteLine("Returning GOOD status from windows firewall");
                return "good";
            }

            if (Globals.debugMode) Console.WriteLine("Returning BAD status from windows firewall");
            return "bad";
        }

    }
}

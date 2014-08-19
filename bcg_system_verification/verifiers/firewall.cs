using System;
using System.Text;
using System.Management;
using Microsoft.Win32;
using bcg_system_verification.common;

namespace bcg_system_verification.verifiers
{
    class firewall
    {
        public static void Verify()
        {
            if (Globals.debugMode) Debug.writeHeader("firewall.verify()");
            string windowsFW = checkForWindowsFW();
            string otherFW = securityCenter.viewObjects("FirewallProduct");
            if (windowsFW == "good"){
                if (Globals.debugMode) Console.WriteLine("Returning GOOD status firewall.Verify()");
                Globals.collection.Add("firewall", windowsFW);
            }else if (otherFW != "problem" || otherFW != "bad"){
                if (Globals.debugMode) Console.WriteLine("Returning status details from firewall.Verify()");
                Globals.collection.Add("firewall", otherFW);
            }else{
                if (Globals.debugMode) Console.WriteLine("Returning BAD status from firewall.Verify()");
                Globals.collection.Add("firewall","bad");
            }

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

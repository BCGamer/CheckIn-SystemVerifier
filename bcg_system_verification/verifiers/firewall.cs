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

            if (checkForWindowsFW() == "good")
            {
                if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "firewall.Verify()", "good");
                Globals.collection.Add("firewall", "397568");
            }
            else
            {
                string otherFW = securityCenter.viewObjects("FirewallProduct");

                if (otherFW != null)
                {
                    if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "firewall.Verify()", otherFW);
                    Globals.collection.Add("firewall", otherFW);
                }
                else
                {
                    if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "firewall.Verify()", "bad");
                    Globals.collection.Add("firewall", "393472");
                }
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
                Console.WriteLine("{0,-30}: {1,-40}", "Std Firewall", stdFwStatus);
                Console.WriteLine("{0,-30}: {1,-40}", "Pub Firewall", pubFwStatus);
            }

            if (stdFwStatus.ToString() == "1" && pubFwStatus.ToString() == "1")
            {
                if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "checkForWindowsFW()", "good");
                return "good";
            }

            if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "checkForWindowsFW()", "bad");
            return "bad";
        }

    }
}

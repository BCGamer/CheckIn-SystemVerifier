using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.Win32;

namespace data_collection.verifiers
{
    class network
    {
        public string dhcpVerify(string ipaddress)
        {
            ManagementScope Scope = new ManagementScope("root\\CIMV2");
            ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            foreach (ManagementObject mo in moSearch.Get())
            {
                //Loops through objects until IPEnabled = true
                if (!(bool)mo["IPEnabled"]) continue;

                //Loops through objects until the IP = ipaddress
                //Array [0 = ipv4, 1=ipv6]
                string[] addresses = (string[])mo["IPAddress"];
                if (addresses[0] != ipaddress) continue;

                //If DHCPEnabled = true, return good, else bad
                if ((bool)mo["DHCPEnabled"]) return "good";
                else return "bad";
            }

            //Couldn't find the IP
            return "problem";
        }

        public string fwVerify()
        {
            /*
             * Query registry for Windows Firewall 
            */
            string systemRoot = "HKEY_LOCAL_MACHINE";
            string keyPath = "\\SYSTEM\\CurrentControlSet\\Services\\SharedAccess\\Parameters\\FirewallPolicy\\StandardProfile";
            string keyName = systemRoot + keyPath;
            string valueName = "EnableFirewall";
            object firewallStatus = Registry.GetValue(keyName, valueName, "0");

            if (firewallStatus.ToString() == "1") { 
                return "good"; 
            }

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
                Console.WriteLine("test");
                Console.WriteLine("{0,-35} {1,-40}", "Firewall Name", mo["displayName"]);
                if (System.Environment.OSVersion.Version.Major < 6) //is XP ?
                {
                    Console.WriteLine("{0,-35} {1,-40}", "Enabled", mo["enabled"]);
                }
                else
                {
                    Console.WriteLine("{0,-35} {1,-40}", "State", mo["productState"]);
                } 
            }

            return "bad";
        }

        public string sfpVerify()
        {
            
            return "problem";
        }

    }
}

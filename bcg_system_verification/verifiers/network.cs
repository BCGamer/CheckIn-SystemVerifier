using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.Win32;

namespace bcg_system_verification.verifiers
{
    class network
    {
        public string dhcpVerify(string ipaddress)
        {
            if (Globals.debugMode)
            {
                Console.WriteLine("");
                Console.WriteLine("***************************");
                Console.WriteLine("*Checking WMI for adapters*");
                Console.WriteLine("***************************");
            }

            ManagementScope Scope = new ManagementScope("root\\CIMV2");
            ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            foreach (ManagementObject mo in moSearch.Get())
            {
                string[] addresses = (string[])mo["IPAddress"];

                if (Globals.debugMode)
                {
                    Console.WriteLine("Interface: " + mo["InterfaceIndex"]);

                    try
                    {
                        Console.WriteLine("IP Address: {0},{1}", addresses[0], addresses[1]);
                    }
                    catch
                    {
                        Console.WriteLine("IP Address: ");
                    }

                    Console.WriteLine("MAC Address:" + mo["MACAddress"]);
                    Console.WriteLine("DHCP Enabled: " + mo["DHCPEnabled"]);
                    Console.WriteLine("Description: " + mo["Description"]);
                    Console.WriteLine("");
                }

                //Loops through objects until IPEnabled = true
                if (!(bool)mo["IPEnabled"]) continue;

                //Loops through objects until the IP = ipaddress
                //Array [0 = ipv4, 1=ipv6]
                if (addresses[0] != ipaddress) continue;


                //If DHCPEnabled = true, return good, else bad
                if ((bool)mo["DHCPEnabled"])
                {
                    if (Globals.debugMode) Console.WriteLine("Returning GOOD status");
                    return "good";
                }
                else
                {
                    if (Globals.debugMode) Console.WriteLine("Returning BAD status");
                    return "bad";
                }
            }

            //Couldn't find the IP
            if (Globals.debugMode) Console.WriteLine("Returning PROBLEM status");
            return "problem";
        }

        public string fwVerify()
        {
            /*
             * Query registry for Windows Firewall 
            */
            if (Globals.debugMode)
            {
                Console.WriteLine("");
                Console.WriteLine("*******************************");
                Console.WriteLine("*Checking for Windows Firewall*");
                Console.WriteLine("*******************************");
            }
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
                if (Globals.debugMode) Console.WriteLine("Returning GOOD status");
                return "good";
            }

            /*
             * Query Security Center for external firewalls
             * Needs testing - on laptop with McAfee
            */
            if (Globals.debugMode)
            {
                Console.WriteLine("");
                Console.WriteLine("*********************************");
                Console.WriteLine("*Checking for 3rd Party Firewall*");
                Console.WriteLine("*********************************");
            }
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

            if (Globals.debugMode) Console.WriteLine("Returning BAD status");
            return "bad";
        }

        public string sfpVerify()
        {
            
            return "problem";
        }

    }
}

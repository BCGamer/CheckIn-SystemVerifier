using System;
using System.Text;
using System.Management;

namespace bcg_system_verification.verifiers
{
    class network
    {
        public static void Verify()
        {
            if (Globals.debugMode) Debug.writeHeader("dhcp.Verify()");
            Globals.collection.Add("dhcp",checkDHCPStatusFromWMI());
            if (Globals.debugMode) Console.WriteLine("");
        }

        private static void windows6x()
        {

        }

        private static void windows5x()
        {

        }
        private static string checkDHCPStatusFromWMI()
        {
            ManagementScope Scope = new ManagementScope("root\\CIMV2");
            ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            foreach (ManagementObject mo in moSearch.Get())
            {
                string[] addresses = (string[])mo["IPAddress"];

                if (Globals.debugMode)
                {
                    Console.WriteLine(mo["Description"]);
                    try
                    {
                        Console.WriteLine("IPv4 Address: {0}", addresses[0]);
                        Console.WriteLine("IPv6 Address: {0}", addresses[1]);
                    }
                    catch
                    {
                        Console.WriteLine("IPv4 Address: n/a");
                        Console.WriteLine("IPv6 Address: n/a");
                        //Console.WriteLine("IP Address: ");
                        //dont paste a blank link
                    }

                    Console.WriteLine("MAC Address:" + mo["MACAddress"]);
                    Console.WriteLine("DHCP Enabled: " + mo["DHCPEnabled"]);
                    Console.WriteLine("");
                }

                //Loops through objects until IPEnabled = true
                if (!(bool)mo["IPEnabled"]) continue;
                

                //Loops through objects until the IP = ipaddress
                //Array [0 = ipv4, 1=ipv6]
                if (addresses[0] != Globals.collection.Get("ipaddress")) continue;


                //If DHCPEnabled = true, return good, else bad
                if ((bool)mo["DHCPEnabled"])
                {
                    if (Globals.debugMode) Console.WriteLine("Returning GOOD status from dhcp.Verify()");
                    return "good";
                }
                else
                {
                    if (Globals.debugMode) Console.WriteLine("Returning BAD status from dhcp.Verify()");
                    return "bad";
                }
            }

            //Couldn't find the IP
            if (Globals.debugMode) Console.WriteLine("Returning PROBLEM status from dhcp.Verify()");
            return "problem";
        }
    }
}

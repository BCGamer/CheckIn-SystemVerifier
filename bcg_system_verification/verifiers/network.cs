using System;
using System.Text;
using System.Management;

namespace bcg_system_verification.verifiers
{
    class network
    {
        public static void Verify()
        {
            if (Globals.debugMode) Debug.writeHeader("network.Verify()");
            ManagementObject mo = findNetAdptConfigWMI();
            checkDHCPStatusWMI(mo);
        }

        private static ManagementObject findNetAdptConfigWMI()
        {
            ManagementScope Scope = new ManagementScope("root\\CIMV2");
            ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = '1'");
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            foreach (ManagementObject mo in moSearch.Get())
            {
                string[] addresses = (string[])mo["IPAddress"];

                if (Globals.debugMode) dumpDebugData(mo);

                //Loops through objects until the IP = ipaddress
                //Array [0 = ipv4, 1=ipv6]
                if (addresses[0] != Globals.collection.Get("ipaddress")) continue;

                return mo;
            }

            return null;
        }

        private static void checkDHCPStatusWMI(ManagementObject mo)
        {
            switch (mo["DHCPEnabled"].ToString())
            {
                case "True":
                    if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "True", "network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "True");
                    break;
                case "False":
                    if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "False", "network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "False");
                    break;
                default:
                    if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "Problem", "network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "Problem");
                    break;
            }
            
            if (Globals.debugMode) Console.WriteLine("");
        }

        private static void dumpDebugData(ManagementObject mo)
        {
            string[] addresses = (string[])mo["IPAddress"];
            string[] gateways = (string[])mo["DefaultIPGateway"];
            string[] subnets = (string[])mo["IPSubnet"];

            Console.WriteLine(mo["Description"]);
            Console.WriteLine("{0,-15}: {1,-40}", "IPv4 Address", addresses[0]);
            Console.WriteLine("{0,-15}: {1,-40}", "IPv6 Address", addresses[1]);
            Console.WriteLine("{0,-15}: {1,-40}", "MAC Address", mo["MACAddress"]);
            Console.WriteLine("{0,-15}: {1,-40}", "DHCP Enabled", mo["DHCPEnabled"]);
            Console.WriteLine("{0,-15}: {1,-40}", "Interface Index", mo["InterfaceIndex"]);
            Console.WriteLine("");
        }
    }
}

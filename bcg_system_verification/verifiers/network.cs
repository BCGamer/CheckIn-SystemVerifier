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
            if (Globals.debugMode) Console.WriteLine("");
            
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
                    if (Globals.debugMode) Console.WriteLine("Returning True from network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "True");
                    break;
                case "False":
                    if (Globals.debugMode) Console.WriteLine("Returning False from network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "False");
                    break;
                default:
                    if (Globals.debugMode) Console.WriteLine("Returning Problem from network.checkDHCPStatusWMI()");
                    Globals.collection.Add("dhcp", "Problem");
                    break;
            }
        }

        private static void dumpDebugData(ManagementObject mo)
        {
            string[] addresses = (string[])mo["IPAddress"];
            string[] gateways = (string[])mo["DefaultIPGateway"];
            string[] subnets = (string[])mo["IPSubnet"];

            Console.WriteLine(mo["Description"]);
            Console.WriteLine("IPv4 Address: {0}", addresses[0]);
            Console.WriteLine("IPv6 Address: {0}", addresses[1]);
            Console.WriteLine("MAC Address: {0}", mo["MACAddress"]);
            Console.WriteLine("DHCP Enabled: {0}", mo["DHCPEnabled"]);
            Console.WriteLine("Interface Index: {0}", mo["InterfaceIndex"]);
            Console.WriteLine("");
        }
    }
}

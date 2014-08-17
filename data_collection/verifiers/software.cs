using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace data_collection.verifiers
{
    class software
    {
        public string avVerify()
        {
            //FirewallProduct
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

            return "problem";
        }
    }
}

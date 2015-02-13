using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace bcg_system_verification.common
{
    class securityCenter
    {
        public static string viewObjects(string productGroup)
        {

            //Windows XP uses "SecurityCenter" namespace
            //Windows Vista and up uses "SecurityCenter2" namespace
            string WMINameSpace = System.Environment.OSVersion.Version.Major > 5 ? "SecurityCenter2" : "SecurityCenter";
            ManagementScope Scope = new ManagementScope("root\\" + WMINameSpace);
            ObjectQuery Query = new ObjectQuery("SELECT * FROM " + productGroup);
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(Scope, Query);

            string results = null;
            foreach (ManagementObject mo in moSearch.Get())
            {
                if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "Name", mo["displayName"]);
                if (System.Environment.OSVersion.Version.Major < 6) //is XP ?
                {
                    if (results == null)
                    {
                        results = results + windows5x(mo);
                    }
                    else
                    {
                        results = results + "," + windows5x(mo);
                    } 
                }
                else
                {
                    if (results == null ){
                        results = mo["ProductState"].ToString();
                    }
                    else
                    {
                        results = results + "," + mo["ProductState"].ToString();
                    }
                }
            }

            if (Globals.debugMode) Console.WriteLine("{0,-30}: {1,-40}", "securityCenter.viewObjects()", results);
            return results;
        }

        private static string windows5x(ManagementObject mo)
        {
            if (Globals.debugMode) Console.WriteLine("**NEEDS TESTING**");
            if (Globals.debugMode) Console.WriteLine("Enabled: " + mo["enabled"]);

            if (Globals.debugMode) Console.WriteLine("");
            return "needs_testing";
        }
    }
}

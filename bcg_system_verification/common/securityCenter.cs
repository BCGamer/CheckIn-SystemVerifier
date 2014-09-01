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
                if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", "Name", mo["displayName"]);
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

            if (Globals.debugMode) Console.WriteLine("{0,-15}: {1,-40}", results, "securityCenter.viewObjects()");
            return results;
        }

        /*
        private static string windows6x(ManagementObject mo)
        {
            //Vista, 7, 8, 8.1
            //Have to convert the object -> String -> Int -> Hex (String)
            //Once we have the Hex we can see the status based on the last 4 digits
            string productStateStr = (mo["ProductState"].ToString());
            int productStateInt = Convert.ToInt32(productStateStr);
            string productStateHex = productStateInt.ToString("X");

            if (Globals.debugMode) Console.WriteLine("ProductStateInt: " + productStateInt);
            if (Globals.debugMode) Console.WriteLine("ProductStateHex: " + productStateHex);

            string enabledStatus = "null";
            string updateStatus = "null";

            if (productStateHex.Substring(1, 2) == "10") { enabledStatus = "enabled"; };
            if (productStateHex.Substring(1, 2) == "11") { enabledStatus = "enabled"; };
            if (productStateHex.Substring(1, 2) == "00") { enabledStatus = "disabled"; };
            if (productStateHex.Substring(1, 2) == "01") { enabledStatus = "disabled"; };
            if (Globals.debugMode) Console.WriteLine("enabledStatus: " + enabledStatus);

            if (productStateHex.Substring(3, 2) == "00") { updateStatus = "up_to_date"; };
            if (productStateHex.Substring(3, 2) == "10") { updateStatus = "not_updated"; };
            if (Globals.debugMode) Console.WriteLine("updateStatus: " + updateStatus);
            
            if (Globals.debugMode) Console.WriteLine("");

            if (enabledStatus != "null" && updateStatus != "null")
            {
                if (Globals.debugMode) Console.WriteLine("Returning status details from securityCenter.windows6x()");
                return enabledStatus + "," + updateStatus;
            }
            else
            {
                if (Globals.debugMode) Console.WriteLine("Returning PROBLEM status from securityCenter.windows6x()");
                return "problem";
            }

            
        }*/

        private static string windows5x(ManagementObject mo)
        {
            if (Globals.debugMode) Console.WriteLine("**NEEDS TESTING**");
            if (Globals.debugMode) Console.WriteLine("Enabled: " + mo["enabled"]);

            if (Globals.debugMode) Console.WriteLine("");
            return "needs_testing";
        }
    }
}

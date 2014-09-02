using System;
using System.Text;
using System.Management;
using bcg_system_verification.common;

namespace bcg_system_verification.verifiers
{
    class antivirus
    {
        public static void Verify()
        {
            if (Globals.debugMode) Debug.writeHeader("antivirus.Verify()");
            Globals.collection.Add("antivirus", securityCenter.viewObjects("AntiVirusProduct"));
        }
    }
}

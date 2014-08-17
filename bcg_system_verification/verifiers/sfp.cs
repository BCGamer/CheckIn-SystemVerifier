using System;
using System.Collections.Generic;
using System.Text;

namespace bcg_system_verification.verifiers
{
    class sfp
    {
        public static string Verify()
        {
            if (Globals.debugMode)
            {
                Console.WriteLine("");
                Console.WriteLine("**************");
                Console.WriteLine(" sfp.Verify() ");
                Console.WriteLine("**************");
            }

            return "problem";
        }
    }
}

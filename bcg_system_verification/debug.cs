using System;
using System.Collections.Generic;
using System.Text;

namespace bcg_system_verification
{
    class Debug
    {
        public static void init()
        {
            Console.WriteLine("");
            Console.WriteLine("********************");
            Console.WriteLine("  Mode = Debugging  ");
            Console.WriteLine("********************");
            Console.WriteLine("");
            Globals.debugMode = true;
        }

        public static void results()
        {
            Console.WriteLine("");
            Console.WriteLine("********************");
            Console.WriteLine("       Results      ");
            Console.WriteLine("********************");

            Console.WriteLine(Globals.Props[0, 0] + ": " + Globals.Props[0, 1]);
            Console.WriteLine(Globals.Props[1, 0] + ": " + Globals.Props[1, 1]);
            Console.WriteLine(Globals.Props[2, 0] + ": " + Globals.Props[2, 1]);
            Console.WriteLine(Globals.Props[3, 0] + ": " + Globals.Props[3, 1]);
            Console.WriteLine(Globals.Props[4, 0] + ": " + Globals.Props[4, 1]);
            Console.WriteLine(Globals.Props[5, 0] + ": " + Globals.Props[5, 1]);
            Console.WriteLine("Server: " + Globals.server);

            //pause so we can see results
            Console.ReadLine();
        }
    }
}

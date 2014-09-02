using System;
using System.Text;

namespace bcg_system_verification
{
    class Debug
    {
        public static void init()
        {
            Globals.debugMode = true;
            writeHeader("Mode Debugging");
        }

        public static void writeHeader(string text)
        {
            string bar = new String('*', text.Length + 2);
            text = " " + text + " ";
            Console.WriteLine("\n" + bar + "\n" + text + "\n" + bar + "\n");
        }

        public static void results()
        {
            for (int i = 0; i < Globals.collection.Count; i++)
            {
                Console.WriteLine("{0,-30}: {1,-40}", Globals.collection.GetKey(i), Globals.collection.Get(i));
            }

            //pause so we can see results
            Console.ReadLine();
        }
    }
}

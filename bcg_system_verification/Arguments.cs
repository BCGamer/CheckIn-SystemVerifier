using System;
using System.Text;

namespace bcg_system_verification
{
    class Arguments
    {
        public static void check(string[] args)
        {
            foreach (string value in args)
            {
                if (value == "/debug")
                {
                    Debug.init();
                }
                if (value.Contains("/server:"))
                {
                    //To hardcode server for debugging
                    //syntax - /server:10.5.1.5 or /server:nameServer
                    Globals.collection.Remove("server");
                    Globals.collection.Set("server",value.Replace("/server:",""));
                }
            }
        }
    }
}

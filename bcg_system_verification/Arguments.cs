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
            }
        }
    }
}

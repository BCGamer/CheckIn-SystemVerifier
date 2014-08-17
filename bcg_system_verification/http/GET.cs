using System;
using System.Text;

namespace bcg_system_verification.http
{
    class get
    {
        public static void uuid_and_ip()
        {
            Globals.collection.Add("uuid", "1234");
            Globals.collection.Add("ipaddress", "10.5.1.240");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //for Threading operations
using System.Net; //for GET/POST operations
using System.Collections.Specialized; //for GET/POST operations

namespace data_collection
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*
            Thread[] threads = new Thread[4];
            threads[0] = new Thread(avThread);
            threads[1] = new Thread(fwThread);
            threads[2] = new Thread(dhcpThread);
            threads[3] = new Thread(sfpThread);

            
            for (int i = 0; i < 4; i++)
            {
                threads[i].Start();
            }
             */

            
        }
        /*
        static void avThread()
        {
            string av_status = "good";
            
        }
        static public void fwThread()
        {
            string fw_status = "good";
        }
        static public void dhcpThread()
        {
            string dhcp_status = "good";
        }
        static public void sfpThread()
        {
            string sfp_status = "good";
        }
        static public void httpPost()
        {
            NameValueCollection values[] = new NameValueCollection();
            values["antivirus"] = av_status;
            
            values["firewall"] = fw_status;
            values["dhcp"] = dhcp_status;
            values["sfp"] = sfp_status;
            values["uuid"] = "";
            
            using (var client = new WebClient())
            {
                
            }
        }
         */ 
    }
}

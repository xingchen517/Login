using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    static class Program
    {
        static void Main()
        {
            int retry = SeleniumTest.Retry;
            while (!SeleniumTest.Run()&&retry-->0)
            {
                Console.WriteLine($"going to retry::{SeleniumTest.Retry-retry+1}");
            }
            //Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static int NUM_CLASS = 20;

        public static int expireRequest { get; set; }
        public static int fee = 10;

        public static int guestRequestKeySeq = 10000000;
        public static int HostingUnitKeySeq = 10000000;
        public static int OrderKeySeq = 10000000;
    }
}

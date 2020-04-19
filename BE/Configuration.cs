using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static int NUM_CLASS = 20;

        //public static int expireRequest { get; set; }
        public static int NumDaysUntillExpired = 3; // מספר ימי תוקף הזמנה
        public static float fee = 10f;

        public static int HostingUnitKeySeq = 10000004;
        public static int guestRequestKeySeq = 20000004;
        public static int OrderKeySeq = 30000004;

        
        public static MailAddress mailAddress;
        public static string Password;
    }
}

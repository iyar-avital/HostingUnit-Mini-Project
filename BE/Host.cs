using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public int PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        //public List<HostingUnit> Units { get; set; }


        public override string ToString()
        {
             return HostKey.ToString()+ PrivateName.ToString()+ FamilyName.ToString() + PhoneNumber.ToString()
            + MailAddress.ToString() + BankBranchDetails.ToString()+ BankAccountNumber.ToString() + CollectionClearance.ToString();
        }
        /*
        public Host()
        {
            HostKey = 080808;
            PrivateName = "jgjg";
            FamilyName = "dgdsrsedr";
            FhoneNumber = 808008776;
            MailAddress = "ygugfu";
            BankBranchDetails = new BankBranch();
            BankAccountNumber = 000;
            CollectionClearance = true;
        }*/

    }
}

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
            return "Host Key: " + HostKey + "\n" +
               "Private Name: " + PrivateName + "\n" +
               "Family Name: " + PhoneNumber + "\n" +
               "Mail Address: " + MailAddress + "\n" +
               "Bank Branch Details: \n" + BankBranchDetails + "\n" +
               "Bank Account Number: " + BankAccountNumber + "\n" +
               "Collection Clearance:" + CollectionClearance + "\n";
        }
    }
}

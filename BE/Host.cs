using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Host
    {
        [XmlElement("HostKey")]
        public int HostKey { get; set; }
        [XmlElement("PrivateName")]
        public string PrivateName { get; set; }
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }
        [XmlElement("PhoneNumber")]
        public int PhoneNumber { get; set; }
        [XmlElement("MailAddress")]
        public string MailAddress { get; set; }
        [XmlElement("BankAccountDetails")]
        public BankBranch BankBranchDetails { get; set; }
        [XmlElement("BankAccountNumber")]
        public int BankAccountNumber { get; set; }
        [XmlElement("CollectionClearance")]
        public bool CollectionClearance { get; set; }
        //public List<HostingUnit> Units { get; set; }
        
        public string FullName { get { return PrivateName + " " + FamilyName; } }
        
        
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

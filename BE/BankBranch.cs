using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class BankBranch
    {
        [XmlElement("BankNumber")]
        public int BankNumber { get; set; }
        [XmlElement("BankName")]
        public string BankName { get; set; }
        [XmlElement("BranchNumber")]
        public int BranchNumber { get; set; }
        [XmlElement("BranchName")]
        public string BranchName { get; set; }
        [XmlElement("BranchAddress")]
        public string BranchAddress { get; set; }
        [XmlElement("BranchCity")]
        public string BranchCity { get; set; }
        

        public string BankSelectionValue
        {
            get { return BankNumber + " - " + BankName; }
            set { }
        }

        public string BranchSelectionValue
        {
            get { return BranchNumber + " - " + BranchName; }
            set { }
        }



        public override string ToString()
        {
            return BankNumber.ToString() + BankName + BranchNumber.ToString() + BranchAddress+ BranchCity;
               
        }

    }
}

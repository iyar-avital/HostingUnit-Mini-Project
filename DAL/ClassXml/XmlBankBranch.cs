using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.ClassXml
{
    public class FactorySingletonXmlBankBranch
    {
        private static XmlBankBranch singelonXmlBankBranch = null;
        private FactorySingletonXmlBankBranch() { }
        public static XmlBankBranch GetXmlBankBranch()
        {
            if (singelonXmlBankBranch == null)
            {
                singelonXmlBankBranch = new XmlBankBranch();
            }
            return singelonXmlBankBranch;
        }
    }


    public class XmlBankBranch
    {
        public XElement BranchRoot;
        public string BranchPath = @"BranchXml.xml";

        public XmlBankBranch()
        {
            if (!File.Exists(BranchPath))
                CreateXmlOrder();
            else
                LoadXmlOrder();
        }

        public void CreateXmlOrder()
        {
            BranchRoot = new XElement("BRANCHES");
            BranchRoot.Save(BranchPath);
        }

        public void LoadXmlOrder()
        {
            BranchRoot = XElement.Load(BranchPath);
        }
        /*
        public void AddOrder(Order order)
        {
            long key = XC.GetConfiguration<long>("OrderKey");
            BranchRoot.Add(new XElement("Order",
                new XElement("OrderKey", key),
                new XElement("HostingUnitKey", order.HostingUnitKey),
                new XElement("GuestRequestKey", order.GuestRequestKey),
                new XElement("Status", order.Status),
                new XElement("CreateDate", order.CreateDate),
                new XElement("OrderDate", order.OrderDate),
                new XElement("CommissionValue", order.CommissionValue)));
            BranchRoot.Save(BranchPath);
            XC.UpdateConfiguration<long>("OrderKey", ++key);
        }*/

        public List<BankBranch> GetAllBranches()
        {
            return (from p in BranchRoot.Elements()
                    select new BankBranch()
                    {
                        BankNumber = Convert.ToInt32(p.Element("Bank_Code").Value),
                        BankName = p.Element("Bank_Name").Value,
                        BranchNumber = Convert.ToInt32(p.Element("Branch_Code").Value),
                        /*BranchName = p.Element("Branch_Name").Value,*/
                        BranchAddress = p.Element("Branch_Address").Value,
                        BranchCity = p.Element("City").Value/*,
                        BranchZipCode = Convert.ToInt32(p.Element("Zip_Code").Value),
                        BranchType = p.Element("Branch_Type").Value,
                        Telephone = p.Element("Telephone").Value,
                        Fax = p.Element("Fax").Value*/
                    }).ToList();
        }

        public BankBranch GetBranch(int bankKey, int branchKey)
        {
            return (from p in BranchRoot.Elements()
                    where Convert.ToInt32(p.Element("Bank_Code").Value.Trim()) == bankKey && Convert.ToInt32(p.Element("Branch_Code").Value.Trim()) == branchKey
                    select new BankBranch()
                    {
                        BankNumber = int.Parse(p.Element("Bank_Code").Value),
                        BankName = p.Element("Bank_Name").Value.ToString(),
                        BranchNumber = int.Parse(p.Element("Branch_Code").Value),
                        BranchName = p.Element("Branch_Name").Value.ToString(),
                        BranchAddress = p.Element("Branch_Address").Value.ToString(),
                        BranchCity = p.Element("City").Value.ToString()/*,
                        BranchZipCode = int.Parse(p.Element("Zip_Code").Value),
                        BranchType = p.Element("Branch_Type").Value.ToString(),
                        Telephone = p.Element("Telephone").Value.ToString(),
                        Fax = p.Element("Fax").Value.ToString()*/
                    }).FirstOrDefault();
        }
        /*
        public Order GetBranch(long key)
        {
            return (Order)(from p in BranchRoot.Elements()
                           where (long)Convert.ToInt32(p.Element("OrderKey").Value) == key
                           select new Order()
                           {
                               OrderKey = (long)Convert.ToInt32(p.Element("OrderKey").Value),
                               HostingUnitKey = (long)Convert.ToInt32(p.Element("HostingUnitKey").Value),
                               GuestRequestKey = (long)Convert.ToInt32(p.Element("GuestRequestKey").Value),
                               CreateDate = DateTime.Parse(p.Element("CreateDate").Value),
                               OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                               Status = (OrderStatusCode)Enum.Parse(typeof(OrderStatusCode), p.Element("Status").Value),
                               CommissionValue = float.Parse(p.Element("CommissionValue").Value)
                           }).FirstOrDefault();
        }
        */

        public List<string> getBanksList()
        {
            /* 
            select new { AmountKey = g.Key, Requests = g.ToList() })
                                 .ToDictionary(x => x.AmountKey, x => x.Requests);
            
                    */
            return (from i in BranchRoot.Elements()
                    orderby Convert.ToInt32(i.Element("Bank_Code").Value.Trim())
                    group i by i.Element("Bank_Code").Value.Trim() into ii
                    select ii.First().Element("Bank_Code").Value + " - " + ii.First().Element("Bank_Name").Value).ToList();
        }

        public List<string> getBranchesList(string bankName)
        {
            /* 
            select new { AmountKey = g.Key, Requests = g.ToList() })
                                 .ToDictionary(x => x.AmountKey, x => x.Requests);
            
                    */
            return (from i in BranchRoot.Elements()
                    orderby i.Element("Branch_Name").Value
                    where i.Element("Bank_Name").Value.Equals(bankName)
                    select i.Element("Branch_Code").Value + " - " + i.Element("Branch_Name").Value).ToList();
        }



    }
}

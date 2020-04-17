using BE;
using DAL.ClassXml;
using DS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DAL.ClassXml.XmlConfiguration;

namespace DAL
{
    public class DAL_XML_imp : Idal
    {
        XmlOrder XO = FactorySingletonXmlOrder.GetXmlOrder();
        //XmlHostingUnit XH = FactorySingletonXmlHostingUnit.GetXmlHostingUnit();
        XmlGuestRequest XG = FactorySingletonXmlGuestRequest.GetXmlGuestRequest();
        public XmlConfiguration XC = FactorySingletonXmlConfiguration.GetXmlConfiguration();
        XmlBankBranch XB = FactorySingletonXmlBankBranch.GetXmlBankBranch();


        public readonly string HostingUnitPath = @"HostingUnitXml.xml";

        public bool AddOrder(Order order)
        //public void AddOrder(Order order)
        {
            XO.AddOrder(order);
            return true;
        }

        public bool AddClientRequest(GuestRequest guestRequest)
        //public long AddRequest(GuestRequest guestRequest)
        {
            //return XG.AddRequest(guestRequest);
            XG.AddRequest(guestRequest);
            return true;
        }

        public bool AddHostingUnit(HostingUnit hostingUnit)
        //public long AddUnit(HostingUnit hostingUnit)
        {
            HostingUnit hostingUnitCopy = (HostingUnit)hostingUnit.Clone();
            hostingUnitCopy.HostingUnitKey = XC.GetConfiguration<int>("HostUnitKey");
            DataSource.ListHostingUnits.Add(hostingUnitCopy);
            XmlDataSource.SaveToXML<List<HostingUnit>>(DataSource.ListHostingUnits, HostingUnitPath);
            XC.UpdateConfiguration<int>("HostUnitKey", (hostingUnitCopy.HostingUnitKey + 1));
            //return hostingUnitCopy.HostingUnitKey;
            return true;
            //XH.AddUnit(hostingUnit);
        }

        public float GetCommission()
        {
            return XC.GetConfiguration<float>("SumConnission");
        }

        /*
        public List<BankAccount> GetAllBankBranches()
        {
            throw new NotImplementedException();
        }*/
        //List<GuestRequest> LGrequest(Func<GuestRequest, bool> predicat = null) ??????
        public List<GuestRequest> GetAllGuestRequests()
        {
            return XmlDataSource.LoadFromXML<List<GuestRequest>>(XG.GuestRequestPath);
        }
        //List<HostingUnit> Lunit(Func<HostingUnit, bool> predicat = null) ??????
        public List<HostingUnit> GetAllHostingUnits()
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
        }
        //List<Order> Lorder(Func<Order, bool> predicat = null) ???????
        public List<Order> GetAllOrders()
        {
            return XO.GetAllOrders();
        }
        public GuestRequest GetClientRequest(int GKey)
        //public GuestRequest GetGuestRequest(long key)
        {
            return XG.GetGuestRequest(GKey);
        }
        public HostingUnit GetHostingUnit(int HKey)
        //public HostingUnit GetHostingUnit(long key)
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath).Where(item => item.HostingUnitKey == HKey).FirstOrDefault();
            //XH.GetHostingUnit(key);
        }
        public Order GetOrder(int OKey)
        //public Order GetOrder(long key)
        {
            return XO.GetOrder(OKey);
        }

        public float GetSumConnission()
        {
            return Configuration.fee;
        }

        public bool IsBookingAssociatedHostingUnit(long key)
        {
            return XmlDataSource.LoadFromXML<List<Order>>(XO.OrderPath)
                .Where(item => item.HostingUnitKey == key)
                    .Select(item => GetClientRequest(item.GuestRequestKey))
                        .Any(item => item.StatusRequest == Request_Status.Active);
        }

        public bool IsCollectionClearance(HostingUnit hostingUnit)
        {
            return hostingUnit.Owner.CollectionClearance;
        }

        public bool IsExistOrderOpenForHost(long hostingUnitKey)
        {
            return XO.IsExistOrderOpenForHost(hostingUnitKey);
        }

        public bool IsOrderExist(Order order)
        {
            return XO.IsOrderExist(order.OrderKey);
        }

        public bool IsOrderExist(long key)
        {
            return XO.IsOrderExist(key);
        }

        public bool IsOrderStatus(Order order, OrderStatus orderStatus)
        {
            return XO.IsOrderStatus(order, orderStatus);
        }

        public bool IsRequestExit(GuestRequest guestRequest)
        {
            return IsRequestExit(guestRequest.GuestRequestKey);
        }

        public bool IsRequestExit(long key)
        {
            return XG.IsRequestExit(key);
        }

        public bool IsUnitExist(HostingUnit hostingUnit)
        {
            return IsUnitExist(hostingUnit.HostingUnitKey);
        }

        public bool IsUnitExist(long key)
        {
            return XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath).Any(item => item.HostingUnitKey == key);
        }

        public bool DeleteHostingUnit(HostingUnit Dunit)
        //public void RemoveUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> lis = XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            lis.RemoveAll(item => item.HostingUnitKey == Dunit.HostingUnitKey);
            XmlDataSource.SaveToXML<List<HostingUnit>>(lis, HostingUnitPath);
            return true;
        }

        public void UpdateAllOrdersOfGuestRequest(Order order)
        {
            XO.UpdateAllOrdersOfGuestRequest(order);
        }
        public bool UpdateOrder(Order Uor, OrderStatus status)
        //public void UpdateOrder(Order order)
        {
            Uor.StatusOrder = status;
            XO.UpdateOrder(Uor);
            return true;
        }

        public bool UpdateClientRequest(GuestRequest guestRequest, Request_Status request_Status)
        //public void UpdateRequest(GuestRequest guestRequest)
        {
            guestRequest.StatusRequest = request_Status;
            XG.UpdateRequest(guestRequest);
            return true;
        }
        public bool UpdateHostingUnit(HostingUnit Uunit)
        //public void UpdateUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> lis = XmlDataSource.LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            HostingUnit unit = lis.FirstOrDefault(item => item.HostingUnitKey == Uunit.HostingUnitKey);
            foreach (var Property in unit.GetType().GetProperties())
            {
                ParameterInfo[] myParameters = Property.GetIndexParameters();
                if (myParameters.Length == 0)
                {
                    Property.SetValue(unit, Property.GetValue(Uunit));
                }
            }
            XmlDataSource.SaveToXML<List<HostingUnit>>(lis, HostingUnitPath);
            return true;
        }

        public void initilizeListToXml()
        {
            XmlDataSource.SaveToXML<List<HostingUnit>>(DataSource.ListHostingUnits, HostingUnitPath);
            // XmlDataSource.SaveToXML<List<GuestRequest>>(DataSource.GuestRequestsCollection ,XG.GuestRequestPath);
            //XmlDataSource.SaveToXML<List<Order>>(DataSource.OrdersCollection,XO.OrderPath);
            //XC.AddConfiguration();
        }

        public void SetMailAndPasswordToConfiguration(string mail, string password)
        {
            XC.UpdateConfiguration<string>("mailAddress", mail);
            XC.UpdateConfiguration<string>("Password", password);
        }

        public string GetMail()
        {
            return XC.GetConfiguration<string>("mailAddress");
        }

        public string GetPassword()
        {
            return XC.GetConfiguration<string>("Password");
        }

        public string GetWebUsername()
        {
            return XC.GetConfiguration<string>("WebUsername");
        }

        public string GetWebPassword()
        {
            return XC.GetConfiguration<string>("WebPassword");
        }
        //List<BankBranch> Lbank()
        public List<string> GetBanksList()
        {
            return XB.getBanksList();
        }

        public List<string> GetBranchesList(string BankName)
        {
            return XB.getBranchesList(BankName);
        }

        public BankBranch GetBranch(int bankKey, int branchKey)
        {
            return XB.GetBranch(bankKey, branchKey);
        }

        public string msg()
        {
            return XC.GetConfiguration<string>("Msg");
        }
        
        public List<HostingUnit> Lunit(Func<HostingUnit, bool> predicat = null)
        {
            throw new NotImplementedException();
        }
        
        public List<GuestRequest> LGrequest(Func<GuestRequest, bool> predicat = null)
        {
            throw new NotImplementedException();
        }
        
        public List<Order> Lorder(Func<Order, bool> predicat = null)
        {
            throw new NotImplementedException();
        }
        
        public List<BankBranch> Lbank()
        {
            throw new NotImplementedException();
        }
    }
}

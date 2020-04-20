using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;


namespace DAL
{
   class Dal_imp : Idal
    {
        public List<Order> Lorder(Func<Order, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.ListOrder.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.ListOrder
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }

        public List<HostingUnit> Lunit(Func<HostingUnit, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.ListHostingUnits.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.ListHostingUnits
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }

        public List<GuestRequest> LGrequest(Func<GuestRequest, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.ListGuestRequest.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.ListGuestRequest
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }

        public List<BankBranch> Lbank()
        {
            return DataSource.ListBankBranch;
        }

        public List<BankBranch> returnLbank()
        {
            var BankBranches = DataSource.ListHostingUnits.Select(x => x.Owner.BankBranchDetails).ToList();
            return BankBranches;
        }

        //GuestRequest:
        public bool AddClientRequest(GuestRequest Grect)
        {
            if (Grect.GuestRequestKey != 0 && DataSource.ListGuestRequest.Exists(item => item.GuestRequestKey == Grect.GuestRequestKey) == true)
                throw new Exception("Guest Request with the same number already exists.");

            Grect.GuestRequestKey = Configuration.guestRequestKeySeq++;
            DataSource.ListGuestRequest.Add(Grect.Clone());
            return true;
        }

        public bool UpdateClientRequest(GuestRequest Up, Request_Status Rs)
        {
            int index = DataSource.ListGuestRequest.FindIndex(item => item.GuestRequestKey == Up.GuestRequestKey);
            if (index == -1)
                throw new Exception("Guest Request with the same number not found.");
            DataSource.ListGuestRequest[index].StatusRequest = Rs;
            return true;
        }

        //HostingUnit:
        public bool AddHostingUnit(HostingUnit Hunit)
        {
            if (Hunit.HostingUnitKey != 0 && DataSource.ListHostingUnits.Exists(item => item.HostingUnitKey == Hunit.HostingUnitKey) == true)
                throw new Exception("Hosting Unit with the same id already exists.");

            Hunit.HostingUnitKey = Configuration.HostingUnitKeySeq++;
            DataSource.ListHostingUnits.Add(Hunit);
            return true;
        }
        public bool DeleteHostingUnit(HostingUnit Dunit)
        {
            HostingUnit temp = DataSource.ListHostingUnits.Find(x => x.HostingUnitKey == Dunit.HostingUnitKey);
            if (temp != null)//if there is no unit such like that in the list
            {
                DataSource.ListHostingUnits.Remove(Dunit);
                return true;
            }
            throw new Exception("Hosting Unit with the same id not found.");

        }

       public bool UpdateHostingUnit(HostingUnit Uunit)
        {
            int index = DataSource.ListHostingUnits.FindIndex(item => item.HostingUnitKey == Uunit.HostingUnitKey);
            if (index == -1)
                throw new Exception("Hosting Unit with the same number not found.");
            DataSource.ListHostingUnits[index] = Uunit;
            return true;
        }

        //Order:
        public bool AddOrder(Order Aor)
        {
            Aor.OrderKey = Configuration.OrderKeySeq++;
            Aor.CreateDate = DateTime.Now;
            DataSource.ListOrder.Add(Aor.Clone());
            return true;
        }

        public bool UpdateOrder(Order Uor, OrderStatus newstatus)
        {
            int index = DataSource.ListOrder.FindIndex(item => item.OrderKey == Uor.OrderKey);
            if (index == -1)
                throw new Exception("Order with the same number not found.");
            DataSource.ListOrder[index].StatusOrder = newstatus;
            return true;
        }

        public GuestRequest GetClientRequest(int GKey)
        {
            var g = (from guest in DataSource.ListGuestRequest
                     where guest.GuestRequestKey == GKey
                     select guest).FirstOrDefault();
            if (g == null)
                throw new Exception("Guest with Key [" + GKey + "] does not exist");
            return g.Clone();
        }

        public HostingUnit GetHostingUnit(int HKey)
        {
            var h = (from unit in DataSource.ListHostingUnits
                     where unit.HostingUnitKey == HKey
                     select unit).FirstOrDefault();
            if (h == null)
                throw new Exception("Unit with Key [" + HKey + "] does not exist");
            return h.Clone();
        }

        public Order GetOrder(int OKey)
        {
            var o = (from order in DataSource.ListOrder
                     where order.OrderKey == OKey
                     select order).FirstOrDefault();
            if (o == null)
                throw new Exception("Order with Key [" + OKey + "] does not exist");
            return o.Clone();
        }


        public string GetMail()
        {
            return Configuration.mailAddress.Address;// XC.GetConfiguration<string>("mailAddress");
        }

        public string GetPassword()
        {
            return Configuration.Password; //XC.GetConfiguration<string>("Password");
        }

        public void initilizeListToXml()
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            return Configuration.UserName;
        }

        public string GetUserPassword()
        {
            return Configuration.UserPassword;
        }

        public DateTime GetLastDate()
        {
            return Configuration.TheLastDate;   
        }

        public void SetLastDate(DateTime LastDate)
        {
            Configuration.TheLastDate = LastDate;
        }

    }
}

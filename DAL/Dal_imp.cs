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
        public List<Order> Lorder => DataSource.ListOrder;
        public List<HostingUnit> Lunit => DataSource.ListHostingUnits;
        public List<GuestRequest> LGrequest => DataSource.ListGuestRequest;
        public List<BankBranch> Lbank => DataSource.ListBankBranch;
        public List<BankBranch> returnLbank()
        {
            var BankBranches = DataSource.ListHostingUnits.Select(x => x.Owner.BankBranchDetails).ToList();
            return BankBranches;
        }

        //GuestRequest:
        public bool AddClientRequest(GuestRequest Grect)
        {
            if (DataSource.ListGuestRequest.Exists(item => item.GuestRequestKey == Grect.GuestRequestKey) == true)

                throw new Exception("Guest Request with the same number already exists.");
            else
                DataSource.ListGuestRequest.Add(Grect);
            return true;
        }
        public bool UpdateClientRequest(GuestRequest Up)
        {
            int index = DataSource.ListGuestRequest.FindIndex(item => item.GuestRequestKey == Up.GuestRequestKey);
            if (index == -1)
                throw new Exception("Guest Request with the same number not found.");
            DataSource.ListGuestRequest[index] = Up;
            return true;
        }

        //HostingUnit:
        public bool AddHostingUnit(HostingUnit Hunit)
        {
            if (DataSource.ListHostingUnits.Exists(item => item.HostingUnitKey == Hunit.HostingUnitKey) == true)
                throw new Exception("Hosting Unit with the same id already exists.");
            else
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
                DataSource.ListOrder.Add(Aor);
            return true;
        }
        public bool UpdateOrder(Order Uor)
        {
            int index = DataSource.ListOrder.FindIndex(item => item.OrderKey == Uor.OrderKey);
            if (index == -1)
                throw new Exception("Order with the same number not found.");
            DataSource.ListOrder[index] = Uor;
            return true;
        }


       
        
        //public List<Order> Lorder
        //{
        //    return DataSource.ListOrder;
        //}
     

    }
}

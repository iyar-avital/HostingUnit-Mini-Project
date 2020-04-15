using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using DS;

namespace BL
{
    class BL_imp : IBL
    {
        Idal dal = FactoryDal.getDal();

        //////////////////////ClientRequest///////////////////////


        //Add Client Request
        public bool AddClientRequest(GuestRequest G)
        {
            if (G.EntryDate >= G.ReleaseDate)
                return dal.AddClientRequest(G);
            else
            {
                throw new Exception("One of The dates is not correct.");
            }
        }

        // UpdateClientRequest
        public bool UpdateClientRequest(GuestRequest Up)
        {
            return dal.UpdateClientRequest(Up);
        }


        //////////////////////Hosting Unit///////////////////////


        //Add Hosting Unit
        public bool AddHostingUnit(HostingUnit Hunit)
        {
            return dal.AddHostingUnit(Hunit);
        }


        //Delete Hosting unit
        public bool DeleteHostingUnit(HostingUnit D)
        {
            var h = from item in /*DataSource.ListOrder*/dal.Lorder
                    where item.HostingUnitKey == D.HostingUnitKey
                    select new { StatusOrder = item.StatusOrder };

            var g = h.FirstOrDefault(item => (item.StatusOrder.Equals(OrderStatus.Sent_Mail) || item.StatusOrder.Equals(OrderStatus.Closed_from_customer_not_respone)));

            if (g == null)
                return dal.DeleteHostingUnit(D);
            else
            {
                throw new Exception("[Key:" + D.HostingUnitKey + "] The hosting unit cant be deleted!.");
            }
        }


        //UpdateHostingUnit
        public bool UpdateHostingUnit(HostingUnit Uunit)
        {
            var h = (from item in dal.Lunit
                     where item.HostingUnitKey == Uunit.HostingUnitKey
                     select item.Clone()).First();

            if (!Uunit.Owner.CollectionClearance && h.Owner.CollectionClearance)
            {
                foreach (var item in DataSource.ListOrder)
                {
                    if (h.HostingUnitKey == Uunit.HostingUnitKey)
                    {
                        if (item.StatusOrder == OrderStatus.Sent_Mail)
                            throw new Exception("The Field Of Collection Clearance Cannot be false");
                    }
                }
            }
            return dal.UpdateHostingUnit(Uunit);
        }

        //////////////////////Order///////////////////////

        //AddOrder
        public bool AddOrder(Order Aor)
        {
            var guestReq = (from item in dal.LGrequest
                            where item.GuestRequestKey == Aor.GuestRequestKey
                            select item.Clone()).First();

            var hostingUnit = (from item in dal.Lunit
                               where item.HostingUnitKey == Aor.HostingUnitKey
                               select item.Clone()).First();

            //throw exeption
            if (guestReq == null && hostingUnit != null)
                throw new Exception("The Guest Request Is Not Exiest!");
            else if (hostingUnit == null && guestReq != null)
                throw new Exception("The Hosting Unit Is Not Exiest!");
            else if (hostingUnit == null && guestReq == null)
                throw new Exception("The Guest Request and Hosting Unit Are Not Exiest!");

            if (ApproveReq(Aor))//return true if the guest request approved.
                return dal.AddOrder(Aor);
            throw new Exception("the date of this order has already been taken... Sorry");
        }



        //Update Order Status
        public bool UpdateOrder(Order O)
        {
            switch (O.StatusOrder)
            {
                case OrderStatus.Sent_Mail:
                    {
                        var h = (from item in dal.Lunit
                                 where item.HostingUnitKey == O.HostingUnitKey
                                 select item.Clone()).First();
                        if (h.Owner.CollectionClearance == null)
                        {
                            throw new Exception("Owner doesnt have Collection Clearance ");
                        }
                        Console.WriteLine("A mail is sent");
                        dal.UpdateOrder(O);
                        break;
                    }
                case OrderStatus.Closed_from_customer_not_respone:
                case OrderStatus.Closed_on_customer_response:
                    {
                        //calculate the fee for every day
                        int Cal_fee = CalFee(O);
                        // mark the days
                        Mdays(O);
                        dal.UpdateOrder(O);
                        foreach (var i in dal.Lorder)
                        {
                            if (i.GuestRequestKey == O.GuestRequestKey && i.HostingUnitKey != O.HostingUnitKey)
                            {
                                dal.UpdateOrder(O);
                            }
                        }
                        throw new Exception("Cant change the status!!!.");
                    }
            }
            return true;
        }


        public GuestRequest GetClientRequest(int GKey)
        {
            var g = (from guest in dal.LGrequest
                    where guest.GuestRequestKey == GKey
                    select guest).FirstOrDefault();
            if (g == null)
                throw new Exception("Guest with Key [" + GKey + "] does not exist");
            return g;
        }

        public HostingUnit GetHostingUnit(int HKey)
        {
            var h = (from unit in dal.Lunit
                     where unit.HostingUnitKey == HKey
                     select unit).FirstOrDefault();
            if (h == null)
                throw new Exception("Unit with Key [" + HKey + "] does not exist");
            return h;
        }

        public Order GetOrder(int OKey)
        {
            var o = (from order in dal.Lorder
                     where order.OrderKey == OKey
                     select order).FirstOrDefault();
            if (o == null)
                throw new Exception("Unit with Key [" + OKey + "] does not exist");
            return o;
        }



        public List<HostingUnit> Lunit()
        {
            return dal.Lunit;
        }

        public List<GuestRequest> LGrequest()
        {
            return dal.LGrequest;
        }

        public List<Order> Lorder()
        {
            return dal.Lorder;
        }

        public List<BankBranch> Lbank()
        {
            return dal.Lbank;
        }


        //////////////////////Approve///////////////////////

        //check if the order is available
        public bool ApproveReq(Order or)
        {
            var guestReq = (from item in dal.LGrequest
                            where item.GuestRequestKey == or.GuestRequestKey
                            select item.Clone()).First();

            var hostingUnit = (from item in dal.Lunit
                               where item.HostingUnitKey == or.HostingUnitKey
                               select item.Clone()).First();

            int EnterDay = guestReq.EntryDate.Day;
            int EnterMonth = guestReq.EntryDate.Month;
            int num = NumOfDays(guestReq.EntryDate, guestReq.ReleaseDate);//return num of days

            bool flag = true;// if flag = false the date is taken already by someone
            int counter = 0;

            for (int i = EnterMonth; i < 13; i++)
            {

                for (int j = EnterDay; j < 32; j++)
                {

                    counter++;
                    if (hostingUnit.Diary[i, j] == true)

                        flag = false;
                    if (flag == false || counter == num)// finish checking or error
                        break;
                }
                EnterDay = 1;
                if (flag == false || counter == num)// finish checking or error
                    break;

            }
            if (flag == true)
                return true;
            return false;
        }

        //check if the hosting unit is available
        public bool ApproveHostU(HostingUnit host, DateTime date, int num)
        {

            int EnterDay = date.Day;
            int EnterMonth = date.Month;
            int d = num;

            bool flag = true;// if flag = false the date has already been taken by someone.
            int counter = 0;

            for (int i = EnterMonth; i < 13; i++)
            {

                for (int j = EnterDay; j < 32; j++)
                {

                    counter++;

                    if (host.Diary[i, j] == true)
                        flag = false;

                    if (flag == false || counter == num)// finish checking or error
                        break;

                }

                EnterDay = 1;
                if (flag == false || counter == num)// finish checking or error
                    break;

            }

            if (flag == true)
                return true;
            return false;
        }


        //////////////////////Calculating///////////////////////

        // Calculating fee
        public int CalFee(Order O)
        {
            var g = (from item in dal.LGrequest
                     where item.GuestRequestKey == O.GuestRequestKey
                     select item.Clone()).First();
            return Configuration.fee * NumOfDays(g.EntryDate, g.ReleaseDate);
        }


        // Counting the days since entering until exit
        public int NumOfDays(DateTime date1, DateTime date2)
        {
            int DEntry = date1.Day;
            int MEntry = date1.Month;
            int Dexist = date2.Day;
            int Mexist = date2.Month;

            while (MEntry != Mexist)
            {
                Dexist = Dexist + 31;
                Mexist--;
            }
            int Ndays = Dexist - DEntry;

            return Ndays;
        }

        //num of days when the input is 2 values
        public int NumOfDays2(DateTime date1, DateTime date2)
        {
            return NumOfDays(date1, date2);
        }

        //num of days when the input is 1 value
        public int NumOfDays1(DateTime date1)
        {
            return NumOfDays(date1, DateTime.Now);
        }

        //////////////////////Marking days///////////////////////

        //Marking the days when closing order
        public void Mdays(Order od)
        {
            var h = (from item in dal.Lunit
                     where item.HostingUnitKey == od.HostingUnitKey
                     select item.Clone()).First();

            var g = (from item in dal.LGrequest
                     where item.GuestRequestKey == od.GuestRequestKey
                     select item.Clone()).First();

            int EnterDay = g.EntryDate.Day;

            int EnterMonth = g.EntryDate.Month;

            int num = NumOfDays(g.EntryDate, g.ReleaseDate);//return num of days

            for (int i = EnterMonth; i < 13; i++)
            {

                for (int j = EnterDay; j < 32; j++)
                {
                    int counter = 0;

                    if (counter == num)
                        break;

                    while (counter != num)
                    {
                        counter += 1;
                        h.Diary[i, j] = true;
                    }
                }
            }
        }

        
    }
}


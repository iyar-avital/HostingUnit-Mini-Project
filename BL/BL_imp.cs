using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
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

        public Thread orderThread { get; set; } = null;

        #region Thread Order
        private void ThreadOrderMoreThanMonth()
        {
            if ((DateTime.Now - dal.GetLastDate()).Days == 0)
                return;
            dal.SetLastDate(DateTime.Now);
            List<Order> orders = dal.Lorder(item => item.StatusOrder == OrderStatus.Sent_Mail);
            foreach (var order in orders)
            {
                if ((DateTime.Now - order.CreateDate).Days >= 30)
                    dal.UpdateOrder(order, OrderStatus.Closed_from_customer_not_respone);
            }

            try
            {
                Thread.Sleep(86400000);
            }catch(ThreadInterruptedException e)
            {
                //NOTHING
            }
        }

        public void OrderMoreThanMonth()
        {
            orderThread = new Thread(ThreadOrderMoreThanMonth);
            //orderThread.
            orderThread.Start();
        }

        #endregion

        #region Group
        public IEnumerable<IGrouping<Areas, GuestRequest>> GroupByArea()
        {
            var group = dal.LGrequest().GroupBy(item => item.Area).OrderBy(item => item.Key);
            return group;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GroupByPeople()
        {
            var group = from item in dal.LGrequest()
                    group item by item.Adults + item.Children into list
                    select list;
            var g = group.OrderBy(item => item.Key);
            return g;
        }

        public IEnumerable<IGrouping<int, Host>> GroupHostByNumOfUnit()
        {
            List<HostingUnit> hostingUnits = dal.Lunit();
            var Unit = from item in hostingUnits
                            group item by item.Owner.HostKey into g
                            select g.ToList();
            var groug = from item in Unit
                        group item[0].Owner by item.Count() into g
                        select g;
            return groug;
        }

        public IEnumerable<IGrouping<Areas, HostingUnit>> GroupByAreaOfUnit()
        {
            List<HostingUnit> hostingUnits = dal.Lunit();
            var group = from item in hostingUnits
                        group item by item.Area into s
                        select s;
            var g = group.OrderBy(item => item.Key);
            return g;
        }
        #endregion

        //Add Client Request
        public bool AddClientRequest(GuestRequest G)
        {
            if (G.EntryDate < G.ReleaseDate)
                return dal.AddClientRequest(G);
            else
            {
                throw new Exception("One of The dates is not correct.");
            }
        }

        // UpdateClientRequest
        public bool UpdateClientRequest(GuestRequest Up, Request_Status Rs)
        {
            return dal.UpdateClientRequest(Up, Rs);
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
            var h = from item in /*DataSource.ListOrder*/dal.Lorder()
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
            var h = (from item in dal.Lunit()
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
            var guestReq = dal.LGrequest(item => item.GuestRequestKey == Aor.GuestRequestKey);

            var hostingUnit = dal.Lunit(item => item.HostingUnitKey == Aor.HostingUnitKey);

            //throw exeption
            if (hostingUnit == null && guestReq == null)
                throw new Exception("The Guest Request and Hosting Unit Are Not Exiest!");
            else if (guestReq == null)
                throw new Exception("The Guest Request Is Not Exiest!");
            else if (hostingUnit == null)
                throw new Exception("The Hosting Unit Is Not Exiest!");

            //checks if the order already exists
            foreach (var order in dal.Lorder())
                if (order.HostingUnitKey == Aor.HostingUnitKey && order.GuestRequestKey == Aor.GuestRequestKey)
                    throw new Exception("The Order already exists [" + order.OrderKey + "]");

            if (ApproveReq(Aor))//return true if the guest request approved.
            {
                dal.AddOrder(Aor);
                UpdateOrder(Aor, OrderStatus.Sent_Mail);
                return true;
            }
            throw new Exception("the date of this order has already been taken... Sorry");
        }



        //Update Order Status
        public bool UpdateOrder(Order O, OrderStatus status)
        {
            if (status == OrderStatus.Sent_Mail)
            {
                try
                {
                    SendEmail(O);
                    return true;
                }
                catch (Exception e)
                {

                    throw e;
                }
            }

            if (O.StatusOrder == OrderStatus.Closed_from_customer_not_respone || O.StatusOrder == OrderStatus.Closed_on_customer_response)
                throw new Exception("Invitation status cannot be changed after it is closed");

            GuestRequest guestRequest_order = dal.GetClientRequest(O.GuestRequestKey);
            HostingUnit hostingUnit_order = dal.GetHostingUnit(O.HostingUnitKey);

            if (O.StatusOrder == OrderStatus.Closed_on_customer_response)
            {
                //checks if the dates are busy because maybe another customer has already closed these dates
                for (DateTime date = guestRequest_order.EntryDate; date < guestRequest_order.ReleaseDate; date = date.AddDays(1))
                {
                    if (hostingUnit_order[date] == true)
                        throw new Exception("These dates are busy");
                }
            }

            dal.UpdateOrder(O.Clone(), status);

            //save the dates
            if (status == OrderStatus.Closed_from_customer_not_respone)
            {
                //calculate the fee for every day
                float Cal_fee = CalFee(O);
                // mark the days
                Mdays(O);
                dal.UpdateClientRequest(guestRequest_order, Request_Status.Closed_by_Web);

                List<Order> allTheCostumerOrders = dal.Lorder(item => item.GuestRequestKey == O.GuestRequestKey);
                foreach (var order in allTheCostumerOrders)
                {
                    if (order.OrderKey != O.OrderKey)
                        dal.UpdateOrder(order, OrderStatus.Closed_from_customer_not_respone);
                }
            }
            return true;
        }

        private void SendEmail(Order O)
        {
            GuestRequest guestRequest_order = dal.GetClientRequest(O.GuestRequestKey);
            HostingUnit hostingUnit_order = dal.GetHostingUnit(O.HostingUnitKey);
            if (hostingUnit_order.Owner.CollectionClearance == false)
                throw new Exception("Order status cannot be changed without bank account authorization");
            dal.UpdateOrder(O.Clone(), OrderStatus.Sent_Mail);
            MailMessage mail = new MailMessage();
            mail.To.Add(guestRequest_order.MailAddress);
            mail.From = new MailAddress(dal.GetMail());
            mail.Subject = "הזמנה ליחידת אירוח " + O.HostingUnitKey;
            mail.Body = "<div>" +
                         "<p1>הזמנה ליחידת אירוח " + O.HostingUnitKey + "</p1>" +
                         "<div>הזמנה מספר " + O.OrderKey + "</div>" +
                         "</div>";

                         
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(dal.GetMail(), dal.GetPassword());//"xxxxxxx@gmail.com", "password");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GuestRequest GetClientRequest(int GKey)
        {
            try
            {
                return dal.GetClientRequest(GKey);
            }
            catch (Exception arr)
            {
                throw arr;
            }
        }

        public HostingUnit GetHostingUnit(int HKey)
        {
            try
            {
                return dal.GetHostingUnit(HKey);
            }
            catch (Exception arr)
            {
                throw arr;
            }
        }

        public Order GetOrder(int OKey)
        {
            try
            {
                return dal.GetOrder(OKey);
            }
            catch (Exception arr)
            {
                throw arr;
            }
        }

        public List<HostingUnit> Lunit(Func<HostingUnit, bool> predicat = null)
        {
            return dal.Lunit(predicat);
        }

        public List<GuestRequest> LGrequest(Func<GuestRequest, bool> predicat = null)
        {
            return dal.LGrequest(predicat);
        }

        public List<Order> Lorder(Func<Order, bool> predicat = null)
        {
            return dal.Lorder(predicat);
        }

        public List<BankBranch> Lbank()
        {
            return dal.Lbank();
        }
        //////////////////////Approve///////////////////////

        //check if the order is available
        public bool ApproveReq(Order or)
        {
            var guestReq = (from item in dal.LGrequest()
                            where item.GuestRequestKey == or.GuestRequestKey
                            select item.Clone()).First();

            var hostingUnit = (from item in dal.Lunit()
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
        public float CalFee(Order O)
        {
            var g = (from item in dal.LGrequest()
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
            var h = (from item in dal.Lunit()
                     where item.HostingUnitKey == od.HostingUnitKey
                     select item.Clone()).First();

            var g = (from item in dal.LGrequest()
                     where item.GuestRequestKey == od.GuestRequestKey
                     select item.Clone()).First();

            DateTime EnterDay = g.EntryDate;

            DateTime ReleaseDate = g.ReleaseDate;


            for (DateTime i = EnterDay; i < ReleaseDate; i = i.AddDays(1))
            {
                h[i] = true;
            }
        }

        public void initXmls()
        {
            dal.initilizeListToXml();
        }

        public string GetUserName()
        {
            return dal.GetUserName();
        }

        public string GetUserPassword()
        {
            return dal.GetUserPassword();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class classMain
    {
        static void Main(string[] args)
        {
            IBL BL1 = FactoryBL.getBL();

            //example1  of HostingUnit
            HostingUnit h1 = new HostingUnit();
            h1.HostingUnitKey = ++Configuration.HostingUnitKeySeq;
            h1.Owner.HostKey = 00000002;
            h1.Owner.PrivateName = "יוסי";
            h1.Owner.FamilyName = "כהן";
            h1.Owner.PhoneNumber = 0509876543;
            h1.Owner.MailAddress = "Ychoen@gmail.com";
            h1.Owner.BankBranchDetails.BankNumber = 12;
            h1.Owner.BankBranchDetails.BankName = "הפועלים";
            h1.Owner.BankBranchDetails.BranchNumber = 78;
            h1.Owner.BankBranchDetails.BranchAddress = " יפו 67";
            h1.Owner.BankBranchDetails.BranchCity = " תל-אביב";
            h1.Owner.BankAccountNumber = 166685;
            h1.Owner.CollectionClearance = true;
            h1.HostingUnitName = "נוף הגליל";
            h1.Adults = 9;
            h1.Area = Areas.Center;
            h1.Children = 6;
            h1.ChildrensAttractions = true;
            h1.Garden = false;
            h1.Jacuzzi = false;
            h1.Pool = true;
            h1.Type = Type_Unit.Zimmer;
            h1.Diary = new bool[12, 31];

            //example2  of HostingUnit
            HostingUnit h2 = new HostingUnit();
            h2.HostingUnitKey = ++Configuration.HostingUnitKeySeq;
            h2.Owner.HostKey = 00000002;
            h2.Owner.PrivateName = "עופר";
            h2.Owner.FamilyName = "לוי";
            h2.Owner.PhoneNumber = 0509876543;
            h2.Owner.MailAddress = "Levi007@gmail.com";
            h2.Owner.BankBranchDetails.BankNumber = 11;
            h2.Owner.BankBranchDetails.BankName = "דיסקונט";
            h2.Owner.BankBranchDetails.BranchNumber = 78;
            h2.Owner.BankBranchDetails.BranchAddress = " שמואל הנביא 25";
            h2.Owner.BankBranchDetails.BranchCity = " ירושלים";
            h2.Owner.BankAccountNumber = 166685;
            h2.Owner.CollectionClearance = true;
            h2.HostingUnitName = "קראון הוטל";
            h2.Adults = 2;
            h2.Area = Areas.Jerusalem;
            h2.Children = 2;
            h2.ChildrensAttractions = false;
            h2.Garden = true;
            h2.Jacuzzi = true;
            h2.Pool = true;
            h2.Type = Type_Unit.HotelRoom;
            h2.Diary = new bool[12, 31];

            //example1  of GuestRequest
            GuestRequest g1 = new GuestRequest();
            g1.GuestRequestKey = ++Configuration.guestRequestKeySeq;
            g1.PrivateName = "נועה";
            g1.FamilyName = "חלפון";
            g1.MailAddress = "Hnoa@gamil.com";
            g1.StatusRequest = Request_Status.Active;
            g1.RegistrationDate = DateTime.Now;
            g1.EntryDate = new DateTime(2020, 2, 8);
            g1.ReleaseDate = new DateTime(2020, 2, 10);
            g1.Area = Areas.Jerusalem;
            g1.SubArea = Request_SubArea.Jerusalem;
            g1.Type = Type_Unit.HotelRoom;
            g1.Adults = 2;
            g1.Children = 2;
            g1.Pool = Option.Possible;
            g1.Jacuzzi = Option.Not_interested;
            g1.Garden = Option.Not_interested;
            g1.ChildrensAttractions = Option.Possible;

            //example1  of GuestRequest
            GuestRequest g2 = new GuestRequest();
            g2.GuestRequestKey = ++Configuration.guestRequestKeySeq;
            g2.PrivateName = "יעל";
            g2.FamilyName = "גולד";
            g2.MailAddress = "Goldy@gamil.com";
            g2.StatusRequest = Request_Status.Active;
            g2.RegistrationDate = DateTime.Now;
            g2.EntryDate = new DateTime(2020, 2, 4);
            g2.ReleaseDate = new DateTime(2020, 2, 8);
            g2.Area = Areas.North;
            g2.SubArea = Request_SubArea.Hermon;
            g2.Type = Type_Unit.HostingUnit;
            g2.Adults = 3;
            g2.Children = 4;
            g2.Pool = Option.Necessary;
            g2.Jacuzzi = Option.Not_interested;
            g2.Garden = Option.Possible;
            g2.ChildrensAttractions = Option.Necessary;

            //example3  of GuestRequest
            GuestRequest g3 = new GuestRequest();
            g3.GuestRequestKey = ++Configuration.guestRequestKeySeq;
            g3.PrivateName = "שירה";
            g3.FamilyName = "הדר";
            g3.MailAddress = "SH777@gamil.com";
            g3.StatusRequest = Request_Status.Active;
            g3.RegistrationDate = DateTime.Now;
            g3.EntryDate = new DateTime(2020, 3, 5);
            g3.ReleaseDate = new DateTime(2020, 3, 6);
            g3.Area = Areas.Center;
            g3.SubArea = Request_SubArea.Tel_Aviv;
            g3.Type = Type_Unit.Zimmer;
            g3.Adults = 2;
            g3.Children = 5;
            g3.Pool = Option.Possible;
            g3.Jacuzzi = Option.Not_interested;
            g3.Garden = Option.Necessary;
            g3.ChildrensAttractions = Option.Not_interested;
            g3.ChildrensAttractions = Option.Not_interested;

            //1
            Console.WriteLine("Add ClientRequest:");
            try
            {
                BL1.AddClientRequest(g1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //2
            try
            {
                BL1.AddClientRequest(g2);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //3
            try
            {
                BL1.AddClientRequest(g3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //הוספת יחידות אירוח ------------------------------
            //1
            Console.WriteLine("\nAdd HostingUnit:");
            try
            {
                BL1.AddHostingUnit(h1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //2
            try
            {
                BL1.AddHostingUnit(h2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //הוספת הזמנה---------------------------------
            foreach (var item in BL1.Lorder().ToList())
            {
                BL1.AddOrder(item);
            }
            //הדפסת הרשימות-----------------------------------
            Console.WriteLine("\nGuestRequestList: \n");
            foreach (var item in BL1.LGrequest())
            {
                Console.WriteLine(item);
                // Console.WriteLine("  \n   ");
            }
            Console.WriteLine("HostingUnitsList: \n");
            foreach (var item in BL1.Lunit())
            {
                Console.WriteLine(item);
                // Console.WriteLine("  \n   ");
            }
            Console.WriteLine("BankBranchList: \n");
            foreach (var item in BL1.Lbank())
            {
                Console.WriteLine(item);
                Console.WriteLine("  \n   ");
            }
            Console.WriteLine("OrderList: \n");
            foreach (var item in BL1.Lorder())
            {
                Console.WriteLine(item);
                Console.WriteLine("  \n   ");
            }
            //שליחת מיילים-----------------------

            foreach (var item in BL1.Lorder().ToList())
            {
                try
                {
                    // BL1.UpdateOrder(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //  Console.WriteLine("  \n  ");
                }

            }
            //עדכון יחידה------------------------------
            h1.Adults++;
            try
            {
                BL1.UpdateHostingUnit(h1);
                // Console.WriteLine("  \n  ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //  Console.WriteLine("  \n  ");
            }

        }
    }
}

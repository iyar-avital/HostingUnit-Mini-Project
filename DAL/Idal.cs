using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        bool AddClientRequest(GuestRequest Grect);
        bool UpdateClientRequest(GuestRequest Up, Request_Status status);
        bool AddHostingUnit(HostingUnit Hunit);
        bool DeleteHostingUnit(HostingUnit Dunit);
        bool UpdateHostingUnit(HostingUnit Uunit);
        bool AddOrder(Order Aor);
        bool UpdateOrder(Order Uor, OrderStatus status);

        GuestRequest GetClientRequest(int GKey);
        HostingUnit GetHostingUnit(int HKey);
        Order GetOrder(int OKey);

        List<HostingUnit> Lunit(Func<HostingUnit, bool> predicat = null);
        List<GuestRequest> LGrequest(Func<GuestRequest, bool> predicat = null);
        List<Order> Lorder(Func<Order, bool> predicat = null);
        List<BankBranch> Lbank();
        string GetMail();
        string GetPassword();
        void initilizeListToXml();
    }
}

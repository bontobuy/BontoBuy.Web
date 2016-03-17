using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class SupplierOrderRepo : ISupplierOrderRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<OrderViewModel> Retrieve(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            var records = db.Orders.Where(x => x.SupplierUserId == id).ToList();
            if (records == null)
                return null;

            return records;
        }
        public OrderViewModel Get(int id)
        {
            if (id < 1)
                return null;

            var record = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (record == null)
                return null;

            return record;
        }

        public OrderViewModel UpdateOrderStatus(OrderViewModel item)
        {
            if (item == null)
                return null;

            var record = db.Orders.Where(x => x.OrderId == item.OrderId).FirstOrDefault();
            if (record == null)
                return null;

            if (item.ConfirmationCode == record.ConfirmationCode)
            {
                record.Status = "Delivered";
                record.ConfirmationCode = null;
                db.SaveChanges();
            }
            return record;
        }
    }
}
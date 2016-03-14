using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class AdminCommissionRepo : IAdminCommissionRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<CommissionViewModel> Retrieve()
        {
            var records = db.Commissions.ToList();
            if (records == null)
                return null;

            return records;
        }
        public CommissionViewModel Get(int id)
        {
            var record = db.Commissions.Where(x => x.CommissionId == id).FirstOrDefault();
            if (record == null)
                return null;

            return record;
        }
        public CommissionViewModel Create(CommissionViewModel item)
        {
            return null;
        }
        public CommissionViewModel Update(CommissionViewModel item)
        {
            if (item == null)
                return null;

            var itemToUpdate = db.Commissions.Where(x => x.CommissionId == item.CommissionId).FirstOrDefault();
            itemToUpdate.DtUpdated = DateTime.UtcNow;
            itemToUpdate.Percentage = item.Percentage;
            db.SaveChanges();

            return itemToUpdate;
        }
    }
}
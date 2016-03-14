using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BontoBuy.Web.Models;

namespace BontoBuy.Web.App_Start
{
    public class InitializeData
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void InitializeCommissions()
        {
            var commissionRecords = db.Commissions.Where(x => x.Name.Contains("Tier")).ToList();
            if (commissionRecords.Count < 3)
            {
                var commissionList = new List<CommissionViewModel>()
                {
                new CommissionViewModel() { Name = "Tier1", Percentage = 10, DtCreated = DateTime.UtcNow, DtUpdated = DateTime.UtcNow },
                new CommissionViewModel() { Name = "Tier2", Percentage = 12, DtCreated = DateTime.UtcNow, DtUpdated = DateTime.UtcNow },
                new CommissionViewModel() { Name = "Tier3", Percentage = 15, DtCreated = DateTime.UtcNow, DtUpdated = DateTime.UtcNow },
                };

                foreach (var item in commissionList)
                {
                    db.Commissions.Add(item);
                    db.SaveChanges();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ModelRepo : IModelRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ModelViewModel> Retrieve()
        {
            var records = db.Models.ToList();

            return records;
        }

        public ModelAdminViewModel Get(int id)
        {
            var record = db.Models
                .Where(x => x.ModelId == id)
                .FirstOrDefault();

            var model = new ModelAdminViewModel()
            {
                ModelNumber = record.ModelNumber,
                ModelId = record.ModelId,
                DtCreated = record.DtCreated,
                Price = record.Price,
                Status = record.Status,
                BrandName = (from b in db.Brands
                             where b.BrandId == record.BrandId
                             select b.Name).FirstOrDefault(),
                ItemName = (from i in db.Items
                            where i.ItemId == record.ItemId
                            select i.Description).FirstOrDefault(),
                SupplierName = (from s in db.Suppliers
                                where s.SupplierId == record.SupplierId
                                select s.Name).FirstOrDefault(),
                SupplierEmail = (from s in db.Suppliers
                                 where s.SupplierId == record.SupplierId
                                 select s.Email).FirstOrDefault(),
                SupplierId = record.SupplierId
            };

            return model;
        }

        public ModelViewModel Create(ModelViewModel item)
        {
            var newRecord = new ModelViewModel
            {
                BrandId = item.BrandId,
                ModelNumber = item.ModelNumber,
                ItemId = item.ItemId,
            };
            db.Models.Add(newRecord);
            db.SaveChanges();

            return newRecord;
        }

        public ModelViewModel Update(int id, ModelViewModel item)
        {
            var currentrecord = db.Models
               .Where(x => x.ModelId == id)
               .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.ModelNumber)))
            {
                currentrecord.ModelNumber = item.ModelNumber;
                currentrecord.Price = item.Price;
                db.SaveChanges();
                return currentrecord;
            }

            return currentrecord;
        }

        public void Archive(int id)
        {
            var record = db.Models
                .Where(x => x.ModelId == id && x.Status == "Active")
                .FirstOrDefault();

            if (record != null)
            {
                record.Status = "Archived";
                db.SaveChanges();
            }
        }

        public IEnumerable<ModelViewModel> RetrieveArchives()
        {
            var records = (from models in db.Models
                           where models.Status == "Archived"
                           select models).ToList();

            return records;
        }

        public void RevertArchive(int id)
        {
            var record = db.Models
                .Where(x => x.ModelId == id && x.Status == "Archived")
                .FirstOrDefault();

            if (record != null)
            {
                record.Status = "Active";
                db.SaveChanges();
            }
        }
    }
}
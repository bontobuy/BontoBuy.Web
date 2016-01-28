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

        public ModelViewModel Get(int id)
        {
            var record = db.Models
                .Where(x => x.ModelId == id)
                .FirstOrDefault();

            return record;
        }

        public ModelViewModel Create(ModelViewModel Model)
        {
            return null;
        }

        public ModelViewModel Update(int id, ModelViewModel item)
        {
            var currentrecord = db.Models
               .Where(x => x.ModelId == id)
               .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.ModelNumber)))
            {
                currentrecord.ModelNumber = item.ModelNumber;
                db.SaveChanges();
                return currentrecord;
            }

            return currentrecord;
        }

        public void Remove(int id)
        {
        }

        public IEnumerable<ModelViewModel> GetModelsByBrand(int brandId)
        {
            var records =
                from model in db.Models
                join item in db.Items on model.ItemId equals item.ItemId
                where item.BrandId == brandId
                select model;

            return records;
        }
    }
}
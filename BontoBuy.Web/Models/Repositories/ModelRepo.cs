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

        public ModelViewModel Create(ModelViewModel item)
        {
            var newRecord = new ModelViewModel
            {
                BrandId = item.BrandId,
                ModelNumber = item.ModelNumber,
                ItemId = item.ItemId,
                Price = item.Price
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

        public void Remove(int id)
        {
        }

        //public IEnumerable<ModelViewModel> GetModelsByBrand(int brandId)
        //{
        //    var records =
        //        from model in db.Models
        //        join item in db.Items on model.ItemId equals item.ItemId
        //        where item.BrandId == brandId
        //        select model;

        //    return records;
        //}
    }
}
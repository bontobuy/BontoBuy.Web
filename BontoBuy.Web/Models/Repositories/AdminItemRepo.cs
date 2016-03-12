using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BontoBuy.Web.HelperMethods;

namespace BontoBuy.Web.Models
{
    public class AdminItemRepo : IAdminItemRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private Helper helper = new Helper();

        public IEnumerable<AdminRetrieveItemViewModel> Retrieve()
        {
            var records = db.Items.Where(x => x.AdminStatus == "Active").ToList();
            if (records == null)
                return null;

            var itemList = AssignItemList(records);

            if (itemList == null)
                return null;

            return itemList;
        }
        public AdminGetItemViewModel Get(int id)
        {
            if (id < 1)
                return null;

            var record = db.Items.Where(x => x.ItemId == id).FirstOrDefault();
            if (record == null)
                return null;

            var itemDetails = AssignItemDetails(record);
            if (itemDetails == null)
                return null;

            return itemDetails;
        }
        public ItemViewModel Create(AdminCreateItemViewModel item)
        {
            if (item == null || item.ProductId < 1)
                return null;

            var newItem = AssignItemCreation(item);

            if (item == null)
                return null;

            return newItem;
        }
        public AdminUpdateItemViewModel GetUpdateItemViewModel(int id)
        {
            if (id < 1)
                return null;

            var record = db.Items.Where(x => x.ItemId == id).FirstOrDefault();
            if (record == null || record.ItemId < 1)
                return null;

            var itemToUpdate = AssignItemDetailsForUpdate(record);
            if (itemToUpdate == null)
                return null;

            return itemToUpdate;
        }
        public ItemViewModel Update(AdminUpdateItemViewModel item)
        {
            if (item == null || item.ItemId < 1)
                return null;

            var itemToUpdate = AssignItemToUpdate(item);

            return itemToUpdate;
        }
        public IEnumerable<AdminRetrieveItemViewModel> RetrieveArchives()
        {
            var records = db.Items.Where(x => x.AdminStatus == "Archived").ToList();
            if (records == null)
                return null;

            var itemList = AssignItemList(records);
            if (itemList == null)
                return null;

            return itemList;
        }
        public bool Archive(int id)
        {
            var record = db.Items.Where(x => x.ItemId == id).FirstOrDefault();
            if (record == null || record.ItemId < 1)
                return false;

            record.AdminStatus = "Archived";
            db.SaveChanges();

            return true;
        }
        public bool RevertArchive(int id)
        {
            var record = db.Items.Where(x => x.ItemId == id).FirstOrDefault();
            if (record == null || record.ItemId < 1)
                return false;
            record.AdminStatus = "Active";
            db.SaveChanges();

            return true;
        }
        public List<ProductViewModel> AdminRetrieveProducts()
        {
            var records = db.Products.Where(x => x.Status == "Active").ToList();
            if (records == null)
                return null;

            return records;
        }

        private ItemViewModel AssignItemToUpdate(AdminUpdateItemViewModel item)
        {
            if (item == null)
                return null;

            var itemToUpdate = db.Items.Where(x => x.ItemId == item.ItemId).FirstOrDefault();
            if (itemToUpdate == null || itemToUpdate.ItemId < 1)
                return null;

            itemToUpdate.ProductId = item.ProductId;
            itemToUpdate.Description = item.ItemDescription;
            itemToUpdate.DtUpdated = item.DtUpdated;

            db.SaveChanges();

            return itemToUpdate;
        }

        private ItemViewModel AssignItemCreation(AdminCreateItemViewModel item)
        {
            if (item == null || item.ProductId < 1)
                return null;

            item.ItemDescription = helper.ConvertToTitleCase(item.ItemDescription);

            var existingRecord = CheckDuplicates(item.ItemDescription);
            if (existingRecord != null)
                return existingRecord;

            var newItem = new ItemViewModel()
            {
                ProductId = item.ProductId,
                Description = item.ItemDescription,
                AdminStatus = "Active",
                DtCreated = DateTime.UtcNow,
                DtUpdated = DateTime.UtcNow,
            };

            if (newItem == null)
                return null;

            db.Items.Add(newItem);
            db.SaveChanges();

            return newItem;
        }

        private IEnumerable<AdminRetrieveItemViewModel> AssignItemList(List<ItemViewModel> records)
        {
            if (records == null)
                return null;

            var itemList = new List<AdminRetrieveItemViewModel>();

            foreach (var item in records)
            {
                var listItem = new AdminRetrieveItemViewModel()
                {
                    ItemId = item.ItemId,
                    ProductId = item.ProductId,
                    ProductDescription = (from p in db.Products
                                          where p.ProductId == item.ProductId
                                          select p.Description).FirstOrDefault(),
                    ItemDescription = item.Description,
                    DtCreated = item.DtCreated,
                    DtUpdated = item.DtUpdated,
                };

                itemList.Add(listItem);
            }

            return itemList;
        }

        private AdminGetItemViewModel AssignItemDetails(ItemViewModel record)
        {
            if (record == null)
                return null;

            var itemDetails = new AdminGetItemViewModel()
            {
                ItemId = record.ItemId,
                ProductId = record.ProductId,
                ProductDescription = (from p in db.Products
                                      where p.ProductId == record.ProductId
                                      select p.Description).FirstOrDefault(),
                ItemDescription = record.Description,
                Status = record.AdminStatus
            };

            if (itemDetails == null)
                return null;

            return itemDetails;
        }

        private AdminUpdateItemViewModel AssignItemDetailsForUpdate(ItemViewModel record)
        {
            if (record == null || record.ItemId < 1)
                return null;

            var itemToUpdate = new AdminUpdateItemViewModel()
            {
                ItemId = record.ItemId,
                ProductId = record.ProductId,
                ItemDescription = record.Description,
                DtUpdated = DateTime.UtcNow
            };

            if (itemToUpdate == null)
                return null;

            return itemToUpdate;
        }
        private ItemViewModel CheckDuplicates(string description)
        {
            if (String.IsNullOrEmpty(description))
                return null;

            var record = db.Items.Where(x => x.Description == description).FirstOrDefault();
            if (record != null)
                return record;

            return null;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    public class ItemRepo : IItemRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ItemViewModel> Retrieve()
        {
            var records = (from items in db.Items
                           where items.AdminStatus == "Active"
                           select items).ToList();

            return records;
        }

        public ItemViewModel Get(int id)
        {
            var record = db.Items
                .Where(x => x.ItemId == id)
                .FirstOrDefault();

            return record;
        }

        public ItemViewModel Create(ItemViewModel item)
        {
            return null;
        }

        public ItemViewModel Update(int id, ItemViewModel item)
        {
            var currentRecord = db.Items
               .Where(x => x.ItemId == id)
               .FirstOrDefault();

            if (!(String.IsNullOrWhiteSpace(item.Description)))
            {
                currentRecord.Description = item.Description;
                db.SaveChanges();
                return currentRecord;
            }
            return currentRecord;
        }

        public void Archive(int id)
        {
            var record = db.Items
                .Where(x => x.ItemId == id && x.AdminStatus == "Active")
                .FirstOrDefault();

            if (record != null)
            {
                record.AdminStatus = "Archived";
                db.SaveChanges();
            }
        }

        public IEnumerable<ItemViewModel> RetrieveArchives()
        {
            var records = (from items in db.Items
                           where items.AdminStatus == "Archived"
                           select items).ToList();

            return records;
        }

        public void RevertArchive(int id)
        {
            var record = db.Items
                .Where(x => x.ItemId == id && x.AdminStatus == "Archived")
                .FirstOrDefault();

            if (record != null)
            {
                record.AdminStatus = "Active";
                db.SaveChanges();
            }
        }
    }
}
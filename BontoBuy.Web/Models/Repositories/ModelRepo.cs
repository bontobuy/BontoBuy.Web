using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public void ExportToExcel(List<ModelAdminRetrieveViewModel> records)
        {
            var dataTable = ConvertToDatatable(records);
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            ExcelConversion(dataSet);
        }

        private void ExcelConversion(DataSet dataSet)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + DateTime.UtcNow.ToString("F") + "\"" + ".xls");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = dataSet.Tables[0];
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

        private DataTable ConvertToDatatable(List<ModelAdminRetrieveViewModel> records)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ModelAdminRetrieveViewModel));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in records)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
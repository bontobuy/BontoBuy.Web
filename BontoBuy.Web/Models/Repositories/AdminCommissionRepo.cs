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

        public IEnumerable<OrderViewModel> RetrieveDeliveredOrders()
        {
            var records = db.Orders.Where(x => x.Status == "Delivered" &&
                x.CommissionPaid == false).ToList();

            return records;
        }

        public IEnumerable<OrderViewModel> RetrievePaidOrders()
        {
            var records = db.Orders.Where(x => x.Status == "Delivered" &&
                x.CommissionPaid == true).ToList();

            return records;
        }

        public OrderViewModel UpdateCommissionOwnedFromOrders(int id)
        {
            if (id < 1)
                return null;

            var recordToUpdate = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (recordToUpdate.CustomerId < 1)
                return null;

            return recordToUpdate;
        }

        public void ExportToExcel(List<CommissionOrderViewModel> records)
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

        private DataTable ConvertToDatatable(List<CommissionOrderViewModel> records)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(CommissionOrderViewModel));
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
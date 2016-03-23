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
    public class AdminOrderRepo : IAdminOrderRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<AdminRetrieveOrdersViewModel> AdminRetrieveOrders()
        {
            var records = (from o in db.Orders
                           select o).ToList();

            if (records == null)
                return null;

            var itemList = AssignOrderList(records);

            return itemList;
        }

        private IEnumerable<AdminRetrieveOrdersViewModel> AssignOrderList(List<OrderViewModel> records)
        {
            if (records == null)
                return null;

            var itemList = new List<AdminRetrieveOrdersViewModel>();

            foreach (var item in records)
            {
                int commission = (from c in db.Commissions
                                  join mc in db.ModelCommissions on c.CommissionId equals mc.CommissionId
                                  join m in db.Models on mc.ModelId equals m.ModelId
                                  join o in db.Orders on m.ModelId equals o.ModelId
                                  where o.OrderId == item.OrderId
                                  select c.Percentage).FirstOrDefault();

                int unitPrice = (from o in db.Orders
                                 where o.OrderId == item.OrderId
                                 select o.UnitPrice).FirstOrDefault();

                var orderItem = new AdminRetrieveOrdersViewModel()
                {
                    OrderId = item.OrderId,
                    ModelId = item.ModelId,
                    CustomerId = item.CustomerId,
                    CustomerName = (from u in db.Users
                                    join o in db.Orders on u.Id equals o.CustomerUserId
                                    where o.OrderId == item.OrderId
                                    select u.Name).FirstOrDefault(),

                    ModelNumber = (from m in db.Models
                                   where m.ModelId == item.ModelId
                                   select m.ModelNumber).FirstOrDefault(),
                    SupplierName = (from c in db.Suppliers
                                    where c.SupplierId == item.SupplierId
                                    select c.Name).FirstOrDefault(),
                    Status = item.Status,
                    DtCreated = item.DtCreated,
                    SupplierId = item.SupplierId,
                    Price = unitPrice,
                    SupplierCommission = Convert.ToInt32((commission / 100) * unitPrice)
                };
                itemList.Add(orderItem);
            }
            if (itemList == null)
                return null;

            return itemList;
        }

        public void ExportToExcel()
        {
            var dataTable = ConvertToDatatable();
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

        private DataTable ConvertToDatatable()
        {
            var records = AdminRetrieveOrders();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(AdminRetrieveOrdersViewModel));
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
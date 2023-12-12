using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_Export
{
    public partial class ucExport : UserControl
    {
        public DataTable dtItems;
        private DataTable dataItemAdd;
        private DataTable dataExportItems;
        private DataTable dataExportItemDetail;

        private DataTable dataWarehouseExport;

        public ucExport()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            cboWareHouse.DataSource = await Warehouse_DAO.GetLocationWareHouses();
            cboWareHouse.DisplayMember = "NAME";
            cboWareHouse.ValueMember = "ID";
            if (cboWareHouse.Items.Count > 0)
            {
                cboWareHouse.SelectedIndex = 0;
                //dtItems = Item_DAO.GetItems();
                dtItems = await Item_DAO.GetItemByWarehouseIdAsync(Guid.Parse(cboWareHouse.SelectedValue.ToString()));
                grdItems.DataSource = dtItems;
                grdItems.RowTemplate.Height = 100;
            }

            dataItemAdd = new DataTable();
            dataItemAdd.Columns.Add("ID");
            dataItemAdd.Columns.Add("CODE");
            dataItemAdd.Columns.Add("NAME");
            dataItemAdd.Columns.Add("IMAGE", typeof(byte[]));
            dataItemAdd.Columns.Add("QUANTITY");
            dataItemAdd.Columns.Add("NOTE");

            grdItemAdds.RowTemplate.Height = 100;

            dataExportItems = await ExportItem_DAO.GetExportItems();
            grdItemEmports.DataSource = dataExportItems;
            dataExportItemDetail = await ExportItemDetail_DAO.GetEmportItemDetails();
            grdEmportItemDetails.RowTemplate.Height = 100;
            if (grdItemEmports.Rows.Count > 0 && dataExportItemDetail.Rows.Count > 0 && dataExportItems.Rows.Count > 0)
            {
                DataView dv = dataExportItemDetail.DefaultView;
                dv.RowFilter = $"EXPORT_ITEM_ID = '{Guid.Parse(grdItemEmports.Rows[0].Cells[0].Value.ToString())}'";
                grdEmportItemDetails.DataSource = dv.ToTable();
            }

            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PXK-{date}-{await ExportItem_DAO.GetCurrentBillNoInDate(date)}";
            txtBillNo.Text = convertString;

            dataWarehouseExport = new DataTable();
            dataWarehouseExport.Columns.Add("WAREHOUSE_ID");
            dataWarehouseExport.Columns.Add("ITEM_ID");
            dataWarehouseExport.Columns.Add("QUANTITY");
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (grdItemAdds.Rows.Count <= 0 || dataItemAdd.Rows.Count <= 0) return;
            Int64 sumQty = 0;
            foreach (DataRow item in dataItemAdd.Rows)
            {
                sumQty += Convert.ToInt64(item["QUANTITY"].ToString().Replace(",", ""));
            }

            ExportItemDto exportItemDto = new ExportItemDto()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                BillNo = txtBillNo.Text,
                SumQuantity = sumQty,
            };

            if (await ExportItem_DAO.Add(exportItemDto))
            {
                List<ExportItemDetail> lstEmport = new List<ExportItemDetail>();
                foreach (DataRow item in dataItemAdd.Rows)
                {
                    ExportItemDetail emportDetailDto = new ExportItemDetail()
                    {
                        ExportItemId = exportItemDto.Id,
                        ItemId = Guid.Parse(item["ID"].ToString()),
                        Qty = Convert.ToInt64(item["QUANTITY"].ToString()),
                        Note = item["NOTE"].ToString(),
                    };
                    lstEmport.Add(emportDetailDto);
                }

                if (await ExportItemDetail_DAO.AddRange(lstEmport))
                {
                    List<WareHouse_DetailDto> lstWarehouseDetails = new List<WareHouse_DetailDto>();
                    foreach (DataRow item in dataWarehouseExport.Rows)
                    {
                        WareHouse_DetailDto wareHouse_DetailDto = new WareHouse_DetailDto()
                        {
                            WarehouseId = Guid.Parse(item["WAREHOUSE_ID"].ToString()),
                            ItemId = Guid.Parse(item["ITEM_ID"].ToString()),
                            Quantity = Convert.ToInt64(item["QUANTITY"].ToString().Replace(",", "")),
                            Month = txtCreateDate.Value.Month,
                            Year = txtCreateDate.Value.Year,
                        };
                        lstWarehouseDetails.Add(wareHouse_DetailDto);
                    }

                    if (await WarehouseDetail_DAO.UpdateQuantityItemAtWarehouse(lstWarehouseDetails))
                    {
                        dataItemAdd.Rows.Clear();
                        KryptonMessageBox.Show("Created successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (grdItems.Rows.Count <= 0) return;
            if (txtQty.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter quantity !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Focus();
                return;
            }

            int rsl = grdItems.CurrentRow.Index;
            if (grdItems.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                if (!dataItemAdd.AsEnumerable().Any(row => (grdItems.Rows[rsl].Cells[1].Value.ToString()) == row.Field<string>("ID")))
                {
                    var qty = dtItems.AsEnumerable()
                    .Where(row => row.Field<Guid>("ITEM_ID") == Guid.Parse(grdItems.Rows[rsl].Cells[1].Value.ToString()))
                    .Select(row => row.Field<Int64>("QUANTITY"));

                    if (int.Parse(qty.FirstOrDefault().ToString().Replace(",", "")) <= int.Parse(txtQty.Text.Replace(",", "")))
                    {
                        KryptonMessageBox.Show($"Only {qty.FirstOrDefault().ToString().Replace(",", "")} products left in stock {cboWareHouse.Text.ToUpper()}.\nPlease import more products or reduce the number of products export.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtQty.Focus();
                        return;
                    }

                    DataRow rowWarehouse = dataWarehouseExport.NewRow();
                    rowWarehouse["WAREHOUSE_ID"] = grdItems.Rows[rsl].Cells[0].Value.ToString();
                    rowWarehouse["ITEM_ID"] = grdItems.Rows[rsl].Cells[1].Value.ToString();
                    rowWarehouse["QUANTITY"] = /*int.Parse(qty.FirstOrDefault().ToString()) -*/ int.Parse(txtQty.Text.Replace(",", ""));
                    dataWarehouseExport.Rows.Add(rowWarehouse);

                    DataRow r = dataItemAdd.NewRow();
                    r["ID"] = grdItems.Rows[rsl].Cells[1].Value.ToString();
                    r["CODE"] = grdItems.Rows[rsl].Cells[2].Value.ToString();
                    r["NAME"] = grdItems.Rows[rsl].Cells[3].Value.ToString();
                    r["IMAGE"] = (byte[])grdItems.Rows[rsl].Cells[4].Value;
                    r["QUANTITY"] = txtQty.Text.Replace(",", "");
                    r["NOTE"] = txtNote.Text;

                    dataItemAdd.Rows.Add(r);
                    grdItemAdds.DataSource = dataItemAdd;
                    txtQty.Text = "";
                    txtNote.Text = "";
                }
                else
                {
                    KryptonMessageBox.Show("The item has been added to the export list !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (grdItemAdds.Rows.Count <= 0) return;
            int rsl = grdItemAdds.CurrentRow.Index;
            if (grdItemAdds.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataRow[] drr = dataItemAdd.Select($"ID='{grdItemAdds.Rows[rsl].Cells[0].Value}'");
                for (int i = 0; i < drr.Length; i++)
                {
                    drr[i].Delete();
                }
                dataItemAdd.AcceptChanges();
                grdItemAdds.DataSource = dataItemAdd;
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (grdItemAdds.Rows.Count <= 0) return;
            dataItemAdd.Rows.Clear();
            grdItemAdds.DataSource = dataItemAdd;
        }

        private void grdItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdItems.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                {
                    grdItems.Rows[e.RowIndex].Cells[4].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void grdItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private async void txtCreateDate_ValueChanged(object sender, EventArgs e)
        {
            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PXK-{date}-{await ExportItem_DAO.GetCurrentBillNoInDate(date)}";
            txtBillNo.Text = convertString;
        }

        private void grdItemEmports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdItemEmports.Rows.Count <= 0) return;
            int rsl = grdItemEmports.CurrentRow.Index;
            if (grdItemEmports.Rows.Count > 0)
            {
                DataView dv = dataExportItemDetail.DefaultView;
                dv.RowFilter = $"EXPORT_ITEM_ID = '{Guid.Parse(grdItemEmports.Rows[rsl].Cells[0].Value.ToString())}'";
                grdEmportItemDetails.DataSource = dv.ToTable();
            }
        }

        private void grdEmportItemDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdEmportItemDetails.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                {
                    grdEmportItemDetails.Rows[e.RowIndex].Cells[4].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtSearchExports_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearchExports.Text.Length == 0)
            {
                grdItemEmports.DataSource = dataExportItems;
            }
            DataView dv = dataExportItems.DefaultView;
            dv.RowFilter = $"BILL_NO LIKE '%{txtSearchExports.Text}%' ";
            grdItemEmports.DataSource = dv.ToTable();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdItems.DataSource = dtItems;
            }
            DataView dv = dtItems.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' ";
            grdItems.DataSource = dv.ToTable();
        }

        private async void cboWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var id = Guid.Parse(cboWareHouse.SelectedValue.ToString());
                dtItems = await Item_DAO.GetItemByWarehouseIdAsync(id);
                grdItems.DataSource = dtItems;
            }
            catch (Exception)
            {

            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iKeep = txtQty.SelectionStart - 1;
                for (int i = iKeep; i > 0; i--)
                    if (txtQty.Text[i] == ',')
                        iKeep -= 1;

                txtQty.Text = String.Format("{0:N0}", Convert.ToInt64(txtQty.Text.Replace(",", "")));
                for (int i = 0; i < iKeep; i++)
                    if (txtQty.Text[i] == ',')
                        iKeep += 1;

                txtQty.SelectionStart = iKeep + 1;
            }
            catch
            {
                //errorhandling
            }
        }

        private void grdItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 & e.RowIndex >= 0)
            {
                if (grdItems.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdItemAdds_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 & e.RowIndex >= 0)
            {
                if (grdItemAdds.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdItemEmports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 & e.RowIndex >= 0)
            {
                if (grdItemEmports.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdEmportItemDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 & e.RowIndex >= 0)
            {
                if (grdEmportItemDetails.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }
    }
}

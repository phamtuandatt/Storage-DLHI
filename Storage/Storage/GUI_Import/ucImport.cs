using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Storage.GUI_Import
{
    public partial class ucImport : UserControl
    {
        private DataTable dataItemAddPO;
        private DataTable dataItemAdd;
        private DataTable dataImportItems;
        private DataTable dataImportItemDetails;

        public ucImport()
        {
            InitializeComponent();

            LoadData();
        }

        private async void LoadData()
        {
            dataItemAddPO = await Item_DAO.GetItemsAsync();
            grdItems.DataSource = dataItemAddPO;
            grdItems.RowTemplate.Height = 100;

            cboSupplier.DataSource = await SupplierDAO.GetSuppiers();
            cboSupplier.DisplayMember = "NameSuppier";
            cboSupplier.ValueMember = "ID";

            cboWareHouse.DataSource = await Warehouse_DAO.GetLocationWareHouses();
            cboWareHouse.DisplayMember = "NAME";
            cboWareHouse.ValueMember = "ID";

            dataItemAdd = new DataTable();
            dataItemAdd.Columns.Add("ID");
            dataItemAdd.Columns.Add("CODE");
            dataItemAdd.Columns.Add("NAME");
            dataItemAdd.Columns.Add("IMAGE", typeof(byte[]));
            dataItemAdd.Columns.Add("QUANTITY");
            dataItemAdd.Columns.Add("PRICE");
            dataItemAdd.Columns.Add("NOTE");

            grdImportDetail.RowTemplate.Height = 100;

            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PNK-{date}-{ImportItem_DAO.GetCurrentBillNoInDate(date)}";
            txtBillNo.Text = convertString;

            dataImportItems = ImportItem_DAO.GetImportItems();
            grdItemImports.DataSource = dataImportItems;
            dataImportItemDetails = ImportItemDetailDAO.GetImportItemDetails();
            grdImportItemDetails.RowTemplate.Height = 100;
            if (grdItemImports.Rows.Count > 0)
            {
                DataView dv = dataImportItemDetails.DefaultView;
                dv.RowFilter = $"IMPORT_ITEM_ID = '{Guid.Parse(grdItemImports.Rows[0].Cells[0].Value.ToString())}'";
                grdImportItemDetails.DataSource = dv.ToTable();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (cboWareHouse.Items.Count <= 0)
            {
                KryptonMessageBox.Show("Please Choose a Warehouse or Add Warehouse to Import Items !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboSupplier.Items.Count <= 0)
            {
                KryptonMessageBox.Show("Please Choose a supplier or Add supplier to Import Items !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataItemAdd.Rows.Count <= 0 || grdImportDetail.Rows.Count <= 0) return;
            Int64 sumQty = 0;
            Int64 total = 0;
            foreach (DataRow item in dataItemAdd.Rows)
            {
                sumQty += Convert.ToInt64(item["QUANTITY"].ToString().Replace(",", ""));
                total += Convert.ToInt64(item["QUANTITY"].ToString().Replace(",", "")) * Convert.ToInt64(item["PRICE"].ToString().Replace(",", ""));
            }

            ImportItemDto importItemDto = new ImportItemDto()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                Bill_No = txtBillNo.Text,
                SumQuantity = sumQty,
                SumPrice = 0,
                Total = total,
            };

            if (ImportItem_DAO.Add(importItemDto))
            {
                List<ImportItemDetailDto> lstImport = new List<ImportItemDetailDto>();
                List<WareHouse_DetailDto> lstWareHouseDetail = new List<WareHouse_DetailDto>();
                foreach (DataRow item in dataItemAdd.Rows)
                {
                    ImportItemDetailDto importDetailDto = new ImportItemDetailDto()
                    {
                        ImportItemId = importItemDto.Id,
                        ItemId = Guid.Parse(item["ID"].ToString()),
                        Qty = Convert.ToInt64(item["QUANTITY"].ToString().Replace(",", "")),
                        Price = Convert.ToInt64(item["PRICE"].ToString().Replace(",", "")),
                        Note = item["NOTE"].ToString(),
                    };
                    lstImport.Add(importDetailDto);
                    WareHouse_DetailDto wareHouse_DetailDto = new WareHouse_DetailDto()
                    {
                        WarehouseId = Guid.Parse(cboWareHouse.SelectedValue.ToString()),
                        ItemId = Guid.Parse(item["ID"].ToString()),
                        Quantity = int.Parse(item["QUANTITY"].ToString().Replace(",", "")),
                        Month = txtCreateDate.Value.Month,
                        Year = txtCreateDate.Value.Year,
                    };
                    lstWareHouseDetail.Add(wareHouse_DetailDto);
                }

                if (ImportItemDetailDAO.AddRange(lstImport) && await WarehouseDetail_DAO.UpdateItemAtWarehouse(lstWareHouseDetail))
                {
                    dataItemAdd.Rows.Clear();
                    KryptonMessageBox.Show("Created successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (grdItems.Rows.Count <= 0) return;
            if (txtQty.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter quantity !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPrice.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter price !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rsl = grdItems.CurrentRow.Index;
            if (grdItems.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                if (dataItemAdd.AsEnumerable().Any(row => (grdItems.Rows[rsl].Cells[1].Value.ToString()) == row.Field<string>("ID")))
                {

                }
                else
                {
                    DataRow r = dataItemAdd.NewRow();
                    r["ID"] = grdItems.Rows[rsl].Cells[1].Value.ToString();
                    r["CODE"] = grdItems.Rows[rsl].Cells[2].Value.ToString();
                    r["NAME"] = grdItems.Rows[rsl].Cells[3].Value.ToString();
                    r["IMAGE"] = (byte[])grdItems.Rows[rsl].Cells[5].Value;
                    r["QUANTITY"] = txtQty.Text;
                    r["PRICE"] = txtPrice.Text.Replace(",", "");
                    r["NOTE"] = txtNote.Text;

                    dataItemAdd.Rows.Add(r);
                    grdImportDetail.DataSource = dataItemAdd;
                    txtQty.Text = "";
                    txtPrice.Text = "";
                    txtNote.Text = "";
                }
            }
            else
            {

            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (grdImportDetail.Rows.Count <= 0) return;
            int rsl = grdImportDetail.CurrentRow.Index;
            if (grdImportDetail.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataRow[] drr = dataItemAdd.Select($"ID='{grdImportDetail.Rows[rsl].Cells[0].Value}'");
                for (int i = 0; i < drr.Length; i++)
                {
                    drr[i].Delete();
                }
                dataItemAdd.AcceptChanges();
                grdImportDetail.DataSource = dataItemAdd;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdItems.DataSource = dataItemAddPO;
            }
            DataView dv = dataItemAddPO.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdItems.DataSource = dv.ToTable();
        }

        private void grdItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdItems.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdItems.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtCreateDate_ValueChanged(object sender, EventArgs e)
        {
            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PNK-{date}-{ImportItem_DAO.GetCurrentBillNoInDate(date)}";
            txtBillNo.Text = convertString;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void grdImportDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdImportDetail.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                {
                    grdImportDetail.Rows[e.RowIndex].Cells[3].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (grdImportDetail.Rows.Count <= 0) return;
            dataItemAdd.Rows.Clear();
            grdImportDetail.DataSource = dataItemAdd;
        }

        private void grdItemImports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdItemImports.Rows.Count <= 0) return;
            int rsl = grdItemImports.CurrentRow.Index;
            if (grdItemImports.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataView dv = dataImportItemDetails.DefaultView;
                dv.RowFilter = $"IMPORT_ITEM_ID = '{Guid.Parse(grdItemImports.Rows[rsl].Cells[0].Value.ToString())}'";
                grdImportItemDetails.DataSource = dv.ToTable();
            }
            else
            {

            }
        }

        private void grdItemImports_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void grdImportItemDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdImportItemDetails.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                {
                    grdImportItemDetails.Rows[e.RowIndex].Cells[4].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void grdItemImports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 & e.RowIndex >= 0)
            {
                if (grdItemImports.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
            if (e.ColumnIndex == 3 & e.RowIndex >= 0)
            {
                if (grdItemImports.Rows[e.RowIndex].Cells[3].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdImportItemDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 & e.RowIndex >= 0)
            {
                if (grdImportItemDetails.Rows[e.RowIndex].Cells[6].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
            if (e.ColumnIndex == 7 & e.RowIndex >= 0)
            {
                if (grdImportItemDetails.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdImportDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 & e.RowIndex >= 0)
            {
                if (grdImportDetail.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void txtSearchImports_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearchImports.Text.Length == 0)
            {
                grdItemImports.DataSource = dataImportItems;
            }
            DataView dv = dataImportItems.DefaultView;
            dv.RowFilter = $"BILL_NO LIKE '%{txtSearchImports.Text}%' ";
            grdItemImports.DataSource = dv.ToTable();
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            //int val = int.Parse(txtPrice.Text.ToString());
            //txtPrice.Text = txtPrice.Text.ToString("N0");
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iKeep = txtPrice.SelectionStart - 1;
                for (int i = iKeep; i > 0; i--)
                    if (txtPrice.Text[i] == ',')
                        iKeep -= 1;

                txtPrice.Text = String.Format("{0:N0}", Convert.ToInt64(txtPrice.Text.Replace(",", "")));
                for (int i = 0; i < iKeep; i++)
                    if (txtPrice.Text[i] == ',')
                        iKeep += 1;

                txtPrice.SelectionStart = iKeep + 1;
            }
            catch
            {
                //errorhandling
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
    }
}

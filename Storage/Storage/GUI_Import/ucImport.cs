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
        private DataTable dataImportItemDetails;

        public ucImport()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dataItemAddPO = Item_DAO.GetItems();
            grdItems.DataSource = dataItemAddPO;
            grdItems.RowTemplate.Height = 100;

            cboSupplier.DataSource = SupplierDAO.GetSuppiers();
            cboSupplier.DisplayMember = "NAME_SUPPIER";
            cboSupplier.ValueMember = "ID";

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

            grdItemImports.DataSource = ImportItem_DAO.GetImportItems();
            dataImportItemDetails = ImportItemDetailDAO.GetImportItemDetails();
            grdImportItemDetails.RowTemplate.Height = 100;
            if (grdItemImports.Rows.Count > 0)
            {
                DataView dv = dataImportItemDetails.DefaultView;
                dv.RowFilter = $"IMPORT_ITEM_ID = '{Guid.Parse(grdItemImports.Rows[0].Cells[0].Value.ToString())}'";
                grdImportItemDetails.DataSource = dv.ToTable();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int sumQty = 0;
            int total = 0;
            foreach (DataRow item in dataItemAdd.Rows)
            {
                sumQty += int.Parse(item["QUANTITY"].ToString());
                total += int.Parse(item["QUANTITY"].ToString()) * int.Parse(item["PRICE"].ToString());
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
                foreach (DataRow item in dataItemAdd.Rows)
                {
                    ImportItemDetailDto importDetailDto = new ImportItemDetailDto()
                    {
                        ImportItemId = importItemDto.Id,
                        ItemId = Guid.Parse(item["ID"].ToString()),
                        Qty = int.Parse(item["QUANTITY"].ToString()),
                        Price = int.Parse(item["PRICE"].ToString()),
                        Note = item["NOTE"].ToString(),
                    };
                    lstImport.Add(importDetailDto);
                }

                if (ImportItemDetailDAO.AddRange(lstImport))
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
            if (txtQty.Text.Length == 0) return;
            if (txtPrice.Text.Length == 0) return;

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
                    r["PRICE"] = txtPrice.Text;
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
                    int val = int.Parse(e.Value.ToString());
                    e.Value = val.ToString("N0");
                }
            }
        }

        private void grdImportItemDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 & e.RowIndex >= 0)
            {
                if (grdImportItemDetails.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    int val = Int32.Parse(e.Value.ToString());
                    e.Value = val.ToString("N0");
                }
            }
            if (e.ColumnIndex == 7 & e.RowIndex >= 0)
            {
                if (grdImportItemDetails.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    int val = Int32.Parse(e.Value.ToString());
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
                    int val = Int32.Parse(e.Value.ToString());
                    e.Value = val.ToString("N0");
                }
            }
        }
    }
}

﻿using ComponentFactory.Krypton.Toolkit;
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

        public ucExport()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dtItems = Item_DAO.GetItems();
            grdItems.DataSource = dtItems;
            grdItems.RowTemplate.Height = 100;

            dataItemAdd = new DataTable();
            dataItemAdd.Columns.Add("ID");
            dataItemAdd.Columns.Add("CODE");
            dataItemAdd.Columns.Add("NAME");
            dataItemAdd.Columns.Add("IMAGE", typeof(byte[]));
            dataItemAdd.Columns.Add("QUANTITY");
            dataItemAdd.Columns.Add("NOTE");

            grdItemAdds.RowTemplate.Height = 100;

            dataExportItems = ExportItem_DAO.GetExportItems();
            grdItemEmports.DataSource = dataExportItems;
            dataExportItemDetail = ExportItemDetail_DAO.GetEmportItemDetails();
            grdEmportItemDetails.RowTemplate.Height = 100;
            if (grdItemEmports.Rows.Count > 0)
            {
                DataView dv = dataExportItemDetail.DefaultView;
                dv.RowFilter = $"EXPORT_ITEM_ID = '{Guid.Parse(grdItemEmports.Rows[0].Cells[0].Value.ToString())}'";
                grdEmportItemDetails.DataSource = dv.ToTable();
            }

            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PXK-{date}-{ExportItem_DAO.GetCurrentBillNoInDate(date)}";
            txtBillNo.Text = convertString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int sumQty = 0;
            foreach (DataRow item in dataItemAdd.Rows)
            {
                sumQty += int.Parse(item["QUANTITY"].ToString());
            }

            ExportItemDto exportItemDto = new ExportItemDto()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                Bill_No = txtBillNo.Text,
                Sum_Quantity = sumQty,
            };

            if (ExportItem_DAO.Add(exportItemDto))
            {
                List<ExportItemDetail> lstEmport = new List<ExportItemDetail>();
                foreach (DataRow item in dataItemAdd.Rows)
                {
                    ExportItemDetail emportDetailDto = new ExportItemDetail()
                    {
                        ExportItemId = exportItemDto.Id,
                        ItemId = Guid.Parse(item["ID"].ToString()),
                        Qty = int.Parse(item["QUANTITY"].ToString()),
                        Note = item["NOTE"].ToString(),
                    };
                    lstEmport.Add(emportDetailDto);
                }

                if (ExportItemDetail_DAO.AddRange(lstEmport))
                {
                    dataItemAdd.Rows.Clear();
                    KryptonMessageBox.Show("Created successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (grdItems.Rows.Count <= 0) return;
            //if (txtQty.Text.Length == 0) return;

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
                    r["NOTE"] = txtNote.Text;

                    dataItemAdd.Rows.Add(r);
                    grdItemAdds.DataSource = dataItemAdd;
                    txtQty.Text = "";
                    txtNote.Text = "";
                }
            }
            else
            {

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

        private void txtCreateDate_ValueChanged(object sender, EventArgs e)
        {
            var date = txtCreateDate.Value.ToString("dd-MM-yyyy").Replace("-", "");
            var convertString = $"PXK-{date}-{ExportItem_DAO.GetCurrentBillNoInDate(date)}";
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
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdItems.DataSource = dv.ToTable();
        }
    }
}
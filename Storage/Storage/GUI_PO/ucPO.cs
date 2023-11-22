using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using Storage.GUI.LocationWarehouses;
using Storage.GUI.PaymentMethods;
using Storage.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_PO
{
    public partial class ucPO : UserControl
    {
        private DataTable dataItemAddPO;
        private DataTable dtItemPO;

        private DataTable dtPOs;
        private DataTable dtPODetail;

        public ucPO()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dataItemAddPO = Item_DAO.GetItems();
            grdItemAddPO.DataSource = dataItemAddPO;
            grdItemAddPO.RowTemplate.Height = 100;

            grdItemPODetail.RowTemplate.Height = 100;

            dtItemPO = new DataTable();
            dtItemPO.Columns.Add("ID");
            dtItemPO.Columns.Add("CODE");
            dtItemPO.Columns.Add("NAME");
            dtItemPO.Columns.Add("IMAGE", typeof(byte[]));
            dtItemPO.Columns.Add("QUANTITY");
            dtItemPO.Columns.Add("PRICE");
            dtItemPO.Columns.Add("MPRNO");
            dtItemPO.Columns.Add("PONO");

            cboPaymentMethod.DataSource = PaymentMethod_DAO.GetPaymentMethods();
            cboPaymentMethod.DisplayMember = "Name";
            cboPaymentMethod.ValueMember = "ID";

            cboWarehouse.DataSource = Warehouse_DAO.GetLocationWareHouses();
            cboWarehouse.DisplayMember = "Name";
            cboWarehouse.ValueMember = "ID";

            dtPOs = PO_DAO.GetPOs();
            dtPODetail = PO_Detail_DAO.GetPODetails();
            grdPOs.DataSource = dtPOs;
            grdPODetail.RowTemplate.Height = 100;
            if (grdPOs.Rows.Count > 0)
            {
                grdPOs.Rows[0].Selected = true;
                DataView dv = dtPODetail.DefaultView;
                dv.RowFilter = $"PO_ID = '{Guid.Parse(grdPOs.Rows[0].Cells[0].Value.ToString())}'";
                grdPODetail.DataSource = dv.ToTable();
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

        private void grdItemAddPO_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdItemAddPO.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdItemAddPO.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdItemAddPO.DataSource = dataItemAddPO;
            }
            DataView dv = dataItemAddPO.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdItemAddPO.DataSource = dv.ToTable();
        }

        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            if (grdItemAddPO.Rows.Count <= 0) return;
            if (txtMPR.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter MPR No !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMPR.Focus();
                return;
            }
            if (txtPONo.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter PO No!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPONo.Focus();
                return;
            }
            if (txtQuantity.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter quantity !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }
            if (txtPrice.Text.Length == 0)
            {
                KryptonMessageBox.Show("Please enter price !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            int rsl = grdItemAddPO.CurrentRow.Index;
            if (grdItemAddPO.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                if (dtItemPO.AsEnumerable().Any(row => (grdItemAddPO.Rows[rsl].Cells[1].Value.ToString()) == row.Field<string>("ID")))
                {
                    KryptonMessageBox.Show("The item has been added to the order list !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataRow r = dtItemPO.NewRow();
                    r["ID"] = grdItemAddPO.Rows[rsl].Cells[1].Value.ToString();
                    r["CODE"] = grdItemAddPO.Rows[rsl].Cells[2].Value.ToString();
                    r["NAME"] = grdItemAddPO.Rows[rsl].Cells[3].Value.ToString();
                    r["IMAGE"] = (byte[])grdItemAddPO.Rows[rsl].Cells[5].Value;
                    r["QUANTITY"] = txtQuantity.Text;
                    r["PRICE"] = txtPrice.Text;
                    r["MPRNO"] = txtMPR.Text;
                    r["PONO"] = txtPONo.Text;

                    dtItemPO.Rows.Add(r);
                    grdItemPODetail.DataSource = dtItemPO;
                    txtQuantity.Text = "";
                    txtPrice.Text = "";
                    txtMPR.Text = "";
                    txtPONo.Text = "";
                }
            }
            else
            {

            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Add_PaymentMethod paymentMethod = new Add_PaymentMethod();
            paymentMethod.ShowDialog();
            cboPaymentMethod.DataSource = PaymentMethod_DAO.GetPaymentMethods();
            cboPaymentMethod.DisplayMember = "Name";
            cboPaymentMethod.ValueMember = "ID";
        }

        private void btnLocationWarehouse_Click(object sender, EventArgs e)
        {
            Add_LocationWareHouse add_LocationWareHouse = new Add_LocationWareHouse();
            add_LocationWareHouse.ShowDialog();

            cboWarehouse.DataSource = Warehouse_DAO.GetLocationWareHouses();
            cboWarehouse.DisplayMember = "Name";
            cboWarehouse.ValueMember = "ID";
        }

        private void btnRemoveSingle_Click(object sender, EventArgs e)
        {
            if (grdItemPODetail.Rows.Count <= 0) return;
            int rsl = grdItemPODetail.CurrentRow.Index;
            if (grdItemPODetail.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataRow[] drr = dtItemPO.Select($"ID='{grdItemPODetail.Rows[rsl].Cells[0].Value}'");
                for (int i = 0; i < drr.Length; i++)
                {
                    drr[i].Delete();
                }
                dtItemPO.AcceptChanges();
                grdItemPODetail.DataSource = dtItemPO;
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (grdItemPODetail.Rows.Count <= 0) return;
            dtItemPO.Rows.Clear();
            grdItemPODetail.DataSource = dtItemPO;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdItemPODetail.Rows.Count <= 0) return;

            if (cboPaymentMethod.Items.Count <= 0)
            {
                KryptonMessageBox.Show("Please Choose a Payment term or Add Payment term to Make Purchase Order !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboWarehouse.Items.Count <= 0)
            {
                KryptonMessageBox.Show("Please Choose a Warehosue or Add Warehouse to Make Purchase Order !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PODto pODto = new PODto()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                ExpectedDelivery = DateTime.Parse(txtExpected.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                Total = grdItemPODetail.Rows.Count,
                LocationWareHouse_Id = Guid.Parse(cboWarehouse.SelectedValue.ToString()),
                PaymentMethod_Id = Guid.Parse(cboPaymentMethod.SelectedValue.ToString()),
            };

            if (PO_DAO.Add(pODto))
            {
                List<PO_DetailDto> dtos = new List<PO_DetailDto>();
                foreach (DataGridViewRow item in grdItemPODetail.Rows)
                {
                    PO_DetailDto pO_DetailDto = new PO_DetailDto()
                    {
                        PO_Id = pODto.Id,
                        Item_Id = Guid.Parse(item.Cells[0].Value.ToString()),
                        MPR_No = item.Cells[6].Value.ToString(),
                        PO_No = item.Cells[7].Value.ToString(),
                        Price = Convert.ToInt64(item.Cells[5].Value.ToString().Replace(",", "")),
                        Quantity = int.Parse(item.Cells[4].Value.ToString().Replace(",", "")),
                    };
                    dtos.Add(pO_DetailDto);
                }
                if (PO_Detail_DAO.AddRange(dtos))
                {
                    dtItemPO.Rows.Clear();
                    grdItemPODetail.DataSource = dtItemPO;
                    txtQuantity.Text = "";
                    txtPrice.Text = "";
                    txtMPR.Text = "";
                    txtPONo.Text = "";
                    KryptonMessageBox.Show("Created successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grdPOs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPOs.Rows.Count <= 0) return;
            int rsl = grdPOs.CurrentRow.Index;
            if (grdPOs.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataView dv = dtPODetail.DefaultView;
                dv.RowFilter = $"PO_ID = '{Guid.Parse(grdPOs.Rows[rsl].Cells[0].Value.ToString())}'";
                grdPODetail.DataSource = dv.ToTable();

                //grdMPRExportDetail.DataSource = MPR_DAO.GetMPRExportDetail(Guid.Parse(grdMPRExport.Rows[rsl].Cells[0].Value.ToString()));
            }
            else
            {

            }
        }

        private void grdPODetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdPODetail.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                {
                    grdPODetail.Rows[e.RowIndex].Cells[3].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtMPR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NO.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iKeep = txtQuantity.SelectionStart - 1;
                for (int i = iKeep; i > 0; i--)
                    if (txtQuantity.Text[i] == ',')
                        iKeep -= 1;

                txtQuantity.Text = String.Format("{0:N0}", Convert.ToInt64(txtQuantity.Text.Replace(",", "")));
                for (int i = 0; i < iKeep; i++)
                    if (txtQuantity.Text[i] == ',')
                        iKeep += 1;

                txtQuantity.SelectionStart = iKeep + 1;
            }
            catch
            {
                //errorhandling
            }
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

        private void grdPODetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 & e.RowIndex >= 0)
            {
                if (grdPODetail.Rows[e.RowIndex].Cells[6].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }

            if (e.ColumnIndex == 7 & e.RowIndex >= 0)
            {
                if (grdPODetail.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    Int64 val = Convert.ToInt64(e.Value.ToString().Replace(",", ""));
                    e.Value = val.ToString("N0");
                }
            }
        }
    }
}

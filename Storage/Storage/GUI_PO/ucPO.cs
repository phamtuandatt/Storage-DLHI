using ComponentFactory.Krypton.Toolkit;
using OfficeOpenXml.Drawing;
using OfficeOpenXml;
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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Validation = Storage.Helper.Validation;
using System.Threading;
using System.Windows.Media;

namespace Storage.GUI_PO
{
    public partial class ucPO : UserControl
    {
        private DataTable dataItemAddPO;
        private DataTable dtItemPO;

        private DataTable dtPOs;
        private DataTable dtPODetail;

        private DataTable dtExportPO;
        private DataTable dtExportPO_DB;

        public ucPO()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            dataItemAddPO = await Item_DAO.GetItemsAsync();
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

            cboWarehouse.DataSource = await Warehouse_DAO.GetLocationWareHouses();
            cboWarehouse.DisplayMember = "Name";
            cboWarehouse.ValueMember = "ID";

            dtPOs = await PO_DAO.GetPOs();
            dtPODetail = await PO_Detail_DAO.GetPODetails();
            grdPOs.DataSource = dtPOs;
            grdPODetail.RowTemplate.Height = 100;
            if (grdPOs.Rows.Count > 0)
            {
                grdPOs.Rows[0].Selected = true;
                DataView dv = dtPODetail.DefaultView;
                dv.RowFilter = $"PO_ID = '{Guid.Parse(grdPOs.Rows[0].Cells[0].Value.ToString())}'";
                grdPODetail.DataSource = dv.ToTable();
            }

            dtExportPO_DB = await PO_Detail_DAO.GetPOExport();
            dtExportPO = new DataTable();
            dtExportPO.Columns.Add("NO");
            dtExportPO.Columns.Add("CODE");
            dtExportPO.Columns.Add("NAME");
            dtExportPO.Columns.Add("PICTURE", typeof(byte[]));
            dtExportPO.Columns.Add("QTY");
            dtExportPO.Columns.Add("UNIT");
            dtExportPO.Columns.Add("WEIGHT");
            dtExportPO.Columns.Add("MPR_NO");
            dtExportPO.Columns.Add("REQ_DATE");
            dtExportPO.Columns.Add("COMPANY_NAME");
            dtExportPO.Columns.Add("PRICE");
            dtExportPO.Columns.Add("AMOUNT");
            dtExportPO.Columns.Add("RECEIVE");
            dtExportPO.Columns.Add("REAMARK");
            dtExportPO.Columns.Add("PICTURE_LINK");
            dtExportPO.Columns.Add("PO_NO");

            if (dtExportPO_DB.Rows.Count > 0)
            {
                int stt = 1;
                DataView dvExport = dtExportPO_DB.DefaultView;
                dvExport.RowFilter = $"PO_ID = '{Guid.Parse(grdPOs.Rows[0].Cells[0].Value.ToString())}'";
                dtExportPO.Rows.Clear();
                foreach (DataRow item in dvExport.ToTable().Rows)
                {
                    DataRow row = dtExportPO.NewRow();
                    row[0] = stt;
                    row[1] = item.Field<string>("CODE");
                    row[2] = item.Field<string>("NAME");
                    row[3] = item.Field<byte[]>("PICTURE");
                    row[4] = item.Field<Int64>("QUANTITY");
                    row[5] = item.Field<string>("UNIT");
                    row[6] = "";
                    row[7] = item.Field<string>("MPR_NO");
                    row[8] = grdPOs.Rows[0].Cells[2].Value.ToString();
                    row[9] = "";
                    row[10] = item.Field<Int64>("PRICE");
                    row[11] = "";
                    row[12] = "";
                    row[13] = "";
                    row[14] = item.Field<string>("PICTURE_LINK");
                    row[15] = item.Field<string>("PO_NO");

                    dtExportPO.Rows.Add(row);
                    stt++;
                }
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
                    txtMPR.ReadOnly = true;
                    txtPONo.ReadOnly = true;
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

        private async void btnLocationWarehouse_Click(object sender, EventArgs e)
        {
            Add_LocationWareHouse add_LocationWareHouse = new Add_LocationWareHouse();
            add_LocationWareHouse.ShowDialog();

            cboWarehouse.DataSource = await Warehouse_DAO.GetLocationWareHouses();
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
            txtMPR.Text = "";
            txtPONo.Text = "";
            txtMPR.ReadOnly = false;
            txtPONo.ReadOnly = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
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

            if (await  PO_DAO.Add(pODto))
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
                if (await PO_Detail_DAO.AddRange(dtos))
                {
                    dtItemPO.Rows.Clear();
                    grdItemPODetail.DataSource = dtItemPO;
                    txtQuantity.Text = "";
                    txtPrice.Text = "";
                    txtMPR.Text = "";
                    txtPONo.Text = "";
                    txtMPR.ReadOnly = false;
                    txtPONo.ReadOnly = false;
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


                int stt = 1;
                DataView dvExport = dtExportPO_DB.DefaultView;
                dvExport.RowFilter = $"PO_ID = '{Guid.Parse(grdPOs.Rows[rsl].Cells[0].Value.ToString())}'";
                dtExportPO.Rows.Clear();
                foreach (DataRow item in dvExport.ToTable().Rows)
                {
                    DataRow row = dtExportPO.NewRow();
                    row[0] = stt;
                    row[1] = item.Field<string>("CODE");
                    row[2] = item.Field<string>("NAME");
                    row[3] = item.Field<byte[]>("PICTURE");
                    row[4] = item.Field<Int64>("QUANTITY");
                    row[5] = item.Field<string>("UNIT");
                    row[6] = "";
                    row[7] = item.Field<string>("MPR_NO");
                    row[8] = grdPOs.Rows[rsl].Cells[2].Value.ToString();
                    row[9] = "";
                    row[10] = item.Field<Int64>("PRICE");
                    row[11] = "";
                    row[12] = "";
                    row[13] = "";
                    row[14] = item.Field<string>("PICTURE_LINK");
                    row[15] = item.Field<string>("PO_NO");

                    dtExportPO.Rows.Add(row);
                    stt++;
                }
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (grdPOs.Rows.Count > 0 && grdPODetail.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets.Add("PO");
                        sheet.Row(1).Height = 40;
                        sheet.Row(2).Height = 40;
                        sheet.Row(3).Height = 40;
                        sheet.Row(4).Height = 40;
                        sheet.Row(5).Height = 40;
                        sheet.Row(6).Height = 40;

                        sheet.Column(1).Width = 5;
                        sheet.Column(2).Width = 20;
                        sheet.Column(3).Width = 25;
                        sheet.Column(4).Width = 10;
                        sheet.Column(5).Width = 10;
                        sheet.Column(6).Width = 10;
                        sheet.Column(7).Width = 20;
                        sheet.Column(8).Width = 15;
                        sheet.Column(9).Width = 15;
                        sheet.Column(10).Width = 35;
                        sheet.Column(11).Width = 20;
                        sheet.Column(12).Width = 20;
                        sheet.Column(13).Width = 20;
                        sheet.Column(14).Width = 20;
                        sheet.Column(15).Width = 20;
                        sheet.Column(16).Width = 20;
                        sheet.Column(17).Width = 20;

                        var cellStyleRange = sheet.Cells["A1:Q6"].Style;
                        cellStyleRange.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cellStyleRange.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        sheet.Cells["A1:B1"].Merge = true;
                        sheet.Cells["A1"].Value = $"Project Name\n(공 사 명)";
                        sheet.Cells["A2:B2"].Merge = true;
                        sheet.Cells["A2"].Value = "Work Order No. \n(수주통보서 번호)";
                        sheet.Cells["A3:B4"].Merge = true;
                        sheet.Cells["A3"].Value = "M.P.R No. (요구서 번호)\n / P.O No. (발주 번호)";
                        sheet.Cells["A5:B5"].Merge = true;
                        sheet.Cells["A5"].Value = "Buyer \n (구 매 자)";
                        sheet.Cells["A6"].Value = "No. \n 순위";
                        sheet.Cells["B6"].Value = "Code";

                        sheet.Cells["C1:E1"].Merge = true;
                        sheet.Cells["C1"].Value = "";
                        sheet.Cells["C2:E2"].Merge = true;
                        sheet.Cells["C2"].Value = "";
                        sheet.Cells["C3:E3"].Merge = true;
                        sheet.Cells["C3"].Value = dtExportPO.Rows[0][7].ToString();
                        sheet.Cells["C4:E4"].Merge = true;
                        sheet.Cells["C4"].Value = dtExportPO.Rows[0][15].ToString();
                        sheet.Cells["C5:F5"].Merge = true;
                        sheet.Cells["C5"].Value = "";
                        sheet.Cells["C6"].Value = "Part Name 부 품 명";
                        sheet.Cells["D6:F6"].Merge = true;
                        sheet.Cells["D6"].Value = "Picture";

                        sheet.Cells["F1:K2"].Merge = true;
                        sheet.Cells["F1"].Value = "구  매  발  주  서 \n (Purchase Order)";
                        sheet.Cells["F3:K4"].Merge = true;
                        sheet.Cells["F3"].Value = "Công Ty TNHH DLHI Việt Nam \n Mã số thuế: 3502362464      Tel : 84392901871";
                        sheet.Cells["G5"].Value = "Supplier\n(공 급 자)";
                        sheet.Cells["H5:J5"].Merge = true;
                        sheet.Cells["H5"].Value = "";
                        sheet.Cells["K5"].Value = "Supplier Tel No.\n/ FAX NO.";
                        sheet.Cells["G6"].Value = "QT'Y\n수량";
                        sheet.Cells["H6"].Value = "Util\n단위";
                        sheet.Cells["I6"].Value = "Weight(kg)\n중  량";
                        sheet.Cells["J6"].Value = "MPS No. / Rev.\n자재사양서 번호";
                        sheet.Cells["K6"].Value = "Req'd Date\n입고요청일";

                        sheet.Cells["L1"].Value = "Rev. No.\n개정번호";
                        sheet.Cells["M1"].Value = "Date\n일 자";
                        sheet.Cells["N1"].Value = "Prepared\n작   성";
                        sheet.Cells["O1"].Value = "Reviewed\n검  토";
                        sheet.Cells["P1"].Value = "Agreement\n합 의";
                        sheet.Cells["Q1"].Value = "Approved\n승   인";

                        sheet.Cells["L2:L4"].Merge = true;
                        sheet.Cells["L2"].Value = "";
                        sheet.Cells["M2:M4"].Merge = true;
                        sheet.Cells["M2"].Value = "";
                        sheet.Cells["N2:N4"].Merge = true;
                        sheet.Cells["N2"].Value = "" + DateTime.Now.ToString("dd-MM-yyyy");
                        sheet.Cells["O2:O4"].Merge = true;
                        sheet.Cells["O2"].Value = "";
                        sheet.Cells["P2:P4"].Merge = true;
                        sheet.Cells["P2"].Value = "";
                        sheet.Cells["Q2:Q4"].Merge = true;
                        sheet.Cells["Q2"].Value = "";

                        sheet.Cells["L5:M5"].Merge = true;
                        sheet.Cells["L5"].Value = "";
                        sheet.Cells["N5"].Value = "Payment Terms\n지불조건";
                        sheet.Cells["O5:Q5"].Merge = true;
                        sheet.Cells["O5"].Value = grdPOs.Rows[0].Cells[5].Value.ToString(); ;
                        sheet.Cells["L6"].Value = "입고장소";
                        sheet.Cells["M6"].Value = "U/price\n단가";
                        sheet.Cells["N6"].Value = "Amount\n금  액";
                        sheet.Cells["O6"].Value = "Receive\r접  수";
                        sheet.Cells["P6:Q6"].Merge = true;
                        sheet.Cells["P6"].Value = "Remarks\n비  고";

                        int r = 7;
                        foreach (DataRow item in dtExportPO.Rows)
                        {
                            sheet.Cells[$"A{r}"].Value = item[0].ToString();
                            sheet.Cells[$"B{r}"].Value = item[1].ToString();
                            sheet.Cells[$"C{r}"].Value = item[2].ToString();

                            sheet.Cells[$"D{r}:F{r}"].Merge = true;
                            sheet.Cells[$"D{r}"].Value = item[3].ToString();

                            sheet.Cells[$"G{r}"].Value = Int64.Parse(item[4].ToString()).ToString("N0");
                            sheet.Cells[$"H{r}"].Value = item[5].ToString();
                            sheet.Cells[$"I{r}"].Value = item[6].ToString();
                            sheet.Cells[$"J{r}"].Value = "";
                            sheet.Cells[$"K{r}"].Value = DateTime.Parse(item[8].ToString()).ToString("dd-MM-yyyy");
                            sheet.Cells[$"L{r}"].Value = item[9].ToString();
                            sheet.Cells[$"M{r}"].Value = Int64.Parse(item[10].ToString()).ToString("N0");
                            sheet.Cells[$"N{r}"].Value = item[11].ToString();

                            sheet.Cells[$"O{r}"].Value = item[12].ToString();

                            sheet.Cells[$"P{r}:Q{r}"].Merge = true;
                            sheet.Cells[$"P{r}"].Value = item[13].ToString();

                            r++;
                        }

                        int rowIndex = 6;
                        foreach (DataRow item in dtExportPO.Rows)
                        {
                            if (!string.IsNullOrEmpty(item[14].ToString()))
                            {
                                FileInfo imageFile = new FileInfo(item[14].ToString());
                                Image image = Image.FromFile(item[14].ToString());

                                // Add the image to the worksheet
                                ExcelPicture picture = sheet.Drawings.AddPicture($"PictureName {rowIndex}", imageFile, new ExcelHyperLink(item[14].ToString()));
                                picture.SetSize(130, 70);
                                picture.SetPosition(rowIndex, 0, 4, 0);
                                rowIndex++;
                                item[4] = new byte[0];
                                sheet.Cells[$"D{rowIndex}"].Value = "";
                            }
                            else
                            {
                                string fileName = string.Format("{0}Resources\\picture-bg1.jpg", Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));
                                FileInfo imageFile = new FileInfo(fileName);
                                Image image = Image.FromHbitmap(Properties.Resources.picture_bg.GetHbitmap());

                                // Add the image to the worksheet
                                ImageConverter converter= new ImageConverter();
                                ExcelPicture picture = sheet.Drawings.AddPicture($"PictureName {rowIndex}", 
                                                new MemoryStream((byte[])converter.ConvertTo(image, typeof(byte[]))));

                                picture.SetSize(130, 70);
                                picture.SetPosition(rowIndex, 0, 4, 0);
                                rowIndex++;
                                item[4] = new byte[0];
                                sheet.Cells[$"D{rowIndex}"].Value = "";
                            }
                        }

                        // Get the style object for the range of cells
                        var addressTable = $"A1:Q{dtExportPO.Rows.Count + 6}";
                        var rangeStyle = sheet.Cells[addressTable].Style;
                        var loop = dtExportPO.Rows.Count + 7;
                        for (int i = 7; i < loop; i++)
                        {
                            sheet.Row(i).Height = 70;
                        }

                        // Center the content horizontally and vertically for the range
                        rangeStyle.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rangeStyle.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        // Set border for the range
                        rangeStyle.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rangeStyle.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rangeStyle.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rangeStyle.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        package.SaveAs(new FileInfo(path));

                        KryptonMessageBox.Show("Export successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}

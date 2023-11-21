using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI.Suppliers
{
    public partial class ucSupplier : UserControl
    {
        public DataTable data;
        public ucSupplier()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            grdSupplierType.DataSource = SupplierDAO.GetSuppierTypes();
            data = SupplierDAO.GetSuppiers();
            grdSupplier.DataSource = data;
        }

        private void btnAddSupplierType_Click(object sender, EventArgs e)
        {
            Add_SupplierType add_SupplierType = new Add_SupplierType();
            add_SupplierType.ShowDialog();
            LoadData();
        }

        private void grdSupplierType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void grdSupplier_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void Add_Serial_Number(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Add_Supplier_V2 add_Supplier = new Add_Supplier_V2();
            add_Supplier.ShowDialog();
            LoadData();
        }

        private void grdSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdSupplier.Rows.Count <= 0) return;
            int rsl = grdSupplier.CurrentRow.Index;
            if (grdSupplier.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                Edit_Supplier edit_Supplier = new Edit_Supplier(Guid.Parse(grdSupplier.Rows[rsl].Cells[0].Value.ToString()));
                edit_Supplier.ShowDialog();
                LoadData();
            }
            else
            {
                
            }
        }

        private void mnuDeleteSupplier_Click(object sender, EventArgs e)
        {
            if (grdSupplier.Rows.Count <= 0) return;
            int rsl = grdSupplier.CurrentRow.Index;
            if (grdSupplier.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                string name_supplier = grdSupplier.Rows[rsl].Cells[2].Value.ToString();
                if (KryptonMessageBox.Show($"Do you want delete supplier {name_supplier.ToUpper()} ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel)
                    return;

                SupplierDAO.Delete(Guid.Parse(grdSupplier.Rows[rsl].Cells[0].Value.ToString()));
                LoadData();
            }
            else
            {

            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdSupplier.DataSource = data;
            }
            DataView dv = data.DefaultView;
            dv.RowFilter = $"CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NAME_SUPPIER LIKE '%{txtSearch.Text}%' " +
                        $"OR NAME_COMPANY_SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ADDRESS LIKE '%{txtSearch.Text}%' " +
                        $"OR PHONE LIKE '%{txtSearch.Text}%' " +
                        $"OR EMAIL LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%'";
            grdSupplier.DataSource = dv.ToTable();
        }
    }
}

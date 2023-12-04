using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using Storage.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Storage.GUI.Items
{
    public partial class Add_Item : KryptonForm
    {
        private string path = string.Empty;
        public Add_Item()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            var dtUnits = await Unit_DAO.GetUnits();
            cboUnit.DataSource = dtUnits;
            cboUnit.DisplayMember = "Name";
            cboUnit.ValueMember = "ID";

            var dtGroups = await Group_DAO.GetGroups();
            cboGroup.DataSource = dtGroups;
            cboGroup.DisplayMember = "Name";
            cboGroup.ValueMember = "ID";

            var dtTypes = await Type_DAO.GetTypes();
            cboType.DataSource = dtTypes;
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "ID";

            var dtSupplier = SupplierDAO.GetSuppiers();
            cboSupplier.DataSource = dtSupplier;
            cboSupplier.DisplayMember = "NAME_SUPPIER";
            cboSupplier.ValueMember = "ID";
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                KryptonMessageBox.Show("Please fill in all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            ItemDto itemDto = new ItemDto()
            {
                Id = Guid.NewGuid(),
                Code = txtCode.Text,
                Name = txtName.Text,
                PictureLink = path,
                Picture = !string.IsNullOrEmpty(path) ? File.ReadAllBytes(path) : null,
                Note = "",
                EngName = "",
                UnitId = Guid.Parse(cboUnit.SelectedValue.ToString()),
                GroupId = Guid.Parse(cboGroup.SelectedValue.ToString()),
                TypeId = Guid.Parse(cboType.SelectedValue.ToString()),
                SupplierId = Guid.Parse(cboSupplier.SelectedValue.ToString()),
            };

            if (string.IsNullOrEmpty(path))
            {
                if (await Item_DAO.AddNoIamge(itemDto))
                {
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (await Item_DAO.Add(itemDto))
                {
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnGetCode_Click(object sender, EventArgs e)
        {
            string code = cboType.Text;
            string number = await Item_DAO.GetCode(code.ToUpper().Trim());

            txtCode.Text = code + number;
        }

        private void picItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picItem.Image = new Bitmap(open.FileName);
                path = open.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Do you want to cancel the current action ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel)
                return;
            this.Close();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

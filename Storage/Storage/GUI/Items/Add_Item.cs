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

        private void LoadData()
        {
            var dtUnits = Unit_DAO.GetUnits();
            cboUnit.DataSource = dtUnits;
            cboUnit.DisplayMember = "Name";
            cboUnit.ValueMember = "ID";

            var dtGroups = Group_DAO.GetGroups();
            cboGroup.DataSource = dtGroups;
            cboGroup.DisplayMember = "Name";
            cboGroup.ValueMember = "ID";

            var dtTypes = Type_DAO.GetTypes();
            cboType.DataSource = dtTypes;
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "ID";

            var dtSupplier = SupplierDAO.GetSuppiers();
            cboSupplier.DataSource = dtSupplier;
            cboSupplier.DisplayMember = "NAME_SUPPIER";
            cboSupplier.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                KryptonMessageBox.Show("Please fill in all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ItemDto itemDto = new ItemDto()
            {
                Id = Guid.NewGuid(),
                Code = txtCode.Text,
                Name = txtName.Text,
                PictureLink = path,
                Picture = path,
                Note = "",
                Eng_Name = "",
                UnitId = Guid.Parse(cboUnit.SelectedValue.ToString()),
                GroupId = Guid.Parse(cboGroup.SelectedValue.ToString()),
                TypeId = Guid.Parse(cboType.SelectedValue.ToString()),
                SupplierId = Guid.Parse(cboSupplier.SelectedValue.ToString()),
            };

            if (string.IsNullOrEmpty(path))
            {
                this.Close();
                if (Item_DAO.AddNoIamge(itemDto))
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
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Item_DAO.Add(itemDto))
                {
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            string code = cboType.Text;
            string number = Item_DAO.GetCode(code.ToUpper().Trim());

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
    }
}

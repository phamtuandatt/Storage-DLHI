using ComponentFactory.Krypton.Ribbon;
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

namespace Storage.GUI.Items
{
    public partial class Edit_Item : KryptonForm
    {
        public Guid Id { get; set; }
        public string path = string.Empty;

        public Edit_Item()
        {
            InitializeComponent();
        }

        public Edit_Item(Guid Id)
        {
            InitializeComponent();
            this.Id = Id;
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

            ItemDto dto = await Item_DAO.GetItemAsync(this.Id);
            if (dto != null)
            {
                txtCode.Text = dto.Code;
                txtName.Text = dto.Name;
                cboUnit.SelectedValue = dto.UnitId;
                cboGroup.SelectedValue = dto.GroupId;
                cboType.SelectedValue = dto.TypeId;
                cboSupplier.SelectedValue = dto.SupplierId;
                txtNote.Text = dto.Note;
                txtEngName.Text = dto.EngName;
                picItem.Image = dto.Image.Length == 100 ? picItem.InitialImage : Image.FromStream(new MemoryStream(dto.Image));
                path = dto.PictureLink;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Do you want to cancel the current action ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel)
                return;
            this.Close();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(path))
            {
                ItemDto itemDto = new ItemDto()
                {
                    Id = this.Id,
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    Note = txtNote.Text,
                    EngName = txtEngName.Text,
                    UnitId = Guid.Parse(cboUnit.SelectedValue.ToString()),
                    GroupId = Guid.Parse(cboGroup.SelectedValue.ToString()),
                    TypeId = Guid.Parse(cboType.SelectedValue.ToString()),
                    SupplierId = Guid.Parse(cboSupplier.SelectedValue.ToString()),
                };
                if (await Item_DAO.UpdateNoImage(itemDto))
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
                ItemDto itemDto = new ItemDto()
                {
                    Id = this.Id,
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    PictureLink = path,
                    Picture = !string.IsNullOrEmpty(path) ? File.ReadAllBytes(path) : new byte[100],
                    Image = File.ReadAllBytes(path),
                    Note = txtNote.Text,
                    EngName = txtEngName.Text,
                    UnitId = Guid.Parse(cboUnit.SelectedValue.ToString()),
                    GroupId = Guid.Parse(cboGroup.SelectedValue.ToString()),
                    TypeId = Guid.Parse(cboType.SelectedValue.ToString()),
                    SupplierId = Guid.Parse(cboSupplier.SelectedValue.ToString()),
                };
                if (await Item_DAO.Update(itemDto))
                {
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


            
        }

        private void picItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picItem.Image = new Bitmap(open.FileName);
                path = open.FileName;
            }
        }

        private void txtEngName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

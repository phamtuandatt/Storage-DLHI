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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_MPR
{
    public partial class Add_MPR : KryptonForm
    {
        public Guid Item_Id { get; set; }
        public Add_MPR()
        {
            InitializeComponent();
        }

        public Add_MPR(Guid Item_Id)
        {
            InitializeComponent();
            this.Item_Id = Item_Id;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMPR.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMPR.Focus();
                return;
            }
            MakeNewRequestDto makeNewRequestDto = new MakeNewRequestDto()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                ExpectDelivery = DateTime.Parse(txtExpectedDelivery.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                Note = txtNote.Text,
                Item_Id = Item_Id,
                MPR_No = txtMPR.Text,
                Usage = txtUsage.Text,
                Quantity = Convert.ToInt64(txtQuantity.Text.Replace(",", "")),
            };

            if (MPR_DAO.Add(makeNewRequestDto))
            {
                // Add MPR Export
                if (chkCreateNew.Checked)
                {
                    //Update Status of MPR_Export have status is 2 become is 1
                    if (MPR_DAO.UpdateMPR_Export_Status())
                    {
                        // Create MPR_Export
                        MPR_Export mPR_Export = new MPR_Export()
                        {
                            Id = Guid.NewGuid(),
                            Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                            ItemCount = 1,
                            Status = 2,
                        };
                        MPR_Export_Detail detail = new MPR_Export_Detail()
                        {
                            MPR_Export_Id = mPR_Export.Id,
                            MPR_Id = makeNewRequestDto.Id,
                        };

                        if (MPR_DAO.CreateMPR_Export(mPR_Export) && MPR_DAO.CreateMPR_Export_Detail(detail))
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        // Create MPR_Export
                        MPR_Export mPR_Export = new MPR_Export()
                        {
                            Id = Guid.NewGuid(),
                            Created = DateTime.Parse(txtCreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt")),
                            ItemCount = 1,
                            Status = 2,
                        };
                        MPR_Export_Detail detail = new MPR_Export_Detail()
                        {
                            MPR_Export_Id = mPR_Export.Id,
                            MPR_Id = makeNewRequestDto.Id,
                        };

                        if (MPR_DAO.CreateMPR_Export(mPR_Export) && MPR_DAO.CreateMPR_Export_Detail(detail))
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    // Insert MPR_Export have status = 2
                    MPR_Export_Detail detail = new MPR_Export_Detail()
                    {
                        MPR_Export_Id = Guid.NewGuid(),
                        MPR_Id = makeNewRequestDto.Id,
                    };

                    if (MPR_DAO.InsertDetailExportIntoCurrentMPRExport(detail))
                    {
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Please checked Create new MPR File Because You completed Current MPR and Exported File !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMPR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NO.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtUsage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
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
    }
}

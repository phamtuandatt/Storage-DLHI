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
            MakeNewRequestDto makeNewRequestDto = new MakeNewRequestDto()
            {
                Id = Guid.NewGuid(),
                Created = txtCreateDate.Value,
                ExpectDelivery = txtExpectedDelivery.Value,
                Note = txtNote.Text,
                Item_Id = Item_Id,
                MPR_No = txtMPR.Text,
                Usage = txtUsage.Text,
                Quantity = int.Parse(txtQuantity.Text),
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
                            Created = txtCreateDate.Value,
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
    }
}

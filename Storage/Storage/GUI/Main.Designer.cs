namespace Storage.GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.btnAddUnit = new System.Windows.Forms.Button();
            this.btnAddGroupConsumable = new System.Windows.Forms.Button();
            this.btnAddTypeConsumable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Location = new System.Drawing.Point(25, 43);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(75, 23);
            this.btnAddSupplier.TabIndex = 0;
            this.btnAddSupplier.Text = "Add Supplier";
            this.btnAddSupplier.UseVisualStyleBackColor = true;
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // btnAddUnit
            // 
            this.btnAddUnit.Location = new System.Drawing.Point(25, 93);
            this.btnAddUnit.Name = "btnAddUnit";
            this.btnAddUnit.Size = new System.Drawing.Size(75, 23);
            this.btnAddUnit.TabIndex = 0;
            this.btnAddUnit.Text = "Add Unit";
            this.btnAddUnit.UseVisualStyleBackColor = true;
            this.btnAddUnit.Click += new System.EventHandler(this.btnAddUnit_Click);
            // 
            // btnAddGroupConsumable
            // 
            this.btnAddGroupConsumable.Location = new System.Drawing.Point(25, 137);
            this.btnAddGroupConsumable.Name = "btnAddGroupConsumable";
            this.btnAddGroupConsumable.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroupConsumable.TabIndex = 0;
            this.btnAddGroupConsumable.Text = "Add Group Consumable";
            this.btnAddGroupConsumable.UseVisualStyleBackColor = true;
            this.btnAddGroupConsumable.Click += new System.EventHandler(this.btnAddGroupConsumable_Click);
            // 
            // btnAddTypeConsumable
            // 
            this.btnAddTypeConsumable.Location = new System.Drawing.Point(25, 179);
            this.btnAddTypeConsumable.Name = "btnAddTypeConsumable";
            this.btnAddTypeConsumable.Size = new System.Drawing.Size(75, 23);
            this.btnAddTypeConsumable.TabIndex = 0;
            this.btnAddTypeConsumable.Text = "Add Type Consumable";
            this.btnAddTypeConsumable.UseVisualStyleBackColor = true;
            this.btnAddTypeConsumable.Click += new System.EventHandler(this.btnAddTypeConsumable_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 625);
            this.Controls.Add(this.btnAddTypeConsumable);
            this.Controls.Add(this.btnAddGroupConsumable);
            this.Controls.Add(this.btnAddUnit);
            this.Controls.Add(this.btnAddSupplier);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnAddUnit;
        private System.Windows.Forms.Button btnAddGroupConsumable;
        private System.Windows.Forms.Button btnAddTypeConsumable;
    }
}
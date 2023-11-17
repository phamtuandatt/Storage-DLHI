namespace Storage.GUI.Suppliers
{
    partial class ucSupplier
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.txtSearch = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel5 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox3 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.grdSupplier = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuStripSuppliers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnAddSupplier = new System.Windows.Forms.ToolStripButton();
            this.kryptonPanel4 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.grdSupplierType = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddSupplierType = new System.Windows.Forms.ToolStripButton();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItems3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).BeginInit();
            this.kryptonPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSupplier)).BeginInit();
            this.mnuStripSuppliers.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSupplierType)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.txtSearch);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kryptonPanel1.Size = new System.Drawing.Size(1412, 62);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel1.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(89, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(379, 30);
            this.txtSearch.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtSearch.StateCommon.Border.Rounding = 15;
            this.txtSearch.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, -1, -1, -1);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonLabel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(73, 62);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "   Search:";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 62);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kryptonPanel2.Size = new System.Drawing.Size(1412, 768);
            this.kryptonPanel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1412, 768);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kryptonPanel5
            // 
            this.kryptonPanel5.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel5.Location = new System.Drawing.Point(286, 4);
            this.kryptonPanel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel5.Name = "kryptonPanel5";
            this.kryptonPanel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kryptonPanel5.Size = new System.Drawing.Size(1122, 760);
            this.kryptonPanel5.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel5.StateCommon.Color2 = System.Drawing.Color.White;
            this.kryptonPanel5.TabIndex = 2;
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            this.kryptonGroupBox3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.grdSupplier);
            this.kryptonGroupBox3.Panel.Controls.Add(this.toolStrip3);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(1122, 760);
            this.kryptonGroupBox3.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonGroupBox3.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonGroupBox3.TabIndex = 3;
            this.kryptonGroupBox3.Values.Heading = "Suppliers";
            // 
            // grdSupplier
            // 
            this.grdSupplier.AllowUserToAddRows = false;
            this.grdSupplier.AllowUserToDeleteRows = false;
            this.grdSupplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.grdSupplier.ContextMenuStrip = this.mnuStripSuppliers;
            this.grdSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSupplier.Location = new System.Drawing.Point(0, 27);
            this.grdSupplier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdSupplier.MultiSelect = false;
            this.grdSupplier.Name = "grdSupplier";
            this.grdSupplier.ReadOnly = true;
            this.grdSupplier.RowHeadersWidth = 51;
            this.grdSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSupplier.Size = new System.Drawing.Size(1118, 705);
            this.grdSupplier.TabIndex = 3;
            this.grdSupplier.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdSupplier_CellDoubleClick);
            this.grdSupplier.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdSupplier_RowPostPaint);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn3.HeaderText = "ID";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Code";
            this.Column3.HeaderText = "Code";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "NAME_SUPPIER";
            this.Column4.HeaderText = "Name";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "NAME_COMPANY_SUPPLIER";
            this.Column5.HeaderText = "Company";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ADDRESS";
            this.Column6.HeaderText = "Address";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "PHONE";
            this.Column7.HeaderText = "Phone";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "EMAIL";
            this.Column8.HeaderText = "Email";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "NOTE";
            this.Column9.HeaderText = "Note";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // mnuStripSuppliers
            // 
            this.mnuStripSuppliers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mnuStripSuppliers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuStripSuppliers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteSupplier});
            this.mnuStripSuppliers.Name = "mnuStripSuppliers";
            this.mnuStripSuppliers.Size = new System.Drawing.Size(182, 28);
            // 
            // mnuDeleteSupplier
            // 
            this.mnuDeleteSupplier.Name = "mnuDeleteSupplier";
            this.mnuDeleteSupplier.Size = new System.Drawing.Size(181, 24);
            this.mnuDeleteSupplier.Text = "Delete Supplier";
            this.mnuDeleteSupplier.Click += new System.EventHandler(this.mnuDeleteSupplier_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSupplier});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip3.Size = new System.Drawing.Size(1118, 27);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddSupplier.Image = global::Storage.Properties.Resources.add;
            this.btnAddSupplier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(29, 24);
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel4.Location = new System.Drawing.Point(4, 4);
            this.kryptonPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kryptonPanel4.Size = new System.Drawing.Size(274, 760);
            this.kryptonPanel4.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel4.StateCommon.Color2 = System.Drawing.Color.White;
            this.kryptonPanel4.TabIndex = 3;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            this.kryptonGroupBox2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.grdSupplierType);
            this.kryptonGroupBox2.Panel.Controls.Add(this.toolStrip2);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(274, 760);
            this.kryptonGroupBox2.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonGroupBox2.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonGroupBox2.TabIndex = 2;
            this.kryptonGroupBox2.Values.Heading = "Supplier type";
            // 
            // grdSupplierType
            // 
            this.grdSupplierType.AllowUserToAddRows = false;
            this.grdSupplierType.AllowUserToDeleteRows = false;
            this.grdSupplierType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdSupplierType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSupplierType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.grdSupplierType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSupplierType.Location = new System.Drawing.Point(0, 27);
            this.grdSupplierType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdSupplierType.MultiSelect = false;
            this.grdSupplierType.Name = "grdSupplierType";
            this.grdSupplierType.ReadOnly = true;
            this.grdSupplierType.RowHeadersWidth = 51;
            this.grdSupplierType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSupplierType.Size = new System.Drawing.Size(270, 705);
            this.grdSupplierType.TabIndex = 1;
            this.grdSupplierType.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdSupplierType_RowPostPaint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSupplierType});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(270, 27);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddSupplierType
            // 
            this.btnAddSupplierType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddSupplierType.Image = global::Storage.Properties.Resources.add;
            this.btnAddSupplierType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSupplierType.Name = "btnAddSupplierType";
            this.btnAddSupplierType.Size = new System.Drawing.Size(29, 24);
            this.btnAddSupplierType.Click += new System.EventHandler(this.btnAddSupplierType_Click);
            // 
            // ucSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucSupplier";
            this.Size = new System.Drawing.Size(1412, 830);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).EndInit();
            this.kryptonPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            this.kryptonGroupBox3.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSupplier)).EndInit();
            this.mnuStripSuppliers.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSupplierType)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel5;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grdSupplier;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnAddSupplier;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grdSupplierType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddSupplierType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.ContextMenuStrip mnuStripSuppliers;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteSupplier;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems3;
    }
}

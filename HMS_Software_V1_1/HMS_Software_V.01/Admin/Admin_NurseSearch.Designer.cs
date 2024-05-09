namespace HMS_Software_V1._01.Admin
{
    partial class Admin_NurseSearch
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
            this.dataGridView_nurse = new System.Windows.Forms.DataGridView();
            this.nurseSearch_combobox = new System.Windows.Forms.ComboBox();
            this.panel1_top2 = new System.Windows.Forms.Panel();
            this.A_N_search_tbx = new System.Windows.Forms.TextBox();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.TopTable_Panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.A_DS_date = new System.Windows.Forms.Label();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.A_DS_time = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TopTable_Panel2 = new System.Windows.Forms.Panel();
            this.top_tableLayoutPanle = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_LOGO = new System.Windows.Forms.PictureBox();
            this.TopTable_Panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_nurse)).BeginInit();
            this.panel1_top2.SuspendLayout();
            this.TopTable_Panel.SuspendLayout();
            this.TopTable_Panel2.SuspendLayout();
            this.top_tableLayoutPanle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LOGO)).BeginInit();
            this.TopTable_Panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_nurse
            // 
            this.dataGridView_nurse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_nurse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_nurse.Location = new System.Drawing.Point(4, 185);
            this.dataGridView_nurse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView_nurse.Name = "dataGridView_nurse";
            this.dataGridView_nurse.RowHeadersWidth = 51;
            this.dataGridView_nurse.Size = new System.Drawing.Size(1336, 472);
            this.dataGridView_nurse.TabIndex = 11;
            // 
            // nurseSearch_combobox
            // 
            this.nurseSearch_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nurseSearch_combobox.BackColor = System.Drawing.Color.White;
            this.nurseSearch_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nurseSearch_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nurseSearch_combobox.ForeColor = System.Drawing.Color.Black;
            this.nurseSearch_combobox.FormattingEnabled = true;
            this.nurseSearch_combobox.Items.AddRange(new object[] {
            "By ID",
            "By NIC"});
            this.nurseSearch_combobox.Location = new System.Drawing.Point(1009, 11);
            this.nurseSearch_combobox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nurseSearch_combobox.Name = "nurseSearch_combobox";
            this.nurseSearch_combobox.Size = new System.Drawing.Size(160, 28);
            this.nurseSearch_combobox.TabIndex = 47;
            this.nurseSearch_combobox.SelectedIndexChanged += new System.EventHandler(this.nurseSearch_combobox_SelectedIndexChanged);
            // 
            // panel1_top2
            // 
            this.panel1_top2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.panel1_top2.Controls.Add(this.nurseSearch_combobox);
            this.panel1_top2.Controls.Add(this.A_N_search_tbx);
            this.panel1_top2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_top2.Location = new System.Drawing.Point(4, 128);
            this.panel1_top2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1_top2.Name = "panel1_top2";
            this.panel1_top2.Size = new System.Drawing.Size(1336, 49);
            this.panel1_top2.TabIndex = 10;
            // 
            // A_N_search_tbx
            // 
            this.A_N_search_tbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.A_N_search_tbx.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A_N_search_tbx.Location = new System.Drawing.Point(665, 10);
            this.A_N_search_tbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.A_N_search_tbx.MaximumSize = new System.Drawing.Size(665, 50);
            this.A_N_search_tbx.MinimumSize = new System.Drawing.Size(320, 20);
            this.A_N_search_tbx.Name = "A_N_search_tbx";
            this.A_N_search_tbx.Size = new System.Drawing.Size(335, 29);
            this.A_N_search_tbx.TabIndex = 27;
            this.A_N_search_tbx.TextChanged += new System.EventHandler(this.A_N_search_tbx_TextChanged);
            // 
            // materialDivider2
            // 
            this.materialDivider2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialDivider2.BackColor = System.Drawing.Color.Gray;
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(383, -21);
            this.materialDivider2.Margin = new System.Windows.Forms.Padding(0);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(4, 154);
            this.materialDivider2.TabIndex = 4;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // TopTable_Panel
            // 
            this.TopTable_Panel.Controls.Add(this.label1);
            this.TopTable_Panel.Controls.Add(this.materialDivider2);
            this.TopTable_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTable_Panel.Location = new System.Drawing.Point(4, 4);
            this.TopTable_Panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TopTable_Panel.Name = "TopTable_Panel";
            this.TopTable_Panel.Size = new System.Drawing.Size(399, 108);
            this.TopTable_Panel.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nurse Search";
            // 
            // A_DS_date
            // 
            this.A_DS_date.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.A_DS_date.AutoSize = true;
            this.A_DS_date.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A_DS_date.Location = new System.Drawing.Point(86, 63);
            this.A_DS_date.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.A_DS_date.Name = "A_DS_date";
            this.A_DS_date.Size = new System.Drawing.Size(73, 32);
            this.A_DS_date.TabIndex = 3;
            this.A_DS_date.Text = "Date";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialDivider1.BackColor = System.Drawing.Color.Gray;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(332, -18);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(0);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(4, 154);
            this.materialDivider1.TabIndex = 2;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // A_DS_time
            // 
            this.A_DS_time.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.A_DS_time.AutoSize = true;
            this.A_DS_time.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A_DS_time.Location = new System.Drawing.Point(82, 16);
            this.A_DS_time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.A_DS_time.Name = "A_DS_time";
            this.A_DS_time.Size = new System.Drawing.Size(81, 32);
            this.A_DS_time.TabIndex = 5;
            this.A_DS_time.Text = "Time";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Admin";
            // 
            // TopTable_Panel2
            // 
            this.TopTable_Panel2.Controls.Add(this.label2);
            this.TopTable_Panel2.Controls.Add(this.materialDivider1);
            this.TopTable_Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTable_Panel2.Location = new System.Drawing.Point(411, 4);
            this.TopTable_Panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TopTable_Panel2.Name = "TopTable_Panel2";
            this.TopTable_Panel2.Size = new System.Drawing.Size(385, 108);
            this.TopTable_Panel2.TabIndex = 8;
            // 
            // top_tableLayoutPanle
            // 
            this.top_tableLayoutPanle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.top_tableLayoutPanle.ColumnCount = 4;
            this.top_tableLayoutPanle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.43912F));
            this.top_tableLayoutPanle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.44112F));
            this.top_tableLayoutPanle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.top_tableLayoutPanle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.top_tableLayoutPanle.Controls.Add(this.pictureBox_LOGO, 3, 0);
            this.top_tableLayoutPanle.Controls.Add(this.TopTable_Panel2, 1, 0);
            this.top_tableLayoutPanle.Controls.Add(this.TopTable_Panel3, 2, 0);
            this.top_tableLayoutPanle.Controls.Add(this.TopTable_Panel, 0, 0);
            this.top_tableLayoutPanle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top_tableLayoutPanle.Location = new System.Drawing.Point(4, 4);
            this.top_tableLayoutPanle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.top_tableLayoutPanle.Name = "top_tableLayoutPanle";
            this.top_tableLayoutPanle.RowCount = 1;
            this.top_tableLayoutPanle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.top_tableLayoutPanle.Size = new System.Drawing.Size(1336, 116);
            this.top_tableLayoutPanle.TabIndex = 9;
            // 
            // pictureBox_LOGO
            // 
            this.pictureBox_LOGO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_LOGO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.pictureBox_LOGO.Image = global::HMS_Software_V1._01.Properties.Resources.HMS_Logo4_100_;
            this.pictureBox_LOGO.Location = new System.Drawing.Point(1168, 4);
            this.pictureBox_LOGO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox_LOGO.Name = "pictureBox_LOGO";
            this.pictureBox_LOGO.Size = new System.Drawing.Size(133, 108);
            this.pictureBox_LOGO.TabIndex = 11;
            this.pictureBox_LOGO.TabStop = false;
            // 
            // TopTable_Panel3
            // 
            this.TopTable_Panel3.Controls.Add(this.A_DS_date);
            this.TopTable_Panel3.Controls.Add(this.A_DS_time);
            this.TopTable_Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTable_Panel3.Location = new System.Drawing.Point(804, 4);
            this.TopTable_Panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TopTable_Panel3.Name = "TopTable_Panel3";
            this.TopTable_Panel3.Size = new System.Drawing.Size(326, 108);
            this.TopTable_Panel3.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.top_tableLayoutPanle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1_top2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_nurse, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.61855F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.38145F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1344, 661);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Admin_NurseSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1359, 698);
            this.Name = "Admin_NurseSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin_NurseSearch";
            this.Load += new System.EventHandler(this.Admin_NurseSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_nurse)).EndInit();
            this.panel1_top2.ResumeLayout(false);
            this.panel1_top2.PerformLayout();
            this.TopTable_Panel.ResumeLayout(false);
            this.TopTable_Panel.PerformLayout();
            this.TopTable_Panel2.ResumeLayout(false);
            this.TopTable_Panel2.PerformLayout();
            this.top_tableLayoutPanle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LOGO)).EndInit();
            this.TopTable_Panel3.ResumeLayout(false);
            this.TopTable_Panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_nurse;
        private System.Windows.Forms.ComboBox nurseSearch_combobox;
        private System.Windows.Forms.Panel panel1_top2;
        private System.Windows.Forms.TextBox A_N_search_tbx;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private System.Windows.Forms.Panel TopTable_Panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label A_DS_date;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.Label A_DS_time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel TopTable_Panel2;
        private System.Windows.Forms.TableLayoutPanel top_tableLayoutPanle;
        private System.Windows.Forms.Panel TopTable_Panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox_LOGO;
    }
}
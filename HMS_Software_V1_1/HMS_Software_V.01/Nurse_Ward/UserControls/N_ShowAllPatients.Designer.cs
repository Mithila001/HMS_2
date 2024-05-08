namespace HMS_Software_V1._01.Nurse_Ward.UserControls
{
    partial class N_ShowAllPatients
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.label2 = new System.Windows.Forms.Label();
            this.NSAP_id = new System.Windows.Forms.Label();
            this.AR_patienRID = new System.Windows.Forms.Label();
            this.NSAP_name = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.label5 = new System.Windows.Forms.Label();
            this.NSAP_gender = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NSAP_age = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.NSAP_Condition = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 87);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 87);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.materialDivider1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.NSAP_id);
            this.panel2.Controls.Add(this.AR_patienRID);
            this.panel2.Controls.Add(this.NSAP_name);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 87);
            this.panel2.TabIndex = 1;
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            this.panel2.MouseLeave += new System.EventHandler(this.panel2_MouseLeave);
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.materialDivider1.BackColor = System.Drawing.Color.Gray;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(381, 4);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(0);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(3, 80);
            this.materialDivider1.TabIndex = 17;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "ID:";
            // 
            // NSAP_id
            // 
            this.NSAP_id.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NSAP_id.AutoSize = true;
            this.NSAP_id.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NSAP_id.Location = new System.Drawing.Point(68, 49);
            this.NSAP_id.Name = "NSAP_id";
            this.NSAP_id.Size = new System.Drawing.Size(86, 19);
            this.NSAP_id.TabIndex = 16;
            this.NSAP_id.Text = "PXXXXXX";
            // 
            // AR_patienRID
            // 
            this.AR_patienRID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AR_patienRID.AutoSize = true;
            this.AR_patienRID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_patienRID.Location = new System.Drawing.Point(17, 17);
            this.AR_patienRID.Name = "AR_patienRID";
            this.AR_patienRID.Size = new System.Drawing.Size(45, 16);
            this.AR_patienRID.TabIndex = 13;
            this.AR_patienRID.Text = "Name:";
            // 
            // NSAP_name
            // 
            this.NSAP_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NSAP_name.AutoSize = true;
            this.NSAP_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NSAP_name.Location = new System.Drawing.Point(68, 16);
            this.NSAP_name.Name = "NSAP_name";
            this.NSAP_name.Size = new System.Drawing.Size(110, 19);
            this.NSAP_name.TabIndex = 14;
            this.NSAP_name.Text = "Patient Name";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.materialDivider2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.NSAP_gender);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.NSAP_age);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(412, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(196, 87);
            this.panel3.TabIndex = 2;
            // 
            // materialDivider2
            // 
            this.materialDivider2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.materialDivider2.BackColor = System.Drawing.Color.Gray;
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(184, 4);
            this.materialDivider2.Margin = new System.Windows.Forms.Padding(0);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(3, 80);
            this.materialDivider2.TabIndex = 18;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Gender:";
            // 
            // NSAP_gender
            // 
            this.NSAP_gender.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NSAP_gender.AutoSize = true;
            this.NSAP_gender.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NSAP_gender.Location = new System.Drawing.Point(94, 50);
            this.NSAP_gender.Name = "NSAP_gender";
            this.NSAP_gender.Size = new System.Drawing.Size(53, 19);
            this.NSAP_gender.TabIndex = 18;
            this.NSAP_gender.Text = "XXXX";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Age:";
            // 
            // NSAP_age
            // 
            this.NSAP_age.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NSAP_age.AutoSize = true;
            this.NSAP_age.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NSAP_age.Location = new System.Drawing.Point(93, 18);
            this.NSAP_age.Name = "NSAP_age";
            this.NSAP_age.Size = new System.Drawing.Size(31, 19);
            this.NSAP_age.TabIndex = 16;
            this.NSAP_age.Text = "XX";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.NSAP_Condition);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(608, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(392, 87);
            this.panel4.TabIndex = 3;
            // 
            // NSAP_Condition
            // 
            this.NSAP_Condition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NSAP_Condition.AutoSize = true;
            this.NSAP_Condition.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NSAP_Condition.Location = new System.Drawing.Point(17, 18);
            this.NSAP_Condition.Name = "NSAP_Condition";
            this.NSAP_Condition.Size = new System.Drawing.Size(141, 19);
            this.NSAP_Condition.TabIndex = 15;
            this.NSAP_Condition.Text = "Patient Condition";
            // 
            // N_ShowAllPatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "N_ShowAllPatients";
            this.Size = new System.Drawing.Size(1000, 87);
            this.Click += new System.EventHandler(this.N_ShowAllPatients_Click);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label NSAP_name;
        public System.Windows.Forms.Label AR_patienRID;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label NSAP_id;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label NSAP_gender;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label NSAP_age;
        public System.Windows.Forms.Label NSAP_Condition;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
    }
}

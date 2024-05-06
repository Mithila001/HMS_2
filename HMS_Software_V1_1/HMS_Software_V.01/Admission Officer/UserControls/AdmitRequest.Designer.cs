namespace HMS_Software_V1._01.Admission_Officer.UserControls
{
    partial class AdmitRequest
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
            this.AR_patienName = new System.Windows.Forms.Label();
            this.AR_patienRID = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AR_patientAge = new System.Windows.Forms.Label();
            this.AR_patientGender = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.AR_unitType = new System.Windows.Forms.Label();
            this.AR_patientPanel_colorChanger = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.AR_patientPanel_colorChanger, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.AR_patienName);
            this.panel1.Controls.Add(this.AR_patienRID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 80);
            this.panel1.TabIndex = 0;
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // AR_patienName
            // 
            this.AR_patienName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AR_patienName.AutoSize = true;
            this.AR_patienName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_patienName.Location = new System.Drawing.Point(26, 13);
            this.AR_patienName.Name = "AR_patienName";
            this.AR_patienName.Size = new System.Drawing.Size(110, 19);
            this.AR_patienName.TabIndex = 12;
            this.AR_patienName.Text = "Patient Name";
            // 
            // AR_patienRID
            // 
            this.AR_patienRID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AR_patienRID.AutoSize = true;
            this.AR_patienRID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_patienRID.Location = new System.Drawing.Point(26, 46);
            this.AR_patienRID.Name = "AR_patienRID";
            this.AR_patienRID.Size = new System.Drawing.Size(107, 18);
            this.AR_patienRID.TabIndex = 11;
            this.AR_patienRID.Text = "RegistrationID";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.AR_patientAge);
            this.panel2.Controls.Add(this.AR_patientGender);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 80);
            this.panel2.TabIndex = 1;
            // 
            // AR_patientAge
            // 
            this.AR_patientAge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AR_patientAge.AutoSize = true;
            this.AR_patientAge.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_patientAge.Location = new System.Drawing.Point(38, 57);
            this.AR_patientAge.Name = "AR_patientAge";
            this.AR_patientAge.Size = new System.Drawing.Size(86, 18);
            this.AR_patientAge.TabIndex = 13;
            this.AR_patientAge.Text = "displayAge";
            // 
            // AR_patientGender
            // 
            this.AR_patientGender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AR_patientGender.AutoSize = true;
            this.AR_patientGender.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_patientGender.Location = new System.Drawing.Point(33, 21);
            this.AR_patientGender.Name = "AR_patientGender";
            this.AR_patientGender.Size = new System.Drawing.Size(109, 18);
            this.AR_patientGender.TabIndex = 12;
            this.AR_patientGender.Text = "displayGender";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.AR_unitType);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(403, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 80);
            this.panel3.TabIndex = 2;
            // 
            // AR_unitType
            // 
            this.AR_unitType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AR_unitType.AutoSize = true;
            this.AR_unitType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AR_unitType.Location = new System.Drawing.Point(9, 31);
            this.AR_unitType.Name = "AR_unitType";
            this.AR_unitType.Size = new System.Drawing.Size(65, 18);
            this.AR_unitType.TabIndex = 14;
            this.AR_unitType.Text = "unit type";
            // 
            // AR_patientPanel_colorChanger
            // 
            this.AR_patientPanel_colorChanger.BackColor = System.Drawing.Color.White;
            this.AR_patientPanel_colorChanger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AR_patientPanel_colorChanger.Location = new System.Drawing.Point(483, 0);
            this.AR_patientPanel_colorChanger.Margin = new System.Windows.Forms.Padding(0);
            this.AR_patientPanel_colorChanger.Name = "AR_patientPanel_colorChanger";
            this.AR_patientPanel_colorChanger.Size = new System.Drawing.Size(15, 80);
            this.AR_patientPanel_colorChanger.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Gender:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Age:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(360, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(43, 80);
            this.panel4.TabIndex = 4;
            // 
            // AdmitRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AdmitRequest";
            this.Size = new System.Drawing.Size(498, 80);
            this.Click += new System.EventHandler(this.AdmitRequest_Click);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label AR_patienName;
        public System.Windows.Forms.Panel AR_patientPanel_colorChanger;
        public System.Windows.Forms.Label AR_patienRID;
        public System.Windows.Forms.Label AR_patientAge;
        public System.Windows.Forms.Label AR_patientGender;
        public System.Windows.Forms.Label AR_unitType;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
    }
}

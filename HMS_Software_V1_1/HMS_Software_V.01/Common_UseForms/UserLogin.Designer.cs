namespace HMS_Software_V1._01.Common_UseForms
{
    partial class UserLogin
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wardNumber_tbx = new System.Windows.Forms.TextBox();
            this.warNumber_lbl = new System.Windows.Forms.Label();
            this.unit_lbl = new System.Windows.Forms.Label();
            this.comboB_selceUnit = new System.Windows.Forms.ComboBox();
            this.userLogin_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboB_selcePosition = new System.Windows.Forms.ComboBox();
            this.userPassword_tbx = new System.Windows.Forms.TextBox();
            this.useName_tbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox_LOGO = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LOGO)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.43723F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.56277F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1221, 569);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.wardNumber_tbx);
            this.panel1.Controls.Add(this.warNumber_lbl);
            this.panel1.Controls.Add(this.unit_lbl);
            this.panel1.Controls.Add(this.comboB_selceUnit);
            this.panel1.Controls.Add(this.userLogin_btn);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboB_selcePosition);
            this.panel1.Controls.Add(this.userPassword_tbx);
            this.panel1.Controls.Add(this.useName_tbx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 167);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 402);
            this.panel1.TabIndex = 0;
            // 
            // wardNumber_tbx
            // 
            this.wardNumber_tbx.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.wardNumber_tbx.BackColor = System.Drawing.Color.White;
            this.wardNumber_tbx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wardNumber_tbx.Location = new System.Drawing.Point(867, 165);
            this.wardNumber_tbx.Margin = new System.Windows.Forms.Padding(4);
            this.wardNumber_tbx.MaximumSize = new System.Drawing.Size(665, 4);
            this.wardNumber_tbx.MinimumSize = new System.Drawing.Size(132, 20);
            this.wardNumber_tbx.Name = "wardNumber_tbx";
            this.wardNumber_tbx.Size = new System.Drawing.Size(291, 26);
            this.wardNumber_tbx.TabIndex = 62;
            // 
            // warNumber_lbl
            // 
            this.warNumber_lbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.warNumber_lbl.AutoSize = true;
            this.warNumber_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warNumber_lbl.Location = new System.Drawing.Point(661, 163);
            this.warNumber_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.warNumber_lbl.Name = "warNumber_lbl";
            this.warNumber_lbl.Size = new System.Drawing.Size(183, 29);
            this.warNumber_lbl.TabIndex = 61;
            this.warNumber_lbl.Text = "Ward Number: ";
            // 
            // unit_lbl
            // 
            this.unit_lbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.unit_lbl.AutoSize = true;
            this.unit_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unit_lbl.Location = new System.Drawing.Point(669, 72);
            this.unit_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unit_lbl.Name = "unit_lbl";
            this.unit_lbl.Size = new System.Drawing.Size(60, 29);
            this.unit_lbl.TabIndex = 60;
            this.unit_lbl.Text = "Unit";
            // 
            // comboB_selceUnit
            // 
            this.comboB_selceUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboB_selceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB_selceUnit.FormattingEnabled = true;
            this.comboB_selceUnit.Items.AddRange(new object[] {
            "Clinic",
            "OPD",
            "Ward"});
            this.comboB_selceUnit.Location = new System.Drawing.Point(867, 71);
            this.comboB_selceUnit.Margin = new System.Windows.Forms.Padding(4);
            this.comboB_selceUnit.Name = "comboB_selceUnit";
            this.comboB_selceUnit.Size = new System.Drawing.Size(291, 24);
            this.comboB_selceUnit.TabIndex = 59;
            // 
            // userLogin_btn
            // 
            this.userLogin_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.userLogin_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(46)))), ((int)(((byte)(219)))));
            this.userLogin_btn.FlatAppearance.BorderSize = 0;
            this.userLogin_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.userLogin_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.userLogin_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userLogin_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLogin_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.userLogin_btn.Location = new System.Drawing.Point(521, 327);
            this.userLogin_btn.Margin = new System.Windows.Forms.Padding(4);
            this.userLogin_btn.Name = "userLogin_btn";
            this.userLogin_btn.Size = new System.Drawing.Size(177, 48);
            this.userLogin_btn.TabIndex = 58;
            this.userLogin_btn.Text = "Login";
            this.userLogin_btn.UseVisualStyleBackColor = false;
            this.userLogin_btn.Click += new System.EventHandler(this.userLogin_btn_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 71);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 32);
            this.label4.TabIndex = 30;
            this.label4.Text = "Position:";
            // 
            // comboB_selcePosition
            // 
            this.comboB_selcePosition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboB_selcePosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB_selcePosition.FormattingEnabled = true;
            this.comboB_selcePosition.Items.AddRange(new object[] {
            "Doctor",
            "Nurse",
            "Admin",
            "Reception",
            "Admission Officer",
            "LabEmployee"});
            this.comboB_selcePosition.Location = new System.Drawing.Point(248, 71);
            this.comboB_selcePosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboB_selcePosition.Name = "comboB_selcePosition";
            this.comboB_selcePosition.Size = new System.Drawing.Size(291, 24);
            this.comboB_selcePosition.TabIndex = 29;
            this.comboB_selcePosition.SelectedIndexChanged += new System.EventHandler(this.comboB_selcePosition_SelectedIndexChanged);
            // 
            // userPassword_tbx
            // 
            this.userPassword_tbx.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.userPassword_tbx.BackColor = System.Drawing.Color.White;
            this.userPassword_tbx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPassword_tbx.Location = new System.Drawing.Point(248, 262);
            this.userPassword_tbx.Margin = new System.Windows.Forms.Padding(4);
            this.userPassword_tbx.MaximumSize = new System.Drawing.Size(665, 4);
            this.userPassword_tbx.MinimumSize = new System.Drawing.Size(132, 20);
            this.userPassword_tbx.Name = "userPassword_tbx";
            this.userPassword_tbx.Size = new System.Drawing.Size(291, 26);
            this.userPassword_tbx.TabIndex = 28;
            // 
            // useName_tbx
            // 
            this.useName_tbx.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useName_tbx.BackColor = System.Drawing.Color.White;
            this.useName_tbx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useName_tbx.Location = new System.Drawing.Point(248, 163);
            this.useName_tbx.Margin = new System.Windows.Forms.Padding(4);
            this.useName_tbx.MaximumSize = new System.Drawing.Size(665, 4);
            this.useName_tbx.MinimumSize = new System.Drawing.Size(132, 20);
            this.useName_tbx.Name = "useName_tbx";
            this.useName_tbx.Size = new System.Drawing.Size(291, 26);
            this.useName_tbx.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 257);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "User Name:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.panel2.Controls.Add(this.pictureBox_LOGO);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1221, 167);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox_LOGO
            // 
            this.pictureBox_LOGO.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox_LOGO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.pictureBox_LOGO.Image = global::HMS_Software_V1._01.Properties.Resources.HMS_Logo4_100_;
            this.pictureBox_LOGO.Location = new System.Drawing.Point(1053, 27);
            this.pictureBox_LOGO.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_LOGO.Name = "pictureBox_LOGO";
            this.pictureBox_LOGO.Size = new System.Drawing.Size(133, 123);
            this.pictureBox_LOGO.TabIndex = 11;
            this.pictureBox_LOGO.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 44);
            this.label3.TabIndex = 8;
            this.label3.Text = "User Login";
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 569);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1261, 666);
            this.MinimumSize = new System.Drawing.Size(1237, 606);
            this.Name = "UserLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserLogin";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LOGO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboB_selcePosition;
        private System.Windows.Forms.TextBox userPassword_tbx;
        private System.Windows.Forms.TextBox useName_tbx;
        private System.Windows.Forms.Button userLogin_btn;
        private System.Windows.Forms.Label unit_lbl;
        private System.Windows.Forms.ComboBox comboB_selceUnit;
        private System.Windows.Forms.Label warNumber_lbl;
        private System.Windows.Forms.TextBox wardNumber_tbx;
        private System.Windows.Forms.PictureBox pictureBox_LOGO;
    }
}
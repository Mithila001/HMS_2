namespace HMS_Software_V1._01.Reception
{
    partial class Reception_AppontmentRegister
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.RPR_assign_btn = new System.Windows.Forms.Button();
            this.RPR_RegistrationNo_tbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RPR_clinicName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RPR_assign_btn);
            this.panel1.Controls.Add(this.RPR_RegistrationNo_tbx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.RPR_clinicName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 283);
            this.panel1.TabIndex = 0;
            // 
            // RPR_assign_btn
            // 
            this.RPR_assign_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RPR_assign_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(46)))), ((int)(((byte)(219)))));
            this.RPR_assign_btn.FlatAppearance.BorderSize = 0;
            this.RPR_assign_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.RPR_assign_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.RPR_assign_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RPR_assign_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPR_assign_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.RPR_assign_btn.Location = new System.Drawing.Point(265, 196);
            this.RPR_assign_btn.Name = "RPR_assign_btn";
            this.RPR_assign_btn.Size = new System.Drawing.Size(178, 52);
            this.RPR_assign_btn.TabIndex = 54;
            this.RPR_assign_btn.Text = "Assign";
            this.RPR_assign_btn.UseVisualStyleBackColor = false;
            this.RPR_assign_btn.Click += new System.EventHandler(this.RPR_assign_btn_Click);
            // 
            // RPR_RegistrationNo_tbx
            // 
            this.RPR_RegistrationNo_tbx.Font = new System.Drawing.Font("Arial", 15.75F);
            this.RPR_RegistrationNo_tbx.Location = new System.Drawing.Point(241, 118);
            this.RPR_RegistrationNo_tbx.Name = "RPR_RegistrationNo_tbx";
            this.RPR_RegistrationNo_tbx.Size = new System.Drawing.Size(231, 32);
            this.RPR_RegistrationNo_tbx.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Registration No:";
            // 
            // RPR_clinicName
            // 
            this.RPR_clinicName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RPR_clinicName.AutoSize = true;
            this.RPR_clinicName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPR_clinicName.Location = new System.Drawing.Point(48, 23);
            this.RPR_clinicName.Name = "RPR_clinicName";
            this.RPR_clinicName.Size = new System.Drawing.Size(140, 29);
            this.RPR_clinicName.TabIndex = 2;
            this.RPR_clinicName.Text = "Clinic Type";
            // 
            // Reception_AppontmentRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 283);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(637, 322);
            this.MinimumSize = new System.Drawing.Size(637, 322);
            this.Name = "Reception_AppontmentRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reception_AppontmentRegister";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RPR_clinicName;
        private System.Windows.Forms.TextBox RPR_RegistrationNo_tbx;
        private System.Windows.Forms.Button RPR_assign_btn;
    }
}
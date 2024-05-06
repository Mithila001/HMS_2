namespace HMS_Software_V1._01.Doctor_OPD.UserControls
{
    partial class DoctorCheck_showAddedClinics
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DOPDA_ClincType_lbl = new System.Windows.Forms.Label();
            this.DOPDA_delete_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.DOPDA_delete_btn);
            this.panel1.Controls.Add(this.DOPDA_ClincType_lbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 79);
            this.panel1.TabIndex = 1;
            // 
            // DOPDA_ClincType_lbl
            // 
            this.DOPDA_ClincType_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DOPDA_ClincType_lbl.AutoSize = true;
            this.DOPDA_ClincType_lbl.BackColor = System.Drawing.Color.White;
            this.DOPDA_ClincType_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DOPDA_ClincType_lbl.Location = new System.Drawing.Point(41, 27);
            this.DOPDA_ClincType_lbl.Name = "DOPDA_ClincType_lbl";
            this.DOPDA_ClincType_lbl.Size = new System.Drawing.Size(108, 22);
            this.DOPDA_ClincType_lbl.TabIndex = 3;
            this.DOPDA_ClincType_lbl.Text = "Clinc Type";
            // 
            // DOPDA_delete_btn
            // 
            this.DOPDA_delete_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DOPDA_delete_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.DOPDA_delete_btn.FlatAppearance.BorderSize = 0;
            this.DOPDA_delete_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.DOPDA_delete_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.DOPDA_delete_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DOPDA_delete_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DOPDA_delete_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.DOPDA_delete_btn.Image = global::HMS_Software_V1._01.Properties.Resources.Delete_icon33;
            this.DOPDA_delete_btn.Location = new System.Drawing.Point(396, 9);
            this.DOPDA_delete_btn.Name = "DOPDA_delete_btn";
            this.DOPDA_delete_btn.Size = new System.Drawing.Size(60, 60);
            this.DOPDA_delete_btn.TabIndex = 55;
            this.DOPDA_delete_btn.UseVisualStyleBackColor = false;
            // 
            // DoctorOPD_showAddedClinics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DoctorOPD_showAddedClinics";
            this.Size = new System.Drawing.Size(493, 79);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label DOPDA_ClincType_lbl;
        public System.Windows.Forms.Button DOPDA_delete_btn;
    }
}

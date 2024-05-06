namespace HMS_Software_V1._01.Reception.UserControls
{
    partial class Recep_PatientDischarge
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
            this.RPD_patienName_lbl = new System.Windows.Forms.Label();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.RPD_warNo_lbl = new System.Windows.Forms.Label();
            this.RPD_registrationNo_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.materialCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RPD_patienName_lbl
            // 
            this.RPD_patienName_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RPD_patienName_lbl.AutoSize = true;
            this.RPD_patienName_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPD_patienName_lbl.Location = new System.Drawing.Point(13, 14);
            this.RPD_patienName_lbl.Name = "RPD_patienName_lbl";
            this.RPD_patienName_lbl.Size = new System.Drawing.Size(131, 22);
            this.RPD_patienName_lbl.TabIndex = 2;
            this.RPD_patienName_lbl.Text = "Patient Name";
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.label1);
            this.materialCard1.Controls.Add(this.RPD_warNo_lbl);
            this.materialCard1.Controls.Add(this.RPD_registrationNo_lbl);
            this.materialCard1.Controls.Add(this.RPD_patienName_lbl);
            this.materialCard1.Depth = 0;
            this.materialCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(0, 0);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(376, 98);
            this.materialCard1.TabIndex = 1;
            // 
            // RPD_warNo_lbl
            // 
            this.RPD_warNo_lbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RPD_warNo_lbl.AutoSize = true;
            this.RPD_warNo_lbl.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPD_warNo_lbl.Location = new System.Drawing.Point(316, 36);
            this.RPD_warNo_lbl.Name = "RPD_warNo_lbl";
            this.RPD_warNo_lbl.Size = new System.Drawing.Size(39, 29);
            this.RPD_warNo_lbl.TabIndex = 7;
            this.RPD_warNo_lbl.Text = "10";
            // 
            // RPD_registrationNo_lbl
            // 
            this.RPD_registrationNo_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RPD_registrationNo_lbl.AutoSize = true;
            this.RPD_registrationNo_lbl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPD_registrationNo_lbl.Location = new System.Drawing.Point(14, 55);
            this.RPD_registrationNo_lbl.Name = "RPD_registrationNo_lbl";
            this.RPD_registrationNo_lbl.Size = new System.Drawing.Size(122, 19);
            this.RPD_registrationNo_lbl.TabIndex = 3;
            this.RPD_registrationNo_lbl.Text = "Registration ID";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ward No:";
            // 
            // Recep_PatientDischarge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialCard1);
            this.MinimumSize = new System.Drawing.Size(376, 98);
            this.Name = "Recep_PatientDischarge";
            this.Size = new System.Drawing.Size(376, 98);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label RPD_patienName_lbl;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        public System.Windows.Forms.Label RPD_warNo_lbl;
        public System.Windows.Forms.Label RPD_registrationNo_lbl;
        public System.Windows.Forms.Label label1;
    }
}

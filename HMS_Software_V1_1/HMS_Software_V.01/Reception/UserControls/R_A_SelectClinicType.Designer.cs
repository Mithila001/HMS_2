namespace HMS_Software_V1._01.Reception.UserControls
{
    partial class R_A_SelectClinicType
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
            this.RDTC_availability = new System.Windows.Forms.Label();
            this.RASCT_ClincType_lbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.RDTC_availability);
            this.panel1.Controls.Add(this.RASCT_ClincType_lbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 98);
            this.panel1.TabIndex = 0;
            // 
            // RDTC_availability
            // 
            this.RDTC_availability.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RDTC_availability.AutoSize = true;
            this.RDTC_availability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RDTC_availability.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDTC_availability.Location = new System.Drawing.Point(324, 35);
            this.RDTC_availability.Name = "RDTC_availability";
            this.RDTC_availability.Padding = new System.Windows.Forms.Padding(5);
            this.RDTC_availability.Size = new System.Drawing.Size(87, 29);
            this.RDTC_availability.TabIndex = 8;
            this.RDTC_availability.Text = "Available";
            // 
            // RASCT_ClincType_lbl
            // 
            this.RASCT_ClincType_lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RASCT_ClincType_lbl.AutoSize = true;
            this.RASCT_ClincType_lbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RASCT_ClincType_lbl.Location = new System.Drawing.Point(26, 39);
            this.RASCT_ClincType_lbl.Name = "RASCT_ClincType_lbl";
            this.RASCT_ClincType_lbl.Size = new System.Drawing.Size(108, 22);
            this.RASCT_ClincType_lbl.TabIndex = 7;
            this.RASCT_ClincType_lbl.Text = "Clinc Type";
            // 
            // R_A_SelectClinicType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(437, 98);
            this.Name = "R_A_SelectClinicType";
            this.Size = new System.Drawing.Size(437, 98);
            this.Click += new System.EventHandler(this.R_A_SelectClinicType_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label RDTC_availability;
        public System.Windows.Forms.Label RASCT_ClincType_lbl;
    }
}

namespace HMS_Software_V1._01.Reception
{
    partial class Reception_PatientSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_R_PatS_top = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_R_PatS_mid = new System.Windows.Forms.Panel();
            this.R_PaSearch_combobox = new System.Windows.Forms.ComboBox();
            this.R_PaSearch_tbx = new System.Windows.Forms.TextBox();
            this.panel_R_PatS_bot = new System.Windows.Forms.Panel();
            this.dataGridView_R_PaSearch = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_R_PatS_top.SuspendLayout();
            this.panel_R_PatS_mid.SuspendLayout();
            this.panel_R_PatS_bot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_R_PaSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_R_PatS_top, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_R_PatS_mid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_R_PatS_bot, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1344, 661);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_R_PatS_top
            // 
            this.panel_R_PatS_top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.panel_R_PatS_top.Controls.Add(this.label1);
            this.panel_R_PatS_top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_R_PatS_top.Location = new System.Drawing.Point(4, 4);
            this.panel_R_PatS_top.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_R_PatS_top.Name = "panel_R_PatS_top";
            this.panel_R_PatS_top.Size = new System.Drawing.Size(1336, 78);
            this.panel_R_PatS_top.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Patient";
            // 
            // panel_R_PatS_mid
            // 
            this.panel_R_PatS_mid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.panel_R_PatS_mid.Controls.Add(this.R_PaSearch_combobox);
            this.panel_R_PatS_mid.Controls.Add(this.R_PaSearch_tbx);
            this.panel_R_PatS_mid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_R_PatS_mid.Location = new System.Drawing.Point(4, 90);
            this.panel_R_PatS_mid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_R_PatS_mid.Name = "panel_R_PatS_mid";
            this.panel_R_PatS_mid.Size = new System.Drawing.Size(1336, 70);
            this.panel_R_PatS_mid.TabIndex = 1;
            // 
            // R_PaSearch_combobox
            // 
            this.R_PaSearch_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.R_PaSearch_combobox.BackColor = System.Drawing.Color.White;
            this.R_PaSearch_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.R_PaSearch_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.R_PaSearch_combobox.ForeColor = System.Drawing.Color.Black;
            this.R_PaSearch_combobox.FormattingEnabled = true;
            this.R_PaSearch_combobox.Items.AddRange(new object[] {
            "By ID",
            "By NIC"});
            this.R_PaSearch_combobox.Location = new System.Drawing.Point(1124, 22);
            this.R_PaSearch_combobox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.R_PaSearch_combobox.Name = "R_PaSearch_combobox";
            this.R_PaSearch_combobox.Size = new System.Drawing.Size(160, 28);
            this.R_PaSearch_combobox.TabIndex = 49;
            // 
            // R_PaSearch_tbx
            // 
            this.R_PaSearch_tbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.R_PaSearch_tbx.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.R_PaSearch_tbx.Location = new System.Drawing.Point(780, 21);
            this.R_PaSearch_tbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.R_PaSearch_tbx.MaximumSize = new System.Drawing.Size(665, 50);
            this.R_PaSearch_tbx.MinimumSize = new System.Drawing.Size(320, 20);
            this.R_PaSearch_tbx.Name = "R_PaSearch_tbx";
            this.R_PaSearch_tbx.Size = new System.Drawing.Size(335, 29);
            this.R_PaSearch_tbx.TabIndex = 48;
            this.R_PaSearch_tbx.TextChanged += new System.EventHandler(this.R_PaSearch_tbx_TextChanged);
            // 
            // panel_R_PatS_bot
            // 
            this.panel_R_PatS_bot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(249)))));
            this.panel_R_PatS_bot.Controls.Add(this.dataGridView_R_PaSearch);
            this.panel_R_PatS_bot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_R_PatS_bot.Location = new System.Drawing.Point(4, 168);
            this.panel_R_PatS_bot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_R_PatS_bot.Name = "panel_R_PatS_bot";
            this.panel_R_PatS_bot.Size = new System.Drawing.Size(1336, 489);
            this.panel_R_PatS_bot.TabIndex = 2;
            // 
            // dataGridView_R_PaSearch
            // 
            this.dataGridView_R_PaSearch.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_R_PaSearch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_R_PaSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_R_PaSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_R_PaSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_R_PaSearch.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_R_PaSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_R_PaSearch.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_R_PaSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView_R_PaSearch.Name = "dataGridView_R_PaSearch";
            this.dataGridView_R_PaSearch.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_R_PaSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_R_PaSearch.RowHeadersWidth = 51;
            this.dataGridView_R_PaSearch.Size = new System.Drawing.Size(1336, 489);
            this.dataGridView_R_PaSearch.TabIndex = 0;
            // 
            // Reception_PatientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1359, 698);
            this.Name = "Reception_PatientSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reception_PatientSearch";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Reception_PatientSearch_FormClosed);
            this.Load += new System.EventHandler(this.Reception_PatientSearch_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_R_PatS_top.ResumeLayout(false);
            this.panel_R_PatS_top.PerformLayout();
            this.panel_R_PatS_mid.ResumeLayout(false);
            this.panel_R_PatS_mid.PerformLayout();
            this.panel_R_PatS_bot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_R_PaSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_R_PatS_top;
        private System.Windows.Forms.Panel panel_R_PatS_mid;
        private System.Windows.Forms.Panel panel_R_PatS_bot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox R_PaSearch_combobox;
        private System.Windows.Forms.TextBox R_PaSearch_tbx;
        private System.Windows.Forms.DataGridView dataGridView_R_PaSearch;
    }
}
namespace Rahman_Educational_Service
{
    partial class frmFilterTransactions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilterTransactions));
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.rbStockOut = new System.Windows.Forms.RadioButton();
            this.rbSchoolSale = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.find = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.rbDistributor = new System.Windows.Forms.RadioButton();
            this.rbStockIn = new System.Windows.Forms.RadioButton();
            this.rbCounterSale = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDebtInS = new System.Windows.Forms.RadioButton();
            this.rbDebtIn = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MMM-dd-yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(184, 17);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(138, 25);
            this.dtpStartDate.TabIndex = 3;
            // 
            // rbStockOut
            // 
            this.rbStockOut.AutoSize = true;
            this.rbStockOut.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStockOut.Location = new System.Drawing.Point(180, 195);
            this.rbStockOut.Name = "rbStockOut";
            this.rbStockOut.Size = new System.Drawing.Size(92, 22);
            this.rbStockOut.TabIndex = 9;
            this.rbStockOut.TabStop = true;
            this.rbStockOut.Text = "StockOut";
            this.rbStockOut.UseVisualStyleBackColor = true;
            // 
            // rbSchoolSale
            // 
            this.rbSchoolSale.AutoSize = true;
            this.rbSchoolSale.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSchoolSale.Location = new System.Drawing.Point(333, 153);
            this.rbSchoolSale.Name = "rbSchoolSale";
            this.rbSchoolSale.Size = new System.Drawing.Size(107, 22);
            this.rbSchoolSale.TabIndex = 7;
            this.rbSchoolSale.TabStop = true;
            this.rbSchoolSale.Text = "School Sale";
            this.rbSchoolSale.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(72, 23);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(83, 17);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Start Date :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 69);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Constantia", 10F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(328, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 42);
            this.button4.TabIndex = 16;
            this.button4.Text = "&Cancel";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Constantia", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(207, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 42);
            this.button3.TabIndex = 15;
            this.button3.Text = "&Load Report";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // find
            // 
            this.find.Font = new System.Drawing.Font("Consolas", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.find.Location = new System.Drawing.Point(184, 106);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(100, 25);
            this.find.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Search :";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(9, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(208, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Transactions Report";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(468, 77);
            this.panel2.TabIndex = 56;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MMM-dd-yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(184, 56);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(138, 25);
            this.dtpEndDate.TabIndex = 4;
            // 
            // rbDistributor
            // 
            this.rbDistributor.AutoSize = true;
            this.rbDistributor.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDistributor.Location = new System.Drawing.Point(180, 153);
            this.rbDistributor.Name = "rbDistributor";
            this.rbDistributor.Size = new System.Drawing.Size(108, 22);
            this.rbDistributor.TabIndex = 6;
            this.rbDistributor.TabStop = true;
            this.rbDistributor.Text = "Distributor";
            this.rbDistributor.UseVisualStyleBackColor = true;
            // 
            // rbStockIn
            // 
            this.rbStockIn.AutoSize = true;
            this.rbStockIn.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStockIn.Location = new System.Drawing.Point(27, 195);
            this.rbStockIn.Name = "rbStockIn";
            this.rbStockIn.Size = new System.Drawing.Size(81, 22);
            this.rbStockIn.TabIndex = 5;
            this.rbStockIn.TabStop = true;
            this.rbStockIn.Text = "StockIn";
            this.rbStockIn.UseVisualStyleBackColor = true;
            // 
            // rbCounterSale
            // 
            this.rbCounterSale.AutoSize = true;
            this.rbCounterSale.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCounterSale.Location = new System.Drawing.Point(27, 153);
            this.rbCounterSale.Name = "rbCounterSale";
            this.rbCounterSale.Size = new System.Drawing.Size(117, 22);
            this.rbCounterSale.TabIndex = 5;
            this.rbCounterSale.TabStop = true;
            this.rbCounterSale.Text = "Counter Sale";
            this.rbCounterSale.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "End Date :";
            // 
            // GroupBox3
            // 
            this.GroupBox3.BackColor = System.Drawing.Color.White;
            this.GroupBox3.Controls.Add(this.rbDebtInS);
            this.GroupBox3.Controls.Add(this.rbDebtIn);
            this.GroupBox3.Controls.Add(this.find);
            this.GroupBox3.Controls.Add(this.label2);
            this.GroupBox3.Controls.Add(this.dtpStartDate);
            this.GroupBox3.Controls.Add(this.rbStockOut);
            this.GroupBox3.Controls.Add(this.rbSchoolSale);
            this.GroupBox3.Controls.Add(this.rbDistributor);
            this.GroupBox3.Controls.Add(this.rbStockIn);
            this.GroupBox3.Controls.Add(this.rbCounterSale);
            this.GroupBox3.Controls.Add(this.dtpEndDate);
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.Controls.Add(this.Label5);
            this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox3.Location = new System.Drawing.Point(0, 77);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(468, 288);
            this.GroupBox3.TabIndex = 55;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Filter By";
            // 
            // rbDebtInS
            // 
            this.rbDebtInS.AutoSize = true;
            this.rbDebtInS.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDebtInS.Location = new System.Drawing.Point(27, 238);
            this.rbDebtInS.Name = "rbDebtInS";
            this.rbDebtInS.Size = new System.Drawing.Size(94, 22);
            this.rbDebtInS.TabIndex = 14;
            this.rbDebtInS.TabStop = true;
            this.rbDebtInS.Text = "DebtIn -S";
            this.rbDebtInS.UseVisualStyleBackColor = true;
            // 
            // rbDebtIn
            // 
            this.rbDebtIn.AutoSize = true;
            this.rbDebtIn.Font = new System.Drawing.Font("Constantia", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDebtIn.Location = new System.Drawing.Point(333, 195);
            this.rbDebtIn.Name = "rbDebtIn";
            this.rbDebtIn.Size = new System.Drawing.Size(94, 22);
            this.rbDebtIn.TabIndex = 13;
            this.rbDebtIn.TabStop = true;
            this.rbDebtIn.Text = "DebtIn-D";
            this.rbDebtIn.UseVisualStyleBackColor = true;
            // 
            // frmFilterTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button4;
            this.ClientSize = new System.Drawing.Size(468, 434);
            this.ControlBox = false;
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmFilterTransactions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        internal System.Windows.Forms.RadioButton rbStockOut;
        internal System.Windows.Forms.RadioButton rbSchoolSale;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox find;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        internal System.Windows.Forms.RadioButton rbDistributor;
        internal System.Windows.Forms.RadioButton rbStockIn;
        internal System.Windows.Forms.RadioButton rbCounterSale;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.RadioButton rbDebtIn;
        internal System.Windows.Forms.RadioButton rbDebtInS;
    }
}
namespace Rahman_Educational_Service
{
    partial class rptStockOut
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptStockOut));
            this.AccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rahman_educational_serviceDataSet = new Rahman_Educational_Service.rahman_educational_serviceDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.AccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rahman_educational_serviceDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // AccountBindingSource
            // 
            this.AccountBindingSource.DataMember = "Account";
            this.AccountBindingSource.DataSource = this.rahman_educational_serviceDataSet;
            // 
            // rahman_educational_serviceDataSet
            // 
            this.rahman_educational_serviceDataSet.DataSetName = "rahman_educational_serviceDataSet";
            this.rahman_educational_serviceDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AccountBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Rahman_Educational_Service.rptStockOut.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(719, 475);
            this.reportViewer1.TabIndex = 0;
            // 
            // rptStockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 475);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "rptStockOut";
            this.Text = "Stock Out Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptStockOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rahman_educational_serviceDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AccountBindingSource;
        private rahman_educational_serviceDataSet rahman_educational_serviceDataSet;
    }
}
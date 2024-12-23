namespace HospitalDatabaseProject
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appointmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicationStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicalRecordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prescriptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem; // Added Billing Menu Item

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appointmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicationStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prescriptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Initialize Billing Menu Item
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 28);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(929, 368);
            this.mainPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.homeToolStripMenuItem,
                this.patientsToolStripMenuItem,
                this.appointmentsToolStripMenuItem,
                this.medicationStockToolStripMenuItem,
                this.reportsToolStripMenuItem,
                this.medicalRecordsToolStripMenuItem,
                this.billingToolStripMenuItem, // Add Billing to MenuStrip
                this.prescriptionsToolStripMenuItem
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(929, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // patientsToolStripMenuItem
            // 
            this.patientsToolStripMenuItem.Name = "patientsToolStripMenuItem";
            this.patientsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.patientsToolStripMenuItem.Text = "Patients";
            // 
            // appointmentsToolStripMenuItem
            // 
            this.appointmentsToolStripMenuItem.Name = "appointmentsToolStripMenuItem";
            this.appointmentsToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.appointmentsToolStripMenuItem.Text = "Appointments";
            // 
            // medicationStockToolStripMenuItem
            // 
            this.medicationStockToolStripMenuItem.Name = "medicationStockToolStripMenuItem";
            this.medicationStockToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.medicationStockToolStripMenuItem.Text = "Medication Stock";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // medicalRecordsToolStripMenuItem
            // 
            this.medicalRecordsToolStripMenuItem.Name = "medicalRecordsToolStripMenuItem";
            this.medicalRecordsToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.medicalRecordsToolStripMenuItem.Text = "Medical Records";
            this.medicalRecordsToolStripMenuItem.Click += new System.EventHandler(this.medicalRecordsToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.billingToolStripMenuItem.Text = "Billing";
            this.billingToolStripMenuItem.Click += new System.EventHandler(this.billingToolStripMenuItem_Click); // Assign Click Event
            // 
            // prescriptionsToolStripMenuItem
            // 
            this.prescriptionsToolStripMenuItem.Name = "prescriptionsToolStripMenuItem";
            this.prescriptionsToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.prescriptionsToolStripMenuItem.Text = "Prescriptions";
            this.prescriptionsToolStripMenuItem.Click += new System.EventHandler(this.prescriptionsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(929, 396);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Hospital Management System";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

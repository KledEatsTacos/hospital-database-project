namespace HospitalDatabaseProject
{
    partial class AddMedicalRecordForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Label lblTreatmentPlan;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.RichTextBox rtbTreatmentPlan;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.lblTreatmentPlan = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.rtbTreatmentPlan = new System.Windows.Forms.RichTextBox();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Location = new System.Drawing.Point(30, 20);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(60, 15);
            this.lblDiagnosis.TabIndex = 1;
            this.lblDiagnosis.Text = "Diagnosis:";
            // 
            // lblTreatmentPlan
            // 
            this.lblTreatmentPlan.AutoSize = true;
            this.lblTreatmentPlan.Location = new System.Drawing.Point(30, 70);
            this.lblTreatmentPlan.Name = "lblTreatmentPlan";
            this.lblTreatmentPlan.Size = new System.Drawing.Size(88, 15);
            this.lblTreatmentPlan.TabIndex = 2;
            this.lblTreatmentPlan.Text = "Treatment Plan:";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(30, 120);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 15);
            this.lblNotes.TabIndex = 3;
            this.lblNotes.Text = "Notes:";
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(150, 17);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(500, 23);
            this.txtDiagnosis.TabIndex = 4;
            // 
            // rtbTreatmentPlan
            // 
            this.rtbTreatmentPlan.Location = new System.Drawing.Point(150, 67);
            this.rtbTreatmentPlan.Name = "rtbTreatmentPlan";
            this.rtbTreatmentPlan.Size = new System.Drawing.Size(500, 50);
            this.rtbTreatmentPlan.TabIndex = 5;
            this.rtbTreatmentPlan.Text = "";
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(150, 117);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(500, 100);
            this.rtbNotes.TabIndex = 6;
            this.rtbNotes.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(494, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(575, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddMedicalRecordForm
            // 
            this.ClientSize = new System.Drawing.Size(700, 300);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.rtbTreatmentPlan);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblTreatmentPlan);
            this.Controls.Add(this.lblDiagnosis);
            this.Name = "AddMedicalRecordForm";
            this.Text = "Add Medical Record";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

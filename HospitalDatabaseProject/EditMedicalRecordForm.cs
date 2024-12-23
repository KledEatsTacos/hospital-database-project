using System;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class EditMedicalRecordForm : Form
    {
        private DatabaseHelper dbHelper;
        private int recordId;
        private int patientId;

        public EditMedicalRecordForm(DatabaseHelper dbHelper, int recordId, int patientId, string diagnosis, string treatmentPlan, string notes)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.recordId = recordId;
            this.patientId = patientId;

            txtDiagnosis.Text = diagnosis;
            rtbTreatmentPlan.Text = treatmentPlan;
            rtbNotes.Text = notes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string diagnosis = txtDiagnosis.Text.Trim();
            string treatmentPlan = rtbTreatmentPlan.Text.Trim();
            string notes = rtbNotes.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(diagnosis))
            {
                MessageBox.Show("Diagnosis is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = dbHelper.UpdateMedicalRecord(recordId, patientId, diagnosis, treatmentPlan, notes);
            if (success)
            {
                MessageBox.Show("Medical record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update medical record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

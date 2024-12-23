using System;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class AddMedicalRecordForm : Form
    {
        private DatabaseHelper dbHelper;
        private int patientId;
        private int appointmentId;

        public AddMedicalRecordForm(DatabaseHelper dbHelper, int patientId, int appointmentId)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.patientId = patientId;
            this.appointmentId = appointmentId;
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

            try
            {
                // Create Medical Record
                bool recordCreated = dbHelper.CreateMedicalRecord(patientId, diagnosis, treatmentPlan, notes);

                if (recordCreated)
                {
                    // Assume a standard appointment cost or calculate based on treatments
                    decimal amount = CalculateAppointmentCost();

                    // Get patient's insurance ID if any
                    int? insuranceId = dbHelper.GetPatientInsuranceId(patientId);

                    // Create a billing record
                    bool billingCreated = dbHelper.CreateBilling(appointmentId, insuranceId, amount);

                    if (billingCreated)
                    {
                        MessageBox.Show("Medical record created and billing generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Medical record created but failed to generate billing.", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating medical record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private decimal CalculateAppointmentCost()
        {
            // Placeholder for cost calculation logic
            // You can implement logic based on treatments, procedures, etc.
            return 100.00m; // For example, a flat rate of $100
        }
    }
}

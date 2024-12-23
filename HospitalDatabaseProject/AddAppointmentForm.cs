using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class AddAppointmentForm : Form
    {
        private DatabaseHelper dbHelper;
        private bool isEditMode;
        private int appointmentId;

        // Define controls
        private ComboBox cbPatients;
        private ComboBox cbStaff;
        private DateTimePicker dtpDateTime;
        private TextBox txtReason;
        private Button btnSave;
        private Button btnCancel;

        public AddAppointmentForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            isEditMode = false;
            InitializeControls();
            InitializePatientComboBox();
            InitializeStaffComboBox();
        }

        public AddAppointmentForm(DatabaseHelper dbHelper, int appointmentId)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.appointmentId = appointmentId;
            isEditMode = true;
            InitializeControls();
            InitializePatientComboBox();
            InitializeStaffComboBox();
            LoadAppointmentDetails();
        }

        private void InitializeControls()
        {
            this.Text = isEditMode ? "Edit Appointment" : "Add Appointment";
            this.Size = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Label for Patient
            Label lblPatient = new Label
            {
                Text = "Patient:",
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblPatient);

            // ComboBox for Patients
            cbPatients = new ComboBox
            {
                Location = new Point(150, 25),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cbPatients);

            // Label for Staff (Doctor/Nurse)
            Label lblStaff = new Label
            {
                Text = "Doctor/Nurse:",
                Location = new Point(30, 80),
                AutoSize = true
            };
            this.Controls.Add(lblStaff);

            // ComboBox for Staff
            cbStaff = new ComboBox
            {
                Location = new Point(150, 75),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cbStaff);

            // Label for Date and Time
            Label lblDateTime = new Label
            {
                Text = "Date & Time:",
                Location = new Point(30, 130),
                AutoSize = true
            };
            this.Controls.Add(lblDateTime);

            // DateTimePicker for Appointment Date and Time
            dtpDateTime = new DateTimePicker
            {
                Location = new Point(150, 125),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/dd/yyyy hh:mm tt",
                ShowUpDown = true
            };
            this.Controls.Add(dtpDateTime);

            // Label for Reason
            Label lblReason = new Label
            {
                Text = "Reason:",
                Location = new Point(30, 180),
                AutoSize = true
            };
            this.Controls.Add(lblReason);

            // TextBox for Reason
            txtReason = new TextBox
            {
                Location = new Point(150, 175),
                Width = 200,
                Multiline = true,
                Height = 60
            };
            this.Controls.Add(txtReason);

            // Save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(150, 250),
                Width = 80,
                Height = 30
            };
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(270, 250),
                Width = 80,
                Height = 30
            };
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
        }

        private void InitializePatientComboBox()
        {
            DataTable patients = dbHelper.GetPatientList();
            cbPatients.DataSource = patients;
            cbPatients.DisplayMember = "FullName";
            cbPatients.ValueMember = "PatientID";
        }

        private void InitializeStaffComboBox()
        {
            DataTable staff = dbHelper.GetStaffList();
            cbStaff.DataSource = staff;
            cbStaff.DisplayMember = "FullName";
            cbStaff.ValueMember = "StaffID";
        }

        private void LoadAppointmentDetails()
        {
            DataTable appointmentDetails = dbHelper.GetAppointmentById(appointmentId);
            if (appointmentDetails.Rows.Count > 0)
            {
                DataRow row = appointmentDetails.Rows[0];
                cbPatients.SelectedValue = row["PatientID"];
                cbStaff.SelectedValue = row["StaffID"];
                dtpDateTime.Value = Convert.ToDateTime(row["Date"]);
                txtReason.Text = row["Reason"].ToString();
            }
            else
            {
                MessageBox.Show("Appointment details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbPatients.SelectedValue == null || cbStaff.SelectedValue == null)
            {
                MessageBox.Show("Please select both Patient and Doctor/Nurse.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int patientId = Convert.ToInt32(cbPatients.SelectedValue);
            int staffId = Convert.ToInt32(cbStaff.SelectedValue);
            DateTime dateTime = dtpDateTime.Value;
            string reason = txtReason.Text.Trim();

            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Reason for appointment is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success;
            if (isEditMode)
            {
                success = dbHelper.UpdateAppointment(appointmentId, patientId, staffId, dateTime, reason);
                if (success)
                {
                    MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                success = dbHelper.AddAppointment(patientId, staffId, dateTime, reason);
                if (success)
                {
                    MessageBox.Show("Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            // Error messages are handled within DatabaseHelper
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
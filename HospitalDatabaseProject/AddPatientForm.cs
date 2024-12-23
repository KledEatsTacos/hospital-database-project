using System;
using System.Data;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class AddPatientForm : Form
    {
        private DatabaseHelper dbHelper;
        private bool isEditMode;
        private int patientId;

        public AddPatientForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            InitializeGenderComboBox();
            InitializeInsuranceComboBox();
            isEditMode = false;
        }

        public AddPatientForm(DatabaseHelper dbHelper, int patientId, string firstName, string lastName, DateTime dateOfBirth, char gender, string phoneNumber, string address, string insuranceCompany)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.patientId = patientId;
            InitializeGenderComboBox();
            InitializeInsuranceComboBox();
            PopulateFields(firstName, lastName, dateOfBirth, gender, phoneNumber, address, insuranceCompany);
            isEditMode = true;
            this.Text = "Edit Patient";
        }

        private void InitializeGenderComboBox()
        {
            cbGender.Items.AddRange(new string[] { "M", "F" });
            cbGender.SelectedIndex = 0;
        }

        private void InitializeInsuranceComboBox()
        {
            DataTable insuranceData = dbHelper.GetInsuranceCompanies();

            cbInsuranceCompany.Items.Clear();

            // Add an option for "None"
            cbInsuranceCompany.Items.Add("None");

            foreach (DataRow row in insuranceData.Rows)
            {
                cbInsuranceCompany.Items.Add(row["insurance_company"].ToString());
            }

            cbInsuranceCompany.SelectedIndex = 0; // Default to "None"
        }

        private void PopulateFields(string firstName, string lastName, DateTime dateOfBirth, char gender, string phoneNumber, string address, string insuranceCompany)
        {
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            dtpDateOfBirth.Value = dateOfBirth;
            cbGender.SelectedItem = gender.ToString();
            txtPhoneNumber.Text = phoneNumber;
            txtAddress.Text = address;
            cbInsuranceCompany.SelectedItem = insuranceCompany ?? "None";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            DateTime dateOfBirth = dtpDateOfBirth.Value.Date;
            char gender = cbGender.SelectedItem.ToString()[0];
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string address = txtAddress.Text.Trim();
            string insuranceCompany = cbInsuranceCompany.SelectedItem.ToString();

            if (insuranceCompany == "None")
                insuranceCompany = null;

            // Validate inputs
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success;
            if (isEditMode)
            {
                success = dbHelper.UpdatePatient(patientId, firstName, lastName, dateOfBirth, gender, phoneNumber, address, insuranceCompany);
                if (success)
                {
                    MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                success = dbHelper.AddPatient(firstName, lastName, dateOfBirth, gender, phoneNumber, address, insuranceCompany);
                if (success)
                {
                    MessageBox.Show("Patient added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            // Error messages are handled within DatabaseHelper
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


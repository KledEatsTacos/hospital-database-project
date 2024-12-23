using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class Form1 : Form
    {
        private DatabaseHelper dbHelper;
        private Panel addPrescriptionPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeAddPrescriptionPanel();
            dbHelper = new DatabaseHelper();

            // Assign event handlers
            homeToolStripMenuItem.Click += homeToolStripMenuItem_Click;
            patientsToolStripMenuItem.Click += patientsToolStripMenuItem_Click;
            appointmentsToolStripMenuItem.Click += appointmentsToolStripMenuItem_Click;
            medicationStockToolStripMenuItem.Click += medicationStockToolStripMenuItem_Click;
            prescriptionsToolStripMenuItem.Click += prescriptionsToolStripMenuItem_Click;
            reportsToolStripMenuItem.Click += reportsToolStripMenuItem_Click;
            medicalRecordsToolStripMenuItem.Click += medicalRecordsToolStripMenuItem_Click;
            // In the constructor after other menu items
            billingToolStripMenuItem.Click += billingToolStripMenuItem_Click;

        }

        private void LoadBillingManagement()
        {
            mainPanel.Controls.Clear();

            // Create the DataGridView for bills
            DataGridView dgvBills = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 250,
                DataSource = dbHelper.GetBills(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            dgvBills.DataBindingComplete += (s, e) =>
            {
                if (dgvBills.Columns.Contains("Bill ID"))
                    dgvBills.Columns["Bill ID"].Visible = false;

                if (dgvBills.Columns.Contains("Appointment ID"))
                    dgvBills.Columns["Appointment ID"].Visible = false;
            };

            // Create buttons panel
            Panel buttonsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50
            };

            Button btnAddPayment = new Button
            {
                Text = "Add Payment",
                Location = new Point(10, 10),
                Width = 150
            };
            btnAddPayment.Click += (s, e) => AddPayment(dgvBills);

            Button btnViewPayments = new Button
            {
                Text = "View Payments",
                Location = new Point(170, 10),
                Width = 150
            };
            btnViewPayments.Click += (s, e) => ViewPayments(dgvBills);

            buttonsPanel.Controls.Add(btnAddPayment);
            buttonsPanel.Controls.Add(btnViewPayments);

            mainPanel.Controls.Add(dgvBills);
            mainPanel.Controls.Add(buttonsPanel);
        }


        private void ViewPayments(DataGridView dgvBills)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                var selectedRow = dgvBills.SelectedRows[0];
                int billId = Convert.ToInt32(selectedRow.Cells["Bill ID"].Value);
                string patientName = selectedRow.Cells["Patient Name"].Value.ToString();

                DataTable payments = dbHelper.GetPaymentsByBillId(billId);

                if (payments.Rows.Count > 0)
                {
                    // Display payments in a new form or a DataGridView
                    Form paymentsForm = new Form
                    {
                        Text = $"Payments for {patientName}",
                        Size = new Size(600, 400),
                        StartPosition = FormStartPosition.CenterParent
                    };

                    DataGridView dgvPayments = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        DataSource = payments,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        MultiSelect = false,
                        AllowUserToAddRows = false
                    };

                    if (dgvPayments.Columns.Contains("Payment ID"))
                        dgvPayments.Columns["Payment ID"].Visible = false;

                    paymentsForm.Controls.Add(dgvPayments);
                    paymentsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No payments found for this bill.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a bill to view payments.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void InitializeAddPrescriptionPanel()
        {
            addPrescriptionPanel = new Panel
            {
                Name = "addPrescriptionPanel",
                Size = new Size(400, 300),
                Location = new Point(10, 10),
                // Add additional properties and controls as needed
            };

            this.Controls.Add(addPrescriptionPanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHome();
        }

        private void LoadHome()
        {
            mainPanel.Controls.Clear();
            Label homeLabel = new Label
            {
                Text = "Welcome to the Hospital Management System",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 24, FontStyle.Bold)
            };
            mainPanel.Controls.Add(homeLabel);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHome();
        }

        #region Patients Management

        private void patientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            // Create a Panel for the search bar
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            // Create TextBoxes for first name and last name
            TextBox txtFirstNameSearch = new TextBox
            {
                PlaceholderText = "First Name",
                Location = new Point(10, 10),
                Width = 150
            };

            TextBox txtLastNameSearch = new TextBox
            {
                PlaceholderText = "Last Name",
                Location = new Point(170, 10),
                Width = 150
            };

            // Create the Search button
            Button btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(330, 8),
                Width = 80,
                Height = 25
            };

            // Add controls to the search panel
            searchPanel.Controls.Add(txtFirstNameSearch);
            searchPanel.Controls.Add(txtLastNameSearch);
            searchPanel.Controls.Add(btnSearch);

            // Create the DataGridView
            DataGridView dgvPatients = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            dgvPatients.DataSource = dbHelper.GetPatients();

            dgvPatients.DataBindingComplete += (s, args) =>
            {
                if (dgvPatients.Columns.Contains("Patient ID"))
                {
                    dgvPatients.Columns["Patient ID"].Visible = false;
                }
            };

            // Attach event handler to the Search button
            btnSearch.Click += (s, args) =>
            {
                string firstName = txtFirstNameSearch.Text.Trim();
                string lastName = txtLastNameSearch.Text.Trim();

                DataTable searchResults = dbHelper.SearchPatients(firstName, lastName);

                dgvPatients.DataSource = searchResults;

                // Ensure the "Patient ID" column remains hidden after search
                if (dgvPatients.Columns.Contains("Patient ID"))
                {
                    dgvPatients.Columns["Patient ID"].Visible = false;
                }
            };

            // Add buttons for patient actions
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnAddPatient = new Button
            {
                Text = "Add Patient",
                Width = 100,
                Height = 30,
                Location = new Point(10, 10)
            };
            btnAddPatient.Click += (s, args) => { AddPatient(); };
            buttonPanel.Controls.Add(btnAddPatient);

            Button btnEditPatient = new Button
            {
                Text = "Edit Patient",
                Width = 100,
                Height = 30,
                Location = new Point(120, 10)
            };
            btnEditPatient.Click += (s, args) => { EditPatient(dgvPatients); };
            buttonPanel.Controls.Add(btnEditPatient);

            Button btnDeletePatient = new Button
            {
                Text = "Delete Patient",
                Width = 100,
                Height = 30,
                Location = new Point(230, 10)
            };
            btnDeletePatient.Click += (s, args) => { DeletePatient(dgvPatients); };
            buttonPanel.Controls.Add(btnDeletePatient);

            // Add controls to mainPanel
            mainPanel.Controls.Add(dgvPatients);
            mainPanel.Controls.Add(buttonPanel);
            mainPanel.Controls.Add(searchPanel);
        }

        private void AddPatient()
        {
            using (AddPatientForm addForm = new AddPatientForm(dbHelper))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Clear search boxes

                    // Refresh the patients DataGridView
                    DataGridView dgvPatients = mainPanel.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dgvPatients != null)
                    {
                        dgvPatients.DataSource = dbHelper.GetPatients();

                        // Ensure the "Patient ID" column remains hidden after refresh
                        if (dgvPatients.Columns.Contains("Patient ID"))
                        {
                            dgvPatients.Columns["Patient ID"].Visible = false;
                        }
                    }
                }
            }
        }

        [SupportedOSPlatform("windows")]
        private void EditPatient(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                // Retrieve selected patient data
                var selectedRow = dgv.SelectedRows[0];
                if (!selectedRow.Cells["Patient ID"].Value.Equals(DBNull.Value))
                {
                    int patientId = Convert.ToInt32(selectedRow.Cells["Patient ID"].Value);
                    string firstName = selectedRow.Cells["First Name"].Value.ToString();
                    string lastName = selectedRow.Cells["Last Name"].Value.ToString();
                    DateTime dateOfBirth = Convert.ToDateTime(selectedRow.Cells["Date of Birth"].Value);
                    char gender = selectedRow.Cells["Gender"].Value.ToString()[0];
                    string phoneNumber = selectedRow.Cells["Phone Number"].Value.ToString();
                    string address = selectedRow.Cells["Address"].Value.ToString();
                    string insuranceCompany = selectedRow.Cells["Insurance Company"].Value.ToString();
                    if (insuranceCompany == "None")
                        insuranceCompany = null;

                    using (AddPatientForm editForm = new AddPatientForm(dbHelper, patientId, firstName, lastName, dateOfBirth, gender, phoneNumber, address, insuranceCompany))
                    {
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            // Clear search boxes

                            // Refresh the patients DataGridView
                            dgv.DataSource = dbHelper.GetPatients();

                            // Ensure the "Patient ID" column remains hidden after refresh
                            if (dgv.Columns.Contains("Patient ID"))
                            {
                                dgv.Columns["Patient ID"].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected patient does not have a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [SupportedOSPlatform("windows")]
        private void DeletePatient(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                if (!selectedRow.Cells["Patient ID"].Value.Equals(DBNull.Value))
                {
                    int patientId = Convert.ToInt32(selectedRow.Cells["Patient ID"].Value);

                    // Confirm deletion
                    var confirmResult = MessageBox.Show("Are you sure to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Logic to delete the selected patient
                        bool success = dbHelper.DeletePatient(patientId);
                        if (success)
                        {
                            MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv.DataSource = dbHelper.GetPatients();

                            // Ensure the "Patient ID" column remains hidden after refresh
                            if (dgv.Columns.Contains("Patient ID"))
                            {
                                dgv.Columns["Patient ID"].Visible = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected patient does not have a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Appointments Management

        private void appointmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            // Create the DataGridView
            DataGridView dgvAppointments = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            dgvAppointments.DataSource = dbHelper.GetAppointments();

            // Add buttons for appointment actions
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnAddAppointment = new Button
            {
                Text = "Add Appointment",
                Width = 120,
                Height = 30,
                Location = new Point(10, 10)
            };
            btnAddAppointment.Click += (s, args) => { AddAppointment(); };
            buttonPanel.Controls.Add(btnAddAppointment);

            Button btnEditAppointment = new Button
            {
                Text = "Edit Appointment",
                Width = 120,
                Height = 30,
                Location = new Point(140, 10)
            };
            btnEditAppointment.Click += (s, args) => { EditAppointment(dgvAppointments); };
            buttonPanel.Controls.Add(btnEditAppointment);

            Button btnDeleteAppointment = new Button
            {
                Text = "Delete Appointment",
                Width = 120,
                Height = 30,
                Location = new Point(270, 10)
            };
            btnDeleteAppointment.Click += (s, args) => { DeleteAppointment(dgvAppointments); };
            buttonPanel.Controls.Add(btnDeleteAppointment);

            // Create Medical Record button
            Button btnCreateMedicalRecord = new Button
            {
                Text = "Create Medical Record",
                Width = 150,
                Height = 30,
                Location = new Point(400, 10)
            };
            btnCreateMedicalRecord.Click += (s, args) => { CreateMedicalRecord(dgvAppointments); };
            buttonPanel.Controls.Add(btnCreateMedicalRecord);

            // Add controls to mainPanel
            mainPanel.Controls.Add(dgvAppointments);
            mainPanel.Controls.Add(buttonPanel);
        }

        private void AddAppointment()
        {
            using (AddAppointmentForm addForm = new AddAppointmentForm(dbHelper))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the appointments DataGridView
                    DataGridView dgvAppointments = mainPanel.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dgvAppointments != null)
                    {
                        dgvAppointments.DataSource = dbHelper.GetAppointments();
                    }
                }
            }
        }

        private void EditAppointment(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["Appointment ID"].Value);

                using (AddAppointmentForm editForm = new AddAppointmentForm(dbHelper, appointmentId))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the appointments DataGridView
                        dgv.DataSource = dbHelper.GetAppointments();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteAppointment(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["Appointment ID"].Value);

                // Confirm deletion
                var confirmResult = MessageBox.Show("Are you sure to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool success = dbHelper.DeleteAppointment(appointmentId);
                    if (success)
                    {
                        MessageBox.Show("Appointment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv.DataSource = dbHelper.GetAppointments();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateMedicalRecord(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["Appointment ID"].Value);
                int patientId = Convert.ToInt32(selectedRow.Cells["Patient ID"].Value);
                string patientName = selectedRow.Cells["Patient Name"].Value.ToString();

                using (AddMedicalRecordForm addRecordForm = new AddMedicalRecordForm(dbHelper, patientId, appointmentId))
                {
                    if (addRecordForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the appointments DataGridView
                        dgv.DataSource = dbHelper.GetAppointments();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to create a medical record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Medical Records Management

        private void medicalRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            // Create the DataGridView for medical records
            DataGridView dgvMedicalRecords = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = dbHelper.GetMedicalRecords(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            dgvMedicalRecords.DataBindingComplete += (s, args) =>
            {
                if (dgvMedicalRecords.Columns.Contains("Record ID"))
                {
                    dgvMedicalRecords.Columns["Record ID"].Visible = false;
                }
                if (dgvMedicalRecords.Columns.Contains("Patient ID"))
                {
                    dgvMedicalRecords.Columns["Patient ID"].Visible = false;
                }
            };

            // Add buttons for medical record actions
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnEditMedicalRecord = new Button
            {
                Text = "Edit Medical Record",
                Width = 150,
                Height = 30,
                Location = new Point(10, 10)
            };
            btnEditMedicalRecord.Click += (s, args) => { EditMedicalRecord(dgvMedicalRecords); };
            buttonPanel.Controls.Add(btnEditMedicalRecord);

            Button btnDeleteMedicalRecord = new Button
            {
                Text = "Delete Medical Record",
                Width = 150,
                Height = 30,
                Location = new Point(170, 10)
            };
            btnDeleteMedicalRecord.Click += (s, args) => { DeleteMedicalRecord(dgvMedicalRecords); };
            buttonPanel.Controls.Add(btnDeleteMedicalRecord);

            // Add controls to mainPanel
            mainPanel.Controls.Add(dgvMedicalRecords);
            mainPanel.Controls.Add(buttonPanel);
        }

        private void EditMedicalRecord(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int recordId = Convert.ToInt32(selectedRow.Cells["Record ID"].Value);
                int patientId = Convert.ToInt32(selectedRow.Cells["Patient ID"].Value);
                string diagnosis = selectedRow.Cells["Diagnosis"].Value.ToString();
                string treatmentPlan = selectedRow.Cells["Treatment Plan"].Value.ToString();
                string notes = selectedRow.Cells["Notes"].Value.ToString();

                using (EditMedicalRecordForm editForm = new EditMedicalRecordForm(dbHelper, recordId, patientId, diagnosis, treatmentPlan, notes))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the medical records DataGridView
                        dgv.DataSource = dbHelper.GetMedicalRecords();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a medical record to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteMedicalRecord(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int recordId = Convert.ToInt32(selectedRow.Cells["Record ID"].Value);

                var confirmResult = MessageBox.Show("Are you sure you want to delete this medical record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool success = dbHelper.DeleteMedicalRecord(recordId);
                    if (success)
                    {
                        MessageBox.Show("Medical record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh the medical records DataGridView
                        dgv.DataSource = dbHelper.GetMedicalRecords();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete medical record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a medical record to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Medication Stock Management

        private void medicationStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            // Create the DataGridView for medication stock
            DataGridView dgvMedications = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = dbHelper.GetMedications(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            dgvMedications.DataBindingComplete += (s, args) =>
            {
                if (dgvMedications.Columns.Contains("Medication ID"))
                {
                    dgvMedications.Columns["Medication ID"].Visible = false;
                }
            };

            // Add buttons for medication stock actions
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnAddStock = new Button
            {
                Text = "Add Stock",
                Width = 100,
                Height = 30,
                Location = new Point(10, 10)
            };
            btnAddStock.Click += (s, args) => { AddMedicationStock(dgvMedications); };
            buttonPanel.Controls.Add(btnAddStock);

            // Add controls to mainPanel
            mainPanel.Controls.Add(dgvMedications);
            mainPanel.Controls.Add(buttonPanel);
        }

        private void AddMedicationStock(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedRow = dgv.SelectedRows[0];
                int medicationId = Convert.ToInt32(selectedRow.Cells["Medication ID"].Value);
                string medicationName = selectedRow.Cells["Name"].Value.ToString();

                using (AddMedicationStockForm addStockForm = new AddMedicationStockForm())
                {
                    addStockForm.Text = $"Add Stock for {medicationName}";
                    if (addStockForm.ShowDialog() == DialogResult.OK)
                    {
                        int quantityToAdd = addStockForm.QuantityToAdd;
                        bool success = dbHelper.UpdateMedicationStock(medicationId, quantityToAdd);

                        if (success)
                        {
                            MessageBox.Show("Medication stock updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Refresh the medications DataGridView
                            dgv.DataSource = dbHelper.GetMedications();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update medication stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a medication to add stock.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region prescriptions Management
        private void prescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPrescriptionManagement();
        }

        private void LoadPrescriptionManagement()
        {
            mainPanel.Controls.Clear();

            // Create the DataGridView for prescriptions
            DataGridView dgvPrescriptions = new DataGridView
            {
                Name = "dgvPrescriptions",
                Dock = DockStyle.Top,
                Height = 250,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };
            dgvPrescriptions.DataSource = dbHelper.GetPrescriptions();

            dgvPrescriptions.DataBindingComplete += (s, args) =>
            {
                if (dgvPrescriptions.Columns.Contains("Prescription ID"))
                {
                    dgvPrescriptions.Columns["Prescription ID"].Visible = false;
                }
            };

            // Create buttons panel
            Panel buttonsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50
            };

            Button btnAddPrescription = new Button
            {
                Text = "Add Prescription",
                Location = new Point(10, 10),
                Width = 150
            };
            btnAddPrescription.Click += (s, e) => AddPrescription();

            Button btnDeletePrescription = new Button
            {
                Text = "Delete Prescription",
                Location = new Point(170, 10),
                Width = 150
            };
            btnDeletePrescription.Click += (s, e) => DeletePrescription(dgvPrescriptions);

            buttonsPanel.Controls.Add(btnAddPrescription);
            buttonsPanel.Controls.Add(btnDeletePrescription);

            // Create the panel for adding new prescriptions
            Panel addPrescriptionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Patient selection
            Label lblPatient = new Label { Text = "Patient:", Location = new Point(10, 10), AutoSize = true };
            ComboBox cbPatients = new ComboBox { Name = "cbPatients", Location = new Point(100, 10), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPatients.DataSource = dbHelper.GetPatientList();
            cbPatients.DisplayMember = "FullName";
            cbPatients.ValueMember = "PatientID";


            // Date Prescribed
            Label lblDatePrescribed = new Label { Text = "Date Prescribed:", Location = new Point(10, 70), AutoSize = true };
            DateTimePicker dtpDatePrescribed = new DateTimePicker { Name = "dtpDatePrescribed", Location = new Point(100, 70), Width = 200, Format = DateTimePickerFormat.Short };
            dtpDatePrescribed.Value = DateTime.Now;

            // Medications section
            GroupBox gbMedications = new GroupBox
            {
                Text = "Medications",
                Location = new Point(10, 100),
                Size = new Size(500, 200)
            };

            // Medications DataGridView
            DataGridView dgvMedications = new DataGridView
            {
                Name = "dgvMedications",
                Location = new Point(10, 20),
                Size = new Size(480, 150),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false
            };

            // Add and Remove Medication buttons
            Button btnAddMedication = new Button
            {
                Text = "Add Medication",
                Location = new Point(10, 175),
                Width = 120
            };
            btnAddMedication.Click += (s, e) => AddMedicationToPrescription(gbMedications, dgvMedications);

            Button btnRemoveMedication = new Button
            {
                Text = "Remove Medication",
                Location = new Point(140, 175),
                Width = 140
            };
            btnRemoveMedication.Click += (s, e) => RemoveMedicationFromPrescription(gbMedications, dgvMedications);

            gbMedications.Controls.Add(dgvMedications);
            gbMedications.Controls.Add(btnAddMedication);
            gbMedications.Controls.Add(btnRemoveMedication);

            // Save Prescription button
            Button btnSavePrescription = new Button
            {
                Text = "Save Prescription",
                Location = new Point(10, 310),
                Width = 150
            };
            btnSavePrescription.Click += (s, e) => SavePrescription(addPrescriptionPanel, dgvPrescriptions);

            // Add controls to addPrescriptionPanel
            addPrescriptionPanel.Controls.Add(lblPatient);
            addPrescriptionPanel.Controls.Add(cbPatients);
            addPrescriptionPanel.Controls.Add(lblDatePrescribed);
            addPrescriptionPanel.Controls.Add(dtpDatePrescribed);
            addPrescriptionPanel.Controls.Add(gbMedications);
            addPrescriptionPanel.Controls.Add(btnSavePrescription);

            // Add controls to mainPanel
            mainPanel.Controls.Add(addPrescriptionPanel);
            mainPanel.Controls.Add(buttonsPanel);
            mainPanel.Controls.Add(dgvPrescriptions);
        }


        private void RemoveMedicationFromPrescription(GroupBox gbMedications, DataGridView dgvMedications)
        {
            if (dgvMedications.SelectedRows.Count > 0)
            {
                DataTable dt = (DataTable)dgvMedications.DataSource;
                dt.Rows.RemoveAt(dgvMedications.SelectedRows[0].Index);
                dgvMedications.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please select a medication to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void AddPrescription()
        {
            // Clear the prescription input fields for a new entry
            ComboBox cbPatients = (ComboBox)addPrescriptionPanel.Controls.Find("cbPatients", true).FirstOrDefault();
            DateTimePicker dtpDatePrescribed = (DateTimePicker)addPrescriptionPanel.Controls.Find("dtpDatePrescribed", true).FirstOrDefault();
            GroupBox gbMedications = addPrescriptionPanel.Controls.OfType<GroupBox>().FirstOrDefault();
            DataGridView dgvMedications = gbMedications?.Controls.OfType<DataGridView>().FirstOrDefault();

            if (cbPatients != null)
            {
                cbPatients.SelectedIndex = -1;
            }

            if (dtpDatePrescribed != null)
            {
                dtpDatePrescribed.Value = DateTime.Now;
            }

            if (dgvMedications != null && dgvMedications.DataSource is DataTable dt)
            {
                dt.Clear();
            }

            MessageBox.Show("Ready to add a new prescription.", "Add Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditPrescription(DataGridView dgvPrescriptions)
        {
            if (dgvPrescriptions.SelectedRows.Count > 0)
            {
                var selectedRow = dgvPrescriptions.SelectedRows[0];
                int prescriptionId = Convert.ToInt32(selectedRow.Cells["Prescription ID"].Value);

                // Retrieve prescription details
                DataTable dtPrescription = dbHelper.GetPrescriptionById(prescriptionId);
                if (dtPrescription.Rows.Count > 0)
                {
                    DataRow row = dtPrescription.Rows[0];
                    ComboBox cbPatients = (ComboBox)addPrescriptionPanel.Controls.Find("cbPatients", true).FirstOrDefault();
                    DateTimePicker dtpDatePrescribed = (DateTimePicker)addPrescriptionPanel.Controls.Find("dtpDatePrescribed", true).FirstOrDefault();
                    GroupBox gbMedications = addPrescriptionPanel.Controls.OfType<GroupBox>().FirstOrDefault();
                    DataGridView dgvMedications = gbMedications?.Controls.OfType<DataGridView>().FirstOrDefault();

                    if (cbPatients != null)
                    {
                        cbPatients.SelectedValue = Convert.ToInt32(row["PatientID"]);
                    }

                    if (dtpDatePrescribed != null)
                    {
                        dtpDatePrescribed.Value = Convert.ToDateTime(row["Date Prescribed"]);
                    }

                    // Load medications for the prescription
                    DataTable dtPrescriptionMedications = dbHelper.GetMedicationsByPrescriptionId(prescriptionId);
                    if (dgvMedications != null && dgvMedications.DataSource is DataTable dt)
                    {
                        dt.Clear();
                        foreach (DataRow medRow in dtPrescriptionMedications.Rows)
                        {
                            dt.Rows.Add(medRow["Medication ID"], medRow["Medication Name"], medRow["Dosage"]);
                        }
                    }

                    MessageBox.Show("Ready to edit the selected prescription.", "Edit Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Selected prescription details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeletePrescription(DataGridView dgvPrescriptions)
        {
            if (dgvPrescriptions.SelectedRows.Count > 0)
            {
                int prescriptionId = Convert.ToInt32(dgvPrescriptions.SelectedRows[0].Cells["Prescription ID"].Value);
                var confirmResult = MessageBox.Show("Are you sure you want to delete this prescription?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool success = dbHelper.DeletePrescription(prescriptionId);
                    if (success)
                    {
                        MessageBox.Show("Prescription deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh DataGridView
                        dgvPrescriptions.DataSource = dbHelper.GetPrescriptions();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Reports

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            Label reportsLabel = new Label
            {
                Text = "Reports Section",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 20, FontStyle.Bold),
                Height = 60
            };

            // Example buttons for different report types
            Button btnPatientReport = new Button
            {
                Text = "Patient Report",
                Width = 150,
                Height = 50,
                Location = new Point(300, 100)
            };
            btnPatientReport.Click += (s, args) => { GenerateReport("Patient"); };
            mainPanel.Controls.Add(btnPatientReport);

            Button btnAppointmentReport = new Button
            {
                Text = "Appointment Report",
                Width = 150,
                Height = 50,
                Location = new Point(300, 170)
            };
            btnAppointmentReport.Click += (s, args) => { GenerateReport("Appointment"); };
            mainPanel.Controls.Add(btnAppointmentReport);

            Button btnMedicalRecordReport = new Button
            {
                Text = "Medical Record Report",
                Width = 150,
                Height = 50,
                Location = new Point(300, 240)
            };
            btnMedicalRecordReport.Click += (s, args) => { GenerateReport("MedicalRecord"); };
            mainPanel.Controls.Add(btnMedicalRecordReport);

            mainPanel.Controls.Add(reportsLabel);
        }

        private void GenerateReport(string reportType)
        {
            // Implement report generation logic based on reportType
            // This could involve exporting data to PDF, Excel, etc.
            MessageBox.Show($"Generating {reportType} Report...", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AddMedicationToPrescription(GroupBox gbMedications, DataGridView dgvMedications)
        {
            // Create a new form to select medication
            using (Form addMedForm = new Form())
            {
                addMedForm.Text = "Add Medication";
                addMedForm.Size = new Size(350, 150);
                addMedForm.StartPosition = FormStartPosition.CenterParent;

                Label lblMedication = new Label { Text = "Medication:", Location = new Point(10, 20), AutoSize = true };
                ComboBox cbMedications = new ComboBox { Location = new Point(100, 20), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
                cbMedications.DataSource = dbHelper.GetMedications();
                cbMedications.DisplayMember = "Name";
                cbMedications.ValueMember = "Medication ID";

                Button btnAdd = new Button { Text = "Add", Location = new Point(100, 60), Width = 100 };
                btnAdd.Click += (s, e) =>
                {
                    if (cbMedications.SelectedValue != null)
                    {
                        int medId = (int)cbMedications.SelectedValue;
                        string medName = cbMedications.Text;

                        // Add to DataGridView
                        DataTable dt;
                        if (dgvMedications.DataSource == null)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("Medication ID", typeof(int));
                            dt.Columns.Add("Medication Name", typeof(string));
                        }
                        else
                        {
                            dt = (DataTable)dgvMedications.DataSource;
                        }

                        // Prevent duplicate entries
                        bool exists = dt.AsEnumerable().Any(row => row.Field<int>("Medication ID") == medId);
                        if (!exists)
                        {
                            dt.Rows.Add(medId, medName);
                            dgvMedications.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Medication already added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        addMedForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a medication.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                addMedForm.Controls.Add(lblMedication);
                addMedForm.Controls.Add(cbMedications);
                addMedForm.Controls.Add(btnAdd);

                addMedForm.ShowDialog();
            }
        }

        private void SavePrescription(Panel addPrescriptionPanel, DataGridView dgvPrescriptions)
        {
            ComboBox cbPatients = (ComboBox)addPrescriptionPanel.Controls.Find("cbPatients", true).FirstOrDefault();
            DateTimePicker dtpDatePrescribed = (DateTimePicker)addPrescriptionPanel.Controls.Find("dtpDatePrescribed", true).FirstOrDefault();
            GroupBox gbMedications = addPrescriptionPanel.Controls.OfType<GroupBox>().FirstOrDefault();
            DataGridView dgvMedications = gbMedications.Controls.OfType<DataGridView>().FirstOrDefault();

            if (cbPatients.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvMedications.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one medication.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int patientId = (int)cbPatients.SelectedValue;
            DateTime datePrescribed = dtpDatePrescribed.Value.Date;

            List<int> medicationIds = new List<int>();
            foreach (DataRow row in ((DataTable)dgvMedications.DataSource).Rows)
            {
                medicationIds.Add(Convert.ToInt32(row["Medication ID"]));
            }

            bool success = dbHelper.AddPrescription(patientId, datePrescribed, medicationIds);
            if (success)
            {
                MessageBox.Show("Prescription added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Refresh DataGridView
                dgvPrescriptions.DataSource = dbHelper.GetPrescriptions();

                // Clear the input fields
                AddPrescription();
            }
        }

        #endregion

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBillingManagement();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        // Example of opening AddPaymentForm with a specific bill ID
        private void AddPayment(DataGridView dgvBills)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                int selectedBillId = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["Bill ID"].Value);
                using (AddPaymentForm paymentForm = new AddPaymentForm(dbHelper, selectedBillId))
                {
                    if (paymentForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh bills DataGridView if necessary
                        dgvBills.DataSource = dbHelper.GetBills();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a bill to add payment.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
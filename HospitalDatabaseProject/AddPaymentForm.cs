using System;
using System.Data; // Added to recognize DataTable and DataRow
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class AddPaymentForm : Form
    {
        private DatabaseHelper dbHelper;
        private int billId; // Assuming you pass the Bill ID to this form

        public AddPaymentForm(DatabaseHelper dbHelper, int billId)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.billId = billId;
            InitializePaymentDetails();
        }

        private void AddPaymentForm_Load(object sender, EventArgs e)
        {
            // Populate Amount Due based on Bill ID
            decimal amountDue = GetAmountDue(billId);
            lblAmountDue.Text = $"Amount Due: ${amountDue}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal amountPaid))
            {
                MessageBox.Show("Please enter a valid amount paid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (amountPaid <= 0)
            {
                MessageBox.Show("Amount paid must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string paymentMethod = cbPaymentMethod.SelectedItem.ToString();
            DateTime paymentDate = dtpPaymentDate.Value;

            // Add payment to the database
            bool success = dbHelper.AddPayment(billId, paymentDate, amountPaid, paymentMethod);
            if (success)
            {
                MessageBox.Show("Payment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Error message is handled within DatabaseHelper
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializePaymentDetails()
        {
            // Initialize Payment Method ComboBox if not already done in Designer
            if (cbPaymentMethod.Items.Count == 0)
            {
                cbPaymentMethod.Items.AddRange(new object[] {
                    "Cash",
                    "Credit Card",
                    "Debit Card",
                    "Check",
                    "Insurance"
                });
                cbPaymentMethod.SelectedIndex = 0; // Set default selection
            }
        }
        private decimal GetAmountDue(int billId)
        {
            return dbHelper.GetBillAmount(billId);
        }

    }
}

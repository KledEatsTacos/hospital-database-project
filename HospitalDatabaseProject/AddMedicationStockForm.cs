using System;
using System.Windows.Forms;

namespace HospitalDatabaseProject
{
    public partial class AddMedicationStockForm : Form
    {
        public int QuantityToAdd { get; private set; }

        public AddMedicationStockForm()
        {
            InitializeComponent();
        }

        private void AddMedicationStockForm_Load(object sender, EventArgs e)
        {
            // Initialize form components if needed
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuantity.Text.Trim(), out int quantity) && quantity > 0)
            {
                QuantityToAdd = quantity;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

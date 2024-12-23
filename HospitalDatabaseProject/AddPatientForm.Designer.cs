namespace HospitalDatabaseProject
{
    partial class AddPatientForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblGender = new System.Windows.Forms.Label();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblInsuranceCompany = new System.Windows.Forms.Label();
            this.cbInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(12, 15);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(76, 20);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(150, 12);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 27);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(12, 50);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(76, 20);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(150, 47);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 27);
            this.txtLastName.TabIndex = 3;
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Location = new System.Drawing.Point(12, 85);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(89, 20);
            this.lblDateOfBirth.TabIndex = 4;
            this.lblDateOfBirth.Text = "Date of Birth:";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Location = new System.Drawing.Point(150, 82);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 27);
            this.dtpDateOfBirth.TabIndex = 5;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(12, 120);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(57, 20);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "Gender:";
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Location = new System.Drawing.Point(150, 117);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(200, 28);
            this.cbGender.TabIndex = 7;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Location = new System.Drawing.Point(12, 155);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(106, 20);
            this.lblPhoneNumber.TabIndex = 8;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(150, 152);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(200, 27);
            this.txtPhoneNumber.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 190);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(64, 20);
            this.lblAddress.TabIndex = 10;
            this.lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(150, 187);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 27);
            this.txtAddress.TabIndex = 11;
            // 
            // lblInsuranceCompany
            // 
            this.lblInsuranceCompany.AutoSize = true;
            this.lblInsuranceCompany.Location = new System.Drawing.Point(12, 225);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new System.Drawing.Size(132, 20);
            this.lblInsuranceCompany.TabIndex = 12;
            this.lblInsuranceCompany.Text = "Insurance Company:";
            // 
            // cbInsuranceCompany
            // 
            this.cbInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInsuranceCompany.FormattingEnabled = true;
            this.cbInsuranceCompany.Location = new System.Drawing.Point(150, 222);
            this.cbInsuranceCompany.Name = "cbInsuranceCompany";
            this.cbInsuranceCompany.Size = new System.Drawing.Size(200, 28);
            this.cbInsuranceCompany.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddPatientForm
            // 
            this.ClientSize = new System.Drawing.Size(370, 320);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbInsuranceCompany);
            this.Controls.Add(this.lblInsuranceCompany);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.lblDateOfBirth);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Name = "AddPatientForm";
            this.Text = "Add Patient";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblInsuranceCompany;
        private System.Windows.Forms.ComboBox cbInsuranceCompany;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

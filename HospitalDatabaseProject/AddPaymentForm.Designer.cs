namespace HospitalDatabaseProject
{
    partial class AddPaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Define controls
        private System.Windows.Forms.Label lblAmountDue;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.Label lblAmountPaid;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.ComboBox cbPaymentMethod;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAmountDue = new System.Windows.Forms.Label();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.cbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAmountDue
            // 
            this.lblAmountDue.AutoSize = true;
            this.lblAmountDue.Location = new System.Drawing.Point(30, 30);
            this.lblAmountDue.Name = "lblAmountDue";
            this.lblAmountDue.Size = new System.Drawing.Size(93, 20);
            this.lblAmountDue.TabIndex = 0;
            this.lblAmountDue.Text = "Amount Due:";
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.AutoSize = true;
            this.lblPaymentDate.Location = new System.Drawing.Point(30, 70);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Size = new System.Drawing.Size(111, 20);
            this.lblPaymentDate.TabIndex = 1;
            this.lblPaymentDate.Text = "Payment Date:";
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.AutoSize = true;
            this.lblAmountPaid.Location = new System.Drawing.Point(30, 110);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(102, 20);
            this.lblAmountPaid.TabIndex = 2;
            this.lblAmountPaid.Text = "Amount Paid:";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(30, 150);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(132, 20);
            this.lblPaymentMethod.TabIndex = 3;
            this.lblPaymentMethod.Text = "Payment Method:";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Location = new System.Drawing.Point(180, 65);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(200, 27);
            this.dtpPaymentDate.TabIndex = 4;
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Location = new System.Drawing.Point(180, 105);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(200, 27);
            this.txtAmountPaid.TabIndex = 5;
            // 
            // cbPaymentMethod
            // 
            this.cbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaymentMethod.FormattingEnabled = true;
            this.cbPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Check",
            "Insurance"});
            this.cbPaymentMethod.Location = new System.Drawing.Point(180, 145);
            this.cbPaymentMethod.Name = "cbPaymentMethod";
            this.cbPaymentMethod.Size = new System.Drawing.Size(200, 28);
            this.cbPaymentMethod.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(180, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(300, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 260);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbPaymentMethod);
            this.Controls.Add(this.txtAmountPaid);
            this.Controls.Add(this.dtpPaymentDate);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.lblAmountPaid);
            this.Controls.Add(this.lblPaymentDate);
            this.Controls.Add(this.lblAmountDue);
            this.Name = "AddPaymentForm";
            this.Text = "Add Payment";
            this.Load += new System.EventHandler(this.AddPaymentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

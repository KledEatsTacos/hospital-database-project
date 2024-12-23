using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Npgsql; // Ensure you have the Npgsql package installed

namespace HospitalDatabaseProject
{
    public class DatabaseHelper
    {
        private string connectionString;

        public bool CreateBilling(int appointmentId, int? insuranceId, decimal amount)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO ""Billing"" (appointment_id, insurance_id, amount, payment_status)
                                 VALUES (@appointment_id, @insurance_id, @amount, @payment_status)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("appointment_id", appointmentId);
                        cmd.Parameters.AddWithValue("insurance_id", insuranceId.HasValue ? (object)insuranceId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("amount", amount);
                        cmd.Parameters.AddWithValue("payment_status", false);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating billing: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetBills()
        {
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT b.bill_id AS ""Bill ID"",
                                        a.appointment_id AS ""Appointment ID"",
                                        p.first_name || ' ' || p.last_name AS ""Patient Name"",
                                        b.amount AS ""Amount Due"",
                                        b.payment_status AS ""Payment Status""
                                 FROM ""Billing"" b
                                 JOIN ""Appointment"" a ON b.appointment_id = a.appointment_id
                                 JOIN ""Patient"" pt ON a.patient_id = pt.patient_id
                                 JOIN ""Person"" p ON pt.person_id = p.person_id";
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching bills: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public bool UpdatePaymentStatus(int billId, bool paymentStatus)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE ""Billing"" SET payment_status = @payment_status WHERE bill_id = @bill_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("payment_status", paymentStatus);
                        cmd.Parameters.AddWithValue("bill_id", billId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment status: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public bool AddPayment(int billId, DateTime paymentDate, decimal amountPaid, string paymentMethod)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO ""Payment"" (bill_id, payment_date, amount_paid, payment_method)
                                 VALUES (@bill_id, @payment_date, @amount_paid, @payment_method)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("bill_id", billId);
                        cmd.Parameters.AddWithValue("payment_date", paymentDate);
                        cmd.Parameters.AddWithValue("amount_paid", amountPaid);
                        cmd.Parameters.AddWithValue("payment_method", paymentMethod);
                        cmd.ExecuteNonQuery();
                    }
                }
                // Update the payment status in Billing table
                return UpdatePaymentStatus(billId, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetPaymentsByBillId(int billId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT payment_id AS ""Payment ID"",
                                        payment_date AS ""Payment Date"",
                                        amount_paid AS ""Amount Paid"",
                                        payment_method AS ""Payment Method""
                                 FROM ""Payment""
                                 WHERE bill_id = @bill_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("bill_id", billId);
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching payments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public DatabaseHelper()
        {
            // Initialize your connection string here
            // Example for PostgreSQL:
            connectionString = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=Hospital;";
        }

        public DataTable GetPatients()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            p.patient_id AS ""Patient ID"",
            pr.first_name AS ""First Name"",
            pr.last_name AS ""Last Name"",
            pr.date_of_birth AS ""Date of Birth"",
            pr.gender AS ""Gender"",
            pr.phone_number AS ""Phone Number"",
            pr.address AS ""Address"",
            COALESCE(i.insurance_company, 'None') AS ""Insurance Company""
        FROM 
            ""Patient"" p
        INNER JOIN 
            ""Person"" pr ON p.person_id = pr.person_id
        LEFT JOIN 
            ""Insurance"" i ON p.insurance_id = i.insurance_id
        ORDER BY 
            pr.last_name, pr.first_name;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Restrict MessageBox to Windows platforms
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving patients: {ex.Message}");
                }
                else
                {
                    // Handle or log the error appropriately for other platforms
                }
            }

            return dt;
        }


        public DataTable GetPatientList()
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT 
                p.patient_id AS ""PatientID"",
                CONCAT(pr.first_name, ' ', pr.last_name) AS ""FullName""
            FROM 
                ""Patient"" p
            INNER JOIN 
                ""Person"" pr ON p.person_id = pr.person_id
            ORDER BY 
                pr.last_name, pr.first_name;
            ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving patient list: {ex.Message}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                // Handle or log the error appropriately for other platforms
            }

            return dt;
        }

        public DataTable GetStaffList()
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT 
                s.staff_id AS ""StaffID"",
                CONCAT(pr.first_name, ' ', pr.last_name) AS ""FullName""
            FROM 
                ""Staff"" s
            INNER JOIN 
                ""Person"" pr ON s.person_id = pr.person_id
            ORDER BY 
                pr.last_name, pr.first_name;
            ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving staff list: {ex.Message}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                // Handle or log the error appropriately for other platforms
            }

            return dt;
        }


        public DataTable GetAppointments()
        {
            DataTable dt = new DataTable();
            string query = @"
    SELECT 
        a.appointment_id AS ""Appointment ID"",
        p.patient_id AS ""Patient ID"",
        CONCAT(pr_patient.first_name, ' ', pr_patient.last_name) AS ""Patient Name"",
        CONCAT(pr_staff.first_name, ' ', pr_staff.last_name) AS ""Staff Name"",
        a.date_time AS ""Date"",
        a.reason AS ""Reason"",
        r.room_id AS ""Room""
    FROM 
        ""Appointment"" a
    INNER JOIN 
        ""Patient"" p ON a.patient_id = p.patient_id
    INNER JOIN 
        ""Person"" pr_patient ON p.person_id = pr_patient.person_id
    INNER JOIN
        ""Staff"" s ON a.staff_id = s.staff_id
    INNER JOIN 
        ""Person"" pr_staff ON s.person_id = pr_staff.person_id
    LEFT JOIN 
        ""Room"" r ON a.room_id = r.room_id
    ORDER BY 
        a.date_time;
    ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving appointments: {ex.Message}");
                }
            }

            return dt;
        }


        public DataTable GetAppointmentById(int appointmentId)
        {
            DataTable dt = new DataTable();
            string query = @"
    SELECT 
        a.appointment_id AS ""Appointment ID"",
        p.patient_id AS ""PatientID"",
        s.staff_id AS ""StaffID"",
        a.date_time AS ""Date"",
        a.reason AS ""Reason""
    FROM 
        ""Appointment"" a
    INNER JOIN 
        ""Patient"" p ON a.patient_id = p.patient_id
    INNER JOIN
        ""Staff"" s ON a.staff_id = s.staff_id
    WHERE
        a.appointment_id = @appointmentId;
    ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving appointment details: {ex.Message}");
                }
            }

            return dt;
        }



        public DataTable GetMedications()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    medication_id AS ""Medication ID"",
                    name AS ""Name"",
                    stock AS ""Stock"",
                    price AS ""Price""
                FROM 
                    ""Medication""
                ORDER BY 
                    medication_id;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                System.Windows.Forms.MessageBox.Show($"Error retrieving medications: {ex.Message}");
            }

            return dt;
        }

        public DataTable GetInsuranceCompanies()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT insurance_id, insurance_company FROM ""Insurance"" ORDER BY insurance_company;";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error retrieving insurance companies: {ex.Message}");
            }

            return dt;
        }

        public bool AddPatient(string firstName, string lastName, DateTime dateOfBirth, char gender, string phoneNumber, string address, string insuranceCompany)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("add_patient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new NpgsqlParameter("p_first_name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = firstName });
                        cmd.Parameters.Add(new NpgsqlParameter("p_last_name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = lastName });
                        cmd.Parameters.Add(new NpgsqlParameter("p_date_of_birth", NpgsqlTypes.NpgsqlDbType.Date) { Value = dateOfBirth.Date });
                        cmd.Parameters.Add(new NpgsqlParameter("p_gender", NpgsqlTypes.NpgsqlDbType.Char) { Value = gender });
                        cmd.Parameters.Add(new NpgsqlParameter("p_phone_number", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = phoneNumber });
                        cmd.Parameters.Add(new NpgsqlParameter("p_address", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = address });
                        cmd.Parameters.Add(new NpgsqlParameter("p_insurance_company", NpgsqlTypes.NpgsqlDbType.Varchar)
                        {
                            Value = (object)insuranceCompany ?? DBNull.Value
                        });

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error adding patient: {ex.Message}");
                return false;
            }
        }

        public bool UpdatePatient(int patientId, string firstName, string lastName, DateTime dateOfBirth, char gender, string phoneNumber, string address, string insuranceCompany)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("update_patient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new NpgsqlParameter("p_patient_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = patientId });
                        cmd.Parameters.Add(new NpgsqlParameter("p_first_name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = firstName });
                        cmd.Parameters.Add(new NpgsqlParameter("p_last_name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = lastName });
                        cmd.Parameters.Add(new NpgsqlParameter("p_date_of_birth", NpgsqlTypes.NpgsqlDbType.Date) { Value = dateOfBirth.Date });
                        cmd.Parameters.Add(new NpgsqlParameter("p_gender", NpgsqlTypes.NpgsqlDbType.Char) { Value = gender });
                        cmd.Parameters.Add(new NpgsqlParameter("p_phone_number", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = phoneNumber });
                        cmd.Parameters.Add(new NpgsqlParameter("p_address", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = address });
                        cmd.Parameters.Add(new NpgsqlParameter("p_insurance_company", NpgsqlTypes.NpgsqlDbType.Varchar)
                        {
                            Value = (object)insuranceCompany ?? DBNull.Value
                        });

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error updating patient: {ex.Message}");
                return false;
            }
        }

        public bool DeletePatient(int patientId)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Begin a transaction
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Retrieve person_id from Patient table
                            int personId;
                            using (var cmd = new NpgsqlCommand("SELECT person_id FROM \"Patient\" WHERE patient_id = @patientId", conn))
                            {
                                cmd.Parameters.AddWithValue("patientId", patientId);
                                personId = (int)cmd.ExecuteScalar();
                            }

                            // Delete from Patient table
                            using (var cmd = new NpgsqlCommand("DELETE FROM \"Patient\" WHERE patient_id = @patientId", conn))
                            {
                                cmd.Parameters.AddWithValue("patientId", patientId);
                                cmd.ExecuteNonQuery();
                            }

                            // Optionally, delete from Person table if no other references exist
                            using (var cmd = new NpgsqlCommand("DELETE FROM \"Person\" WHERE person_id = @personId", conn))
                            {
                                cmd.Parameters.AddWithValue("personId", personId);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error deleting patient: {ex.Message}");
                return false;
            }
        }

        public DataTable SearchPatients(string firstName, string lastName)
        {
            DataTable dt = new DataTable();

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    // Use CommandType.Text with a SELECT statement to call the function
                    string query = "SELECT * FROM search_patients(@p_first_name, @p_last_name);";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("p_first_name", string.IsNullOrEmpty(firstName) ? (object)DBNull.Value : firstName);
                        cmd.Parameters.AddWithValue("p_last_name", string.IsNullOrEmpty(lastName) ? (object)DBNull.Value : lastName);

                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error searching patients: {ex.Message}");
                }
                // Handle or log the error appropriately for other platforms
            }

            return dt;
        }


        public bool UpdateAppointment(int appointmentId, int patientId, int staffId, DateTime dateTime, string reason)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("update_appointment", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_appointment_id", appointmentId);
                        cmd.Parameters.AddWithValue("p_patient_id", patientId);
                        cmd.Parameters.AddWithValue("p_staff_id", staffId);
                        cmd.Parameters.AddWithValue("p_date_time", dateTime);
                        cmd.Parameters.AddWithValue("p_reason", reason);

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error updating appointment: {ex.Message}");
                }
                return false;
            }
        }

        public bool DeleteAppointment(int appointmentId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("delete_appointment", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_appointment_id", NpgsqlTypes.NpgsqlDbType.Integer, appointmentId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool UpdateMedicationStock(int medicationId, int quantityToAdd)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"UPDATE ""Medication"" SET stock = stock + @quantityToAdd WHERE medication_id = @medicationId;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quantityToAdd", quantityToAdd);
                        cmd.Parameters.AddWithValue("@medicationId", medicationId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error updating medication stock: {ex.Message}");
                }
                return false;
            }
        }

        public DataTable GetMedicalRecords()
        {
            DataTable dt = new DataTable();
            string query = @"
      SELECT 
          mr.record_id AS ""Record ID"",
          mr.patient_id AS ""Patient ID"",
          CONCAT(pr.first_name, ' ', pr.last_name) AS ""Patient Name"",
          mr.diagnosis AS ""Diagnosis"",
          mr.treatment_plan AS ""Treatment Plan"",
          mr.notes AS ""Notes""
      FROM 
          ""Medical_Record"" mr
      INNER JOIN 
          ""Patient"" p ON mr.patient_id = p.patient_id
      INNER JOIN 
          ""Person"" pr ON p.person_id = pr.person_id
      ORDER BY 
          mr.record_id DESC;
  ";

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving medical records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public int? GetPatientInsuranceId(int patientId)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT insurance_id FROM ""Patient"" WHERE patient_id = @patient_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("patient_id", patientId);
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching patient insurance: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool CreateMedicalRecord(int patientId, string diagnosis, string treatmentPlan, string notes)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("create_medical_record", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_patient_id", NpgsqlTypes.NpgsqlDbType.Integer, patientId);
                        cmd.Parameters.AddWithValue("p_diagnosis", NpgsqlTypes.NpgsqlDbType.Varchar, diagnosis);
                        cmd.Parameters.AddWithValue("p_treatment_plan", NpgsqlTypes.NpgsqlDbType.Text, treatmentPlan);
                        cmd.Parameters.AddWithValue("p_notes", NpgsqlTypes.NpgsqlDbType.Text, notes);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating medical record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateMedicalRecord(int recordId, int patientId, string diagnosis, string treatmentPlan, string notes)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE ""Medical_Record"" SET diagnosis = @diagnosis, treatment_plan = @treatmentPlan, notes = @notes WHERE record_id = @recordId AND patient_id = @patientId;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                        cmd.Parameters.AddWithValue("@treatmentPlan", treatmentPlan);
                        cmd.Parameters.AddWithValue("@notes", notes);
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.Parameters.AddWithValue("@patientId", patientId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating medical record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool DeleteMedicalRecord(int recordId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"DELETE FROM ""Medical_Record"" WHERE record_id = @recordId;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting medical record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetPrescriptions()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            p.prescription_id AS ""Prescription ID"",
            p.patient_id AS ""Patient ID"",
            pr.first_name || ' ' || pr.last_name AS ""Patient Name"",
            p.date_prescribed AS ""Date Prescribed""
        FROM 
            ""Prescription"" p
        INNER JOIN 
            ""Patient"" pa ON p.patient_id = pa.patient_id
        INNER JOIN 
            ""Person"" pr ON pa.person_id = pr.person_id
        ORDER BY 
            p.date_prescribed DESC;
    ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving prescriptions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        public bool DeletePrescription(int prescriptionId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Delete from Prescribed_Medications
                        string deletePrescribedMedicine = @"
                            DELETE FROM ""Prescribed_Medications"" WHERE prescription_id = @prescription_id;
                        ";
                        using (var cmd = new NpgsqlCommand(deletePrescribedMedicine, conn))
                        {
                            cmd.Parameters.AddWithValue("@prescription_id", prescriptionId);
                            cmd.ExecuteNonQuery();
                        }
                        // Delete from Prescription
                        string deletePrescription = @"
                            DELETE FROM ""Prescription"" WHERE prescription_id = @prescription_id;
                        ";
                        using (var cmd = new NpgsqlCommand(deletePrescription, conn))
                        {
                            cmd.Parameters.AddWithValue("@prescription_id", prescriptionId);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting prescription: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public DataTable GetPrescriptionById(int prescriptionId)
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            p.prescription_id AS ""Prescription ID"",
            p.patient_id AS ""PatientID"",
            p.date_prescribed AS ""Date Prescribed""
        FROM 
            ""Prescription"" p
        WHERE
            p.prescription_id = @prescriptionId;
    ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving prescription details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }



        public bool UpdatePrescription(int prescriptionId, int patientId, DateTime datePrescribed, List<int> medicationIds)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Update Prescription table
                        string updatePrescription = @"
                            UPDATE ""Prescription""
                            SET patient_id = @patient_id, date_prescribed = @date_prescribed
                            WHERE prescription_id = @prescription_id;
                        ";
                        using (var cmd = new NpgsqlCommand(updatePrescription, conn))
                        {
                            cmd.Parameters.AddWithValue("@patient_id", patientId);
                            cmd.Parameters.AddWithValue("@date_prescribed", datePrescribed);
                            cmd.Parameters.AddWithValue("@prescription_id", prescriptionId);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete existing entries in Prescribed_Medications
                        string deletePrescribedMedicine = @"
                            DELETE FROM ""Prescribed_Medications"" WHERE prescription_id = @prescription_id;
                        ";
                        using (var cmd = new NpgsqlCommand(deletePrescribedMedicine, conn))
                        {
                            cmd.Parameters.AddWithValue("@prescription_id", prescriptionId);
                            cmd.ExecuteNonQuery();
                        }

                        // Insert updated medications into Prescribed_Medications
                        string insertPrescribedMedicine = @"
                            INSERT INTO ""Prescribed_Medications"" (prescription_id, medication_id)
                            VALUES (@prescription_id, @medication_id);
                        ";
                        foreach (var medId in medicationIds)
                        {
                            using (var cmd = new NpgsqlCommand(insertPrescribedMedicine, conn))
                            {
                                cmd.Parameters.AddWithValue("@prescription_id", prescriptionId);
                                cmd.Parameters.AddWithValue("@medication_id", medId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error updating prescription: {ex.Message}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return false;
            }
        }
        public DataTable GetMedicationsByPrescriptionId(int prescriptionId)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    m.medication_id AS ""Medication ID"",
                    m.name AS ""Medication Name""
                FROM 
                    ""Prescribed_Medications"" pm
                INNER JOIN 
                    ""Medication"" m ON pm.medication_id = m.medication_id
                WHERE 
                    pm.prescription_id = @prescriptionId;
            ";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (OperatingSystem.IsWindows())
                {
                    System.Windows.Forms.MessageBox.Show($"Error retrieving medications for prescription: {ex.Message}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return dt;
        }
        public decimal GetBillAmount(int billId)
        {
            decimal amountDue = 0m;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT amount FROM ""Billing"" WHERE bill_id = @bill_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("bill_id", billId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            amountDue = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving bill amount: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return amountDue;
        }
        public bool AddAppointment(int patientId, int staffId, DateTime dateTime, string reason)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("add_appointment", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_patient_id", patientId);
                        cmd.Parameters.AddWithValue("p_staff_id", staffId);
                        cmd.Parameters.AddWithValue("p_date_time", dateTime);
                        cmd.Parameters.AddWithValue("p_reason", reason);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool AddPrescription(int patientId, DateTime datePrescribed, List<int> medicationIds)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Insert into Prescription table
                        using (var cmd = new NpgsqlCommand("INSERT INTO \"Prescription\" (patient_id, date_prescribed) VALUES (@patient_id, @date_prescribed) RETURNING prescription_id;", conn))
                        {
                            cmd.Parameters.AddWithValue("patient_id", patientId);
                            cmd.Parameters.AddWithValue("date_prescribed", datePrescribed);
                            int prescriptionId = (int)cmd.ExecuteScalar();

                            // Insert into Prescribed_Medications
                            foreach (var medId in medicationIds)
                            {
                                using (var medCmd = new NpgsqlCommand("INSERT INTO \"Prescribed_Medications\" (prescription_id, medication_id) VALUES (@prescription_id, @medication_id);", conn))
                                {
                                    medCmd.Parameters.AddWithValue("prescription_id", prescriptionId);
                                    medCmd.Parameters.AddWithValue("medication_id", medId);
                                    medCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding prescription: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



    }
}

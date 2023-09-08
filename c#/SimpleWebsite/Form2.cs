using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleWebsite
{
    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=NITRO-5\SQLEXPRESS;Initial Catalog=SimpleWebsite;Integrated Security=True");

        public Form2()
        {
            InitializeComponent();
        }

        private void InsertRegistrationIntoLoginTable(string username, string password ,string email)
        {
            try
            {
                conn.Open();

                string query = @"INSERT INTO Users (username, password ,email)
                         VALUES (@username, @password ,@email)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding registration to login table: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=NITRO-5\SQLEXPRESS;Initial Catalog=SimpleWebsite;Integrated Security=True"))
                    {
                        conn.Open();

                        string query = @"INSERT INTO UserRegistration (FirstName, LastName, DateOfBirth, Age, Gender, PhoneNumber, Email, Address, State, City, Username, Password)
                                 VALUES (@FirstName, @LastName, @DateOfBirth, @Age, @Gender, @PhoneNumber, @Email, @Address, @State, @City, @Username, @Password)";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                        cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(textBox3.Text));
                        cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox6.Text);
                        cmd.Parameters.AddWithValue("@State", textBox7.Text);
                        cmd.Parameters.AddWithValue("@City", textBox8.Text);
                        cmd.Parameters.AddWithValue("@Username", textBox9.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox10.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        InsertRegistrationIntoLoginTable(textBox9.Text, textBox10.Text, textBox5.Text);


                        ClearFields();

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                comboBox1.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Age must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidEmail(textBox5.Text))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!IsValidPhoneNumber(textBox4.Text))
            {
                MessageBox.Show("Invalid phone number format. Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidPassword(textBox10.Text))
            {
                MessageBox.Show("Password must meet the following criteria:\n" +
                                "- At least 8 characters\n" +
                                "- Contains at least one uppercase letter\n" +
                                "- Contains at least one lowercase letter\n" +
                                "- Contains at least one digit", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;

            if (!IsValidPassword(textBox10.Text))
            {
                MessageBox.Show("Password must meet the following criteria:\n" +
                                "- At least 8 characters\n" +
                                "- Contains at least one uppercase letter\n" +
                                "- Contains at least one lowercase letter\n" +
                                "- Contains at least one digit", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUppercase = true;
                else if (char.IsLower(c))
                    hasLowercase = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
            }

            return hasUppercase && hasLowercase && hasDigit;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
           

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c) && c != '-' && c != ' ')
                    return false;
            }

            return true;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
            comboBox1.Items.Add("Other");
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = 0;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Value = DateTime.Today;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }

}

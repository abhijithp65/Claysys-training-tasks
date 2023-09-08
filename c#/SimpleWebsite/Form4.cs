using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimpleWebsite
{
    public partial class Form4 : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=NITRO-5\SQLEXPRESS;Initial Catalog=SimpleWebsite;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
            LoadData();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           comboBox1.Items.Add("Male");
           comboBox1.Items.Add("Female");
           comboBox1.Items.Add("Other");
           comboBox1.SelectedIndex = 0;
        }
        private void LoadData()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM UserRegistration", conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.UserDatabaseConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO UserRegistration (FirstName, LastName, DateOfBirth, Age, Gender, PhoneNumber, Email, Address, State, City, Username, Password) " +
                                     "VALUES (@FirstName, @LastName, @DateOfBirth, @Age, @Gender, @PhoneNumber, @Email, @Address, @State, @City, @Username, @Password)";
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(textBox3.Text));
                cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PhoneNumber",textBox4.Text);
                cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmd.Parameters.AddWithValue("@Address", textBox6.Text);
                cmd.Parameters.AddWithValue("@State", textBox7.Text);
                cmd.Parameters.AddWithValue("@City", textBox8.Text);
                cmd.Parameters.AddWithValue("@Username", textBox9.Text);
                cmd.Parameters.AddWithValue("@Password", textBox10.Text);
                cmd.ExecuteNonQuery();
                connection.Close();

                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.UserDatabaseConnectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE UserRegistration SET " +
                                         "FirstName = @FirstName, " +
                                         "LastName = @LastName, " +
                                         "DateOfBirth = @DateOfBirth, " +
                                         "Age = @Age, " +
                                         "Gender = @Gender, " +
                                         "PhoneNumber = @PhoneNumber, " +
                                         "Email = @Email, " +
                                         "Address = @Address, " +
                                         "State = @State, " +
                                         "City = @City, " +
                                         "Username = @Username, " +
                                         "Password = @Password " +
                                         "WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
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
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(selectedRow.Cells["Id"].Value));

                    cmd.ExecuteNonQuery();
                    connection.Close();

                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Select a single row to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.UserDatabaseConnectionString))
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM UserRegistration WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(selectedRow.Cells["Id"].Value));
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a single row to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Value = DateTime.Today;
           comboBox1.SelectedIndex = -1;
        }
    }
}

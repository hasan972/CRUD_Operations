using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_Operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=RAKIBUL-ERP;Initial Catalog=employeeCurd;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            GetEmployeeRecord();
        }

        private void GetEmployeeRecord()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from EmployeeTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            EmployeeRecordDataGridView.DataSource = dt;
;        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeTb VALUES(@name, @FatherName,@MotherName,@Address,@Mobile)",con);
                cmd.Parameters.AddWithValue("@name", txtEmployeeName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Employee Infi is Successfullu saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetEmployeeRecord();
            }
        }

        private bool Isvalid()
        {
            if (txtEmployeeName.Text == string.Empty)
            {
                MessageBox.Show("STUDENT NAME IS REQUIRE", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFatherName.Text == string.Empty)
            {
                MessageBox.Show("FATHER NAME IS REQUIRE", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMotherName.Text == string.Empty)
            {
                MessageBox.Show("MOTHER NAME IS REQUIRE", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Address  IS REQUIRE", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == string.Empty)
            {
                MessageBox.Show("MOBILE NUMBER IS REQUIRE", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;//now go to the Is valid methode..
        }
    }
}

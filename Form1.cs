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
        public int EmployeeID;
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
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtEmployeeName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Employee Information is Successfully saved in the system", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                GetEmployeeRecord();//view the Employee recird into the text Grid
                ResetFormControl();//Clear the all information into the text from.
            }
        }
        // hare is the insert code...
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

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControl();

        }
        //hare is the reset code...

        private void ResetFormControl()
        {
           EmployeeID = 0;
            txtEmployeeName.Clear();
            txtFatherName.Clear();
            txtMotherName.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtEmployeeName.Focus();//focus the employeeName carsure...
        }

        private void EmployeeRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeID = Convert.ToInt32( EmployeeRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtEmployeeName.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtMotherName.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = EmployeeRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }
        //here is the update code...

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmployeeID > 0)
            
                {
                    SqlCommand cmd = new SqlCommand("UPDATE EmployeeTb SET Name = @name,Fathername = @FatherName,MotherName = @MotherName,Address = @Address,Mobile = @Mobile WHERE EmployeeID = @ID", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", txtEmployeeName.Text);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    cmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@ID", this.EmployeeID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Employee information is updated succussfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetEmployeeRecord();//view the Employee recird into the text Grid
                    ResetFormControl();//Clear the all information into the text from.
            }
            else
            {
                MessageBox.Show("Please select an employee to update his informations", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //hare is the delet code...
        private void button3_Click(object sender, EventArgs e)
        {
            if ( EmployeeID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM EmployeeTb WHERE EmployeeID = @ID", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@ID", this.EmployeeID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Employee in deleted from the system", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);


                GetEmployeeRecord();//view the Employee recird into the text Grid
                ResetFormControl();//Clear the all information into the text from.
            }
            else 
            {
                MessageBox.Show("Please select an employee to Delect", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show("Select the process","Select" ,MessageBoxButtons.OK);
                MessageBox.Show("Plea");
            }

        }
    }
}

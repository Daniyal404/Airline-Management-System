using Oracle.ManagedDataAccess.Client;

namespace Semester_4_Database_Systems_Project
{
    public partial class Register_Account : Form
    {
        public AMS FormInstance;
        public Register_Account()
        {
            InitializeComponent();
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (usertype_box_input.SelectedIndex < 0) { return; }

            //Make Custom ID + 1 from current
            //string userid = userid_input.Text.ToString();

            int userid = -1;
            string username = username_input.Text.ToString();
            string usertype = usertype_box_input.SelectedItem.ToString();
            string email = email_input.Text.ToString();
            string pass = password_input.Text.ToString();
            string phone = phone_input.Text.ToString();
            string cnic = cnic_input.Text.ToString();

            //if(cnic.Length != -1)
            //{
            //}
            if (username == "")
            {
                MessageBox.Show("Error: Username cannot be Empty");
                return;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Error: Recheck your email");
                return;
            }

            try
            {
                FormInstance.conn.Open();
                string Sql = "SELECT COALESCE(MAX(USERID), 0) FROM AMS.USER_TABLE";

                OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    userid = reader.GetInt32(0);
                    userid++;
                }
                else
                {
                    userid = 0;
                }

                FormInstance.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                FormInstance.conn.Close();
                return;
            }


            try
            {
                FormInstance.conn.Open();
                string Sql = string.Format("INSERT INTO AMS.User_Table (UserID, Name, UserType, Email, Password, Phone_Number, CNIC) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', {5}, {6})",
                        userid.ToString(), username, usertype, email, pass, phone, cnic
                   );

                OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
                cmd.ExecuteNonQuery();
                FormInstance.conn.Close();

                MessageBox.Show("Account Created!");
                FormInstance.loginSignupFormInstance = (LoginSignupForm)FormInstance.loadform(new LoginSignupForm());
                FormInstance.loginSignupFormInstance.FormInstance = FormInstance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FormInstance.conn.Close();
            }
        }
    }
}

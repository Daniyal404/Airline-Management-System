using Oracle.ManagedDataAccess.Client;

namespace Semester_4_Database_Systems_Project
{
    public partial class LoginSignupForm : Form
    {

        public AMS FormInstance;
        public LoginSignupForm()
        {
            InitializeComponent();
        }

        private void LoginSignupForm_Load(object sender, EventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            FormInstance.conn.Open();
            string userType = "";
            int userId = -1;
            string selectSql = string.Format("SELECT * FROM AMS.USER_TABLE WHERE NAME = '{0}' AND PASSWORD = '{1}'",
                username_input.Text.ToString(), password_input.Text.ToString());
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                // Execute the SQL command
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Check if any rows were returned
            if (reader.HasRows)
            {
                // Iterate through the rows
                while (reader.Read())
                {
                    // Access data using reader
                    userId = reader.GetInt32(reader.GetOrdinal("USERID"));
                    userType = reader.GetString(reader.GetOrdinal("USERTYPE"));
                    string email = reader.GetString(reader.GetOrdinal("EMAIL"));
                    // Access other columns as needed
                    label1.Text = "Login Success: " + userType;
                }
            }
            else
            {
                label1.Text = "Login Fail!";
            }

            // Close the reader
            reader.Close();
            cmd.Dispose();
            FormInstance.conn.Close();
            if (userType.Equals("ADMIN"))
            {
                FormInstance.userID = userId;
                FormInstance.userName = username_input.Text;
                FormInstance.login_signup_btn.Enabled = false;
                FormInstance.login_signup_btn.Visible = false;

                FormInstance.logout_btn.Enabled = true;
                FormInstance.logout_btn.Visible = true;

                FormInstance.goto_admin_dashboard_btn.Enabled = true;
                FormInstance.goto_admin_dashboard_btn.Visible = true;

                FormInstance.admin_Dashboard = (Admin_Dashboard)FormInstance.loadform(new Admin_Dashboard());
                FormInstance.admin_Dashboard.FormInstance = FormInstance;
            }
            else if (userType.Equals("PASSENGER"))
            {
                FormInstance.userID = userId;
                FormInstance.userName = username_input.Text;
                FormInstance.login_signup_btn.Enabled = false;
                FormInstance.login_signup_btn.Visible = false;

                FormInstance.logout_btn.Enabled = true;
                FormInstance.logout_btn.Visible = true;

                FormInstance.goto_passenger_homepage.Enabled = true;
                FormInstance.goto_passenger_homepage.Visible = true;

                FormInstance.passenger_Dashboard = (Passenger_Dashboard)FormInstance.loadform(new Passenger_Dashboard());
                FormInstance.passenger_Dashboard.FormInstance = FormInstance;
                FormInstance.passenger_Dashboard.label_welcome_passenger.Text =
                    "Welcome back, " + FormInstance.userName;
            }
            else if (userType.Equals("EMPLOYEE"))
            {
                FormInstance.userID = userId;
                FormInstance.userName = username_input.Text;
                FormInstance.login_signup_btn.Enabled = false;
                FormInstance.login_signup_btn.Visible = false;

                FormInstance.logout_btn.Enabled = true;
                FormInstance.logout_btn.Visible = true;

                FormInstance.goto_employee_dashboard.Enabled = true;
                FormInstance.goto_employee_dashboard.Visible = true;

                FormInstance.emploee_Dashboard = (Emploee_Dashboard)FormInstance.loadform(new Emploee_Dashboard());
                FormInstance.emploee_Dashboard.FormInstance = FormInstance;
            }
        }

        private void label_signup_clickhere_Click(object sender, EventArgs e)
        {
            if (FormInstance != null)
            {
                FormInstance.register_AccountFormInstance = (Register_Account)FormInstance.loadform(new Register_Account());
                FormInstance.register_AccountFormInstance.FormInstance = FormInstance;
            }
        }
    }
}

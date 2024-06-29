using Oracle.ManagedDataAccess.Client;

public struct UserData
{
    public int id { get; set; }
    public string name;
    public string password;
    public string usertype;
    public string email;
    public double phone;
    public double cnic;
    public UserData(int ID = -1, string USERTPYE = "", string EMAIL = "",
        string PASSWORD = "", double PHONE = -1, double CNIC = -1, string NAME = "")
    {
        id = ID;
        name = NAME;
        password = PASSWORD;
        usertype = USERTPYE;
        email = EMAIL;
        phone = PHONE;
        cnic = CNIC;
    }
}
public struct FlightData
{
    public int FLIGHTID;
    public string DEPARTURE;
    public string DESTINATION;
    public string DEPARTURE_TIME;
    public string ARRIVAL_TIME;
    public int AIRCRAFTID;
    public string STATUS;
    public FlightData(int f_id = -1, string dep = "", string des = "", string dep_time = "",
        string arr_time = "", int aircraft_id = -1, string status = "")
    {
        FLIGHTID = f_id;
        DEPARTURE = dep;
        DESTINATION = des;
        DEPARTURE_TIME = dep_time;
        ARRIVAL_TIME = arr_time;
        AIRCRAFTID = aircraft_id;
        STATUS = status;
    }
}
public struct Aircraft
{
    public int id;
    public string name;
    public int capacity;
    public Aircraft(int ID = -1, string NAME = "nil", int CAP = 0)
    {
        id = ID;
        name = NAME;
        capacity = CAP;
    }
}

namespace Semester_4_Database_Systems_Project
{
    public partial class AMS : Form
    {
        public OracleConnection conn;

        public LoginSignupForm loginSignupFormInstance;
        public Register_Account register_AccountFormInstance;
        public Admin_Dashboard admin_Dashboard;
        public Passenger_Dashboard passenger_Dashboard;
        public Emploee_Dashboard emploee_Dashboard;

        public int userID = -1;
        public string userName;

        public AMS()
        {
            InitializeComponent();
        }
        private void AMS_Load(object sender, EventArgs e)
        {
            string str = "Data Source=localhost:1521/XE;User Id=dani;Password=123;";
            conn = new OracleConnection(str);
        }
        public Form loadform(Object form)
        {
            if (this.main_panel.Controls.Count > 0)
                this.main_panel.Controls.RemoveAt(0);
            Form f = form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.main_panel.Controls.Add(f);
            this.main_panel.Tag = f;
            f.Show();
            return f;
        }
        /// <SQL>
        public UserData getUserFromID(string id)
        {
            UserData userData;
            conn.Open();
            string selectSql = string.Format("SELECT * FROM AMS.USER_TABLE WHERE USERID = {0}",
                id);
            OracleCommand cmd = new OracleCommand(selectSql, conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                    userData = new UserData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                                        reader.GetInt64(4), reader.GetInt64(5), reader.GetString(6));
                else
                    userData = new UserData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                userData = new UserData();
            }
            conn.Close();

            return userData;
        }
        public FlightData getFlightFromID(string id)
        {
            FlightData flightData;
            conn.Open();
            string selectSql = string.Format("SELECT * FROM AMS.FLIGHT WHERE FLIGHTID = {0}",
                id);
            OracleCommand cmd = new OracleCommand(selectSql, conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int f_id = reader.GetInt32(0);
                    string dept_loc = reader.GetString(1);
                    string arr_loc = reader.GetString(2);
                    string dept_time = reader.GetString(3);
                    string arr_time = reader.GetString(4);
                    int aircraft_id;
                    string flight_status;//flight_status = reader.GetString(6)
                    try
                    { aircraft_id = reader.GetInt32(5); }
                    catch (Exception)
                    { aircraft_id = -1; }
                    try
                    { flight_status = reader.GetString(6); }
                    catch (Exception)
                    { flight_status = ""; }
                    flightData = new FlightData(f_id, dept_loc, arr_loc, dept_time, arr_time,
                        aircraft_id, flight_status);
                }
                else
                    flightData = new FlightData(-1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                flightData = new FlightData(-1);
            }
            conn.Close();

            return flightData;
        }
        public int getFlightPriceFromFlightID(string id)
        {
            int price = 0;

            try
            {
                conn.Open();
                string sql = "SELECT PRICE FROM AMS.FLIGHT_PRICES WHERE FLIGHTID = " + id;
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    price = reader.GetInt32(0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            conn.Close();

            return price;
        }
        public Aircraft getAircraftFromID(string id)
        {
            Aircraft aircraft;

            try
            {
                conn.Open();
                string selectSql = string.Format("SELECT * FROM AMS.AIRCRAFT WHERE AIRCRAFTID = {0}",
                    id);
                OracleCommand cmd = new OracleCommand(selectSql, conn);
                OracleDataReader reader;
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int aid = reader.GetInt32(reader.GetOrdinal("AIRCRAFTID"));
                    string aname = reader.GetString(reader.GetOrdinal("NAME"));
                    int acap = reader.GetInt32(reader.GetOrdinal("CAPACITY"));

                    aircraft = new Aircraft(aid, aname, acap);
                }
                else
                {
                    aircraft = new Aircraft();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                aircraft = new Aircraft();
            }
            conn.Close();

            return aircraft;
        }

        public bool executeNonReturnSQL(string sql) //Mostly for update
        {
            bool status = true;
            conn.Open();
            OracleCommand cmd = new OracleCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                status = false;
            }
            conn.Close();
            return status;
        }
        /// </SQL>
        private void login_signup_btn_Click(object sender, EventArgs e)
        {
            loginSignupFormInstance = (LoginSignupForm)loadform(new LoginSignupForm());
            loginSignupFormInstance.FormInstance = this;
        }

        private void logo_plane_Click(object sender, EventArgs e)
        {
            if (this.main_panel.Controls.Count > 0)
                this.main_panel.Controls.RemoveAt(0);
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            logout_btn.Enabled = false;
            logout_btn.Visible = false;

            login_signup_btn.Enabled = true;
            login_signup_btn.Visible = true;

            goto_admin_dashboard_btn.Enabled = false;
            goto_admin_dashboard_btn.Visible = false;

            goto_passenger_homepage.Enabled = false;
            goto_passenger_homepage.Visible = false;

            goto_employee_dashboard.Enabled = false;
            goto_employee_dashboard.Visible = false;

            userID = -1;

            loginSignupFormInstance = (LoginSignupForm)loadform(new LoginSignupForm());
            loginSignupFormInstance.FormInstance = this;
        }

        private void goto_admin_dashboard_btn_Click(object sender, EventArgs e)
        {
            admin_Dashboard = (Admin_Dashboard)loadform(new Admin_Dashboard());
            admin_Dashboard.FormInstance = this;
        }

        private void goto_passenger_homepage_Click(object sender, EventArgs e)
        {
            passenger_Dashboard = (Passenger_Dashboard)loadform(new Passenger_Dashboard());
            passenger_Dashboard.FormInstance = this;
            passenger_Dashboard.label_welcome_passenger.Text =
                    "Welcome back, " + userName;
        }

        private void goto_employee_dashboard_Click(object sender, EventArgs e)
        {
            emploee_Dashboard = (Emploee_Dashboard)loadform(new Emploee_Dashboard());
            emploee_Dashboard.FormInstance = this;
        }
    }
}

using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Semester_4_Database_Systems_Project
{
    public partial class Admin_Dashboard : Form
    {
        public AMS FormInstance;
        public Admin_Dashboard()
        {
            InitializeComponent();
            setDashboard(true);
            setCheckProfileBox(false);
            setManageEmployeeBox(false);
            setAssignTask(false);
        }

        // Setting GroupBoxes
        private void setDashboard(bool status)
        {
            admin_dashboard_main.Parent = this;

            admin_dashboard_main.Enabled = status;
            admin_dashboard_main.Visible = status;

            admin_dashboard_main.SetBounds(12, 12, 526, 344);
        }
        private void setCheckProfileBox(bool status)
        {
            check_profile_box.Parent = this;

            check_profile_box.Enabled = status;
            check_profile_box.Visible = status;

            check_profile_box.SetBounds(12, 12, 526, 344);
        }
        private void setManageEmployeeBox(bool status)
        {
            manage_employee_box.Parent = this;

            manage_employee_box.Enabled = status;
            manage_employee_box.Visible = status;

            manage_employee_box.SetBounds(12, 12, 526, 344);
        }
        private void setManageFlightSchedule(bool status)
        {
            mng_flight_sche_box.Parent = this;

            mng_flight_sche_box.Enabled = status;
            mng_flight_sche_box.Visible = status;

            mng_flight_sche_box.SetBounds(12, 12, 526, 344);

            //Hide Tab Header
            mng_flight_schedule_tab.Appearance = TabAppearance.FlatButtons;
            mng_flight_schedule_tab.ItemSize = new Size(0, 1);
            mng_flight_schedule_tab.SizeMode = TabSizeMode.Fixed;
            // https://stackoverflow.com/questions/6953487/hide-tab-header-on-c-sharp-tabcontrol
        }
        private void setAssignTask(bool status)
        {
            admin_assign_task_box.Parent = this;

            admin_assign_task_box.Enabled = status;
            admin_assign_task_box.Visible = status;

            admin_assign_task_box.SetBounds(12, 12, 526, 344);
        }
        private void setFeedbackBox(bool status)
        {
            view_feedback_box.Parent = this;

            view_feedback_box.Enabled = status;
            view_feedback_box.Visible = status;

            view_feedback_box.SetBounds(12, 12, 526, 344);
        }

        // End of Settings GroupBoxes

        private void populateEMPGridView()
        {
            FormInstance.conn.Open();
            string selectSql = string.Format("SELECT * FROM AMS.USER_TABLE WHERE USERTYPE = \'EMPLOYEE\'",
                FormInstance.userID);
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataTable.Columns.Remove("Password");
                dataTable.Columns.Remove("Usertype");
                dgv_employee_data.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();
        }
        private void populateFlightGridView()
        {
            FormInstance.conn.Open();
            string selectSql = "SELECT * FROM AMS.FLIGHT";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgv_flight_schedule.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();
        }

        private void a_check_profile_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setCheckProfileBox(true);

            int id = FormInstance.userID;
            string user_id = id.ToString();

            UserData userData = FormInstance.getUserFromID(user_id);

            label_userId.Text = "User ID: " + userData.id.ToString();
            label_username.Text = "Name: " + userData.name;
            label_email.Text = "Email Address: " + userData.email;
            label_cnic.Text = "CNIC: " + userData.cnic.ToString();
            label_phone.Text = "Phone Number: " + userData.phone.ToString();

        }

        private void a_view_flight_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setManageFlightSchedule(true);

            populateFlightGridView();
        }

        private void a_mng_employee_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setManageEmployeeBox(true);

            populateEMPGridView();

            admin_mng_employees_tab.SelectTab("emp_main");

            admin_mng_employees_tab.Appearance = TabAppearance.FlatButtons;
            admin_mng_employees_tab.ItemSize = new Size(0, 1);
            admin_mng_employees_tab.SizeMode = TabSizeMode.Fixed;
        }

        private void mng_emp_fire_btn_Click(object sender, EventArgs e)
        {
            if (mng_emp_Id_input.Text == "")
            {
                MessageBox.Show("ID cannot be Empty");
                return;
            }


            UserData user = FormInstance.getUserFromID(mng_emp_Id_input.Text);
            if (user.id == -1 || user.usertype != "EMPLOYEE")   //Dont Fire Passenger or Admin xD
            { MessageBox.Show("No such Employee ID Found!"); return; }

            FormInstance.conn.Open();
            string Sql = string.Format("DELETE FROM AMS.USER_TABLE WHERE USERID = {0}",
                mng_emp_Id_input.Text);
            OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Deleted User: " + user.name + ", with ID: " + user.id);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();

            populateEMPGridView();
        }

        private void a_mng_flight_sche_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setManageFlightSchedule(true);

            populateFlightGridView();

        }

        private void add_flight_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("add_flight");

            add_flight_confirm_btn.Enabled = true;
            add_flight_aircraft_id_input.Items.Clear();

            FormInstance.conn.Open();
            string selectSql = "SELECT * FROM AMS.AIRCRAFT";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    add_flight_aircraft_id_input.Items.Add(reader.GetInt32(reader.GetOrdinal("AIRCRAFTID")));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();

            if (add_flight_aircraft_id_input.Items.Count == 0)
            {
                add_flight_aircraft_id_input.Items.Insert(0, "No Aircraft Found!");
                add_flight_aircraft_id_input.SelectedIndex = 0;
                add_flight_aircraft_id_input.DisplayMember = "Name";
                add_flight_confirm_btn.Enabled = false;
            }

        }

        private void remove_flight_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("remove_flight");
        }

        private void update_flight_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("update_flight");
            upd_flight_dept_loc_input.Enabled = upd_enable_dept_loc.Checked;
            upd_flight_arr_loc_input.Enabled = upd_enable_arr_loc.Checked;
            upd_dept_day_input.Enabled = upd_enable_dept_time.Checked;
            upd_dept_time_input.Enabled = upd_enable_dept_time.Checked;
            upd_arr_day_input.Enabled = upd_enable_arr_time.Checked;
            upd_arr_time_input.Enabled = upd_enable_arr_time.Checked;
            upd_arrcraft_id_input.Enabled = upd_enable_aircraft_id.Checked;
            upd_flight_status_input.Enabled = upd_enable_flight_status.Checked;

            upd_arrcraft_id_input.Items.Clear();

            FormInstance.conn.Open();
            string selectSql = "SELECT * FROM AMS.AIRCRAFT";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    upd_arrcraft_id_input.Items.Add(reader.GetInt32(reader.GetOrdinal("AIRCRAFTID")));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();
        }

        private void add_flight_back_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("main");
            populateFlightGridView();
        }

        private void remove_flight_back_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("main");
            populateFlightGridView();
        }

        private void update_flight_back_btn_Click(object sender, EventArgs e)
        {
            mng_flight_schedule_tab.SelectTab("main");
            populateFlightGridView();
        }

        private void add_flight_confirm_btn_Click(object sender, EventArgs e)
        {
            bool error = false;
            string error_msg = "";

            if (add_flight_status_input.SelectedIndex < 0) { error_msg = "No Status Selected!"; error = true; }
            if (add_flight_aircraft_id_input.SelectedIndex < 0) { error_msg = "No Aircraft ID Selected!"; error = true; }
            if (add_flight_arr_loc_input.Text == "") { error_msg = "No Arrival Location Entered!"; error = true; }
            if (add_flight_dep_loc_input.Text == "") { error_msg = "No Departure Location Entered!"; error = true; }
            if (add_flight_id_input.Text == "") { error_msg = "No Flight ID Entered!"; error = true; }

            if (error) { MessageBox.Show(error_msg); return; }

            DateTime dept_day = add_flight_dept_day_input.Value;
            DateTime dept_time = add_flight_dept_time_input.Value;
            DateTime arr_day = add_flight_arr_day_input.Value;
            DateTime arr_time = add_flight_arr_time_input.Value;
            string flight_id = add_flight_id_input.Text;
            string dept_loc = add_flight_dep_loc_input.Text;
            string arr_loc = add_flight_arr_loc_input.Text;
            string dept_day_str = dept_day.Month + "/" + dept_day.Day + "/" + dept_day.Year;
            string dept_time_str = dept_time.Hour + ":" + dept_time.Minute + ":" + dept_time.Second;
            string arr_day_str = arr_day.Month + "/" + arr_day.Day + "/" + arr_day.Year;
            string arr_time_str = arr_time.Hour + ":" + arr_time.Minute + ":" + arr_time.Second;
            string aircraft_id = add_flight_aircraft_id_input.SelectedItem.ToString();
            string status = add_flight_status_input.SelectedItem.ToString();



            //INSERT INTO AMS.FLIGHT(FLIGHTID,DEPARTURE,DESTINATION,DEPARTURE_TIME,ARRIVAL_TIME,AIRCRAFTID,STATUS) 
            //VALUES('{0}', '{1}', '{2}', TO_TIMESTAMP('{3}', 'MM/DD/YYYY HH:MI:SS AM'), TO_TIMESTAMP('{4}', 'MM/DD/YYYY HH:MI:SS AM'), '{5}', '{6}');

            if (aircraft_id == "")
            {
                MessageBox.Show("<Error> No Aircraft Selected.");
            }
            if (status == "")
            {
                MessageBox.Show("<Error> No Status Selected.");
            }

            FormInstance.conn.Open();
            string Sql =
                string.Format("INSERT INTO AMS.FLIGHT(FLIGHTID,DEPARTURE,DESTINATION,DEPARTURE_TIME,ARRIVAL_TIME,AIRCRAFTID,STATUS) " +
                "VALUES('{0}', '{1}', '{2}', TO_TIMESTAMP('{3}', 'MM/DD/YYYY HH24:MI:SS'), TO_TIMESTAMP('{4}', 'MM/DD/YYYY HH24:MI:SS'), '{5}', '{6}')",
                flight_id, dept_loc, arr_loc, (dept_day_str + " " + dept_time_str),
                (arr_day_str + " " + arr_time_str), aircraft_id, status);
            OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();

            MessageBox.Show("Flight Added Successfully!");
        }

        private void remove_flight_confirm_btn_Click(object sender, EventArgs e)
        {
            string flight_id = remove_flight_id_input.Text;
            if (flight_id == "") { return; }

            FlightData flight = FormInstance.getFlightFromID(flight_id);
            if (flight.FLIGHTID == -1)
            { MessageBox.Show("No such Flight ID Found."); return; }

            FormInstance.conn.Open();
            string Sql = string.Format("DELETE FROM AMS.FLIGHT WHERE FLIGHTID = {0}",
                flight_id);
            OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Deleted Flight: " + flight_id);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();

        }

        private void upd_enable_dept_loc_CheckedChanged(object sender, EventArgs e)
        {
            upd_flight_dept_loc_input.Enabled = upd_enable_dept_loc.Checked;
        }

        private void upd_enable_arr_loc_CheckedChanged(object sender, EventArgs e)
        {
            upd_flight_arr_loc_input.Enabled = upd_enable_arr_loc.Checked;
        }

        private void upd_enable_dept_time_CheckedChanged(object sender, EventArgs e)
        {
            upd_dept_day_input.Enabled = upd_enable_dept_time.Checked;
            upd_dept_time_input.Enabled = upd_enable_dept_time.Checked;
        }

        private void upd_enable_arr_time_CheckedChanged(object sender, EventArgs e)
        {
            upd_arr_day_input.Enabled = upd_enable_arr_time.Checked;
            upd_arr_time_input.Enabled = upd_enable_arr_time.Checked;
        }

        private void upd_enable_aircraft_id_CheckedChanged(object sender, EventArgs e)
        {
            upd_arrcraft_id_input.Enabled = upd_enable_aircraft_id.Checked;
        }

        private void upd_enable_flight_status_CheckedChanged(object sender, EventArgs e)
        {
            upd_flight_status_input.Enabled = upd_enable_flight_status.Checked;
        }

        private void update_flight_confirm_btn_Click(object sender, EventArgs e)
        {
            if (!upd_enable_dept_loc.Checked && !upd_enable_arr_loc.Checked && !upd_enable_dept_time.Checked
                && !upd_enable_arr_time.Checked && !upd_enable_aircraft_id.Checked &&
                !upd_enable_flight_status.Checked)
            {
                MessageBox.Show("No Option Enabled!");
                return;
            }

            FlightData flight = FormInstance.getFlightFromID(upd_flight_id_input.Text);
            if (flight.FLIGHTID == -1) { MessageBox.Show("No Such Flight Id Found"); return; }

            if (upd_enable_dept_loc.Checked)
            {
                string sql = string.Format("UPDATE AMS.FLIGHT SET DEPARTURE = '{0}' WHERE FLIGHTID = {1}",
                    upd_flight_dept_loc_input.Text, flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_arr_loc.Checked)
            {
                string sql = string.Format("UPDATE AMS.FLIGHT SET DESTINATION = '{0}' WHERE FLIGHTID = {1}",
                    upd_flight_arr_loc_input.Text, flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_dept_time.Checked)
            {
                DateTime date = upd_dept_day_input.Value;
                DateTime time = upd_dept_time_input.Value;
                string day_time = date.Month + "/" + date.Day + "/" + date.Year + " " +
                    time.Hour + ":" + time.Minute + ":" + time.Second;
                string sql = string.Format("UPDATE AMS.FLIGHT SET DEPARTURE_TIME = TO_TIMESTAMP('{0}', 'MM/DD/YYYY HH:MI:SS AM') WHERE FLIGHTID = {1}",
                    day_time, flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_arr_time.Checked)
            {
                DateTime date = upd_arr_day_input.Value;
                DateTime time = upd_arr_time_input.Value;
                string day_time = date.Month + "/" + date.Day + "/" + date.Year + " " +
                    time.Hour + ":" + time.Minute + ":" + time.Second;
                string sql = string.Format("UPDATE AMS.FLIGHT SET ARRIVAL_TIME = TO_TIMESTAMP('{0}', 'MM/DD/YYYY HH:MI:SS AM') WHERE FLIGHTID = {1}",
                        day_time, flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_aircraft_id.Checked)
            {
                if (upd_arrcraft_id_input.SelectedIndex < 0) { return; }
                string sql = string.Format("UPDATE AMS.FLIGHT SET AIRCRAFTID = {0} WHERE FLIGHTID = {1}",
                        upd_arrcraft_id_input.SelectedItem.ToString(), flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_flight_status.Checked)
            {
                if (upd_flight_status_input.SelectedIndex < 0) { return; }
                string sql = string.Format("UPDATE AMS.FLIGHT SET STATUS = '{0}' WHERE FLIGHTID = {1}",
                        upd_flight_status_input.SelectedItem.ToString(), flight.FLIGHTID);
                FormInstance.executeNonReturnSQL(sql);
            }
        }

        private void admin_hire_emp_Click(object sender, EventArgs e)
        {
            admin_mng_employees_tab.SelectTab("emp_hire");
        }

        private void admin_upd_emp_Click(object sender, EventArgs e)
        {
            admin_mng_employees_tab.SelectTab("emp_upd");
            upd_emp_username_input.Enabled = upd_enable_username.Checked;
            upd_emp_email_input.Enabled = upd_enable_email.Checked;
            upd_emp_pwd_input.Enabled = upd_enable_pwd.Checked;
            upd_emp_phone_input.Enabled = upd_enable_phone.Checked;
            upd_emp_cnic_input.Enabled = upd_enable_cnic.Checked;
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (reg_userid_input.Text == "" || reg_username_input.Text == "" ||
                reg_email_input.Text == "" || reg_password_input.Text == "" ||
                reg_phone_input.Text == "" || reg_cnic_input.Text == "")
            {
                MessageBox.Show("You must fill all Fields!");
                return;
            }

            try
            {
                FormInstance.conn.Open();
                string Sql = string.Format("INSERT INTO AMS.User_Table (UserID, Name, UserType, Email, Password, Phone_Number, CNIC) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', {5}, {6})",
                    reg_userid_input.Text.ToString(),
                    reg_username_input.Text.ToString(),
                    "EMPLOYEE",
                    reg_email_input.Text.ToString(),
                    reg_password_input.Text.ToString(),
                    reg_phone_input.Text.ToString(),
                    reg_cnic_input.Text.ToString()
                   );

                OracleCommand cmd = new OracleCommand(Sql, FormInstance.conn);
                cmd.ExecuteNonQuery();
                FormInstance.conn.Close();

                MessageBox.Show("Employee Hired!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FormInstance.conn.Close();
            }


        }

        private void reg_emp_back_btn_Click(object sender, EventArgs e)
        {
            populateEMPGridView();

            admin_mng_employees_tab.SelectTab("emp_main");
        }

        private void upd_emp_back_btn_Click(object sender, EventArgs e)
        {
            populateEMPGridView();

            admin_mng_employees_tab.SelectTab("emp_main");
        }

        private void upd_emp_confirm_btn_Click(object sender, EventArgs e)
        {
            if (upd_emp_userid_input.Text == "") { MessageBox.Show("EmpId cannot be empty!"); return; }
            if (!upd_enable_username.Checked && !upd_enable_email.Checked &&
                !upd_enable_pwd.Checked && !upd_enable_phone.Checked && !upd_enable_cnic.Checked)
            {
                MessageBox.Show("Nothing Selected for update!"); return;
            }
            UserData user = FormInstance.getUserFromID(upd_emp_userid_input.Text);
            if (user.id == -1 || user.usertype != "EMPLOYEE") { MessageBox.Show("No such Employee Found"); return; }

            if (upd_enable_username.Checked)
            {
                string sql =
                    string.Format("UPDATE AMS.USER_TABLE SET NAME = '{0}' WHERE USERID = {1}",
                    upd_emp_username_input.Text, user.id);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_email.Checked)
            {
                string sql =
                    string.Format("UPDATE AMS.USER_TABLE SET EMAIL = '{0}' WHERE USERID = {1}",
                    upd_emp_email_input.Text, user.id);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_pwd.Checked)
            {
                string sql =
                    string.Format("UPDATE AMS.USER_TABLE SET PASSWORD = '{0}' WHERE USERID = {1}",
                    upd_emp_pwd_input.Text, user.id);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_phone.Checked)
            {
                string sql =
                    string.Format("UPDATE AMS.USER_TABLE SET PHONE_NUMBER = {0} WHERE USERID = {1}",
                    upd_emp_phone_input.Text, user.id);
                FormInstance.executeNonReturnSQL(sql);
            }
            if (upd_enable_cnic.Checked)
            {
                string sql =
                    string.Format("UPDATE AMS.USER_TABLE SET CNIC = {0} WHERE USERID = {1}",
                    upd_emp_cnic_input.Text, user.id);
                FormInstance.executeNonReturnSQL(sql);
            }
        }

        private void at_assign_confirm_btn_Click(object sender, EventArgs e)
        {
            if (at_sel_emp_input.SelectedIndex < 0) { return; }
            if (at_task_details_input.Text == "") { return; }

            UserData user = FormInstance.getUserFromID(at_sel_emp_input.SelectedItem.ToString());
            if (user.usertype == "PASSENGER") { return; }

            string task_id = "";

            //SELECT MAX(TASKID) FROM AMS.TASKS;

            FormInstance.conn.Open();
            string selectSql = "SELECT MAX(TASKID) FROM AMS.TASKS";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                reader.Read();
                int t_id = reader.GetInt32(0);
                t_id++;
                task_id = t_id.ToString();
            }
            catch (Exception) { task_id = "0"; }
            FormInstance.conn.Close();

            string date = at_date_input.Value.Month + "/" + at_date_input.Value.Day + ":" +
                at_date_input.Value.Year;

            string sql =
                    string.Format("INSERT INTO AMS.TASKS(TASKID,EMP_ID,TASK_DETAILS,DUE_DATE) " +
                    "VALUES({0},{1},'{2}',TO_DATE('{3}', 'MM/DD/YYYY'))",
                    task_id, user.id, at_task_details_input.Text, date);
            FormInstance.executeNonReturnSQL(sql);
        }

        private void a_assign_task_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setAssignTask(true);

            at_sel_emp_input.Items.Clear();

            FormInstance.conn.Open();
            string selectSql = "SELECT * FROM AMS.USER_TABLE WHERE USERTYPE = 'EMPLOYEE'";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                //reader.GetOrdinal("<Column-Name>")
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    at_sel_emp_input.Items.Add(reader.GetInt32(reader.GetOrdinal("USERID")));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();
        }

        private void feedback_back_btn_Click(object sender, EventArgs e)
        {
            setDashboard(true);
            setFeedbackBox(false);
        }

        private void a_view_feedback_btn_Click(object sender, EventArgs e)
        {
            setDashboard(false);
            setFeedbackBox(true);

            //Set Feedback Data
            FormInstance.conn.Open();
            string selectSql = "SELECT * FROM AMS.FEEDBACK";
            OracleCommand cmd = new OracleCommand(selectSql, FormInstance.conn);
            OracleDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgv_feedbacks.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormInstance.conn.Close();
        }

        private void upd_enable_username_CheckedChanged(object sender, EventArgs e)
        {
            upd_emp_username_input.Enabled = upd_enable_username.Checked;
        }

        private void upd_enable_email_CheckedChanged(object sender, EventArgs e)
        {
            upd_emp_email_input.Enabled = upd_enable_email.Checked;
        }

        private void upd_enable_pwd_CheckedChanged(object sender, EventArgs e)
        {
            upd_emp_pwd_input.Enabled = upd_enable_pwd.Checked;
        }

        private void upd_enable_phone_CheckedChanged(object sender, EventArgs e)
        {
            upd_emp_phone_input.Enabled = upd_enable_phone.Checked;
        }

        private void upd_enable_cnic_CheckedChanged(object sender, EventArgs e)
        {
            upd_emp_cnic_input.Enabled = upd_enable_cnic.Checked;
        }

    }
}

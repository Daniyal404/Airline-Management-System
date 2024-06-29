using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Semester_4_Database_Systems_Project
{
    public partial class Emploee_Dashboard : Form
    {
        public AMS FormInstance;
        public Emploee_Dashboard()
        {
            InitializeComponent();
            setDashboard(true);
            setCheckProfileBox(false);
            setEmployeeTaskBox(false);
            setSellTicketBox(false);
            setViewFeedback(false);
            setCustomerSupportBox(false);
        }
        private void setDashboard(bool status)
        {
            Employee_Dashboard_Box.Parent = this;
            Employee_Dashboard_Box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                Employee_Dashboard_Box.Show();
            }
            else
            {
                Employee_Dashboard_Box.Hide();
            }
        }
        private void setCheckProfileBox(bool status)
        {
            check_profile_box.Parent = this;
            check_profile_box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                check_profile_box.Show();
            }
            else
            {
                check_profile_box.Hide();
            }
        }
        private void setEmployeeTaskBox(bool status)
        {
            employee_tasks_box.Parent = this;
            employee_tasks_box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                employee_tasks_box.Show();
            }
            else
            {
                employee_tasks_box.Hide();
            }
        }
        private void setSellTicketBox(bool status)
        {
            sell_ticket_box.Parent = this;
            sell_ticket_box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                sell_ticket_box.Show();
            }
            else
            {
                sell_ticket_box.Hide();
            }
        }
        private void setViewFeedback(bool status)
        {
            view_feedback_box.Parent = this;
            view_feedback_box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                view_feedback_box.Show();
            }
            else
            {
                view_feedback_box.Hide();
            }
        }
        private void setCustomerSupportBox(bool status)
        {
            customer_support_box.Parent = this;
            customer_support_box.SetBounds(12, 12, 526, 344);
            if (status)
            {
                customer_support_box.Show();
            }
            else
            {
                customer_support_box.Hide();
            }
        }
        private void goto_check_profile_btn_Click(object sender, EventArgs e)
        {
            setCheckProfileBox(true);
            setDashboard(false);
            int userid = FormInstance.userID;
            UserData user = FormInstance.getUserFromID(userid.ToString());
            if (user.id == -1) { return; }

            label_prof_name.Text = "Name: " + user.name;
            label_prof_email.Text = "Email Address: " + user.email;
            label_prof_cnic.Text = "CNIC: " + user.cnic;
            label_prof_phone.Text = "Phone Number: " + user.phone;
        }
        private void prof_go_back_btn_Click(object sender, EventArgs e)
        {
            setCheckProfileBox(false);
            setDashboard(true);
        }

        private void et_go_back_btn_Click(object sender, EventArgs e)
        {
            setEmployeeTaskBox(false);
            setDashboard(true);
        }

        private void goto_tasks_mngr_btn_Click(object sender, EventArgs e)
        {
            setEmployeeTaskBox(true);
            setDashboard(false);

            //Default Values
            et_details_textbox.Text = "";
            label_et_due_date.Text = "Due Date: ---";
            label_et_status.Text = "Status: ---";

            // Show Tasks' List
            try
            {
                FormInstance.conn.Open();
                int userid = FormInstance.userID;
                string sql = "SELECT TASKID FROM AMS.TASKS WHERE EMP_ID = " + userid;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                all_tasks_list.Items.Clear();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        string task = reader.GetInt32(0).ToString();
                        all_tasks_list.Items.Add(task);
                    }
                    else { break; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();
        }

        private void all_tasks_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (all_tasks_list.SelectedIndex < 0) { return; }
            string taskid = all_tasks_list.SelectedItem.ToString();

            //
            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT * FROM AMS.TASKS WHERE TASKID = " + taskid;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    et_details_textbox.Text = reader.GetString(reader.GetOrdinal("TASK_DETAILS"));
                    DateTime d = reader.GetDateTime(reader.GetOrdinal("DUE_DATE"));
                    label_et_due_date.Text = "Due Date: " + d.Day.ToString() + "/" + d.Month.ToString() + "/" +
                        d.Year.ToString();
                    label_et_status.Text = "Status: " + reader.GetString(reader.GetOrdinal("TASK_STATUS"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            FormInstance.conn.Close();

        }

        private void et_completed_Click(object sender, EventArgs e)
        {
            if (all_tasks_list.SelectedIndex < 0) { return; }
            string taskid = all_tasks_list.SelectedItem.ToString();

            bool inProg = false;
            //
            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT TASK_STATUS FROM AMS.TASKS WHERE TASKID = " + taskid;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    if (reader.GetString(reader.GetOrdinal("TASK_STATUS")) == "In Progress")
                    {
                        inProg = true;
                    }
                    else
                    {
                        MessageBox.Show("Task not In-Progress");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            FormInstance.conn.Close();

            //Complete task
            if (inProg)
            {
                string sql = "UPDATE AMS.TASKS SET TASK_STATUS = 'Completed' WHERE TASKID = " + taskid;
                FormInstance.executeNonReturnSQL(sql);

                //To send update list signal
                int i = all_tasks_list.SelectedIndex;
                all_tasks_list.SelectedIndex = -1;
                all_tasks_list.SelectedIndex = i;
            }

        }

        private void goto_sell_ticket_btn_Click(object sender, EventArgs e)
        {
            setSellTicketBox(true);
            setDashboard(false);
        }

        private void st_userid_input_TextChanged(object sender, EventArgs e)
        {

        }

        private void st_go_back_btn_Click(object sender, EventArgs e)
        {
            setSellTicketBox(false);
            setDashboard(true);
        }

        private void st_flightid_input_TextChanged(object sender, EventArgs e)
        {
            string fid = st_flightid_input.Text;
            st_ticket_price_val.Text = "";
            if (fid == "") { return; }

            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT PRICE FROM AMS.FLIGHT_PRICES WHERE FLIGHTID = " + fid;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    st_ticket_price_val.Text = reader.GetInt32(0).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();
        }

        private void st_sell_btn_Click(object sender, EventArgs e)
        {
            string uid = st_userid_input.Text;
            if (st_ticket_price_val.Text == "") { MessageBox.Show("Unable to find related Flight's Price"); return; }

            string rid = "0";
            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT COALESCE(MAX(REVENUEID), 0) FROM AMS.REVENUE";
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    rid = (reader.GetInt32(0) + 1).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

            DateTime dateTime = DateTime.Now;
            string date = dateTime.Day.ToString() + "/" + dateTime.Month.ToString()
                + "/" + dateTime.Year.ToString();

            UserData user = FormInstance.getUserFromID(uid);
            if (user.id == -1) { MessageBox.Show("No User with this ID found!"); return; }
            if (user.usertype != "PASSENGER") { MessageBox.Show("User is not a Passenger"); return; }

            string msg = "User: " + uid + " ,bought a ticket for flight " + st_flightid_input.Text + " for a price of $" + st_ticket_price_val.Text;

            string instSql = String.Format("INSERT INTO AMS.REVENUE (REVENUEID, TRANSACTION_DATE, AMOUNT, SOURCE, DESCRIPTION) " +
                "VALUES ({0}, TO_DATE('{1}', 'DD/MM/YYYY'), {2}, 'Ticket Sales', '{3}')",
                rid, date, st_ticket_price_val.Text, msg);

            if (FormInstance.executeNonReturnSQL(instSql))
            {
                MessageBox.Show(msg);
            }
        }

        private void fb_go_back_btn_Click(object sender, EventArgs e)
        {
            setViewFeedback(false);
            setDashboard(true);
        }

        private void goto_view_feedback_btn_Click(object sender, EventArgs e)
        {
            setViewFeedback(true);
            setDashboard(false);

            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT * FROM AMS.FEEDBACK";
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dgv_feedback.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

        }

        private void goto_customer_supp_btn_Click(object sender, EventArgs e)
        {
            setCustomerSupportBox(true);
            setDashboard(false);
        }

        private void cs_go_back_btn_Click(object sender, EventArgs e)
        {
            setCustomerSupportBox(false);
            setDashboard(true);
        }

        private void cs_bag_submit_btn_Click(object sender, EventArgs e)
        {
            string userid = cs_bag_cid_input.Text;
            string flightid = cs_bag_flightid_input.Text;
            string status = cs_bag_status_input.Text;
            string loc = cs_bag_loc_input.Text;

            UserData userData = FormInstance.getUserFromID(userid);
            if (userData.id == -1) { MessageBox.Show("No Such User Found!"); return; }

            FlightData flightData = FormInstance.getFlightFromID(flightid);
            if (flightData.FLIGHTID == -1) { MessageBox.Show("No Such Flight Found!"); return; }

            //Test for seat booked for this flight
            bool hasASeat = false;
            try
            {
                FormInstance.conn.Open();
                string check = String.Format("SELECT BOOKINGID FROM AMS.BOOKINGS " +
                    "WHERE PASSENGERID = {0} AND FLIGHTID = {1}", userid, flightid);
                OracleCommand cmd = new OracleCommand(check, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.Read())
                {
                    hasASeat = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //
            FormInstance.conn.Open();
            if (!hasASeat) { MessageBox.Show("This Passenger is not Traveling in this Flight"); return; }

            string bid = "0";
            //Get a Baggage ID
            try
            {
                FormInstance.conn.Open();
                string s = "SELECT COALESCE(MAX(BAGGAGEID), 0) FROM AMS.BAGGAGE_HANDLING";
                OracleCommand cmd = new OracleCommand(s, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    bid = (reader.GetInt32(0) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

            //

            string sql = String.Format("INSERT INTO AMS.BAGGAGE_HANDLING (BAGGAGEID, PASSENGERID, FLIGHTID, STATUS, LOCATION)" +
                " VALUES ({0}, {1}, {2}, '{3}', '{4}')", bid, userid, flightid, status, loc
                );

            if (FormInstance.executeNonReturnSQL(sql))
            {
                MessageBox.Show("Bag " + status);
            }

        }

        private void cs_s_submit_Click(object sender, EventArgs e)
        {
            string uid = cs_s_uid_input.Text;
            string fid = cs_s_fid_input.Text;
            string type = cs_s_type_input.Text;
            DateTime dateTime = cs_s_date_input.Value.Date;
            string details = cs_s_details_input.Text;

            UserData userData = FormInstance.getUserFromID(uid);
            if (userData.id == -1) { MessageBox.Show("No Such User Found!"); return; }

            FlightData flightData = FormInstance.getFlightFromID(fid);
            if (flightData.FLIGHTID == -1) { MessageBox.Show("No Such Flight Found!"); return; }

            //Test for seat booked for this flight
            bool hasASeat = false;
            try
            {
                FormInstance.conn.Open();
                string check = String.Format("SELECT BOOKINGID FROM AMS.BOOKINGS " +
                    "WHERE PASSENGERID = {0} AND FLIGHTID = {1}", uid, fid);
                OracleCommand cmd = new OracleCommand(check, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.Read())
                {
                    hasASeat = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //
            FormInstance.conn.Open();
            if (!hasASeat) { MessageBox.Show("This Passenger is not Traveling in this Flight"); return; }


            string sid = "0";
            //Get a Service ID
            try
            {
                FormInstance.conn.Open();
                string s = "SELECT COALESCE(MAX(SERVICEID), 0) FROM AMS.PASSENGER_SERVICES";
                OracleCommand cmd = new OracleCommand(s, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    sid = (reader.GetInt32(0) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();
            //

            string day = dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
            if (type == "") { MessageBox.Show("Invalid Type"); return; }
            if (details == "") { MessageBox.Show("Invalid Details"); return; }

            string sql = String.Format("INSERT INTO AMS.PASSENGER_SERVICES (SERVICEID, PASSENGERID, FLIGHTID, SERVICE_TYPE, SERVICE_DETAILS, SERVICE_TIME) " +
                "VALUES ({0}, {1}, {2}, '{3}', '{4}', TO_DATE('{5}', 'DD/MM/YYYY'))",
                sid, uid, fid, type, details, day);

            if (FormInstance.executeNonReturnSQL(sql))
            {
                MessageBox.Show("!!");
            }
        }
    }
}

using Oracle.ManagedDataAccess.Client;
using System.Data;


namespace Semester_4_Database_Systems_Project
{
    public partial class Passenger_Dashboard : Form
    {
        public AMS FormInstance;
        public Passenger_Dashboard()
        {
            InitializeComponent();
            setPassengerBox(true);
            setCheckProfileBox(false);
            setBookingBox(false);
            setFeedbackBox(false);
            setViewHistBox(false);
            setTackFlightBox(false);
        }
        private void setPassengerBox(bool status)
        {
            Passenger_Homepage_Box.Parent = this;
            if (status)
            {
                Passenger_Homepage_Box.Show();
            }
            else
            {
                Passenger_Homepage_Box.Hide();
            }
            Passenger_Homepage_Box.SetBounds(16, 15, 701, 423);
        }
        private void setCheckProfileBox(bool status)
        {
            check_profile_box.Parent = this;
            if (status)
            {
                check_profile_box.Show();
            }
            else
            {
                check_profile_box.Hide();
            }
            check_profile_box.SetBounds(16, 15, 701, 423);
        }
        private void setBookingBox(bool status)
        {
            Booking_box.Parent = this;
            if (status)
            {
                Booking_box.Show();
            }
            else
            {
                Booking_box.Hide();
            }
            Booking_box.SetBounds(16, 15, 701, 423);
        }
        private void setFeedbackBox(bool status)
        {
            Feedback_box.Parent = this;
            if (status)
            {
                Feedback_box.Show();
            }
            else
            {
                Feedback_box.Hide();
            }
            Feedback_box.SetBounds(16, 15, 701, 423);
        }
        private void setViewHistBox(bool status)
        {
            View_History_Box.Parent = this;
            if (status)
            {
                View_History_Box.Show();
            }
            else
            {
                View_History_Box.Hide();
            }
            View_History_Box.SetBounds(16, 15, 701, 423);
        }
        private void setTackFlightBox(bool status)
        {
            track_flight_box.Parent = this;
            if (status)
            {
                track_flight_box.Show();
            }
            else
            {
                track_flight_box.Hide();
            }
            track_flight_box.SetBounds(16, 15, 701, 423);
        }

        // Other Functions

        public string dateFixer(string str)
        {
            if (str == "0")
            { return "00"; }
            if (str == "1")
            { return "01"; }
            if (str == "2")
            { return "02"; }
            if (str == "3")
            { return "03"; }
            if (str == "4")
            { return "04"; }
            if (str == "5")
            { return "05"; }
            if (str == "6")
            { return "06"; }
            if (str == "7")
            { return "07"; }
            if (str == "8")
            { return "08"; }
            if (str == "9")
            { return "09"; }
            return str;
        }
        //

        private void goto_check_profile_btn_Click(object sender, EventArgs e)
        {
            int userid = FormInstance.userID;
            string uid = userid.ToString();
            UserData user = FormInstance.getUserFromID(uid);

            label_ud_username.Text = "Name: " + user.name;
            label_ud_email.Text = "Email Address: " + user.email;
            label_ud_phone.Text = "Phone Number: " + user.phone;

            int fb_booked = 0;
            label_ud_flights_booked.Text = "Total Flights Booked: " + fb_booked;
            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT COUNT(BOOKINGID) FROM AMS.BOOKINGS WHERE PASSENGERID = " + FormInstance.userID;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                    fb_booked = reader.GetInt32(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            FormInstance.conn.Close();

            label_ud_flights_booked.Text = "Total Flights Booked: " + fb_booked;

            //Get Feedbacks giveb count
            int fb_count = 0;
            label_ud_feedbacks_given.Text = "Feedbacks Given: " + fb_count;
            try
            {
                FormInstance.conn.Open();
                string sql = "SELECT COUNT(FEEDBACKID) " +
                    "FROM AMS.FEEDBACK f INNER JOIN AMS.USER_TABLE u " +
                    "ON f.USERID = u.USERID " +
                    "WHERE u.USERID = " + user.id;
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                fb_count = reader.GetInt32(0);
                FormInstance.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                FormInstance.conn.Close();
                return;
            }
            label_ud_feedbacks_given.Text = "Feedbacks Given: " + fb_count;

            setCheckProfileBox(true);
            setPassengerBox(false);
        }

        private void fb_submit_btn_Click(object sender, EventArgs e)
        {
            if (fb_rating_input.Value <= 0 || fb_rating_input.Value > 10)
            { MessageBox.Show("Give a Valid Rating 1-10"); return; }

            string feedback = fb_input.Text;
            string rating = fb_rating_input.Value.ToString();

            if (feedback == "")
            {
                feedback = "NULL";
            }

            //Generate a Feedback ID:
            string fb_id;
            string sql = "SELECT COALESCE(MAX(FEEDBACKID), 0) FROM AMS.FEEDBACK";
            try
            {
                FormInstance.conn.Open();
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fb_id = (reader.GetInt32(0) + 1).ToString();
                }
                else
                {
                    fb_id = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                FormInstance.conn.Close();
                return;
            }
            FormInstance.conn.Close();

            string sql2 = String.Format("INSERT INTO AMS.FEEDBACK(FEEDBACKID, USERID, FEEDBACK_DETAILS, RATING) VALUES({0}, {1}, '{2}', {3})",
                fb_id, FormInstance.userID, feedback, rating);
            if (FormInstance.executeNonReturnSQL(sql2))
            {
                MessageBox.Show("Feedback Recorded!");
                fb_rating_input.Value = 0;
                fb_input.Text = "";
            }

        }

        private void bs_flight_id_input_TextChanged(object sender, EventArgs e)
        {
            string txt = bs_flight_id_input.Text;
            FormInstance.conn.Open();
            int count = 0;
            string sql = "SELECT COALESCE(MAX(FLIGHTID), 0) FROM AMS.FLIGHT";
            try
            {
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

            bs_flights_list.Items.Clear();

            for (int i = 0; i <= count; i++)
            {
                if (i.ToString().Contains(txt))
                {
                    FlightData f = FormInstance.getFlightFromID(i.ToString());
                    if (f.FLIGHTID != -1)
                    {
                        bs_flights_list.Items.Add(f.FLIGHTID.ToString());
                    }
                }
            }

        }

        private void bs_book_seat_btn_Click(object sender, EventArgs e)
        {
            // Book Seat
            if (bs_flights_list.SelectedIndex < 0) { return; }

            string flight_id = bs_flights_list.SelectedItem.ToString();

            // Generate a new Booking ID
            string sql1 = "SELECT COALESCE(MAX(BOOKINGID), 0) FROM AMS.BOOKINGS";
            int booking_id = 0;
            try
            {
                FormInstance.conn.Open();
                OracleCommand cmd = new OracleCommand(sql1, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    booking_id = reader.GetInt32(0) + 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

            int uid = FormInstance.userID;

            string sql = String.Format("INSERT INTO AMS.BOOKINGS(BOOKINGID, PASSENGERID, FLIGHTID) " +
                "VALUES({0}, {1}, {2})", booking_id.ToString(), uid.ToString(), flight_id);
            if (FormInstance.executeNonReturnSQL(sql))
            {
                MessageBox.Show("Ticket Booked with ID: " + booking_id);
            }
        }

        private void bs_flights_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bs_flights_list.SelectedIndex < 0) { return; }

            string flight_id = bs_flights_list.SelectedItem.ToString();

            FlightData f = FormInstance.getFlightFromID(flight_id);
            Aircraft a = FormInstance.getAircraftFromID(f.AIRCRAFTID.ToString());
            int price = FormInstance.getFlightPriceFromFlightID(f.FLIGHTID.ToString());

            if (f.FLIGHTID == -1) { return; }
            if (a.id == -1) { return; }

            label_bs_from.Text = "From:\n" + f.DEPARTURE;
            label_bs_to.Text = "To:\n" + f.DESTINATION;
            label_bs_departure_time.Text = "Departure Time: " + f.DEPARTURE_TIME;
            label_bs_arrival_time.Text = "Arrival Time: " + f.ARRIVAL_TIME;
            label_bs_status.Text = "Status:\n" + f.STATUS;
            label_bs_plane_name.Text = "Airplane Name: " + a.name;
            bs_price_disp.Text = price.ToString();

            //Get Current Booked Tickets for this Flight
            int count = 0;
            try
            {
                FormInstance.conn.Open();
                string sql = String.Format("SELECT COALESCE(COUNT(PASSENGERID), 0) FROM AMS.BOOKINGS WHERE FLIGHTID = {0} GROUP BY FLIGHTID",
                    f.FLIGHTID.ToString());
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    count = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();

            label_bs_cap.Text = "Capacity: " + count + "/" + a.capacity;

            // End Get Current Booked Tickets;

            if (f.STATUS.ToLower() == "on time" && count != a.capacity)
            {
                //Eable Button
                bs_book_seat_btn.Enabled = true;
            }
            else
            {
                //Disable Button
                bs_book_seat_btn.Enabled = false;
            }
        }

        private void goto_book_seat_btn_Click(object sender, EventArgs e)
        {
            setBookingBox(true);
            setPassengerBox(false);
            bs_flight_id_input.Text = "-1";
            bs_flight_id_input.Text = "";
        }

        private void goto_feedback_btn_Click(object sender, EventArgs e)
        {
            setFeedbackBox(true);
            setPassengerBox(false);
        }

        private void ud_go_back_btn_Click(object sender, EventArgs e)
        {
            setCheckProfileBox(false);
            setPassengerBox(true);
        }

        private void bs_go_back_btn_Click(object sender, EventArgs e)
        {
            setBookingBox(false);
            setPassengerBox(true);
        }

        private void fb_go_back_btn_Click(object sender, EventArgs e)
        {
            setFeedbackBox(false);
            setPassengerBox(true);
        }

        private void goto_prev_resrv_btn_Click(object sender, EventArgs e)
        {
            setViewHistBox(true);
            setPassengerBox(false);

            //Show Past History

            //string sql = "SELECT f.* FROM AMS.FLIGHT f INNER JOIN AMS.BOOKINGS b" +
            //    "ON f.FLIGHTID = b.FLIGHTID WHERE b.PASSENGERID = " + FormInstance.userID;
            string sql = "SELECT b.BOOKINGID, f.* FROM AMS.FLIGHT f INNER JOIN AMS.BOOKINGS b " +
             "ON f.FLIGHTID = b.FLIGHTID WHERE b.PASSENGERID = " + FormInstance.userID;

            try
            {
                FormInstance.conn.Open();
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dgv_vh.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();
        }

        private void vh_go_back_btn_Click(object sender, EventArgs e)
        {
            setViewHistBox(false);
            setPassengerBox(true);
        }

        private void goto_track_flight_Click(object sender, EventArgs e)
        {
            setTackFlightBox(true);
            setPassengerBox(false);
        }

        private void trfl_go_back_btn_Click(object sender, EventArgs e)
        {
            setTackFlightBox(false);
            setPassengerBox(true);
        }

        private void trfl_search_btn_Click(object sender, EventArgs e)
        {
            DataTable clear_table = new DataTable();
            dgv_trfl.DataSource = clear_table;

            if (trfl_From_input.Text == "" || trfl_To_input.Text == "")
            {
                MessageBox.Show("Make sure to fill To: - AND From: -");
                return;
            }
            DateTime date_time = trfl_day_input.Value;
            string departure = trfl_From_input.Text;
            string arrival = trfl_To_input.Text;

            string date = dateFixer(date_time.Day.ToString()) + "/" +
                dateFixer(date_time.Month.ToString()) + "/" +
                date_time.Year.ToString();

            try
            {
                FormInstance.conn.Open();
                string sql = String.Format("SELECT * FROM AMS.FLIGHT WHERE TO_CHAR(DEPARTURE_TIME, 'DD/MM/YYYY') = '{0}'",
                    date);
                OracleCommand cmd = new OracleCommand(sql, FormInstance.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    dgv_trfl.DataSource = table;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FormInstance.conn.Close();
        }
    }
}

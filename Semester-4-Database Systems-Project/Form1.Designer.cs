namespace Semester_4_Database_Systems_Project
{
    partial class AMS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selection_panel = new Panel();
            logout_btn = new Button();
            logo_plane = new PictureBox();
            goto_passenger_homepage = new Button();
            login_signup_btn = new Button();
            goto_admin_dashboard_btn = new Button();
            goto_employee_dashboard = new Button();
            header_panel = new Panel();
            label_header_name = new Label();
            close_btn = new Button();
            main_panel = new Panel();
            label_welcome = new Label();
            selection_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logo_plane).BeginInit();
            header_panel.SuspendLayout();
            main_panel.SuspendLayout();
            SuspendLayout();
            // 
            // selection_panel
            // 
            selection_panel.BackColor = Color.SteelBlue;
            selection_panel.Controls.Add(logout_btn);
            selection_panel.Controls.Add(logo_plane);
            selection_panel.Controls.Add(goto_passenger_homepage);
            selection_panel.Controls.Add(login_signup_btn);
            selection_panel.Controls.Add(goto_admin_dashboard_btn);
            selection_panel.Controls.Add(goto_employee_dashboard);
            selection_panel.Dock = DockStyle.Left;
            selection_panel.Location = new Point(0, 37);
            selection_panel.Margin = new Padding(2);
            selection_panel.Name = "selection_panel";
            selection_panel.Size = new Size(175, 425);
            selection_panel.TabIndex = 0;
            // 
            // logout_btn
            // 
            logout_btn.Enabled = false;
            logout_btn.FlatStyle = FlatStyle.Flat;
            logout_btn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logout_btn.Image = Properties.Resources.logout;
            logout_btn.ImageAlign = ContentAlignment.MiddleLeft;
            logout_btn.Location = new Point(0, 336);
            logout_btn.Margin = new Padding(2);
            logout_btn.Name = "logout_btn";
            logout_btn.Size = new Size(175, 33);
            logout_btn.TabIndex = 2;
            logout_btn.Text = "       Logout";
            logout_btn.UseVisualStyleBackColor = true;
            logout_btn.Visible = false;
            logout_btn.Click += logout_btn_Click;
            // 
            // logo_plane
            // 
            logo_plane.BackgroundImageLayout = ImageLayout.None;
            logo_plane.BorderStyle = BorderStyle.FixedSingle;
            logo_plane.Image = Properties.Resources.airplane2;
            logo_plane.Location = new Point(23, 13);
            logo_plane.Margin = new Padding(2);
            logo_plane.Name = "logo_plane";
            logo_plane.Size = new Size(129, 91);
            logo_plane.SizeMode = PictureBoxSizeMode.CenterImage;
            logo_plane.TabIndex = 1;
            logo_plane.TabStop = false;
            logo_plane.Click += logo_plane_Click;
            // 
            // goto_passenger_homepage
            // 
            goto_passenger_homepage.Enabled = false;
            goto_passenger_homepage.FlatStyle = FlatStyle.Flat;
            goto_passenger_homepage.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            goto_passenger_homepage.Image = Properties.Resources.dashboard;
            goto_passenger_homepage.ImageAlign = ContentAlignment.MiddleLeft;
            goto_passenger_homepage.Location = new Point(0, 134);
            goto_passenger_homepage.Margin = new Padding(2);
            goto_passenger_homepage.Name = "goto_passenger_homepage";
            goto_passenger_homepage.Size = new Size(175, 33);
            goto_passenger_homepage.TabIndex = 4;
            goto_passenger_homepage.Text = "       Home Page";
            goto_passenger_homepage.UseVisualStyleBackColor = true;
            goto_passenger_homepage.Visible = false;
            goto_passenger_homepage.Click += goto_passenger_homepage_Click;
            // 
            // login_signup_btn
            // 
            login_signup_btn.FlatStyle = FlatStyle.Flat;
            login_signup_btn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            login_signup_btn.Image = Properties.Resources.user_profile;
            login_signup_btn.ImageAlign = ContentAlignment.MiddleLeft;
            login_signup_btn.Location = new Point(0, 134);
            login_signup_btn.Margin = new Padding(2);
            login_signup_btn.Name = "login_signup_btn";
            login_signup_btn.Size = new Size(175, 33);
            login_signup_btn.TabIndex = 0;
            login_signup_btn.Text = "       Login";
            login_signup_btn.UseVisualStyleBackColor = true;
            login_signup_btn.Click += login_signup_btn_Click;
            // 
            // goto_admin_dashboard_btn
            // 
            goto_admin_dashboard_btn.Enabled = false;
            goto_admin_dashboard_btn.FlatStyle = FlatStyle.Flat;
            goto_admin_dashboard_btn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            goto_admin_dashboard_btn.Image = Properties.Resources.administrator_32x32;
            goto_admin_dashboard_btn.ImageAlign = ContentAlignment.MiddleLeft;
            goto_admin_dashboard_btn.Location = new Point(0, 134);
            goto_admin_dashboard_btn.Margin = new Padding(2);
            goto_admin_dashboard_btn.Name = "goto_admin_dashboard_btn";
            goto_admin_dashboard_btn.Size = new Size(175, 33);
            goto_admin_dashboard_btn.TabIndex = 3;
            goto_admin_dashboard_btn.Text = "       Dashboard";
            goto_admin_dashboard_btn.UseVisualStyleBackColor = true;
            goto_admin_dashboard_btn.Visible = false;
            goto_admin_dashboard_btn.Click += goto_admin_dashboard_btn_Click;
            // 
            // goto_employee_dashboard
            // 
            goto_employee_dashboard.Enabled = false;
            goto_employee_dashboard.FlatStyle = FlatStyle.Flat;
            goto_employee_dashboard.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            goto_employee_dashboard.Image = Properties.Resources.employee_dashboard;
            goto_employee_dashboard.ImageAlign = ContentAlignment.MiddleLeft;
            goto_employee_dashboard.Location = new Point(0, 134);
            goto_employee_dashboard.Margin = new Padding(2);
            goto_employee_dashboard.Name = "goto_employee_dashboard";
            goto_employee_dashboard.Size = new Size(175, 33);
            goto_employee_dashboard.TabIndex = 5;
            goto_employee_dashboard.Text = "       Dashboard";
            goto_employee_dashboard.UseVisualStyleBackColor = true;
            goto_employee_dashboard.Visible = false;
            goto_employee_dashboard.Click += goto_employee_dashboard_Click;
            // 
            // header_panel
            // 
            header_panel.BackColor = Color.DarkBlue;
            header_panel.Controls.Add(label_header_name);
            header_panel.Controls.Add(close_btn);
            header_panel.Dock = DockStyle.Top;
            header_panel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            header_panel.Location = new Point(0, 0);
            header_panel.Margin = new Padding(2);
            header_panel.Name = "header_panel";
            header_panel.Size = new Size(817, 37);
            header_panel.TabIndex = 1;
            // 
            // label_header_name
            // 
            label_header_name.AutoSize = true;
            label_header_name.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_header_name.Location = new Point(13, 7);
            label_header_name.Margin = new Padding(4, 0, 4, 0);
            label_header_name.Name = "label_header_name";
            label_header_name.Size = new Size(256, 20);
            label_header_name.TabIndex = 1;
            label_header_name.Text = "Airline Management System (AMS)";
            // 
            // close_btn
            // 
            close_btn.Anchor = AnchorStyles.None;
            close_btn.Font = new Font("MV Boli", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            close_btn.Location = new Point(774, 1);
            close_btn.Margin = new Padding(0);
            close_btn.Name = "close_btn";
            close_btn.Size = new Size(44, 37);
            close_btn.TabIndex = 0;
            close_btn.Text = "X";
            close_btn.UseVisualStyleBackColor = true;
            close_btn.Click += close_btn_Click;
            // 
            // main_panel
            // 
            main_panel.Controls.Add(label_welcome);
            main_panel.Dock = DockStyle.Fill;
            main_panel.Location = new Point(175, 37);
            main_panel.Margin = new Padding(2);
            main_panel.Name = "main_panel";
            main_panel.Size = new Size(642, 425);
            main_panel.TabIndex = 2;
            // 
            // label_welcome
            // 
            label_welcome.AutoSize = true;
            label_welcome.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_welcome.Location = new Point(112, 129);
            label_welcome.Margin = new Padding(4, 0, 4, 0);
            label_welcome.Name = "label_welcome";
            label_welcome.Size = new Size(359, 144);
            label_welcome.TabIndex = 0;
            label_welcome.Text = "Welcome To Airline Management System\r\n\r\n\r\nMade By:\r\n          Daniyal Shafiq   22F-3598\r\n          Abdul Rehman  22F-3595\r\n";
            // 
            // AMS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(817, 462);
            Controls.Add(main_panel);
            Controls.Add(selection_panel);
            Controls.Add(header_panel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "AMS";
            Text = "AMS";
            Load += AMS_Load;
            selection_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logo_plane).EndInit();
            header_panel.ResumeLayout(false);
            header_panel.PerformLayout();
            main_panel.ResumeLayout(false);
            main_panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel selection_panel;
        private System.Windows.Forms.Panel header_panel;
        public System.Windows.Forms.Panel main_panel;
        public System.Windows.Forms.Button login_signup_btn;
        private System.Windows.Forms.PictureBox logo_plane;
        private System.Windows.Forms.Button close_btn;
        public System.Windows.Forms.Button logout_btn;
        public System.Windows.Forms.Button goto_admin_dashboard_btn;
        private System.Windows.Forms.Label label_header_name;
        private System.Windows.Forms.Label label_welcome;
        public System.Windows.Forms.Button goto_passenger_homepage;
        public System.Windows.Forms.Button goto_employee_dashboard;
    }
}


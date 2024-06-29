using System.Windows.Forms;

namespace Semester_4_Database_Systems_Project
{
    partial class LoginSignupForm
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
            this.label_username = new System.Windows.Forms.Label();
            this.label_login_signup_title = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.username_input = new System.Windows.Forms.TextBox();
            this.password_input = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.label_signup_clickhere = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_want_to_register = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_username
            // 
            this.label_username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_username.AutoSize = true;
            this.label_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_username.Location = new System.Drawing.Point(124, 181);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(130, 29);
            this.label_username.TabIndex = 0;
            this.label_username.Text = "Username:";
            // 
            // label_login_signup_title
            // 
            this.label_login_signup_title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_login_signup_title.AutoSize = true;
            this.label_login_signup_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_login_signup_title.Location = new System.Drawing.Point(259, 49);
            this.label_login_signup_title.Name = "label_login_signup_title";
            this.label_login_signup_title.Size = new System.Drawing.Size(222, 46);
            this.label_login_signup_title.TabIndex = 1;
            this.label_login_signup_title.Text = "Login Page";
            // 
            // label_password
            // 
            this.label_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_password.Location = new System.Drawing.Point(125, 231);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(126, 29);
            this.label_password.TabIndex = 2;
            this.label_password.Text = "Password:";
            // 
            // username_input
            // 
            this.username_input.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.username_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username_input.Location = new System.Drawing.Point(287, 185);
            this.username_input.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(214, 22);
            this.username_input.TabIndex = 3;
            // 
            // password_input
            // 
            this.password_input.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.password_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_input.Location = new System.Drawing.Point(287, 235);
            this.password_input.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.password_input.Name = "password_input";
            this.password_input.Size = new System.Drawing.Size(214, 22);
            this.password_input.TabIndex = 4;
            // 
            // login_btn
            // 
            this.login_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.Location = new System.Drawing.Point(297, 303);
            this.login_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(145, 50);
            this.login_btn.TabIndex = 5;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // label_signup_clickhere
            // 
            this.label_signup_clickhere.AutoSize = true;
            this.label_signup_clickhere.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_signup_clickhere.ForeColor = System.Drawing.Color.Blue;
            this.label_signup_clickhere.Location = new System.Drawing.Point(473, 383);
            this.label_signup_clickhere.Name = "label_signup_clickhere";
            this.label_signup_clickhere.Size = new System.Drawing.Size(37, 18);
            this.label_signup_clickhere.TabIndex = 6;
            this.label_signup_clickhere.Text = "here";
            this.label_signup_clickhere.Click += new System.EventHandler(this.label_signup_clickhere_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 274);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 7;
            // 
            // label_want_to_register
            // 
            this.label_want_to_register.AutoSize = true;
            this.label_want_to_register.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_want_to_register.Location = new System.Drawing.Point(235, 383);
            this.label_want_to_register.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_want_to_register.Name = "label_want_to_register";
            this.label_want_to_register.Size = new System.Drawing.Size(223, 18);
            this.label_want_to_register.TabIndex = 8;
            this.label_want_to_register.Text = "Dont have an account? Register ";
            // 
            // LoginSignupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 453);
            this.Controls.Add(this.label_signup_clickhere);
            this.Controls.Add(this.label_want_to_register);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_login_signup_title);
            this.Controls.Add(this.label_username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginSignupForm";
            this.Text = "LoginSignupForm";
            this.Load += new System.EventHandler(this.LoginSignupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_login_signup_title;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox username_input;
        private System.Windows.Forms.TextBox password_input;
        private System.Windows.Forms.Button login_btn;
        private Label label_signup_clickhere;
        private Label label1;
        private Label label_want_to_register;
    }
}
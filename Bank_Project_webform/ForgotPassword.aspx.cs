using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BankAccountSimulator
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        // ==========================================
        // DATABASE CONNECTION
        // ==========================================

        private readonly string connectionString =
            ConfigurationManager
            .ConnectionStrings["BankDBConnection"]
            .ConnectionString;


        // ==========================================
        // PAGE LOAD
        // ==========================================

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(
                System.Web.HttpCacheability.NoCache);

            Response.Cache.SetNoStore();

            Response.Cache.SetExpires(
                DateTime.UtcNow.AddDays(-1));

            Response.AppendHeader(
                "Pragma",
                "no-cache");
        }


        // ==========================================
        // RESET PASSWORD
        // ==========================================

        protected void btnResetPassword_Click(
            object sender,
            EventArgs e)
        {
            // ------------------------------------------
            // GET VALUES
            // ------------------------------------------

            string accountNumber =
                txtAccountNumber.Text.Trim();

            string question =
                ddlSecurityQuestion.SelectedValue.Trim();

            string answer =
                txtSecurityAnswer.Text.Trim();

            string newPassword =
                txtNewPassword.Text.Trim();

            string confirmPassword =
                txtConfirmPassword.Text.Trim();


            // ------------------------------------------
            // ACCOUNT NUMBER VALIDATION
            // ------------------------------------------

            if (accountNumber == "")
            {
                ShowError(
                    "Please enter Account Number.");

                return;
            }


            // ------------------------------------------
            // SECURITY QUESTION VALIDATION
            // ------------------------------------------

            if (question == "")
            {
                ShowError(
                    "Please select Security Question.");

                return;
            }


            // ------------------------------------------
            // SECURITY ANSWER VALIDATION
            // ------------------------------------------

            if (answer == "")
            {
                ShowError(
                    "Please enter Security Answer.");

                return;
            }


            // ------------------------------------------
            // NEW PASSWORD VALIDATION
            // ------------------------------------------

            if (newPassword == "")
            {
                ShowError(
                    "Please enter New Password.");

                return;
            }


            // ------------------------------------------
            // CONFIRM PASSWORD VALIDATION
            // ------------------------------------------

            if (confirmPassword == "")
            {
                ShowError(
                    "Please confirm New Password.");

                return;
            }


            // ------------------------------------------
            // PASSWORD MATCH
            // ------------------------------------------

            if (newPassword != confirmPassword)
            {
                ShowError(
                    "Passwords do not match.");

                return;
            }


            // ------------------------------------------
            // PASSWORD LENGTH
            // ------------------------------------------

            if (newPassword.Length < 6)
            {
                ShowError(
                    "Password must contain at least 6 characters.");

                return;
            }


            try
            {
                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    con.Open();


                    // ==========================================
                    // VERIFY ACCOUNT + QUESTION + ANSWER
                    // ==========================================

                    string verifyQuery = @"
                        SELECT COUNT(*)
                        FROM Account
                        WHERE AccountNumber = @AccountNumber
                        AND SecurityQuestion = @SecurityQuestion
                        AND SecurityAnswer = @SecurityAnswer";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            verifyQuery,
                            con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);

                        cmd.Parameters.AddWithValue(
                            "@SecurityQuestion",
                            question);

                        cmd.Parameters.AddWithValue(
                            "@SecurityAnswer",
                            answer);


                        int count =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());


                        // ==========================================
                        // QUESTION OR ANSWER DOES NOT MATCH
                        // ==========================================

                        if (count != 1)
                        {
                            ShowError(
                                "Security Question or Security Answer does not match.");

                            return;
                        }
                    }


                    // ==========================================
                    // UPDATE PASSWORD
                    // ==========================================

                    string updateQuery = @"
                        UPDATE Account
                        SET Password = @Password
                        WHERE AccountNumber = @AccountNumber";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            updateQuery,
                            con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Password",
                            newPassword);

                        cmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);


                        int rows =
                            cmd.ExecuteNonQuery();


                        // ==========================================
                        // PASSWORD RESET SUCCESS
                        // ==========================================

                        if (rows == 1)
                        {
                            ShowSuccess(
                                "Password reset successfully. Please login.");

                            // Clear fields

                            txtSecurityAnswer.Text = "";

                            txtNewPassword.Text = "";

                            txtConfirmPassword.Text = "";
                        }
                        else
                        {
                            ShowError(
                                "Password reset failed.");

                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    "Database Error: " + ex.Message);
            }
        }


        // ==========================================
        // BACK TO LOGIN
        // ==========================================

        protected void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "Login.aspx",
                false);

            Context.ApplicationInstance
                .CompleteRequest();
        }


        // ==========================================
        // SUCCESS MESSAGE
        // ==========================================

        private void ShowSuccess(
            string message)
        {
            lblMessage.Text = message;

            lblMessage.Style["color"] =
                "#198754";

            lblMessage.Style["background"] =
                "#e9f7ef";
        }


        // ==========================================
        // ERROR MESSAGE
        // ==========================================

        private void ShowError(
            string message)
        {
            lblMessage.Text = message;

            lblMessage.Style["color"] =
                "#dc3545";

            lblMessage.Style["background"] =
                "#fdecec";
        }
    }
}
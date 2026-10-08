using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BankAccountSimulator
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager
            .ConnectionStrings["BankDBConnection"]
            .ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string accountNumber =
                txtAccountNumber.Text.Trim();

            string password =
                txtPassword.Text.Trim();

            string confirmPassword =
                txtConfirmPassword.Text.Trim();

            string depositText =
                txtInitialDeposit.Text.Trim();

            string securityQuestion =
                ddlSecurityQuestion.SelectedValue;

            string securityAnswer =
                txtSecurityAnswer.Text.Trim();


            // ==============================
            // VALIDATION
            // ==============================

            if (accountNumber == "")
            {
                ShowMessage(
                    "Please enter Account Number.",
                    false);
                return;
            }


            if (password == "")
            {
                ShowMessage(
                    "Please enter Password.",
                    false);
                return;
            }


            if (confirmPassword == "")
            {
                ShowMessage(
                    "Please confirm Password.",
                    false);
                return;
            }


            if (password != confirmPassword)
            {
                ShowMessage(
                    "Passwords do not match.",
                    false);
                return;
            }


            if (password.Length < 6)
            {
                ShowMessage(
                    "Password must contain at least 6 characters.",
                    false);
                return;
            }


            // ==============================
            // INITIAL DEPOSIT
            // ==============================

            decimal initialDeposit;

            if (!decimal.TryParse(
                depositText,
                out initialDeposit))
            {
                ShowMessage(
                    "Please enter a valid Initial Deposit.",
                    false);
                return;
            }


            if (initialDeposit < 1000)
            {
                ShowMessage(
                    "Minimum initial deposit required is ₹1000.",
                    false);
                return;
            }


            // ==============================
            // SECURITY QUESTION
            // ==============================

            if (securityQuestion == "")
            {
                ShowMessage(
                    "Please select Security Question.",
                    false);
                return;
            }


            if (securityAnswer == "")
            {
                ShowMessage(
                    "Please enter Security Answer.",
                    false);
                return;
            }


            try
            {
                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    con.Open();


                    // ==============================
                    // CHECK ACCOUNT NUMBER
                    // ==============================

                    string checkQuery = @"
                        SELECT COUNT(*)
                        FROM Account
                        WHERE AccountNumber = @AccountNumber";


                    using (SqlCommand checkCmd =
                        new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);


                        int count =
                            Convert.ToInt32(
                                checkCmd.ExecuteScalar());


                        if (count > 0)
                        {
                            ShowMessage(
                                "Account Number already exists.",
                                false);
                            return;
                        }
                    }


                    // ==============================
                    // INSERT ACCOUNT
                    // ==============================
                    // IMPORTANT:
                    // Balance = InitialDeposit

                    string insertQuery = @"
                        INSERT INTO Account
                        (
                            AccountNumber,
                            Password,
                            InitialDeposit,
                            Balance,
                            SecurityQuestion,
                            SecurityAnswer
                        )
                        VALUES
                        (
                            @AccountNumber,
                            @Password,
                            @InitialDeposit,
                            @Balance,
                            @SecurityQuestion,
                            @SecurityAnswer
                        )";


                    using (SqlCommand cmd =
                        new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);

                        cmd.Parameters.AddWithValue(
                            "@Password",
                            password);

                        cmd.Parameters.AddWithValue(
                            "@InitialDeposit",
                            initialDeposit);

                        // Balance starts with initial deposit
                        cmd.Parameters.AddWithValue(
                            "@Balance",
                            initialDeposit);

                        cmd.Parameters.AddWithValue(
                            "@SecurityQuestion",
                            securityQuestion);

                        cmd.Parameters.AddWithValue(
                            "@SecurityAnswer",
                            securityAnswer);


                        int rows =
                            cmd.ExecuteNonQuery();


                        if (rows == 1)
                        {
                            ShowMessage(
                                "Account created successfully. Please login.",
                                true);


                            // CLEAR FIELDS

                            txtAccountNumber.Text = "";

                            txtPassword.Text = "";

                            txtConfirmPassword.Text = "";

                            txtInitialDeposit.Text = "";

                            ddlSecurityQuestion.SelectedIndex = 0;

                            txtSecurityAnswer.Text = "";
                        }
                        else
                        {
                            ShowMessage(
                                "Account creation failed.",
                                false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Database Error: " + ex.Message,
                    false);
            }
        }


        // ==============================
        // BACK TO LOGIN
        // ==============================

        protected void btnLogin_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "Login.aspx",
                false);

            Context.ApplicationInstance
                .CompleteRequest();
        }


        // ==============================
        // MESSAGE
        // ==============================

        private void ShowMessage(
            string message,
            bool success)
        {
            lblMessage.Text = message;


            if (success)
            {
                lblMessage.Style["color"] =
                    "#198754";

                lblMessage.Style["background"] =
                    "#e9f7ef";
            }
            else
            {
                lblMessage.Style["color"] =
                    "#dc3545";

                lblMessage.Style["background"] =
                    "#fdecec";
            }
        }
    }
}
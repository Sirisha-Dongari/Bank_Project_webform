using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BankAccountSimulator
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager
            .ConnectionStrings["BankDBConnection"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlMessage.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string accountNumber =
                txtAccountNumber.Text.Trim();

            string password =
                txtPassword.Text.Trim();

            // Account Number Empty Check
            if (accountNumber == "")
            {
                ShowError("Please enter Account Number.");
                return;
            }

            // Account Number must be 11 to 16 digits
            if (accountNumber.Length < 11 ||
                accountNumber.Length > 16)
            {
                ShowError(
                    "Account Number must contain 11 to 16 digits.");
                return;
            }

            // Account Number should contain numbers only
            foreach (char c in accountNumber)
            {
                if (!char.IsDigit(c))
                {
                    ShowError(
                        "Account Number must contain numbers only.");
                    return;
                }
            }

            // Password Empty Check
            if (password == "")
            {
                ShowError("Please enter Password.");
                return;
            }

            try
            {
                using (SqlConnection con =
                    new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"
                        SELECT COUNT(*)
                        FROM Account
                        WHERE AccountNumber = @AccountNumber
                        AND Password = @Password";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);

                        cmd.Parameters.AddWithValue(
                            "@Password",
                            password);

                        int count =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            Session["AccountNumber"] =
                                accountNumber;

                            Response.Redirect(
                                "Bank.aspx",
                                false);

                            Context.ApplicationInstance
                                .CompleteRequest();
                        }
                        else
                        {
                            ShowError(
                                "Invalid Account Number or Password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    "Database Error: " +
                    ex.Message);
            }
        }

        private void ShowError(string message)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = message;

            lblMessage.CssClass =
                "message error-message";
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankAccountSimulator
{
    public partial class Bank : System.Web.UI.Page
    {
        private readonly string connectionString =
            ConfigurationManager
            .ConnectionStrings["BankDBConnection"]
            .ConnectionString;

        private const decimal MinimumBalance = 1000.00m;

        // =========================================================
        // PAGE LOAD
        // =========================================================

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

            // Check Login
            if (Session["AccountNumber"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string accountNumber =
                    Session["AccountNumber"].ToString();

                lblAccountNumber.Text =
                    accountNumber;

                // Create first transaction
                // using actual Account.Balance
                EnsureInitialDepositTransaction(
                    accountNumber);

                // Load balance from Account table
                LoadCurrentBalance(
                    accountNumber);

                // Load transaction history
                LoadTransactions(
                    accountNumber);
            }
        }


        // =========================================================
        // ENSURE INITIAL DEPOSIT TRANSACTION
        // =========================================================

        private void EnsureInitialDepositTransaction(
            string accountNumber)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                con.Open();

                // Check whether transaction already exists
                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM TransactionHistory
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
                        return;
                    }
                }


                // Get actual balance from Account table
                string balanceQuery = @"
                    SELECT Balance
                    FROM Account
                    WHERE AccountNumber = @AccountNumber";

                decimal initialBalance = 0;

                using (SqlCommand balanceCmd =
                       new SqlCommand(
                           balanceQuery,
                           con))
                {
                    balanceCmd.Parameters.AddWithValue(
                        "@AccountNumber",
                        accountNumber);

                    object result =
                        balanceCmd.ExecuteScalar();

                    if (result != null &&
                        result != DBNull.Value)
                    {
                        initialBalance =
                            Convert.ToDecimal(result);
                    }
                }


                // Insert initial deposit transaction
                string insertQuery = @"
                    INSERT INTO TransactionHistory
                    (
                        AccountNumber,
                        TransactionType,
                        Amount,
                        Balance,
                        TransactionDateTime
                    )
                    VALUES
                    (
                        @AccountNumber,
                        @TransactionType,
                        @Amount,
                        @Balance,
                        @TransactionDateTime
                    )";

                using (SqlCommand cmd =
                       new SqlCommand(
                           insertQuery,
                           con))
                {
                    cmd.Parameters.AddWithValue(
                        "@AccountNumber",
                        accountNumber);

                    cmd.Parameters.AddWithValue(
                        "@TransactionType",
                        "Initial Deposit");

                    cmd.Parameters.AddWithValue(
                        "@Amount",
                        initialBalance);

                    cmd.Parameters.AddWithValue(
                        "@Balance",
                        initialBalance);

                    cmd.Parameters.AddWithValue(
                        "@TransactionDateTime",
                        DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // =========================================================
        // LOAD CURRENT BALANCE
        // =========================================================

        private void LoadCurrentBalance(
            string accountNumber)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
                    SELECT Balance
                    FROM Account
                    WHERE AccountNumber = @AccountNumber";

                using (SqlCommand cmd =
                       new SqlCommand(
                           query,
                           con))
                {
                    cmd.Parameters.AddWithValue(
                        "@AccountNumber",
                        accountNumber);

                    object result =
                        cmd.ExecuteScalar();

                    decimal balance = 0;

                    if (result != null &&
                        result != DBNull.Value)
                    {
                        balance =
                            Convert.ToDecimal(result);
                    }

                    lblBalance.Text =
                        "₹ " +
                        balance.ToString("N2");
                }
            }
        }


        // =========================================================
        // DEPOSIT
        // =========================================================

        protected void btnDeposit_Click(
            object sender,
            EventArgs e)
        {
            decimal amount;

            if (!TryGetAmount(out amount))
            {
                return;
            }

            string accountNumber =
                Session["AccountNumber"].ToString();

            try
            {
                using (SqlConnection con =
                       new SqlConnection(
                           connectionString))
                {
                    con.Open();

                    // Get current balance
                    decimal currentBalance =
                        GetCurrentBalance(
                            con,
                            accountNumber);

                    // Calculate new balance
                    decimal newBalance =
                        currentBalance + amount;


                    // Update Account balance
                    UpdateAccountBalance(
                        con,
                        accountNumber,
                        newBalance);


                    // Insert transaction
                    InsertTransaction(
                        con,
                        accountNumber,
                        "Deposit",
                        amount,
                        newBalance);


                    // Update screen
                    lblBalance.Text =
                        "₹ " +
                        newBalance.ToString("N2");

                    ShowSuccess(
                        "₹" +
                        amount.ToString("N2") +
                        " deposited successfully.");

                    txtAmount.Text = "";

                    LoadTransactions(
                        accountNumber);
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    "Database Error: " +
                    ex.Message);
            }
        }


        // =========================================================
        // WITHDRAW
        // =========================================================

        protected void btnWithdraw_Click(
            object sender,
            EventArgs e)
        {
            decimal amount;

            if (!TryGetAmount(out amount))
            {
                return;
            }

            string accountNumber =
                Session["AccountNumber"].ToString();

            try
            {
                using (SqlConnection con =
                       new SqlConnection(
                           connectionString))
                {
                    con.Open();

                    // Get current balance
                    decimal currentBalance =
                        GetCurrentBalance(
                            con,
                            accountNumber);


                    // Check minimum balance
                    if (amount >
                        currentBalance -
                        MinimumBalance)
                    {
                        ShowError(
                            "Insufficient balance. " +
                            "Minimum balance of ₹1,000 " +
                            "must be maintained.");

                        return;
                    }


                    // Calculate new balance
                    decimal newBalance =
                        currentBalance - amount;


                    // Update Account balance
                    UpdateAccountBalance(
                        con,
                        accountNumber,
                        newBalance);


                    // Insert transaction
                    InsertTransaction(
                        con,
                        accountNumber,
                        "Withdraw",
                        amount,
                        newBalance);


                    // Update screen
                    lblBalance.Text =
                        "₹ " +
                        newBalance.ToString("N2");

                    ShowSuccess(
                        "₹" +
                        amount.ToString("N2") +
                        " withdrawn successfully.");

                    txtAmount.Text = "";

                    LoadTransactions(
                        accountNumber);
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    "Database Error: " +
                    ex.Message);
            }
        }


        // =========================================================
        // GET AMOUNT
        // =========================================================

        private bool TryGetAmount(
            out decimal amount)
        {
            amount = 0;

            string value =
                txtAmount.Text.Trim();

            if (string.IsNullOrEmpty(value))
            {
                ShowError(
                    "Please enter an amount.");

                return false;
            }


            if (!decimal.TryParse(
                    value,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out amount))
            {
                ShowError(
                    "Please enter a valid amount.");

                return false;
            }


            if (amount <= 0)
            {
                ShowError(
                    "Amount must be greater than ₹0.");

                return false;
            }


            return true;
        }


        // =========================================================
        // GET CURRENT BALANCE FROM ACCOUNT TABLE
        // =========================================================

        private decimal GetCurrentBalance(
            SqlConnection con,
            string accountNumber)
        {
            string query = @"
                SELECT Balance
                FROM Account
                WHERE AccountNumber = @AccountNumber";

            using (SqlCommand cmd =
                   new SqlCommand(
                       query,
                       con))
            {
                cmd.Parameters.AddWithValue(
                    "@AccountNumber",
                    accountNumber);

                object result =
                    cmd.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToDecimal(result);
            }
        }


        // =========================================================
        // UPDATE ACCOUNT BALANCE
        // =========================================================

        private void UpdateAccountBalance(
            SqlConnection con,
            string accountNumber,
            decimal newBalance)
        {
            string query = @"
                UPDATE Account
                SET Balance = @Balance
                WHERE AccountNumber = @AccountNumber";

            using (SqlCommand cmd =
                   new SqlCommand(
                       query,
                       con))
            {
                cmd.Parameters.AddWithValue(
                    "@Balance",
                    newBalance);

                cmd.Parameters.AddWithValue(
                    "@AccountNumber",
                    accountNumber);

                cmd.ExecuteNonQuery();
            }
        }


        // =========================================================
        // INSERT TRANSACTION
        // =========================================================

        private void InsertTransaction(
            SqlConnection con,
            string accountNumber,
            string transactionType,
            decimal amount,
            decimal balance)
        {
            string query = @"
                INSERT INTO TransactionHistory
                (
                    AccountNumber,
                    TransactionType,
                    Amount,
                    Balance,
                    TransactionDateTime
                )
                VALUES
                (
                    @AccountNumber,
                    @TransactionType,
                    @Amount,
                    @Balance,
                    @TransactionDateTime
                )";

            using (SqlCommand cmd =
                   new SqlCommand(
                       query,
                       con))
            {
                cmd.Parameters.AddWithValue(
                    "@AccountNumber",
                    accountNumber);

                cmd.Parameters.AddWithValue(
                    "@TransactionType",
                    transactionType);

                cmd.Parameters.AddWithValue(
                    "@Amount",
                    amount);

                cmd.Parameters.AddWithValue(
                    "@Balance",
                    balance);

                cmd.Parameters.AddWithValue(
                    "@TransactionDateTime",
                    DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }


        // =========================================================
        // LOAD TRANSACTIONS
        // =========================================================

        private void LoadTransactions(
            string accountNumber)
        {
            using (SqlConnection con =
                   new SqlConnection(
                       connectionString))
            {
                con.Open();

                string query = @"
                    SELECT
                        ROW_NUMBER() OVER
                        (
                            ORDER BY TransactionId ASC
                        ) AS SequenceID,

                        TransactionType,
                        Amount,
                        Balance,
                        TransactionDateTime

                    FROM TransactionHistory

                    WHERE AccountNumber =
                        @AccountNumber

                    ORDER BY TransactionId ASC";

                using (SqlCommand cmd =
                       new SqlCommand(
                           query,
                           con))
                {
                    cmd.Parameters.AddWithValue(
                        "@AccountNumber",
                        accountNumber);

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        DataTable dt =
                            new DataTable();

                        da.Fill(dt);

                        gvTransactions.DataSource =
                            dt;

                        gvTransactions.DataBind();
                    }
                }
            }
        }


        // =========================================================
        // FILTER TRANSACTIONS BY DATE
        // =========================================================

        protected void btnFilter_Click(
            object sender,
            EventArgs e)
        {
            string accountNumber =
                Session["AccountNumber"].ToString();

            DateTime fromDate;
            DateTime toDate;


            if (string.IsNullOrEmpty(
                txtFromDate.Text))
            {
                ShowError(
                    "Please select From Date.");

                return;
            }


            if (string.IsNullOrEmpty(
                txtToDate.Text))
            {
                ShowError(
                    "Please select To Date.");

                return;
            }


            if (!DateTime.TryParse(
                txtFromDate.Text,
                out fromDate))
            {
                ShowError(
                    "Invalid From Date.");

                return;
            }


            if (!DateTime.TryParse(
                txtToDate.Text,
                out toDate))
            {
                ShowError(
                    "Invalid To Date.");

                return;
            }


            if (fromDate.Date >
                toDate.Date)
            {
                ShowError(
                    "From Date cannot be greater than To Date.");

                return;
            }


            toDate =
                toDate.Date
                .AddDays(1)
                .AddTicks(-1);


            try
            {
                using (SqlConnection con =
                       new SqlConnection(
                           connectionString))
                {
                    con.Open();

                    string query = @"
                        SELECT
                            ROW_NUMBER() OVER
                            (
                                ORDER BY TransactionId ASC
                            ) AS SequenceID,

                            TransactionType,
                            Amount,
                            Balance,
                            TransactionDateTime

                        FROM TransactionHistory

                        WHERE AccountNumber =
                            @AccountNumber

                        AND TransactionDateTime
                            BETWEEN @FromDate
                            AND @ToDate

                        ORDER BY TransactionId ASC";

                    using (SqlCommand cmd =
                           new SqlCommand(
                               query,
                               con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AccountNumber",
                            accountNumber);

                        cmd.Parameters.AddWithValue(
                            "@FromDate",
                            fromDate.Date);

                        cmd.Parameters.AddWithValue(
                            "@ToDate",
                            toDate);


                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            DataTable dt =
                                new DataTable();

                            da.Fill(dt);

                            gvTransactions.DataSource =
                                dt;

                            gvTransactions.DataBind();
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


        // =========================================================
        // CLEAR FILTER
        // =========================================================

        protected void btnClear_Click(
            object sender,
            EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";

            string accountNumber =
                Session["AccountNumber"].ToString();

            LoadTransactions(
                accountNumber);

            lblMessage.Text = "";

            lblMessage.Style["background"] =
                "transparent";
        }


        // =========================================================
        // LOGOUT
        // =========================================================

        protected void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(
                "Login.aspx",
                false);

            Context.ApplicationInstance
                .CompleteRequest();
        }


        // =========================================================
        // TRANSACTION ROW COLOR
        // =========================================================

        protected void gvTransactions_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType ==
                DataControlRowType.DataRow)
            {
                string type =
                    DataBinder.Eval(
                        e.Row.DataItem,
                        "TransactionType")
                    .ToString();


                if (type == "Deposit" ||
                    type == "Initial Deposit")
                {
                    e.Row.Cells[1]
                        .Style["color"] =
                        "#198754";

                    e.Row.Cells[1]
                        .Style["font-weight"] =
                        "700";
                }
                else if (type == "Withdraw")
                {
                    e.Row.Cells[1]
                        .Style["color"] =
                        "#dc3545";

                    e.Row.Cells[1]
                        .Style["font-weight"] =
                        "700";
                }
                else
                {
                    e.Row.Cells[1]
                        .Style["color"] =
                        "#0d6efd";

                    e.Row.Cells[1]
                        .Style["font-weight"] =
                        "700";
                }
            }
        }


        // =========================================================
        // SUCCESS MESSAGE
        // =========================================================

        private void ShowSuccess(
            string message)
        {
            lblMessage.Text =
                message;

            lblMessage.Style["color"] =
                "#198754";

            lblMessage.Style["background"] =
                "#e9f7ef";
        }


        // =========================================================
        // ERROR MESSAGE
        // =========================================================

        private void ShowError(
            string message)
        {
            lblMessage.Text =
                message;

            lblMessage.Style["color"] =
                "#dc3545";

            lblMessage.Style["background"] =
                "#fdecec";
        }
    }
}
<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Bank.aspx.cs"
    Inherits="BankAccountSimulator.Bank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Bank Account Simulator</title>

    <style type="text/css">

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        body {
            min-height: 100vh;
            background: linear-gradient(135deg, #0b3d78, #1976d2);
            padding: 18px;
        }

        .main-container {
            width: 96%;
            max-width: 1450px;
            margin: 0 auto;
        }


        /* =====================================================
           HEADER
        ===================================================== */

        .header {
            background: white;
            border-radius: 16px;
            padding: 18px 25px;
            margin-bottom: 18px;

            display: flex;
            justify-content: space-between;
            align-items: center;

            box-shadow: 0 8px 22px rgba(0,0,0,0.15);
        }

        .header-left {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .bank-icon {
            font-size: 38px;
        }

        .header-title {
            color: #123f78;
            font-size: 28px;
            font-weight: 700;
        }

        .welcome {
            color: #777;
            font-size: 13px;
            margin-top: 3px;
        }

        .logout-btn {
            border: none;
            background: #dc3545;
            color: white;

            padding: 10px 20px;

            border-radius: 8px;

            font-size: 13px;
            font-weight: 700;

            cursor: pointer;
        }

        .logout-btn:hover {
            background: #bb2d3b;
        }


        /* =====================================================
           MAIN CONTENT
        ===================================================== */

        .content {
            display: grid;

            grid-template-columns:
                minmax(330px, 38%)
                minmax(500px, 62%);

            gap: 18px;
        }

        .card {
            background: white;

            border-radius: 16px;

            padding: 22px;

            box-shadow:
                0 8px 22px rgba(0,0,0,0.15);
        }


        /* =====================================================
           SECTION TITLE
        ===================================================== */

        .section-title {
            color: #123f78;

            font-size: 21px;

            font-weight: 700;

            margin-bottom: 18px;
        }


        /* =====================================================
           BALANCE
        ===================================================== */

        .balance-box {
            background: #eaf4ff;

            border: 1px solid #c6e0ff;

            border-radius: 13px;

            padding: 18px;

            text-align: center;

            margin-bottom: 20px;
        }

        .balance-label {
            color: #607d9d;

            font-size: 13px;

            font-weight: 600;

            margin-bottom: 6px;
        }

        .balance-value {
            color: #1976d2;

            font-size: 32px;

            font-weight: 700;
        }


        /* =====================================================
           ACCOUNT NUMBER
        ===================================================== */

        .account-box {
            background: #f7f9fc;

            border: 1px solid #e0e6ed;

            border-radius: 9px;

            padding: 11px 13px;

            margin-bottom: 18px;
        }

        .account-label {
            color: #777;

            font-size: 11px;

            margin-bottom: 3px;
        }

        .account-number {
            color: #24415e;

            font-size: 15px;

            font-weight: 700;
        }


        /* =====================================================
           AMOUNT
        ===================================================== */

        .input-label {
            display: block;

            color: #24415e;

            font-size: 13px;

            font-weight: 600;

            margin-bottom: 6px;
        }

        .amount-input {
            width: 100%;

            height: 42px;

            border: 1px solid #ccd5df;

            border-radius: 8px;

            padding: 0 12px;

            font-size: 14px;

            outline: none;

            margin-bottom: 14px;
        }

        .amount-input:focus {
            border-color: #1976d2;

            box-shadow:
                0 0 0 3px rgba(25,118,210,0.10);
        }


        /* =====================================================
           DEPOSIT / WITHDRAW
        ===================================================== */

        .button-row {
            display: grid;

            grid-template-columns:
                1fr 1fr;

            gap: 10px;
        }

        .deposit-btn,
        .withdraw-btn {

            height: 40px;

            border: none;

            border-radius: 8px;

            color: white;

            font-size: 13px;

            font-weight: 700;

            cursor: pointer;
        }

        .deposit-btn {
            background: #198754;
        }

        .deposit-btn:hover {
            background: #157347;
        }

        .withdraw-btn {
            background: #dc3545;
        }

        .withdraw-btn:hover {
            background: #bb2d3b;
        }


        /* =====================================================
           MESSAGE
        ===================================================== */

        .message {
            display: block;

            margin-top: 13px;

            padding: 9px;

            border-radius: 7px;

            text-align: center;

            font-size: 12px;

            font-weight: 600;
        }


        /* =====================================================
           TRANSACTION HEADER
        ===================================================== */

        .transaction-header {
            display: flex;

            justify-content: space-between;

            align-items: center;

            margin-bottom: 15px;
        }


        /* =====================================================
           DATE FILTER
        ===================================================== */

        .filter-row {
            display: grid;

            grid-template-columns:
                1fr
                1fr
                auto
                auto;

            gap: 8px;

            align-items: end;

            margin-bottom: 15px;
        }

        .date-group label {
            display: block;

            color: #24415e;

            font-size: 11px;

            font-weight: 600;

            margin-bottom: 5px;
        }

        .date-input {
            width: 100%;

            height: 38px;

            border: 1px solid #ccd5df;

            border-radius: 7px;

            padding: 0 9px;

            font-size: 12px;

            outline: none;
        }

        .date-input:focus {
            border-color: #1976d2;
        }

        .filter-btn,
        .clear-btn {

            height: 38px;

            border: none;

            border-radius: 7px;

            padding: 0 16px;

            color: white;

            font-size: 12px;

            font-weight: 700;

            cursor: pointer;
        }

        .filter-btn {
            background: #1976d2;
        }

        .filter-btn:hover {
            background: #0d57a5;
        }

        .clear-btn {
            background: #6c757d;
        }

        .clear-btn:hover {
            background: #5c636a;
        }


        /* =====================================================
           TRANSACTION TABLE
        ===================================================== */

        .table-wrapper {
            width: 100%;

            overflow-x: auto;

            max-height: 430px;

            overflow-y: auto;

            border-radius: 9px;

            border: 1px solid #e0e6ed;
        }

        .transaction-grid {
            width: 100%;

            border-collapse: collapse;

            font-size: 12px;

            background: white;
        }

        .transaction-grid th {
            background: #174b82;

            color: white;

            padding: 10px 9px;

            text-align: left;

            font-size: 12px;

            font-weight: 700;

            position: sticky;

            top: 0;

            z-index: 2;
        }

        .transaction-grid td {
            padding: 9px;

            border-bottom:
                1px solid #e8edf2;

            color: #394b5e;

            white-space: nowrap;
        }

        .transaction-grid tr:hover {
            background: #f5faff;
        }


        /* =====================================================
           ID
        ===================================================== */

        .transaction-id {
            font-weight: 700;

            color: #123f78;
        }


        /* =====================================================
           FOOTER
        ===================================================== */

        .footer {
            text-align: center;

            color: rgba(255,255,255,0.75);

            font-size: 10px;

            margin-top: 14px;
        }


        /* =====================================================
           MOBILE / SMALL SCREEN
        ===================================================== */

        @media (max-width: 1000px) {

            body {
                padding: 12px;
            }

            .content {
                grid-template-columns: 1fr;
            }

            .header {
                padding: 15px 18px;
            }

            .header-title {
                font-size: 23px;
            }

            .bank-icon {
                font-size: 30px;
            }
        }


        @media (max-width: 650px) {

            .header {
                flex-direction: column;

                gap: 12px;

                text-align: center;
            }

            .header-left {
                flex-direction: column;

                gap: 5px;
            }

            .filter-row {
                grid-template-columns: 1fr 1fr;
            }

            .filter-btn,
            .clear-btn {
                width: 100%;
            }
        }

    </style>

</head>


<body>

<form id="form1" runat="server">


    <div class="main-container">


        <!-- =====================================================
             HEADER
        ====================================================== -->

        <div class="header">

            <div class="header-left">

                <div class="bank-icon">
                    🏦
                </div>

                <div>

                    <div class="header-title">
                        Bank Account Simulator
                    </div>

                    <div class="welcome">
                        Welcome to your account
                    </div>

                </div>

            </div>


            <asp:Button
                ID="btnLogout"
                runat="server"
                Text="Logout"
                CssClass="logout-btn"
                OnClick="btnLogout_Click" />

        </div>



        <!-- =====================================================
             MAIN CONTENT
        ====================================================== -->

        <div class="content">


            <!-- =================================================
                 ACCOUNT OPERATIONS
            ================================================== -->

            <div class="card">


                <div class="section-title">
                    Account Operations
                </div>


                <!-- ACCOUNT NUMBER -->

                <div class="account-box">

                    <div class="account-label">
                        Account Number
                    </div>

                    <asp:Label
                        ID="lblAccountNumber"
                        runat="server"
                        CssClass="account-number">
                    </asp:Label>

                </div>


                <!-- CURRENT BALANCE -->

                <div class="balance-box">

                    <div class="balance-label">
                        Current Balance
                    </div>

                    <asp:Label
                        ID="lblBalance"
                        runat="server"
                        CssClass="balance-value">
                        ₹ 0.00
                    </asp:Label>

                </div>


                <!-- ENTER AMOUNT -->

                <label
                    class="input-label">

                    Enter Amount

                </label>


                <asp:TextBox
                    ID="txtAmount"
                    runat="server"
                    CssClass="amount-input"
                    placeholder="Enter amount">
                </asp:TextBox>


                <!-- DEPOSIT / WITHDRAW -->

                <div class="button-row">


                    <asp:Button
                        ID="btnDeposit"
                        runat="server"
                        Text="Deposit"
                        CssClass="deposit-btn"
                        OnClick="btnDeposit_Click" />


                    <asp:Button
                        ID="btnWithdraw"
                        runat="server"
                        Text="Withdraw"
                        CssClass="withdraw-btn"
                        OnClick="btnWithdraw_Click" />

                </div>


                <!-- MESSAGE -->

                <asp:Label
                    ID="lblMessage"
                    runat="server"
                    CssClass="message">
                </asp:Label>


            </div>



            <!-- =================================================
                 TRANSACTION HISTORY
            ================================================== -->

            <div class="card">


                <div class="transaction-header">

                    <div class="section-title"
                         style="margin-bottom:0;">

                        Transaction History

                    </div>

                </div>


                <!-- DATE FILTER -->

                <div class="filter-row">


                    <div class="date-group">

                        <label>
                            From Date
                        </label>

                        <asp:TextBox
                            ID="txtFromDate"
                            runat="server"
                            CssClass="date-input"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>


                    <div class="date-group">

                        <label>
                            To Date
                        </label>

                        <asp:TextBox
                            ID="txtToDate"
                            runat="server"
                            CssClass="date-input"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>


                    <asp:Button
                        ID="btnFilter"
                        runat="server"
                        Text="Filter"
                        CssClass="filter-btn"
                        OnClick="btnFilter_Click" />


                    <asp:Button
                        ID="btnClear"
                        runat="server"
                        Text="Clear"
                        CssClass="clear-btn"
                        OnClick="btnClear_Click" />

                </div>



                <!-- TRANSACTION TABLE -->

                <div class="table-wrapper">


                    <asp:GridView
                        ID="gvTransactions"
                        runat="server"
                        CssClass="transaction-grid"
                        AutoGenerateColumns="False"
                        GridLines="None"
                        EmptyDataText="No transactions found."
                        OnRowDataBound="gvTransactions_RowDataBound">


                        <Columns>


                            <asp:BoundField
                                DataField="SequenceID"
                                HeaderText="ID"
                                ItemStyle-CssClass="transaction-id" />


                            <asp:BoundField
                                DataField="TransactionType"
                                HeaderText="Type" />


                            <asp:BoundField
                                DataField="Amount"
                                HeaderText="Amount"
                                DataFormatString="₹ {0:N2}" />


                            <asp:BoundField
                                DataField="Balance"
                                HeaderText="Balance"
                                DataFormatString="₹ {0:N2}" />


                            <asp:BoundField
                                DataField="TransactionDateTime"
                                HeaderText="Date &amp; Time"
                                DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />


                        </Columns>


                    </asp:GridView>


                </div>


            </div>


        </div>


        <!-- =====================================================
             FOOTER
        ====================================================== -->

        <div class="footer">

            Secure Bank Account Simulator

        </div>


    </div>


</form>

</body>

</html>
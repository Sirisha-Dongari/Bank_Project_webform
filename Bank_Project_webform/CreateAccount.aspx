<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="CreateAccount.aspx.cs"
    Inherits="BankAccountSimulator.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Create Account - Bank Account Simulator</title>

    <style>

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
        }

        body {
            min-height: 100vh;
            background: linear-gradient(135deg, #0b3d78, #1976d2);
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 30px 15px;
        }

        .account-box {
            width: 460px;
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 10px 35px rgba(0,0,0,0.25);
        }

        .title {
            text-align: center;
            color: #123f78;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 6px;
        }

        .subtitle {
            text-align: center;
            color: #777;
            font-size: 13px;
            margin-bottom: 25px;
        }

        .form-group {
            margin-bottom: 16px;
        }

        .form-group label {
            display: block;
            color: #24415e;
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 7px;
        }

        .input {
            width: 100%;
            height: 42px;
            border: 1px solid #cbd5e1;
            border-radius: 7px;
            padding: 0 12px;
            font-size: 14px;
            outline: none;
        }

        .input:focus {
            border-color: #1976d2;
        }

        .select {
            width: 100%;
            height: 42px;
            border: 1px solid #cbd5e1;
            border-radius: 7px;
            padding: 0 10px;
            font-size: 14px;
            background: white;
            outline: none;
        }

        .select:focus {
            border-color: #1976d2;
        }

        .deposit-note {
            margin-top: -8px;
            margin-bottom: 15px;
            color: #198754;
            font-size: 12px;
            font-weight: bold;
        }

        .create-btn {
            width: 100%;
            height: 44px;
            border: none;
            border-radius: 7px;
            background: #1976d2;
            color: white;
            font-size: 15px;
            font-weight: bold;
            cursor: pointer;
        }

        .create-btn:hover {
            background: #125ca8;
        }

        .login-btn {
            width: 100%;
            height: 42px;
            margin-top: 10px;
            border: none;
            border-radius: 7px;
            background: #6c757d;
            color: white;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
        }

        .login-btn:hover {
            background: #5c636a;
        }

        .message {
            display: block;
            text-align: center;
            margin-top: 14px;
            padding: 9px;
            border-radius: 6px;
            font-size: 13px;
        }

        .footer {
            text-align: center;
            margin-top: 18px;
            color: #777;
            font-size: 12px;
        }

    </style>

</head>

<body>

    <form id="form1" runat="server">

        <div class="account-box">

            <div class="title">
                Create Account
            </div>

            <div class="subtitle">
                Open your bank account securely
            </div>


            <!-- Account Number -->

            <div class="form-group">

                <label>Account Number</label>

                <asp:TextBox
                    ID="txtAccountNumber"
                    runat="server"
                    CssClass="input"
                    placeholder="Enter Account Number">
                </asp:TextBox>

            </div>


            <!-- Password -->

            <div class="form-group">

                <label>Password</label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    CssClass="input"
                    TextMode="Password"
                    placeholder="Enter Password">
                </asp:TextBox>

            </div>


            <!-- Confirm Password -->

            <div class="form-group">

                <label>Confirm Password</label>

                <asp:TextBox
                    ID="txtConfirmPassword"
                    runat="server"
                    CssClass="input"
                    TextMode="Password"
                    placeholder="Confirm Password">
                </asp:TextBox>

            </div>


            <!-- Initial Deposit -->

            <div class="form-group">

                <label>Initial Deposit</label>

                <asp:TextBox
                    ID="txtInitialDeposit"
                    runat="server"
                    CssClass="input"
                    placeholder="Enter Initial Deposit">
                </asp:TextBox>

            </div>

            <div class="deposit-note">
                💰 Minimum initial deposit required: ₹1000
            </div>


            <!-- Security Question -->

            <div class="form-group">

                <label>Security Question</label>

                <asp:DropDownList
                    ID="ddlSecurityQuestion"
                    runat="server"
                    CssClass="select">

                    <asp:ListItem
                        Text="-- Select Security Question --"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="What is your favorite color?"
                        Value="What is your favorite color?">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="What is your favorite food?"
                        Value="What is your favorite food?">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="What is your favorite city?"
                        Value="What is your favorite city?">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="What is your pet name?"
                        Value="What is your pet name?">
                    </asp:ListItem>

                </asp:DropDownList>

            </div>


            <!-- Security Answer -->

            <div class="form-group">

                <label>Security Answer</label>

                <asp:TextBox
                    ID="txtSecurityAnswer"
                    runat="server"
                    CssClass="input"
                    placeholder="Enter Security Answer">
                </asp:TextBox>

            </div>


            <!-- Create Account Button -->

            <asp:Button
                ID="btnCreateAccount"
                runat="server"
                Text="Create Account"
                CssClass="create-btn"
                OnClick="btnCreateAccount_Click" />


            <!-- Login Button -->

            <asp:Button
                ID="btnLogin"
                runat="server"
                Text="← Back to Login"
                CssClass="login-btn"
                OnClick="btnLogin_Click" />


            <!-- Message -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>


            <div class="footer">
                Secure Bank Account Simulator
            </div>

        </div>

    </form>

</body>
</html>
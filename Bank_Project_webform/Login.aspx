<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="BankAccountSimulator.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Login - Bank Account Simulator</title>

    <style>

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        body {
            min-height: 100vh;
            background: linear-gradient(135deg, #0b3d78, #1976d2);
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }

        .login-card {
            width: 100%;
            max-width: 380px;
            background: white;
            border-radius: 16px;
            padding: 28px 30px;
            box-shadow: 0 15px 35px rgba(0,0,0,0.22);
        }

        .logo {
            text-align: center;
            font-size: 38px;
            margin-bottom: 5px;
        }

        .title {
            text-align: center;
            color: #123f78;
            font-size: 24px;
            font-weight: 700;
            margin-bottom: 5px;
        }

        .subtitle {
            text-align: center;
            color: #777;
            font-size: 13px;
            margin-bottom: 22px;
        }

        .form-group {
            margin-bottom: 16px;
        }

        .form-group label {
            display: block;
            color: #24415e;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 6px;
        }

        .input-box {
            width: 100%;
            height: 44px;
            border: 1px solid #ccd5df;
            border-radius: 8px;
            padding: 0 12px;
            font-size: 15px;
            outline: none;
        }

        .input-box:focus {
            border-color: #1976d2;
            box-shadow: 0 0 0 3px rgba(25,118,210,0.10);
        }

        .login-btn {
            width: 100%;
            height: 44px;
            border: none;
            border-radius: 8px;
            background: #176cc4;
            color: white;
            font-size: 15px;
            font-weight: 700;
            cursor: pointer;
            margin-top: 5px;
            transition: 0.2s;
        }

        .login-btn:hover {
            background: #0d57a5;
            transform: translateY(-1px);
        }

        .message {
            display: block;
            margin-top: 14px;
            padding: 10px;
            border-radius: 7px;
            text-align: center;
            font-size: 13px;
            font-weight: 600;
        }

        .error-message {
            color: #dc3545;
            background: #fdecec;
        }

        .forgot-password {
            text-align: center;
            margin-top: 12px;
            font-size: 13px;
        }

        .forgot-password a {
            color: #176cc4;
            font-weight: 700;
            text-decoration: none;
        }

        .forgot-password a:hover {
            text-decoration: underline;
        }

        .create-account {
            text-align: center;
            margin-top: 16px;
            font-size: 13px;
            color: #666;
        }

        .create-account a {
            color: #176cc4;
            font-weight: 700;
            text-decoration: none;
        }

        .create-account a:hover {
            text-decoration: underline;
        }

        .footer {
            text-align: center;
            margin-top: 18px;
            color: #999;
            font-size: 11px;
        }

        @media (max-width: 450px) {

            body {
                padding: 15px;
            }

            .login-card {
                max-width: 340px;
                padding: 24px 22px;
            }

            .title {
                font-size: 22px;
            }

            .logo {
                font-size: 34px;
            }

        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="login-card">

        <!-- LOGO -->

        <div class="logo">
            🏦
        </div>

        <!-- TITLE -->

        <div class="title">
            Bank Account Simulator
        </div>

        <div class="subtitle">
            Login to access your account
        </div>

        <!-- ACCOUNT NUMBER -->

        <div class="form-group">

            <label>
                Account Number
            </label>

            <asp:TextBox
                ID="txtAccountNumber"
                runat="server"
                CssClass="input-box"
                placeholder="Enter account number">
            </asp:TextBox>

        </div>

        <!-- PASSWORD -->

        <div class="form-group">

            <label>
                Password
            </label>

            <asp:TextBox
                ID="txtPassword"
                runat="server"
                CssClass="input-box"
                TextMode="Password"
                placeholder="Enter password">
            </asp:TextBox>

        </div>

        <!-- LOGIN BUTTON -->

        <asp:Button
            ID="btnLogin"
            runat="server"
            Text="Login"
            CssClass="login-btn"
            OnClick="btnLogin_Click" />

        <!-- MESSAGE -->

        <asp:Panel
            ID="pnlMessage"
            runat="server"
            Visible="false">

            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>

        </asp:Panel>

        <!-- FORGOT PASSWORD -->

        <div class="forgot-password">

            <asp:HyperLink
                ID="lnkForgotPassword"
                runat="server"
                NavigateUrl="ForgotPassword.aspx">

                Forgot Password?

            </asp:HyperLink>

        </div>

        <!-- CREATE ACCOUNT -->

        <div class="create-account">

            Don't have an account?

            <asp:HyperLink
                ID="lnkCreateAccount"
                runat="server"
                NavigateUrl="CreateAccount.aspx">

                Create your account

            </asp:HyperLink>

        </div>

        <!-- FOOTER -->

        <div class="footer">

            Secure Bank Account Simulator

        </div>

    </div>

</form>

</body>

</html>
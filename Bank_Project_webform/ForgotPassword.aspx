<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs"
    Inherits="BankAccountSimulator.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Forgot Password</title>

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
        }

        .forgot-box {
            width: 390px;
            background: white;
            padding: 28px;
            border-radius: 14px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.25);
        }

        .title {
            text-align: center;
            color: #123f78;
            font-size: 26px;
            font-weight: bold;
            margin-bottom: 6px;
        }

        .subtitle {
            text-align: center;
            color: #777;
            font-size: 13px;
            margin-bottom: 22px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            color: #24415e;
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 6px;
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
        }

        .reset-button {
            width: 100%;
            height: 42px;
            border: none;
            border-radius: 7px;
            background: #198754;
            color: white;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
        }

        .reset-button:hover {
            background: #157347;
        }

        .back-button {
            width: 100%;
            height: 40px;
            margin-top: 9px;
            border: none;
            border-radius: 7px;
            background: #6c757d;
            color: white;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
        }

        .back-button:hover {
            background: #5c636a;
        }

        .message {
            display: block;
            text-align: center;
            margin-top: 13px;
            padding: 9px;
            border-radius: 6px;
            font-size: 13px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="forgot-box">

        <div class="title">
            🔐 Forgot Password
        </div>

        <div class="subtitle">
            Reset your account password
        </div>

        <div class="form-group">

            <label>Account Number</label>

            <asp:TextBox
                ID="txtAccountNumber"
                runat="server"
                CssClass="input"
                placeholder="Enter Account Number">
            </asp:TextBox>

        </div>

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

        <div class="form-group">

            <label>Security Answer</label>

            <asp:TextBox
                ID="txtSecurityAnswer"
                runat="server"
                CssClass="input"
                placeholder="Enter Security Answer">
            </asp:TextBox>

        </div>

        <div class="form-group">

            <label>New Password</label>

            <asp:TextBox
                ID="txtNewPassword"
                runat="server"
                CssClass="input"
                TextMode="Password"
                placeholder="Enter New Password">
            </asp:TextBox>

        </div>

        <div class="form-group">

            <label>Confirm Password</label>

            <asp:TextBox
                ID="txtConfirmPassword"
                runat="server"
                CssClass="input"
                TextMode="Password"
                placeholder="Confirm New Password">
            </asp:TextBox>

        </div>

        <asp:Button
            ID="btnResetPassword"
            runat="server"
            Text="Reset Password"
            CssClass="reset-button"
            OnClick="btnResetPassword_Click" />

        <asp:Button
            ID="btnBack"
            runat="server"
            Text="← Back to Login"
            CssClass="back-button"
            OnClick="btnBack_Click" />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

</form>

</body>

</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Presentation.Views.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Join my Bank!!!</h1>
            <nav align="right"><asp:Button ID="createButton" runat="server" Text="Create Account"  BorderStyle="Solid" BorderColor="Black" BackColor="White" PostBackUrl="~/Views/CreateAccount.aspx"></asp:Button></nav>
        </div>

        <div align="right">
            <p>Possibly Secure Log In</p>
            <p>UserName: <asp:TextBox ID="usernameTextBox" runat="server" MaxLength="50"></asp:TextBox><br />
                Password: <asp:TextBox ID="passwordTextBox" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="loginErrorLabel" Text="" runat="server" style="color:red"></asp:Label>
            </p>
            <asp:Button ID="loginButton" runat="server" Text="Log In" OnClick="loginButton_Click" />
        </div>
        <asp:Button ID="Button1" runat="server" PostBackUrl="~/Views/AfterThoughts.aspx" Text="After Thoughts" />
    </form>
</body>
</html>

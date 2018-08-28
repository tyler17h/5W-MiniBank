<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="Presentation.Views.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the Bank!</h1>
            <h3>Fill out the info below</h3>
        </div>

        <div>
            Enter Name: <asp:TextBox ID="nameCreateTextBox" runat="server" align-to="usernameCreate" MaxLength="10"></asp:TextBox><br />
            Create Username: <asp:TextBox ID="usernameTextBox" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="usernameExistLabel" Text="" runat="server" style="color:red"></asp:Label><br />
            Create Password: <asp:TextBox ID="passwordTextBox" runat="server" MaxLength="50"></asp:TextBox>
            <p><asp:Button ID="createAccountButton" runat="server" Text="Create Account"  OnClick="createAccountButton_Click"/></p>
            <p>&nbsp;</p>
            <p>
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Views/AfterThoughts.aspx" Text="After Thoughts" />
            </p>

        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Presentation.Views.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the home page</h1> 
            <asp:Button ID="signOutButton" runat="server" Text="SignOut" style="float:right" PostBackUrl="~/Views/login.aspx"/>
            <h3>Signed in as <asp:Label ID="usernameLabel" runat="server" Text="" Font-Names="verdana" style="color:BlueViolet "></asp:Label></h3>
            
            <table border="1">
                <tr>
                    <th>User <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label></th>
                    <th>User's Id: <asp:Label ID="idLabel" runat="server" Text=""></asp:Label></th>                    
                </tr>
            </table>
        </div>
        <br />

        <div>
            <asp:Button ID="editAccountButton" runat="server" Text="Edit Accounts" OnClick="editAccountButton_Click"/>
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click"/>
        </div>

            <div style="float:left; margin:10px">
                <asp:GridView ID="userAccountGridView" runat="server" OnRowCommand="editAccountGridView_RowCommand">
                    <Columns>
                        <asp:ButtonField Text="Select" />
                    </Columns>
                </asp:GridView>
            </div>
            <div style="float:left; margin:10px; width: 527px;">
                <p><asp:Button ID="createAccountButton" runat="server" Text="Create Account" OnClick="createAccountButton_Click" />
                    <asp:TextBox ID="editAccountTextBox" runat="server" Width="250px" placeholder="place holding"></asp:TextBox>
                    <asp:Label ID="accountUpdateLabel"  runat="server"></asp:Label>
                </p>

                <p><asp:Button ID="deleteAccountButton" runat="server" Text="Delete Account" OnClick="deleteAccountButton_Click" /></p>

                <p><asp:Button ID="withdrawlButton" runat="server" Text="Withdrawal" OnClick="withdrawlButton_Click" /></p>
            
                <p><asp:Button ID="depositeButton" runat="server" Text="Deposit" OnClick="depositeButton_Click" /></p>
            </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Views/AfterThoughts.aspx" Text="After Thoughts" />
        </p>
    </form>
</body>
</html>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation.Views
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            var userLogin = new BankDTO.CustomerDTO();
            userLogin.Username = usernameTextBox.Text;
            userLogin.Password = passwordTextBox.Text;

            if (Domain.Manager.ValidateLogin(userLogin) == true)
            {
                Session["username"] = userLogin.Username;
                Session["password"] = userLogin.Password;
                Response.Redirect("http://localhost:53854/Views/Home.aspx");
            }
            else
            {
                loginErrorLabel.Text = "Incorrect Username or Password";
            }
        }
    }
}
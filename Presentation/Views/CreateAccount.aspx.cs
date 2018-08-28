using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankDTO;

namespace Presentation.Views
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createAccountButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordTextBox.Text == "" || nameCreateTextBox.Text == "")
            {
                usernameExistLabel.Text = "3 boxes... You couldn't just fill them in?";
            }
            else
            { 
                var createdCustomer = new BankDTO.CustomerDTO();

                createdCustomer = createCustomer();

                if (Domain.Manager.IsExisting(createdCustomer) == false)
                {
                    Domain.Manager.SaveCustomer(createdCustomer);

                    Session["username"] = createdCustomer.Username;
                    Session["password"] = createdCustomer.Password;
                    Response.Redirect("http://localhost:53854/Views/Home.aspx");
                }
                else
                {
                    usernameExistLabel.Text = "Username already exits";
                }
            }
        }

        private CustomerDTO createCustomer()
        {
            var newCustomerDTO = new CustomerDTO();
            newCustomerDTO.CustomerId = Guid.NewGuid();
            newCustomerDTO.Name = nameCreateTextBox.Text.Trim();
            newCustomerDTO.Username = usernameTextBox.Text;
            newCustomerDTO.Password = passwordTextBox.Text;

            return newCustomerDTO;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankDTO;

namespace Presentation.Views
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = (string)(Session["username"]);
                string password = (string)(Session["password"]);

                var userInfo = new CustomerDTO() { Username = username, Password = password };
                List<AccountDTO> userAccounts = new List<AccountDTO>();

                userInfo = Domain.Manager.LoadUser(userInfo);
                userAccounts = Domain.Manager.LoadAccounts(userInfo);

                SetUpPage(userInfo, userAccounts);
                DefaultSetUp();
            }          
        }

        private void SetUpPage(CustomerDTO userInfo, List<AccountDTO> accounts)
        {
            usernameLabel.Text = userInfo.Name;

            idLabel.Text = userInfo.CustomerId.ToString();
            nameLabel.Text = userInfo.Username;

            RefreshGridView(accounts);          
        }

        internal void DefaultSetUp()
        {            
            editAccountTextBox.Visible = false;
            createAccountButton.Visible = false;
            deleteAccountButton.Visible = false;
            editAccountButton.Visible = true;
            cancelButton.Visible = false;
            withdrawlButton.Visible = false;
            depositeButton.Visible = false;
        }

        public void FetchAccounts()
        {
            string username = (string)(Session["username"]);
            string password = (string)(Session["password"]);

            var userInfo = new CustomerDTO() { Username = username, Password = password };
            List<AccountDTO> userAccounts = new List<AccountDTO>();

            userInfo = Domain.Manager.LoadUser(userInfo);
            userAccounts = Domain.Manager.LoadAccounts(userInfo);

            DefaultSetUp();
            RefreshGridView(userAccounts);
        }

        public void RefreshGridView(List<AccountDTO> accounts)
        {
            userAccountGridView.DataSource = accounts;
            userAccountGridView.DataBind();
        }

        protected void editAccountButton_Click(object sender, EventArgs e)
        {
            accountUpdateLabel.Text = "";
            editAccountButton.Visible = false;
            createAccountButton.Visible = true;//
            deleteAccountButton.Visible = true;//
            cancelButton.Visible = true; //
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            FetchAccounts();
        }

        protected void editAccountGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DefaultSetUp();
            accountUpdateLabel.Text = "";
            editAccountButton.Visible = false;//
            cancelButton.Visible = true; //
            withdrawlButton.Visible = true;
            depositeButton.Visible = true;

            /*Get the Row*/
            int index = Convert.ToInt32(e.CommandArgument); //Determine which row was selected
            GridViewRow rowAccount = userAccountGridView.Rows[index];  //select row value
            var accountValue = rowAccount.Cells[2];    //select column value in selected row
            string accountId = accountValue.Text.ToString();    //use the accountId to do stuff with

            /*Display Only the selected Row*/
            var accountSelected = new List<AccountDTO>();
            accountSelected = Domain.Manager.SelectedAccount(accountId);

            RefreshGridView(accountSelected);

            //should have been using viewstate this entire time
            Session["SelectedAccount"] = accountSelected;
        }

        protected void createAccountButton_Click(object sender, EventArgs e)
        {
            deleteAccountButton.Visible = false;
            editAccountTextBox.Visible = true;

            if (editAccountTextBox.Text.Length == 0) editAccountTextBox.Text = "Please Select a Starting Amount";
            else
            {
                string amount = editAccountTextBox.Text.Trim();
                decimal amount_Int;
                
                if (!decimal.TryParse(amount, out amount_Int)) editAccountTextBox.Text = "Please enter a Valid Amount";
                amount_Int = decimal.Round(amount_Int, 2, MidpointRounding.AwayFromZero);

                if (amount_Int >= 0M)
                {
                    Guid CustomerId = Guid.Parse(idLabel.Text);
                    Domain.Manager.CreateAccount(CustomerId, amount_Int);
                    FetchAccounts();
                    editAccountTextBox.Text = "";
                    accountUpdateLabel.Text = "New Account Has been added";
                }
            }
            
        }

        protected void deleteAccountButton_Click(object sender, EventArgs e)
        {
            createAccountButton.Visible = false;
            editAccountTextBox.Visible = true;

            if (editAccountTextBox.Text == "") editAccountTextBox.Text = "Select the Saving Id you wish to delete";
            else
            {
                int index;
                if (!int.TryParse(editAccountTextBox.Text.Trim(), out index))
                {
                    editAccountTextBox.Text = "Enter a valid Integer";
                }
                else
                {
                    bool existingValue = Domain.Manager.CountRows(index, idLabel.Text);
                    if (existingValue == true)
                    {
                        FetchAccounts();
                        editAccountTextBox.Text = "";
                        accountUpdateLabel.Text = "Account Deleted";
                    }
                    else
                    {
                        editAccountTextBox.Text = "Pick an Existing Value";
                    }
                }
            }
             
            

        }

        protected void withdrawlButton_Click(object sender, EventArgs e)
        {
            depositeButton.Visible = false;
            editAccountTextBox.Visible = true;

            if (editAccountTextBox.Text == "") editAccountTextBox.Text = "Enter Withdrawl Amount";

            var AccountsSelected = (List<AccountDTO>)Session["SelectedAccount"];
            var selectedAccount = Domain.Manager.ConvertAccount(AccountsSelected);

            decimal withdrawl;
            if (!decimal.TryParse(editAccountTextBox.Text.Trim(), out withdrawl) || decimal.Parse(editAccountTextBox.Text.Trim()) < 0)
            {
                editAccountTextBox.Text = "Enter a valid amount";
            }
            else
            {
                if (withdrawl <= selectedAccount.Amount)
                {
                    withdrawl = -1 * (decimal.Round(withdrawl, 2, MidpointRounding.AwayFromZero));
                                        
                    selectedAccount = (Domain.Manager.ManageFunds(withdrawl, selectedAccount));
                    Domain.Manager.StoreChange(selectedAccount);

                    FetchAccounts();
                    editAccountTextBox.Text = "";
                    accountUpdateLabel.Text = "Withdrawl Successful";
                }
                else
                {
                    editAccountTextBox.Text = "Withdrawl Cannot Exceed Amount";
                }
            }
        }

        protected void depositeButton_Click(object sender, EventArgs e)
        {
            withdrawlButton.Visible = false;
            editAccountTextBox.Visible = true;

            if (editAccountTextBox.Text == "") editAccountTextBox.Text = "Enter deposite Amount";

            var AccountsSelected = (List<AccountDTO>)Session["SelectedAccount"];
            var selectedAccount = Domain.Manager.ConvertAccount(AccountsSelected);

            decimal deposite;
            if (!decimal.TryParse(editAccountTextBox.Text.Trim(), out deposite) || decimal.Parse(editAccountTextBox.Text.Trim()) < 0)
            {
                editAccountTextBox.Text = "Enter a valid amount";
            }
            else
            {
                deposite = decimal.Round(deposite, 2, MidpointRounding.AwayFromZero);
                selectedAccount.Amount += deposite;

                Domain.Manager.StoreChange(selectedAccount);

                FetchAccounts();
                editAccountTextBox.Text = "";
                accountUpdateLabel.Text = "deposite Successful";
            }
        }
    }
}
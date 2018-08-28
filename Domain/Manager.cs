using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDTO;

namespace Domain
{
    public class Manager
    {
        public static bool IsExisting(BankDTO.CustomerDTO createdCustomer)
        {
            return Persistence.SaveDb.IsExisting(createdCustomer);

        }

        public static void SaveCustomer(BankDTO.CustomerDTO createdCustomer)
        {
            Persistence.SaveDb.SaveCustomer(createdCustomer);
        }

        public static bool ValidateLogin(CustomerDTO userLogin)
        {
            return Persistence.ValidateDb.ValidateLogin(userLogin);
        }

        public static CustomerDTO LoadUser(CustomerDTO userLogin)
        {
            return Persistence.ValidateDb.LoadUserInfo(userLogin);
        }

        public static List<AccountDTO> LoadAccounts(CustomerDTO userInfo)
        {
            return Persistence.ValidateDb.LoadUserAccounts(userInfo);
        }

        public static List<AccountDTO> SelectedAccount(string accountValue)
        {
            return Persistence.EditDb.SelectedAccount(accountValue);
        }

        public static void CreateAccount(Guid customerId, decimal amount)
        {
            Persistence.EditDb.CreateAccount(customerId, amount);
        }

        public static bool CountRows(int index, string customerId)
        {
            Guid customer_Id = Guid.Parse(customerId);

            bool rowExist = Persistence.EditDb.DoesRowExist(customer_Id, index);

            if (rowExist == true)
            {
                Persistence.EditDb.DeleteRow(index, customer_Id);
                return true;
            }
            else return false;
        }

        public static AccountDTO ConvertAccount(List<AccountDTO> selectedAccount)
        {
            var convertedAccount = new AccountDTO();
            convertedAccount = selectedAccount.FirstOrDefault();

            return convertedAccount;
        }

        public static AccountDTO ManageFunds(decimal amountChange, AccountDTO selectedAccount)
        {
            selectedAccount.Amount += amountChange;
            
            return selectedAccount;
        }

        public static void StoreChange(AccountDTO selectedAccount)
        {
            Persistence.EditDb.StoreChange(selectedAccount);
        }
    }
}

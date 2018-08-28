using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDTO;

namespace Persistence
{
    public class SaveDb
    {
        public static bool IsExisting(CustomerDTO createdCustomer)
        {
            var db = new dbBankEntities();
            foreach (var customer in db.tbCustomers)
            {
                if (customer.Username == createdCustomer.Username)
                {
                    return true;
                }
            }
            return false;
        }

        public static void SaveCustomer(BankDTO.CustomerDTO createdCustomer)
        {
            var db = new dbBankEntities();

            var newCustomer = setCustomerInfo(createdCustomer);
                
            db.tbCustomers.Add(newCustomer);
            db.SaveChanges();
        }

        private static tbCustomer setCustomerInfo(CustomerDTO createdCustomer)
        {
            var newCustomer = new tbCustomer();
            newCustomer.CustomerId = createdCustomer.CustomerId;
            newCustomer.Name = createdCustomer.Name;
            newCustomer.Username = createdCustomer.Username;
            newCustomer.Password = createdCustomer.Password;
            return newCustomer;
        }

    }

    public class ValidateDb
    {
        public static CustomerDTO LoadUserInfo(CustomerDTO userLogin)
        {        
            //my first query!!!!!! find user with matching username and password
            var queryDoesUserExist = QueryFindUser(userLogin);

            //does it exist?
            if (queryDoesUserExist != null)
            {
                var currentUser = new CustomerDTO
                {
                    CustomerId = queryDoesUserExist.CustomerId,
                    Name = queryDoesUserExist.Name,
                    Username = queryDoesUserExist.Username,
                    Password = queryDoesUserExist.Password
                };

                return currentUser;
            }
            return null;
        }

        public static bool ValidateLogin(CustomerDTO userLogin)
        {
            //my first query!!!!!! find user with matching username and password
            var queryDoesUserExist = QueryFindUser(userLogin);

            return (null != queryDoesUserExist);         
        }

        public static tbCustomer QueryFindUser(CustomerDTO userLogin)
        {
            using (var db = new dbBankEntities())
            {
                return (from cust in db.tbCustomers
                       where cust.Username == userLogin.Username
                      && cust.Password == userLogin.Password
                       select cust).SingleOrDefault();
            }

        }



        public static List<AccountDTO> LoadUserAccounts(CustomerDTO userInfo)
        {
            List<AccountDTO> userAccounts = QueryFindAccount(userInfo);

            return userAccounts;
        }

        private static List<AccountDTO> QueryFindAccount(CustomerDTO userInfo)
        {
            var db = new dbBankEntities();

            var account = from saving in db.tbSavings
                            where saving.Customer_Id == userInfo.CustomerId
                            select saving;

            return ConvertQueryToList(account);
        }

        public static List<AccountDTO> ConvertQueryToList(IQueryable<tbSaving> account)
        {
            
            List<AccountDTO> accountsDTO = new List<AccountDTO>();

            foreach (var saving in account)
            {
                var accountDTO = new AccountDTO();

                accountDTO.Customer_Id = saving.Customer_Id;
                accountDTO.SavingId = saving.SavingId;
                accountDTO.RoutingNumber = saving.RoutingNumber;
                accountDTO.AccountNumber = saving.AccountNumber;
                accountDTO.Amount = saving.Amount;
                saving.SavingId = saving.SavingId;

                accountsDTO.Add(accountDTO);
            }

            return accountsDTO;
        }
    }

    public class EditDb
    { 
        public static List<AccountDTO> SelectedAccount(string accountValue)
        {
            var db = new dbBankEntities();
            int accountId = int.Parse(accountValue);

            IQueryable<tbSaving> selectedAccounts = from accounts in db.tbSavings
                                                   where accounts.SavingId == accountId
                                                   select accounts;

            var selectedAccount = ValidateDb.ConvertQueryToList(selectedAccounts);

            return selectedAccount;

        }

        public static void CreateAccount(Guid customerId, decimal amount)
        {
            var db = new dbBankEntities();

            Random random = new Random();
            var newAccount = new tbSaving();

            newAccount.Customer_Id = customerId;
            newAccount.RoutingNumber = CreateNewRoutingNumber(random, db);
            newAccount.AccountNumber = CreateNewAccountNumber(random);
            newAccount.Amount = amount;

            db.tbSavings.Add(newAccount);
            db.SaveChanges();
        }

        private static string CreateNewAccountNumber(Random random)
        {           
            return random.Next(0, 99999).ToString();
        }

        private static string CreateNewRoutingNumber(Random random, dbBankEntities db)
        {

            string newRoutingNumber;

            IQueryable<tbSaving> queryRoutingMatches = null;

            do
            {
                newRoutingNumber = RandomGenerator(random);
                queryRoutingMatches = FindRoutingMatches(db, newRoutingNumber);
            } while (CheckingRouting(queryRoutingMatches) == false);

            return newRoutingNumber;
        }

        private static bool CheckingRouting(IQueryable queryRoutingMatches)
        {
            foreach (var account in queryRoutingMatches)
            {
                return false;
            }
            return true;
        }

        private static IQueryable<tbSaving> FindRoutingMatches(dbBankEntities db, string newRoutingNumber)
        {
            return from accounts in db.tbSavings
                where accounts.RoutingNumber == newRoutingNumber
                select accounts;
        }

        private static string RandomGenerator(Random random)
        {
            
            int maxRoutingNumber = 999999999;
            return random.Next(0, maxRoutingNumber).ToString();

            
        }



        public static bool DoesRowExist(Guid customerId, int index)
        {
            IQueryable<tbSaving> userAccounts = QueryUserAccount(customerId, index);

            if (userAccounts.Count() == 1) return true;
            else return false;
        }

        public static IQueryable<tbSaving> QueryUserAccount(Guid customerId, int index)
        {
            var db = new dbBankEntities();

            return  from users in db.tbSavings
                    where users.Customer_Id == customerId && users.SavingId == index
                    select users;
        }

        public static void DeleteRow(int index, Guid customerId)
        {
            var db = new dbBankEntities();

            var deleteSelectedRow = (from account in db.tbSavings
                                           where account.Customer_Id == customerId && account.SavingId == index
                                           select account).FirstOrDefault();

            db.tbSavings.Remove(deleteSelectedRow);
            db.SaveChanges();        
        }



        public static void StoreChange(AccountDTO _changedAccount)
        {
            var db = new dbBankEntities();
            
            var queryDbAmount = (from account in db.tbSavings
                                 where account.SavingId == _changedAccount.SavingId
                                 select account).FirstOrDefault();

            queryDbAmount.Amount = _changedAccount.Amount;

            /*var changedAccount = new tbSaving();
            changedAccount.AccountNumber = _changedAccount.AccountNumber;
            changedAccount.Amount = _changedAccount.Amount;
            changedAccount.Customer_Id = _changedAccount.Customer_Id;
            changedAccount.RoutingNumber = _changedAccount.RoutingNumber;
            changedAccount.SavingId = _changedAccount.SavingId;

            db.tbSavings.Remove(queryDbAmount);
            db.tbSavings.Add(changedAccount);*/
            db.SaveChanges();

        }
    }

}

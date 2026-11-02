using System;
using System.Collections.Generic;
using bank_project.customer;
using bank_project.transaction;

namespace bank_project.account
{
    internal class accountservice
    {
        private accountrepo _repo;
        public accountservice()
        {
            _repo = new accountrepo();
        }

        public List<accountmodel> getalldata()
        {
            return _repo.GetAccountfromfile();
        }

        // quiz data
       
        public void saveaccount(accountmodel account)
        {
            _repo.saveinfile(account);
        }
        public void saveallaccounts(List<accountmodel> accounts)
        {
            _repo.savealldataintofile(accounts);
        }

        public accountmodel searchbyaccountnumber(string accountNumber)
        {
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                if (account.getaccountnumber() == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
        public accountmodel searchbycustomerid(int customerId)
        {
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                if (account.getcustomerid() == customerId)
                {
                    return account;
                }
            }
            return null;
        }
        public List<accountmodel> searchallbycustomerid(int customerId)
        {
            List<accountmodel> myaccounts = new List<accountmodel>();
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                if (account.getcustomerid() == customerId)
                {
                    myaccounts.Add(account);
                }
            }
            return myaccounts;
        }

        public string deposit(accountmodel account, double amount, List<transactionmodel> transactions, ref int nextTransactionId)
        {
            if (account.getstatus() != "Active")
            {
                return "Account is not Active.";
            }
            if (amount <= 0)
            {
                return "Amount must be greater than zero.";
            }
            account.setbalance(account.getbalance() + amount);
            transactions.Add(new transactionmodel(nextTransactionId, account.getaccountnumber(), "Deposit", amount, DateTime.Now.ToString("yyyy-MM-dd"), account.getbalance(), "Deposit"));
            nextTransactionId++;
            return "OK";
        }

        public string withdraw(accountmodel account, double amount, List<transactionmodel> transactions, ref int nextTransactionId)
        {
            if (account.getstatus() != "Active")
            {
                return "Account is not Active.";
            }
            if (amount <= 0)
            {
                return "Amount must be greater than zero.";
            }
            if (amount > account.getbalance())
            {
                return "Not enough balance.";
            }
            if (account.getaccounttype() == "Savings" && account.getbalance() - amount < 500)
            {
                return "Savings account must keep at least Rs. 500 balance.";
            }
            account.setbalance(account.getbalance() - amount);
            transactions.Add(new transactionmodel(nextTransactionId, account.getaccountnumber(), "Withdraw", amount, DateTime.Now.ToString("yyyy-MM-dd"), account.getbalance(), "Withdraw"));
            nextTransactionId++;
            return "OK";
        }

        public string transfer(accountmodel from, accountmodel to, double amount, List<transactionmodel> transactions, ref int nextTransactionId)
        {
            if (from.getaccountnumber() == to.getaccountnumber())
            {
                return "Cannot transfer to the same account.";
            }
            if (from.getstatus() != "Active" || to.getstatus() != "Active")
            {
                return "Both accounts must be Active.";
            }
            if (amount <= 0)
            {
                return "Amount must be greater than zero.";
            }
            if (amount > from.getbalance())
            {
                return "Not enough balance.";
            }
            if (from.getaccounttype() == "Savings" && from.getbalance() - amount < 500)
            {
                return "Savings account must keep at least Rs. 500 balance.";
            }
            from.setbalance(from.getbalance() - amount);
            transactions.Add(new transactionmodel(nextTransactionId, from.getaccountnumber(), "TransferOut", amount, DateTime.Now.ToString("yyyy-MM-dd"), from.getbalance(), "Transfer to " + to.getaccountnumber()));
            nextTransactionId++;
            to.setbalance(to.getbalance() + amount);
            transactions.Add(new transactionmodel(nextTransactionId, to.getaccountnumber(), "TransferIn", amount, DateTime.Now.ToString("yyyy-MM-dd"), to.getbalance(), "Transfer from " + from.getaccountnumber()));
            nextTransactionId++;
            return "OK";
        }

        public bool closeaccount(string accountNumber, string status)
        {
            List<accountmodel> accounts = _repo.GetAccountfromfile();
            foreach (accountmodel account in accounts)
            {
                if (account.getaccountnumber() == accountNumber)
                {
                    account.setstatus(status);
                    _repo.savealldataintofile(accounts);
                    return true;
                }
            }
            return false;
        }

        public double gettotalbalance()
        {
            double total = 0;
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                total = total + account.getbalance();
            }
            return total;
        }
        public int countactive()
        {
            int count = 0;
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                if (account.getstatus() == "Active")
                {
                    count++;
                }
            }
            return count;
        }





        public void applyinterest(double ratePercent, List<transactionmodel> transactions, ref int nextTransactionId)
        {
            List<accountmodel> accounts = _repo.GetAccountfromfile();
            foreach (accountmodel account in accounts)
            {
                if ((account.getaccounttype() == "Savings" || account.getaccountnumber() == "savings" ) && account.getstatus() == "Active")
                {
                    double interest = account.getbalance() * ratePercent / 100;
                    account.setbalance(account.getbalance() + interest);
                    transactions.Add(new transactionmodel(nextTransactionId, account.getaccountnumber(), "Interest", interest, DateTime.Now.ToString("yyyy-MM-dd"), account.getbalance(), "Monthly interest"));
                    nextTransactionId++;
                }
            }
            _repo.savealldataintofile(accounts);
        }

        // Sort by acc balance
        public List<accountmodel> filterbybalance()
        {
            List<accountmodel> accounts = _repo.GetAccountfromfile();
            for (int i = 0; i < accounts.Count - 1; i++)
            {
                for (int j = 0; j < accounts.Count - i - 1; j++)
                {
                    if (accounts[j].getbalance() < accounts[j + 1].getbalance())
                    {
                        accountmodel temp = accounts[j];
                        accounts[j] = accounts[j + 1];
                        accounts[j + 1] = temp;
                    }
                }
            }
            return accounts;
        }

        public List<accountmodel> filteraccounts(List<accountmodel> accounts, List<customermodel> customers, string namePart, double minBalance, double maxBalance, string category)
        {
            List<accountmodel> result = new List<accountmodel>();
            foreach (accountmodel account in accounts)
            {
                string name = getcustomername(customers, account.getcustomerid());

                bool nameMatch = false;
                if (namePart == "")
                {
                    nameMatch = true;
                }
                else if (name.ToLower().Contains(namePart.ToLower()))
                {
                    nameMatch = true;
                }

                bool balanceMatch = false;
                if (account.getbalance() >= minBalance && account.getbalance() <= maxBalance)
                {
                    balanceMatch = true;
                }

                bool categoryMatch = false;
                if (category == "")
                {
                    categoryMatch = true;
                }
                else if (account.getaccounttype().ToLower() == category.ToLower())
                {
                    categoryMatch = true;
                }

                if (nameMatch && balanceMatch && categoryMatch)
                {
                    result.Add(account);
                }
            }
            return result;
        }

        private string getcustomername(List<customermodel> customers, int customerId)
        {
            foreach (customermodel customer in customers)
            {
                if (customer.getcustomerid() == customerId)
                {
                    return customer.getname();
                }
            }
            return "";
        }

        // min and max 
        public List<accountmodel> filterbalance(double min , double max)
        {
            List<accountmodel> accounts = _repo.GetAccountfromfile();
           foreach( accountmodel balance in accounts)
            {
                if( balance.getbalance() >= min && balance.getbalance() <= max)
                {
                    return accounts;

                }
            }
            return accounts;
        }

        // filter by account type 

        public List<accountmodel> filterbytype(string accountType)
        {
            List<accountmodel> result = new List<accountmodel>();
            foreach (accountmodel account in _repo.GetAccountfromfile())
            {
                if (account.getaccounttype() == accountType)
                {
                    result.Add(account);
                }
            }
            return result;
        }
        public static List<accountmodel> filterbytype(List<accountmodel> accounts, string accountType)
        {
            List<accountmodel> result = new List<accountmodel>();
            foreach (accountmodel account in accounts)
            {
                if (account.getaccounttype() == accountType) { result.Add(account); }
            }
            return result;
        }


        public static List<accountmodel> filterbybalancerange(List<accountmodel> accounts, double min, double max)
        {
            List<accountmodel> result = new List<accountmodel>();
            foreach (accountmodel account in accounts)
            {
                if (account.getbalance() >= min && account.getbalance() <= max) { result.Add(account); }
            }
            return result;
        }
        public List<accountmodel> filterbyshortname(List<accountmodel> accounts, List<customermodel> customers, string shortName)
        {
            List<accountmodel> result = new List<accountmodel>();
            string shortNameLower = shortName.Trim().ToLower();
            foreach (accountmodel account in accounts)
            {
                string name = getcustomername(customers, account.getcustomerid());
                if (shortNameLower == "" || name.ToLower().StartsWith(shortNameLower))
                {
                    result.Add(account);
                }
            }
            return result;
        }

    }
}

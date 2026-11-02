using System;
using System.Collections.Generic;
using bank_project.user;
using bank_project.customer;
using bank_project.transaction;

namespace bank_project.account
{
    internal class accountui
    {
        private accountservice _accountservice = new accountservice();
        private customerservice _customerservice = new customerservice();
        private userservice _userservice = new userservice();
        private transactionservice _transactionservice = new transactionservice();

        public void accountdriver()
        {
            while (true)
            {
                string option = accountmenu();
                if (option == "1")
                {
                    createcustomerandaccount();
                }
                else if (option == "2")
                {
                    viewallaccounts();
                }
                else if (option == "3")
                {
                    searchaccount();
                }
                else if (option == "4")
                {
                    depositmoney();
                }
                else if (option == "5")
                {
                    withdrawmoney();
                }
                else if (option == "6")
                {
                    transfermoney();
                }
                else if (option == "7")
                {
                    viewtransactions();
                }
                else if (option == "8")
                {
                    printstatement();
                }
                else if (option == "9")
                {
                    closeorreactivate();
                }
                else if (option == "10")
                {
                    showreports();
                }
                else if (option == "11")
                {
                    managelogins();
                }
                else if (option == "12")
                {
                    savingsinterest();
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong option selected");
                }
            }
        }

        public string accountmenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("     💼 Accountant Menu    ");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1  . ➕ Create Customer and Account");
            Console.WriteLine("2  . 👀 View All Accounts");
            Console.WriteLine("3  . 🔍 Search Account");
            Console.WriteLine("4  . ➕ Deposit");
            Console.WriteLine("5  . ➖ Withdraw");
            Console.WriteLine("6  . 🔄 Transfer");
            Console.WriteLine("7  . 📄 View Transactions");
            Console.WriteLine("8  . 🖨️ Print Statement");
            Console.WriteLine("9  . 🔒 Close / Reactivate Account");
            Console.WriteLine("10 . 📊 Reports");
            Console.WriteLine("11 . 🔑 Manage Customer Logins (reset password)");
            Console.WriteLine("12 . 💹 Savings Interest");
            Console.WriteLine("0  . 🖐️ Logout");
            Console.Write("Choice: ");
            string option = Console.ReadLine();
            return option;
        }

        public customermodel takecustomerinput()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter CNIC: ");
            string cnic = Console.ReadLine();
           while(!(IsValidCnic(cnic)))
            {
                Console.Write("Enter Valid CNIC : ");
                cnic = Console.ReadLine();
            }
            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();
            while (!(IsValidphone(phone)))
            {
                Console.Write("Enter Valid Phone Number : ");
                phone = Console.ReadLine();
            }
            Console.Write("Enter address: ");
            string address = Console.ReadLine();

            customermodel customer = new customermodel(0, name, cnic, phone, address, 0);
            return customer;
        }

        public accountmodel takeaccountinput(customermodel customer, string accountNumber)
        {
            Console.Write("Account Type (Savings / Current): ");
            string accountType = Console.ReadLine();
            if (accountType != "Savings" && accountType != "Current")
            {
                accountType = "Savings";
            }


            accountmodel account = new accountmodel(accountNumber, customer.getcustomerid(), accountType, 0, "Active", "");
            return account;
        }

        public void createcustomerandaccount()
        {
            customermodel customer = takecustomerinput();
            if (_customerservice.searchbycnic(customer.getcnic()) != null)
            {
                Console.WriteLine("CNIC already exists.");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter username for customer: ");
            string username = Console.ReadLine();
            if (username == "admin")
            {
                Console.WriteLine("Username admin is reserved for the admin.");
                Console.ReadKey();
                return;
            }
            if (_userservice.searchbyusername(username) != null)
            {
                Console.WriteLine("Username already exists.");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter password for customer: ");
            string password = Console.ReadLine();

            int nextCustomerId = getnextcustomerid();
            int nextUserId = getnextuserid();
            string nextAccountNumber = getnextaccountnumber();

            usermodel user = new usermodel(nextUserId, username, password, nextCustomerId);
            _userservice.saveuser(user);

            customer.setcustomerid(nextCustomerId);
            customer.setuserid(nextUserId);
            _customerservice.savecustomer(customer);

            accountmodel account = takeaccountinput(customer, nextAccountNumber);
            _accountservice.saveaccount(account);

            Console.WriteLine("Created! CustomerId=" + nextCustomerId + ", AccountNumber=" + nextAccountNumber);
            Console.ReadKey();
        }

        public void viewallaccounts()
        {
            Console.Clear();
            List<accountmodel> accounts = _accountservice.getalldata();
            List<customermodel> customers = _customerservice.getalldata();
            foreach (accountmodel account in accounts)
            {
                showaccountline(account, customers);
            }
            Console.ReadKey();
        }

        public void searchaccount()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            accountmodel account = _accountservice.searchbyaccountnumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                List<customermodel> customers = _customerservice.getalldata();
                showaccountline(account, customers);
            }
            Console.ReadKey();
        }

        public void depositmoney()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel account = findaccountinlist(accounts, accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                Console.ReadKey();
                return;
            }
            double amount = readamount("Enter amount: ");
            List<transactionmodel> transactions = _transactionservice.getalldata();
            int nextTransactionId = getnexttransactionid(transactions);
            string result = _accountservice.deposit(account, amount, transactions, ref nextTransactionId);
            if (result == "OK")
            {
                _accountservice.saveallaccounts(accounts);
                _transactionservice.savealltransactions(transactions);
                Console.WriteLine("Done. New balance: Rs. " + account.getbalance());
            }
            else
            {
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }

        public void withdrawmoney()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel account = findaccountinlist(accounts, accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                Console.ReadKey();
                return;
            }
            double amount = readamount("Enter amount: ");
            List<transactionmodel> transactions = _transactionservice.getalldata();
            int nextTransactionId = getnexttransactionid(transactions);
            string result = _accountservice.withdraw(account, amount, transactions, ref nextTransactionId);
            if (result == "OK")
            {
                _accountservice.saveallaccounts(accounts);
                _transactionservice.savealltransactions(transactions);
                Console.WriteLine("Done. New balance: Rs. " + account.getbalance());
            }
            else
            {
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }

        public void transfermoney()
        {
            Console.Write("Enter from account number: ");
            string fromNumber = Console.ReadLine();
            Console.Write("Enter to account number: ");
            string toNumber = Console.ReadLine();
            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel from = findaccountinlist(accounts, fromNumber);
            accountmodel to = findaccountinlist(accounts, toNumber);
            if (from == null || to == null)
            {
                Console.WriteLine("Account not found.");
                Console.ReadKey();
                return;
            }
            double amount = readamount("Enter amount: ");
            List<transactionmodel> transactions = _transactionservice.getalldata();
            int nextTransactionId = getnexttransactionid(transactions);
            string result = _accountservice.transfer(from, to, amount, transactions, ref nextTransactionId);
            if (result == "OK")
            {
                _accountservice.saveallaccounts(accounts);
                _transactionservice.savealltransactions(transactions);
                Console.WriteLine("Done. New balance: Rs. " + from.getbalance());
            }
            else
            {
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }

        public void viewtransactions()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            List<transactionmodel> transactions = _transactionservice.getbyaccountnumber(accountNumber);
            showtransactionlist(transactions);
        }

        public void printstatement()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            accountmodel account = _accountservice.searchbyaccountnumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                Console.ReadKey();
                return;
            }
            List<customermodel> customers = _customerservice.getalldata();
            Console.Clear();
            Console.WriteLine("----- Statement -----");
            showaccountline(account, customers);
            List<transactionmodel> transactions = _transactionservice.getbyaccountnumber(accountNumber);
            foreach (transactionmodel transaction in transactions)
            {
                Console.WriteLine(transaction.getdate() + " | " + transaction.gettype() + " | Rs. " + transaction.getamount() + " | Balance after: Rs. " + transaction.getbalanceafter() + " | " + transaction.getdescription());
            }
            Console.ReadKey();
        }

        public void closeorreactivate()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Console.WriteLine("1 . Closed");
            Console.WriteLine("2 . Inactive");
            Console.WriteLine("3 . Active");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();
            string status = "";
            if (choice == "1")
            {
                status = "Closed";
            }
            else if (choice == "2")
            {
                status = "Inactive";
            }
            else if (choice == "3")
            {
                status = "Active";
            }
            else
            {
                Console.WriteLine("Wrong option selected");
                Console.ReadKey();
                return;
            }
            if (_accountservice.closeaccount(accountNumber, status))
            {
                Console.WriteLine("Status updated to " + status);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
            Console.ReadKey();
        }

        public void showreports()
        {
            Console.Clear();
            Console.WriteLine("----- Bank Reports -----");
            Console.WriteLine("Total bank balance: Rs. " + _accountservice.gettotalbalance());
            Console.WriteLine("Active accounts: " + _accountservice.countactive());
            List<accountmodel> accounts = _accountservice.getalldata();
            Console.WriteLine("Total accounts: " + accounts.Count);
            Console.WriteLine("Low-balance accounts (under Rs. 1000):");
            foreach (accountmodel account in accounts)
            {
                if (account.getbalance() < 1000)
                {
                    Console.WriteLine(account.getaccountnumber() + " | Rs. " + account.getbalance() + " | " + account.getstatus());
                }
            }
            Console.ReadKey();
        }

        public void managelogins()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            if (_userservice.updatepassword(username, newPassword))
            {
                Console.WriteLine("Password reset.");
            }
            else
            {
                Console.WriteLine("Customer login not found.");
            }
            Console.ReadKey();
        }

        public void savingsinterest()
        {
            double rate = readamount("Enter interest rate percent: ");
            List<transactionmodel> transactions = _transactionservice.getalldata();
            int nextTransactionId = getnexttransactionid(transactions);
            _accountservice.applyinterest(rate, transactions, ref nextTransactionId);
            _transactionservice.savealltransactions(transactions);
            Console.WriteLine("Interest applied to Savings accounts.");
            Console.ReadKey();
        }

        private int getnextcustomerid()
        {
            int max = 100;
            foreach (customermodel customer in _customerservice.getalldata())
            {
                if (customer.getcustomerid() > max)
                {
                    max = customer.getcustomerid();
                }
            }
            return max + 1;
        }
        private int getnextuserid()
        {
            int max = 0;
            foreach (usermodel user in _userservice.getalldata())
            {
                if (user.getuserid() > max)
                {
                    max = user.getuserid();
                }
            }
            return max + 1;
        }
        private string getnextaccountnumber()
        {
            int max = 1000;
            foreach (accountmodel account in _accountservice.getalldata())
            {
                int number = int.Parse(account.getaccountnumber());
                if (number > max)
                {
                    max = number;
                }
            }
            return (max + 1).ToString();
        }
        private int getnexttransactionid(List<transactionmodel> transactions)
        {
            int max = 0;
            foreach (transactionmodel transaction in transactions)
            {
                if (transaction.gettransactionid() > max)
                {
                    max = transaction.gettransactionid();
                }
            }
            return max + 1;
        }
        private accountmodel findaccountinlist(List<accountmodel> accounts, string accountNumber)
        {
            foreach (accountmodel account in accounts)
            {
                if (account.getaccountnumber() == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
        private string getholdername(List<customermodel> customers, int customerId)
        {
            foreach (customermodel customer in customers)
            {
                if (customer.getcustomerid() == customerId)
                {
                    return customer.getname();
                }
            }
            return "Unknown";
        }
        private void showaccountline(accountmodel account, List<customermodel> customers)
        {
            string name = getholdername(customers, account.getcustomerid());
            Console.WriteLine(account.getaccountnumber() + " | " + name + " | " + account.getaccounttype() + " | Rs. " + account.getbalance() + " | " + account.getstatus());
        }
        private void showtransactionlist(List<transactionmodel> transactions)
        {
            Console.Clear();
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            foreach (transactionmodel transaction in transactions)
            {
                Console.WriteLine(transaction.getdate() + " | " + transaction.gettype() + " | Rs. " + transaction.getamount() + " | Balance after: Rs. " + transaction.getbalanceafter() + " | " + transaction.getdescription());
            }
            Console.ReadKey();
        }
        private double readamount(string prompt)
        {
            double amount = 0;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Enter a valid number: ");
            }
            return amount;
        }
        private bool IsValidCnic(String Cnic)
        {
            if(Cnic.Length != 13)
            {
                return false;
            }
            for(int i = 0; i< Cnic.Length;i++)
            {
                if (Cnic[i] < 48 && Cnic[i] > 57)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsValidphone(String phone)
        {
            if (phone.Length != 11)
            {
                return false;
            }
            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] < 48 || phone[i] > 57)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

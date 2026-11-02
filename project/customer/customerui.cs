using System;
using System.Collections.Generic;
using bank_project.user;
using bank_project.account;
using bank_project.transaction;

namespace bank_project.customer
{
    internal class customerui
    {
        private usermodel _user;
        private customermodel _customer;
        private string _ownaccountnumber;
        private customerservice _customerservice = new customerservice();
        private accountservice _accountservice = new accountservice();
        private transactionservice _transactionservice = new transactionservice();
        private userservice _userservice = new userservice();

        public customerui(usermodel user)
        {
            _user = user;
        }

        public void customerdriver()
        {
            _customer = _customerservice.searchbyid(_user.getcustomerid());
            if (_customer == null)
            {
                Console.WriteLine("Customer profile not found for this login.");
                Console.ReadKey();
                return;
            }
            accountmodel myaccount = _accountservice.searchbycustomerid(_customer.getcustomerid());
            if (myaccount == null)
            {
                Console.WriteLine("No account found for you.");
                Console.ReadKey();
                return;
            }
            _ownaccountnumber = myaccount.getaccountnumber();


            while (true)
            {
                string option = customermenu();
                if (option == "1")
                {
                    viewmyaccount();
                }
                else if (option == "2")
                {
                    checkbalance();
                }
                else if (option == "3")
                {
                    depositmoney();
                }
                else if (option == "4")
                {
                    withdrawmoney();
                }
                else if (option == "5")
                {
                    transfermoney();
                }
                else if (option == "6")
                {
                    ministatement();
                }
                else if (option == "7")
                {
                    fullhistory();
                }
                else if (option == "8")
                {
                    changepassword();
                }
                else if (option == "9")
                {
                    updatemycontact();
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

        public string customermenu()
        {
            Console.Clear();
            Console.WriteLine("-----------------------");
            Console.WriteLine("     👤 Customer Menu     ");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1  . 👀 View My Account");
            Console.WriteLine("2  . 💰 Check Balance");
            Console.WriteLine("3  . ➕ Deposit");
            Console.WriteLine("4  . ➖ Withdraw");
            Console.WriteLine("5  . 🔄 Transfer");
            Console.WriteLine("6  . 📄 Mini Statement (last 5)");
            Console.WriteLine("7  . 📜 Full History");
            Console.WriteLine("8  . 🔑 Change Password");
            Console.WriteLine("9  . ✏️ Update My Contact Details");
            Console.WriteLine("0  . 🖐️ Logout");
            Console.Write("Choice: ");
            string option = Console.ReadLine();
            return option;
        }

        public void viewmyaccount()
        {
            Console.Clear();
            accountmodel account = _accountservice.searchbyaccountnumber(_ownaccountnumber);
            Console.WriteLine("Name: " + _customer.getname());
            Console.WriteLine("CNIC: " + _customer.getcnic());
            Console.WriteLine("Phone: " + _customer.getphone());
            Console.WriteLine("Address: " + _customer.getaddress());
            if (account != null)
            {
                Console.WriteLine(account.getaccountnumber() + " | " + account.getaccounttype() + " | Rs. " + account.getbalance() + " | " + account.getstatus());
            }
            Console.ReadKey();
        }

        public void checkbalance()
        {
            accountmodel account = _accountservice.searchbyaccountnumber(_ownaccountnumber);
            if (account == null)
            {
                Console.WriteLine("No account found for you.");
            }
            else
            {
                Console.WriteLine("Balance: Rs. " + account.getbalance());
            }
            Console.ReadKey();
        }

        public void depositmoney()
        {



            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel account = findaccountinlist(accounts, _ownaccountnumber);
            if (account == null)
            {
                Console.WriteLine("No account found for you.");
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
            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel account = findaccountinlist(accounts, _ownaccountnumber);
            if (account == null)
            {
                Console.WriteLine("No account found for you.");
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
            Console.Write("Enter destination account number: ");
            string toNumber = Console.ReadLine();
            List<accountmodel> accounts = _accountservice.getalldata();
            accountmodel from = findaccountinlist(accounts, _ownaccountnumber);
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

        public void ministatement()
        {
            List<transactionmodel> transactions = _transactionservice.getlastn(_ownaccountnumber, 5);
            showtransactionlist(transactions);
        }

        public void fullhistory()
        {
            List<transactionmodel> transactions = _transactionservice.getbyaccountnumber(_ownaccountnumber);
            showtransactionlist(transactions);
        }

    

        public void changepassword()
        {
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            if (_userservice.updatepassword(_user.getusername(), newPassword))
            {
                _user.setpassword(newPassword);
                Console.WriteLine("Password changed.");
            }
            else
            {
                Console.WriteLine("Password could not be changed.");
            }
            Console.ReadKey();
        }

        public void updatemycontact()
        {
            Console.Write("Enter new phone: ");
            string phone = Console.ReadLine();
            while (!(IsValidphone(phone)))
            {
                Console.Write("Enter Valid Phone Number : ");
                phone = Console.ReadLine();
            }
            Console.Write("Enter new address: ");
            string address = Console.ReadLine();
            if (_customerservice.updatecontact(_customer.getcustomerid(), phone, address))
            {
                Console.WriteLine("Contact details updated.");
            }
            else
            {
                Console.WriteLine("Contact details could not be updated.");
            }
            Console.ReadKey();
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

    using System;
using System.Collections.Generic;
using bank_project.user;
using bank_project.account;
using bank_project.customer;
using bank_project.transaction;

namespace bank_project
{
    internal class bank
    {
        private const string adminUsername = "admin";
        private const string adminPassword = "1234";
        private userservice _userservice = new userservice();
        private customerservice _customerservice = new customerservice();
        private accountservice _accountservice = new accountservice();
        private transactionservice _transactionservice = new transactionservice();

        public void start()
        {
            _userservice.getalldata();
            _customerservice.getalldata();
            _accountservice.getalldata();
            _transactionservice.getalldata();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("======================================================");
                Console.WriteLine("       MW  🏦  BANK ACCOUNT MANAGEMENT SYSTEM   🏦  ");
                Console.WriteLine("======================================================");
                Console.WriteLine();
                Console.WriteLine("[Admin login] username : admin | pass : 1234");
                Console.Write("👤 Username (or type exit): ");
                string username = Console.ReadLine();
                if (username == "exit" || username == "EXIT"  || username == "Exit")
                {
                    break;
                }
                Console.Write("🔑 Password: ");
                string password = Console.ReadLine();

                if (username == adminUsername && password == adminPassword)
                {
                    accountui accountui = new accountui();
                    accountui.accountdriver();
                    continue;
                }

                usermodel user = _userservice.login(username, password);
                if (user == null)
                {
                    Console.WriteLine("Invalid login.");
                    Console.ReadKey();
                    continue;
                }
                customerui customerui = new customerui(user);
                customerui.customerdriver();
            }
            Console.WriteLine("Data saved. Goodbye! 👋");
        }
    }
}

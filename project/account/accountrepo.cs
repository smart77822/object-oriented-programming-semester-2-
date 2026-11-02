using System;
using System.Collections.Generic;
using System.IO;
using bank_project.common;

namespace bank_project.account
{
    internal class accountrepo
    {
        private string getfilepath()
        {
            return datafilehelper.getresolvedfilepath("accounts.txt");
        }
        public accountrepo() { }
        public void saveinfile(accountmodel accountmodel)
        {
            if (!File.Exists(getfilepath()))
            {
                File.Create(getfilepath()).Close();
            }
            using (StreamWriter writer = new StreamWriter(getfilepath(), true))
            {
                writer.WriteLine(accountmodel.ToString());
            }
        }
        public void savealldataintofile(List<accountmodel> accounts)
        {
            File.WriteAllText(getfilepath(), string.Empty);
            foreach (accountmodel account in accounts)
            {
                saveinfile(account);
            }
        }

        public List<accountmodel> GetAccountfromfile()
        {
            List<accountmodel> accountlist = new List<accountmodel>();


            if (!File.Exists(getfilepath()))
            {
                return accountlist;
            }
            using (StreamReader reader = new StreamReader(getfilepath()))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    string[] parts = line.Split('|');
                    string accountNumber = parts[0];
                    int customerId = int.Parse(parts[1]);
                    string accountType = parts[2];
                    double balance = double.Parse(parts[3]);
                    string status = parts[4];
                    string createdDate = parts[5];

                    accountmodel accountmodel = new accountmodel(accountNumber, customerId, accountType, balance, status, createdDate);

                    accountlist.Add(accountmodel);
                }
            }
            return accountlist;
        }
    }
}

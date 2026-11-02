using System;
using System.Collections.Generic;
using System.IO;

namespace bank_project.transaction
{
    internal class transactionrepo
    {
        public readonly string file = "transactions.txt";

        public transactionrepo() { }
        public void saveinfile(transactionmodel transactionmodel)
        {
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine(transactionmodel.ToString());
            }
        }
        public void savealldataintofile(List<transactionmodel> transactions)
        {
            File.WriteAllText(file, string.Empty);
            foreach (transactionmodel transaction in transactions)
            {
                saveinfile(transaction);
            }
        }

        public List<transactionmodel> GetTransactionfromfile()
        {
            List<transactionmodel> transactionlist = new List<transactionmodel>();


            if (!File.Exists(file))
            {
                return transactionlist;
            }
            using (StreamReader reader = new StreamReader(file))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    string[] parts = line.Split('|');
                    int transactionId = int.Parse(parts[0]);
                    string accountNumber = parts[1];
                    string type = parts[2];
                    double amount = double.Parse(parts[3]);
                    string date = parts[4];
                    double balanceAfter = double.Parse(parts[5]);
                    string description = parts[6];

                    transactionmodel transactionmodel = new transactionmodel(transactionId, accountNumber, type, amount, date, balanceAfter, description);

                    transactionlist.Add(transactionmodel);
                }
            }
            return transactionlist;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using bank_project.common;

namespace bank_project.customer
{
    internal class customerrepo
    {
        private string getfilepath()
        {
            return datafilehelper.getresolvedfilepath("customers.txt");
        }
        public customerrepo() { }
        public void saveinfile(customermodel customermodel)
        {
            if (!File.Exists(getfilepath()))
            {
                File.Create(getfilepath()).Close();
            }
            using (StreamWriter writer = new StreamWriter(getfilepath(), true))
            {
                writer.WriteLine(customermodel.ToString());
            }
        }
        public void savealldataintofile(List<customermodel> customers)
        {
            File.WriteAllText(getfilepath(), string.Empty);
            foreach (customermodel customer in customers)
            {
                saveinfile(customer);
            }
        }

        public List<customermodel> GetCustomerfromfile()
        {
            List<customermodel> customerlist = new List<customermodel>();


            if (!File.Exists(getfilepath()))
            {
                return customerlist;
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
                    int customerId = int.Parse(parts[0]);
                    string name = parts[1];
                    string cnic = parts[2];
                    string phone = parts[3];
                    string address = parts[4];
                    int userId = int.Parse(parts[5]);

                    customermodel customermodel = new customermodel(customerId, name, cnic, phone, address, userId);

                    customerlist.Add(customermodel);
                }
            }
            return customerlist;
        }
    }
}

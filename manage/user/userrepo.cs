using System;
using System.Collections.Generic;
using System.IO;
using bank_project.common;

namespace bank_project.user
{
    internal class userrepo
    {
        private string getfilepath()
        {
            return datafilehelper.getresolvedfilepath("users.txt");
        }
        public userrepo() { }
        public void saveinfile(usermodel usermodel)
        {
            if (!File.Exists(getfilepath()))
            {
                File.Create(getfilepath()).Close();
            }
            using (StreamWriter writer = new StreamWriter(getfilepath(), true))
            {
                writer.WriteLine(usermodel.ToString());
            }
        }
        public void savealldataintofile(List<usermodel> users)
        {
            File.WriteAllText(getfilepath(), string.Empty);
            foreach (usermodel user in users)
            {
                saveinfile(user);
            }
        }

        public List<usermodel> GetUserfromfile()
        {
            List<usermodel> userlist = new List<usermodel>();


            if (!File.Exists(getfilepath()))
            {
                return userlist;
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
                    int userId = int.Parse(parts[0]);
                    string username = parts[1];
                    string password = parts[2];
                    int customerId = int.Parse(parts[3]);

                    usermodel usermodel = new usermodel(userId, username, password, customerId);

                    userlist.Add(usermodel);
                }
            }
            return userlist;
        }
    }
}

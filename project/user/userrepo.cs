using System;
using System.Collections.Generic;
using System.IO;

namespace bank_project.user
{
    internal class userrepo
    {
        public readonly string file = "users.txt";

        public userrepo() { }
        public void saveinfile(usermodel usermodel)
        {
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine(usermodel.ToString());
            }
        }
        public void savealldataintofile(List<usermodel> users)
        {
            File.WriteAllText(file, string.Empty);
            foreach (usermodel user in users)
            {
                saveinfile(user);
            }
        }

        public List<usermodel> GetUserfromfile()
        {
            List<usermodel> userlist = new List<usermodel>();


            if (!File.Exists(file))
            {
                return userlist;
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

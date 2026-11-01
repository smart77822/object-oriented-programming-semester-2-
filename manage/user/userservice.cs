using System;
using System.Collections.Generic;

namespace bank_project.user
{
    internal class userservice
    {
        private userrepo _repo;
        public userservice()
        {
            _repo = new userrepo();
        }

        public List<usermodel> getalldata()
        {
            return _repo.GetUserfromfile();
        }

        public void saveuser(usermodel user)
        {
            _repo.saveinfile(user);
        }

        public usermodel login(string username, string password)
        {
            foreach (usermodel user in _repo.GetUserfromfile())
            {
                if (user.getusername() == username && user.getpassword() == password)
                {
                    return user;
                }
            }
            return null;
        }

        public usermodel searchbyusername(string username)
        {
            foreach (usermodel user in _repo.GetUserfromfile())
            {
                if (user.getusername() == username)
                {
                    return user;
                }
            }
            return null;
        }

        public bool updatepassword(string username, string newPassword)
        {
            List<usermodel> users = _repo.GetUserfromfile();
            foreach (usermodel user in users)
            {
                if (user.getusername() == username)
                {
                    user.setpassword(newPassword);
                    _repo.savealldataintofile(users);
                    return true;
                }
            }
            return false;
        }
    }
}

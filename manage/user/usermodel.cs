using System;

namespace bank_project.user
{
    internal class usermodel
    {
        private int _userId;
        private string _username;
        private string _password;
        private int _customerId;

        public usermodel() { }
        public usermodel(int userId, string username, string password, int customerId)
        {
            this._userId = userId;
            this._username = username;
            this._password = password;
            this._customerId = customerId;
        }

        public void setuserid(int userId) { this._userId = userId; }
        public int getuserid() { return this._userId; }
        public void setusername(string username) { this._username = username; }
        public string getusername() { return this._username; }
        public void setpassword(string password) { this._password = password; }
        public string getpassword() { return this._password; }
        public void setcustomerid(int customerId) { this._customerId = customerId; }
        public int getcustomerid() { return this._customerId; }

        public override string ToString()
        {
            return _userId + "|" + _username + "|" + _password + "|" + _customerId;
        }
    }
}

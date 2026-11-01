using System;

namespace bank_project.account
{
    internal class accountmodel
    {
        private string _accountNumber;
        private int _customerId;
        private string _accountType;
        private double _balance;
        private string _status;
        private string _createdDate;

        public accountmodel() { }
        public accountmodel(string accountNumber, int customerId, string accountType, double balance, string status, string createdDate)
        {
            this._accountNumber = accountNumber;
            this._customerId = customerId;
            this._accountType = accountType;
            this._balance = balance;
            this._status = status;




            if (createdDate == null || createdDate == "")
            {
                this._createdDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                this._createdDate = createdDate;
            }
        }

        public void setaccountnumber(string accountNumber)
        {
            this._accountNumber = accountNumber;
        }
        public string getaccountnumber() { return this._accountNumber; }
        public void setcustomerid(int customerId) { this._customerId = customerId; }
        public int getcustomerid() { return this._customerId; }
        public void setaccounttype(string accountType) { this._accountType = accountType; }
        public string getaccounttype() { return this._accountType; }
        public void setbalance(double balance) { this._balance = balance; }
        public double getbalance() { return this._balance; }
        public void setstatus(string status) { this._status = status; }
        public string getstatus() { return this._status; }
        public void setcreateddate(string createdDate) { this._createdDate = createdDate; }
        public string getcreateddate() { return this._createdDate; }


        public override string ToString()
        {
            return $"{_accountNumber}|{_customerId}|{_accountType}|{_balance}|{_status}|{_createdDate}";
        }
    }
}

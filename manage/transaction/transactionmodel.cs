using System;

namespace bank_project.transaction
{
    internal class transactionmodel
    {
        private int _transactionId;
        private string _accountNumber;
        private string _type;
        private double _amount;
        private string _date;
        private double _balanceAfter;
        private string _description;

        public transactionmodel() { }
        public transactionmodel(int transactionId, string accountNumber, string type, double amount, string date, double balanceAfter, string description)
        {
            this._transactionId = transactionId;
            this._accountNumber = accountNumber;
            this._type = type;
            this._amount = amount;
            this._date = date;
            this._balanceAfter = balanceAfter;
            this._description = description;
        }

        public void settransactionid(int transactionId)
        {
            this._transactionId = transactionId;
        }
        public int gettransactionid() { return this._transactionId; }
        public void setaccountnumber(string accountNumber) { this._accountNumber = accountNumber; }
        public string getaccountnumber() { return this._accountNumber; }
        public void settype(string type) { this._type = type; }
        public string gettype() { return this._type; }
        public void setamount(double amount) { this._amount = amount; }
        public double getamount() { return this._amount; }
        public void setdate(string date) { this._date = date; }
        public string getdate() { return this._date; }
        public void setbalanceafter(double balanceAfter) { this._balanceAfter = balanceAfter; }
        public double getbalanceafter() { return this._balanceAfter; }
        public void setdescription(string description) { this._description = description; }
        public string getdescription() { return this._description; }


        public override string ToString()
        {
            return $"{_transactionId}|{_accountNumber}|{_type}|{_amount}|{_date}|{_balanceAfter}|{_description}";
        }
    }
}

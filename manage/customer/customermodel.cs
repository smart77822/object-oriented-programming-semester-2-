using System;

namespace bank_project.customer
{
    internal class customermodel
    {
        private int _customerId;
        private string _name;
        private string _cnic;
        private string _phone;
        private string _address;
        private int _userId;

        public customermodel() { }
        public customermodel(int customerId, string name, string cnic, string phone, string address, int userId)
        {
            this._customerId = customerId;
            this._name = name;
            this._cnic = cnic;
            this._phone = phone;
            this._address = address;
            this._userId = userId;
        }

        public void setcustomerid(int customerId)
        {
            this._customerId = customerId;
        }
        public int getcustomerid() { return this._customerId; }
        public void setname(string name) { this._name = name; }
        public string getname() { return this._name; }
        public void setcnic(string cnic) { this._cnic = cnic; }
        public string getcnic() { return this._cnic; }
        public void setphone(string phone) { this._phone = phone; }
        public string getphone() { return this._phone; }
        public void setaddress(string address) { this._address = address; }
        public string getaddress() { return this._address; }
        public void setuserid(int userId) { this._userId = userId; }
        public int getuserid() { return this._userId; }


        public override string ToString()
        {
            return $"{_customerId}|{_name}|{_cnic}|{_phone}|{_address}|{_userId}";
        }
    }
}

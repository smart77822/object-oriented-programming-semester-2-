using System;
using System.Collections.Generic;

namespace bank_project.customer
{
    internal class customerservice
    {
        private customerrepo _repo;
        public customerservice()
        {
            _repo = new customerrepo();
        }

        public List<customermodel> getalldata()
        {
            return _repo.GetCustomerfromfile();
        }
        public void savecustomer(customermodel customer)
        {
            _repo.saveinfile(customer);
        }

        public customermodel searchbyid(int customerId)
        {
            foreach (customermodel customer in _repo.GetCustomerfromfile())
            {
                if (customer.getcustomerid() == customerId)
                {
                    return customer;
                }
            }
            return null;
        }
       
        public customermodel searchbycnic(string cnic)
        {
            foreach (customermodel customer in _repo.GetCustomerfromfile())
            {
                if (customer.getcnic() == cnic)
                {
                    return customer;
                }
            }
            return null;
        }
        public bool updatecontact(int customerId, string phone, string address)
        {
            List<customermodel> customers = _repo.GetCustomerfromfile();
            foreach (customermodel customer in customers)
            {
                if (customer.getcustomerid() == customerId)
                {
                    customer.setphone(phone);
                    customer.setaddress(address);
                    _repo.savealldataintofile(customers);
                    return true;
                }
            }
            return false;
        }
    }
}

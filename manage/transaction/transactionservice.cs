using System;
using System.Collections.Generic;

namespace bank_project.transaction
{
    internal class transactionservice
    {
        private transactionrepo _repo;
        public transactionservice()
        {
            _repo = new transactionrepo();
        }

        public List<transactionmodel> getalldata()
        {
            return _repo.GetTransactionfromfile();
        }
        public void savetransaction(transactionmodel transaction)
        {
            _repo.saveinfile(transaction);
        }
        public void savealltransactions(List<transactionmodel> transactions)
        {
            _repo.savealldataintofile(transactions);
        }

        public List<transactionmodel> getbyaccountnumber(string accountNumber)
        {
            List<transactionmodel> result = new List<transactionmodel>();
            foreach (transactionmodel transaction in _repo.GetTransactionfromfile())
            {
                if (transaction.getaccountnumber() == accountNumber)
                {
                    result.Add(transaction);
                }
            }
            return result;
        }
        public List<transactionmodel> getlastn(string accountNumber, int n)
        {
            List<transactionmodel> all = getbyaccountnumber(accountNumber);
            List<transactionmodel> result = new List<transactionmodel>();
            int start = all.Count - n;
            if (start < 0)
            {
                start = 0;
            }
            for (int i = start; i < all.Count; i++)
            {
                result.Add(all[i]);
            }
            return result;
        }
    }
}

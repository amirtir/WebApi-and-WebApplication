using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWeb.Models;

namespace MyWeb.Contracts
{
    public interface ICustomerRepository
    {

        IEnumerable<Customers> GetAll();
        Customers Find(int id);
        Customers Add(Customers customer);
        Customers Update(Customers customer);
        Customers Delete(int id);
        bool IsExist(int id);
        int Count();
        
    }
}

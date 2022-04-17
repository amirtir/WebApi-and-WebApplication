using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWeb.Models;

namespace MyWeb.Contracts
{
 public interface ISalePersonRepository
    {
        IEnumerable<SalePersons> GetAll();
        SalePersons Find(int id);
        SalePersons Add(SalePersons SalePerson);
        SalePersons Update(SalePersons SalePerson);
        SalePersons Delete(int id);
        bool IsExist(int id);
        int Count();

    }
}

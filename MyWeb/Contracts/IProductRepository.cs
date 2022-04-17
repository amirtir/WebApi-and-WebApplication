using MyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.Contracts
{
   public interface IProductRepository
    {


        IEnumerable<Products> GetAll();
        Products Find(int id);
        Products Add(Products product);
        Products Update(Products product);
        Products Delete(int id);
        bool IsExist(int id);
        int Count();
    }
}

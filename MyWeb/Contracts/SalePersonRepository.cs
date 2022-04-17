using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWeb.Contracts
{
    public class SalePersonRepository : ISalePersonRepository
    {


        private EshopApi_DBContext _db;

        public SalePersonRepository(EshopApi_DBContext db)
        {
            _db = db;
        }


        public IEnumerable<SalePersons> GetAll()
        {
            return _db.SalePersons.ToList();
        }

        public SalePersons Find(int id)
        {
            return _db.SalePersons.Include(s => s.Orders).SingleOrDefault(c => c.SalePersonId == id);
        }

        public SalePersons Add(SalePersons SalePerson)
        {
            _db.SalePersons.Add(SalePerson);
            _db.SaveChanges();
            return (SalePerson);
        }

        public SalePersons Update(SalePersons SalePerson)
        {
            _db.SalePersons.Update(SalePerson);
            _db.SaveChanges();
            return SalePerson;
        }
        public SalePersons Delete(int id)
        {
            var SalePerson = _db.SalePersons.SingleOrDefault(s => s.SalePersonId == id);
            _db.SalePersons.Remove(SalePerson);
            _db.SaveChanges();
            return SalePerson;
        }

        public int Count()
        {
            return _db.SalePersons.Count();
        }

        public bool IsExist(int id)
        {
            return _db.SalePersons.Any(s => s.SalePersonId == id);
        }

    }
}

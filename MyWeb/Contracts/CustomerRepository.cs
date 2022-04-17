using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyWeb.Models;

namespace MyWeb.Contracts
{
    public class CustomerRepository : ICustomerRepository
    {
        private EshopApi_DBContext _db;
        private IMemoryCache _cache;

        public CustomerRepository( EshopApi_DBContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }


        public IEnumerable<Customers> GetAll()
        {
          return  _db.Customers.ToList();
        }

        public Customers Find(int id)
        {
            var cacheCustomer = _cache.Get<Customers>(id);

            if (cacheCustomer != null)
            {
                return cacheCustomer;

            }
            else
            {
             var customer= _db.Customers.Include(c => c.Orders).SingleOrDefault(c => c.CustomerId == id);

                _cache.Set<Customers>(id, customer, TimeSpan.FromHours(1));

                return customer;
            }


            
        }

        public Customers Add(Customers customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return (customer);
        }

        public Customers Update(Customers customer)
        {
            _cache.Set<Customers>(customer.CustomerId, customer);
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return customer;
        }
        public Customers Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return customer;
        }

        public int Count()
        {
          return  _db.Customers.Count();
        }

        public bool IsExist(int id)
        {
            return _db.Customers.Any(c => c.CustomerId == id);
        }

       
    }
}

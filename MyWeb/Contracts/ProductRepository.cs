using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWeb.Models;
using Microsoft.EntityFrameworkCore;
namespace MyWeb.Contracts
{
    public class ProductRepository : IProductRepository
    {

        private EshopApi_DBContext _db;
        public ProductRepository(EshopApi_DBContext db)
        {
            _db = db;
        }

        public IEnumerable<Products> GetAll()
        {
            return _db.Products.ToList();
        }

        public Products Find(int id)
        {
            return _db.Products.Include(p => p.OrderItem).SingleOrDefault(p => p.ProductId == id);
        }


        public Products Add(Products product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Products Update(Products product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return product;
        }
        public Products Delete(int id)
        {
            var product = _db.Products.SingleOrDefault(p => p.ProductId == id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }
        public int Count()
        {
         return _db.Products.Count();
        }

        public bool IsExist(int id)
        {
         return _db.Products.Any(p => p.ProductId == id);
        }

       
    }
}

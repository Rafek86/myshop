using myshop.DataAccess.Data;
using myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; } 
        public IShoppingCartRepository ShoppingCart { get; private set; }   
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category=new CategoryRepository(_context);
            Product=new ProductRepository(_context);
            ShoppingCart=new ShoppingCartRepository(_context);  
        }

        public int Complete()
        {
         return _context.SaveChanges();
        }

        public void Dispose()
        {
           _context.Dispose();
        }
    }
}

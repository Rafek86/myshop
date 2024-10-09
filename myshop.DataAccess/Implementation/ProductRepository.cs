using myshop.DataAccess.Data;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DataAccess.Implementation
{
    public class ProductRepository :GenericRepository<Product>, IProductRepository  
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productinDb = _context.Products.FirstOrDefault(x => x.Id == product.Id);

            if (productinDb != null)
            {
                productinDb.Name = product.Name;
                productinDb.Description = product.Description;
                productinDb.Price = product.Price;
                productinDb.CategoryId = product.CategoryId;    
                productinDb.Img = product.Img;
            }
        }
    }
}

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
    public class CategoryRepository :GenericRepository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) :base (context)
        { 
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryinDb =_context.Categories.FirstOrDefault(x=>x.Id == category.Id);
            
            if (categoryinDb != null) { 
                categoryinDb.Name = category.Name;  
                categoryinDb.Description = category.Description;
                categoryinDb.CreatedTime = DateTime.Now;    
            }
        }
    }
}

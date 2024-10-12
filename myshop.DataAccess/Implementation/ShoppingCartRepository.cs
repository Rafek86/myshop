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
    public class ShoppingCartRepository :GenericRepository<ShoppingCart>,IShoppingCartRepository
    {
        public readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context):
            base(context) { 
        _context = context; 
        }

        public int DecreaseCount(ShoppingCart shoppingCart, int count)
        {
           shoppingCart.Count-=count;   
            return shoppingCart.Count;
        }

        public int IncreaseCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
    }
}

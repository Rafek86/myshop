﻿using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.ViewModels
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> CartsList { get; set; }

		public decimal TotalCarts { get; set; }

	}
}

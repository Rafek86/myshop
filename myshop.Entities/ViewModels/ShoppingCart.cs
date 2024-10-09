﻿using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.ViewModels
{
    public class ShoppingCart
    {
        public Product Product { get; set; }

        [Range(1,100,ErrorMessage ="You Must Enter value between 1 to 100")]
        public int Count { get; set; }  
    }
}

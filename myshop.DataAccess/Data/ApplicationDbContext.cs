﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myshop.Entities.Models;


namespace myshop.DataAccess.Data;

public class ApplicationDbContext :IdentityDbContext <IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
        
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }    

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }        
    public DbSet<ShoppingCart> ShoppingCarts  { get; set; }        
}
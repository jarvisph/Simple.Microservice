﻿using Microsoft.EntityFrameworkCore;
using Simple.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.DBContext
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
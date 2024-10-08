﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Object.Models;

namespace Object.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<Races> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}

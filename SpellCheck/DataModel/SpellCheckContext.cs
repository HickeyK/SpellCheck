﻿using SpellCheck.Entities;
using System.Data.Entity;

namespace SpellCheck
{
    public class SpellCheckContext : DbContext 
    {
        public DbSet<SpellTest> SpellTest { get; set; }

        public DbSet<Spelling> Spelling { get; set; }

        public DbSet<TestOccurance> TestOccurance { get; set; }

        public DbSet<TestAnswer> TestAnswer { get; set; }

    }
}

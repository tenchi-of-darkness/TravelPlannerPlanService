﻿using Hike.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hike.Data.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(_configuration.GetConnectionString("NoDatabase")),
            options =>
            {
                options.UseNetTopologySuite();
            });
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrailEntity>().HasKey(x => x.Id);
    }
    
    public DbSet<TrailEntity> Trails { get; set; }
}
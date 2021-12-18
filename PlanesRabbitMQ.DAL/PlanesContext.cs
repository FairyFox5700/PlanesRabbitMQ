using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanesRabbitMQ.DAL.Entities;

namespace PlanesRabbitMQ.DAL
{
    public class PlanesContext:DbContext
    {
        public  virtual  DbSet<Plane> Planes { get; set; }
        public  virtual  DbSet<Chars> Chars { get; set; }
        public  virtual  DbSet<Parameters> Parameters { get; set; }
        public  virtual  DbSet<ParametersCharacteristics> ParametersCharacteristics { get; set; }
        public  virtual  DbSet<ScoutCharacteristics> ScoutCharacteristics { get; set; }
        
        public PlanesContext(DbContextOptions<PlanesContext> context):base(context)
        {
            
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plane>()
                .HasOne<Chars>(e => e.Chars)
                .WithMany();

            modelBuilder.Entity<Chars>()
                .HasOne<ScoutCharacteristics>()
                .WithMany();
            
            var converter = new ValueConverter<PlaneType, string>(
                v => v.ToString(),
                v => (PlaneType)Enum.Parse(typeof(PlaneType), v));

            modelBuilder.Entity<Chars>()
                .Property(e => e.Type)
                .HasConversion(converter);
        }
    }
}
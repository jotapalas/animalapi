using Microsoft.EntityFrameworkCore;
using AnimalApi.Models;    

namespace AnimalApi.Context {
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options){}
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users {get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Owner)
                .WithMany(owner => owner.pets)
                .HasForeignKey(pet => pet.OwnerId);  
            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Animal)
                .WithMany(animal => animal.owners)
                .HasForeignKey(pet => pet.AnimalId);
        }
    }
}
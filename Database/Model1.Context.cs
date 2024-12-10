﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shelter.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShelterEntities : DbContext
    {
        public ShelterEntities()
            : base("name=ShelterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Breed> Breed { get; set; }
        public virtual DbSet<BreedStatistic> BreedStatistic { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodAttribute> FoodAttribute { get; set; }
        public virtual DbSet<FoodNutrientDerivation> FoodNutrientDerivation { get; set; }
        public virtual DbSet<FoodNutrientSource> FoodNutrientSource { get; set; }
        public virtual DbSet<FoodPortion> FoodPortion { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Nutrient> Nutrient { get; set; }
        public virtual DbSet<NutrientConversionFactor> NutrientConversionFactor { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TakingAnimal> TakingAnimal { get; set; }
        public virtual DbSet<WCFStatus> WCFStatus { get; set; }
        public virtual DbSet<InputFoods> InputFoods { get; set; }
    }
}

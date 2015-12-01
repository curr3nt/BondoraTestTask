using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public class DalContext : DbContext
    {
        /// <summary>
        /// This EF's DbContext wrapper uses the lazy loading for loading related entities
        /// </summary>
        public DalContext()
            : base("DalContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }
        
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceRow> InvoiceRows { get; set; }
        public DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

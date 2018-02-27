using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsApi.Models
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options)
        { }

        public DbSet<CountryModel> Country { get; set; }
        public DbSet<SmsModel> Sms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<CountryModel>().Property(x => x.sms_price)..HasPrecision(16, 3);
        }

    }
}

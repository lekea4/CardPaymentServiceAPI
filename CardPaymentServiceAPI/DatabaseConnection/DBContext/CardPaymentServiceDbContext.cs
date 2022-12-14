using CardPaymentServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CardPaymentServiceAPI.DatabaseConnection.DBContext
{
    public class CardPaymentServiceDbContext  : DbContext
    {
        public CardPaymentServiceDbContext(DbContextOptions<CardPaymentServiceDbContext> options) : base(options)
        {

        }

        public DbSet<CardsDetails> CardsDetails { get; set; }
        public DbSet<Fintechs> Fintechs { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<ReoccuringPayment> ReoccuringPayment { get; set; }
        public DbSet<ReoccuringPaymentFrquency> ReoccuringPaymentFrquency { get; set; }
    }
}

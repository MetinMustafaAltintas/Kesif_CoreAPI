using Kesif_CoreAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kesif_CoreAPI.Models.Seed
{
    public static class DataSeedExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            UserCardInfo ucInfo = new()
            {
                ID = 1,
                Balance = 100000,
                CardLimit = 100000,
                CardNumber = "1111 1111 1111 1111",
                CardUserName = "Kesif Dünyası",
                CCV = "222",
                ExpiryMonth = 12,
                ExpiryYear = 2024

            };

            modelBuilder.Entity<UserCardInfo>().HasData(ucInfo);
        }
    }
}

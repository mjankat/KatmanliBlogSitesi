using KatmanliBlogSitesi.Entities;
using Microsoft.EntityFrameworkCore;

namespace KatmanliBlogSitesi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // OnConfiguring metodu entity framework core içerisinden gelir ve veritabanı ayarlarını yapabilmemizi sağlar.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=KatmanliBlogSitesi; integrated security=true"); // Burada uygulamamıza sql server kullanacağımızı entity framework core a belirttik. UseSqlServer metoduna () içerisinde connection string ile veritabanı bilgilerimizi parametre olarak gönderebiliyoruz. 
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aşağıdaki metot uygulama ilk defa çalıştığında veritabanı oluşturduktan sonra admin paneline giriş yapabilmek için veritabanındaki users tablosuna 1 tane kayıt ekler.
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    Email = "admin@blogsitesi.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "admin",
                    Surname = "admin",
                    Password = "123"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
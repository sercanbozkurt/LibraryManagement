using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data{

    public class DataContext : DbContext{

        public DataContext(DbContextOptions<DataContext>options):base(options){}

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Reservation> Reservations => Set<Reservation>();


        
    }
}
using BookSrote.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {


        }

        public DbSet<Books> Books { get; set; }

        /*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=.; Database=BookStoreAPINew;Integrated Security=True");
                base.OnConfiguring(optionsBuilder);
            }*/
    }
}
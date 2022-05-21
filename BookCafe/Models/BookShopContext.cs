using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookShop.Models
{
    public partial class BookShopContext : DbContext
    {
        public BookShopContext()
        {
        }

        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookProvider> BookProviders { get; set; }
        public virtual DbSet<BooksImage> BooksImages { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<DonatedBook> DonatedBooks { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<OrderClient> OrderClients { get; set; }
        public virtual DbSet<ProvidingBook> ProvidingBooks { get; set; }
        public virtual DbSet<Publishing> Publishings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS01;Initial Catalog=Book_Shop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuthorCode)
                    .HasName("PK__Author__A750C0F7D9BD126E");

                entity.ToTable("Author");

                entity.HasIndex(e => e.Fio, "UQ__Author__D9908D6EDB7C8B72")
                    .IsUnique();

                entity.Property(e => e.AuthorCode).HasColumnName("author_code");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("fio");

                entity.Property(e => e.YearOfBirth).HasColumnName("year_of_birth");

                entity.Property(e => e.YearOfDeath).HasColumnName("year_of_death");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookCode)
                    .HasName("PK__Book__3EAC6781824388EE");

                entity.ToTable("Book");

                entity.HasIndex(e => e.Title, "UQ__Book__E52A1BB306760590")
                    .IsUnique();

                entity.Property(e => e.BookCode).HasColumnName("book_code");

                entity.Property(e => e.AuthorCode).HasColumnName("author_code");

                entity.Property(e => e.GenreCode).HasColumnName("genre_code");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.PublishingCode).HasColumnName("publishing_code");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.YearOfPublishing).HasColumnName("year_of_publishing");

                entity.Property(e => e.YearOfWriting).HasColumnName("year_of_writing");

                entity.HasOne(d => d.AuthorCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Author_Book");

                entity.HasOne(d => d.GenreCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Genre_Book");

                entity.HasOne(d => d.ImgNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Img)
                    .HasConstraintName("FK_Book_Image");

                entity.HasOne(d => d.PublishingCodeNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublishingCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Publishing_Book");
            });

            modelBuilder.Entity<BookProvider>(entity =>
            {
                entity.HasKey(e => e.ProviderCode)
                    .HasName("PK__Book_pro__21A8A1045F90D4E4");

                entity.ToTable("Book_provider");

                entity.HasIndex(e => e.Title, "UQ__Book_pro__E52A1BB3D353C5A1")
                    .IsUnique();

                entity.Property(e => e.ProviderCode).HasColumnName("provider_code");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<BooksImage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasColumnType("image");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientCode)
                    .HasName("PK__Client__EC5CBC36414C0402");

                entity.ToTable("Client");

                entity.HasIndex(e => e.Phone, "UQ__Client__B43B145F29F52B7E")
                    .IsUnique();

                entity.HasIndex(e => e.Fio, "UQ__Client__D9908D6EF58900E5")
                    .IsUnique();

                entity.Property(e => e.ClientCode).HasColumnName("client_code");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.DiscountCode).HasColumnName("discount_code");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("fio");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.DiscountCodeNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.DiscountCode)
                    .HasConstraintName("FK_Client_Discount");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountCode)
                    .HasName("PK__Discount__75C1F007DD3F30D0");

                entity.ToTable("Discount");

                entity.HasIndex(e => e.Title, "UQ__Discount__E52A1BB3F110F5DB")
                    .IsUnique();

                entity.Property(e => e.DiscountCode).HasColumnName("discount_code");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<DonatedBook>(entity =>
            {
                entity.HasKey(e => new { e.ClientCode, e.BookCode })
                    .HasName("PK_Book_Donate");

                entity.ToTable("Donated_books");

                entity.Property(e => e.ClientCode).HasColumnName("client_code");

                entity.Property(e => e.BookCode).HasColumnName("book_code");

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.DonatedBooks)
                    .HasForeignKey(d => d.BookCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonatedBooks_Book");

                entity.HasOne(d => d.ClientCodeNavigation)
                    .WithMany(p => p.DonatedBooks)
                    .HasForeignKey(d => d.ClientCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonatedBooks_Client");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreCode)
                    .HasName("PK__Genre__FDF55E468547D9C3");

                entity.ToTable("Genre");

                entity.HasIndex(e => e.DescriptionGenre, "UQ__Genre__C631F9A883E6B112")
                    .IsUnique();

                entity.HasIndex(e => e.Title, "UQ__Genre__E52A1BB3FCDB7FDD")
                    .IsUnique();

                entity.Property(e => e.GenreCode).HasColumnName("genre_code");

                entity.Property(e => e.DescriptionGenre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description_genre");

                entity.Property(e => e.DiscountCode).HasColumnName("discount_code");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.DiscountCodeNavigation)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.DiscountCode)
                    .HasConstraintName("FK_Genre_Discount");
            });

            modelBuilder.Entity<OrderClient>(entity =>
            {
                entity.HasKey(e => e.OrderCode)
                    .HasName("PK__Order_cl__99D12D3E9104C7C7");

                entity.ToTable("Order_client");

                entity.Property(e => e.OrderCode).HasColumnName("order_code");

                entity.Property(e => e.BookCode).HasColumnName("book_code");

                entity.Property(e => e.ClientCode).HasColumnName("client_code");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.OrderClients)
                    .HasForeignKey(d => d.BookCode)
                    .HasConstraintName("FK_Order_Book");

                entity.HasOne(d => d.ClientCodeNavigation)
                    .WithMany(p => p.OrderClients)
                    .HasForeignKey(d => d.ClientCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Order");
            });

            modelBuilder.Entity<ProvidingBook>(entity =>
            {
                entity.HasKey(e => new { e.ProviderCode, e.BookCode })
                    .HasName("PK_Book_Providint");

                entity.ToTable("Providing_books");

                entity.Property(e => e.ProviderCode).HasColumnName("provider_code");

                entity.Property(e => e.BookCode).HasColumnName("book_code");

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.ProvidingBooks)
                    .HasForeignKey(d => d.BookCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProvidingBooks_Book");

                entity.HasOne(d => d.ProviderCodeNavigation)
                    .WithMany(p => p.ProvidingBooks)
                    .HasForeignKey(d => d.ProviderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProvidingBooks_Provider");
            });

            modelBuilder.Entity<Publishing>(entity =>
            {
                entity.HasKey(e => e.PublishingCode)
                    .HasName("PK__Publishi__7C4D091F3A911FDB");

                entity.ToTable("Publishing");

                entity.HasIndex(e => e.Title, "UQ__Publishi__E52A1BB3206CEDE6")
                    .IsUnique();

                entity.Property(e => e.PublishingCode).HasColumnName("publishing_code");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }
        public DbSet<Crew> Crew { get; set; }
        public DbSet<MovieCrew> MovieCrew { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }
        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(20);
        }
        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18, 2)");
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(x => new { x.MovieId, x.UserId });
            builder.Property(x => x.Rating).HasColumnType("decimal(3, 2)");
        }
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(x => x.Id);
        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // you can specify the Fluent API Rules
            // another way apart from data annoations for your schema

            builder.ToTable("Movie");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);

            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Gender);
            builder.Property(x => x.TmdbUrl);
            builder.Property(x => x.ProfilePath).HasMaxLength(2084);

        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(x => new { x.MovieId, x.CastId, x.Character });
        }
        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Gender);
            builder.Property(x => x.TmdbUrl);
            builder.Property(x => x.ProfilePath).HasMaxLength(2084);

        }
        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(x => new { x.MovieId, x.CrewId, x.Department, x.Job });
        }
        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(128);
            builder.Property(x => x.LastName).HasMaxLength(128);
            builder.Property(x => x.DateOfBirth).HasMaxLength(7);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.HashedPassword).HasMaxLength(1024);
            builder.Property(x => x.Salt).HasMaxLength(1024);
            builder.Property(x => x.PhoneNumber).HasMaxLength(16);
            builder.Property(x => x.TwoFactorEnabled);
            builder.Property(x => x.LockoutEndDate).HasMaxLength(7);
            builder.Property(x => x.LastLoginDateTime).HasMaxLength(7);
            builder.Property(x => x.IsLocked);
            builder.Property(x => x.AccessFailedCount);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EventImage> EventImages { get; set; }
    public DbSet<EventRating> EventRatings { get; set; }
    public DbSet<EventInvitation> EventInvitations { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>().HasData(
               new EventType { Id = 1, Name = "Koncert" },
               new EventType { Id = 2, Name = "Sport" },
               new EventType { Id = 3, Name = "Film" },
               new EventType { Id = 4, Name = "Zábava" },
               new EventType { Id = 5, Name = "Děti" },
               new EventType { Id = 6, Name = "Workshop" },
               new EventType { Id = 7, Name = "Vzdělávání" },
               new EventType { Id = 8, Name = "Turistika" },
               new EventType { Id = 9, Name = "Divadlo" },
               new EventType { Id = 10, Name = "Jiné" }

           );
        // Vztah mezi uživateli a událostmi
        modelBuilder.Entity<User>()
            .HasMany(u => u.SubmittedEvents) // Uživatel může mít více událostí
            .WithOne(e => e.Organizer) // Každá událost má jednoho organizátora
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict); // Nesmí se smazat události při smazání uživatele

        // Vztah mezi uživateli a přáteli
        modelBuilder.Entity<User>()
            .HasMany(u => u.Friends) // Uživatel může mít více přátel
            .WithMany()
            .UsingEntity<FriendRequest>(
                j => j
                    .HasOne(fr => fr.ToUser)
                    .WithMany()
                    .HasForeignKey(fr => fr.ToUserId)
                    .OnDelete(DeleteBehavior.Cascade), // Smazání požadavku na přátelství
                j => j
                    .HasOne(fr => fr.FromUser)
                    .WithMany()
                    .HasForeignKey(fr => fr.FromUserId)
                    .OnDelete(DeleteBehavior.Cascade)); // Smazání požadavku na přátelství

        // Vztah mezi událostmi a obrázky
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Images) // Událost může mít více obrázků
            .WithOne() // Obrázky nevyžadují navigační vlastnost zpět k události
            .HasForeignKey(img => img.EventId) // Cizí klíč
            .OnDelete(DeleteBehavior.Cascade); // Smazání obrázků při smazání události

        // Vztah mezi událostmi a hodnoceními
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Ratings) // Událost může mít více hodnocení
            .WithOne() // Hodnocení nevyžadují navigační vlastnost zpět k události
            .HasForeignKey(r => r.EventId) // Cizí klíč
            .OnDelete(DeleteBehavior.Cascade); // Smazání hodnocení při smazání události

        // Vztah mezi událostmi a pozvánkami
        modelBuilder.Entity<Event>()
            .HasMany(e => e.InvitedGuests) // Událost může mít více pozvánek
            .WithOne(inv => inv.Event) // Každá pozvánka je spojena s jednou událostí
            .HasForeignKey(inv => inv.EventId) // Cizí klíč
            .OnDelete(DeleteBehavior.Cascade); // Smazání pozvánek při smazání události
    }
}



using JdemeVen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace JdemeVen.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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
            // Přednastavení typů událostí
            modelBuilder.Entity<EventType>().HasData(
                new EventType { Id = 1, Name = "Koncert" },
                new EventType { Id = 2, Name = "Divadlo" },
                new EventType { Id = 3, Name = "Děti" },
                new EventType { Id = 4, Name = "Vzdělávání" },
                new EventType { Id = 5, Name = "Zábava" },
                new EventType { Id =6, Name = "Film" },
                new EventType { Id = 7, Name = "Workshop" },
                new EventType { Id = 8, Name = "Jiné" },
                new EventType { Id = 9, Name = "Komunitní" }
                );

            // Vztah mezi uživateli a událostmi
            modelBuilder.Entity<User>()
                .HasMany(u => u.SubmittedEvents)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
               .HasOne(fr => fr.FromUser)
               .WithMany(u => u.FriendRequestsSent)
               .HasForeignKey(fr => fr.FromUserId)
               .OnDelete(DeleteBehavior.Restrict); // Zamezení automatického mazání

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.ToUser)
                .WithMany(u => u.FriendRequestsReceived)
                .HasForeignKey(fr => fr.ToUserId)
                .OnDelete(DeleteBehavior.Restrict); // Zamezení automatického mazání

            // Vztah mezi událostmi a obrázky
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Images)
                .WithOne(img => img.Event)
                .HasForeignKey(img => img.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vztah mezi událostmi a hodnoceními
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Ratings)
                .WithOne(er => er.Event)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vztah mezi událostmi a pozvánkami
            modelBuilder.Entity<Event>()
                .HasMany(e => e.InvitedGuests)
                .WithOne(inv => inv.Event)
                .HasForeignKey(inv => inv.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            // Vztah mezi událostmi a typy událostí
            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventType)
                .WithMany() // Pokud chcete, aby typ události mohl mít více událostí
                .HasForeignKey(e => e.EventTypeId);
        }
    }
}

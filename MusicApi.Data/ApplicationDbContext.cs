using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data.Entities;

namespace MusicApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<LabelEntity> Labels { get; set; }

        public DbSet<ArtistEntity> Artists { get; set; }

        public DbSet<SongEntity> Songs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Establishing many to one relationship between Label table and Artist table (one label can have many artists, but artists only have one label)
            modelBuilder.Entity<ArtistEntity>()
            .HasOne(a => a.Label)
            .WithMany(l => l.Artists)
            .HasForeignKey(a => a.LabelId);

            //Establishing many to one relationship between Label table and Song table (one label can have many songs, but songs can only have one label)

            modelBuilder.Entity<SongEntity>()
            .HasOne(s => s.Label)
            .WithMany(l => l.Songs)
            .HasForeignKey(s => s.LabelId);

            //Establishing many to one relationship between Artist table and Song table (one artist can have many songs, but songs can only have one artist)
            //will probably change to a many to many relationship down the line 

            modelBuilder.Entity<SongEntity>()
            .HasOne(s => s.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId);
        }



    }
}
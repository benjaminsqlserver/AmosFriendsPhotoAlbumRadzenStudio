using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using AmosFriendsPhotoAlbum.Models.ConData;

namespace AmosFriendsPhotoAlbum.Data
{
  public partial class ConDataContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public ConDataContext(DbContextOptions<ConDataContext> options):base(options)
    {
    }

    public ConDataContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.Friend>()
              .HasOne(i => i.Gender)
              .WithMany(i => i.Friends)
              .HasForeignKey(i => i.GenderID)
              .HasPrincipalKey(i => i.GenderID);
        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>()
              .HasOne(i => i.Friend)
              .WithMany(i => i.FriendPhotos)
              .HasForeignKey(i => i.FriendID)
              .HasPrincipalKey(i => i.FriendID);


        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.Friend>()
              .Property(p => p.DateOfBirth)
              .HasColumnType("date");

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>()
              .Property(p => p.DateTaken)
              .HasColumnType("date");

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.Friend>()
              .Property(p => p.FriendID)
              .HasPrecision(10, 0);

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.Friend>()
              .Property(p => p.GenderID)
              .HasPrecision(10, 0);

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>()
              .Property(p => p.PhotoID)
              .HasPrecision(10, 0);

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>()
              .Property(p => p.FriendID)
              .HasPrecision(10, 0);

        builder.Entity<AmosFriendsPhotoAlbum.Models.ConData.Gender>()
              .Property(p => p.GenderID)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<AmosFriendsPhotoAlbum.Models.ConData.Friend> Friends
    {
      get;
      set;
    }

    public DbSet<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> FriendPhotos
    {
      get;
      set;
    }

    public DbSet<AmosFriendsPhotoAlbum.Models.ConData.Gender> Genders
    {
      get;
      set;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }
  }
}

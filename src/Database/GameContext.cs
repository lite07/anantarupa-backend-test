using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Anantarupa.Database.Models;

namespace Anantarupa.Database
{
    public partial class GameContext : DbContext
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ShopItem> ShopItems { get; set; } = null!;
        public virtual DbSet<UserCurrency> UserCurrencies { get; set; } = null!;
        public virtual DbSet<UserDatum> UserData { get; set; } = null!;
        public virtual DbSet<UserInventory> UserInventories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=./game.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currencies");

                entity.Property(e => e.CurrencyId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("currency_id");

                entity.Property(e => e.CurrencyName)
                    .HasColumnType("VARCHAR(10)")
                    .HasColumnName("currency_name");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.ItemId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("item_id");

                entity.Property(e => e.ItemName)
                    .HasColumnType("VARCHAR(20)")
                    .HasColumnName("item_name");
            });

            modelBuilder.Entity<ShopItem>(entity =>
            {
                entity.ToTable("shop_items");

                entity.Property(e => e.ShopItemId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("shop_item_id");

                entity.Property(e => e.AllowedQuantity)
                    .HasColumnType("INT")
                    .HasColumnName("allowed_quantity");

                entity.Property(e => e.CurrencyType)
                    .HasColumnType("INT")
                    .HasColumnName("currency_type");

                entity.Property(e => e.ItemId)
                    .HasColumnType("INT")
                    .HasColumnName("item_id");

                entity.Property(e => e.Price)
                    .HasColumnType("INT")
                    .HasColumnName("price");

                entity.HasOne(d => d.CurrencyTypeNavigation)
                    .WithMany(p => p.ShopItems)
                    .HasForeignKey(d => d.CurrencyType)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ShopItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserCurrency>(entity =>
            {
                entity.ToTable("user_currency");

                entity.Property(e => e.UserCurrencyId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("user_currency_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("INT")
                    .HasColumnName("amount");

                entity.Property(e => e.CurrencyType)
                    .HasColumnType("INT")
                    .HasColumnName("currency_type")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserId)
                    .HasColumnType("INT")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.CurrencyTypeNavigation)
                    .WithMany(p => p.UserCurrencies)
                    .HasForeignKey(d => d.CurrencyType)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCurrencies)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("user_data");

                entity.Property(e => e.UserId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("TIMESTAMP")
                    .HasColumnName("join_date");

                entity.Property(e => e.Username)
                    .HasColumnType("VARCHAR(20)")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserInventory>(entity =>
            {
                entity.ToTable("user_inventory");

                entity.HasIndex(e => new { e.UserId, e.ItemId }, "IX_user_inventory_user_id_item_id")
                    .IsUnique();

                entity.Property(e => e.UserInventoryId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever()
                    .HasColumnName("user_inventory_id");

                entity.Property(e => e.ItemId)
                    .HasColumnType("INT")
                    .HasColumnName("item_id");

                entity.Property(e => e.Quantity)
                    .HasColumnType("INT")
                    .HasColumnName("quantity");

                entity.Property(e => e.UserId)
                    .HasColumnType("INT")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.UserInventories)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInventories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

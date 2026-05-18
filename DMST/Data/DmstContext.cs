using System;
using System.Collections.Generic;
using DMST.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DMST.Data;

public partial class DmstContext : DbContext
{
    public DmstContext()
    {
    }

    public DmstContext(DbContextOptions<DmstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<CharacterClassRel> CharacterClassRels { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ItemLoot> ItemLoots { get; set; }

    public virtual DbSet<Npc> Npcs { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Playercharacter> Playercharacters { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<Sessionlog> Sessionlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=DMST;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.3-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PRIMARY");

            entity.ToTable("campaign");

            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Setting).HasMaxLength(255);
        });

        modelBuilder.Entity<CharacterClassRel>(entity =>
        {
            entity.HasKey(e => new { e.CharId, e.ClassId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("character_class_rel");

            entity.HasIndex(e => e.ClassId, "fk_ccrel_class");

            entity.Property(e => e.CharId).HasColumnName("Char_ID");
            entity.Property(e => e.ClassId).HasColumnName("Class_ID");
            entity.Property(e => e.Lvl).HasColumnName("LVL");

            entity.HasOne(d => d.Char).WithMany(p => p.CharacterClassRels)
                .HasForeignKey(d => d.CharId)
                .HasConstraintName("fk_ccrel_character");

            entity.HasOne(d => d.Class).WithMany(p => p.CharacterClassRels)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ccrel_class");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PRIMARY");

            entity.ToTable("classes");

            entity.Property(e => e.ClassId).HasColumnName("Class_ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.HpDice)
                .HasMaxLength(10)
                .HasColumnName("HP_Dice");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SpellCast)
                .HasMaxLength(100)
                .HasColumnName("Spell_Cast");
        });

        modelBuilder.Entity<ItemLoot>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("item_loot");

            entity.HasIndex(e => e.PlaceId, "fk_item_place");

            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.Attributes).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PlaceId).HasColumnName("Place_ID");
            entity.Property(e => e.Rarity).HasMaxLength(100);
            entity.Property(e => e.Weight).HasPrecision(8, 2);

            entity.HasOne(d => d.Place).WithMany(p => p.ItemLoots)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_item_place");
        });

        modelBuilder.Entity<Npc>(entity =>
        {
            entity.HasKey(e => e.NpcId).HasName("PRIMARY");

            entity.ToTable("npc");

            entity.HasIndex(e => e.CampaignId, "fk_npc_campaign");

            entity.HasIndex(e => e.PlaceId, "fk_npc_place");

            entity.Property(e => e.NpcId).HasColumnName("NPC_ID");
            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Mindset).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.PlaceId).HasColumnName("Place_ID");
            entity.Property(e => e.Race).HasMaxLength(100);

            entity.HasOne(d => d.Campaign).WithMany(p => p.Npcs)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("fk_npc_campaign");

            entity.HasOne(d => d.Place).WithMany(p => p.Npcs)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_npc_place");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PRIMARY");

            entity.ToTable("place");

            entity.HasIndex(e => e.CampaignId, "fk_place_campaign");

            entity.Property(e => e.PlaceId).HasColumnName("Place_ID");
            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Campaign).WithMany(p => p.Places)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("fk_place_campaign");
        });

        modelBuilder.Entity<Playercharacter>(entity =>
        {
            entity.HasKey(e => e.CharId).HasName("PRIMARY");

            entity.ToTable("playercharacter");

            entity.HasIndex(e => e.CampaignId, "fk_playerchar_campaign");

            entity.Property(e => e.CharId).HasColumnName("Char_ID");
            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Race).HasMaxLength(100);
            entity.Property(e => e.Spells).HasColumnType("text");
            entity.Property(e => e.Weapons).HasColumnType("text");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Playercharacters)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("fk_playerchar_campaign");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.QuestId).HasName("PRIMARY");

            entity.ToTable("quests");

            entity.HasIndex(e => e.CampaignId, "fk_quests_campaign");

            entity.Property(e => e.QuestId).HasColumnName("Quest_ID");
            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Requirement).HasColumnType("text");
            entity.Property(e => e.Reward).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Campaign).WithMany(p => p.Quests)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("fk_quests_campaign");
        });

        modelBuilder.Entity<Sessionlog>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PRIMARY");

            entity.ToTable("sessionlog");

            entity.HasIndex(e => e.CampaignId, "fk_sessionlog_campaign");

            entity.Property(e => e.SessionId).HasColumnName("Session_ID");
            entity.Property(e => e.CampaignId).HasColumnName("Campaign_ID");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Summary).HasColumnType("text");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Sessionlogs)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("fk_sessionlog_campaign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

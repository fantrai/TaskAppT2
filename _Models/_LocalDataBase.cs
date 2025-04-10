using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;

namespace TaskAppT2._Models
{
    partial class LocalDataBase : DbContext
    {
        public LocalDataBase()
        {
            //Database.EnsureDeleted();
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbmane = "DataBase.db3";
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbmane);

            optionsBuilder.UseSqlite($"Filename={dbPath}")
                .UseLazyLoadingProxies();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<CardSetCathegory> CardSetsCathegorys { get; set; }
        public DbSet<CathegoryNote> CathegoryNotes { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<IntervalPattern> IntervalPatterns { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}

using InlamningDatabase2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InlamningDatabase2.Context;

internal class DataContext : DbContext
{
    public DataContext()
    {

    }


    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\C#\InlamningDatabase2\InlamningDatabase2\Context\LocalSqlDb.mdf;Integrated Security=True;Connect Timeout=30");
    }

    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CaseEntity> Cases { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

}

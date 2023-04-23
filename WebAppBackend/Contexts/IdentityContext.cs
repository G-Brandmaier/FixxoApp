using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Contexts;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ShowcaseEntity> Showcases { get; set; }
    public DbSet<UserEntity> UserProfiles { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
}

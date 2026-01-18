using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Blog;
using Api.KapiDunyasi.Domain.Categories;
using Api.KapiDunyasi.Domain.Contacts;
using Api.KapiDunyasi.Domain.Faqs;
using Api.KapiDunyasi.Domain.Orders;
using Api.KapiDunyasi.Domain.Policies;
using Api.KapiDunyasi.Domain.Products;
using Api.KapiDunyasi.Domain.References;
using Api.KapiDunyasi.Domain.Showrooms;
using Api.KapiDunyasi.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<Showroom> Showrooms => Set<Showroom>();
    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<PolicyContent> PolicyContents => Set<PolicyContent>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

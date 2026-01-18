using Api.KapiDunyasi.Domain.Categories;
using Api.KapiDunyasi.Domain.Products;
using Api.KapiDunyasi.Domain.Blog;
using Api.KapiDunyasi.Domain.Contacts;
using Api.KapiDunyasi.Domain.Faqs;
using Api.KapiDunyasi.Domain.Orders;
using Api.KapiDunyasi.Domain.Policies;
using Api.KapiDunyasi.Domain.References;
using Api.KapiDunyasi.Domain.Showrooms;
using Api.KapiDunyasi.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<BlogPost> BlogPosts { get; }
    DbSet<Reference> References { get; }
    DbSet<Showroom> Showrooms { get; }
    DbSet<Faq> Faqs { get; }
    DbSet<PolicyContent> PolicyContents { get; }
    DbSet<ContactMessage> ContactMessages { get; }
    DbSet<Order> Orders { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcBlog.Models;
using System;

public class BlogContext : IdentityDbContext<ApplicationUser>
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ✅ Seed roles
        var adminRole = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
        var contributorRole = new IdentityRole { Id = "2", Name = "Contributor", NormalizedName = "CONTRIBUTOR" };
        builder.Entity<IdentityRole>().HasData(adminRole, contributorRole);

        // ✅ Precomputed password hashes
        var adminPasswordHash = "AQAAAAIAAYagAAAAEBvmtQzDAU7KY/F21wpOphGlvhrqzPEQ9i7rKYzblf9jjU9HH4POTLn3iE/jQsuv9A=="; // "P@$$w0rd"
        var contributorPasswordHash = "AQAAAAIAAYagAAAAELX+G/BQU8H3QdzbRrg4Xfw53uvTEWUEQhOR9/kTvx6AfMPqAPy6PeRsk87wvu+ahQ=="; // "P@$$w0rd"

        // ✅ Seed users
        var admin = new ApplicationUser
        {
            Id = "10",
            UserName = "a@a.a",
            NormalizedUserName = "A@A.A",
            Email = "a@a.a",
            NormalizedEmail = "A@A.A",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            PasswordHash = adminPasswordHash,
            SecurityStamp = "1234567890", // Fixed SecurityStamp
            ConcurrencyStamp = "abcdefghij" // Fixed ConcurrencyStamp
        };

        var contributor = new ApplicationUser
        {
            Id = "20",
            UserName = "c@c.c",
            NormalizedUserName = "C@C.C",
            Email = "c@c.c",
            NormalizedEmail = "C@C.C",
            EmailConfirmed = true,
            FirstName = "Contributor",
            LastName = "User",
            PasswordHash = contributorPasswordHash,
            SecurityStamp = "0987654321", // Fixed SecurityStamp
            ConcurrencyStamp = "jihgfedcba" // Fixed ConcurrencyStamp
        };

        builder.Entity<ApplicationUser>().HasData(admin, contributor);

        // ✅ Assign roles
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1", UserId = "10" }, // Admin
            new IdentityUserRole<string> { RoleId = "2", UserId = "20" }  // Contributor
        );

        // ✅ Seed Article with hardcoded DateTime values
        builder.Entity<Article>().HasData(new Article
        {
            ArticleId = 1,
            Title = "Sample Article",
            Body = "This is a sample seeded article.",
            CreateDate = new DateTime(2024, 2, 16, 12, 0, 0),  // Fixed date
            StartDate = new DateTime(2024, 2, 16, 12, 0, 0),   // Fixed date
            EndDate = new DateTime(2024, 3, 16, 12, 0, 0),     // Fixed date
            ContributorUsername = "c@c.c"
        });
    }
}

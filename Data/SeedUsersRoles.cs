using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

public class SeedUsersRoles
{
    private readonly List<IdentityRole> _roles;
    private readonly List<ApplicationUser> _users;
    private readonly List<IdentityUserRole<string>> _userRoles;

    public SeedUsersRoles()
    {
        _roles = GetRoles();
        _users = GetUsers();
        _userRoles = GetUserRoles(_users, _roles);
    }

    public List<IdentityRole> Roles => _roles;
    public List<ApplicationUser> Users => _users;
    public List<IdentityUserRole<string>> UserRoles => _userRoles;

    private List<IdentityRole> GetRoles()
    {
        var adminRole = new IdentityRole("Admin") { NormalizedName = "ADMIN" };
        var contributorRole = new IdentityRole("Contributor") { NormalizedName = "CONTRIBUTOR" };

        return new List<IdentityRole> { adminRole, contributorRole };
    }

    private List<ApplicationUser> GetUsers()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        string pwd = "P@$$w0rd";

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
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
            PasswordHash = hasher.HashPassword(null, pwd)
        };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
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
            PasswordHash = hasher.HashPassword(null, pwd)
        };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        return new List<ApplicationUser> { admin, contributor };
    }

    private List<IdentityUserRole<string>> GetUserRoles(List<ApplicationUser> users, List<IdentityRole> roles)
    {
        return new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { UserId = users[0].Id, RoleId = roles.First(r => r.Name == "Admin").Id },
            new IdentityUserRole<string> { UserId = users[1].Id, RoleId = roles.First(r => r.Name == "Contributor").Id }
        };
    }
}

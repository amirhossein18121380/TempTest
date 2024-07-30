using Domain.Entities.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Identity.Identity.Store;

public class AppUserStore:UserStore<User,Role,ApplicationDbContext,int,UserClaim,UserRole,UserLogin,UserToken,RoleClaim>
{
    public AppUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}
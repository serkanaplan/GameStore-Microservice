using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityServer.Models;
using IdentityService.Models;
using System.Security.Claims;
using IdentityModel;

namespace IdentityService.Services;

public class CustomProfileService(UserManager<ApplicationUser> _userManager) : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject); 
        var roles = await _userManager.GetRolesAsync(user);

        var existingClaims = await _userManager.GetClaimsAsync(user);
        var claims = new List<Claim>(){
            new ("username",user.UserName),
            new (ClaimTypes.NameIdentifier,user.Id),    
            // new ("role",roles.Last().ToString())
        };
        context.IssuedClaims.AddRange(claims);
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
    } 

    public Task IsActiveAsync(IsActiveContext context) => Task.CompletedTask;
}
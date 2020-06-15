using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LTC_Covid.Data
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<BusinessUserInfo>
    {
        public CustomClaimsPrincipalFactory(
            UserManager<BusinessUserInfo> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(BusinessUserInfo user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("OfficeSequence", user.Office_Sequence.ToString()));
            identity.AddClaim(new Claim("Name", user.LastName + " " + user.FirstName));
            return identity;
        }
    }
}

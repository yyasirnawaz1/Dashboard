﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LTCDataModel.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LTCAdminPortal.Data
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public CustomClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("OfficeSequence", user.Office_Sequence.ToString()));
            identity.AddClaim(new Claim("Name", user.LastName + " " + user.FirstName));
            identity.AddClaim(new Claim("IsDefault", user.IsDefaultUser.ToString()));



            return identity;
        }
    }
}

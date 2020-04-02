using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTCDashboard.Data
{
    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        //code to disable encryption in password
        public string HashPassword(ApplicationUser user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            return hashedPassword.Equals(providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}

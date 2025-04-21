using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string? UserCode { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public int isDeleted { get; set; }
        public string PageNames { get; set; }
        public bool IsSalesUser { get; set; }
        public string? SelectedWorkCenter { get; set; }
        public string Routes { get; set; }

        public AppUser()
        {
            UserCode = GenerateUserCode();
        }

        private string GenerateUserCode()
        {
            return $"USR-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        }

    }
}

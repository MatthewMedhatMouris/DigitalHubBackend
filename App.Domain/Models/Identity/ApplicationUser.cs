using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; }
        public int AccountType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public List<RefreshToken>? RefreshTokens { get; set; }
        public string? AdditionalNumber { get; set; }
    }
}

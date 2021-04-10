using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlobUploadWebApp.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserMedia = new HashSet<UserMedium>();
        }

        public virtual ICollection<UserMedium> UserMedia { get; set; }
    }
}


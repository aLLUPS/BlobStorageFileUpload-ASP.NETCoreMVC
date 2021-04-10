using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobUploadWebApp.Models
{
    public partial class UserMedium
    {
        public int MediaId { get; set; }
        public string UserId { get; set; }
        public string MediaFileName { get; set; }
        public string MediaFileType { get; set; }
        public string MediaUrl { get; set; }
        public DateTime DateTimeUploaded { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

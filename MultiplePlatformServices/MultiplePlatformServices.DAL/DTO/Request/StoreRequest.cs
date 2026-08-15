using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public class StoreRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Logo { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public string VendorId { get; set; } = null!;
    }
}

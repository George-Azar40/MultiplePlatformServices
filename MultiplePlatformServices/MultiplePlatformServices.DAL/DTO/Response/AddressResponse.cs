using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class AddressResponse
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string BuildingNumber { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? AdditionalDetails { get; set; }

        public bool IsDefault { get; set; }
    }
}

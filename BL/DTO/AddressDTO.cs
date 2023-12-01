using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? Appartment { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            string addressString = $"{Street} {HouseNumber}";

            if (!string.IsNullOrEmpty(Appartment))
            {
                addressString += $" Appartment {Appartment}" + Environment.NewLine;
            }
            else
            {
                addressString += Environment.NewLine;
            }

            addressString += $"{PostalCode} {City}" + Environment.NewLine + $"{Country}";

            return addressString;
        }
    }


}

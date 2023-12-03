using DAL.DapperAttributes;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string? MedicalHistory { get; set; }
        public int AddressId;
        [Navigation]
        public Address Address { get; set; }
        [Navigation]
        public ICollection<Prescription> Prescriptions { get; set; }

    }
}

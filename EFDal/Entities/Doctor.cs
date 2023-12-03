using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DapperAttributes;

namespace DAL.Entities
{
    public class Doctor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Specialization { get; set; }

        [Navigation]
        public ICollection<Prescription>? Prescriptions { get; set; }
    }

}

﻿using DAL.DapperAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
    
        public class Address : BaseEntity
        {
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string? Appartment { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }

            [Navigation]
            public List<Patient> Patients { get; } = new(); 

        
    }

    

}


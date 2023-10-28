using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class DoctorManager : GenericManager<Doctor>, IDoctorManager
    {
        internal readonly IDoctorRepository _doctorRepository;
        public DoctorManager(IDoctorRepository doctorRepository) : base(doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public Doctor GetByName(string firstName, string lastName)
        {
           return _doctorRepository.GetByName(firstName, lastName);
        }
    }
}

using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using EFDal.Repositories;
using HealthCareAppWPF.DTO;
using System.Linq.Expressions;

namespace BL.Managers
{
    public class PrescriptionManager : GenericManager<Prescription>, IPrescriptionManager
    {
        internal readonly IPrescriptionRepository _prescriptionRepository;
        public PrescriptionManager(IPrescriptionRepository prescriptionRepository) : base(prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }
    }
}

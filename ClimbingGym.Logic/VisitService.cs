using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Repository;
using ClimbingGym.Data;

namespace ClimbingGym.Logic
{
    public class VisitService
    {
        private VisitRepository _visitRepository;

        public VisitService(VisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public void AddVisit(Visit visit)
        {
            _visitRepository.AddVisit(visit);
        }

        public void RemoveVisit(int visitId)
        {
            _visitRepository.RemoveVisit(visitId);
        }

        public List<Visit> GetAllVisits()
        {
            return _visitRepository.GetAllVisits();
        }

        public Visit GetVisitById(int visitId)
        {
            return _visitRepository.GetVisitById(visitId);
        }

        public List<Visit> GetVisitsByClimberId(int climberId)
        {
            return _visitRepository.GetVisitsByClimberId(climberId);
        }

        public void UpdateVisit(Visit visit)
        {
            _visitRepository.UpdateVisit(visit);
        }

        // További logikai műveletek hozzáadása szükség esetén
    }
}

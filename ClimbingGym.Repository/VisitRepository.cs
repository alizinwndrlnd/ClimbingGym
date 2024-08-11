using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Data;

namespace ClimbingGym.Repository
{
    public class VisitRepository
    {
        private List<Visit> _visits;

        public VisitRepository(List<Visit> initialVisits = null)
        {
            _visits = initialVisits ?? new List<Visit>();
        }

        public void AddVisit(Visit visit)
        {
            _visits.Add(visit);
        }

        public void RemoveVisit(int visitId)
        {
            var visitToRemove = _visits.FirstOrDefault(v => v.VisitId == visitId);
            if (visitToRemove != null)
            {
                _visits.Remove(visitToRemove);
            }
        }

        public List<Visit> GetAllVisits()
        {
            return _visits;
        }

        public Visit GetVisitById(int visitId)
        {
            return _visits.FirstOrDefault(v => v.VisitId == visitId);
        }

        public List<Visit> GetVisitsByClimberId(int climberId)
        {
            return _visits.Where(v => v.ClimberId == climberId).ToList();
        }

        public void UpdateVisit(Visit updatedVisit)
        {
            var existingVisit = _visits.FirstOrDefault(v => v.VisitId == updatedVisit.VisitId);
            if (existingVisit != null)
            {
                existingVisit.VisitDate = updatedVisit.VisitDate;
                existingVisit.RouteDifficulty = updatedVisit.RouteDifficulty;
            }
        }
    }
}

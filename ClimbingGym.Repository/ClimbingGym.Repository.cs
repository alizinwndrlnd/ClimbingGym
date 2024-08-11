using ClimbingGym.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Data;

namespace ClimbingGym.Repository
{
    public class ClimberRepository
    {
        private List<Climber> _climbers;

        public ClimberRepository(List<Climber> initialClimbers = null)
        {
            _climbers = initialClimbers ?? new List<Climber>();
        }

        public void AddClimber(Climber climber)
        {
            _climbers.Add(climber);
        }

        public void RemoveClimber(int climberId)
        {
            var climberToRemove = _climbers.FirstOrDefault(c => c.ClimberId == climberId);
            if (climberToRemove != null)
            {
                _climbers.Remove(climberToRemove);
            }
        }

        public List<Climber> GetAllClimbers()
        {
            return _climbers;
        }

        public Climber GetClimberById(int climberId)
        {
            return _climbers.FirstOrDefault(c => c.ClimberId == climberId);
        }

        public void UpdateClimber(Climber updatedClimber)
        {
            var existingClimber = _climbers.FirstOrDefault(c => c.ClimberId == updatedClimber.ClimberId);
            if (existingClimber != null)
            {
                existingClimber.Name = updatedClimber.Name;
                existingClimber.MembershipType = updatedClimber.MembershipType;
            }
        }
    }
}

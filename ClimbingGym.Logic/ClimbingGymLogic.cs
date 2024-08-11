using ClimbingGym.Data;
using ClimbingGym.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGym.Logic
{
    public class ClimbingGymLogic
    {
        private ClimberRepository climberRepository;
        private VisitRepository visitRepository;

        public ClimbingGymLogic(ClimberRepository climberRepository, VisitRepository visitRepository)
        {
            this.climberRepository = climberRepository;
            this.visitRepository = visitRepository;
        }


        public void AddClimber(string name, string membershipType)
        {
            int newId = new Random().Next(1000, 9999);
            Climber newClimber = new Climber(newId, name, membershipType);
            climberRepository.AddClimber(newClimber);
        }

    }
}

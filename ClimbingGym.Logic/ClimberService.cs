using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Repository;
using ClimbingGym.Data;


namespace ClimbingGym.Logic
{
    public class ClimberService
    {
        private ClimberRepository _climberRepository;

        public ClimberService(ClimberRepository climberRepository)
        {
            _climberRepository = climberRepository;
        }

        public void AddClimber(Climber climber)
        {
            _climberRepository.AddClimber(climber);
        }

        public void RemoveClimber(int climberId)
        {
            _climberRepository.RemoveClimber(climberId);
        }

        public List<Climber> GetAllClimbers()
        {
            return _climberRepository.GetAllClimbers();
        }

        public Climber GetClimberById(int climberId)
        {
            return _climberRepository.GetClimberById(climberId);
        }

        public void UpdateClimber(Climber climber)
        {
            _climberRepository.UpdateClimber(climber);
        }

        // További logikai műveletek hozzáadása szükség esetén
    }
}

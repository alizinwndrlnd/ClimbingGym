using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGym.Data
{
    public class Visit
    {
        public int VisitId { get; set; }
        public int ClimberId { get; set; }
        public DateTime VisitDate { get; set; }
        public string RouteDifficulty { get; set; }

        public Visit(int visitId, int climberId, DateTime visitDate, string routeDifficulty)
        {
            VisitId = visitId;
            ClimberId = climberId;
            VisitDate = visitDate;
            RouteDifficulty = routeDifficulty;
        }

        public override string ToString()
        {
            return $"Látogatás ID: {VisitId}, Mászó ID: {ClimberId}, Dátum: {VisitDate.ToShortDateString()}, Nehézség: {RouteDifficulty}";
        }
    }
}

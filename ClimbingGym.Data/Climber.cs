using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClimbingGym.Data
{
    public class Climber
    {
        public int ClimberId { get; set; }
        public string Name { get; set; }
        public string MembershipType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int NewId { get; }

        public Climber(int climberId, string name, string membershipType, string email, string phone)
    {
        ClimberId = climberId;
        Name = name;
        MembershipType = membershipType;
        Email = email;
        Phone = phone;

    }

        public Climber(int newId, string name, string membershipType)
        {
            NewId = newId;
            Name = name;
            MembershipType = membershipType;
        }

        public override string ToString()
    {
        return $"ID: {ClimberId}, Név: {Name}, Tagság: {MembershipType}";
    }

    }
}

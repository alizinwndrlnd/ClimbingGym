using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Logic;
using ClimbingGym.Repository;
using ClimbingGym.Data;
using System.IO;
using System.Text.Json;
using System.Windows;


namespace ClimbingGym.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Üdvözlő üzenet
            Console.WriteLine("Üdvözöljük a 'Flash it!' Falmászó Termünk Nyilvántartó Rendszerében!");

            // Inicializálás
            ClimberRepository climberRepository;
            VisitRepository visitRepository;

            // Fájl betöltése vagy új létrehozása
            if (args.Length > 0 && File.Exists(args[0]))
            {
                string filePath = args[0];
                // Adatok betöltése a fájlból (ehhez fájlkezelés kell)
                climberRepository = LoadClimberData(filePath);
                visitRepository = LoadVisitData(filePath);
            }
            else
            {
                // Új, üres repository-k létrehozása
                climberRepository = new ClimberRepository();
                visitRepository = new VisitRepository();
                Console.WriteLine("Új adatbázis létrehozva.");
            }

            ClimberService climberService = new ClimberService(climberRepository);
            VisitService visitService = new VisitService(visitRepository);

            // Főmenü megjelenítése
            while (true)
            {
                Console.WriteLine("\nFőmenü:");
                Console.WriteLine("1. Új mászó hozzáadása");
                Console.WriteLine("2. Mászók listázása");
                Console.WriteLine("3. Mászó törlése");
                Console.WriteLine("");
                Console.WriteLine("4. Új látogatás hozzáadása");
                Console.WriteLine("5. Látogatások listázása");
                Console.WriteLine("");
                Console.WriteLine("6. Kilépés és adatok mentése");

                Console.Write("Válasszon egy opciót: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddNewClimber(climberService);
                        break;
                    case "2":
                        ListAllClimbers(climberService);
                        break;
                    case "3":
                        RemoveClimber(climberService);
                        break;
                    case "4":
                        AddNewVisit(visitService);
                        break;
                    case "5":
                        ListAllVisits(visitService);
                        break;
                    case "6":
                        SaveData(climberRepository, visitRepository);
                        Console.WriteLine("Adatok mentve. Kilépés...");
                        return;
                    default:
                        Console.WriteLine("Érvénytelen opció, próbálja újra.");
                        break;
                }
            }
        }

        //  metódusok: AddNewClimber, ListAllClimbers, RemoveClimber, AddNewVisit, ListAllVisits, SaveData stb.
        static void AddNewClimber(ClimberService climberService)
        {
            Console.Write("Adja meg a mászó nevét (Vezetéknév, Keresztnév): ");
            string name = Console.ReadLine();

            Console.Write("Adja meg a tagság típusát (jegy, Havi bérlet, Éves bérlet): ");
            string membershipType = Console.ReadLine();

            Console.Write("Adja meg a mászó Email címét: ");
            string email = Console.ReadLine();

            Console.Write("Adja meg a mászó telefonszámát: ");
            string phone = Console.ReadLine();

            int newId = new Random().Next(1000, 9999);
            Climber climber = new Climber(newId, name, membershipType, phone, email);

            climberService.AddClimber(climber);
            Console.WriteLine("Mászó hozzáadva.");
        }

        static void ListAllClimbers(ClimberService climberService)
        {
            List<Climber> climbers = climberService.GetAllClimbers();

            if (climbers.Count == 0)
            {
                Console.WriteLine("Nincsenek mászók a rendszerben.");
            }
            else
            {
                Console.WriteLine("Mászók listája:");
                foreach (var climber in climbers)
                {
                    Console.WriteLine($"ID: {climber.ClimberId}, Név: {climber.Name}, Tagság: {climber.MembershipType}, E-mail cím: {climber.Email}, Telefonszám: {climber.Phone}");
                }
            }
        }

        static void RemoveClimber(ClimberService climberService)
        {
            Console.Write("Adja meg a törlendő mászó ID-ját: ");
            int climberId;
            if (int.TryParse(Console.ReadLine(), out climberId))
            {
                climberService.RemoveClimber(climberId);
                Console.WriteLine("Mászó törölve.");
            }
            else
            {
                Console.WriteLine("Érvénytelen ID.");
            }
        }

        static void AddNewVisit(VisitService visitService)
        {
            Console.Write("Adja meg a mászó ID-ját: ");
            int climberId = int.Parse(Console.ReadLine());

            Console.Write("Adja meg a látogatás dátumát (YYYY-MM-DD): ");
            DateTime visitDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Adja meg a megmászott út nehézségét: ");
            string difficulty = Console.ReadLine();

            int newId = new Random().Next(1000, 9999);
            Visit visit = new Visit(newId, climberId, visitDate, difficulty);

            visitService.AddVisit(visit);
            Console.WriteLine("Látogatás hozzáadva.");
        }

        static void ListAllVisits(VisitService visitService)
        {
            List<Visit> visits = visitService.GetAllVisits();

            if (visits.Count == 0)
            {
                Console.WriteLine("Nincsenek látogatások a rendszerben.");
            }
            else
            {
                Console.WriteLine("Látogatások listája:");
                foreach (var visit in visits)
                {
                    Console.WriteLine($"ID: {visit.VisitId}, Mászó ID: {visit.ClimberId}, Dátum: {visit.VisitDate}, Nehézség: {visit.RouteDifficulty}");
                }
            }
        }

        //-----------------------------
        //fájlba írás

        static void SaveData(ClimberRepository climberRepository, VisitRepository visitRepository)
        {
            // ide még kell egy mentési metódus, ami a climberRepository és visitRepository tartalmát fájlba menti
            string mentettFile = "climbingGymData.txt";
            using (StreamWriter writer = new StreamWriter(mentettFile))
            {
                // Climber adatok mentése
                writer.WriteLine("Climbers:");
                foreach (var climber in climberRepository.GetAllClimbers())
                {
                    writer.WriteLine($"ID: {climber.ClimberId}, Name: {climber.Name}, Email: {climber.Email}, Phone number: {climber.Phone}, Membership: {climber.MembershipType}");
                }

                // Visit adatokat mentése
                writer.WriteLine("\nVisits:");
                foreach (var visit in visitRepository.GetAllVisits())
                {
                    writer.WriteLine($"ClimberID: {visit.ClimberId}, Date: {visit.VisitDate}, RouteLevel: {visit.RouteDifficulty}");
                }
            }
            Console.WriteLine("Adatok sikeresen mentve TXT fájlba.");

        }


        // adatok betöltése fájlból
        private static ClimberRepository LoadClimberData(string mentettFile)
        {
            ClimberRepository climberRepository = new ClimberRepository();

            if (!File.Exists(mentettFile))
            {
                Console.WriteLine("A megadott fájl nem található.");
                return climberRepository;
            }

            using (StreamReader reader = new StreamReader(mentettFile))
            {
                string line;
                bool readingClimbers = false;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Climbers:"))
                    {
                        readingClimbers = true;
                        continue;
                    }

                    if (readingClimbers && line.StartsWith("ID:"))
                    {
                        string[] parts = line.Split(',');

                        int id = int.Parse(parts[0].Split(':')[1].Trim());
                        string name = parts[1].Split(':')[1].Trim();
                        string email = parts[2].Split(':')[1].Trim();
                        string phone = parts[3].Split(':')[1].Trim();
                        string membership = parts[4].Split(':')[1].Trim();

                        Climber climber = new Climber(id, name, email, phone, membership);
                        climberRepository.AddClimber(climber);
                    }
                }
            }

            return climberRepository;
        }




        // látogatási adatok betöltése fájlból

        static VisitRepository LoadVisitData(string mentettFile)
        {
            // Adatok betöltése fájlból
            //  az adatok betöltése a fájlból, és visszaadni a VisitRepository példányát

            VisitRepository repository = new VisitRepository();

            // fájl ellenőrzés
            if (!File.Exists(mentettFile))
            {
                Console.WriteLine("A fájl nem található.");
                return repository;
            }

            using (StreamReader reader = new StreamReader(mentettFile))
            {
                string line;
                bool readingVisits = false;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Visits:"))
                    {
                        readingVisits = true;
                        continue;
                    }

                    if (readingVisits && line.StartsWith("ClimberID:"))
                    {
                        // A látogatási adatok feldarabolása és feldolgozása
                        string[] parts = line.Split(',');

                        int climberId = int.Parse(parts[0].Split(':')[1].Trim());
                        DateTime visitDate = DateTime.Parse(parts[1].Split(':')[1].Trim());
                        string routeDifficulty = parts[2].Split(':')[1].Trim();

                        // Új Visit példány hozzáadása a repository-hoz
                        Visit visit = new Visit(climberId, visitDate, routeDifficulty);
                        repository.AddVisit(visit);
                    }
                }
            }

            
            return repository;
        }
    }
}



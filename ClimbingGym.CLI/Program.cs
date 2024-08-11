﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbingGym.Logic;
using ClimbingGym.Repository;
using ClimbingGym.Data;
using System.IO;

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

        // Implementáld a metódusokat: AddNewClimber, ListAllClimbers, RemoveClimber, AddNewVisit, ListAllVisits, SaveData stb.
        static void AddNewClimber(ClimberService climberService)
        {
            Console.Write("Adja meg a mászó nevét (Vezetéknév, Keresztnév): ");
            string name = Console.ReadLine();

            Console.Write("Adja meg a tagság típusát (jegy, Havi bérlet, Éves bérlet): ");
            string membershipType = Console.ReadLine();

            int newId = new Random().Next(1000, 9999); // Ez egy egyszerű példa, inkább valós id-k generálása szükséges
            Climber climber = new Climber(newId, name, membershipType);

            Console.Write("Adja meg a mászó Email címét: ");
            string email = Console.ReadLine();

            Console.Write("Adja meg a mászó telefonszámát: ");
            string phone = Console.ReadLine();

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

        static void SaveData(ClimberRepository climberRepository, VisitRepository visitRepository)
        {
            // Adatok mentése fájlba
            // Implementálj egy mentési metódust, amely a climberRepository és visitRepository tartalmát fájlba menti
        }

        static ClimberRepository LoadClimberData(string filePath)
        {
            // Adatok betöltése fájlból
            // Implementáld az adatok betöltését a fájlból, és add vissza a ClimberRepository példányát
            return new ClimberRepository();
        }

        static VisitRepository LoadVisitData(string filePath)
        {
            // Adatok betöltése fájlból
            // Implementáld az adatok betöltését a fájlból, és add vissza a VisitRepository példányát
            return new VisitRepository();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClimbingGym.Logic;
using ClimbingGym.Repository;
using ClimbingGym.Data;
using System.Text.Json;
using System.IO;


namespace ClimbingGym.GUI
{
   
    public partial class MainWindow : Window
    {
        private ClimberRepository climberRepository;
        private VisitRepository visitRepository;
        public ClimbingGymLogic logic;

        public MainWindow()
        {
            InitializeComponent();
            climberRepository = new ClimberRepository();
            visitRepository = new VisitRepository();
            logic = new ClimbingGymLogic(climberRepository, visitRepository);
        }

        public void AddClimberButton_Click(object sender, RoutedEventArgs e)
        {
            // Új mászó hozzáadása
            string name = ClimberNameTextBox.Text;
            string membershipType = (MembershipTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(membershipType))
            {
                logic.AddClimber(name, membershipType);
                UpdateClimberList();
            }
        }

        private void UpdateClimberList()
        {
            // Lista frissítése
            ClimbersListBox.Items.Clear();
            foreach (var climber in climberRepository.GetAllClimbers())
            {
                ClimbersListBox.Items.Add(climber.Name);
            }
        }

        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Adatok mentése
            SaveData(climberRepository, visitRepository);
        }

        private void SaveData(ClimberRepository climberRepository, VisitRepository visitRepository)
        {
            // Adatok mentése fájlba
            string filePath = "climbingGymData.json";
            var dataToSave = new
            {
                Climbers = climberRepository.GetAllClimbers(),
                Visits = visitRepository.GetAllVisits()
            };
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            string jsonData = System.Text.Json.JsonSerializer.Serialize(dataToSave, options);
            System.IO.File.WriteAllText(filePath, jsonData);
            MessageBox.Show("Adatok sikeresen mentve.");
        }

    
    }
}

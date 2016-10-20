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
using System.Windows.Shapes;
using DatabaseTool.Entities;
using DatabaseTool.Connector;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow {
    /// <summary>
    /// Interaction logic for EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window {
        private Employee _employee;
        private DatabaseConnection _connection;
        private List<Headquarters> _headquartersList;
        public EmployeeEditor(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            _employee = employee;
            EmployeeBsn.Content = $"Employee with BSN: { _employee.Bsn.ToString("000000000")}";
            _connection = connection;//new DatabaseConnection("localhost", "assignment1", "root", "");
            _headquartersList = EntityContentSelector.SelectHeadquarters(this._connection);
            foreach (Headquarters hq in _headquartersList) {
                HeadquarterList.Items.Add($"{hq.PostalCode},\t{hq.BuildingName}");
            }
            ResetFields(this, new RoutedEventArgs());
        }

        private void ResetFields(object sender, RoutedEventArgs e) {
            NameBox.Text = _employee.Name;
            SurnameBox.Text = _employee.Surname;
            HeadquarterList.SelectedIndex = _headquartersList.IndexOf(_headquartersList.Select(h => h).Where(h => h.BuildingName == _employee.MainBuildingName).ToList()[0]);
        }

        private void ProcessChanges(object sender, RoutedEventArgs e) {
            if (NameBox.Text == _employee.Name && SurnameBox.Text == _employee.Surname && _headquartersList[HeadquarterList.SelectedIndex].BuildingName == _employee.MainBuildingName) {
                MessageBox.Show("No changes were detected", "No Changes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool? isChecked = Validator.IsChecked;
            if (isChecked != null && !isChecked.Value) {
                MessageBox.Show("Please make sure to check if the information is correct", "Changes Not Confirmed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string eN = _employee.Name, eS = _employee.Surname, eB = _employee.MainBuildingName;
            if (NameBox.Text != _employee.Name) {
                eN = NameBox.Text;
            }
            if (SurnameBox.Text != _employee.Surname) {
                eS = SurnameBox.Text;
            }
            if (_headquartersList[HeadquarterList.SelectedIndex].BuildingName != _employee.MainBuildingName) {
                eB = _headquartersList[HeadquarterList.SelectedIndex].BuildingName;
            }
            UpdateTable.UpdateEmployee(this._connection, _employee, eN, eS, eB);
        }
    }
}

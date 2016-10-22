using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow.EmployeeWindows {
    /// <summary>
    /// Interaction logic for EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window {
        private Employee _employee;
        private DatabaseConnection _connection;
        private List<Headquarters> _headquartersList;
        public EmployeeEditor(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            this._employee = employee;
            this.Header.Content = $"Employee: {this._employee.Name} {this._employee.Surname}, BSN: {this._employee.Bsn}";
            this._connection = connection;//new DatabaseConnection("localhost", "assignment1", "root", "");
            this._headquartersList = EntityContentSelector.SelectHeadquarters(this._connection);
            foreach (Headquarters hq in this._headquartersList) {
                this.HeadquarterList.Items.Add($"{hq.PostalCode},\t{hq.BuildingName}");
            }
            this.ResetFields(this, new RoutedEventArgs());
        }

        private void ResetFields(object sender, RoutedEventArgs e) {
            this.NameBox.Text = this._employee.Name;
            this.SurnameBox.Text = this._employee.Surname;
            this.HeadquarterList.SelectedIndex = this._headquartersList.IndexOf(this._headquartersList.Select(h => h).Where(h => h.BuildingName == this._employee.MainBuildingName).ToList()[0]);
        }

        private void ProcessChanges(object sender, RoutedEventArgs e) {
            if (this.NameBox.Text == this._employee.Name && this.SurnameBox.Text == this._employee.Surname && this._headquartersList[this.HeadquarterList.SelectedIndex].BuildingName == this._employee.MainBuildingName) {
                MessageBox.Show("No changes were detected", "No Changes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool? isChecked = this.Validator.IsChecked;
            if (isChecked != null && !isChecked.Value) {
                MessageBox.Show("Please make sure to check if the information is correct", "Changes Not Confirmed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string eN = this._employee.Name, eS = this._employee.Surname, eB = this._employee.MainBuildingName;
            if (this.NameBox.Text != this._employee.Name) {
                eN = this.NameBox.Text;
            }
            if (this.SurnameBox.Text != this._employee.Surname) {
                eS = this.SurnameBox.Text;
            }
            if (this._headquartersList[this.HeadquarterList.SelectedIndex].BuildingName != this._employee.MainBuildingName) {
                eB = this._headquartersList[this.HeadquarterList.SelectedIndex].BuildingName;
            }
            UpdateTable.UpdateEmployee(this._connection, this._employee, eN, eS, eB);
        }
    }
}

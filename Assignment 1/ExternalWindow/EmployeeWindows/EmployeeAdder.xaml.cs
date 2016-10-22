using System.Collections.Generic;
using System.Windows;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow.EmployeeWindows {
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeAdder : Window {
        private List<Headquarters> _headquartersList;
        private DatabaseConnection _connection;
        private string _windowText;

        public EmployeeAdder(DatabaseConnection connection) {
            InitializeComponent();
            this._windowText = "Add a new Employee";
            this._connection = connection;
            this._headquartersList = EntityContentSelector.SelectHeadquarters(this._connection);
            foreach (Headquarters hq in this._headquartersList) {
                this.MainHeadquarters.Items.Add($"{hq.PostalCode},\t{hq.BuildingName}");
            }
        }

        private void InsertIntoEmployeeTable_Click(object sender, RoutedEventArgs e) {
            int employeeBsn;
            if (!int.TryParse(this.EmployeeBsn.Text, out employeeBsn) || this.EmployeeBsn.Text.Length != 9) {
                MessageBox.Show("Please enter a valid BSN\n\nTIP: A valid BSN is 9 numbers long", "Invalid BSN",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (this.EmployeeName.Text.Length < 2 || this.EmployeeSurname.Text.Length <= 1) {
                MessageBox.Show("Please make sure the new employee his Name and Surname are filled in!", "Empty Fields",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (this.MainHeadquarters.SelectedIndex < 0 || this.MainHeadquarters.SelectedIndex > this._headquartersList.Count) {
                MessageBox.Show("Please make sure to select a main headquarters for the new employee", "Select main HQ",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployee(this._connection, employeeBsn, this.EmployeeName.Text, this.EmployeeSurname.Text,
                this._headquartersList[this.MainHeadquarters.SelectedIndex].BuildingName);
        }
    }
}
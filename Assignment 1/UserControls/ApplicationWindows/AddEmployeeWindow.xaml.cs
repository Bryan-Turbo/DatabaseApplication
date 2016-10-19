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
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.UserControls.ApplicationWindows {
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : UserControl {
        private List<Headquarters> _headquartersList;
        private DatabaseConnection _connection;
        private string _windowText;

        public AddEmployeeWindow() {
            InitializeComponent();
            _windowText = "Add a new Employee";
            _connection = new DatabaseConnection("localhost", "assignment1", "root", "");
            _headquartersList = EntityContentSelector.SelectHeadquarters(this._connection);
            foreach (Headquarters hq in _headquartersList) {
                MainHeadquarters.Items.Add($"{hq.PostalCode},\t{hq.BuildingName}");
            }
        }

        private void InsertIntoEmployeeTable_Click(object sender, RoutedEventArgs e) {
            int employeeBsn;
            if (!int.TryParse(EmployeeBsn.Text, out employeeBsn) || EmployeeBsn.Text.Length != 9) {
                MessageBox.Show("Please enter a valid BSN\n\nTIP: A valid BSN is 9 numbers long", "Invalid BSN",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (EmployeeName.Text.Length < 2 || EmployeeSurname.Text.Length <= 1) {
                MessageBox.Show("Please make sure the new employee his Name and Surname are filled in!", "Empty Fields",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MainHeadquarters.SelectedIndex < 0 || MainHeadquarters.SelectedIndex > _headquartersList.Count) {
                MessageBox.Show("Please make sure to select a main headquarters for the new employee", "Select main HQ",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployee(this._connection, employeeBsn, EmployeeName.Text, EmployeeSurname.Text,
                _headquartersList[MainHeadquarters.SelectedIndex].BuildingName);
        }
    }
}
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
        public EmployeeEditor(Employee employee) {
            InitializeComponent();
            _employee = employee;
            EmployeeBsn.Content = $"Employee with BSN: { _employee.Bsn.ToString("000000000")}";
            _connection = new DatabaseConnection("localhost", "assignment1", "root", "");
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
    }
}

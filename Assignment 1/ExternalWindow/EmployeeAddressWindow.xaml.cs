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
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow {
    /// <summary>
    /// Interaction logic for EmployeeAddressWindow.xaml
    /// </summary>
    public partial class EmployeeAddressWindow : Window {
        private Employee _employee;
        private readonly DatabaseConnection _connection;

        private List<EmployeeAddress> _employeeAddressList;
        private List<Address> _adressList;
        public EmployeeAddressWindow(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            this._employee = employee;
            this._connection = connection;
            GetData();

            AddressListViewer.PopulateList(GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._adressList));

            this.Header.Content = $"Employee {this._employee.Name} {this._employee.Surname} - Addresses";

            foreach (Address a in _adressList) {
                AddressBox.Items.Add($"{a.PostalCode}, {a.Street} {a.HouseNumber}, {a.Country}");
            }
        }

        private void GetData() {
            _employeeAddressList = EntityContentSelector.SelectEmployeeAddress(this._connection);
            _adressList = EntityContentSelector.SelectAddress(this._connection);
        }

        private List<Address> GetAddressesAssociatedWithEmployee(List<EmployeeAddress> eaList, List<Address> aList) {
            var fresult = eaList.Where(a => a.Bsn == this._employee.Bsn).Select(a => new {a.PostalCode, a.Country });
            List<Address> result = aList.Where(a => fresult.Contains(new {a.PostalCode, a.Country})).ToList();
            return result;
        }

        private void AddEmployeeAddress_Click(object sender, RoutedEventArgs e) {
            if (AddressBox.SelectedIndex < 0) {
                MessageBox.Show("Please select an address","NO ADDRESS SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeeAddress(this._connection, _employee.Bsn,
                _adressList.ElementAt(AddressBox.SelectedIndex).PostalCode,
                _adressList.ElementAt(AddressBox.SelectedIndex).Country, ResidenceBox.IsChecked == true);

            GetData();
            var list = GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._adressList);
            AddressListViewer.PopulateList(list);
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e) {
            if (AddressListViewer.SelectedIndex < 0) {
                MessageBox.Show("Please select an address", "No Address Selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

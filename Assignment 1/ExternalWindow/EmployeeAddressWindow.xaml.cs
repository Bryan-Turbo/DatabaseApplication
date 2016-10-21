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
        private List<Address> _addressList;
        public EmployeeAddressWindow(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            this._employee = employee;
            this._connection = connection;
            GetData();

            AddressListViewer.PopulateList(GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._addressList));

            this.Header.Content = $"Employee {this._employee.Name} {this._employee.Surname} - Addresses";

            foreach (Address a in _addressList) {
                AddressBox.Items.Add($"{a.PostalCode}, {a.Street} {a.HouseNumber}, {a.Country}");
            }
            ShowResidence();
        }

        private void GetData() {
            _employeeAddressList = EntityContentSelector.SelectEmployeeAddress(this._connection);
            _addressList = EntityContentSelector.SelectAddress(this._connection);
        }

        private List<Address> GetAddressesAssociatedWithEmployee(List<EmployeeAddress> eaList, List<Address> aList) {
            var fresult = 
                eaList
                .Where(a => a.Bsn == this._employee.Bsn)
                .Select(a => new {a.PostalCode, a.Country });

            List<Address> result = 
                aList
                .Where(a => fresult.Contains(new {a.PostalCode, a.Country}))
                .ToList();
            return result;
        }

        private void AddEmployeeAddress_Click(object sender, RoutedEventArgs e) {
            if (AddressBox.SelectedIndex < 0) {
                MessageBox.Show("Please select an address","NO ADDRESS SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            List<EmployeeAddress> checklist = EntityContentSelector.SelectEmployeeAddress(this._connection);
            bool containsResidence = checklist.Where(ea => ea.Bsn == this._employee.Bsn).Select(r => r.IsResidence).Contains(true);
            if (containsResidence && ResidenceBox.IsChecked == true) {
                MessageBox.Show("This employee already has a residence", "Residence Already Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            InsertIntoTable.InsertEmployeeAddress(this._connection, _employee.Bsn,
                _addressList.ElementAt(AddressBox.SelectedIndex).PostalCode,
                _addressList.ElementAt(AddressBox.SelectedIndex).Country, ResidenceBox.IsChecked == true);

            GetData();
            var list = GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._addressList);
            AddressListViewer.PopulateList(list);
            ShowResidence();
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e) {
            if (AddressListViewer.SelectedItem == null) {
                MessageBox.Show("Please select an address", "No Address Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeAddress(this._connection, this._employee.Bsn, AddressListViewer.SelectedItem.PostalCode, AddressListViewer.SelectedItem.Country);
            AddressListViewer.RemoveItem(AddressListViewer.SelectedItem);
            ShowResidence();
        }

        private void SetResidence_Click(object sender, RoutedEventArgs e) {
            if (AddressListViewer.SelectedItem == null) {
                MessageBox.Show("Please select an address", "No Address Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var list = AddressListViewer.Items;
            foreach (Address a in list) {
                UpdateTable.UpdateEmployeeAddress(this._connection,
                new EmployeeAddress {
                    Bsn = _employee.Bsn,
                    PostalCode = a.PostalCode,
                    Country = a.Country
                },
                new EmployeeAddress {
                    Bsn = _employee.Bsn,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    IsResidence = false
                });
            }

            UpdateTable.UpdateEmployeeAddress(this._connection,
                new EmployeeAddress {
                    Bsn = _employee.Bsn,
                    PostalCode = AddressListViewer.SelectedItem.PostalCode,
                    Country = AddressListViewer.SelectedItem.Country
                }, 
                new EmployeeAddress {
                    Bsn = _employee.Bsn,
                    PostalCode = AddressListViewer.SelectedItem.PostalCode,
                    Country = AddressListViewer.SelectedItem.Country,
                    IsResidence = true
                });
            ShowResidence();
        }

        private void ShowResidence() {
            GetData();
            var residenceList =
                this._employeeAddressList.Where(b => b.Bsn == this._employee.Bsn && b.IsResidence).Select(a => new {a.PostalCode, a.Country}).ToList();

            if (residenceList.Count < 1) {
                this.CurrentResidence.Content = $"{_employee.Name} {_employee.Surname} does not have a residence";
                return;
            }

            var residence =
                _addressList.Where(a => a.PostalCode == residenceList[0].PostalCode && a.Country == residenceList[0].Country)
                    .Select(p => new {p.PostalCode, p.Street, p.HouseNumber})
                    .ToList()[0];

            if (residence == null)
                return;

            this.CurrentResidence.Content = 
                $"Current residence of {_employee.Name} {_employee.Surname}: {residence.PostalCode}, {residence.Street} {residence.HouseNumber}";
                
        }
    }
}

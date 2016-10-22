using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow.EmployeeWindows {
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
            this.GetData();

            this.AddressListViewer.PopulateList(this.GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._addressList));

            this.Header.Content = $"Employee: {this._employee.Name} {this._employee.Surname}";

            foreach (Address a in this._addressList) {
                this.AddressBox.Items.Add($"{a.PostalCode}, {a.Street} {a.HouseNumber}, {a.Country}");
            }
            this.ShowResidence();
        }

        private void GetData() {
            this._employeeAddressList = EntityContentSelector.SelectEmployeeAddress(this._connection);
            this._addressList = EntityContentSelector.SelectAddress(this._connection);
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
            if (this.AddressBox.SelectedIndex < 0) {
                MessageBox.Show("Please select an address","NO ADDRESS SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            List<EmployeeAddress> checklist = EntityContentSelector.SelectEmployeeAddress(this._connection);
            bool containsResidence = checklist.Where(ea => ea.Bsn == this._employee.Bsn).Select(r => r.IsResidence).Contains(true);
            if (containsResidence && this.ResidenceBox.IsChecked == true) {
                MessageBox.Show("This employee already has a residence", "Residence Already Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            InsertIntoTable.InsertEmployeeAddress(this._connection, this._employee.Bsn,
                this._addressList.ElementAt(this.AddressBox.SelectedIndex).PostalCode,
                this._addressList.ElementAt(this.AddressBox.SelectedIndex).Country, this.ResidenceBox.IsChecked == true);

            this.GetData();
            var list = this.GetAddressesAssociatedWithEmployee(this._employeeAddressList, this._addressList);
            this.AddressListViewer.PopulateList(list);
            this.ShowResidence();
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e) {
            if (this.AddressListViewer.SelectedItem == null) {
                MessageBox.Show("Please select an address", "No Address Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeAddress(this._connection, this._employee.Bsn, this.AddressListViewer.SelectedItem.PostalCode, this.AddressListViewer.SelectedItem.Country);
            this.AddressListViewer.RemoveItem(this.AddressListViewer.SelectedItem);
            this.ShowResidence();
        }

        private void SetResidence_Click(object sender, RoutedEventArgs e) {
            if (this.AddressListViewer.SelectedItem == null) {
                MessageBox.Show("Please select an address", "No Address Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var list = this.AddressListViewer.Items;
            foreach (Address a in list) {
                UpdateTable.UpdateEmployeeAddress(this._connection,
                new EmployeeAddress {
                    Bsn = this._employee.Bsn,
                    PostalCode = a.PostalCode,
                    Country = a.Country
                },
                new EmployeeAddress {
                    Bsn = this._employee.Bsn,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    IsResidence = false
                });
            }

            UpdateTable.UpdateEmployeeAddress(this._connection,
                new EmployeeAddress {
                    Bsn = this._employee.Bsn,
                    PostalCode = this.AddressListViewer.SelectedItem.PostalCode,
                    Country = this.AddressListViewer.SelectedItem.Country
                }, 
                new EmployeeAddress {
                    Bsn = this._employee.Bsn,
                    PostalCode = this.AddressListViewer.SelectedItem.PostalCode,
                    Country = this.AddressListViewer.SelectedItem.Country,
                    IsResidence = true
                });
            this.ShowResidence();
        }

        private void ShowResidence() {
            this.GetData();
            var residenceList =
                this._employeeAddressList.Where(b => b.Bsn == this._employee.Bsn && b.IsResidence).Select(a => new {a.PostalCode, a.Country}).ToList();

            if (residenceList.Count < 1) {
                this.CurrentResidence.Content = $"{this._employee.Name} {this._employee.Surname} does not have a residence";
                return;
            }

            var residence =
                this._addressList.Where(a => a.PostalCode == residenceList[0].PostalCode && a.Country == residenceList[0].Country)
                    .Select(p => new {p.PostalCode, p.Street, p.HouseNumber})
                    .ToList()[0];

            if (residence == null)
                return;

            this.CurrentResidence.Content = 
                $"Current residence of {this._employee.Name} {this._employee.Surname}: {residence.PostalCode}, {residence.Street} {residence.HouseNumber}";
                
        }
    }
}
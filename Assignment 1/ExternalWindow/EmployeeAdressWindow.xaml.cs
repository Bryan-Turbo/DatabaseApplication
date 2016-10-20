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
    /// Interaction logic for EmployeeAdressWindow.xaml
    /// </summary>
    public partial class EmployeeAdressWindow : Window {
        private Employee _employee;
        private readonly DatabaseConnection _connection;

        private List<EmployeeAdress> _employeeAdressList;
        private List<Adress> _adressList;
        public EmployeeAdressWindow(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            this._employee = employee;
            this._connection = connection;
            _employeeAdressList = EntityContentSelector.SelectEmployeeAdress(this._connection);
            _adressList = EntityContentSelector.SelectAdress(this._connection);

            AdresListViewer.AddAdresses(GetAdressesAssociatedWithEmployee(this._employeeAdressList, this._adressList));

            this.Header.Content = $"Employee {this._employee.Name} {this._employee.Surname} - Adresses";
        }

        private List<Adress> GetAdressesAssociatedWithEmployee(List<EmployeeAdress> eaList, List<Adress> aList) {
            var fresult = eaList.Select(a => new {a.PostalCode, a.Country });
            List<Adress> result = aList.Where(a => fresult.Contains(new {a.PostalCode, a.Country})).ToList();

            return result;
        }
    }
}

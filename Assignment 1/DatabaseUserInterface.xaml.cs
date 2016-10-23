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

namespace Assignment_1 {
    /// <summary>
    /// Interaction logic for DatabaseUserInterface.xaml
    /// </summary>
    public partial class DatabaseUserInterface : Window {
        private List<Address> adressList;

        public DatabaseUserInterface() {
            InitializeComponent();
            adressList = EntityContentSelector.SelectAddress();
            foreach (Address address in adressList) {
                comboBox.Items.Add($"{address.PostalCode}, {address.Street}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Address Selecteditem = adressList[comboBox.SelectedIndex];
            // .InsertIntoTable(BuildingName.Text, Rent.Text, Rooms.Text, Selecteditem.PostalCode, Selecteditem.Country);
        }
    }
}
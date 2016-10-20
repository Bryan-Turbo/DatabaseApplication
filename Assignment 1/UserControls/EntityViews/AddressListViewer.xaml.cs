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
using DatabaseTool.Entities;

namespace Assignment_1.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for AddressListViewer.xaml
    /// </summary>
    public partial class AddressListViewer : UserControl {
        public AddressListViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Address> adressList) {
            this.AddressViewer.Items.Clear();
            foreach (Address address in adressList) {
                this.AddressViewer.Items.Add(address);
            }
        }

        public int SelectedIndex {
            get { return this.AddressViewer.SelectedIndex; }
        }
    }
}

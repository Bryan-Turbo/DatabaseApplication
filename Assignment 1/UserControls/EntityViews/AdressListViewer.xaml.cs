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
    /// Interaction logic for AdressListViewer.xaml
    /// </summary>
    public partial class AdressListViewer : UserControl {
        public AdressListViewer() {
            InitializeComponent();
        }

        public void AddAdresses(List<Adress> adressList) {
            foreach (Adress adress in adressList) {
                AdressViewer.Items.Add(adress);
            }
        }
    }
}

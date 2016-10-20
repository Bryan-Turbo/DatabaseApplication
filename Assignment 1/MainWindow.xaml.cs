using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Assignment_1.UserControls.ApplicationWindows;
using DatabaseTool.Entities;
using DatabaseTool.Connector;
using DatabaseTool.Query;

namespace Assignment_1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private DatabaseConnection _connection;
        public MainWindow() {
            InitializeComponent();
            _connection = new DatabaseConnection("localhost", "assignment1", "root", "");
        }
    }
}

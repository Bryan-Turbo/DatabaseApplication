using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DatabaseTool.Connector;

namespace Assignment_1 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            ConnectionHolder.SetConnection("localhost", "fukki", "root", "");
        }
    }
}

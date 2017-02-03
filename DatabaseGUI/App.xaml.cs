using System.Windows;
using DatabaseTool.Connector;

namespace DatabaseGUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            ConnectionHolder.SetConnection("database.db");
        }
    }
}

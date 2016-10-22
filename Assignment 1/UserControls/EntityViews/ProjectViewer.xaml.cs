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
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for ProjectViewer.xaml
    /// </summary>
    public partial class ProjectViewer : UserControl {
        public DatabaseConnection Connection;
        private List<Project> _projectList;
        public ProjectViewer() {
            InitializeComponent();
            Connection = new DatabaseConnection("localhost", "assignment1", "root", "");

            this._projectList = EntityContentSelector.SelectProject(this.Connection);
            PopulateViewer();
        }

        private void PopulateViewer() {
            ProjectList.Items.Clear();
            foreach (Project employee in _projectList) {
                ProjectList.Items.Add(employee);
            }
        }

        public void UpdateViewer() {
            _projectList = EntityContentSelector.SelectProject(this.Connection);
            PopulateViewer();
        }

        public Project SelectedItem {
            get {
                if (this.ProjectList.SelectedIndex < 0) 
                    return null;
                return (Project) this.ProjectList.SelectedItem;
            }
        }
    }
}

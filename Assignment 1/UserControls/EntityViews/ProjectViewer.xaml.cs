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
        public bool IsShowingFailingProjects;
        private List<Project> _projectList;
        public ProjectViewer() {
            InitializeComponent();

            PopulateViewer();
        }

        private void PopulateViewer() {
            this.IsShowingFailingProjects = false;
            this._projectList = EntityContentSelector.SelectProject();
            foreach (Project p in _projectList) {
                ProjectList.Items.Add(p);
            }
        }

        private void PopulateFailingViewer() {
            this.IsShowingFailingProjects = true;
            this._projectList = EntityContentSelector.SelectProject();
            var hqList = EntityContentSelector.SelectHeadquarters();

            foreach (Project p in this._projectList) {
                Headquarters hq = hqList.Where(h => h.BuildingName == p.BuildingName).ToList()[0];
                if (p.Budget * 0.10F < hq.Rent) {
                    ProjectList.Items.Add(p);
                }
            }
        }

        public void UpdateViewer() {
            ProjectList.Items.Clear();
            PopulateViewer();
        }

        public void UpdateFailingViewer() {
            ProjectList.Items.Clear();
            PopulateFailingViewer();
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

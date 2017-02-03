using System.Collections.Generic;
using System.Windows.Controls;
using DatabaseTool.Entities;

namespace DatabaseGUI.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for DegreeViewer.xaml
    /// </summary>
    public partial class DegreeViewer : UserControl {
        public DegreeViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Degree> degreeList) {
            this.DegreeListViewer.Items.Clear();
            foreach (Degree d in degreeList) {
                this.DegreeListViewer.Items.Add(d);
            }
        }

        public Degree SelectedItem {
            get { return (Degree) this.DegreeListViewer.SelectedItem; }
        }

        public List<Degree> Items {
            get {
                List<Degree> dL = new List<Degree>();
                var list = this.DegreeListViewer.Items;
                foreach (Degree d in list) {
                    dL.Add(d);
                }
                return dL;
            }
        }
    }
}

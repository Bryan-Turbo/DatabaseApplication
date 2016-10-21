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
    /// Interaction logic for DegreeViewer.xaml
    /// </summary>
    public partial class DegreeViewer : UserControl {
        public DegreeViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Degree> degreeList) {
            DegreeListViewer.Items.Clear();
            foreach (Degree d in degreeList) {
                DegreeListViewer.Items.Add(d);
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

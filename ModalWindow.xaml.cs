using KnowledgeBase.Persistence.Sql;
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

namespace KnowledgeBase
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : UserControl
    {
        public new ModalWindowsDataContext DataContext { get; set; }
        public ModalWindow(ModalWindowsDataContext modalWindowsDataContext)
        {
            InitializeComponent();

            DataContext = modalWindowsDataContext;
            base.DataContext = DataContext;
            KnowledgeBaseDataContext.Setup();
        }
    }
}

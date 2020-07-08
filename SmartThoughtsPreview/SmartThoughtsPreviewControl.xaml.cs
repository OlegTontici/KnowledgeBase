using KnowledgeBase.SmartThoughtsPreview;
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

namespace KnowlegdeBase.SmartThoughtsPreview
{
    /// <summary>
    /// Interaction logic for NotesPreviewControl.xaml
    /// </summary>
    public partial class SmartThoughtsPreviewControl : UserControl
    {
        public new SmartThoughtsPreviewControlDataContext DataContext{ get; private set; }
        public SmartThoughtsPreviewControl(SmartThoughtsPreviewControlDataContext smartThoughtsPreviewControlDataContext)
        {
            InitializeComponent();

            DataContext = smartThoughtsPreviewControlDataContext;
            base.DataContext = DataContext;
        }
    }
}

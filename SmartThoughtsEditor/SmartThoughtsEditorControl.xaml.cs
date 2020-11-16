using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KnowledgeBase.SmartThoughtsEditor
{
    /// <summary>
    /// Interaction logic for SmartThoughtsEditorControl.xaml
    /// </summary>
    public partial class SmartThoughtsEditorControl : UserControl
    {
        public new SmartThoughtsEditorControlDataContext DataContext { get; private set; }
        public SmartThoughtsEditorControl(SmartThoughtsEditorControlDataContext smartThoughtsEditorControlDataContext)
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, (sender, e) => Process.Start((string)e.Parameter)));

            DataContext = smartThoughtsEditorControlDataContext;
            base.DataContext = DataContext;
        }

        private void EditPreviewToggle_Click(object sender, RoutedEventArgs e)
        {
            var tb = sender as ToggleButton;

            if (tb.IsChecked.Value)
            {
                DoubleAnimation da = new DoubleAnimation
                {
                    To = this.ActualWidth / 2,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new ExponentialEase
                    {
                         EasingMode = EasingMode.EaseOut,
                         Exponent = 4
                    }
                };

                EditSourceContainer.BeginAnimation(Grid.WidthProperty, da);

                SaveButton.Visibility = Visibility.Visible;
                //SaveButton.Margin = new Thickness(this.ActualWidth / 2, 0, 0, 0);
            }
            else
            {
                DoubleAnimation da = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(0),
                };

                EditSourceContainer.BeginAnimation(Grid.WidthProperty, da);


                SaveButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}

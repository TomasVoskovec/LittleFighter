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

namespace Little_Fighter
{
    /// <summary>
    /// Interakční logika pro Map.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private void nightField_click(object sender, RoutedEventArgs e)
        {
            //Map nightField = new NightFieldMap();

            //NavigationService.Navigate(new Uri("/Views/Page.xaml?parameter=test", UriKind.Relative));
        }
    }
}

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
using JsonClassLibrary;

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

        JsonFileManager fileManager = new JsonFileManager();

        private void nightField_click(object sender, RoutedEventArgs e)
        {
            List<Map> loadedMaps = fileManager.LoadMaps("../../../AppData/Maps.json");

            Map loadedMap = new Map(null, null, null);

            foreach(Map map in loadedMaps)
            {
                if (map.Name == "Night Field")
                {
                    loadedMap = map;
                }
            }

            Combat p = new Combat(loadedMap);
            NavigationService.Navigate(p);
        }
    }
}

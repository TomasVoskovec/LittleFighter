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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace Little_Fighter
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Player playerData = new Player();
        static Enemy enemyData = new Enemy();

        bool isAnimating;

        public MainWindow()
        {
            InitializeComponent();
        }

        void fasAttackAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = playerData.Anims["fastAttack"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));

            player.Margin = new Thickness(750, 0, 0, 0);

            //isAnimating = true;
        }

        void idleAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = playerData.Anims["idle"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
        }

        private void animEnd(object sender, EventArgs e)
        {
            player.Margin = new Thickness(150, 0, 0, 0);
            idleAnim();
            isAnimating = false;
        }

        private void fastAttack_click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                fasAttackAnim();
                enemyData.HP = enemyData.HP - playerData.FastAttack();

                enemyHp.Content = enemyData.HP + " HP";

                if (enemyData.HP >= 0)
                {
                    float width = enemyData.HP * (500 / enemyData.MaxHP);
                    statsEnemy.Width = width;
                }
                else
                {
                    statsEnemy.Width = 0;
                }
            }
        }

        private void def_click(object sender, RoutedEventArgs e)
        {
            isAnimating = false;
        }
    }
}

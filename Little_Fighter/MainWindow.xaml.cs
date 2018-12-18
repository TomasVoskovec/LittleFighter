using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
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

        DispatcherTimer timer = new DispatcherTimer();

        bool isAnimating;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(oneSecondDelay);
        }

        void oneSecondDelay(object sender, EventArgs e)
        {
            isAnimating = false;
            timer.Stop();
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

            isAnimating = true;
        }

        void idleAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = playerData.Anims["idle"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
        }

        private void animEnd(object sender, RoutedEventArgs e)
        {
            player.Margin = new Thickness(150, 0, 0, 0);
            idleAnim();
        }

        private void fastAttack_click(object sender, RoutedEventArgs e)
        {
            timer.Start();

            if (!isAnimating)
            {
                fasAttackAnim();

                int damage = playerData.FastAttack();
                enemyData.HP = enemyData.HP - damage;

                gameConsole.Text = gameConsole.Text + "You dealed " + damage + " demage \n";

                enemyHp.Content = enemyData.HP + " HP";

                if (enemyData.HP >= 0)
                {
                    statsEnemy.Value = enemyData.HP;
                }
                else
                {
                    statsEnemy.Value = 0;
                }
            }
        }

        private void def_click(object sender, RoutedEventArgs e)
        {
            isAnimating = false;
        }
    }
}

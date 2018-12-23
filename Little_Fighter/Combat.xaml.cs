using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interakční logika pro Combat.xaml
    /// </summary>
    public partial class Combat : Page
    {
        public Combat()
        {
            InitializeComponent();
            startGame();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(oneSecondDelay);
        }

        // Objects
        GameData gameData = new GameData(new Player(), new Bat());
        DispatcherTimer timer = new DispatcherTimer();
        List<string> consoleCommands = new List<string> { "help", "clear", "heal enemy", "kill enemy" };

        // Bools
        bool isAnimating;
        bool enemyIsAnimating;
        bool gameOver;

        // Start game action
        void startGame()
        {
            statsEnemy.Maximum = gameData.Enemy.MaxHP;
            statsEnemy.Value = gameData.Enemy.HP;
            enemyHp.Content = gameData.Enemy.HP + " HP";
            enemy.Width = gameData.Enemy.Size * 100;

            statsPlayer.Maximum = gameData.Player.MaxHP;
            statsEnemy.Value = gameData.Player.HP;
            playerHp.Content = gameData.Player.HP + " HP";
        }

        // Delays
        void oneSecondDelay(object sender, EventArgs e)
        {
            isAnimating = false;
            timer.Stop();
        }

        // PLAYER ANIMATIONS
        void fastAttackAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["fastAttack"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));

            player.Margin = new Thickness(725 + (150 / gameData.Enemy.Size), 0, 0, 0);

            isAnimating = true;
        }

        void jumpAttackAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["jumpAttack"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));

            player.Margin = new Thickness(760, 0, 0, 0);

            isAnimating = true;
        }

        void idleAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["idle"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
        }

        // ENEMY ANIMATIONS
        void enemyIdleAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Enemy.Anims["idle"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(enemy, image);
        }

        // Animation end actions
        private void animEnd(object sender, RoutedEventArgs e)
        {
            player.Margin = new Thickness(150, 0, 0, 0);
            idleAnim();
        }

        // Write info about attack in game console
        private void attackInfo(int damage)
        {
            if (damage > 0)
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "You dealed " + damage + " demage \n";
            }
            else
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "You missed \n";
            }

            gameConsoleInfo.ScrollToEnd();
        }

        // Update anemy's life and stats
        void updateEnemyStats()
        {
            if (gameData.Enemy.HP >= 0)
            {
                statsEnemy.Value = gameData.Enemy.HP;
                enemyHp.Content = gameData.Enemy.HP + " HP";
            }
            else
            {
                statsEnemy.Value = 0;
                enemyHp.Content = "0 HP";
            }
        }

        // ACTION BUTTONS
        private void fastAttack_click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                fastAttackAnim();

                int damage = gameData.FastAttack();
                gameData.Enemy.HP = gameData.Enemy.HP - damage;

                attackInfo(damage);

                updateEnemyStats();

                timer.Start();
            }
        }

        private void jumpAttack_click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                jumpAttackAnim();

                int damage = gameData.JumpAttack();
                gameData.Enemy.HP = gameData.Enemy.HP - damage;

                attackInfo(damage);

                updateEnemyStats();

                timer.Start();
            }
        }

        private void def_click(object sender, RoutedEventArgs e)
        {
            isAnimating = false;
        }

        void deleteGameConsoleInput()
        {
            gameConsoleInput.Clear();
        }

        // CONSOLE
        bool gameConsoleCommands(string command)
        {
            bool commandExist = false;

            if (command == "clear")
            {
                commandExist = true;
            }
            else if (command == "heal enemy")
            {
                gameData.Enemy.HP = gameData.Enemy.MaxHP;
                updateEnemyStats();
                commandExist = true;
            }
            else if (command == "kill enemy")
            {
                gameData.Enemy.HP = 0;
                updateEnemyStats();
                commandExist = true;
            }
            if (command == "help")
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "######## Commands ########\n\n";
                foreach (string consoleCommand in consoleCommands)
                {
                    gameConsoleInfo.Text = gameConsoleInfo.Text + "/" + consoleCommand + "\n";
                }
                gameConsoleInfo.Text = gameConsoleInfo.Text + "\n##########################\n";
                commandExist = true;
            }

            return commandExist;
        }

        void sendGameConsoleData()
        {
            if (gameConsoleInput.Text != "")
            {
                string conseleInput = gameConsoleInput.Text;
                deleteGameConsoleInput();

                char[] commandArray = conseleInput.ToCharArray();

                if (commandArray[0] == 47)
                {
                    if (gameConsoleCommands(new string(commandArray.Skip(1).ToArray())))
                    {
                        if (conseleInput != "/clear")
                        {
                            conseleInput = "Command '" + new string(commandArray.Skip(1).ToArray()) + "' activated\n";
                        }
                        else
                        {
                            gameConsoleInfo.Clear();
                            conseleInput = "";
                        }
                    }
                    else
                    {
                        conseleInput = "Unknown command '" + new string(commandArray.Skip(1).ToArray()) + "'\n";
                    }
                }
                else
                {
                    conseleInput = conseleInput + "\n";
                }

                gameConsoleInfo.Text = gameConsoleInfo.Text + conseleInput;
                gameConsoleInfo.ScrollToEnd();
            }
        }

        private void gameConsole_click(object sender, RoutedEventArgs e)
        {
            sendGameConsoleData();
        }

        private void gameConsolInfo_click(object sender, RoutedEventArgs e)
        {
            sendGameConsoleData();
            gameConsoleInput.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendGameConsoleData();
            }
            if (e.Key == Key.Tab)
            {
                if (gameConsole.Visibility == Visibility.Hidden)
                {
                    gameConsole.Visibility = Visibility.Visible;
                }
                else
                {
                    gameConsole.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}

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

            timerCanAttack.Interval = TimeSpan.FromSeconds(3);
            timerCanAttack.Tick += new EventHandler(canAttack);

            timerEnemyAttack.Interval = TimeSpan.FromSeconds(1.5);
            timerEnemyAttack.Tick += new EventHandler(enemyAttack);
        }

        // Objects
        GameData gameData = new GameData(new Player(), new Bat());
        DispatcherTimer timerCanAttack = new DispatcherTimer();
        DispatcherTimer timerEnemyAttack = new DispatcherTimer();
        List<string> consoleCommands = new List<string> { "help", "clear", "heal enemy", "kill enemy", "game data" };
        Stack<string> lastConsoleComands = new Stack<string>();

        int lastCommandIndex = 0;

        // Bools
        bool isAnimating;
        //bool enemyIsAnimating;

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
        void canAttack(object sender, EventArgs e)
        {
            isAnimating = false;
            timerCanAttack.Stop();
            timerEnemyAttack.Stop();
        }

        // PLAYER ANIMATIONS
        void attackAnim (Uri animationUri)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = animationUri;
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));

            player.Margin = new Thickness(725 + (150 / gameData.Enemy.Size), 0, 0, 0);

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

        void enemyHurtAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Enemy.Anims["hurt"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));
        }

        void enemyAttack(object sender, EventArgs e)
        {
            enemyAttackAnim(gameData.Enemy.Anims["idle"]);

            int damage = gameData.FastAttack();
            gameData.Player.HP = gameData.Player.HP - damage;

            updateStats();
            timerEnemyAttack.Stop();
        }

        void enemyAttackAnim(Uri animationUri)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = animationUri;
            image.EndInit();

            ImageBehavior.SetAnimatedSource(enemy, image);
            ImageBehavior.SetRepeatBehavior(enemy, new RepeatBehavior(1));

            enemy.Margin = new Thickness(0, 0, 750 + (150 / gameData.Enemy.Size), 0);
        }

        // Animation end actions
        private void animEnd(object sender, RoutedEventArgs e)
        {
            player.Margin = new Thickness(150, 0, 0, 0);
            idleAnim();
        }

        private void enemyAnimEnd(object sender, RoutedEventArgs e)
        {
            enemy.Margin = new Thickness(0, 0, 150, 0);
            enemyIdleAnim();
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
        void updateStats()
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

            if (gameData.Player.HP >= 0)
            {
                statsPlayer.Value = gameData.Player.HP;
                playerHp.Content = gameData.Player.HP + " HP";
            }
            else
            {
                statsPlayer.Value = 0;
                playerHp.Content = "0 HP";
            }
        }

        // ACTION BUTTONS
        private void fastAttack_click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                attackAnim(gameData.Player.Anims["fastAttack"]);

                int damage = gameData.FastAttack();
                gameData.Enemy.HP = gameData.Enemy.HP - damage;

                attackInfo(damage);

                updateStats();

                timerCanAttack.Start();
                timerEnemyAttack.Start();
            }
        }

        private void jumpAttack_click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                attackAnim(gameData.Player.Anims["jumpAttack"]);

                int damage = gameData.JumpAttack();
                gameData.Enemy.HP = gameData.Enemy.HP - damage;

                attackInfo(damage);

                updateStats();

                timerCanAttack.Start();
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
        void gameConsoleWrite(string text)
        {
            gameConsoleInfo.Text = gameConsoleInfo.Text + text + "\n";
        }

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
                updateStats();
                commandExist = true;
            }
            else if (command == "kill enemy")
            {
                gameData.Enemy.HP = 0;
                updateStats();
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
            if (command.Contains("color"))
            {
                string[] commWord = command.Split(' ');

                string color = commWord[1];

                gameConsoleWrite(color);
            }
            if (command.Contains("game data"))
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "######## Game Data ########\n\n";

                gameConsoleInfo.Text = gameConsoleInfo.Text + "-------- PLAYER -----------\n";
                gameConsoleInfo.Text = gameConsoleInfo.Text + "-------- ENEMY -----------\n";

                gameConsoleInfo.Text = gameConsoleInfo.Text + "\n##########################\n";

                commandExist = true;
            }
            if (commandExist)
            {
                lastConsoleComands.Push(command);
                lastCommandIndex = 0;
            }

            return commandExist;
        }

        void lastCommandUp()
        {
            try
            {
                gameConsoleInput.Text = "/" + lastConsoleComands.ElementAt(lastCommandIndex);

                if (lastCommandIndex + 1 < lastConsoleComands.Count)
                {
                    lastCommandIndex++;
                }
            }
            catch (Exception)
            {
            }
        }

        void lastCommandDown()
        {
            try
            {
                if (lastCommandIndex - 1 >= 0)
                {
                    lastCommandIndex--;
                }
                gameConsoleInput.Text = "/" + lastConsoleComands.ElementAt(lastCommandIndex);
            }
            catch (Exception)
            {
            }
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

        // MENU
        void showMenu()
        {
            menuBckgrnd.Visibility = Visibility.Visible;
            menu.Visibility = Visibility.Visible;
        }

        void hideMenu()
        {
            menuBckgrnd.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Hidden;
        }

        // Not worknig msg
        private void notWorknig_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not worknig yet");
        }

        // Binds
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
            if (e.Key == Key.F1)
            {
                if(menu.Visibility == Visibility.Hidden)
                {
                    showMenu();
                }
                else
                {
                    hideMenu();
                }
            }

            if (e.Key == Key.NumPad8)
            {
                lastCommandUp();
            }

            if (e.Key == Key.NumPad2)
            {
                lastCommandDown();
            }

        }
    }
}

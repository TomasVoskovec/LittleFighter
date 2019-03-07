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

            timerEnemyAttack.Interval = TimeSpan.FromSeconds(1);
            timerEnemyAttack.Tick += new EventHandler(enemyAttack_timer);

            timerEndEnemyAttack.Interval = TimeSpan.FromSeconds(0.5);
            timerEndEnemyAttack.Tick += new EventHandler(enemyAttackEnd_timer);

            //gameData.Player.HP = 1;
        }

        // Objects
        DispatcherTimer timerCanAttack = new DispatcherTimer();
        DispatcherTimer timerEnemyAttack = new DispatcherTimer();
        DispatcherTimer timerEndEnemyAttack = new DispatcherTimer();
        List<string> consoleCommands = new List<string> { "help", "clear", "heal enemy", "kill enemy", "game data" , "suicide", "dýl dymič"};
        Stack<string> lastConsoleComands = new Stack<string>();
        static Random rn = new Random();
        GameData gameData = new GameData(new Player(), new Bat());

        int lastCommandIndex = 0;

        // Bools
        bool isAttack;
        bool isEnemyAttack;
        bool isEnemyDeath = false;
        bool isDeath = false;
        bool isGame = true;
        bool isCriticalEffected = false;
        bool isEnemyCriticalEffected = false;

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

            loadButtons();
        }

        void loadButtons()
        {
            foreach (KeyValuePair<string, PlayerAttack> attack in gameData.Player.Attacks)
            {
                Button newButton = new Button();
                newButton.SetValue(Grid.RowProperty, 2);
                newButton.Width = 200;
                newButton.Height = 40;
                newButton.Margin = new Thickness(5);
                newButton.Content = attack.Key;
                newButton.FontSize = 15;
                newButton.FontWeight = FontWeights.Bold;
                newButton.Click += attack_click;

                actionBtns.Children.Add(newButton);
            }
        }

        // Delays
        void canAttack(object sender, EventArgs e)
        {
            isAttack = false;
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

            enemyHurtAnim();

            updateStats();

            isAttack = true;
        }

        void hurtAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["hurt"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));
        }

        void defAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["def"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));
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

            ImageBehavior.SetAnimatedSource(enemy, image);
            ImageBehavior.SetRepeatBehavior(enemy, new RepeatBehavior(1));
        }

        void enemyAttack_timer(object sender, EventArgs e)
        {
            enemyAttack();
        }

        void enemyAttack()
        {
            if (isGame)
            {
                EnemyAttack enemyAttack = gameData.Enemy.Attacks[rn.Next(0, gameData.Enemy.Attacks.Count() - 1)];

                int damage = enemyAttack.Damage(gameData.Player, gameData.Enemy);
                gameData.Player.HP = gameData.Player.HP - damage;

                if (!isCriticalEffected)
                {
                    foreach (CriticalEffect criticalEffect in enemyAttack.CriticalEffects)
                    {
                        if (isCriticalEffected == criticalEffect.isEffect())
                        {
                            isCriticalEffected = true;
                            break;
                        }
                    }
                }

                enemyAttackInfo(damage, enemyAttack.Name);

                updateStats();

                enemyAttackAnim(gameData.Enemy.Anims["attack"]);

                if(damage != 0)
                {
                    hurtAnim();
                }

                timerEnemyAttack.Stop();
            }
        }

        void enemyAttackEnd_timer(object sender, EventArgs e)
        {
            if (isCriticalEffected)
            {
                hurtAnim();
                isEnemyAttack = false;
            }
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

            isEnemyAttack = true;

        }

        void enemyDeathAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Enemy.Anims["hurt"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(enemy, image);
            ImageBehavior.SetRepeatBehavior(enemy, new RepeatBehavior(1));

            isEnemyDeath = true;
        }

        void playerDeathAnim()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = gameData.Player.Anims["death"];
            image.EndInit();

            ImageBehavior.SetAnimatedSource(player, image);
            ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));

            isDeath = true;
        }

        // Animation end actions
        private void animEnd(object sender, RoutedEventArgs e)
        {
            player.Margin = new Thickness(150, 0, 0, 0);

            if (isDeath)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = gameData.Player.Anims["after_death"];
                image.EndInit();

                ImageBehavior.SetAnimatedSource(player, image);
                ImageBehavior.SetRepeatBehavior(player, new RepeatBehavior(1));
            }
            else
            {
                idleAnim();
            }
        }

        private void enemyAnimEnd(object sender, RoutedEventArgs e)
        {
            if (isEnemyDeath)
            {
                enemy.Visibility = Visibility.Hidden;
            }

            enemy.Margin = new Thickness(0, 0, 150, 0);
            enemyIdleAnim();
        }

        // Write info about attack in game console
        private void attackInfo(int damage, string attackName = "")
        {
            if (damage > 0)
            {
                if (attackName != "")
                {
                    gameConsoleInfo.Text = gameConsoleInfo.Text + "You dealed " + damage + " demage (" + attackName + ") \n";
                }
                else
                {
                    gameConsoleInfo.Text = gameConsoleInfo.Text + "You dealed " + damage + " demage \n";
                }
            }
            else
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "You missed \n";
            }

            gameConsoleInfo.ScrollToEnd();
        }

        // Write info about enemy attack in game console
        private void enemyAttackInfo(int damage, string attackName = "")
        {
            if (damage > 0)
            {
                if (attackName != "")
                {
                    gameConsoleInfo.Text = gameConsoleInfo.Text + "Enemy dealed " + damage + " demage (" + attackName + ") \n";
                }
                else
                {
                    gameConsoleInfo.Text = gameConsoleInfo.Text + "Enemy dealed " + damage + " demage \n";
                }
            }
            else
            {
                gameConsoleInfo.Text = gameConsoleInfo.Text + "Enemy missed \n";
            }

            gameConsoleInfo.ScrollToEnd();
        }

        // Update game stats
        void updateStats()
        {
            // Update lifes
            if (gameData.Enemy.HP > 0)
            {
                statsEnemy.Value = gameData.Enemy.HP;
                enemyHp.Content = gameData.Enemy.HP + " HP";
            }
            else
            {
                statsEnemy.Value = 0;
                enemyHp.Content = "0 HP";

                gameWinAction();
            }

            if (gameData.Player.HP > 0)
            {
                statsPlayer.Value = gameData.Player.HP;
                playerHp.Content = gameData.Player.HP + " HP";
            }
            else
            {
                statsPlayer.Value = 0;
                playerHp.Content = "0 HP";

                gameOverAction();
            }
        }

        void gameOverAction()
        {
            playerDeathAnim();
            disableButtons();
            isGame = false;
        }

        void gameWinAction()
        {
            enemyDeathAnim();
            disableButtons();
            isGame = false;
        }

        void disableButtons()
        {
            foreach (object child in actionBtns.Children)
            {
                if (child is Button)
                {
                    Button button = child as Button;

                    button.IsEnabled = false;
                }
            }
        }

        void enableButtons()
        {
            foreach (object child in actionBtns.Children)
            {
                if (child is Button)
                {
                    Button button = child as Button;

                    button.IsEnabled = true;
                }
            }
        }

        // PLayer attack action
        void attackAction(string name)
        {
            if (!isAttack && isGame)
            {
                attackAnim(gameData.Player.Anims[name]);

                PlayerAttack playerAttack = gameData.Player.Attacks[name];

                int damage = playerAttack.Damage(gameData.Player, gameData.Enemy);
                gameData.Enemy.HP -= damage;

                if (!isCriticalEffected)
                {
                    foreach (CriticalEffect criticalEffect in playerAttack.CriticalEffects)
                    {
                        if (isCriticalEffected == criticalEffect.isEffect())
                        {
                            isCriticalEffected = true;
                            break;
                        }
                    }
                }

                attackInfo(damage, name);

                updateStats();

                timerCanAttack.Start();
                timerEnemyAttack.Start();
            }
        }

        // ACTION BUTTONS
        private void attack_click(object sender, RoutedEventArgs e)
        {
            Button attackBtn = (Button)sender;

            attackAction(attackBtn.Content.ToString());
        }

        private void def_click(object sender, RoutedEventArgs e)
        {
            isAttack = false;
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
            else if (command == "heal")
            {
                gameData.Player.HP = gameData.Player.MaxHP;
                isDeath = false;
                if (!isEnemyDeath)
                {
                    //enableButtons();
                    isGame = true;
                }
                idleAnim();
                updateStats();
                commandExist = true;
            }
            else if (command == "suicide")
            {
                gameData.Player.HP = 0;
                updateStats();
                commandExist = true;
            }
            else if (command == "heal enemy")
            {
                gameData.Enemy.HP = gameData.Enemy.MaxHP;
                updateStats();
                enemy.Visibility = Visibility.Visible;
                if (!isDeath)
                {
                    //enableButtons();
                    isGame = true;
                }
                isEnemyDeath = false;
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
                gameConsoleInfo.Text += "HP: " + gameData.Player.HP + "\n";
                gameConsoleInfo.Text += "Max HP: " + gameData.Player.MaxHP + "\n";
                gameConsoleInfo.Text += "Attack: " + gameData.Player.Attack + "\n";
                gameConsoleInfo.Text += "Deffense: " + gameData.Player.Defense + "\n";

                gameConsoleInfo.Text = gameConsoleInfo.Text + "\n-------- ENEMY -----------\n";
                gameConsoleInfo.Text += "HP: " + gameData.Enemy.HP + "\n";
                gameConsoleInfo.Text += "Max HP: " + gameData.Enemy.MaxHP + "\n";
                gameConsoleInfo.Text += "Attack: " + gameData.Enemy.Attack + "\n";
                gameConsoleInfo.Text += "Deffense: " + gameData.Enemy.Defense + "\n";

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

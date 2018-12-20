﻿using System;
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

        static Player playerData = new Player();
        static Enemy enemyData = new Enemy();

        DispatcherTimer timer = new DispatcherTimer();

        List<string> consoleCommands = new List<string> { "clear", "heal enemy", "kill enemy" };

        bool isAnimating;
        bool gameOver;

        void startGame()
        {
            statsEnemy.Maximum = enemyData.MaxHP;
            statsEnemy.Value = enemyData.HP;
            enemyHp.Content = enemyData.HP + " HP";

            statsPlayer.Maximum = playerData.MaxHP;
            statsEnemy.Value = playerData.HP;
            playerHp.Content = playerData.HP + " HP";
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

                gameConsoleInfo.Text = gameConsoleInfo.Text + "You dealed " + damage + " demage \n";
                gameConsoleInfo.ScrollToEnd();

                if (enemyData.HP >= 0)
                {
                    updateEnemyStats();
                }
                else
                {
                    statsEnemy.Value = 0;
                    enemyHp.Content = "0 HP";
                }
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

        bool gameConsoleCommands(string command)
        {
            bool commandExist = false;

            if (command == "clear")
            {
                commandExist = true;
            }
            else if (command == "heal enemy")
            {
                enemyData.HP = enemyData.MaxHP;
                updateEnemyStats();
                commandExist = true;
            }
            else if (command == "kill enemy")
            {
                enemyData.HP = 0;
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

        void updateEnemyStats()
        {
            statsEnemy.Value = enemyData.HP;
            enemyHp.Content = enemyData.HP + " HP";
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

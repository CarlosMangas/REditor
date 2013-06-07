/*    
*	REditor, an editor for rpg and adventure videogames.
*   Copyright (C) 2011 Carlos Garcia Mangas
*
*   This program is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*	To contact the author: drake.ahener@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Interface.Controller;

namespace Interface.View
{
    /// <summary>
    /// Logic interaction for InputKeysWindow.xaml
    /// </summary>
    public partial class InputKeysWindow : Window
    {
        private readonly List<String> keys;
        private MainWindowManager manager;

        public InputKeysWindow(MainWindowManager manager)
        {
            InitializeComponent();

            this.manager = manager;

            keys = new List<string>();

            LoadKeys();

            InitializeValues();
            SetValues();
        }

        /// <summary>
        /// Load the avaliable keys in the combo boxes.
        /// </summary>
        private void LoadKeys()
        {
            keys.Add("A");
            keys.Add("B");
            keys.Add("C");
            keys.Add("D");
            keys.Add("E");
            keys.Add("F");
            keys.Add("G");
            keys.Add("H");
            keys.Add("I");
            keys.Add("J");
            keys.Add("K");
            keys.Add("L");
            keys.Add("M");
            keys.Add("N");
            keys.Add("O");
            keys.Add("P");
            keys.Add("Q");
            keys.Add("R");
            keys.Add("S");
            keys.Add("T");
            keys.Add("U");
            keys.Add("V");
            keys.Add("W");
            keys.Add("X");
            keys.Add("Y");
            keys.Add("Z");
            keys.Add("Space");
            keys.Add("Back");
            keys.Add("Enter");
            keys.Add("LeftCtrl");
            keys.Add("LeftAlt");
            keys.Add("LeftShift");
            keys.Add("RightCtrl");
            keys.Add("RightAlt");
            keys.Add("RightShift");
            keys.Add("D0");
            keys.Add("D1");
            keys.Add("D2");
            keys.Add("D3");
            keys.Add("D4");
            keys.Add("D5");
            keys.Add("D6");
            keys.Add("D7");
            keys.Add("D8");
            keys.Add("D9");
            keys.Add("Up");
            keys.Add("Down");
            keys.Add("Left");
            keys.Add("Right");
        }

        private void InitializeValues()
        {           
            keys.Sort();
            foreach (String key in keys)
            {
                UpKeyComboBox.Items.Add(key);
                LeftKeyComboBox.Items.Add(key);
                RightKeyComboBox.Items.Add(key);
                DownKeyComboBox.Items.Add(key);
                AttackKeyComboBox.Items.Add(key);
                ShootKeyComboBox.Items.Add(key);
                UseAbilityKeyComboBox.Items.Add(key);
                ChangeMeleeKeyComboBox.Items.Add(key);
                ChangeDistanceKeyComboBox.Items.Add(key);
                ChangeAbilityKeyComboBox.Items.Add(key);
                ActionKeyComboBox.Items.Add(key);
            }                       
        }

        /// <summary>
        /// Sets the values to their actual state if the character is being edited.
        /// </summary>
        private void SetValues()
        {
            if (String.IsNullOrEmpty(manager.KeyUp))
            {
                UpKeyComboBox.SelectedIndex = (int)FindIndex("W");
            }
            else
            {
                UpKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyUp);
            }

            if (String.IsNullOrEmpty(manager.KeyLeft))
            {
                LeftKeyComboBox.SelectedIndex = (int)FindIndex("A");
            }
            else
            {
                LeftKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyLeft);
            }

            if (String.IsNullOrEmpty(manager.KeyRight))
            {
                RightKeyComboBox.SelectedIndex = (int)FindIndex("D");
            }
            else
            {
                RightKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyRight);
            }

            if (String.IsNullOrEmpty(manager.KeyDown))
            {
                DownKeyComboBox.SelectedIndex = (int)FindIndex("S");
            }
            else
            {
                DownKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyDown);
            }

            if (String.IsNullOrEmpty(manager.KeyAttack))
            {
                AttackKeyComboBox.SelectedIndex = (int)FindIndex("J");
            }
            else
            {
                AttackKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyAttack);
            }

            if (String.IsNullOrEmpty(manager.KeyShoot))
            {
                ShootKeyComboBox.SelectedIndex = (int)FindIndex("K");
            }
            else
            {
                ShootKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyShoot);
            }

            if (String.IsNullOrEmpty(manager.KeyUseAbility))
            {
                UseAbilityKeyComboBox.SelectedIndex = (int)FindIndex("L");
            }
            else
            {
                UseAbilityKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyUseAbility);
            }

            if (String.IsNullOrEmpty(manager.KeyChangeMelee))
            {
                ChangeMeleeKeyComboBox.SelectedIndex = (int)FindIndex("Q");
            }
            else
            {
                ChangeMeleeKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyChangeMelee);
            }

            if (String.IsNullOrEmpty(manager.KeyChangeDistance))
            {
                ChangeDistanceKeyComboBox.SelectedIndex = (int)FindIndex("E");
            }
            else
            {
                ChangeDistanceKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyChangeDistance);
            }

            if (String.IsNullOrEmpty(manager.KeyChangeAbility))
            {
                ChangeAbilityKeyComboBox.SelectedIndex = (int)FindIndex("R");
            }
            else
            {
                ChangeAbilityKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyChangeAbility);
            }

            if (String.IsNullOrEmpty(manager.KeyAction))
            {
                ActionKeyComboBox.SelectedIndex = (int)FindIndex("Space");
            }
            else
            {
                ActionKeyComboBox.SelectedIndex = (int)FindIndex(manager.KeyAction);
            }
        }

        /// <summary>
        /// Find the index of a key.
        /// </summary>
        /// <param name="searchedKey">Key</param>
        /// <returns>Index of the Key</returns>
        private int? FindIndex(string searchedKey)
        {
            int total = 0;

            foreach (string key in keys)
            {
                if (searchedKey == key)
                {
                    return total;
                }
                else
                {
                    total++;
                }
            }

            return null;
        }

        /// <summary>
        /// Saves the input key's information and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptKeys_Click(object sender, RoutedEventArgs e)
        {
            manager.KeyUp = (string)UpKeyComboBox.SelectedItem;
            manager.KeyLeft = (string)LeftKeyComboBox.SelectedItem;
            manager.KeyRight = (string)RightKeyComboBox.SelectedItem;
            manager.KeyDown = (string)DownKeyComboBox.SelectedItem;
            manager.KeyAttack = (string)AttackKeyComboBox.SelectedItem;
            manager.KeyShoot = (string)ShootKeyComboBox.SelectedItem;
            manager.KeyUseAbility = (string)UseAbilityKeyComboBox.SelectedItem;
            manager.KeyChangeMelee = (string)ChangeMeleeKeyComboBox.SelectedItem;
            manager.KeyChangeDistance = (string)ChangeDistanceKeyComboBox.SelectedItem;
            manager.KeyChangeAbility = (string)ChangeAbilityKeyComboBox.SelectedItem;
            manager.KeyAction = (string)ActionKeyComboBox.SelectedItem;

            this.Close();
        }


    }
}

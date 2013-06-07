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
using Interface.Model;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for WeaponWindow.xaml
    /// </summary>
    public partial class WeaponWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private Weapon weapon;
        private bool edit;

        private string type;
        private string speed;
        private string name;
        private int damage;
        private string imageWeapon;
        private string imageArrowUp, imageArrowDown, imageArrowLeft, imageArrowRight;

        public WeaponWindow(MainWindowManager manager, bool edit, Weapon weapon)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.weapon = weapon;

            if (edit)
            {
                SetValues();
            }

            InitializeValues(); 
        }

        private void InitializeValues()
        {
            damage = (int)EffectValue.Value;            
        }

        /// <summary>
        /// Sets the values to their actual state if the character is being edited.
        /// </summary>
        private void SetValues()
        {
            WeaponNameTextBox.Text = weapon.Name;
            name = weapon.Name;

            EffectValue.Value = weapon.Damage;
            
            type = weapon.Type;
            switch (weapon.Type)
            {
                case "melee":
                    TypeComboBox.SelectedIndex = 0;
                    break;
                case "distance":
                    TypeComboBox.SelectedIndex = 1;
                    break;
            }

            speed = weapon.Speed;
            switch (weapon.Speed)
            {
                case "slow":
                    SpeedComboBox.SelectedIndex = 0;
                    break;
                case "normal":
                    SpeedComboBox.SelectedIndex = 1;
                    break;
                case "fast":
                    SpeedComboBox.SelectedIndex = 2;
                    break;
            }

            imageWeapon = SetImageLabel(weapon.ImageWeapon, WeaponImageLabel);

            if (weapon.ImageArrowUp != null)
            {

                imageArrowUp = SetImageLabel(weapon.ImageArrowUp, WeaponArrowUpLabel);
                imageArrowDown = SetImageLabel(weapon.ImageArrowDown, WeaponArrowDownLabel);
                imageArrowLeft = SetImageLabel(weapon.ImageArrowLeft, WeaponArrowLeftLabel);
                imageArrowRight = SetImageLabel(weapon.ImageArrowRight, WeaponArrowRightLabel);
            }

        }

        /// <summary>
        /// Sets the labels to show the actual state of the image files.
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="label">Label to be changed</param>
        /// <returns>Path of the file</returns>
        private string SetImageLabel(string path, Label label)
        {
            BitmapImage bmi = new BitmapImage(new Uri(path));
            Image im = new Image();
            im.Source = bmi;

            if (label.MinHeight < bmi.Height)
            {
                label.Height = bmi.Height + 20;
            }
            else
            {
                label.Height = label.MinHeight;
            }

            if (label.MinWidth < bmi.Width)
            {
                label.Width = bmi.Width + 20;
            }
            else
            {
                label.Width = label.MinWidth;
            }

            label.Content = im;

            return path;
        }

        /// <summary>
        /// Sets the weapon's name when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = WeaponNameTextBox.Text;
        }

        /// <summary>
        /// Changes the weapon's type when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)TypeComboBox.SelectedItem;
            type = selected.Content.ToString();

            if (type == "distance")
            {
                WeaponArrowUpButton.IsEnabled = true;
                WeaponArrowDownButton.IsEnabled = true;
                WeaponArrowLeftButton.IsEnabled = true;
                WeaponArrowRightButton.IsEnabled = true;
            }
            else
            {
                WeaponArrowUpButton.IsEnabled = false;
                WeaponArrowDownButton.IsEnabled = false;
                WeaponArrowLeftButton.IsEnabled = false;
                WeaponArrowRightButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Set's the weapon's speed when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)SpeedComboBox.SelectedItem;
            speed = selected.Content.ToString();
        }

        /// <summary>
        /// Sets the damage value of the weapon when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DamageValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            damage = (int)EffectValue.Value;
        }

        /// <summary>
        /// Sets the weapon's immage when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WeaponImageLabel);

            if (aux != null)
            {
                imageWeapon = aux;
            }
        }

        /// <summary>
        /// Sets the up bullet image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponArrowUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WeaponArrowUpLabel);

            if (aux != null)
            {
                imageArrowUp = aux;
            }
        }

        /// <summary>
        /// Sets the down bullet image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponArrowDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WeaponArrowDownLabel);

            if (aux != null)
            {
                imageArrowDown = aux;
            }
        }

        /// <summary>
        /// Sets the left bullet image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponArrowLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WeaponArrowLeftLabel);

            if (aux != null)
            {
                imageArrowLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right bullet image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeaponArrowRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WeaponArrowRightLabel);

            if (aux != null)
            {
                imageArrowRight = aux;
            }
        }

        /// <summary>
        /// Cancel the creation of the weapon and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelWeapon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the weapon's information and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptWeapon_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    errorMessage = "You have to input a name.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(imageWeapon))
                {
                    errorMessage = "You have to select an image for the weapon.";
                    throw new NullReferenceException();
                }

                if(damage == 0)
                {
                    errorMessage = "The damage must be higher than 0.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(type))
                {
                    errorMessage = "You have to select the type of the weapon.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(speed))
                {
                    errorMessage = "You have to select the speed of the weapon.";
                    throw new NullReferenceException();
                }

                if (type == "distance")
                {
                    if (String.IsNullOrEmpty(imageArrowUp) || String.IsNullOrEmpty(imageArrowDown) || String.IsNullOrEmpty(imageArrowLeft) || String.IsNullOrEmpty(imageArrowRight))
                    {
                        errorMessage = "You have to select all the arrow images.";
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    imageArrowUp = null;
                    imageArrowDown = null;
                    imageArrowLeft = null;
                    imageArrowRight = null;
                }

                name = name.Replace(" ", "_");

                if (manager.CheckIfNumber(name))
                {
                    errorMessage = "The name cannot be only numbers.";
                    throw new Exception();
                }

                if (edit)
                {
                    manager.Weapons.Remove(weapon);
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exits";
                    throw new NullReferenceException();
                }

                Weapon auxWeapon = new Weapon(name, type, damage, speed, imageWeapon, imageArrowUp, imageArrowDown, 
                                                imageArrowLeft, imageArrowRight);
                manager.Weapons.Add(auxWeapon);
                manager.Weapons.OrderBy(x => x.Name);


                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}

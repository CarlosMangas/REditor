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
using Interface.Model;
using Interface.Controller;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for AbilityWindow.xaml
    /// </summary>
    public partial class AbilityWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private Ability ability;
        private bool edit;

        private string type;
        private string affect;
        private int duration;
        private string name;
        private int effect, mana;
        private string imageAbility;
        private string imageAttackUp, imageAttackDown, imageAttackLeft, imageAttackRight;
        private string speed;

        public AbilityWindow(MainWindowManager manager, bool edit, Ability ability)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.ability = ability;

            if (edit)
            {
                SetValues();
            }

            InitializeValues();
        }

        private void InitializeValues()
        {
            mana = (int)ManaCostValue.Value;
        }

        /// <summary>
        /// Sets the values to their actual state if the ability is being edited.
        /// </summary>
        private void SetValues()
        {
            AbilityNameTextBox.Text = ability.Name;
            name = ability.Name;

            EffectValue.Value = ability.Effect;
            effect = ability.Effect;
            ManaCostValue.Value = ability.Mana;

            type = ability.Type;
            switch (type)
            {
                case "heal":
                    TypeComboBox.SelectedIndex = 0;
                    break;
                case "attack":
                    TypeComboBox.SelectedIndex = 1;
                    break;
                case "augmentation":
                    TypeComboBox.SelectedIndex = 2;

                    SecondsValue.Value = ability.Duration;
                    duration = ability.Duration;

                    affect = ability.Affect;
                    switch (affect)
                    {
                        case "dexterity":
                            AffectComboBox.SelectedIndex = 0;
                            break;
                        case "intelligence":
                            AffectComboBox.SelectedIndex = 1;
                            break;
                        case "strength":
                            AffectComboBox.SelectedIndex = 2;
                                break;
                    }
                    break;
            }

            speed = ability.Speed;
            switch (ability.Speed)
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

            imageAbility = SetImageLabel(ability.ImageAbility, AbilityImageLabel);

            if (type == " attack")
            {

                imageAttackUp = SetImageLabel(ability.ImageAttackUp, AbilityAttackUpLabel);
                imageAttackDown = SetImageLabel(ability.ImageAttackDown, AbilityAttackDownLabel);
                imageAttackLeft = SetImageLabel(ability.ImageAttackLeft, AbilityAttackLeftLabel);
                imageAttackRight = SetImageLabel(ability.ImageAttackRight, AbilityAttackRightLabel);
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
        /// Sets the type of the ability when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)TypeComboBox.SelectedItem;
            type = selected.Content.ToString();

            if (type == "attack")
            {
                AbilityAttackUpButton.IsEnabled = true;
                AbilityAttackDownButton.IsEnabled = true;
                AbilityAttackLeftButton.IsEnabled = true;
                AbilityAttackRightButton.IsEnabled = true;
                SpeedComboBox.IsEnabled = true;
            }
            else
            {
                AbilityAttackUpButton.IsEnabled = false;
                AbilityAttackDownButton.IsEnabled = false;
                AbilityAttackLeftButton.IsEnabled = false;
                AbilityAttackRightButton.IsEnabled = false;
                SpeedComboBox.IsEnabled = false;
            }

            if (type == "augmentation")
            {
                AffectComboBox.IsEnabled = true;
                SecondsValue.IsEnabled = true;
            }
            else
            {
                AffectComboBox.IsEnabled = false;
                SecondsValue.IsEnabled = false;
            }
        }

        /// <summary>
        /// Sets the ability name when the text box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = AbilityNameTextBox.Text;
        }

        /// <summary>
        /// Sets the effect of the ability when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            effect = (int)EffectValue.Value;
        }

        /// <summary>
        /// Sets the mana cost of the ability when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManaCostValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            mana = (int)ManaCostValue.Value;
        }

        /// <summary>
        /// Sets the main image of the ability when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(AbilityImageLabel);

            if (aux != null)
            {
                imageAbility = aux;
            }
        }

        /// <summary>
        /// Sets the up attack image of the attack ability when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityAttackUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(AbilityAttackUpLabel);

            if (aux != null)
            {
                imageAttackUp = aux;
            }
        }

        /// <summary>
        /// Sets the down attack image of the attack ability when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityAttackDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(AbilityAttackDownLabel);

            if (aux != null)
            {
                imageAttackDown = aux;
            }
        }

        /// <summary>
        /// Sets the left attack image of the attack ability when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityAttackLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(AbilityAttackLeftLabel);

            if (aux != null)
            {
                imageAttackLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right attack image of the attack ability when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbilityAttackRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(AbilityAttackRightLabel);

            if (aux != null)
            {
                imageAttackRight = aux;
            }
        }

        /// <summary>
        /// Sets the atribute affected by the augmentation ability when you change the combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)AffectComboBox.SelectedItem;
            affect = selected.Content.ToString();
        }

        /// <summary>
        /// Sets the duration of the augmentation ability when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondsValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            duration = (int)SecondsValue.Value;
        }

        /// <summary>
        /// Saves the information of the ability.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptAbility_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    errorMessage = "You have to input a name.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(imageAbility))
                {
                    errorMessage = "You have to select an image for the ability.";
                    throw new NullReferenceException();
                }

                if (effect == 0)
                {
                    errorMessage = "The effect/damage for the ability must be higher than 0.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(type))
                {
                    errorMessage = "You have to select the ability type.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(affect) && type == "augmentation")
                {
                    errorMessage = "You have to select the characteristic the ability is going to affect.";
                    throw new NullReferenceException();           
                }

                if (duration == 0 && type == "augmentation")
                {
                    errorMessage = "You have to input the duration for the ability.";
                    throw new NullReferenceException();
                }

                if (type == "attack")
                {
                    if (imageAttackUp == null || imageAttackDown == null || imageAttackLeft == null || imageAttackRight == null)
                    {
                        errorMessage = "You have to select all the attack images.";
                        throw new NullReferenceException();
                    }

                    if (speed == null)
                    {
                        errorMessage = "You have to select the speed of the ability.";
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    imageAttackUp = null;
                    imageAttackDown = null;
                    imageAttackLeft = null;
                    imageAttackRight = null;
                    speed = null;
                }

                name = name.Replace(" ", "_");

                if (edit)
                {
                    manager.Abilities.Remove(ability);
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exists";
                    throw new NullReferenceException();
                }

                Ability auxAbility = new Ability(name, type, effect, mana, imageAbility,
                                                    imageAttackUp, imageAttackDown,
                                                    imageAttackLeft, imageAttackRight, affect, duration, speed);

                manager.Abilities.Add(auxAbility);
                manager.Abilities.OrderBy(x => x.Name);


                this.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Cancel the creation of the ability and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelAbility_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sets the speed of the attack ability when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)SpeedComboBox.SelectedItem;
            speed = selected.Content.ToString();
        }

    }
}

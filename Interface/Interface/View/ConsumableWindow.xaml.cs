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
    /// Interaction logic for Consumable.xaml
    /// </summary>
    public partial class ConsumableWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private Consumable consumable;
        private bool edit;

        private string type;
        private string name;
        private string image;
        private int effect;

        public ConsumableWindow(MainWindowManager manager, bool edit, Consumable consumable)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.consumable = consumable;

            if (edit)
            {
                SetValues();
            }
        }

        /// <summary>
        /// Sets the values to their actual state if the ability is being edited.
        /// </summary>
        private void SetValues()
        {
            ConsumableNameTextBox.Text = consumable.Name;
            name = consumable.Name;

            EffectValue.Value = consumable.Effect;
            effect = consumable.Effect;

            type = consumable.Type;
            switch (type)
            {
                case "strenght":
                    TypeComboBox.SelectedIndex = 0;
                    break;
                case "intelligence":
                    TypeComboBox.SelectedIndex = 1;
                    break;
                case "dexterity":
                    TypeComboBox.SelectedIndex = 2;
                    break;
                case "health":
                    TypeComboBox.SelectedIndex = 3;
                    break;
                case "mana":
                    TypeComboBox.SelectedIndex = 4;
                    break;
            }

            image = SetImageLabel(consumable.Image, ConsumableImageLabel);
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
        /// Sets the consumable name when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsumableNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = ConsumableNameTextBox.Text;
        }

        /// <summary>
        /// Sets the type of consumable when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)TypeComboBox.SelectedItem;
            type = selected.Content.ToString();
        }

        /// <summary>
        /// Sets the effect of the consumable when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            effect = (int)EffectValue.Value;
        }

        /// <summary>
        /// Sets the image of the consumable when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsumableImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(ConsumableImageLabel);

            if (aux != null)
            {
                image = aux;
            }
        }

        /// <summary>
        /// Cancel the creation of the consumable and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelConsumable_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the consumable's information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptConsumable_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    errorMessage = "You have to input a name.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(image))
                {
                    errorMessage = "You have to select an image for the consumable.";
                    throw new NullReferenceException();
                }

                if (effect == 0)
                {
                    errorMessage = "The effect/damage for the ability must be higher than 0.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(type))
                {
                    errorMessage = "You have to select the consumable type.";
                    throw new NullReferenceException();
                }

                name = name.Replace(" ", "_");

                if (manager.CheckIfNumber(name))
                {
                    errorMessage = "The name cannot be only numbers.";
                    throw new Exception();
                }

                if (edit)
                {
                    manager.Consumables.Remove(consumable);
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exists";
                    throw new NullReferenceException();
                }               

                Consumable auxConsumable = new Consumable(name, type, effect, image);

                manager.Consumables.Add(auxConsumable);
                manager.Consumables.OrderBy(x => x.Name);

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}

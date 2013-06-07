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
    /// Interaction logic for DoorKeyWindow.xaml
    /// </summary>
    public partial class DoorKeyWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private Door door;
        private KeyItem key;
        private bool edit;

        private string nameDoor;
        private string nameKey;
        private string imageDoor;
        private string imageKey;

        public DoorKeyWindow(MainWindowManager manager, bool edit, Door door, KeyItem key)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.door = door;
            this.key = key;

            if (edit)
            {
                SetValues();
            }
        }

        /// <summary>
        /// Sets the values to their actual state if the character is being edited.
        /// </summary>
        private void SetValues()
        {
            string [] aux = door.Name.Split('_');
            DoorNameTextBox.Text = aux[0];
            nameDoor = door.Name;
            nameKey = key.Name;

            imageDoor = SetImageLabel(door.Image, DoorImageLabel);
            imageKey = SetImageLabel(key.Image, KeyImageLabel);
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
        /// Sets the name of the door when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameDoor = DoorNameTextBox.Text;
            nameDoor += "_Door";

            nameKey = DoorNameTextBox.Text;
            nameKey += "_Key";
        }

        /// <summary>
        /// Sets the door's image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(DoorImageLabel);

            if (aux != null)
            {
                imageDoor = aux;
            }
        }

        /// <summary>
        /// Sets the key's image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(KeyImageLabel);

            if (aux != null)
            {
                imageKey = aux;
            }
        }

        /// <summary>
        /// Cancel the creation of the items and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelDoor_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// Saves the door's information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptDoor_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (String.IsNullOrEmpty(nameDoor))
                {
                    errorMessage = "You have to input a name.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(imageDoor))
                {
                    errorMessage = "You have to select an image for the door.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(imageKey))
                {
                    errorMessage = "You have to select an image for the key.";
                    throw new NullReferenceException();
                }

                nameDoor = nameDoor.Replace(" ", "_");
                nameKey = nameKey.Replace(" ", "_");

                if (manager.CheckIfNumber(nameDoor))
                {
                    errorMessage = "The name cannot be only numbers.";
                    throw new Exception();
                }

                if (edit)
                {
                    manager.Doors.Remove(door);
                    manager.Keys.Remove(key);
                }
                if (manager.NameRepeated(nameDoor))
                {
                    errorMessage = "This name already exists";
                    throw new NullReferenceException();
                }

                Door auxDoor = new Door(nameDoor, imageDoor, manager.DoorID);

                manager.Doors.Add(auxDoor);
                manager.Doors.OrderBy(x => x.Name);

                KeyItem auxKey = new KeyItem(nameKey, imageKey, manager.DoorID);

                manager.Keys.Add(auxKey);
                manager.Keys.OrderBy(x => x.Name);

                manager.DoorID++;

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}

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
    /// Interaction logic for WallWindow.xaml
    /// </summary>
    public partial class WallWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private BackgroundItem bgItem;
        private bool edit;

        private string name;
        private string solid, bulletProof;
        private string image;

        public WallWindow(MainWindowManager manager, bool edit, BackgroundItem bgItem)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.bgItem = bgItem;

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
            WallNameTextBox.Text = bgItem.Name;
            name = bgItem.Name;

            image = SetImageLabel(bgItem.Image, WallImageLabel);

            if (bgItem.Solid == "true")
            {
                CheckSolid.IsChecked = true;
                solid = "true";
            }
            else
            {
                solid = "false";
            }

            if (bgItem.BulletProof == "true")
            {
                CheckBulletProof.IsChecked = true;
                bulletProof = "true";
            }
            else
            {
                bulletProof = "false";
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
        /// Sets the wall's name when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WallNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = WallNameTextBox.Text;
        }

        /// <summary>
        /// If it's checked the wall is solid, no object can go through, except bullets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSolid_Checked(object sender, RoutedEventArgs e)
        {
            solid = "true";
        }

        /// <summary>
        /// If it's checked the bullets can't go through.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBulletProof_Checked(object sender, RoutedEventArgs e)
        {
            bulletProof = "true";
        }

        /// <summary>
        /// If it's unchecked any object can go through it, except bullets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSolid_Unchecked(object sender, RoutedEventArgs e)
        {
            solid = "false";
        }

        /// <summary>
        /// If it's unchecked the bullets can go through.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBulletProof_Unchecked(object sender, RoutedEventArgs e)
        {
            bulletProof = "false";
        }

        /// <summary>
        /// Sets the wall's image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WallImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(WallImageLabel);

            if (aux != null)
            {
                image = aux;
            }
        }

        /// <summary>
        /// Cancel the creation of the wall and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelWall_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the wall's information and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptWall_Click(object sender, RoutedEventArgs e)
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
                    errorMessage = "You have to select an image for the wall.";
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
                    manager.BgItems.Remove(bgItem);
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exists";
                    throw new NullReferenceException();
                }

                BackgroundItem auxBgItem = new BackgroundItem(name, solid, bulletProof, image);

                manager.BgItems.Add(auxBgItem);
                manager.BgItems.OrderBy(x => x.Name);

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}

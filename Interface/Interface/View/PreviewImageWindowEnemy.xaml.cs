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
    /// Interaction logic for PreviewImageWindowEnemy.xaml
    /// </summary>
    public partial class PreviewImageWindowEnemy : Window
    {
        private Enemy enemy;
        private OpenFileManager openFileManager;

        private string image;
        private bool edit;

        public PreviewImageWindowEnemy(Enemy enemy , bool edit)
        {
            InitializeComponent();

            openFileManager = new OpenFileManager();
            this.enemy = enemy;
            this.edit = edit;

            if (edit)
            {
                image = SetImageLabel(enemy.PreviewImage, PreviewImageLabel);
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
        /// Saves the preview image for the enemy and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewAccept_Click(object sender, RoutedEventArgs e)
        {
            enemy.PreviewImage = image;

            this.Close();
        }

        /// <summary>
        /// Sets the preview image for the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(PreviewImageLabel);

            if (aux != null)
            {
                image = aux;
            }
        }
    }
}

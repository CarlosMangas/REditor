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
    /// Interaction logic for ScreensWindow.xaml
    /// </summary>
    public partial class ScreensWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private string deathScreen;
        private string endScreen;

        private string deathScreenImage;
        private string endScreenImage;

        public ScreensWindow(MainWindowManager manager, string deathScreen, string endScreen)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.deathScreen = deathScreen;
            this.endScreen = endScreen;

            InitializeValues();
        }

        private void InitializeValues()
        {
            if (deathScreen != null)
            {
                BitmapImage bmiDeath = new BitmapImage(new Uri(deathScreen));
                Image imDeath = new Image();
                imDeath.Source = bmiDeath;

                DeathScreenImageLabel.Content = imDeath;
            }

            if (endScreen != null)
            {
                BitmapImage bmiEnd = new BitmapImage(new Uri(endScreen));
                Image imEnd = new Image();
                imEnd.Source = bmiEnd;

                EndScreenImageLabel.Content = imEnd;
            }
        }

        /// <summary>
        /// Sets the end screen's image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndScreenImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EndScreenImageLabel);

            if (aux != null)
            {
                endScreenImage = aux;
            }
        }

        /// <summary>
        /// Sets the death screen's image when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeathScreenImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(DeathScreenImageLabel);

            if (aux != null)
            {
                deathScreenImage = aux;
            }
        }

        /// <summary>
        /// Cancel the screen editing anf closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelScreens_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the changes and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptScreens_Click(object sender, RoutedEventArgs e)
        {
            manager.EndScreen = endScreenImage;
            manager.DeathScreen = deathScreenImage;

            this.Close();
        }


    }
}

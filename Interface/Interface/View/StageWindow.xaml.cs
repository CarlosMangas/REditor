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
    /// Interaction logic for StageWindow.xaml
    /// </summary>
    public partial class StageWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private bool edit;
        private Stage stage;

        private string image;
        private string music;

        public StageWindow(MainWindowManager manager, bool edit, Stage stage)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.stage = stage;

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
            image = SetImageLabel(stage.Background, StageBackgroundImageLabel);

            if (stage.Music != null)
            {
                music = stage.Music;
                StageMusicText.Text = stage.Music;
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
        /// Cancel the creation of the stage and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelStage_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sets the stage's background when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageBackgroundImageButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(StageBackgroundImageLabel);

            try
            {
                if (aux != null)
                {
                    BitmapImage bmi = new BitmapImage(new Uri(aux));

                    if (bmi.Height > 380 || bmi.Width > 640)
                    {
                        StageBackgroundImageLabel.Content = null;
                        throw new Exception();
                    }

                    image = aux;
                }
            }

            catch(Exception)
            {
                MessageBox.Show("You cannot load an image bigger than 640x380", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Sets the stage's music when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageMusicButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetTextBoxPathFromFile(StageMusicText);

            if (aux != null)
            {
                music = aux;
            }
        }

        /// <summary>
        /// Saves the stage's information and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptStage_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (String.IsNullOrEmpty(image))
                {
                    errorMessage = "You have to select an image for the stage.";
                    throw new NullReferenceException();
                }

                if (edit)
                {
                    stage.Background = image;
                    stage.Music = music;
                }
                else
                {
                    manager.ID++;

                    Stage auxStage = new Stage(manager.ID, image, music);

                    manager.Stages.Add(auxStage);
                } 

                this.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

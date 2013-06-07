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
using Microsoft.Win32;
using System.Windows.Forms;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public string PathTemplateProject { get; set; }
        public string PathNewGame { get; set; }

        public CreateProjectWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the window and set the result to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptProject_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Closes the window and set the result to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelProject_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Sets the location of the Template folder and writes the informative text box. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the Folder that contains Template Project";
            fbd.ShowNewFolderButton = false;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            Nullable<DialogResult> result = fbd.ShowDialog();

            if (result.HasValue && result.Value.ToString() == "OK")
            {
                FolderTemplateTextBox.Text = fbd.SelectedPath;
                PathTemplateProject = fbd.SelectedPath;
            } 
        }

        /// <summary>
        /// Sets the location of the new game and writes the informative text box. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the Folder where Game will be created";
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            Nullable<DialogResult> result = fbd.ShowDialog();

            if (result.HasValue && result.Value.ToString() == "OK")
            {
                FolderNewGameTextBox.Text = fbd.SelectedPath;
                PathNewGame = fbd.SelectedPath;
            }        
        }
    }
}

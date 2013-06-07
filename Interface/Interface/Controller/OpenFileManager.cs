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
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Interface.Controller
{
    /// <summary>
    /// This class allows the other classes to load images or sounds.
    /// </summary>
    class OpenFileManager
    {
        /// <summary>
        /// Allows a interface control to load an image and set the content of a label with the image.
        /// </summary>
        /// <param name="label">Label to set the image</param>
        /// <returns>Name of the file</returns>
        public string SetLabelImageFromFile(Label label)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select the Image";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "PNG Images (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            Stream myStream;
            FileStream fs = null;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fs = (FileStream)myStream;

                            if (!(fs.Name.EndsWith(".png", true, null)))
                            {
                                throw new FileFormatException();
                            }

                            BitmapImage bmi = new BitmapImage(new Uri(fs.Name));
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
                        }
                    }
                }
                catch (FileFormatException)
                {
                    MessageBox.Show("File not valid.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not open file from disk.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (fs != null)
            {
                return fs.Name;
            }

            return null;
        }

        /// <summary>
        /// Allows a interface control to load an sound and set the content of a text box with the name fo the file.
        /// </summary>
        /// <param name="textBox">Text box to set the name of the file</param>
        /// <returns>Name of the file</returns>
        public string SetTextBoxPathFromFile(TextBox textBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select the Music Background";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "WAV (*.wav)|*.wav|MP3 (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            Stream myStream;
            FileStream fs = null;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fs = (FileStream)myStream;
                            if (!(fs.Name.EndsWith(".wav", true, null)) && !(fs.Name.EndsWith(".mp3", true, null)))
                                throw new FileFormatException();

                            textBox.Text = fs.Name;
                        }
                    }
                }
                catch (FileFormatException)
                {
                    MessageBox.Show("File not valid.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not open file from disk.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (fs != null)
            {
                return fs.Name;
            }

            return null;
        }

    }
}

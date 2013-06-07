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
using Microsoft.Win32.SafeHandles;
using Interface.Controller;
using Interface.Model;
using Interface.View;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private bool edit;

        private string name;
        private string imageMovingUp, imageMovingDown, imageMovingLeft, imageMovingRight;
        private string imageAttackingUp, imageAttackingDown, imageAttackingLeft, imageAttackingRight;
        private int frameCount, frameAttackingCount;
        private string attackSoundEffect, shootSoundEffect, castSoundEffect;
        private Weapon melee, distance;
        private int strenght, intelligence, dexterity, hitPoints, mana;
        private string initialState, speed;

        public CharacterWindow(MainWindowManager manager, bool edit)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;

            InitializeLists();

            if (edit)
            {
                SetValues();
            }

            InitializeValues();  
        }

        private void InitializeLists()
        {
            MeleeEquipedList.Items.Add("Nothing");
            DistanceEquipedList.Items.Add("Nothing");

            if (manager.Weapons.Count > 0)
            {
                foreach (Weapon auxWeapon in manager.Weapons)
                {
                    if (auxWeapon.Type == "melee")
                    {
                        MeleeEquipedList.Items.Add(auxWeapon.Name);
                    }
                    else
                    {
                        DistanceEquipedList.Items.Add(auxWeapon.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the values to their actual state if the character is being edited.
        /// </summary>
        private void SetValues()
        {
            CharacterNameTextBox.Text = manager.Player.Name;
            name = manager.Player.Name;

            StrenghtValue.Value = manager.Player.Strenght;
            IntelligenceValue.Value = manager.Player.Intelligence;
            DexterityValue.Value = manager.Player.Dexterity;
            HPValue.Value = manager.Player.HitPoints;
            ManaValue.Value = manager.Player.Mana;

            FramecountMovement.Value = manager.Player.FrameCount;;
            FramecountAttack.Value = manager.Player.FrameAttackingCount;

            initialState = manager.Player.InitialState;
            switch(manager.Player.InitialState)
            {
                case "up":
                    InitialDirectionComboBox.SelectedIndex = 0;
                    break;
                case "down":
                    InitialDirectionComboBox.SelectedIndex = 1;
                    break;
                case "left":
                    InitialDirectionComboBox.SelectedIndex = 2;
                    break;
                case "right":
                    InitialDirectionComboBox.SelectedIndex = 3;
                    break;
            }

            speed = manager.Player.Speed;
            switch (manager.Player.Speed)
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

            imageMovingUp = SetImageLabel(manager.Player.ImageMovingUp, CharacterMovingUpLabel);
            imageMovingDown = SetImageLabel(manager.Player.ImageMovingDown, CharacterMovingDownLabel);
            imageMovingLeft = SetImageLabel(manager.Player.ImageMovingLeft, CharacterMovingLeftLabel);
            imageMovingRight = SetImageLabel(manager.Player.ImageMovingRight, CharacterMovingRightLabel);

            if (manager.Player.ImageAttackingUp != null)
            {
                CanMelee.IsChecked = true;

                imageAttackingUp = SetImageLabel(manager.Player.ImageAttackingUp, CharacterAttackingUpLabel);
                imageAttackingDown = SetImageLabel(manager.Player.ImageAttackingDown, CharacterAttackingDownLabel);
                imageAttackingLeft = SetImageLabel(manager.Player.ImageAttackingLeft, CharacterAttackingLeftLabel);
                imageAttackingRight = SetImageLabel(manager.Player.ImageAttackingRight, CharacterAttackingRightLabel);

                if (manager.Player.Melee != null)
                {
                    melee = manager.Player.Melee;
                    CurrentMeleeTextBox.Text = melee.Name;
                }
            }

            if (manager.Player.AttackSoundEffect != null)
            {
                attackSoundEffect = manager.Player.AttackSoundEffect;
                AttackSoundEffectTextBox.Text = manager.Player.AttackSoundEffect;
            }
            if (manager.Player.ShootSoundEffect != null)
            {
                shootSoundEffect = manager.Player.ShootSoundEffect;
                ShootSoundEffectTextBox.Text = manager.Player.ShootSoundEffect;
            }
            if (manager.Player.CastSoundEffect != null)
            {
                castSoundEffect = manager.Player.CastSoundEffect;
                CastSoundEffectTextBox.Text = manager.Player.CastSoundEffect;
            }

            if (manager.Player.Distance != null)
            {
                distance = manager.Player.Distance;
                CurrentDistanceTextBox.Text = distance.Name;
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

        private void InitializeValues()
        {
            strenght = (int)StrenghtValue.Value;
            intelligence = (int)IntelligenceValue.Value;
            dexterity = (int)DexterityValue.Value;
            hitPoints = (int)HPValue.Value;
            mana = (int)ManaValue.Value;

            frameCount = (int)FramecountMovement.Value;
            frameAttackingCount = (int)FramecountAttack.Value;
        }

        /// <summary>
        /// Cancel the creation of the character and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCharacter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sets the up attack image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterAttackingUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterAttackingUpLabel);

            if(aux != null)
            {
                imageAttackingUp = aux;
            }
        }

        /// <summary>
        /// Sets the down attack image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterAttackingDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterAttackingDownLabel);

            if (aux != null)
            {
                imageAttackingDown = aux;
            }
        }

        /// <summary>
        /// Sets the left attack image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterAttackingLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterAttackingLeftLabel);

            if (aux != null)
            {
                imageAttackingLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right attack image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterAttackingRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterAttackingRightLabel);

            if (aux != null)
            {
                imageAttackingRight = aux;
            }
        }

        /// <summary>
        /// Sets the up moving image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterMovingUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterMovingUpLabel);

            if (aux != null)
            {
                imageMovingUp = aux;
            }
        }

        /// <summary>
        /// Sets the down moving image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterMovingDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterMovingDownLabel);

            if (aux != null)
            {
                imageMovingDown = aux;
            }
        }

        /// <summary>
        /// Sets the left moving image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterMovingLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterMovingLeftLabel);

            if (aux != null)
            {
                imageMovingLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right moving image of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterMovingRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(CharacterMovingRightLabel);

            if (aux != null)
            {
                imageMovingRight = aux;
            }
        }

        /// <summary>
        /// Sets the attack sound of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttackSoundEffectButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetTextBoxPathFromFile(AttackSoundEffectTextBox);

            if (aux != null)
            {
                attackSoundEffect = aux;
            }
        }

        /// <summary>
        /// Sets the shoot sound of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShootSoundEffectButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetTextBoxPathFromFile(ShootSoundEffectTextBox);

            if (aux != null)
            {
                shootSoundEffect = aux;
            }
        }

        /// <summary>
        /// Sets the cast sound of the character when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CastSoundEffectButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetTextBoxPathFromFile(CastSoundEffectTextBox);

            if (aux != null)
            {
                castSoundEffect = aux;
            }
        }

        /// <summary>
        /// Sets the initial melee weapon when you change the selection in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeleeEquipedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (string)MeleeEquipedList.SelectedItem;

            if (selectedItem == "Nothing")
            {
                melee = null;
                CurrentMeleeTextBox.Text = null;
            }
            else
            {
                melee = manager.GetWeapon(selectedItem);
                CurrentMeleeTextBox.Text = melee.Name;
            }
        }

        /// <summary>
        /// Sets the initial distance weapon when you change the selection in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistanceEquipedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (string)DistanceEquipedList.SelectedItem;

            if (selectedItem == "Nothing")
            {
                distance = null;
                CurrentDistanceTextBox.Text = null;
            }
            else
            {
                distance = manager.GetWeapon(selectedItem);
                CurrentDistanceTextBox.Text = distance.Name;
            }
        }

        /// <summary>
        /// Sets the character name when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = CharacterNameTextBox.Text;
        }

        /// <summary>
        /// Sets the strenght value of the character when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrenghtValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            strenght = (int)StrenghtValue.Value;
        }

        /// <summary>
        /// Sets the intelligence value of the character when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntelligenceValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            intelligence = (int)IntelligenceValue.Value;
        }

        /// <summary>
        /// Sets the dexterity value of the character when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DexterityValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dexterity = (int)DexterityValue.Value;
        }

        /// <summary>
        /// Sets the hit points value of the character when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HPValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            hitPoints = (int)HPValue.Value;
        }

        /// <summary>
        /// Sets the mana value of the character when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManaValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            mana = (int)ManaValue.Value;
        }

        /// <summary>
        /// Sets the framecount value of the moving images when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FramecountMovement_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            frameCount = (int)FramecountMovement.Value;
        }

        /// <summary>
        /// Sets the framecount value of the attacking images when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FramecountAttack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            frameAttackingCount = (int)FramecountAttack.Value;
        }

        /// <summary>
        /// Sets the initial direction of the character when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialDirectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)InitialDirectionComboBox.SelectedItem;
            initialState = selected.Content.ToString();
        }

        /// <summary>
        /// Saves the character's information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptCharacter_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            bool isAlreadySet = false;
            string previewImage = "";

            PreviewImageWindowCharacter previewWindow = null;

            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    errorMessage = "You have to input a name.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(imageMovingUp) || String.IsNullOrEmpty(imageMovingDown) || String.IsNullOrEmpty(imageMovingLeft) || String.IsNullOrEmpty(imageMovingRight))
                {
                    errorMessage = "You have to select all the moving animation images.";
                    throw new NullReferenceException();
                }

                if (frameCount == 0)
                {
                    errorMessage = "You have to input the number of frames for the moving animation.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(speed))
                {
                    errorMessage = "You have to select the speed of the character.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(initialState))
                {
                    errorMessage = "You have to select the initial direction of the character.";
                    throw new NullReferenceException();
                }

                if ((bool)CanMelee.IsChecked)
                {
                    if (String.IsNullOrEmpty(imageAttackingUp) || String.IsNullOrEmpty(initialState) || String.IsNullOrEmpty(initialState) || String.IsNullOrEmpty(initialState))
                    {
                        errorMessage = "You have to select all the attacking animation images.";
                        throw new NullReferenceException();
                    }

                    if (frameAttackingCount == 0)
                    {
                        errorMessage = "You have to input the number of frames for the attacking animation.";
                        throw new NullReferenceException();
                    }

                    if (melee == null)
                    {
                        errorMessage = "You have to select an initial melee weapon.";
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    imageAttackingUp = null;
                    imageAttackingDown = null;
                    imageAttackingLeft = null;
                    imageAttackingRight = null;
                    frameAttackingCount = 0;
                    melee = null;
                    attackSoundEffect = null;
                }

                name = name.Replace(" ", "_");

                if (manager.CheckIfNumber(name))
                {
                    errorMessage = "The name cannot be only numbers.";
                    throw new Exception();
                }
                
                if (edit)
                {
                    previewWindow = new PreviewImageWindowCharacter(manager.Player, edit);
                    previewWindow.ShowDialog();

                    previewImage = manager.Player.PreviewImage;

                    isAlreadySet = manager.Player.IsSet;
                    manager.Player = null;
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exits";
                    throw new NullReferenceException();
                }                

                manager.Player = new Character(name, strenght, intelligence, dexterity, hitPoints, mana, imageMovingUp,
                                                 imageMovingDown, imageMovingLeft, imageMovingRight, imageAttackingUp,
                                                 imageAttackingDown, imageAttackingLeft, imageAttackingRight, melee, distance,
                                                 frameCount, frameAttackingCount, attackSoundEffect, shootSoundEffect,
                                                 castSoundEffect, initialState, speed);

                manager.Player.IsSet = isAlreadySet;

                if (edit)
                {
                    manager.Player.PreviewImage = previewImage;
                }
                else
                {
                    previewWindow = new PreviewImageWindowCharacter(manager.Player, edit);
                    previewWindow.ShowDialog();
                }

                if (manager.Player.PreviewImage == null)
                {
                    manager.Player = null;
                    errorMessage = "You have to select a preview image.";
                    throw new NullReferenceException();
                }

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Let the character attack when the check box is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanMelee_Checked(object sender, RoutedEventArgs e)
        {
            CharacterAttackingDownButton.IsEnabled = true;
            CharacterAttackingUpButton.IsEnabled = true;
            CharacterAttackingLeftButton.IsEnabled = true;
            CharacterAttackingRightButton.IsEnabled = true;

            AttackSoundEffectButton.IsEnabled = true;

            MeleeEquipedList.IsEnabled = true; 
        }

        /// <summary>
        /// When the check box is unchecked the character cannot attack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanMelee_Unchecked(object sender, RoutedEventArgs e)
        {
            CharacterAttackingDownButton.IsEnabled = false;
            CharacterAttackingUpButton.IsEnabled = false;
            CharacterAttackingLeftButton.IsEnabled = false;
            CharacterAttackingRightButton.IsEnabled = false;

            AttackSoundEffectButton.IsEnabled = false;

            MeleeEquipedList.IsEnabled = false;
        }

        /// <summary>
        /// Changes the speed of the character when the combo box changes.
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

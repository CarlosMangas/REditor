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
using Interface.View;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for EnemyWindow.xaml
    /// </summary>
    public partial class EnemyWindow : Window
    {
        private MainWindowManager manager;
        private OpenFileManager openFileManager;
        private Enemy enemy;
        private bool edit;

        private string name;
        private string imageMovingUp, imageMovingDown, imageMovingLeft, imageMovingRight;
        private string imageArrowUp, imageArrowDown, imageArrowLeft, imageArrowRight;
        private int frameCount;
        private int strenght, intelligence, dexterity, hitPoints;
        private string loot;
        private string behavoir, preference, boss;
        private string initialState, speed;
        private int patrolZone, detectZone;

        public EnemyWindow(MainWindowManager manager, bool edit, Enemy enemy)
        {
            InitializeComponent();

            this.manager = manager;
            openFileManager = new OpenFileManager();
            this.edit = edit;
            this.enemy = enemy;

            InitializeList();

            if (edit)
            {
                SetValues();
            }

            InitializeValues(); 
        }

        private void InitializeValues()
        {
            strenght = (int)StrenghtValue.Value;
            intelligence = (int)IntelligenceValue.Value;
            dexterity = (int)DexterityValue.Value;
            hitPoints = (int)HPValue.Value;

            frameCount = (int)FramecountMovement.Value;

            if ((bool)FinalBossCheckBox.IsChecked)
            {
                boss = "true";
            }
            else
            {
                boss = "false";
            }
        }

        /// <summary>
        /// Sets the values to their actual state if the character is being edited.
        /// </summary>
        private void SetValues()
        {
            EnemyNameTextBox.Text = enemy.Name;
            name = enemy.Name;

            StrenghtValue.Value = enemy.Strenght;
            IntelligenceValue.Value = enemy.Intelligence;
            DexterityValue.Value = enemy.Dexterity;
            HPValue.Value = enemy.HitPoints;

            FramecountMovement.Value = enemy.FrameCount; ;

            initialState = enemy.InitialState;
            switch (enemy.InitialState)
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

            speed = enemy.Speed;
            switch (enemy.Speed)
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

            imageMovingUp = SetImageLabel(enemy.ImageMovingUp, EnemyMovingUpLabel);
            imageMovingDown = SetImageLabel(enemy.ImageMovingDown, EnemyMovingDownLabel);
            imageMovingLeft = SetImageLabel(enemy.ImageMovingLeft, EnemyMovingLeftLabel);
            imageMovingRight = SetImageLabel(enemy.ImageMovingRight, EnemyMovingRightLabel);

            if (enemy.ImageArrowUp != null)
            {
                CanShoot.IsChecked = true;

                imageArrowUp = SetImageLabel(enemy.ImageArrowUp, EnemyArrowUpLabel);
                imageArrowDown = SetImageLabel(enemy.ImageArrowDown, EnemyArrowDownLabel);
                imageArrowLeft = SetImageLabel(enemy.ImageArrowLeft, EnemyArrowLeftLabel);
                imageArrowRight = SetImageLabel(enemy.ImageArrowRight, EnemyArrowRightLabel);
            }

            behavoir = enemy.Behavoir;
            switch (enemy.Behavoir)
            {
                case "alert":
                    BehavoirComboBox.SelectedIndex = 0;
                    break;
                case "patrolling":
                    BehavoirComboBox.SelectedIndex = 1;

                    PatrolZoneValue.Value = enemy.PatrolZone;
                    patrolZone = enemy.PatrolZone;

                    break;
                case "standing":
                    BehavoirComboBox.SelectedIndex = 2;
                    break;
            }

            DetectZoneValue.Value = enemy.DetectZone;
            detectZone = enemy.DetectZone;

            preference = enemy.Preference;
            switch (enemy.Preference)
            {
                case "horizontal":
                    PreferenceComboBox.SelectedIndex = 0;
                    break;
                case "vertical":
                    PreferenceComboBox.SelectedIndex = 1;
                    break;
            }

            boss = enemy.Boss;
            if (enemy.Boss == "true")
            {
                FinalBossCheckBox.IsChecked = true;
            }

            if (enemy.Loot != null)
            {
                loot = enemy.Loot;
                CurrentLootTextBox.Text = loot;
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

        private void InitializeList()
        {
            LootList.Items.Add("Nothing");

            if (manager.Weapons.Count > 0)
            {
                foreach (Weapon auxWeapon in manager.Weapons)
                {
                    LootList.Items.Add(auxWeapon.Name);
                }
            }

            if (manager.Consumables.Count > 0)
            {
                foreach (Consumable auxConsumable in manager.Consumables)
                {
                    LootList.Items.Add(auxConsumable.Name);
                }
            }

            if (manager.Keys.Count > 0)
            {
                foreach (KeyItem auxKey in manager.Keys)
                {
                    LootList.Items.Add(auxKey.Name);
                }
            }
        }
        
        /// <summary>
        /// Sets the enemy's name when you write in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = EnemyNameTextBox.Text;
        }

        /// <summary>
        /// Sets the strenght value of the enemy when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrenghtValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            strenght = (int)StrenghtValue.Value;
        }

        /// <summary>
        /// Sets the intelligence value of the enemy when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntelligenceValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            intelligence = (int)IntelligenceValue.Value;
        }

        /// <summary>
        /// Sets the dexterity value of the enemy when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DexterityValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dexterity = (int)DexterityValue.Value;
        }

        /// <summary>
        /// Sets the hit points of the enemy when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HPValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            hitPoints = (int)HPValue.Value;
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
        /// Sets the up moving image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyMovingUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyMovingUpLabel);

            if (aux != null)
            {
                imageMovingUp = aux;
            }
        }

        /// <summary>
        /// Sets the down moving image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyMovingDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyMovingDownLabel);

            if (aux != null)
            {
                imageMovingDown = aux;
            }
        }

        /// <summary>
        /// Sets the left moving image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyMovingLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyMovingLeftLabel);

            if (aux != null)
            {
                imageMovingLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right moving image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyMovingRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyMovingRightLabel);

            if (aux != null)
            {
                imageMovingRight = aux;
            }
        }

        /// <summary>
        /// Sets the up bullet image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyArrowUpButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyArrowUpLabel);

            if (aux != null)
            {
                imageArrowUp = aux;
            }
        }

        /// <summary>
        /// Sets the down bullet image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyArrowDownButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyArrowDownLabel);

            if (aux != null)
            {
                imageArrowDown = aux;
            }
        }

        /// <summary>
        /// Sets the left bullet image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyArrowLeftButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyArrowLeftLabel);

            if (aux != null)
            {
                imageArrowLeft = aux;
            }
        }

        /// <summary>
        /// Sets the right bullet image of the enemy when you click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyArrowRightButton_Click(object sender, RoutedEventArgs e)
        {
            string aux = openFileManager.SetLabelImageFromFile(EnemyArrowRightLabel);

            if (aux != null)
            {
                imageArrowRight = aux;
            }
        }

        /// <summary>
        /// Lets the enemy shoot if it's checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanShoot_Checked(object sender, RoutedEventArgs e)
        {
            EnemyArrowUpButton.IsEnabled = true;
            EnemyArrowDownButton.IsEnabled = true;
            EnemyArrowLeftButton.IsEnabled = true;
            EnemyArrowRightButton.IsEnabled = true;
        }

        /// <summary>
        /// When it's unchecked the enemy cannot shoot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanShoot_Unchecked(object sender, RoutedEventArgs e)
        {
            EnemyArrowUpButton.IsEnabled = false;
            EnemyArrowDownButton.IsEnabled = false;
            EnemyArrowLeftButton.IsEnabled = false;
            EnemyArrowRightButton.IsEnabled = false;
        }

        /// <summary>
        /// Sets the initial direction of the enemy when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialDirectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)InitialDirectionComboBox.SelectedItem;
            initialState = selected.Content.ToString();
        }

        /// <summary>
        /// Sets the behavoir of the enemy when the combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BehavoirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)BehavoirComboBox.SelectedItem;
            behavoir = selected.Content.ToString();

            if (behavoir == "patrolling")
            {
                PatrolZoneValue.IsEnabled = true;
            }
            else
            {
                PatrolZoneValue.IsEnabled = false;
            }
        }

        /// <summary>
        /// Sets the value of the patrol area when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatrolZoneValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            patrolZone = (int)PatrolZoneValue.Value;
        }

        /// <summary>
        /// Sets the value of the detect area when the NumericUpAndDown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetectZoneValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            detectZone = (int)DetectZoneValue.Value;
        }

        /// <summary>
        /// Sets the preference of movement when the combobox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreferenceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)PreferenceComboBox.SelectedItem;
            preference = selected.Content.ToString();
        }

        /// <summary>
        /// If it's checked the enemy is the final boss.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalBossCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            boss = "true";
        }

        /// <summary>
        /// If it's unchecked the enemy is not the final boss.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalBossCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            boss = "false";
        }

        /// <summary>
        /// Sets the loot when the selection in the list changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LootList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selectedItem = (String)LootList.SelectedItem;

            if (selectedItem == "Nothing")
            {
                loot = null;
                CurrentLootTextBox.Text = null;
            }
            else
            {
                loot = selectedItem;
                CurrentLootTextBox.Text = loot;
            }
        }

        /// <summary>
        /// Cancel the creation of the enemy and closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEnemy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the enemy's information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptEnemy_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            string previewImage = "";

            PreviewImageWindowEnemy previewWindow = null;

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

                if (String.IsNullOrEmpty(initialState))
                {
                    errorMessage = "You have to select the initial direction of the character.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(behavoir))
                {
                    errorMessage = "You have to select the IA behavoir of the enemy.";
                    throw new NullReferenceException();
                }

                if (behavoir == "patrolling" && patrolZone == 0)
                {
                    errorMessage = "You have to input a number for the patrol zone.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(preference))
                {
                    errorMessage = "You have to select the IA preference of the enemy.";
                    throw new NullReferenceException();
                }

                if (String.IsNullOrEmpty(speed))
                {
                    errorMessage = "You have to select the speed of the enemy.";
                    throw new NullReferenceException();
                }

                if (detectZone == 0)
                {
                    errorMessage = "You have to input a number for the detection zone.";
                    throw new NullReferenceException();
                }

                if ((bool)CanShoot.IsChecked)
                {
                    if (String.IsNullOrEmpty(imageArrowUp) || String.IsNullOrEmpty(imageArrowDown) || String.IsNullOrEmpty(imageArrowLeft) || String.IsNullOrEmpty(imageArrowRight))
                    {
                        errorMessage = "You have to select all the arrow images.";
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    imageArrowUp = null;
                    imageArrowDown = null;
                    imageArrowLeft = null;
                    imageArrowRight = null;
                }

                name = name.Replace(" ", "_");

                if (manager.CheckIfNumber(name))
                {
                    errorMessage = "The name cannot be only numbers.";
                    throw new Exception();
                }

                if (edit)
                {
                    previewWindow = new PreviewImageWindowEnemy(enemy, edit);
                    previewWindow.ShowDialog();

                    previewImage = enemy.PreviewImage;

                    manager.Enemies.Remove(enemy);
                }
                if (manager.NameRepeated(name))
                {
                    errorMessage = "This name already exits";
                    throw new NullReferenceException();
                }

                Enemy auxEnemy = new Enemy(name, strenght, intelligence, dexterity, hitPoints, imageMovingUp,
                                                imageMovingDown, imageMovingLeft, imageMovingRight, frameCount, imageArrowUp,
                                                imageArrowDown, imageArrowLeft, imageArrowRight, loot, behavoir, preference,
                                                boss, initialState, patrolZone, detectZone, speed);

                if (edit)
                {
                    auxEnemy.PreviewImage = previewImage;
                }
                else
                {
                    previewWindow = new PreviewImageWindowEnemy(auxEnemy, edit);
                    previewWindow.ShowDialog();
                }

                if (auxEnemy.PreviewImage == null)
                {
                    errorMessage = "You have to select a preview image.";
                    throw new NullReferenceException();
                }
                else
                {
                    manager.Enemies.Add(auxEnemy);
                    manager.Enemies.OrderBy(x => x.Name);
                }

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Set's the speed of the enemy when the combo box changes.
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

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Interface.Controller;
using Interface.Model;
using System.Runtime.InteropServices;
using System.IO;

namespace Interface.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowManager manager;

        private int currentID;
        private Image textureSelected;
        private Image textureCanvas;

        public MainWindow()
        {
            InitializeComponent();

            manager = new MainWindowManager();
            currentID = -1;
        }

        /// <summary>
        /// Opens the InputKeysWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditKeys_Click(object sender, RoutedEventArgs e)
        {
            InputKeysWindow inputKeysWindow = new InputKeysWindow(manager);
            inputKeysWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the ScreensWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditScreens_Click(object sender, RoutedEventArgs e)
        {
            ScreensWindow screensWindow = new ScreensWindow(manager, manager.DeathScreen, manager.EndScreen);
            screensWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the AboutWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        /// <summary>
        /// Fills the ChildList with the elements avaliable of the selected category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainList.SelectedIndex > -1)
            {
                ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

                string selected = selectedItem.Content.ToString();

                ResetInfoLabel();
                GeneralInfoTextBox.Text = "";

                ChildList.SelectedIndex = -1;
                ChildList.Items.Clear();

                switch (selected)
                {
                    case "Abilities":

                        if (manager.Abilities.Count > 0)
                        {
                            foreach (Ability auxAbility in manager.Abilities)
                            {
                                ChildList.Items.Add(auxAbility.Name);
                            }
                        }

                        break;
                    case "Character":

                        if (manager.Player != null)
                        {
                            ChildList.Items.Add(manager.Player.Name);
                        }

                        break;
                    case "Consumables":

                        if (manager.Consumables.Count > 0)
                        {
                            foreach (Consumable auxConsumable in manager.Consumables)
                            {
                                ChildList.Items.Add(auxConsumable.Name);
                            }
                        }

                        break;
                    case "Doors & Keys":

                        if (manager.Doors.Count > 0)
                        {
                            foreach (Door auxDoor in manager.Doors)
                            {
                                string[] auxString = auxDoor.Name.Split('_');
                                string total = "";

                                for (int i = 0; i < auxString.Length - 1; i++)
                                {
                                    total += auxString[i];

                                    if (i + 1 != auxString.Length - 1)
                                    {
                                        total += "_";
                                    }
                                }

                                ChildList.Items.Add(total);
                            }
                        }

                        break;
                    case "Enemies":

                        if (manager.Enemies.Count > 0)
                        {
                            foreach (Enemy auxEnemy in manager.Enemies)
                            {
                                ChildList.Items.Add(auxEnemy.Name);
                            }
                        }

                        break;
                    case "NPCs":

                        if (manager.NonPlayers.Count > 0)
                        {
                            foreach (NPC auxNPC in manager.NonPlayers)
                            {
                                ChildList.Items.Add(auxNPC.Name);
                            }
                        }

                        break;
                    case "Walls":

                        if (manager.BgItems.Count > 0)
                        {
                            foreach (BackgroundItem auxBgItem in manager.BgItems)
                            {
                                ChildList.Items.Add(auxBgItem.Name);
                            }
                        }

                        break;
                    case "Weapons":

                        if (manager.Weapons.Count > 0)
                        {
                            foreach (Weapon auxWeapon in manager.Weapons)
                            {
                                ChildList.Items.Add(auxWeapon.Name);
                            }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Sets the general information text box and label when you choose an element from the ChildList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ChildList.SelectedIndex > -1)
                {

                    String selectedElement = (String)ChildList.SelectedItem;
                    ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

                    string selected = selectedItem.Content.ToString();

                
                    switch (selected)
                    {
                        case "Abilities":

                            Ability auxAbility = manager.GetAbility(selectedElement);

                            BitmapImage bmiAbility = new BitmapImage(new Uri(auxAbility.ImageAbility));
                            Image imAbility = new Image();
                            imAbility.Source = bmiAbility;

                            ResizeInfoLabel(bmiAbility);

                            LabelPreviewInfo.Content = imAbility;

                            GeneralInfoTextBox.Text = auxAbility.GetGeneralInfoString();

                            break;
                        case "Character":

                            BitmapImage bmiCharacter = new BitmapImage(new Uri(manager.Player.PreviewImage));
                            Image imCharacter = new Image();
                            imCharacter.Source = bmiCharacter;

                            ResizeInfoLabel(bmiCharacter);

                            LabelPreviewInfo.Content = imCharacter;

                            GeneralInfoTextBox.Text = manager.Player.GetGeneralInfoString();

                            break;
                        case "Consumables":

                            Consumable auxConsumable = manager.GetConsumable(selectedElement);

                            BitmapImage bmiConsumable = new BitmapImage(new Uri(auxConsumable.Image));
                            Image imConsumable = new Image();
                            imConsumable.Source = bmiConsumable;

                            ResizeInfoLabel(bmiConsumable);

                            LabelPreviewInfo.Content = imConsumable;

                            GeneralInfoTextBox.Text = auxConsumable.GetGeneralInfoString();

                            break;
                        case "Doors & Keys":

                            Door auxDoor = manager.GetDoor(selectedElement + "_Door");
                            KeyItem auxKey = manager.GetKey(selectedElement + "_Key");

                            BitmapImage bmiDoor = new BitmapImage(new Uri(auxDoor.Image));
                            Image imDoor = new Image();
                            imDoor.Source = bmiDoor;

                            ResizeInfoLabel(bmiDoor);

                            LabelPreviewInfo.Content = imDoor;

                            GeneralInfoTextBox.Text = auxDoor.GetGeneralInfoString();
                            GeneralInfoTextBox.Text += auxKey.GetGeneralInfoString();

                            break;
                        case "Enemies":

                            Enemy auxEnemy = manager.GetEnemy(selectedElement);

                            BitmapImage bmiEnemy = new BitmapImage(new Uri(auxEnemy.PreviewImage));
                            Image imEnemy = new Image();
                            imEnemy.Source = bmiEnemy;

                            ResizeInfoLabel(bmiEnemy);

                            LabelPreviewInfo.Content = imEnemy;

                            GeneralInfoTextBox.Text = auxEnemy.GetGeneralInfoString();

                            break;
                        case "NPCs":

                            NPC auxNPC = manager.GetNPC(selectedElement);

                            BitmapImage bmiNPC = new BitmapImage(new Uri(auxNPC.Image));
                            Image imNPC = new Image();
                            imNPC.Source = bmiNPC;

                            ResizeInfoLabel(bmiNPC);

                            LabelPreviewInfo.Content = imNPC;

                            GeneralInfoTextBox.Text = auxNPC.GetGeneralInfoString();

                            break;
                        case "Walls":

                            BackgroundItem auxBgItem = manager.GetBgItem(selectedElement);

                            BitmapImage bmiWall = new BitmapImage(new Uri(auxBgItem.Image));
                            Image imWall = new Image();
                            imWall.Source = bmiWall;

                            ResizeInfoLabel(bmiWall);

                            LabelPreviewInfo.Content = imWall;

                            GeneralInfoTextBox.Text = auxBgItem.GetGeneralInfoString();

                            break;
                        case "Weapons":

                            Weapon auxWeapon = manager.GetWeapon(selectedElement);

                            BitmapImage bmiWeapon = new BitmapImage(new Uri(auxWeapon.ImageWeapon));
                            Image imWeapon = new Image();
                            imWeapon.Source = bmiWeapon;

                            ResizeInfoLabel(bmiWeapon);

                            LabelPreviewInfo.Content = imWeapon;

                            GeneralInfoTextBox.Text = auxWeapon.GetGeneralInfoString();

                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The application couldn't show the information of the element.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Launchs a creation window depending on the category selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            textureSelected = null;

            try
            {
                ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

                string selected = selectedItem.Content.ToString();

                ResetInfoLabel();
                GeneralInfoTextBox.Text = "";

                ChildList.SelectedIndex = -1;

                switch (selected)
                {
                    case "Abilities":

                        AbilityWindow abilityWindow = new AbilityWindow(manager, false, null);
                        abilityWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Character":

                        CharacterWindow characterWindow = new CharacterWindow(manager, false);
                        characterWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Consumables":

                        ConsumableWindow consumableWindow = new ConsumableWindow(manager, false, null);
                        consumableWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Doors & Keys":

                        DoorKeyWindow doorKeyWindow = new DoorKeyWindow(manager, false, null, null);
                        doorKeyWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Enemies":

                        EnemyWindow enemyWindow = new EnemyWindow(manager, false, null);
                        enemyWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "NPCs":

                        NPCWindow npcWindow = new NPCWindow(manager, false, null);
                        npcWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Walls":

                        WallWindow wallWindoe = new WallWindow(manager, false, null);
                        wallWindoe.ShowDialog();
                        RefreshList();

                        break;
                    case "Weapons":

                        WeaponWindow weaponWindow = new WeaponWindow(manager, false, null);
                        weaponWindow.ShowDialog();
                        RefreshList();

                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No category selected.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Launch a creation window, with the "edit" option, depending on the category selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            textureSelected = null;

            try
            {
                if (ChildList.SelectedItem == null)
                {
                    throw new NullReferenceException();
                }

                String selectedElement = (String)ChildList.SelectedItem;
                ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

                string selected = selectedItem.Content.ToString();

                ResetInfoLabel();
                GeneralInfoTextBox.Text = "";

                switch (selected)
                {
                    case "Abilities":

                        AbilityWindow abilityWindow = new AbilityWindow(manager, true, manager.GetAbility(selectedElement));
                        abilityWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Character":

                        CharacterWindow characterWindow = new CharacterWindow(manager, true);
                        characterWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Consumables":

                        ConsumableWindow consumableWindow = new ConsumableWindow(manager, true, manager.GetConsumable(selectedElement));
                        consumableWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Doors & Keys":

                        DoorKeyWindow doorKeyWindow = new DoorKeyWindow(manager, true, manager.GetDoor(selectedElement + "_Door"), manager.GetKey(selectedElement + "_Key"));
                        doorKeyWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Enemies":

                        EnemyWindow enemyWindow = new EnemyWindow(manager, true, manager.GetEnemy(selectedElement));
                        enemyWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "NPCs":

                        NPCWindow npcWindow = new NPCWindow(manager, true, manager.GetNPC(selectedElement));
                        npcWindow.ShowDialog();
                        RefreshList();

                        break;
                    case "Walls":

                        WallWindow wallWindoe = new WallWindow(manager, true, manager.GetBgItem(selectedElement));
                        wallWindoe.ShowDialog();
                        RefreshList();

                        break;
                    case "Weapons":

                        WeaponWindow weaponWindow = new WeaponWindow(manager, true, manager.GetWeapon(selectedElement));
                        weaponWindow.ShowDialog();
                        RefreshList();

                        break;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No element selected.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("No category selected.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Deletes the selected element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChildList.SelectedItem == null)
                {
                    throw new NullReferenceException();
                }

                String selectedElement = (String)ChildList.SelectedItem;
                ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

                string selected = selectedItem.Content.ToString();

                ResetInfoLabel();
                GeneralInfoTextBox.Text = "";

                switch (selected)
                {
                    case "Abilities":

                        manager.Abilities.Remove(manager.GetAbility(selectedElement));
                        manager.Abilities.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "Character":

                        manager.Player = null;
                        RefreshList();

                        break;
                    case "Consumables":

                        manager.Consumables.Remove(manager.GetConsumable(selectedElement));
                        manager.Consumables.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "Doors & Keys":

                        manager.Doors.Remove(manager.GetDoor(selectedElement + "_Door"));
                        manager.Keys.Remove(manager.GetKey(selectedElement + "_Key"));
                        manager.Doors.OrderBy(x => x.Name);
                        manager.Keys.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "Enemies":

                        manager.Enemies.Remove(manager.GetEnemy(selectedElement));
                        manager.Enemies.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "NPCs":

                        manager.NonPlayers.Remove(manager.GetNPC(selectedElement));
                        manager.NonPlayers.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "Walls":

                        manager.BgItems.Remove(manager.GetBgItem(selectedElement));
                        manager.BgItems.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                    case "Weapons":

                        manager.Weapons.Remove(manager.GetWeapon(selectedElement));
                        manager.Weapons.OrderBy(x => x.Name);
                        RefreshList();

                        break;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No element selected.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("No category selected.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Refresh the ChildList when any change occurs.
        /// </summary>
        private void RefreshList()
        {
            ListBoxItem selectedItem = (ListBoxItem)MainList.SelectedItem;

            string selected = selectedItem.Content.ToString();

            ChildList.Items.Clear();

            switch (selected)
            {
                case "Abilities":

                    if (manager.Abilities.Count > 0)
                    {
                        foreach (Ability auxAbility in manager.Abilities)
                        {
                            ChildList.Items.Add(auxAbility.Name);
                        }
                    }

                    break;
                case "Character":

                    if (manager.Player != null)
                    {
                        ChildList.Items.Add(manager.Player.Name);
                    }

                    break;
                case "Consumables":

                    if (manager.Consumables.Count > 0)
                    {
                        foreach (Consumable auxConsumable in manager.Consumables)
                        {
                            ChildList.Items.Add(auxConsumable.Name);
                        }
                    }

                    break;
                case "Doors & Keys":

                    if (manager.Doors.Count > 0)
                    {
                        foreach (Door auxDoor in manager.Doors)
                        {
                            string[] auxString = auxDoor.Name.Split('_');
                            string total = "";

                            for (int i = 0; i < auxString.Length - 1; i++)
                            {
                                total += auxString[i];

                                if (i + 1 != auxString.Length - 1)
                                {
                                    total += "_";
                                }
                            }

                            ChildList.Items.Add(total);
                        }
                    }

                    break;
                case "Enemies":

                    if (manager.Enemies.Count > 0)
                    {
                        foreach (Enemy auxEnemy in manager.Enemies)
                        {
                            ChildList.Items.Add(auxEnemy.Name);
                        }
                    }

                    break;
                case "NPCs":

                    if (manager.NonPlayers.Count > 0)
                    {
                        foreach (NPC auxNPC in manager.NonPlayers)
                        {
                            ChildList.Items.Add(auxNPC.Name);
                        }
                    }

                    break;
                case "Walls":

                    if (manager.BgItems.Count > 0)
                    {
                        foreach (BackgroundItem auxBgItem in manager.BgItems)
                        {
                            ChildList.Items.Add(auxBgItem.Name);
                        }
                    }

                    break;
                case "Weapons":

                    if (manager.Weapons.Count > 0)
                    {
                        foreach (Weapon auxWeapon in manager.Weapons)
                        {
                            ChildList.Items.Add(auxWeapon.Name);
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Resets the general information label.
        /// </summary>
        private void ResetInfoLabel()
        {
            LabelPreviewInfo.Height = 250;
            LabelPreviewInfo.Width = 250;

            LabelPreviewInfo.Content = null;
        }

        /// <summary>
        /// Resize the general information label, depending on the size of the selected image.
        /// </summary>
        /// <param name="bmi">Image</param>
        private void ResizeInfoLabel(BitmapImage bmi)
        {
            if (LabelPreviewInfo.MinHeight < bmi.Height)
            {
                LabelPreviewInfo.Height = bmi.Height + 20;
            }
            else
            {
                LabelPreviewInfo.Height = LabelPreviewInfo.MinHeight;
            }

            if (LabelPreviewInfo.MinWidth < bmi.Width)
            {
                LabelPreviewInfo.Width = bmi.Width + 20;
            }
            else
            {
                LabelPreviewInfo.Width = LabelPreviewInfo.MinWidth;
            }
        }

        /// <summary>
        /// Lets you create the first level, and modify any level after the first is created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonModifyStage_Click(object sender, RoutedEventArgs e)
        {
            if ((string)ButtonModifyStage.Content == "Create")
            {
                StageWindow stageWindow = new StageWindow(manager, false, null);
                stageWindow.ShowDialog();

                if (manager.ID == 0)
                {
                    currentID = 0;
                    ButtonModifyStage.Content = "Modify";

                    LoadStageCanvas();
                }
            }
            else
            {
                StageWindow stageWindow = new StageWindow(manager, true, manager.GetStage(currentID));
                stageWindow.ShowDialog();

                LoadStageCanvas();
            }
        }        
       
        /// <summary>
        /// Fills the ListItems when you select a category from teh CategoriesList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCategories.SelectedIndex > -1)
            {
                ListBoxItem selectedItem = (ListBoxItem)ListCategories.SelectedItem;

                string selected = selectedItem.Content.ToString();

                ResetSetLabel();

                ListItems.SelectedIndex = -1;
                ListItems.Items.Clear();

                textureSelected = null;

                switch (selected)
                {
                    case "Abilities":

                        if (manager.Abilities.Count > 0)
                        {
                            foreach (Ability auxAbility in manager.Abilities)
                            {
                                ListItems.Items.Add(auxAbility.Name);
                            }
                        }

                        break;
                    case "Character":

                        if (manager.Player != null)
                        {
                            ListItems.Items.Add(manager.Player.Name);
                        }

                        break;
                    case "Consumables":

                        if (manager.Consumables.Count > 0)
                        {
                            foreach (Consumable auxConsumable in manager.Consumables)
                            {
                                ListItems.Items.Add(auxConsumable.Name);
                            }
                        }

                        break;
                    case "Doors & Keys":

                        if (manager.Doors.Count > 0)
                        {
                            foreach (Door auxDoor in manager.Doors)
                            {
                                ListItems.Items.Add(auxDoor.Name);
                            }
                        }
                        if (manager.Keys.Count > 0)
                        {
                            foreach (KeyItem auxKey in manager.Keys)
                            {
                                ListItems.Items.Add(auxKey.Name);
                            }
                        }

                        break;
                    case "Enemies":

                        if (manager.Enemies.Count > 0)
                        {
                            foreach (Enemy auxEnemy in manager.Enemies)
                            {
                                ListItems.Items.Add(auxEnemy.Name);
                            }
                        }

                        break;
                    case "NPCs":

                        if (manager.NonPlayers.Count > 0)
                        {
                            foreach (NPC auxNPC in manager.NonPlayers)
                            {
                                ListItems.Items.Add(auxNPC.Name);
                            }
                        }

                        break;
                    case "Walls":

                        if (manager.BgItems.Count > 0)
                        {
                            foreach (BackgroundItem auxBgItem in manager.BgItems)
                            {
                                ListItems.Items.Add(auxBgItem.Name);
                            }
                        }

                        break;
                    case "Weapons":

                        if (manager.Weapons.Count > 0)
                        {
                            foreach (Weapon auxWeapon in manager.Weapons)
                            {
                                ListItems.Items.Add(auxWeapon.Name);
                            }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Gets the texture of the selected element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ListItems.SelectedIndex > -1)
                {
                    String selectedElement = (String)ListItems.SelectedItem;
                    ListBoxItem selectedItem = (ListBoxItem)ListCategories.SelectedItem;

                    string selected = selectedItem.Content.ToString();

                    ResetSetLabel();
                
                    textureSelected = new Image();
                    textureSelected.Name = selectedElement;

                    switch (selected)
                    {
                        case "Character":

                            BitmapImage bmiCharacter = new BitmapImage(new Uri(manager.Player.PreviewImage));
                            Image imageForSetCharacter = new Image();

                            imageForSetCharacter.Source = bmiCharacter;

                            textureSelected.Source = bmiCharacter;
                            textureSelected.Height = bmiCharacter.Height;
                            textureSelected.Width = bmiCharacter.Width;

                            textureSelected.Uid = "Character";

                            ResizeSetLabel(bmiCharacter);

                            LabelSetInfo.Content = imageForSetCharacter;

                            break;
                        case "Consumables":

                            Consumable auxConsumable = manager.GetConsumable(selectedElement);

                            BitmapImage bmiConsumable = new BitmapImage(new Uri(auxConsumable.Image));
                            Image imageForSetConsumable = new Image();

                            imageForSetConsumable.Source = bmiConsumable;

                            textureSelected.Source = bmiConsumable;
                            textureSelected.Height = bmiConsumable.Height;
                            textureSelected.Width = bmiConsumable.Width;

                            textureSelected.Uid = "Consumable";

                            ResizeSetLabel(bmiConsumable);

                            LabelSetInfo.Content = imageForSetConsumable;

                            break;
                        case "Doors & Keys":

                            Door auxDoor = null;
                            KeyItem auxKey = null;

                            if (manager.GetDoor(selectedElement) == null)
                            {
                                auxKey = manager.GetKey(selectedElement);

                                BitmapImage bmiKey = new BitmapImage(new Uri(auxKey.Image));
                                Image imageForSetKey = new Image();

                                imageForSetKey.Source = bmiKey;

                                textureSelected.Source = bmiKey;
                                textureSelected.Height = bmiKey.Height;
                                textureSelected.Width = bmiKey.Width;

                                textureSelected.Uid = "Key";

                                ResizeSetLabel(bmiKey);

                                LabelSetInfo.Content = imageForSetKey;
                            }
                            else
                            {
                                auxDoor = manager.GetDoor(selectedElement);

                                BitmapImage bmiDoor = new BitmapImage(new Uri(auxDoor.Image));
                                Image imageForSetDoor = new Image();

                                imageForSetDoor.Source = bmiDoor;

                                textureSelected.Source = bmiDoor;
                                textureSelected.Height = bmiDoor.Height;
                                textureSelected.Width = bmiDoor.Width;

                                textureSelected.Uid = "Door";

                                ResizeSetLabel(bmiDoor);

                                LabelSetInfo.Content = imageForSetDoor;
                            }

                            break;
                        case "Enemies":

                            Enemy auxEnemy = manager.GetEnemy(selectedElement);

                            BitmapImage bmiEnemy = new BitmapImage(new Uri(auxEnemy.PreviewImage));
                            Image imageForSetEnemy = new Image();

                            imageForSetEnemy.Source = bmiEnemy;

                            textureSelected.Source = bmiEnemy;
                            textureSelected.Height = bmiEnemy.Height;
                            textureSelected.Width = bmiEnemy.Width;

                            textureSelected.Uid = "Enemy";

                            ResizeSetLabel(bmiEnemy);

                            LabelSetInfo.Content = imageForSetEnemy;

                            break;
                        case "NPCs":

                            NPC auxNPC = manager.GetNPC(selectedElement);

                            BitmapImage bmiNPC = new BitmapImage(new Uri(auxNPC.Image));
                            Image imageForSetNPC = new Image();

                            imageForSetNPC.Source = bmiNPC;

                            textureSelected.Source = bmiNPC;
                            textureSelected.Height = bmiNPC.Height;
                            textureSelected.Width = bmiNPC.Width;

                            textureSelected.Uid = "NPC";

                            ResizeSetLabel(bmiNPC);

                            LabelSetInfo.Content = imageForSetNPC;

                            break;
                        case "Walls":

                            BackgroundItem auxBgItem = manager.GetBgItem(selectedElement);

                            BitmapImage bmiWall = new BitmapImage(new Uri(auxBgItem.Image));
                            Image imageForSetWall = new Image();

                            imageForSetWall.Source = bmiWall;

                            textureSelected.Source = bmiWall;
                            textureSelected.Height = bmiWall.Height;
                            textureSelected.Width = bmiWall.Width;

                            textureSelected.Uid = "Wall";

                            ResizeSetLabel(bmiWall);

                            LabelSetInfo.Content = imageForSetWall;

                            break;
                        case "Weapons":

                            Weapon auxWeapon = manager.GetWeapon(selectedElement);

                            BitmapImage bmiWeapon = new BitmapImage(new Uri(auxWeapon.ImageWeapon));
                            Image imageForSetWeapon = new Image();

                            imageForSetWeapon.Source = bmiWeapon;

                            textureSelected.Source = bmiWeapon;
                            textureSelected.Height = bmiWeapon.Height;
                            textureSelected.Width = bmiWeapon.Width;

                            textureSelected.Uid = "Weapon";

                            ResizeSetLabel(bmiWeapon);

                            LabelSetInfo.Content = imageForSetWeapon;

                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocurred when selecting the element.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creates an stage, north of the current stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentID < 0)
                {
                    throw new Exception();
                }

                if ((string)ButtonUpStage.Content == "↑")
                {
                    currentID = manager.GetStage(currentID).ExitUp;

                    CheckExits();

                    LoadStageCanvas();
                }
                else
                {
                    int previousID = manager.ID;

                    StageWindow stageWindow = new StageWindow(manager, false, null);
                    stageWindow.ShowDialog();

                    if (previousID != manager.ID)
                    {
                        Stage previous = manager.GetStage(currentID);

                        previous.ExitUp = manager.ID;

                        Stage current = manager.GetStage(manager.ID);

                        current.ExitDown = currentID;

                        currentID = manager.ID;

                        LoadStageCanvas();

                        CheckExits();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You have to create the first stage.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Creates an stage, west of the current stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLeftStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentID < 0)
                {
                    throw new Exception();
                }

                if ((string)ButtonLeftStage.Content == "←")
                {
                    currentID = manager.GetStage(currentID).ExitLeft;

                    CheckExits();

                    LoadStageCanvas();
                }
                else
                {
                    int previousID = manager.ID;

                    StageWindow stageWindow = new StageWindow(manager, false, null);
                    stageWindow.ShowDialog();

                    if (previousID != manager.ID)
                    {

                        Stage previous = manager.GetStage(currentID);

                        previous.ExitLeft = manager.ID;

                        Stage current = manager.GetStage(manager.ID);

                        current.ExitRight = currentID;

                        currentID = manager.ID;

                        LoadStageCanvas();

                        CheckExits();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You have to create the first stage.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Creates an stage, east of the current stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRightStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentID < 0)
                {
                    throw new Exception();
                }

                if ((string)ButtonRightStage.Content == "→")
                {
                    currentID = manager.GetStage(currentID).ExitRight;

                    CheckExits();

                    LoadStageCanvas();
                }
                else
                {
                    int previousID = manager.ID;

                    StageWindow stageWindow = new StageWindow(manager, false, null);
                    stageWindow.ShowDialog();

                    if (previousID != manager.ID)
                    {
                        Stage previous = manager.GetStage(currentID);

                        previous.ExitRight = manager.ID;

                        Stage current = manager.GetStage(manager.ID);

                        current.ExitLeft = currentID;

                        currentID = manager.ID;

                        LoadStageCanvas();

                        CheckExits();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You have to create the first stage.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Creates an stage, south of the current stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDownStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentID < 0)
                {
                    throw new Exception();
                }

                if ((string)ButtonDownStage.Content == "↓")
                {
                    currentID = manager.GetStage(currentID).ExitDown;

                    CheckExits();

                    LoadStageCanvas();
                }
                else
                {
                    int previousID = manager.ID;

                    StageWindow stageWindow = new StageWindow(manager, false, null);
                    stageWindow.ShowDialog();

                    if (previousID != manager.ID)
                    {
                        Stage previous = manager.GetStage(currentID);

                        previous.ExitDown = manager.ID;

                        Stage current = manager.GetStage(manager.ID);

                        current.ExitUp = currentID;

                        currentID = manager.ID;

                        LoadStageCanvas();

                        CheckExits();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You have to create the first stage.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Checks if there is any exit from the current stage and changes the content of the exits buttons.
        /// </summary>
        private void CheckExits()
        {
            Stage aux = manager.GetStage(currentID);

            if (aux.ExitUp > -1)
            {
                ButtonUpStage.Content = "↑";
            }
            else
            {
                ButtonUpStage.Content = "+";
            }

            if (aux.ExitDown > -1)
            {
                ButtonDownStage.Content = "↓";
            }
            else
            {
                ButtonDownStage.Content = "+";
            }

            if (aux.ExitLeft > -1)
            {
                ButtonLeftStage.Content = "←";
            }
            else
            {
                ButtonLeftStage.Content = "+";
            }

            if (aux.ExitRight > -1)
            {
                ButtonRightStage.Content = "→";
            }
            else
            {
                ButtonRightStage.Content = "+";
            }
        }

        /// <summary>
        /// Inserts the selected element in the stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasGame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string errorMessage = "An error ocurred when introducing the element.";

            try
            {
                if (currentID < 0)
                {
                    throw new NullReferenceException();
                }

                if (textureSelected != null)
                {
                    if (textureSelected.Uid == "Eraser")
                    {
                        CanvasGame_MouseRightButtonDown(sender, e);
                    }

                    textureCanvas = new Image();

                    textureCanvas.Source = textureSelected.Source;
                    textureCanvas.Width = textureSelected.Width;
                    textureCanvas.Height = textureSelected.Height;
                    textureCanvas.Uid = textureSelected.Uid;
                    textureCanvas.Name = textureSelected.Name;

                    Point pos = Mouse.GetPosition(CanvasGame);

                    pos.X -= textureCanvas.Width;
                    pos.Y -= textureCanvas.Height;

                    if (pos.X  < 0)
                    {
                        pos.X = 0;
                    }

                    if (pos.Y < 0)
                    {
                        pos.Y = 0;
                    }

                    //if ((pos.X + textureCanvas.Width) > 640)
                    //{
                    //    pos.X = 640 - textureCanvas.Width;
                    //}

                    //if ((pos.Y + textureCanvas.Height) > 380)
                    //{
                    //    pos.Y = 380 - textureCanvas.Height;
                    //}

                    pos.X = Math.Round(pos.X, 2);
                    pos.Y = Math.Round(pos.Y, 2);

                    Stage currentStage = manager.GetStage(currentID);

                    if (textureCanvas.Uid == "Character")
                    {
                        if (manager.Player.IsSet == true)
                        {
                            errorMessage = "The main character is already set.";
                            throw new Exception();
                        }

                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        currentStage.Player = manager.Player;

                        currentStage.Player.PositionX = (int)pos.X;
                        currentStage.Player.PositionY = (int)pos.Y;
                        currentStage.Player.CurrentImage = textureCanvas;

                        manager.Player.IsSet = true;
                    }
                    else if (textureCanvas.Uid == "Consumable")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        Consumable consumableStage = manager.GetConsumable(textureCanvas.Name);
                        Consumable newConsumable = new Consumable(consumableStage.Name, consumableStage.Type, consumableStage.Effect, consumableStage.Image);

                        newConsumable.PositionX = (int)pos.X;
                        newConsumable.PositionY = (int)pos.Y;
                        newConsumable.CurrentImage = textureCanvas;

                        currentStage.Consumables.Add(newConsumable);
                    }
                    else if (textureCanvas.Uid == "Weapon")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        Weapon weaponStage = manager.GetWeapon(textureCanvas.Name);
                        Weapon newWeapon = new Weapon(weaponStage.Name, weaponStage.Type, weaponStage.Damage, weaponStage.Speed, weaponStage.ImageWeapon, 
                            weaponStage.ImageArrowUp, weaponStage.ImageArrowDown, weaponStage.ImageArrowLeft, weaponStage.ImageArrowRight);

                        newWeapon.PositionX = (int)pos.X;
                        newWeapon.PositionY = (int)pos.Y;
                        newWeapon.CurrentImage = textureCanvas;

                        currentStage.Weapons.Add(newWeapon);
                    }
                    else if (textureCanvas.Uid == "Enemy")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        Enemy enemyStage = manager.GetEnemy(textureCanvas.Name);
                        Enemy newEnemy = new Enemy(enemyStage.Name, enemyStage.Strenght, enemyStage.Intelligence, enemyStage.Dexterity, enemyStage.HitPoints, 
                            enemyStage.ImageMovingUp, enemyStage.ImageMovingDown, enemyStage.ImageMovingLeft, enemyStage.ImageMovingRight, enemyStage.FrameCount, 
                            enemyStage.ImageArrowUp, enemyStage.ImageArrowDown, enemyStage.ImageArrowLeft, enemyStage.ImageArrowRight, enemyStage.Loot,
                            enemyStage.Behavoir, enemyStage.Preference, enemyStage.Boss, enemyStage.InitialState, enemyStage.PatrolZone, enemyStage.DetectZone, 
                            enemyStage.Speed);

                        newEnemy.PositionX = (int)pos.X;
                        newEnemy.PositionY = (int)pos.Y;
                        newEnemy.CurrentImage = textureCanvas;

                        currentStage.Enemies.Add(newEnemy);
                    }
                    else if (textureCanvas.Uid == "NPC")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        NPC npcStage = manager.GetNPC(textureCanvas.Name);
                        NPC newNPC = new NPC(npcStage.Name, npcStage.Image, npcStage.DialogImage);

                        newNPC.PositionX = (int)pos.X;
                        newNPC.PositionY = (int)pos.Y;
                        newNPC.CurrentImage = textureCanvas;

                        currentStage.NonPlayers.Add(newNPC);
                    }
                    else if (textureCanvas.Uid == "Wall")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        BackgroundItem wallStage = manager.GetBgItem(textureCanvas.Name);
                        BackgroundItem newWall = new BackgroundItem(wallStage.Name, wallStage.Solid, wallStage.BulletProof, wallStage.Image);

                        newWall.PositionX = (int)pos.X;
                        newWall.PositionY = (int)pos.Y;
                        newWall.CurrentImage = textureCanvas;

                        currentStage.BgItems.Add(newWall);
                    }
                    else if (textureCanvas.Uid == "Key")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        KeyItem keyStage = manager.GetKey(textureCanvas.Name);
                        KeyItem newKey = new KeyItem(keyStage.Name, keyStage.Image, keyStage.ID);

                        newKey.PositionX = (int)pos.X;
                        newKey.PositionY = (int)pos.Y;
                        newKey.CurrentImage = textureCanvas;

                        currentStage.Keys.Add(newKey);
                    }
                    else if (textureCanvas.Uid == "Door")
                    {
                        CanvasGame.Children.Add(textureCanvas);

                        Canvas.SetLeft(textureCanvas, pos.X);
                        Canvas.SetTop(textureCanvas, pos.Y);

                        Door doorStage = manager.GetDoor(textureCanvas.Name);
                        Door newDoor = new Door(doorStage.Name, doorStage.Image, doorStage.ID);

                        newDoor.PositionX = (int)pos.X;
                        newDoor.PositionY = (int)pos.Y;
                        newDoor.CurrentImage = textureCanvas;

                        currentStage.Doors.Add(newDoor);
                    }
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("You have to create a stage first.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(errorMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /// <summary>
        /// Loads the stage when you change the current stage.
        /// </summary>
        private void LoadStageCanvas()
        {
            if (CanvasGame.Children.Count > 0)
            {
                CanvasGame.Children.Clear();
            }

            Stage currentStage = manager.GetStage(currentID);

            BitmapImage bmi = new BitmapImage(new Uri(currentStage.Background));
            Image im = new Image();
            im.Source = bmi;

            CanvasGame.Children.Add(im);

            if (currentStage.Player != null)
            {
                CanvasGame.Children.Add(currentStage.Player.CurrentImage);

                Canvas.SetLeft(currentStage.Player.CurrentImage, currentStage.Player.PositionX);
                Canvas.SetTop(currentStage.Player.CurrentImage, currentStage.Player.PositionY);
            }

            foreach (Enemy auxEnemy in currentStage.Enemies)
            {
                CanvasGame.Children.Add(auxEnemy.CurrentImage);

                Canvas.SetLeft(auxEnemy.CurrentImage, auxEnemy.PositionX);
                Canvas.SetTop(auxEnemy.CurrentImage, auxEnemy.PositionY);
            }

            foreach (BackgroundItem auxBg in currentStage.BgItems)
            {
                CanvasGame.Children.Add(auxBg.CurrentImage);

                Canvas.SetLeft(auxBg.CurrentImage, auxBg.PositionX);
                Canvas.SetTop(auxBg.CurrentImage, auxBg.PositionY);
            }

            foreach (Weapon auxWeapon in currentStage.Weapons)
            {
                CanvasGame.Children.Add(auxWeapon.CurrentImage);

                Canvas.SetLeft(auxWeapon.CurrentImage, auxWeapon.PositionX);
                Canvas.SetTop(auxWeapon.CurrentImage, auxWeapon.PositionY);
            }

            foreach (NPC auxNPC in currentStage.NonPlayers)
            {
                CanvasGame.Children.Add(auxNPC.CurrentImage);

                Canvas.SetLeft(auxNPC.CurrentImage, auxNPC.PositionX);
                Canvas.SetTop(auxNPC.CurrentImage, auxNPC.PositionY);
            }

            foreach (Consumable auxConsumable in currentStage.Consumables)
            {
                CanvasGame.Children.Add(auxConsumable.CurrentImage);

                Canvas.SetLeft(auxConsumable.CurrentImage, auxConsumable.PositionX);
                Canvas.SetTop(auxConsumable.CurrentImage, auxConsumable.PositionY);
            }

            foreach (KeyItem auxKey in currentStage.Keys)
            {
                CanvasGame.Children.Add(auxKey.CurrentImage);

                Canvas.SetLeft(auxKey.CurrentImage, auxKey.PositionX);
                Canvas.SetTop(auxKey.CurrentImage, auxKey.PositionY);
            }

            foreach (Door auxDoor in currentStage.Doors)
            {
                CanvasGame.Children.Add(auxDoor.CurrentImage);

                Canvas.SetLeft(auxDoor.CurrentImage, auxDoor.PositionX);
                Canvas.SetTop(auxDoor.CurrentImage, auxDoor.PositionY);
            }
        }

        /// <summary>
        /// Resets the general information label of the stages tab.
        /// </summary>
        private void ResetSetLabel()
        {
            LabelSetInfo.Height = 200;
            LabelSetInfo.Width = 200;

            LabelSetInfo.Content = null;
        }

        /// <summary>
        /// Resizes the general information label of the stages tab, depending on the image to be load.
        /// </summary>
        /// <param name="bmi">Image</param>
        private void ResizeSetLabel(BitmapImage bmi)
        {
            if (LabelSetInfo.MinHeight < bmi.Height)
            {
                LabelSetInfo.Height = bmi.Height + 20;
            }
            else
            {
                LabelSetInfo.Height = LabelSetInfo.MinHeight;
            }

            if (LabelSetInfo.MinWidth < bmi.Width)
            {
                LabelSetInfo.Width = bmi.Width + 20;
            }
            else
            {
                LabelSetInfo.Width = LabelSetInfo.MinWidth;
            }
        }

        /// <summary>
        /// Selects the eraser. Lets you erase element from the stage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            textureSelected = new Image();

            ListItems.SelectedIndex = -1;

            BitmapImage bmi = new BitmapImage(new Uri("../../borrar.png", UriKind.Relative));
            Image imForSet = new Image();

            imForSet.Source = bmi;

            textureSelected.Source = bmi;
            textureSelected.Uid = "Eraser";

            ResizeSetLabel(bmi);

            LabelSetInfo.Content = imForSet;
        }

        /// <summary>
        /// Erase an element from the stage, when you right click in the canvas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasGame_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (currentID < 0)
                {
                    throw new NullReferenceException();
                }

                Stage currentStage = manager.GetStage(currentID);

                if (currentStage.Player != null && currentStage.Player.CurrentImage.IsMouseOver)
                {
                    CanvasGame.Children.Remove(currentStage.Player.CurrentImage);
                    currentStage.Player = null;

                    manager.Player.IsSet = false;

                    return;
                }

                foreach (Enemy auxEnemy in currentStage.Enemies)
                {
                    if (auxEnemy.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxEnemy.CurrentImage);

                        currentStage.Enemies.Remove(auxEnemy);
                        return;
                    }
                }

                foreach (Door auxDoor in currentStage.Doors)
                {
                    if (auxDoor.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxDoor.CurrentImage);

                        currentStage.Doors.Remove(auxDoor);
                        return;
                    }
                }

                foreach (KeyItem auxKey in currentStage.Keys)
                {
                    if (auxKey.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxKey.CurrentImage);

                        currentStage.Keys.Remove(auxKey);
                        return;
                    }
                }

                foreach (NPC auxNPC in currentStage.NonPlayers)
                {
                    if (auxNPC.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxNPC.CurrentImage);

                        currentStage.NonPlayers.Remove(auxNPC);
                        return;
                    }
                }

                foreach (BackgroundItem auxBgItem in currentStage.BgItems)
                {
                    if (auxBgItem.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxBgItem.CurrentImage);

                        currentStage.BgItems.Remove(auxBgItem);
                        return;
                    }
                }

                foreach (Consumable auxConsumable in currentStage.Consumables)
                {
                    if (auxConsumable.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxConsumable.CurrentImage);

                        currentStage.Consumables.Remove(auxConsumable);
                        return;
                    }
                }

                foreach (Weapon auxWeapon in currentStage.Weapons)
                {
                    if (auxWeapon.CurrentImage.IsMouseOver)
                    {
                        CanvasGame.Children.Remove(auxWeapon.CurrentImage);

                        currentStage.Weapons.Remove(auxWeapon);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocurred while deleting the element.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Restart the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mgResult = MessageBox.Show("If you start a new project all the work will be lost.\n Are you sure you want to create a new project?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (mgResult == MessageBoxResult.Yes)
            {
                currentID = -1;
                textureSelected = null;
                textureCanvas = null;

                ListCategories.SelectedIndex = -1;
                MainList.SelectedIndex = -1;

                ChildList.Items.Clear();
                ListItems.Items.Clear();

                ResetInfoLabel();
                GeneralInfoTextBox.Text = null;
                CanvasGame.Children.Clear();
                manager = new MainWindowManager();
            }
        }

        /// <summary>
        /// Opens the CreateProjectWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchProject_Click(object sender, RoutedEventArgs e)
        {
            int bosses = 0;
            string pathFolderGame;
            string pathTemplateProject;
            bool playerFound = false; ;

            if(manager.Stages.Count == 0)
            {
                MessageBox.Show("Could not launch the game. You have to create at least one stage.", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            foreach(Stage stage in manager.Stages)
            {
                if (stage.Player != null)
                {
                    playerFound = true;
                }

                foreach(Enemy enemy in stage.Enemies)
                {
                    if(enemy.Boss == "true")
                    {
                        bosses++;
                    }
                }
            }

            if (!playerFound)
            {
                MessageBox.Show("Could not launch the game. Character is missing in the stages.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(bosses == 0)
            {
                MessageBox.Show("Could not launch the game. The game needs at least one final boss.", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            CreateProjectWindow launchProject = new CreateProjectWindow();

            if(launchProject.ShowDialog() == true)
            {
                try
                {
                    if (String.IsNullOrEmpty(launchProject.PathNewGame) || String.IsNullOrEmpty(launchProject.PathTemplateProject))
                    {
                        throw new NullReferenceException();
                    }
                    else
                    {
                        pathFolderGame = launchProject.PathNewGame;
                        pathTemplateProject = launchProject.PathTemplateProject;

                        pathFolderGame = pathFolderGame.Replace("\\\\", "\\");
                        pathTemplateProject = pathTemplateProject.Replace("\\\\", "\\");
                    }

                    TextWriter tw = new StreamWriter("game.xml");
                    tw.Write(//Header
                        "<?xml version=" + "\"" + "1.0" + "\"" + " encoding=" + "\"" + "utf-8" + "\"" + "?>\r\n" +
                        "<!DOCTYPE game SYSTEM " + "\"" + "dsl.dtd" + "\"" + ">\r\n" +
                        "<game>\r\n");

                    tw.Write(manager.Player.ToString(manager));

                    tw.Write(manager.ToString());

                    foreach (Stage stage in manager.Stages)
                    {
                        tw.Write(stage.ToString(manager));
                        tw.Flush();
                    }

                    tw.Write("</game>");
                    tw.Close();

                    string pathXML = Environment.CurrentDirectory;
                    pathXML = pathXML.Replace("\\\\", "\\");

                    //Launch Analyzer        
                    try
                    {
                        string path = Environment.CurrentDirectory.Replace("Interface\\bin\\Debug", "Analyzer\\bin\\Debug\\Analyzer.exe");
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(path);
                        //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                        startInfo.Arguments = "\"" + System.IO.Path.Combine(pathXML, "game.xml") + "\"" + " " + "\"" + pathTemplateProject + "\"" + " " + "\"" + pathFolderGame + "\"";

                        System.Diagnostics.Process.Start(startInfo);

                        MessageBox.Show("Game created!.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (InvalidDataException de)
                    {
                        MessageBox.Show("Entry point has not been found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch(NullReferenceException)
                {
                    MessageBox.Show("You must complete both paths.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}

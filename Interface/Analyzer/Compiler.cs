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
using System.Xml;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO.IsolatedStorage;

namespace Analyzer
{
    /// <summary>
    /// This class compiles the DSL into the selected Template.
    /// </summary>
    class Compiler
    {
        private string contents;
        private string sourceFolder;
        private string destFolder;
        private string fileNameCS;

        private StreamWriter streamWriter;

        private XmlDocument doc;    

        #region Definitions

        //Character
        private string character_name;
        private string character_image_up;
        private string character_image_down;
        private string character_image_left;
        private string character_image_right;
        private string character_image_attack_up;
        private string character_image_attack_down;
        private string character_image_attack_left;
        private string character_image_attack_right;
        private string character_framecount_iddle;
        private string character_framecount_attacking;
        private string character_initial_state;
        private string character_x_position;
        private string character_y_position;
        private string character_speed;
        private string character_key_left;
        private string character_key_up;
        private string character_key_down;
        private string character_key_right;
        private string character_key_attack;
        private string character_key_shoot;
        private string character_key_cast;
        private string character_key_change_melee;
        private string character_key_change_distance;
        private string character_key_change_ability;
        private string character_key_action;
        private string character_music_attack;
        private string character_music_shoot;
        private string character_music_cast;
        private string character_strenght;
        private string character_dexterity;
        private string character_intelligence;
        private string character_hit_points;
        private string character_mana_points;

        //Window
        private string window_background;
        private string window_background_music;
        private string window_id;
        private string window_north;
        private string window_east;
        private string window_south;
        private string window_west;

        //Enemy
        private string enemy_name;
        private string enemy_image_up;
        private string enemy_image_down;
        private string enemy_image_left;
        private string enemy_image_right;
        private string enemy_bullet_image_up;
        private string enemy_bullet_image_down;
        private string enemy_bullet_image_left;
        private string enemy_bullet_image_right;
        private string enemy_initial_state;
        private string enemy_framecount;
        private string enemy_x_position;
        private string enemy_y_position;
        private string enemy_speed;
        private string enemy_bullet_speed;
        private string enemy_strenght;
        private string enemy_dexterity;
        private string enemy_intelligence;
        private string enemy_hit_points;
        private string enemy_ia;
        private string enemy_preference;
        private string enemy_boss;
        private string enemy_detect_zone;
        private string enemy_patrol_zone;

        //Wall
        private string wall_name;
        private string wall_image;
        private string wall_x_position;
        private string wall_y_position;
        private string wall_solid;
        private string wall_bullet_proof;

        //NPC
        private string npc_image;
        private string npc_dialog;
        private string npc_x_position;
        private string npc_y_position;

        //Door
        private string door_image;
        private string door_x_position;
        private string door_y_position;
        private string door_id;

        //Key
        private string key_image;
        private string key_x_position;
        private string key_y_position;
        private string key_id;

        //Melee
        private string melee_image;
        private string melee_damage;
        private string melee_speed;
        private string melee_x_position;
        private string melee_y_position;

        //Distance
        private string distance_image;
        private string distance_damage;
        private string distance_speed;
        private string distance_x_position;
        private string distance_y_position;
        private string distance_bullet_image_up;
        private string distance_bullet_image_down;
        private string distance_bullet_image_left;
        private string distance_bullet_image_right;

        //Attack ability
        private string attack_ability_image;
        private string attack_ability_damage;
        private string attack_ability_speed;
        private string attack_ability_mana;
        private string attack_ability_bullet_image_up;
        private string attack_ability_bullet_image_down;
        private string attack_ability_bullet_image_left;
        private string attack_ability_bullet_image_right;

        //Augmentation ability
        private string augmentation_ability_image;
        private string augmentation_ability_affect;
        private string augmentation_ability_effect;
        private string augmentation_ability_duration;
        private string augmentation_ability_mana;

        //Heal ability
        private string heal_ability_image;
        private string heal_ability_mana;
        private string heal_ability_effect;

        //Consumable
        private string consumable_image;
        private string consumable_affect;
        private string consumable_effect;
        private string consumable_x_position;
        private string consumable_y_position;

        //Screen
        private string screen_end_game;
        private string screen_dead_game;

        #endregion

        private string abilities;
        private string stages;
        private string loadStages;
        private string enemies;
        private string loot;
        private string consumables;
        private string melees;
        private string distances;
        private string doors;
        private string keys;
        private string walls;
        private string npcs;

        public Compiler(string gameCode, string sourceFolder, string destFolder)
        {
            this.sourceFolder = sourceFolder;
            this.destFolder = destFolder;

            string completeSourcePath = sourceFolder + "\\Template\\Template";

            if (String.IsNullOrEmpty(SearchForWindowCS(completeSourcePath)))
            {
                throw new InvalidDataException();
            }

            //se supone que es source folder
            CopyFolder(sourceFolder, destFolder);

            string completeDestPath = destFolder + "\\Template\\Template";

            fileNameCS = SearchForWindowCS(completeDestPath);

            doc = new XmlDocument();
            doc.Load(gameCode);

            XmlNodeList characterNodeList = doc.GetElementsByTagName("character");

            foreach (XmlNode characterNode in characterNodeList)
            {
                foreach (XmlNode characterInfo in characterNode.ChildNodes)
                {
                    switch (characterInfo.Name.ToLower())
                    {
                        #region Character
                        case "character_name":
                            character_name = characterInfo.InnerText;
                            break;
                        case "character_image_up":
                            character_image_up = characterInfo.InnerText;
                            break;
                        case "character_image_down":
                            character_image_down = characterInfo.InnerText;
                            break;
                        case "character_image_left":
                            character_image_left = characterInfo.InnerText;
                            break;
                        case "character_image_right":
                            character_image_right = characterInfo.InnerText;
                            break;
                        case "character_image_attack_up":
                            character_image_attack_up = characterInfo.InnerText;
                            break;
                        case "character_image_attack_down":
                            character_image_attack_down = characterInfo.InnerText;
                            break;
                        case "character_image_attack_left":
                            character_image_attack_left = characterInfo.InnerText;
                            break;
                        case "character_image_attack_right":
                            character_image_attack_right = characterInfo.InnerText;
                            break;
                        case "character_framecount_iddle":
                            character_framecount_iddle = characterInfo.InnerText;
                            break;
                        case "character_framecount_attacking":
                            character_framecount_attacking = characterInfo.InnerText;
                            break;
                        case "character_initial_state":
                            character_initial_state = characterInfo.InnerText;
                            break;
                        case "character_x_position":
                            character_x_position = characterInfo.InnerText;
                            break;
                        case "character_y_position":
                            character_y_position = characterInfo.InnerText;
                            break;
                        case "character_speed":
                            character_speed = characterInfo.InnerText;
                            break;
                        case "character_key_left":
                            character_key_left = characterInfo.InnerText;
                            break;
                        case "character_key_up":
                            character_key_up = characterInfo.InnerText;
                            break;
                        case "character_key_down":
                            character_key_down = characterInfo.InnerText;
                            break;
                        case "character_key_right":
                            character_key_right = characterInfo.InnerText;
                            break;
                        case "character_key_attack":
                            character_key_attack = characterInfo.InnerText;
                            break;
                        case "character_key_shoot":
                            character_key_shoot = characterInfo.InnerText;
                            break;
                        case "character_key_cast":
                            character_key_cast = characterInfo.InnerText;
                            break;
                        case "character_key_change_melee":
                            character_key_change_melee = characterInfo.InnerText;
                            break;
                        case "character_key_change_distance":
                            character_key_change_distance = characterInfo.InnerText;
                            break;
                        case "character_key_change_ability":
                            character_key_change_ability = characterInfo.InnerText;
                            break;
                        case "character_key_action":
                            character_key_action = characterInfo.InnerText;
                            break;
                        case "character_music_attack":
                            character_music_attack = characterInfo.InnerText;
                            break;
                        case "character_music_shoot":
                            character_music_shoot = characterInfo.InnerText;
                            break;
                        case "character_music_cast":
                            character_music_cast = characterInfo.InnerText;
                            break;
                        case "character_strenght":
                            character_strenght = characterInfo.InnerText;
                            break;
                        case "character_dexterity":
                            character_dexterity = characterInfo.InnerText;
                            break;
                        case "character_intelligence":
                            character_intelligence = characterInfo.InnerText;
                            break;
                        case "character_hit_points":
                            character_hit_points = characterInfo.InnerText;
                            break;
                        case "character_mana_points":
                            character_mana_points = characterInfo.InnerText;
                            break;
                        #endregion

                        #region Initial Melee
                        case "melee":

                            foreach (XmlNode meleeInfo in characterInfo.ChildNodes)
                            {
                                switch (meleeInfo.Name.ToLower())
                                {
                                    case "melee_image":
                                        melee_image = meleeInfo.InnerText;
                                        break;
                                    case "melee_damage":
                                        melee_damage = meleeInfo.InnerText;
                                        break;
                                    case "melee_speed":
                                        melee_speed = meleeInfo.InnerText;
                                        break;
                                }
                            }

                            break;
                        #endregion

                        #region Initial Distance
                        case "distance":

                            foreach (XmlNode distanceInfo in characterInfo.ChildNodes)
                            {
                                switch (distanceInfo.Name.ToLower())
                                {
                                    case "distance_image":
                                        distance_image = distanceInfo.InnerText;
                                        break;
                                    case "distance_damage":
                                        distance_damage = distanceInfo.InnerText;
                                        break;
                                    case "distance_speed":
                                        distance_speed = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_up":
                                        distance_bullet_image_up = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_down":
                                        distance_bullet_image_down = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_left":
                                        distance_bullet_image_left = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_right":
                                        distance_bullet_image_right = distanceInfo.InnerText;
                                        break;
                                }
                            }

                            break;
                        #endregion

                        #region Attack Ability
                        case "attack_ability":

                            foreach (XmlNode attackAbilityInfo in characterInfo.ChildNodes)
                            {
                                switch (attackAbilityInfo.Name.ToLower())
                                {
                                    case "attack_ability_image":
                                        attack_ability_image = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_mana":
                                        attack_ability_mana = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_damage":
                                        attack_ability_damage = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_speed":
                                        attack_ability_speed = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_bullet_image_up":
                                        attack_ability_bullet_image_up = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_bullet_image_down":
                                        attack_ability_bullet_image_down = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_bullet_image_left":
                                        attack_ability_bullet_image_left = attackAbilityInfo.InnerText;
                                        break;
                                    case "attack_ability_bullet_image_right":
                                        attack_ability_bullet_image_right = attackAbilityInfo.InnerText;
                                        break;
                                }
                            }

                            abilities += "player.AddAbility(new AttackAbility(\"" + attack_ability_image +
                                "\", " + attack_ability_mana + "," + attack_ability_damage + ", " + attack_ability_speed +
                                ", \"" + attack_ability_bullet_image_up + "\", \"" + attack_ability_bullet_image_down + "\", \"" + attack_ability_bullet_image_left +
                                "\", \"" + attack_ability_bullet_image_right + "\", graphics));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Heal Ability
                        case "heal_ability":

                            foreach (XmlNode healAbilityInfo in characterInfo.ChildNodes)
                            {
                                switch (healAbilityInfo.Name.ToLower())
                                {
                                    case "heal_ability_image":
                                        heal_ability_image = healAbilityInfo.InnerText;
                                        break;
                                    case "heal_ability_mana":
                                        heal_ability_mana = healAbilityInfo.InnerText;
                                        break;
                                    case "heal_ability_effect":
                                        heal_ability_effect = healAbilityInfo.InnerText;
                                        break;                                  
                                }
                            }

                            abilities += "player.AddAbility(new HealAbility(" + heal_ability_mana +
                                ", " + heal_ability_effect + ", \"" + heal_ability_image + "\", graphics));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Augmentation Ability
                        case "augmentation_ability":

                            foreach (XmlNode augmentationAbilityInfo in characterInfo.ChildNodes)
                            {
                                switch (augmentationAbilityInfo.Name.ToLower())
                                {
                                    case "augmentation_ability_image":
                                        augmentation_ability_image = augmentationAbilityInfo.InnerText;
                                        break;
                                    case "augmentation_ability_affect":
                                        augmentation_ability_affect = augmentationAbilityInfo.InnerText;
                                        break;
                                    case "augmentation_ability_effect":
                                        augmentation_ability_effect = augmentationAbilityInfo.InnerText;
                                        break;
                                    case "augmentation_ability_mana":
                                        augmentation_ability_mana = augmentationAbilityInfo.InnerText;
                                        break;
                                    case "augmentation_ability_duration":
                                        augmentation_ability_duration = augmentationAbilityInfo.InnerText;
                                        break;
                                }
                            }

                            abilities += "player.AddAbility(new AugmentationAbility(" + augmentation_ability_mana +
                                ", " + augmentation_ability_effect + ", AugmentationAbility.Type." + augmentation_ability_affect + ", \"" + augmentation_ability_image +
                                "\", " + augmentation_ability_duration + ", graphics));" + Environment.NewLine;

                            break;
                        #endregion
                    }
                }
            }

            #region Build Player
            string characterBuild = " new Player(\"" + character_image_up + "\", \"" + character_image_down + "\", \"" + character_image_left +
                "\", \"" + character_image_right + "\", \"" + character_image_attack_up + "\", \"" + character_image_attack_down +
                "\", \"" + character_image_attack_left + "\", \"" + character_image_attack_right + "\", " + character_y_position + ", " + character_x_position + 
                ", " + character_framecount_iddle + ", " + character_framecount_attacking + ", DynamicObject.State." + character_initial_state +
                ", " + character_speed + ", \"" + character_name + "\", " + character_strenght + ", " + character_dexterity + ", " + character_intelligence +
                ", " + character_hit_points + ", " + character_mana_points + ", \"" + character_music_attack + "\", \"" + character_music_shoot +
                "\", \"" + character_music_cast + "\");" + Environment.NewLine;

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_CHARACTER%", characterBuild + ""));
            streamWriter.Close();
			
            string meleeBuild = "null";

            if (!String.IsNullOrEmpty(melee_image))
            {
                meleeBuild = " new MeleeWeapon(\"" + melee_image + "\", " + melee_damage + ", " + melee_speed + ")";
            }


            string distanceBuild = "null";

            if (!String.IsNullOrEmpty(distance_image))
            {
                distanceBuild = " new DistanceWeapon(\"" + distance_image + "\", " + distance_damage + ", " + distance_speed +
                ", \"" + distance_bullet_image_up + "\", \"" + distance_bullet_image_down + "\", \"" + distance_bullet_image_left +
                "\", \"" + distance_bullet_image_right + "\")";
            }

            string loadCharacter = "player.LoadContent(content, " + distanceBuild + "," + meleeBuild + ", graphics);" + Environment.NewLine;

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%LOAD_CHARACTER_CONTENT%", loadCharacter + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_ABILITIES%", abilities + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_UP%", character_key_up + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_DOWN%", character_key_down + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_LEFT%", character_key_left + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_RIGHT%", character_key_right + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_CHANGE_MELEE%", character_key_change_melee + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_CHANGE_DISTANCE%", character_key_change_distance + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_CHANGE_ABILITY%", character_key_change_ability + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_ATTACK%", character_key_attack + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_SHOOT%", character_key_shoot + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_CAST%", character_key_cast + ";" + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%CHARACTER_KEY_ACTION%", character_key_action + ";" + ""));
            streamWriter.Close();
            #endregion

            XmlNodeList screenNodeList = doc.GetElementsByTagName("screens");

            foreach (XmlNode screenNode in screenNodeList)
            {
                foreach (XmlNode screenInfo in screenNode.ChildNodes)
                {
                    switch (screenInfo.Name.ToLower())
                    {
                        case "screen_end_game":
                            screen_end_game = screenInfo.InnerText;
                            break;
                        case "screen_dead_game":
                            screen_dead_game = screenInfo.InnerText;
                            break;
                    }
                }
            }

            #region Build Screens
            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%LOAD_END_SCREEN%", "\"" + screen_end_game + "\""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%LOAD_DEAD_SCREEN%", "\"" + screen_dead_game + "\""));
            streamWriter.Close();
            #endregion

            XmlNodeList stagesNodeList = doc.GetElementsByTagName("stage");

            int indexLevel = 0;
            int bossCount = 0;

            foreach (XmlNode stage in stagesNodeList)
            {
                foreach (XmlNode stageInfo in stage.ChildNodes)
                {
                    switch (stageInfo.Name.ToLower())
                    {
                        #region Window
                        case "window":

                            foreach (XmlNode windowInfo in stageInfo.ChildNodes)
                            {
                                switch (windowInfo.Name.ToLower())
                                {
                                    case "window_background":
                                        window_background = windowInfo.InnerText;
                                        break;
                                    case "window_background_music":
                                        window_background_music = windowInfo.InnerText;
                                        break;
                                    case "window_id":
                                        window_id = windowInfo.InnerText;
                                        break;
                                    case "window_north":
                                        window_north = windowInfo.InnerText;
                                        break;
                                    case "window_east":
                                        window_east = windowInfo.InnerText;
                                        break;
                                    case "window_south":
                                        window_south = windowInfo.InnerText;
                                        break;
                                    case "window_west":
                                        window_west = windowInfo.InnerText;
                                        break;
                                }
                            }

                            stages += "Stage stage" + indexLevel + " = new Stage(\"" + window_background + "\", player, \"" + window_background_music +
                                "\"," + window_id + "," + window_north + "," + window_east + "," + window_south + "," + window_west + ");" + Environment.NewLine;

                            break;
                        #endregion

                        #region Enemy
                        case "enemy":

                            foreach (XmlNode enemyInfo in stageInfo.ChildNodes)
                            {
                                switch (enemyInfo.Name.ToLower())
                                {
                                    #region Enemy Info
                                    case "enemy_name":
                                        enemy_name = enemyInfo.InnerText;
                                        break;
                                    case "enemy_image_up":
                                        enemy_image_up = enemyInfo.InnerText;
                                        break;
                                    case "enemy_image_down":
                                        enemy_image_down = enemyInfo.InnerText;
                                        break;
                                    case "enemy_image_left":
                                        enemy_image_left = enemyInfo.InnerText;
                                        break;
                                    case "enemy_image_right":
                                        enemy_image_right = enemyInfo.InnerText;
                                        break;
                                    case "enemy_bullet_image_up":
                                        enemy_bullet_image_up = enemyInfo.InnerText;
                                        break;
                                    case "enemy_bullet_image_down":
                                        enemy_bullet_image_down = enemyInfo.InnerText;
                                        break;
                                    case "enemy_bullet_image_left":
                                        enemy_bullet_image_left = enemyInfo.InnerText;
                                        break;
                                    case "enemy_bullet_image_right":
                                        enemy_bullet_image_right = enemyInfo.InnerText;
                                        break;
                                    case "enemy_initial_state":
                                        enemy_initial_state = enemyInfo.InnerText;
                                        break;
                                    case "enemy_framecount":
                                        enemy_framecount = enemyInfo.InnerText;
                                        break;
                                    case "enemy_x_position":
                                        enemy_x_position = enemyInfo.InnerText;
                                        break;
                                    case "enemy_y_position":
                                        enemy_y_position = enemyInfo.InnerText;
                                        break;
                                    case "enemy_speed":
                                        enemy_speed = enemyInfo.InnerText;
                                        break;
                                    case "enemy_bullet_speed":
                                        enemy_bullet_speed = enemyInfo.InnerText;
                                        break;
                                    case "enemy_strenght":
                                        enemy_strenght = enemyInfo.InnerText;
                                        break;
                                    case "enemy_dexterity":
                                        enemy_dexterity = enemyInfo.InnerText;
                                        break;
                                    case "enemy_intelligence":
                                        enemy_intelligence = enemyInfo.InnerText;
                                        break;
                                    case "enemy_hit_points":
                                        enemy_hit_points = enemyInfo.InnerText;
                                        break;
                                    case "enemy_ia":
                                        enemy_ia = enemyInfo.InnerText;
                                        break;
                                    case "enemy_preference":
                                        enemy_preference = enemyInfo.InnerText;
                                        break;
                                    case "enemy_boss":
                                        enemy_boss = enemyInfo.InnerText;
                                        break;
                                    case "enemy_detect_zone":
                                        enemy_detect_zone = enemyInfo.InnerText;
                                        break;
                                    case "enemy_patrol_zone":
                                        enemy_patrol_zone = enemyInfo.InnerText;
                                        break;
                                    #endregion

                                    #region Melee Loot
                                    case "melee":

                                        foreach (XmlNode meleeInfo in enemyInfo.ChildNodes)
                                        {
                                            switch (meleeInfo.Name.ToLower())
                                            {
                                                case "melee_image":
                                                    melee_image = meleeInfo.InnerText;
                                                    break;
                                                case "melee_damage":
                                                    melee_damage = meleeInfo.InnerText;
                                                    break;
                                                case "melee_speed":
                                                    melee_speed = meleeInfo.InnerText;
                                                    break;
                                            }
                                        }

                                        loot = " new MeleeWeapon(\"" + melee_image + "\", " + melee_damage + ", " + melee_speed + ")";

                                        break;

                                    #endregion

                                    #region Distance Loot
                                    case "distance":

                                        foreach (XmlNode distanceInfo in enemyInfo.ChildNodes)
                                        {
                                            switch (distanceInfo.Name.ToLower())
                                            {
                                                case "distance_image":
                                                    distance_image = distanceInfo.InnerText;
                                                    break;
                                                case "distance_damage":
                                                    distance_damage = distanceInfo.InnerText;
                                                    break;
                                                case "distance_speed":
                                                    distance_speed = distanceInfo.InnerText;
                                                    break;
                                                case "distance_bullet_image_up":
                                                    distance_bullet_image_up = distanceInfo.InnerText;
                                                    break;
                                                case "distance_bullet_image_down":
                                                    distance_bullet_image_down = distanceInfo.InnerText;
                                                    break;
                                                case "distance_bullet_image_left":
                                                    distance_bullet_image_left = distanceInfo.InnerText;
                                                    break;
                                                case "distance_bullet_image_right":
                                                    distance_bullet_image_right = distanceInfo.InnerText;
                                                    break;
                                            }
                                        }

                                        loot = " new DistanceWeapon(\"" + distance_image + "\", " + distance_damage + ", " + distance_speed +
                                                 ", \"" + distance_bullet_image_up + "\", \"" + distance_bullet_image_down + "\", \"" + distance_bullet_image_left +
                                                 "\", \"" + distance_bullet_image_right + "\")";

                                        break;
                                    #endregion

                                    #region Key Loot
                                    case "key":

                                        foreach (XmlNode keyInfo in enemyInfo.ChildNodes)
                                        {
                                            switch (keyInfo.Name.ToLower())
                                            {
                                                case "key_image":
                                                    key_image = keyInfo.InnerText;
                                                    break;
                                                case "key_id":
                                                    key_id = keyInfo.InnerText;
                                                    break;
                                            }
                                        }

                                        loot = "new KeyItem(\"" + key_image + "\", " + key_id + ")";

                                        break;
                                    #endregion

                                    #region Consumable Loot
                                    case "consumable":

                                        foreach (XmlNode consumableInfo in enemyInfo.ChildNodes)
                                        {
                                            switch (consumableInfo.Name.ToLower())
                                            {
                                                case "consumable_image":
                                                    consumable_image = consumableInfo.InnerText;
                                                    break;
                                                case "consumable_affect":
                                                    consumable_affect = consumableInfo.InnerText;
                                                    break;
                                                case "consumable_effect":
                                                    consumable_effect = consumableInfo.InnerText;
                                                    break;
                                            }
                                        }

                                        loot = "new Consumable(\"" + consumable_image + "\", " + consumable_affect + ", " + consumable_effect + ")";

                                        break;
                                    #endregion
                                }
                            }

                            if (String.IsNullOrEmpty(loot))
                            {
                                loot = "null";
                            }

                            if (String.IsNullOrEmpty(enemy_patrol_zone))
                            {
                                enemy_patrol_zone = "0";
                            }

                            if (enemy_boss == "true")
                            {
                                enemies += "Enemy boss" + bossCount + " = new Enemy(\"" + enemy_image_up + "\", \"" + enemy_image_down +
                                "\", \"" + enemy_image_left + "\", \"" + enemy_image_right + "\", \"" + enemy_bullet_image_up + "\", \"" + enemy_bullet_image_down +
                                "\", \"" + enemy_bullet_image_left + "\", \"" + enemy_bullet_image_right + "\", " + enemy_y_position + ", " + enemy_x_position +
                                ", " + enemy_framecount + ", DynamicObject.State." + enemy_initial_state + ", " + enemy_speed + ", " + enemy_bullet_speed +
                                ", " + enemy_strenght + ", " + enemy_dexterity + ", " + enemy_intelligence + ", " + enemy_hit_points +
                                ", Enemy.IA." + enemy_ia + ", Enemy.Preference." + enemy_preference + ", " + loot + ", " + enemy_detect_zone +
                                ", " + enemy_patrol_zone + ");" + Environment.NewLine;

                                enemies += "stage" + indexLevel + ".AddToEnemies(boss" + bossCount + ");" + Environment.NewLine;

                                enemies += "finalBoss = boss" + bossCount + ";" + Environment.NewLine;

                                bossCount++;
                            }
                            else
                            {
                                enemies += "stage" + indexLevel + ".AddToEnemies(new Enemy(\"" + enemy_image_up + "\", \"" + enemy_image_down +
                                "\", \"" + enemy_image_left + "\", \"" + enemy_image_right + "\", \"" + enemy_bullet_image_up + "\", \"" + enemy_bullet_image_down +
                                "\", \"" + enemy_bullet_image_left + "\", \"" + enemy_bullet_image_right + "\", " + enemy_y_position + ", " + enemy_x_position +
                                ", " + enemy_framecount + ", DynamicObject.State." + enemy_initial_state + ", " + enemy_speed + ", " + enemy_bullet_speed +
                                ", " + enemy_strenght + ", " + enemy_dexterity + ", " + enemy_intelligence + ", " + enemy_hit_points +
                                ", Enemy.IA." + enemy_ia + ", Enemy.Preference." + enemy_preference + ", " + loot + ", " + enemy_detect_zone +
                                ", " + enemy_patrol_zone + "));" + Environment.NewLine;
                            }

                            break;
                        #endregion

                        #region Wall
                        case "wall":

                            foreach (XmlNode wallInfo in stageInfo.ChildNodes)
                            {
                                switch (wallInfo.Name.ToLower())
                                {
                                    case "wall_name":
                                        wall_name = wallInfo.InnerText;
                                        break;
                                    case "wall_image":
                                        wall_image = wallInfo.InnerText;
                                        break;
                                    case "wall_x_position":
                                        wall_x_position = wallInfo.InnerText;
                                        break;
                                    case "wall_y_position":
                                        wall_y_position = wallInfo.InnerText;
                                        break;
                                    case "wall_solid":
                                        wall_solid = wallInfo.InnerText;
                                        break;
                                    case "wall_bullet_proof":
                                        wall_bullet_proof = wallInfo.InnerText;
                                        break;
                                } 
                            }

                            walls += "stage" + indexLevel + ".AddToBgItems(new BackgroundItem(\"" + wall_image + "\", " + wall_y_position +
                                     ", " + wall_x_position + ", " + wall_solid + ", " + wall_bullet_proof + "));" + Environment.NewLine;

                            break;
                        #endregion

                        #region NPC
                        case "npc":

                            foreach (XmlNode npcInfo in stageInfo.ChildNodes)
                            {
                                switch (npcInfo.Name.ToLower())
                                {
                                    case "npc_image":
                                        npc_image = npcInfo.InnerText;
                                        break;
                                    case "npc_dialog":
                                        npc_dialog = npcInfo.InnerText;
                                        break;
                                    case "npc_x_position":
                                        npc_x_position = npcInfo.InnerText;
                                        break;
                                    case "npc_y_position":
                                        npc_y_position = npcInfo.InnerText;
                                        break;
                                }
                            }

                            npcs += "stage" + indexLevel + ".AddToNonPlayerCharacters(new NonPlayer(\"" + npc_image + "\", " + npc_x_position +
                                    ", " + npc_y_position + ", \"" + npc_dialog + "\"));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Door
                        case "door":

                            foreach (XmlNode doorInfo in stageInfo.ChildNodes)
                            {
                                switch (doorInfo.Name.ToLower())
                                {
                                    case "door_image":
                                        door_image = doorInfo.InnerText;
                                        break;
                                    case "door_x_position":
                                        door_x_position = doorInfo.InnerText;
                                        break;
                                    case "door_y_position":
                                        door_y_position = doorInfo.InnerText;
                                        break;
                                    case "door_id":
                                        door_id = doorInfo.InnerText;
                                        break;
                                }
                            }

                            doors += "stage" + indexLevel + ".AddToDoors(new Door(\"" + door_image + "\", " + door_y_position +
                                    ", " + door_x_position + ", " + door_id + "));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Key
                        case "key":

                            foreach (XmlNode keyInfo in stageInfo.ChildNodes)
                            {
                                switch (keyInfo.Name.ToLower())
                                {
                                    case "key_image":
                                        key_image = keyInfo.InnerText;
                                        break;
                                    case "key_x_position":
                                        key_x_position = keyInfo.InnerText;
                                        break;
                                    case "key_y_position":
                                        key_y_position = keyInfo.InnerText;
                                        break;
                                    case "key_id":
                                        key_id = keyInfo.InnerText;
                                        break;
                                }
                            }

                            keys += "stage" + indexLevel + ".AddToDropableItems(new KeyItem(\"" + key_image + "\", " + key_id +
                                   ", " + key_x_position + ", " + key_y_position + "));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Consumable
                        case "consumable":

                            foreach (XmlNode consumableInfo in stageInfo.ChildNodes)
                            {
                                switch (consumableInfo.Name.ToLower())
                                {
                                    case "consumable_image":
                                        consumable_image = consumableInfo.InnerText;
                                        break;
                                    case "consumable_affect":
                                        consumable_affect = consumableInfo.InnerText;
                                        break;
                                    case "consumable_effect":
                                        consumable_effect = consumableInfo.InnerText;
                                        break;
                                    case "consumable_x_position":
                                        consumable_x_position = consumableInfo.InnerText;
                                        break;
                                    case "consumable_y_position":
                                        consumable_y_position = consumableInfo.InnerText;
                                        break;
                                }
                            }

                            consumables += "stage" + indexLevel + ".AddToDropableItems(new Consumable(\"" + consumable_image +
                                          "\", Consumable.Type." + consumable_affect + ", " + consumable_effect + ", " + consumable_x_position +
                                          ", " + consumable_y_position + "));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Melee
                        case "melee":

                            foreach (XmlNode meleeInfo in stageInfo.ChildNodes)
                            {
                                switch (meleeInfo.Name.ToLower())
                                {
                                    case "melee_image":
                                        melee_image = meleeInfo.InnerText;
                                        break;
                                    case "melee_damage":
                                        melee_damage = meleeInfo.InnerText;
                                        break;
                                    case "melee_speed":
                                        melee_speed = meleeInfo.InnerText;
                                        break;
                                    case "melee_x_position":
                                        melee_x_position = meleeInfo.InnerText;
                                        break;
                                    case "melee_y_position":
                                        melee_y_position = meleeInfo.InnerText;
                                        break;
                                }
                            }

                            melees += "stage" + indexLevel + ".AddToDropableItems(new MeleeWeapon(\"" + melee_image +
                                     "\", " + melee_damage + ", " + melee_speed + ", " + melee_x_position + ", " + melee_y_position + "));" + Environment.NewLine;

                            break;
                        #endregion

                        #region Distance
                        case "distance":

                            foreach (XmlNode distanceInfo in stageInfo.ChildNodes)
                            {
                                switch (distanceInfo.Name.ToLower())
                                {
                                    case "distance_image":
                                        distance_image = distanceInfo.InnerText;
                                        break;
                                    case "distance_damage":
                                        distance_damage = distanceInfo.InnerText;
                                        break;
                                    case "distance_speed":
                                        distance_speed = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_up":
                                        distance_bullet_image_up = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_down":
                                        distance_bullet_image_down = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_left":
                                        distance_bullet_image_left = distanceInfo.InnerText;
                                        break;
                                    case "distance_bullet_image_right":
                                        distance_bullet_image_right = distanceInfo.InnerText;
                                        break;
                                    case "distance_x_position":
                                        distance_x_position = distanceInfo.InnerText;
                                        break;
                                    case "distance_y_position":
                                        distance_y_position = distanceInfo.InnerText;
                                        break;
                                }
                            }

                            distances += "stage" + indexLevel + ".AddToDropableItems(new DistanceWeapon(\"" + distance_image + "\", " + distance_damage +
                                        ", " + distance_speed + ", \"" + distance_bullet_image_up + "\", \"" + distance_bullet_image_down +
                                        "\", \"" + distance_bullet_image_left + "\", \"" + distance_bullet_image_right + "\", " + distance_x_position +
                                        ", " + distance_y_position + "));" + Environment.NewLine;

                            break;
                        #endregion
                    }
                }

                loadStages += "stage" + indexLevel + ".LoadContent(graphics);" + Environment.NewLine;
                loadStages += "stages.Add(stage" + indexLevel + ");" + Environment.NewLine;

                if (indexLevel == 0)
                {
                    loadStages += "currentStage = stage0;";
                }

                indexLevel++;
            }

            #region Build Stages
            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_STAGES%", stages + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_ENEMIES%", enemies));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_WALLS%", walls + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_NPCS%", npcs + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_CONSUMABLES%", consumables + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_MELEE%", melees + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_DISTANCE%", distances + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_DOORS%", doors + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%BUILD_KEYS%", keys + ""));
            streamWriter.Close();

            contents = Refresh(fileNameCS);
            streamWriter = File.CreateText(fileNameCS);
            streamWriter.Write(contents.Replace("%LOAD_STAGES%", loadStages + ""));
            streamWriter.Close();
            #endregion

        }

        /// <summary>
        /// Refresh the stream reader.
        /// </summary>
        /// <param name="filename">Name of the file being read</param>
        /// <returns>Content of the file</returns>
        private string Refresh(string filename)
        {
            // Open a file for reading                                        
            StreamReader streamReader = File.OpenText(filename);

            // Now, read the entire file into a string
            string contents = streamReader.ReadToEnd();
            streamReader.Close();

            return contents;
        }

        /// <summary>
        /// Copy every folder and file from the selected folder.
        /// </summary>
        /// <param name="sourceFolder">Selected folder</param>
        /// <param name="destFolder">Destination folder</param>
        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder) || Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        /// <summary>
        /// Search for the Template folder.
        /// </summary>
        /// <param name="destFolder">Destination folder</param>
        /// <returns>Path of the file to be written by the analyzer</returns>
        private string SearchForWindowCS(string destFolder)
        {
            string[] folders = Directory.GetDirectories(destFolder);

            foreach (string folder in folders)
            {
                string[] files = Directory.GetFiles(folder);
                foreach (string file in files)
                {
                    if (file == Path.Combine(folder, "World.cs"))
                    {
                        fileNameCS = Path.Combine(folder, "World.cs");
                        return fileNameCS;
                    }
                }
            }
            return null;
        }
    }
}

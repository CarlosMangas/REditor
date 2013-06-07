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
using Interface.Controller;

namespace Interface.Model
{
    /// <summary>
    /// Model class to save the information of a stage.
    /// </summary>
    public class Stage
    {
        #region Definitions
        public Character Player { get; set; }

        public List<Enemy> Enemies { get; set; }

        public List<BackgroundItem> BgItems { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<Door> Doors { get; set; }

        public List<KeyItem> Keys { get; set; }

        public List<Consumable> Consumables { get; set; }

        public List<NPC> NonPlayers { get; set; }

        public string Background { get; set; }

        public string Music { get; set; }

        public int ID { get; set; }

        public int ExitUp { get; set; }

        public int ExitDown { get; set; }

        public int ExitLeft { get; set; }

        public int ExitRight { get; set; }
        #endregion

        public Stage(int id, string background, string music)
        {
            this.ID = id;
            this.Background = background;
            this.Music = music;

            Enemies = new List<Enemy>();
            Consumables = new List<Consumable>();
            NonPlayers = new List<NPC>();
            Weapons = new List<Weapon>();
            Doors = new List<Door>();
            Keys = new List<KeyItem>();
            BgItems = new List<BackgroundItem>();

            ExitUp = -1;
            ExitDown = -1;
            ExitLeft = -1;
            ExitRight = -1;
        }

        /// <summary>
        /// Search for an enemy in the enemy list.
        /// </summary>
        /// <param name="name">Name of the enemy</param>
        /// <returns>Enemy</returns>
        public Enemy GetEnemy(string name)
        {
            foreach (Enemy auxEnemy in Enemies)
            {
                if (auxEnemy.Name == name)
                {
                    return auxEnemy;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an consumable in the consumable list.
        /// </summary>
        /// <param name="name">Name of the consumable</param>
        /// <returns>Consumable</returns>
        public Consumable GetConsumable(string name)
        {
            foreach (Consumable auxConsumable in Consumables)
            {
                if (auxConsumable.Name == name)
                {
                    return auxConsumable;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an weapon in the weapon list.
        /// </summary>
        /// <param name="name">Name of the weapon</param>
        /// <returns>Weapon</returns>
        public Weapon GetWeapon(string name)
        {
            foreach (Weapon auxWeapon in Weapons)
            {
                if (auxWeapon.Name == name)
                {
                    return auxWeapon;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an npc in the npc list.
        /// </summary>
        /// <param name="name">Name of the npc</param>
        /// <returns>NPC</returns>
        public NPC GetNPC(string name)
        {
            foreach (NPC auxNPC in NonPlayers)
            {
                if (auxNPC.Name == name)
                {
                    return auxNPC;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an door in the door list.
        /// </summary>
        /// <param name="name">Name of the door</param>
        /// <returns>Door</returns>
        public Door GetDoor(string name)
        {
            foreach (Door auxDoor in Doors)
            {
                if (auxDoor.Name == name)
                {
                    return auxDoor;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an key in the key list.
        /// </summary>
        /// <param name="name">Name of the key</param>
        /// <returns>Key</returns>
        public KeyItem GetKey(string name)
        {
            foreach (KeyItem auxKey in Keys)
            {
                if (auxKey.Name == name)
                {
                    return auxKey;
                }
            }

            return null;
        }

        /// <summary>
        /// Builds a string of the stage's information and content.
        /// </summary>
        /// <param name="manager">Main window manager</param>
        /// <returns>String for the DSL</returns>
        public string ToString(MainWindowManager manager)
        {
            string total = "";

            Background = Background.Replace('\\', '/');

            if (!String.IsNullOrEmpty(Music))
            {
                Music = Music.Replace('\\', '/');
            }

            total += "\t<stage>\r\n";

            total += "\t\t<window>\r\n" +
                        "\t\t\t<window_background>" + Background + "</window_background>\r\n" +
                        "\t\t\t<window_background_music>" + Music + "</window_background_music>\r\n" +
                        "\t\t\t<window_id>" + ID + "</window_id>\r\n" +
                        "\t\t\t<window_north>" + ExitUp + "</window_north>\r\n" +
                        "\t\t\t<window_east>" + ExitRight + "</window_east>\r\n" +
                        "\t\t\t<window_south>" + ExitDown + "</window_south>\r\n" +
                        "\t\t\t<window_west>" + ExitLeft + "</window_west>\r\n" +
                        "\t\t</window>\r\n";

            foreach (Enemy enemy in Enemies)
            {
                enemy.ImageMovingUp = enemy.ImageMovingUp.Replace('\\', '/');
                enemy.ImageMovingDown = enemy.ImageMovingDown.Replace('\\', '/');
                enemy.ImageMovingLeft = enemy.ImageMovingLeft.Replace('\\', '/');
                enemy.ImageMovingRight = enemy.ImageMovingRight.Replace('\\', '/');

                if (!String.IsNullOrEmpty(enemy.ImageArrowUp))
                {
                    enemy.ImageArrowUp = enemy.ImageArrowUp.Replace('\\', '/');
                    enemy.ImageArrowDown = enemy.ImageArrowDown.Replace('\\', '/');
                    enemy.ImageArrowLeft = enemy.ImageArrowLeft.Replace('\\', '/');
                    enemy.ImageArrowRight = enemy.ImageArrowRight.Replace('\\', '/');
                }

                int enemySpeed;

                if (enemy.Speed == "slow")
                {
                    enemySpeed = 1;
                }
                else if (enemy.Speed == "normal")
                {
                    enemySpeed = 2;
                }
                else
                {
                    enemySpeed = 3;
                }

                int bulletspeed = enemySpeed + 1;

                total += "\t\t<enemy>\r\n" +
                            "\t\t\t<enemy_image_up>" + enemy.ImageMovingUp + "</enemy_image_up>\r\n" +
                            "\t\t\t<enemy_image_down>" + enemy.ImageMovingDown + "</enemy_image_down>\r\n" +
                            "\t\t\t<enemy_image_left>" + enemy.ImageMovingLeft + "</enemy_image_left>\r\n" +
                            "\t\t\t<enemy_image_right>" + enemy.ImageMovingRight + "</enemy_image_right>\r\n" +
                            "\t\t\t<enemy_bullet_image_up>" + enemy.ImageArrowUp + "</enemy_bullet_image_up>\r\n" +
                            "\t\t\t<enemy_bullet_image_down>" + enemy.ImageArrowDown + "</enemy_bullet_image_down>\r\n" +
                            "\t\t\t<enemy_bullet_image_left>" + enemy.ImageArrowLeft + "</enemy_bullet_image_left>\r\n" +
                            "\t\t\t<enemy_bullet_image_right>" + enemy.ImageArrowRight + "</enemy_bullet_image_right>\r\n" +
                            "\t\t\t<enemy_initial_state>" + enemy.InitialState + "</enemy_initial_state>\r\n" +
                            "\t\t\t<enemy_framecount>" + enemy.FrameCount + "</enemy_framecount>\r\n" +
                            "\t\t\t<enemy_x_position>" + enemy.PositionX + "</enemy_x_position>\r\n" +
                            "\t\t\t<enemy_y_position>" + enemy.PositionY + "</enemy_y_position>\r\n" +
                            "\t\t\t<enemy_speed>" + enemySpeed + "</enemy_speed>\r\n" +
                            "\t\t\t<enemy_bullet_speed>" + bulletspeed + "</enemy_bullet_speed>\r\n" +
                            "\t\t\t<enemy_strenght>" + enemy.Strenght + "</enemy_strenght>\r\n" +
                            "\t\t\t<enemy_dexterity>" + enemy.Dexterity + "</enemy_dexterity>\r\n" +
                            "\t\t\t<enemy_intelligence>" + enemy.Intelligence + "</enemy_intelligence>\r\n" +
                            "\t\t\t<enemy_hit_points>" + enemy.HitPoints + "</enemy_hit_points>\r\n" +
                            "\t\t\t<enemy_ia>" + enemy.Behavoir + "</enemy_ia>\r\n" +
                            "\t\t\t<enemy_preference>" + enemy.Preference + "</enemy_preference>\r\n" +
                            "\t\t\t<enemy_boss>" + enemy.Boss + "</enemy_boss>\r\n" +
                            "\t\t\t<enemy_detect_zone>" + enemy.DetectZone + "</enemy_detect_zone>\r\n" +
                            "\t\t\t<enemy_patrol_zone>" + enemy.PatrolZone + "</enemy_patrol_zone>\r\n";

                if (manager.GetWeapon(enemy.Loot) != null)
                {
                    Weapon weapon = manager.GetWeapon(enemy.Loot);

                    weapon.ImageWeapon = weapon.ImageWeapon.Replace('\\', '/');

                    int weaponSpeed;

                    if (weapon.Speed == "slow")
                    {
                        weaponSpeed = 1;
                    }
                    else if (weapon.Speed == "normal")
                    {
                        weaponSpeed = 2;
                    }
                    else
                    {
                        weaponSpeed = 3;
                    }

                    if (weapon.Type == "melee")
                    {
                        total += "\t\t\t<melee>\r\n" +
                                 "\t\t\t\t<melee_image>" + weapon.ImageWeapon + "</melee_image>\r\n" +
                                 "\t\t\t\t<melee_damage>" + weapon.Damage + "</melee_damage>\r\n" +
                                 "\t\t\t\t<melee_speed>" + weaponSpeed + "</melee_speed>\r\n" +
                                 "\t\t\t</melee>\r\n";
                    }
                    else
                    {
                        weapon.ImageWeapon = weapon.ImageWeapon.Replace('\\', '/');
                        weapon.ImageArrowUp = weapon.ImageArrowUp.Replace('\\', '/');
                        weapon.ImageArrowDown = weapon.ImageArrowDown.Replace('\\', '/');
                        weapon.ImageArrowLeft = weapon.ImageArrowLeft.Replace('\\', '/');
                        weapon.ImageArrowRight = weapon.ImageArrowRight.Replace('\\', '/');

                        total += "\t\t\t<distance>\r\n" +
                                 "\t\t\t\t<distance_image>" + weapon.ImageWeapon + "</distance_image>\r\n" +
                                 "\t\t\t\t<distance_damage>" + weapon.Damage + "</distance_damage>\r\n" +
                                 "\t\t\t\t<distance_speed>" + weaponSpeed + "</distance_speed>\r\n" +
                                 "\t\t\t\t<distance_bullet_image_up>" + weapon.ImageArrowUp + "</distance_bullet_image_up>\r\n" +
                                 "\t\t\t\t<distance_bullet_image_down>" + weapon.ImageArrowDown + "</distance_bullet_image_down>\r\n" +
                                 "\t\t\t\t<distance_bullet_image_left>" + weapon.ImageArrowLeft + "</distance_bullet_image_left>\r\n" +
                                 "\t\t\t\t<distance_bullet_image_right>" + weapon.ImageArrowRight + "</distance_bullet_image_right>\r\n" +
                                 "\t\t\t</distance>\r\n";
                    }
                }
                else if (manager.GetKey(enemy.Loot) != null)
                {
                    KeyItem key = manager.GetKey(enemy.Loot);

                    key.Image = key.Image.Replace('\\', '/');

                    total += "\t\t\t<key>\r\n" +
                                "\t\t\t\t<key_image>" + key.Image + "</key_image>\r\n" +
                                "\t\t\t\t<key_id>" + key.ID + "</key_id>\r\n" +
                                "\t\t\t</key>\r\n";
                }
                else if (manager.GetConsumable(enemy.Loot) != null)
                {
                    Consumable consumable = manager.GetConsumable(enemy.Loot);

                    consumable.Image = consumable.Image.Replace('\\', '/');

                    total += "\t\t\t<consumable>\r\n" +
                                "\t\t\t\t<consumable_image>" + consumable.Image + "</consumable_image>\r\n" +
                                "\t\t\t\t<consumable_affect>" + consumable.Type + "</consumable_affect>\r\n" +
                                "\t\t\t\t<consumable_effect>" + consumable.Effect + "</consumable_effect>\r\n" +
                                "\t\t\t</consumable>\r\n";
                }

                total += "\t\t</enemy>\r\n"; ;
            }

            foreach (Consumable consumable in Consumables)
            {
                consumable.Image = consumable.Image.Replace('\\', '/');

                total += "\t\t<consumable>\r\n" +
                            "\t\t\t<consumable_image>" + consumable.Image + "</consumable_image>\r\n" +
                            "\t\t\t<consumable_affect>" + consumable.Type + "</consumable_affect>\r\n" +
                            "\t\t\t<consumable_effect>" + consumable.Effect + "</consumable_effect>\r\n" +
                            "\t\t\t<consumable_x_position>" + consumable.PositionX + "</consumable_x_position>\r\n" +
                            "\t\t\t<consumable_y_position>" + consumable.PositionY + "</consumable_y_position>\r\n" +
                            "\t\t</consumable>\r\n";
            }

            foreach (BackgroundItem wall in BgItems)
            {
                wall.Image = wall.Image.Replace('\\', '/');

                total += "\t\t<wall>\r\n" +
                            "\t\t\t<wall_image>" + wall.Image + "</wall_image>\r\n" +
                            "\t\t\t<wall_x_position>" + wall.PositionX + "</wall_x_position>\r\n" +
                            "\t\t\t<wall_y_position>" + wall.PositionY + "</wall_y_position>\r\n" +
                            "\t\t\t<wall_solid>" + wall.Solid + "</wall_solid>\r\n" +
                            "\t\t\t<wall_bullet_proof>" + wall.BulletProof + "</wall_bullet_proof>\r\n" +
                            "\t\t</wall>\r\n";
            }

            foreach (NPC npc in NonPlayers)
            {
                npc.Image = npc.Image.Replace('\\', '/');
                npc.DialogImage = npc.DialogImage.Replace('\\', '/');

                total += "\t\t<npc>\r\n" +
                            "\t\t\t<npc_image>" + npc.Image + "</npc_image>\r\n" +
                            "\t\t\t<npc_dialog>" + npc.DialogImage + "</npc_dialog>\r\n" +
                            "\t\t\t<npc_x_position>" + npc.PositionX + "</npc_x_position>\r\n" +
                            "\t\t\t<npc_y_position>" + npc.PositionY + "</npc_y_position>\r\n" +
                            "\t\t</npc>\r\n";
            }

            foreach (Weapon weapon in Weapons)
            {
                weapon.ImageWeapon = weapon.ImageWeapon.Replace('\\', '/');

                int weaponSpeed;

                if (weapon.Speed == "slow")
                {
                    weaponSpeed = 1;
                }
                else if (weapon.Speed == "normal")
                {
                    weaponSpeed = 2;
                }
                else
                {
                    weaponSpeed = 3;
                }

                if (weapon.Type == "melee")
                {
                    total += "\t\t<melee>\r\n" +
                             "\t\t\t<melee_image>" + weapon.ImageWeapon + "</melee_image>\r\n" +
                             "\t\t\t<melee_damage>" + weapon.Damage + "</melee_damage>\r\n" +
                             "\t\t\t<melee_speed>" + weaponSpeed + "</melee_speed>\r\n" +
                             "\t\t\t<melee_x_position>" + weapon.PositionX + "</melee_x_position>\r\n" +
                             "\t\t\t<melee_y_position>" + weapon.PositionY + "</melee_y_position>\r\n" +
                             "\t\t</melee>\r\n";
                }
                else
                {
                    weapon.ImageWeapon = weapon.ImageWeapon.Replace('\\', '/');
                    weapon.ImageArrowUp = weapon.ImageArrowUp.Replace('\\', '/');
                    weapon.ImageArrowDown = weapon.ImageArrowDown.Replace('\\', '/');
                    weapon.ImageArrowLeft = weapon.ImageArrowLeft.Replace('\\', '/');
                    weapon.ImageArrowRight = weapon.ImageArrowRight.Replace('\\', '/');

                    total += "\t\t<distance>\r\n" +
                             "\t\t\t<distance_image>" + weapon.ImageWeapon + "</distance_image>\r\n" +
                             "\t\t\t<distance_damage>" + weapon.Damage + "</distance_damage>\r\n" +
                             "\t\t\t<distance_speed>" + weaponSpeed + "</distance_speed>\r\n" +
                             "\t\t\t<distance_bullet_image_up>" + weapon.ImageArrowUp + "</distance_bullet_image_up>\r\n" +
                             "\t\t\t<distance_bullet_image_down>" + weapon.ImageArrowDown + "</distance_bullet_image_down>\r\n" +
                             "\t\t\t<distance_bullet_image_left>" + weapon.ImageArrowLeft + "</distance_bullet_image_left>\r\n" +
                             "\t\t\t<distance_bullet_image_right>" + weapon.ImageArrowRight + "</distance_bullet_image_right>\r\n" +
                             "\t\t\t<distance_x_position>" + weapon.PositionX + "</distance_x_position>\r\n" +
                             "\t\t\t<distance_y_position>" + weapon.PositionY + "</distance_y_position>\r\n" +
                             "\t\t</distance>\r\n";
                }
            }

            foreach (Door door in Doors)
            {
                door.Image = door.Image.Replace('\\', '/');

                total += "\t\t<door>\r\n" +
                            "\t\t\t<door_image>" + door.Image + "</door_image>\r\n" +
                            "\t\t\t<door_x_position>" + door.PositionX + "</door_x_position>\r\n" +
                            "\t\t\t<door_y_position>" + door.PositionY + "</door_y_position>\r\n" +
                            "\t\t\t<door_id>" + door.ID + "</door_id>\r\n" +
                            "\t\t</door>\r\n";
            }

            foreach (KeyItem key in Keys)
            {
                key.Image = key.Image.Replace('\\', '/');

                total += "\t\t<key>\r\n" +
                            "\t\t\t<key_image>" + key.Image + "</key_image>\r\n" +
                            "\t\t\t<key_x_position>" + key.PositionX + "</key_x_position>\r\n" +
                            "\t\t\t<key_y_position>" + key.PositionY + "</key_y_position>\r\n" +
                            "\t\t\t<key_id>" + key.ID + "</key_id>\r\n" +
                            "\t\t</key>\r\n";
            }

            total += "\t</stage>\r\n";

            return total;
        }
    }
}

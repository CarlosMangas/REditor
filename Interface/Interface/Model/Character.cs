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
using System.Windows.Controls;
using Interface.Controller;

namespace Interface.Model
{
    /// <summary>
    /// Model class to save the information of the main character.
    /// </summary>
    public class Character
    {
        #region Definitions
        public String Name { get; set; }

        public String ImageMovingUp { get; set; }

        public String ImageMovingDown { get; set; }

        public String ImageMovingLeft { get; set; }

        public String ImageMovingRight { get; set; }

        public String ImageAttackingUp { get; set; }

        public String ImageAttackingDown { get; set; }

        public String ImageAttackingLeft { get; set; }

        public String ImageAttackingRight { get; set; }

        public int FrameCount { get; set; }

        public int FrameAttackingCount { get; set; }

        public String Speed { get; set; }

        public String AttackSoundEffect { get; set; }

        public String ShootSoundEffect { get; set; }

        public String CastSoundEffect { get; set; }

        public Weapon Melee { get; set; }

        public Weapon Distance { get; set; }

        public int Strenght { get; set; }

        public int Intelligence { get; set; }

        public int Dexterity { get; set; }

        public int HitPoints { get; set; }

        public int Mana { get; set; }

        public String InitialState { get; set; }

        public string PreviewImage { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public bool IsSet{ get; set;}

        public Image CurrentImage { get; set; }
        #endregion

        public Character(string name, int strenght, int intelligence, int dexterity, int hitPoints, int mana, string imageMovingUp,
                        string imageMovingDown, string imageMovingLeft, string imageMovingRight, string imageAttackingUp,
                        string imageAttackingDown, string imageAttackingLeft, string imageAttackingRight, Weapon melee, Weapon distance,
                        int frameCount, int frameAttackingCount, string attackSoundEffect, string shootSoundEffect,
                        string castSoundEffect, string initialState, string speed)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.Intelligence = intelligence;
            this.Dexterity = dexterity;
            this.HitPoints = hitPoints;
            this.Mana = mana;
            this.ImageMovingUp = imageMovingUp;
            this.ImageMovingDown = imageMovingDown;
            this.ImageMovingLeft = imageMovingLeft;
            this.ImageMovingRight = imageMovingRight;
            this.ImageAttackingUp = imageAttackingUp;
            this.ImageAttackingDown = imageAttackingDown;
            this.ImageAttackingLeft = imageAttackingLeft;
            this.ImageAttackingRight = imageAttackingRight;
            this.FrameCount = frameCount;
            this.FrameAttackingCount = frameAttackingCount;
            this.AttackSoundEffect = attackSoundEffect;
            this.ShootSoundEffect = shootSoundEffect;
            this.CastSoundEffect = castSoundEffect;
            this.Melee = melee;
            this.Distance = distance;
            this.InitialState = initialState;
            this.Speed = speed;
        }

        /// <summary>
        /// Builds the string of the character's information.
        /// </summary>
        /// <param name="manager">Main window manager</param>
        /// <returns>String for the general information text box</returns>
        public string GetGeneralInfoString()
        {
            string aux = "Name: " + Name + "\n";
            aux += "Strenght: " + Strenght + "\n";
            aux += "Intelligence: " + Intelligence + "\n";
            aux += "Dexterity: " + Dexterity + "\n";
            aux += "Hit Points: " + HitPoints + "\n";
            aux += "Mana Points: " + Mana + "\n";
            aux += "Speed: " + Speed + "\n";
            aux += "Moving up path: " + ImageMovingUp + "\n";
            aux += "Moving down path: " + ImageMovingDown + "\n";
            aux += "Moving left path: " + ImageMovingLeft + "\n";
            aux += "Moving right path: " + ImageMovingRight + "\n";

            if (ImageAttackingDown != null)
            {
                aux += "Attacking up path: " + ImageAttackingUp + "\n";
                aux += "Attacking down path: " + ImageAttackingDown + "\n";
                aux += "Attacking left path: " + ImageAttackingLeft + "\n";
                aux += "Attacking right path: " + ImageAttackingRight + "\n";
                aux += "Melee weapon: " + Melee.Name + "\n";
            }

            if (Distance != null)
            {
                aux += "Distance weapon: " + Distance.Name + "\n";
            }

            if (AttackSoundEffect != null)
            {
                aux += "Attack sound path: " + AttackSoundEffect + "\n";
            }

            if (ShootSoundEffect != null)
            {
                aux += "Shoot sound path: " + ShootSoundEffect + "\n";
            }

            if (CastSoundEffect != null)
            {
                aux += "Use ability sound path: " + CastSoundEffect + "\n";
            }

            aux += "Initial direction: " + InitialState + "\n";

            return aux;
        }

        /// <summary>
        /// Builds the string of the character's information.
        /// </summary>
        /// <param name="manager">Main window manager</param>
        /// <returns>String for the DSL</returns>
        public string ToString(MainWindowManager manager)
        {
            int realSpeed;
            string total;

            ImageMovingUp = ImageMovingUp.Replace('\\', '/');
            ImageMovingDown = ImageMovingDown.Replace('\\', '/');
            ImageMovingLeft = ImageMovingLeft.Replace('\\', '/');
            ImageMovingRight = ImageMovingRight.Replace('\\', '/');

            if (!String.IsNullOrEmpty(ImageAttackingUp))
            {
                ImageAttackingUp = ImageAttackingUp.Replace('\\', '/');
                ImageAttackingDown = ImageAttackingDown.Replace('\\', '/');
                ImageAttackingLeft = ImageAttackingLeft.Replace('\\', '/');
                ImageAttackingRight = ImageAttackingRight.Replace('\\', '/');
            }

            if (!String.IsNullOrEmpty(AttackSoundEffect))
            {
                AttackSoundEffect = AttackSoundEffect.Replace('\\', '/');
            }

            if (!String.IsNullOrEmpty(ShootSoundEffect))
            {
                ShootSoundEffect = ShootSoundEffect.Replace('\\', '/');
            }

            if (!String.IsNullOrEmpty(CastSoundEffect))
            {
                CastSoundEffect = CastSoundEffect.Replace('\\', '/');
            }

            if(Speed == "slow")
            {
                realSpeed = 2;
            }
            else if (Speed == "normal")
            {
                realSpeed = 3;
            }
            else
            {
                realSpeed = 4;
            }

            total = "\t<character>\r\n" +
                        "\t\t<character_name>" + Name + "</character_name>\r\n" +
                        "\t\t<character_image_up>" + ImageMovingUp + "</character_image_up>\r\n" +
                        "\t\t<character_image_down>" + ImageMovingDown + "</character_image_down>\r\n" +
                        "\t\t<character_image_left>" + ImageMovingLeft + "</character_image_left>\r\n" +
                        "\t\t<character_image_right>" + ImageMovingRight + "</character_image_right>\r\n" +
                        "\t\t<character_image_attack_up>" + ImageAttackingUp + "</character_image_attack_up>\r\n" +
                        "\t\t<character_image_attack_down>" + ImageAttackingDown + "</character_image_attack_down>\r\n" +
                        "\t\t<character_image_attack_left>" + ImageAttackingLeft + "</character_image_attack_left>\r\n" +
                        "\t\t<character_image_attack_right>" + ImageAttackingRight + "</character_image_attack_right>\r\n" +
                        "\t\t<character_framecount_iddle>" + FrameCount + "</character_framecount_iddle>\r\n" +
                        "\t\t<character_framecount_attacking>" + FrameAttackingCount + "</character_framecount_attacking>\r\n" +
                        "\t\t<character_initial_state>" + InitialState + "</character_initial_state>\r\n" +
                        "\t\t<character_x_position>" + PositionX + "</character_x_position>\r\n" +
                        "\t\t<character_y_position>" + PositionY + "</character_y_position>\r\n" +
                        "\t\t<character_speed>" + realSpeed + "</character_speed>\r\n" +
                        "\t\t<character_key_up>" + manager.KeyUp + "</character_key_up>\r\n" +
                        "\t\t<character_key_down>" + manager.KeyDown + "</character_key_down>\r\n" +
                        "\t\t<character_key_left>" + manager.KeyLeft + "</character_key_left>\r\n" +
                        "\t\t<character_key_right>" + manager.KeyRight + "</character_key_right>\r\n" +
                        "\t\t<character_key_attack>" + manager.KeyAttack + "</character_key_attack>\r\n" +
                        "\t\t<character_key_shoot>" + manager.KeyShoot + "</character_key_shoot>\r\n" +
                        "\t\t<character_key_cast>" + manager.KeyUseAbility + "</character_key_cast>\r\n" +
                        "\t\t<character_key_change_melee>" + manager.KeyChangeMelee + "</character_key_change_melee>\r\n" +
                        "\t\t<character_key_change_distance>" + manager.KeyChangeDistance + "</character_key_change_distance>\r\n" +
                        "\t\t<character_key_change_ability>" + manager.KeyChangeAbility + "</character_key_change_ability>\r\n" +
                        "\t\t<character_key_action>" + manager.KeyAction + "</character_key_action>\r\n" +
                        "\t\t<character_music_attack>" + AttackSoundEffect + "</character_music_attack>\r\n" +
                        "\t\t<character_music_shoot>" + ShootSoundEffect + "</character_music_shoot>\r\n" +
                        "\t\t<character_music_cast>" + CastSoundEffect + "</character_music_cast>\r\n" +
                        "\t\t<character_strenght>" + Strenght + "</character_strenght>\r\n" +
                        "\t\t<character_dexterity>" + Dexterity + "</character_dexterity>\r\n" +
                        "\t\t<character_intelligence>" + Intelligence + "</character_intelligence>\r\n" +
                        "\t\t<character_hit_points>" + HitPoints + "</character_hit_points>\r\n" +
                        "\t\t<character_mana_points>" + Mana + "</character_mana_points>\r\n";

            if (Melee != null)
            {
                Melee.ImageWeapon = Melee.ImageWeapon.Replace('\\', '/');

                int weaponSpeed;

                if (Melee.Speed == "slow")
                {
                    weaponSpeed = 1;
                }
                else if (Melee.Speed == "normal")
                {
                    weaponSpeed = 2;
                }
                else
                {
                    weaponSpeed = 3;
                }

                total += "\t\t<melee>\r\n" +
                             "\t\t\t<melee_image>" + Melee.ImageWeapon + "</melee_image>\r\n" +
                             "\t\t\t<melee_damage>" + Melee.Damage + "</melee_damage>\r\n" +
                             "\t\t\t<melee_speed>" + weaponSpeed + "</melee_speed>\r\n" +
                             "\t\t</melee>\r\n";
            }

            if (Distance != null)
            {
                Distance.ImageWeapon = Distance.ImageWeapon.Replace('\\', '/');
                Distance.ImageArrowUp = Distance.ImageArrowUp.Replace('\\', '/');
                Distance.ImageArrowDown = Distance.ImageArrowDown.Replace('\\', '/');
                Distance.ImageArrowLeft = Distance.ImageArrowLeft.Replace('\\', '/');
                Distance.ImageArrowRight = Distance.ImageArrowRight.Replace('\\', '/');

                int weaponSpeed;

                if (Distance.Speed == "slow")
                {
                    weaponSpeed = 3;
                }
                else if (Distance.Speed == "normal")
                {
                    weaponSpeed = 4;
                }
                else
                {
                    weaponSpeed = 5;
                }

                total += "\t\t<distance>\r\n" +
                             "\t\t\t<distance_image>" + Distance.ImageWeapon + "</distance_image>\r\n" +
                             "\t\t\t<distance_damage>" + Distance.Damage + "</distance_damage>\r\n" +
                             "\t\t\t<distance_speed>" + weaponSpeed + "</distance_speed>\r\n" +
                             "\t\t\t<distance_bullet_image_up>" + Distance.ImageArrowUp + "</distance_bullet_image_up>\r\n" +
                             "\t\t\t<distance_bullet_image_down>" + Distance.ImageArrowDown + "</distance_bullet_image_down>\r\n" +
                             "\t\t\t<distance_bullet_image_left>" + Distance.ImageArrowLeft + "</distance_bullet_image_left>\r\n" +
                             "\t\t\t<distance_bullet_image_right>" + Distance.ImageArrowRight + "</distance_bullet_image_right>\r\n" +
                             "\t\t</distance>\r\n";
            }

            foreach (Ability ability in manager.Abilities)
            {
                ability.ImageAbility = ability.ImageAbility.Replace('\\', '/');

                if (ability.Type == "heal")
                {
                    total += "\t\t<heal_ability>\r\n" +
                             "\t\t\t<heal_ability_image>" + ability.ImageAbility + "</heal_ability_image>\r\n" +
                             "\t\t\t<heal_ability_mana>" + ability.Mana + "</heal_ability_mana>\r\n" +
                             "\t\t\t<heal_ability_effect>" + ability.Effect + "</heal_ability_effect>\r\n" +
                             "\t\t</heal_ability>\r\n";
                }
                else if (ability.Type == "attack")
                {
                    int abilitySpeed;

                    if (ability.Speed == "slow")
                    {
                        abilitySpeed = 3;
                    }
                    else if (ability.Speed == "normal")
                    {
                        abilitySpeed = 4;
                    }
                    else
                    {
                        abilitySpeed = 5;
                    }

                    ability.ImageAttackUp = ability.ImageAttackUp.Replace('\\', '/');
                    ability.ImageAttackDown = ability.ImageAttackDown.Replace('\\', '/');
                    ability.ImageAttackLeft = ability.ImageAttackLeft.Replace('\\', '/');
                    ability.ImageAttackRight = ability.ImageAttackRight.Replace('\\', '/');

                    total += "\t\t<attack_ability>\r\n" +
                             "\t\t\t<attack_ability_image>" + ability.ImageAbility + "</attack_ability_image>\r\n" +
                             "\t\t\t<attack_ability_mana>" + ability.Mana + "</attack_ability_mana>\r\n" +
                             "\t\t\t<attack_ability_damage>" + ability.Effect + "</attack_ability_damage>\r\n" +
                             "\t\t\t<attack_ability_speed>" + abilitySpeed + "</attack_ability_speed>\r\n" +
                             "\t\t\t<attack_ability_bullet_image_up>" + ability.ImageAttackUp + "</attack_ability_bullet_image_up>\r\n" +
                             "\t\t\t<attack_ability_bullet_image_down>" + ability.ImageAttackDown + "</attack_ability_bullet_image_down>\r\n" +
                             "\t\t\t<attack_ability_bullet_image_left>" + ability.ImageAttackLeft + "</attack_ability_bullet_image_left>\r\n" +
                             "\t\t\t<attack_ability_bullet_image_right>" + ability.ImageAttackRight + "</attack_ability_bullet_image_right>\r\n" +
                             "\t\t</attack_ability>\r\n";
                }
                else
                {
                    int totalDuration = ability.Duration * 56;

                    total += "\t\t<augmentation_ability>\r\n" +
                             "\t\t\t<augmentation_ability_image>" + ability.ImageAbility + "</augmentation_ability_image>\r\n" +
                             "\t\t\t<augmentation_ability_affect>" + ability.Affect + "</augmentation_ability_affect>\r\n" +
                             "\t\t\t<augmentation_ability_effect>" + ability.Effect + "</augmentation_ability_effect>\r\n" +
                             "\t\t\t<augmentation_ability_mana>" + ability.Mana + "</augmentation_ability_mana>\r\n" +
                             "\t\t\t<augmentation_ability_duration>" + totalDuration + "</augmentation_ability_duration>\r\n" +
                             "\t\t</augmentation_ability>\r\n";
                }
            }

            total += "\t</character>\r\n";

            return total;
        }
    }
}

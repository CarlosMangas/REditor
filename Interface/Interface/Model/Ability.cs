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

namespace Interface.Model
{
    /// <summary>
    /// Model class to save the information of an ability.
    /// </summary>
    public class Ability
    {
        #region Definitions      
        public string Type { get; set; }

        public string Affect { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; }

        public string Speed { get; set; }

        public int Effect { get; set; }

        public int Mana { get; set; }

        public string ImageAbility { get; set; }

        public string ImageAttackUp { get; set; }

        public string ImageAttackDown { get; set; }

        public string ImageAttackLeft { get; set; }

        public string ImageAttackRight { get; set; }
        #endregion

        public Ability(string name, string type, int effect, int mana, string imageAbility, string imageAttackUp, string imageAttackDown,
                        string imageAttackLeft, string imageAttackRight, string affect, int duration,string speed)
        {
            this.Name = name;
            this.Type = type;
            this.Effect = effect;
            this.Mana = mana;
            this.ImageAbility = imageAbility;
            this.ImageAttackUp = imageAttackUp;
            this.ImageAttackDown = imageAttackDown;
            this.ImageAttackLeft = imageAttackLeft;
            this.ImageAttackRight = imageAttackRight;
            this.Affect = affect;
            this.Duration = duration;
            this.Speed = speed;
        }

        /// <summary>
        /// Builds a string of the ability's information.
        /// </summary>
        /// <returns>String for the general information text box</returns>
        public string GetGeneralInfoString()
        {
            string aux = "Name: " + Name + "\n";
            aux += "Type: " + Type + "\n";

            if (Type == "attack")
            {
                aux += "Damage: " + Effect + "\n";
            }
            else
            {
                aux += "Effect: " + Effect + "\n";
            }

            if (Type == "augmentation")
            {
                aux += "Affect: " + Affect + "\n";
                aux += "Duration: " + Duration + " s" + "\n";
            }

            aux += "Mana Points: " + Mana + "\n";

            aux += "Image path:" + ImageAbility + "\n";

            if (Type == "attack")
            {
                aux += "Attack up path: " + ImageAttackUp + "\n";
                aux += "Attack down path: " + ImageAttackDown + "\n";
                aux += "Attack left path: " + ImageAttackLeft + "\n";
                aux += "Attack right path: " + ImageAttackRight + "\n";
                aux += "Speed: " + Speed + "\n";
            }

            return aux;
        }

    }
}

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

namespace Interface.Model
{
    /// <summary>
    /// Model class to save the information of a weapon.
    /// </summary>
    public class Weapon
    {
        #region Definitions
        public string Type { get; set; }

        public String Speed { get; set; }

        public String Name { get; set; }

        public int Damage { get; set; }

        public string ImageWeapon { get; set; }

        public string ImageArrowUp { get; set; }

        public string ImageArrowDown { get; set; }

        public string ImageArrowLeft { get; set; }

        public string ImageArrowRight { get; set; }

        private int positionX, positionY;

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public Image CurrentImage { get; set; }
        #endregion

        public Weapon(string name, string type, int damage, string speed,string imageWeapon, string imageArrowUp, string imageArrowDown, 
                        string imageArrowLeft, string imageArrowRight)
        {
            this.Name = name;
            this.Type = type;
            this.Damage = damage;
            this.Speed = speed;
            this.ImageWeapon = imageWeapon;
            this.ImageArrowUp = imageArrowUp;
            this.ImageArrowDown = imageArrowDown;
            this.ImageArrowLeft = imageArrowLeft;
            this.ImageArrowRight = imageArrowRight;
        }

        /// <summary>
        /// Builds a string of the weapon's information.
        /// </summary>
        /// <returns>String for the general information text box</returns>
        public string GetGeneralInfoString()
        {
            string aux = "Name: " + Name + "\n";
            aux += "Damage: " + Damage + "\n";
            aux += "Speed: " + Speed + "\n";
            aux += "Image: " + ImageWeapon + "\n";

            if (ImageArrowDown != null)
            {
                aux += "Arrow up path: " + ImageArrowUp + "\n";
                aux += "Arrow down path: " + ImageArrowDown + "\n";
                aux += "Arrow left path: " + ImageArrowLeft + "\n";
                aux += "Arrow right path: " + ImageArrowRight + "\n";
            }

            return aux;
        }
    }
}

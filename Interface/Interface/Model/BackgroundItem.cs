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
    /// Model class to save the information of a background item.
    /// </summary>
    public class BackgroundItem
    {

        #region Definitions
        public String Name { get; set; }

        public string Solid { get; set; }

        public string BulletProof { get; set; }

        public string Image { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public Image CurrentImage { get; set; }
        #endregion

        public BackgroundItem(string name, string solid, string bulletProof, string image)
        {
            this.Name = name;
            this.Solid = solid;
            this.BulletProof = bulletProof;
            this.Image = image;
        }

        /// <summary>
        /// Builds a string of the background item's information.
        /// </summary>
        /// <returns>String for the general information text box</returns>
        public string GetGeneralInfoString()
        {
            string aux = "Name: " + Name + "\n";
            aux += "Image: " + Image + "\n";
            aux += "Solid: " + Solid + "\n";
            aux += "Bullet Proof: " + BulletProof + "\n";
            
            return aux;
        }
    }
}

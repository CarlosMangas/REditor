﻿/*    
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
    /// Model class to save the information of an Enemy
    /// </summary>
    public class Enemy
    {
        #region Definitions
        public String Name { get; set; }

        public string ImageMovingUp { get; set; }

        public string ImageMovingDown { get; set; }

        public string ImageMovingLeft { get; set; }

        public string ImageMovingRight { get; set; }

        public string ImageArrowUp { get; set; }

        public string ImageArrowDown { get; set; }

        public string ImageArrowLeft { get; set; }

        public string ImageArrowRight { get; set; }

        public string Speed { get; set; }

        public int FrameCount { get; set; }

        public int Strenght { get; set; }

        public int Intelligence { get; set; }

        public int Dexterity { get; set; }

        public int HitPoints { get; set; }

        public string Loot { get; set; }

        public string Behavoir { get; set; }

        public int PatrolZone { get; set; }

        public int DetectZone { get; set; }

        public string Preference { get; set; }

        public string Boss { get; set; }

        public string InitialState { get; set; }

        public string PreviewImage { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public Image CurrentImage { get; set; }
        #endregion

        public Enemy(string name, int strenght, int intelligence, int dexterity, int hitPoints, string imageMovingUp,
                       string imageMovingDown, string imageMovingLeft, string imageMovingRight, int frameCount, string imageArrowUp,
                       string imageArrowDown, string imageArrowLeft, string imageArrowRight, string loot, string behavoir, string preference,
                       string boss, string initialState, int patrolZone, int detectZone, string speed)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.Intelligence = intelligence;
            this.Dexterity = dexterity;
            this.HitPoints = hitPoints;
            this.ImageMovingUp = imageMovingUp;
            this.ImageMovingDown = imageMovingDown;
            this.ImageMovingLeft = imageMovingLeft;
            this.ImageMovingRight = imageMovingRight;
            this.ImageArrowUp = imageArrowUp;
            this.ImageArrowDown = imageArrowDown;
            this.ImageArrowLeft = imageArrowLeft;
            this.ImageArrowRight = imageArrowRight;
            this.FrameCount = frameCount;
            this.Loot = loot;
            this.InitialState = initialState;
            this.Preference = preference;
            this.Behavoir = behavoir;
            this.Boss = boss;
            this.PatrolZone = patrolZone;
            this.DetectZone = detectZone;
            this.Speed = speed;
        }

        /// <summary>
        /// Builds a string of the enemy's information
        /// </summary>
        /// <returns>String for the general information text box</returns>
        public string GetGeneralInfoString()
        {
            string aux = "Name: " + Name + "\n";
            aux += "Strenght: " + Strenght + "\n";
            aux += "Intelligence: " + Intelligence + "\n";
            aux += "Dexterity: " + Dexterity + "\n";
            aux += "Hit Points: " + HitPoints + "\n";
            aux += "Speed: " + Speed + "\n";
            aux += "Moving up path: " + ImageMovingUp + "\n";
            aux += "Moving down path: " + ImageMovingDown + "\n";
            aux += "Moving left path: " + ImageMovingLeft + "\n";
            aux += "Moving right path: " + ImageMovingRight + "\n";

            if (ImageArrowDown != null)
            {
                aux += "Arrow up path: " + ImageArrowUp + "\n";
                aux += "Arrow down path: " + ImageArrowDown + "\n";
                aux += "Arrow left path: " + ImageArrowLeft + "\n";
                aux += "Arrow right path: " + ImageArrowRight + "\n";
            }

            aux += "IA behavoir: " + Behavoir + "\n";

            if (Behavoir == "patrolling")
            {
                aux += "Patrol Zone: " + PatrolZone + "\n";
            }

            aux += "IA preference: " + Preference + "\n";
            aux += "Detect Zone: " + DetectZone + "\n";
            aux += "Final boss: " + Boss + "\n";

            if (Loot != null)
            {
                aux += "Loot: " + Loot + "\n";
            }
            
            aux += "Initial direction: " + InitialState + "\n";

            return aux;
        }
    }
}

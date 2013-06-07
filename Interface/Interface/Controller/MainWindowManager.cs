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
using Interface.Model;

namespace Interface.Controller
{
    /// <summary>
    /// This class handles all the game elements made in the main window
    /// </summary>
    public class MainWindowManager
    {
        #region Definitions
        private Character player;
        public Character Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }

        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }
            set
            {
                enemies = value;
            }
        }

        private List<BackgroundItem> bgItems;
        public List<BackgroundItem> BgItems
        {
            get
            {
                return bgItems;
            }
            set
            {
                bgItems = value;
            }
        }

        private List<Weapon> weapons;
        public List<Weapon> Weapons
        {
            get
            {
                return weapons;
            }
            set
            {
                weapons = value;
            }
        }

        private List<Ability> abilities;
        public List<Ability> Abilities
        {
            get
            {
                return abilities;
            }
            set
            {
                abilities = value;
            }
        }

        private List<Door> doors;
        public List<Door> Doors
        {
            get
            {
                return doors;
            }
            set
            {
                doors = value;
            }
        }

        private List<KeyItem> keys;
        public List<KeyItem> Keys
        {
            get
            {
                return keys;
            }
            set
            {
                keys = value;
            }
        }

        private List<Consumable> consumables;
        public List<Consumable> Consumables
        {
            get
            {
                return consumables;
            }
            set
            {
                consumables = value;
            }
        }

        private List<NPC> nonPlayers;
        public List<NPC> NonPlayers
        {
            get
            {
                return nonPlayers;
            }
            set
            {
                nonPlayers = value;
            }
        }

        private string deathScreen;
        public string DeathScreen
        {
            get
            {
                return deathScreen;
            }
            set
            {
                deathScreen = value;
            }
        }

        private string endScreen;
        public string EndScreen
        {
            get
            {
                return endScreen;
            }
            set
            {
                endScreen = value;
            }
        }

        private string keyUp;
        public string KeyUp
        {
            get
            {
                return keyUp;
            }
            set
            {
                keyUp = value;
            }
        }

        private string keyDown;
        public string KeyDown
        {
            get
            {
                return keyDown;
            }
            set
            {
                keyDown = value;
            }
        }

        private string keyLeft;
        public string KeyLeft
        {
            get
            {
                return keyLeft;
            }
            set
            {
                keyLeft = value;
            }
        }

        private string keyRight;
        public string KeyRight
        {
            get
            {
                return keyRight;
            }
            set
            {
                keyRight = value;
            }
        }

        private string keyChangeMelee;
        public string KeyChangeMelee
        {
            get
            {
                return keyChangeMelee;
            }
            set
            {
                keyChangeMelee = value;
            }
        }

        private string keyChangeDistance;
        public string KeyChangeDistance
        {
            get
            {
                return keyChangeDistance;
            }
            set
            {
                keyChangeDistance = value;
            }
        }

        private string keyChangeAbility;
        public string KeyChangeAbility
        {
            get
            {
                return keyChangeAbility;
            }
            set
            {
                keyChangeAbility = value;
            }
        }

        private string keyAttack;
        public string KeyAttack
        {
            get
            {
                return keyAttack;
            }
            set
            {
                keyAttack = value;
            }
        }

        private string keyShoot;
        public string KeyShoot
        {
            get
            {
                return keyShoot;
            }
            set
            {
                keyShoot = value;
            }
        }

        private string keyUseAbility;
        public string KeyUseAbility
        {
            get
            {
                return keyUseAbility;
            }
            set
            {
                keyUseAbility = value;
            }
        }

        private string keyAction;
        public string KeyAction
        {
            get
            {
                return keyAction;
            }
            set
            {
                keyAction = value;
            }
        }

        private List<Stage> stages;
        public List<Stage> Stages
        {
            get
            {
                return stages;
            }
            set
            {
                stages = value;
            }
        }

        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int DoorID { get; set; }

        #endregion 

        public MainWindowManager()
        {
            enemies = new List<Enemy>();
            consumables = new List<Consumable>();
            nonPlayers = new List<NPC>();
            weapons = new List<Weapon>();
            abilities = new List<Ability>();
            doors = new List<Door>();
            keys = new List<KeyItem>();
            bgItems = new List<BackgroundItem>();
            stages = new List<Stage>();

            id = -1;

            keyUp = "W";
            keyDown = "S";
            keyLeft = "A";
            keyRight = "D";
            keyAttack = "J";
            keyShoot = "K";
            keyUseAbility = "L";
            keyChangeMelee = "Q";
            keyChangeDistance = "E";
            keyChangeAbility = "R";
            keyAction = "Space";
        }

        /// <summary>
        /// Search for an enemy in the enemy list.
        /// </summary>
        /// <param name="name">Name of the enemy</param>
        /// <returns>Enemy</returns>
        public Enemy GetEnemy(string name)
        {
            foreach (Enemy auxEnemy in enemies)
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
            foreach (Consumable auxConsumable in consumables)
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
            foreach (Weapon auxWeapon in weapons)
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
            foreach (NPC auxNPC in nonPlayers)
            {
                if (auxNPC.Name == name)
                {
                    return auxNPC;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an ability in the ability list.
        /// </summary>
        /// <param name="name">Name of the ability</param>
        /// <returns>Ability</returns>
        public Ability GetAbility(string name)
        {
            foreach (Ability auxAbility in abilities)
            {
                if (auxAbility.Name == name)
                {
                    return auxAbility;
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
            foreach (Door auxDoor in doors)
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
            foreach (KeyItem auxKey in keys)
            {
                if (auxKey.Name == name)
                {
                    return auxKey;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an wall in the wall list.
        /// </summary>
        /// <param name="name">Name of the wall</param>
        /// <returns>Wall</returns>
        public BackgroundItem GetBgItem(string name)
        {
            foreach (BackgroundItem auxBgItem in bgItems)
            {
                if (auxBgItem.Name == name)
                {
                    return auxBgItem;
                }
            }

            return null;
        }

        /// <summary>
        /// Search for an stage in the stage list.
        /// </summary>
        /// <param name="ident">Idenftification number of the stage</param>
        /// <returns>Stage</returns>
        public Stage GetStage(int ident)
        {
            foreach (Stage auxStage in stages)
            {
                if (auxStage.ID == ident)
                {
                    return auxStage;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if there is a name repeated in any list.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Bool</returns>
        public bool NameRepeated(string name)
        {
            foreach (Ability auxAbility in abilities)
            {
                if (name == auxAbility.Name)
                {
                    return true;
                }
            }
            if (Player != null)
            {
                if (Player.Name == name)
                {
                    return true;
                }
            }
            foreach (Enemy auxEnemy in enemies)
            {
                if (name == auxEnemy.Name)
                {
                    return true;
                }
            }
            foreach (Consumable auxConsumable in consumables)
            {
                if (name == auxConsumable.Name)
                {
                    return true;
                }
            }
            foreach (BackgroundItem auxBgItem in bgItems)
            {
                if (name == auxBgItem.Name)
                {
                    return true;
                }
            }
            foreach (Weapon auxWeapon in weapons)
            {
                if (name == auxWeapon.Name)
                {
                    return true;
                }
            }
            foreach (Door auxDoor in doors)
            {
                if (name == auxDoor.Name)
                {
                    return true;
                }
            }
            foreach (KeyItem auxKey in keys)
            {
                if (name == auxKey.Name)
                {
                    return true;
                }
            }
            foreach (NPC auxNPC in nonPlayers)
            {
                if (name == auxNPC.Name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the name introduced is a number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Bool</returns>
        public bool CheckIfNumber(string name)
        {
            double number;

            bool isNum = double.TryParse(name, out number);

            if (isNum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Builds a string for the final screens.
        /// </summary>
        /// <returns>String for the DSL</returns>
        public override string ToString()
        {
            string total;

            if (!String.IsNullOrEmpty(DeathScreen))
            {
                DeathScreen = DeathScreen.Replace('\\', '/');
            }

            if (!String.IsNullOrEmpty(EndScreen))
            {
                EndScreen = EndScreen.Replace('\\', '/');
            }
            
            total = "\t<screens>\r\n" +
                        "\t\t<screen_end_game>" + EndScreen + "</screen_end_game>\r\n" +
                        "\t\t<screen_dead_game>" + DeathScreen + "</screen_dead_game>\r\n" +
                        "\t</screens>\r\n";

            return total;
        }

    }
}

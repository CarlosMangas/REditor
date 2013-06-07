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
using System.IO;

namespace Analyzer
{
    /// <summary>
    /// This class launchs the analyzer.
    /// </summary>
    public class Launcher
    {
        private string gameCode;
        private string sourceFolder;
        private string destFolder;

       //@"C:\Users\Carlos\Documents\Visual Studio 2010\Projects\Analyzer\game.xml",
       //@"C:\Users\Carlos\Documents\Visual Studio 2010\Projects\Template",
       //@"C:\Users\Carlos\Documents\New REditor Game"

        public Launcher(string gameCode, string sourceFolder, string destFolder)
        {
            this.gameCode = gameCode;
            this.sourceFolder = sourceFolder;
            this.destFolder = destFolder;

            Compiler compiler = new Compiler(gameCode, sourceFolder, destFolder);
        }

        public static void Main(string[] args)
        {
            new Launcher(args[0], args[1], args[2]);
        }

        /// <summary>
        /// Returns the current path of the analyzer.
        /// </summary>
        /// <returns></returns>
        public string returnPath()
        {
            string folder = Environment.CurrentDirectory;
            return folder;
        }
    }
}

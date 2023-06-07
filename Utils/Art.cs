using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeHarvest.Utils
{
    internal class Art
    {
        private static string Logo = @"  __  __                     _    _                           _   
 |  \/  |                   | |  | |                         | |  
 | \  / | ___ _ __ ___   ___| |__| | __ _ _ ____   _____  ___| |_ 
 | |\/| |/ _ \ '_ ` _ \ / _ \  __  |/ _` | '__\ \ / / _ \/ __| __|
 | |  | |  __/ | | | | |  __/ |  | | (_| | |   \ V /  __/\__ \ |_ 
 |_|  |_|\___|_| |_| |_|\___|_|  |_|\__,_|_|    \_/ \___||___/\__|";

        public static void PrintLogo()
        {
            Colorful.Console.WriteLine(Logo, Color.Aqua);
            Colorful.Console.WriteLine("\t\tMemeHarvester made by Serialized.", Color.HotPink);
        }
    }
}

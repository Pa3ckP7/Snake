using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        public static bool Quit { get; set; } = false;
        static void Main(string[] args)
        {
            while (Quit == false)
            {
                Main_Menu.Menu();
            }
        }
    }
}

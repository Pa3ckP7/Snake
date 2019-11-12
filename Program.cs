using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static bool Quit = false;
        static void Main(string[] args)
        {
            while (Quit == false)
            {
                Main_Menu.Menu();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Main_Menu
    {
        static int ScreenWidth = 236;
        static int ScreenHeight = 62;
        static int cursorx = (Console.WindowWidth / 2);
        static int cursory = (Console.WindowHeight / 2) - 8;
        public static void Menu()
        {
            Console.CursorVisible = true;
            bool chosen = false;
            Console.WindowWidth = ScreenWidth;
            Console.WindowHeight = ScreenHeight;
            Menu_draw();
            Console.SetCursorPosition((Console.WindowWidth / 2) -1, (Console.WindowHeight / 2) - 8);
            while (chosen == false)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key.Equals(ConsoleKey.UpArrow))
                    {
                        if (cursory != (Console.WindowHeight / 2) - 8) 
                        {
                            cursory--;
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.DownArrow))
                    {
                        if (cursory != (Console.WindowHeight / 2) - 6)
                        {
                            cursory++;
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.Enter))
                    {
                        chosen = true;
                        Action();
                    }

                }
            }
            chosen = false;

        }

        private static void Menu_draw()
        {
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 10);
            Console.WriteLine("Snake game || by Pa3ckP7");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 8);
            Console.WriteLine(String.Format("{0,15}", "1Player"));
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 7);
            Console.WriteLine(String.Format("{0,16}", "2Players"));
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) - 6);
            Console.WriteLine(String.Format("{0,12}", "Quit"));
        }
        public static void Action() 
        {
            if (cursory == (Console.WindowHeight / 2) - 8)
            {
                Console.Clear();
                SnakeGame.Start();
            }
            else if (cursory == (Console.WindowHeight / 2) - 7)
            {
                Console.Clear();
                SnakeGame2P.Start();
            }
            else if (cursory == (Console.WindowHeight / 2) - 6) 
            {
                Program.Quit = true;
            }

        }
    }
}

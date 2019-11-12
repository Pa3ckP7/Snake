using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class MultGO
    {
        static int ScreenWidth = 236;
        static int ScreenHeight = 62;
        static int cursorx = (Console.WindowWidth / 2) - 14;
        static int cursory = (Console.WindowHeight / 2) - 7;
        static bool Retry;
        public static void MultiplayerGameOver()
        {
            Console.CursorVisible = true;
            bool chosen = false;
            Console.WindowWidth = ScreenWidth;
            Console.WindowHeight = ScreenHeight;
            Menu_draw();
            Console.SetCursorPosition((Console.WindowWidth / 2) - 14, (Console.WindowHeight / 2) - 7);
            while (chosen == false)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key.Equals(ConsoleKey.RightArrow))
                    {
                        if (cursorx != (Console.WindowWidth / 2))
                        {
                            cursorx = (Console.WindowWidth / 2);
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.LeftArrow))
                    {
                        if (cursorx != (Console.WindowWidth / 2) - 14)
                        {
                            cursorx = (Console.WindowWidth / 2) - 14;
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

            Console.SetCursorPosition((Console.WindowWidth / 2) -13, (Console.WindowHeight / 2) - 7);
            Console.WriteLine(String.Format("{0,2} {1,10}", "Rematch", "Quit"));


        }
        public static void Action()
        {
            if (cursorx == (Console.WindowWidth / 2) - 14)
            {
                Console.Clear();
                SnakeGame2P.Start(); 
            }
            else if (cursorx == (Console.WindowWidth / 2))
            {
                Console.Clear();
            }

        }
    }
}

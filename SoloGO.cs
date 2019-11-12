using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SoloGO
    {
        static int ScreenWidth = 236;
        static int ScreenHeight = 62;
        static int cursorx = (Console.WindowWidth / 2)-8;
        static int cursory = (Console.WindowHeight / 2) - 7;
        static bool Retry;
        public static void SoloGameOver()
        {
            Console.CursorVisible = true;
            bool chosen = false;
            Console.WindowWidth = ScreenWidth;
            Console.WindowHeight = ScreenHeight;
            Menu_draw();
            Console.SetCursorPosition((Console.WindowWidth / 2)-8, (Console.WindowHeight / 2) - 7);
            while (chosen == false)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key.Equals(ConsoleKey.RightArrow))
                    {
                        if (cursorx != (Console.WindowWidth / 2)+4)
                        {
                            cursorx= (Console.WindowWidth / 2) + 4;
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.LeftArrow))
                    {
                        if (cursorx != (Console.WindowWidth / 2) - 8)
                        {
                            cursorx= (Console.WindowWidth / 2)-8;
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
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) - 7);
            Console.WriteLine(String.Format("{0,2} {1,10}", "Retry", "Quit"));
            
            
        }
        public static void Action()
        {
            if (cursorx == (Console.WindowWidth / 2) - 8)
            {
                Console.Clear();
                SnakeGame.Start();
            }
            else if (cursorx == (Console.WindowWidth / 2) + 4) 
            {
                Console.Clear();
            }

        }
    }
}

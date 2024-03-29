﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeGame
    {
        
        static DateTime Timer1, Timer2,Wait1,Wait2;
        static Random rng = new Random();
        static List<Pixel> SnakeBody = new List<Pixel>();
        static ConsoleColor SegmentColor = ConsoleColor.Green;
        static ConsoleColor empty = ConsoleColor.Black;
        static readonly double timestep = 50;// in miliseconds
        static bool GameOver = false;
        static bool KeyPressed = false;
        static int StartingSize = 5;
        static int Score = 0;
        static int ScreenWidth = 236;
        static int ScreenHeight = 62;
        static Moving_directions curent_direction = Moving_directions.None;
        static Pixel SnakeHead = new Pixel((ScreenWidth) / 2, (ScreenHeight) / 2, ConsoleColor.DarkYellow);
        static Pixel head_clear = new Pixel(SnakeHead);
        static Pixel body_clear = new Pixel();
        static Pixel Berry = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(1, ScreenHeight - 2), ConsoleColor.Red);
        public enum Moving_directions
        {
            None,//0
            Up,//1
            Left,//2
            Down,//3
            Right//4
        }
        
        public static void Start()
        {
            Console.Title = "Snake game || by Pa3ckP7";
            Console.CursorVisible = false;
            Console.WindowWidth = ScreenWidth;
            Console.WindowHeight = ScreenHeight;
            DrawWalls();
            SnakeHead = new Pixel((ScreenWidth) / 2, (ScreenHeight) / 2, ConsoleColor.DarkYellow);
            do
            {
                Berry = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
            } while (Berry.x%2!=0.0);
            Game();
        }

        private static void DrawWalls()
        {
            //izris zgornjega in spodnjega zidu zidu
            for (int x = 0; x < ScreenWidth-1; x++)
            {
                Pixel wall = new Pixel(x, 1);
                Pixel.drawPixel(wall);

                wall = new Pixel(x, ScreenHeight - 2);
                Pixel.drawPixel(wall);

            }
            //izris levega in desnega zidu
            for (int y = 1; y < Console.WindowHeight-1; y++)
            {
                Pixel wall = new Pixel(0, y);
                Pixel.drawPixel(wall);

                wall = new Pixel(ScreenWidth - 2, y);
                Pixel.drawPixel(wall);

            }
        }
        static void Game()
        {
            while (!GameOver)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"Score: { Convert.ToString(Score)}");
                Pseudoclear();
                Pixel.drawPixel(SnakeHead);
                Pixel.drawPixel(Berry);
                foreach (Pixel segment in SnakeBody)
                {
                    Pixel.drawPixel(segment);
                }
                GetMovementDirection();
                if (curent_direction != Moving_directions.None)
                {
                    Pixel bodySegment = new Pixel(SnakeHead.x, SnakeHead.y, SegmentColor);
                    SnakeBody.Add(bodySegment);
                    if (SnakeBody.Count > Score + StartingSize)
                    {
                        body_clear = new Pixel(SnakeBody[0]);
                        SnakeBody.RemoveAt(0);
                    }
                }

                switch (curent_direction)
                {
                    case Moving_directions.Up:
                        head_clear = new Pixel(SnakeHead);
                        SnakeHead.y--;
                        break;
                    case Moving_directions.Left:
                        head_clear = new Pixel(SnakeHead);
                        SnakeHead.x-=2;
                        break;
                    case Moving_directions.Down:
                        head_clear = new Pixel(SnakeHead);
                        SnakeHead.y++;
                        break;
                    case Moving_directions.Right:
                        head_clear = new Pixel(SnakeHead);
                        SnakeHead.x+=2;
                        break;
                }
                if (SnakeHead.x == Berry.x && SnakeHead.y == Berry.y)
                {
                    Score++;
                    do
                    {
                        Berry = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                    } while (Berry.x % 2 != 0.0);
                }
                
                foreach (Pixel segment in SnakeBody)
                {
                    if (segment.x == SnakeHead.x && segment.y == SnakeHead.y)
                    {
                        GameOver = true;
                        break;
                    }
                }
                if (SnakeHead.x <= 1 || SnakeHead.x == ScreenWidth - 2 || SnakeHead.y == 1 || SnakeHead.y == ScreenHeight-2)
                {
                    GameOver = true;
                }
                ///
            }
            Game_Over();
        }

        private static void GetMovementDirection()
        {
            Timer1 = DateTime.Now;
            KeyPressed = false;
            while (true)
            {
                Timer2 = DateTime.Now;
                if (Timer2.Subtract(Timer1).TotalMilliseconds > timestep)
                {
                    break;
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key.Equals(ConsoleKey.UpArrow) && curent_direction != Moving_directions.Down && KeyPressed == false)
                    {
                        curent_direction = Moving_directions.Up;
                        KeyPressed = true;
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.RightArrow) && curent_direction != Moving_directions.Left && KeyPressed == false)
                    {
                        curent_direction = Moving_directions.Right;
                        KeyPressed = true;
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.DownArrow) && curent_direction != Moving_directions.Up && KeyPressed == false)
                    {
                        curent_direction = Moving_directions.Down;
                        KeyPressed = true;
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.LeftArrow) && curent_direction != Moving_directions.Right && KeyPressed == false)
                    {
                        curent_direction = Moving_directions.Left;
                        KeyPressed = true;
                    }
                }
            }
        }

        static void Pseudoclear()
        {
            Pixel.drawPixel(head_clear.x, head_clear.y, empty);
            Pixel.drawPixel(Berry.x, Berry.y, empty);
            if (body_clear.x != 0 || body_clear.y != 0)
            {
                Pixel.drawPixel(body_clear.x, body_clear.y, empty);
            }
           
        }
        public static void Game_Over() 
        {
            Wait1 = DateTime.Now;
            while (true) 
            {
                Wait2 = DateTime.Now;
                if (Wait2.Subtract(Wait1).TotalSeconds > 3) 
                {
                    break;
                }
            }
            Console.Clear();
            Console.SetCursorPosition((ScreenWidth) / 2-8, (ScreenHeight) / 2-10);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition((ScreenWidth) / 2-8, ((ScreenHeight) / 2) -9);
            Console.WriteLine($"Final score: {Score}");
            Console.SetCursorPosition((ScreenWidth) / 2-8, ((ScreenHeight) / 2) -8);
            Console.WriteLine($"Total snake length: {Score+StartingSize+1}");
            
            
            SnakeBody.Clear();
            Score = 0;
            GameOver = false;
            curent_direction = Moving_directions.None;
            SoloGO.SoloGameOver();

        }
    }
}

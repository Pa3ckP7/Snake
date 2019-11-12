using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeGame2P
    {
        static DateTime Timer1, Timer2, Wait1, Wait2;
        static Random rng = new Random();
        static List<Pixel> SnakeBodyP1 = new List<Pixel>();
        static List<Pixel> SnakeBodyP2 = new List<Pixel>();
        static ConsoleColor SegmentColorP1 = ConsoleColor.Green;
        static ConsoleColor SegmentColorP2 = ConsoleColor.DarkBlue;
        static ConsoleColor empty = ConsoleColor.Black;
        static readonly double timestep = 100;// in miliseconds
        static bool GameOver = false;
        static int StartingSize = 5;
        static int ScoreP1 = 0;
        static int ScoreP2 = 0;
        static int ScreenWidth = 236;
        static int ScreenHeight = 62;
        static Moving_directions curent_directionP1 = Moving_directions.Left;
        static Moving_directions curent_directionP2 = Moving_directions.Right;
        static Winner winner = Winner.None;
        static Pixel SnakeHeadP1 = new Pixel(Console.WindowWidth-10, Console.WindowHeight-30, ConsoleColor.DarkYellow);
        static Pixel head_clearP1 = new Pixel(SnakeHeadP1);
        static Pixel body_clearP1 = new Pixel();
        static Pixel SnakeHeadP2 = new Pixel(10, 30, ConsoleColor.DarkYellow);
        static Pixel head_clearP2 = new Pixel(SnakeHeadP2);
        static Pixel body_clearP2 = new Pixel();
        static Pixel Berry1 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(1, ScreenHeight - 2), ConsoleColor.Red);
        static Pixel Berry2 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(1, ScreenHeight - 2), ConsoleColor.Red);
        static Pixel Berry3 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(1, ScreenHeight - 2), ConsoleColor.Red);
        static Pixel Berry4 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(1, ScreenHeight - 2), ConsoleColor.Red);
        public enum Moving_directions
        {
            None,//0
            Up,//1
            Left,//2
            Down,//3
            Right//4
        }
        public enum Winner 
        {
            None,
            P1,
            P2,
            Draw
        }

        public static void Start()
        {
            Console.Title = "Snake game || by Pa3ckP7";
            Console.CursorVisible = false;
            Console.WindowWidth = ScreenWidth;
            Console.WindowHeight = ScreenHeight;
            DrawWalls();
            SnakeHeadP1 = new Pixel(Console.WindowWidth - 10, Console.WindowHeight - 30, ConsoleColor.DarkYellow);
            SnakeHeadP2 = new Pixel(10, 30, ConsoleColor.DarkYellow);
            Instatiate_Berries();
            Game();
        }

        private static void Instatiate_Berries()
        {
            do
            {
                Berry1 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
            } while (Berry1.x % 2 != 0.0);
            do
            {
                Berry2 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
            } while (Berry2.x % 2 != 0.0);
            do
            {
                Berry3 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
            } while (Berry3.x % 2 != 0.0);
            do
            {
                Berry4 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
            } while (Berry4.x % 2 != 0.0);
        }

        private static void DrawWalls()
        {
            //izris zgornjega in spodnjega zidu zidu
            for (int x = 0; x < ScreenWidth - 1; x++)
            {
                Pixel wall = new Pixel(x, 1);
                Pixel.drawPixel(wall);

                wall = new Pixel(x, ScreenHeight - 2);
                Pixel.drawPixel(wall);

            }
            //izris levega in desnega zidu
            for (int y = 1; y < Console.WindowHeight - 1; y++)
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
                Console.Write(String.Format("{0,0} {1,30}",$"P1 score: { Convert.ToString(ScoreP1)}", $"P2 score: { Convert.ToString(ScoreP2)}"));
                Pseudoclear();
                Pixel.drawPixel(SnakeHeadP1);
                Pixel.drawPixel(SnakeHeadP2);
                Pixel.drawPixel(Berry1);
                Pixel.drawPixel(Berry2);
                Pixel.drawPixel(Berry3);
                Pixel.drawPixel(Berry4);
                foreach (Pixel segment in SnakeBodyP1)
                {
                    Pixel.drawPixel(segment);
                }
                foreach (Pixel segment in SnakeBodyP2)
                {
                    Pixel.drawPixel(segment);
                }
                GetMovementDirection();
                if (curent_directionP1 != Moving_directions.None)
                {
                    Pixel bodySegment = new Pixel(SnakeHeadP1.x, SnakeHeadP1.y, SegmentColorP1);
                    SnakeBodyP1.Add(bodySegment);
                    if (SnakeBodyP1.Count > ScoreP1 + StartingSize)
                    {
                        body_clearP1 = new Pixel(SnakeBodyP1[0]);
                        SnakeBodyP1.RemoveAt(0);
                    }
                }
                if (curent_directionP2 != Moving_directions.None)
                {
                    Pixel bodySegment = new Pixel(SnakeHeadP2.x, SnakeHeadP2.y, SegmentColorP2);
                    SnakeBodyP2.Add(bodySegment);
                    if (SnakeBodyP2.Count > ScoreP2 + StartingSize)
                    {
                        body_clearP2 = new Pixel(SnakeBodyP2[0]);
                        SnakeBodyP2.RemoveAt(0);
                    }
                }

                Move();

                BerryCollisionDetection();

                foreach (Pixel segment in SnakeBodyP1)
                {
                    if (segment.x == SnakeHeadP1.x && segment.y == SnakeHeadP1.y)
                    {
                        GameOver = true;
                        winner = Winner.P2;
                        break;
                    }
                }
                foreach (Pixel segment in SnakeBodyP2)
                {
                    if (segment.x == SnakeHeadP1.x && segment.y == SnakeHeadP1.y)
                    {
                        GameOver = true;
                        winner = Winner.P2;
                        break;
                    }
                }

                foreach (Pixel segment in SnakeBodyP1)
                {
                    if (segment.x == SnakeHeadP2.x && segment.y == SnakeHeadP2.y)
                    {
                        GameOver = true;
                        winner = Winner.P1;
                        break;
                    }
                }
                foreach (Pixel segment in SnakeBodyP2)
                {
                    if (segment.x == SnakeHeadP2.x && segment.y == SnakeHeadP2.y)
                    {
                        GameOver = true;
                        winner = Winner.P1;
                        break;
                    }
                }
                if (SnakeHeadP2.x <= 1 || SnakeHeadP2.x == ScreenWidth - 2 || SnakeHeadP2.y == 1 || SnakeHeadP2.y == ScreenHeight - 2)
                {
                    GameOver = true;
                    winner = Winner.P1;
                }
                if (SnakeHeadP1.x <= 1 || SnakeHeadP1.x == ScreenWidth - 2 || SnakeHeadP1.y == 1 || SnakeHeadP1.y == ScreenHeight - 2)
                {
                    GameOver = true;
                    winner = Winner.P2;
                }
                if (SnakeHeadP1.x == SnakeHeadP2.x && SnakeHeadP1.y == SnakeHeadP2.y) 
                {
                    GameOver = true;
                    winner = Winner.Draw;
                }

            }
            Game_Over();
        }

        private static void Move()
        {
            switch (curent_directionP1)
            {
                case Moving_directions.Up:
                    head_clearP1 = new Pixel(SnakeHeadP1);
                    SnakeHeadP1.y--;
                    break;
                case Moving_directions.Left:
                    head_clearP1 = new Pixel(SnakeHeadP1);
                    SnakeHeadP1.x -= 2;
                    break;
                case Moving_directions.Down:
                    head_clearP1 = new Pixel(SnakeHeadP1);
                    SnakeHeadP1.y++;
                    break;
                case Moving_directions.Right:
                    head_clearP1 = new Pixel(SnakeHeadP1);
                    SnakeHeadP1.x += 2;
                    break;
            }
            switch (curent_directionP2)
            {
                case Moving_directions.Up:
                    head_clearP2 = new Pixel(SnakeHeadP2);
                    SnakeHeadP2.y--;
                    break;
                case Moving_directions.Left:
                    head_clearP2 = new Pixel(SnakeHeadP2);
                    SnakeHeadP2.x -= 2;
                    break;
                case Moving_directions.Down:
                    head_clearP2 = new Pixel(SnakeHeadP2);
                    SnakeHeadP2.y++;
                    break;
                case Moving_directions.Right:
                    head_clearP2 = new Pixel(SnakeHeadP2);
                    SnakeHeadP2.x += 2;
                    break;
            }
        }

        private static void BerryCollisionDetection()
        {
            if (SnakeHeadP1.x == Berry1.x && SnakeHeadP1.y == Berry1.y)
            {
                ScoreP1++;
                do
                {
                    Berry1 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry1.x % 2 != 0.0);
            }
            if (SnakeHeadP1.x == Berry2.x && SnakeHeadP1.y == Berry2.y)
            {
                ScoreP1++;
                do
                {
                    Berry2 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry2.x % 2 != 0.0);
            }
            if (SnakeHeadP1.x == Berry3.x && SnakeHeadP1.y == Berry3.y)
            {
                ScoreP1++;
                do
                {
                    Berry3 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry3.x % 2 != 0.0);
            }
            if (SnakeHeadP1.x == Berry4.x && SnakeHeadP1.y == Berry4.y)
            {
                ScoreP1++;
                do
                {
                    Berry4 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry4.x % 2 != 0.0);
            }
            
            if (SnakeHeadP2.x == Berry1.x && SnakeHeadP2.y == Berry1.y)
            {
                ScoreP2++;
                do
                {
                    Berry1 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry1.x % 2 != 0.0);
            }
            if (SnakeHeadP2.x == Berry2.x && SnakeHeadP2.y == Berry2.y)
            {
                ScoreP2++;
                do
                {
                    Berry2 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry2.x % 2 != 0.0);
            }
            if (SnakeHeadP2.x == Berry3.x && SnakeHeadP2.y == Berry3.y)
            {
                ScoreP2++;
                do
                {
                    Berry3 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry3.x % 2 != 0.0);
            }
            if (SnakeHeadP2.x == Berry4.x && SnakeHeadP2.y == Berry4.y)
            {
                ScoreP2++;
                do
                {
                    Berry4 = new Pixel(rng.Next(2, ScreenWidth - 2), rng.Next(2, ScreenHeight - 2), ConsoleColor.Red);
                } while (Berry4.x % 2 != 0.0);
            }
        }

        private static void GetMovementDirection()
        {
            Timer1 = DateTime.Now;
            
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
                    if (keyInfo.Key.Equals(ConsoleKey.UpArrow) && curent_directionP1 != Moving_directions.Down)
                    {
                        curent_directionP1 = Moving_directions.Up;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.RightArrow) && curent_directionP1 != Moving_directions.Left)
                    {
                        curent_directionP1 = Moving_directions.Right;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.DownArrow) && curent_directionP1 != Moving_directions.Up)
                    {
                        curent_directionP1 = Moving_directions.Down;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.LeftArrow) && curent_directionP1 != Moving_directions.Right)
                    {
                        curent_directionP1 = Moving_directions.Left;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.W) && curent_directionP2 != Moving_directions.Down)
                    {
                        curent_directionP2 = Moving_directions.Up;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.A) && curent_directionP2 != Moving_directions.Right)
                    {
                        curent_directionP2 = Moving_directions.Left;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.S) && curent_directionP2 != Moving_directions.Up)
                    {
                        curent_directionP2 = Moving_directions.Down;
                        
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.D) && curent_directionP2 != Moving_directions.Left)
                    {
                        curent_directionP2 = Moving_directions.Right;
                        
                    }
                }
            }
        }

        static void Pseudoclear()
        {
            Pixel.drawPixel(head_clearP1.x, head_clearP1.y, empty);
            Pixel.drawPixel(Berry1.x, Berry1.y, empty);
            if (body_clearP1.x != 0 || body_clearP1.y != 0)
            {
                Pixel.drawPixel(body_clearP1.x, body_clearP1.y, empty);
            }
            Pixel.drawPixel(head_clearP2.x, head_clearP2.y, empty);
            if (body_clearP2.x != 0 || body_clearP2.y != 0)
            {
                Pixel.drawPixel(body_clearP2.x, body_clearP2.y, empty);
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
            Console.SetCursorPosition((ScreenWidth) / 2 - 8, (ScreenHeight) / 2 - 10);
            switch (winner) 
            {
                case Winner.P1:
                    Console.WriteLine("P1 Wins");
                    break;
                case Winner.P2:
                    Console.WriteLine("P2 Wins");
                    break;
                case Winner.Draw:
                    Console.WriteLine("It's a DRAW");
                    break;
            }
            Console.WriteLine();
            Console.SetCursorPosition((ScreenWidth) / 2 -28, ((ScreenHeight) / 2) - 9);
            Console.WriteLine(String.Format("{0,-24} | {1,24}",$"P1 final score: {ScoreP1}",$"P2 final score: {ScoreP2}"));
            Console.SetCursorPosition((ScreenWidth) / 2 - 28, ((ScreenHeight) / 2) - 8);
            Console.WriteLine(String.Format("{0,-10} | {1,10}",$"P1 total snake length: {ScoreP1 + StartingSize + 1}", $"P2 total snake length: {ScoreP2 + StartingSize + 1}"));


            SnakeBodyP1.Clear();
            ScoreP1 = 0;
            SnakeBodyP2.Clear();
            ScoreP2 = 0;
            GameOver = false;
            curent_directionP1 = Moving_directions.Left;
            curent_directionP2 = Moving_directions.Right;
            winner = Winner.None;
            MultGO.MultiplayerGameOver();

        }
    }
}

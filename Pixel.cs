using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Pixel
    {
        public int x { get; set; }
        public int y { get; set; }
        public ConsoleColor PixelColor;
        public Pixel(int x, int y, ConsoleColor PixelColor) 
        {
            this.x = x;
            this.y = y;
            this.PixelColor = PixelColor;
        }
        public Pixel(int x, int y) 
        {
            this.x = x;
            this.y = y;
            this.PixelColor = ConsoleColor.White;
        }
        public Pixel(Pixel pix) 
        {
            x = pix.x;
            y = pix.y;
            PixelColor = pix.PixelColor;
        }
        public Pixel()
        {

        }
        
        public static void drawPixel(Pixel pixel) 
        {
            Console.ForegroundColor = pixel.PixelColor;
            Console.SetCursorPosition(pixel.x, pixel.y);
            Console.Write("■");
            Console.ResetColor();
        }
        public static void drawPixel(int x, int y, ConsoleColor color) 
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write("■");
            Console.ResetColor();
        }
    }
}

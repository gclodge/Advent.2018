using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent._2018.Classes
{
    public class StarPosition
    {
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;

        public int VelX { get; private set; } = 0;
        public int VelY { get; private set; } = 0;

        public StarPosition(string input)
        {
            //< position=< 9,  1> velocity=< 0,  2>
            var arr = input.Split('=');

            var pos = ParseValueString(arr[1]);
            var vel = ParseValueString(arr[2]);

            this.X = pos[0];
            this.Y = pos[1];
            this.VelX = vel[0];
            this.VelY = vel[1];
        }

        public void SimulateSteps(int steps)
        {
            this.X += steps * VelX;
            this.Y += steps * VelY;
        }

        static int[] ParseValueString(string str)
        {
            //< "< 9,  1> velocity"
            var clean = str.Substring(str.IndexOf("<") + 1, str.IndexOf(">") - 1);
            //< Now, have "9, 1"
            var vals = clean.Split(new string[] { ", " }, StringSplitOptions.None);
            return vals.Select(int.Parse).ToArray();
        }
    }

    public class StarMessage
    {
        public List<StarPosition> Stars = null;

        public int MinX { get; private set; } = int.MaxValue;
        public int MinY { get; private set; } = int.MaxValue;
        public int MaxX { get; private set; } = int.MinValue;
        public int MaxY { get; private set; } = int.MinValue;

        public int Area => GetArea();

        public StarMessage(IEnumerable<string> input)
        {
            this.Stars = input.Select(x => new StarPosition(x)).ToList();
        }

        public void SimulateSteps(int steps)
        {
            //< Move each Star to their correct position (based on time steps)
            foreach (var star in Stars)
            {
                star.SimulateSteps(steps);
            }
            //< Get the current extrema after the steps
            GetExtrema();
        }

        int GetArea()
        {
            return Math.Abs((MaxX - MinX) * (MaxY - MinY));
        }

        void GetExtrema(bool resetExtrema = true)
        {
            if (resetExtrema)
            {
                this.MinX = int.MaxValue;
                this.MinY = int.MaxValue;
                this.MaxX = int.MinValue;
                this.MaxY = int.MinValue;
            }

            foreach (var star in Stars)
            {
                this.MinX = Math.Min(star.X, MinX);
                this.MinY = Math.Min(star.Y, MinY);
                this.MaxX = Math.Max(star.X, MaxX);
                this.MaxY = Math.Max(star.Y, MaxY);
            }
        }

        public void PrintMessage(string image)
        {
            int w = MaxX - MinX + 1;
            int h = MaxY - MinY + 1;

            int[,] grid = new int[w, h];
            foreach (var star in Stars)
            {
                int x = star.X - MinX;
                int y = star.Y - MinY;

                grid[x, y] = 1;
            }
            
            var bmp = GenerateImage(grid, w, h);
            bmp.Save(image, System.Drawing.Imaging.ImageFormat.Png);
        }

        private static Bitmap GenerateImage(int[,] image, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmp.SetPixel(x, y, GetColor(image[x, y]));
                }
            }
            return bmp;
        }

        public static Color GetColor(int value)
        {
            switch (value)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.White;
                default:
                    return Color.Transparent;
            }
        }
    }
}

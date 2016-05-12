using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakePart
    {
        public int X { get; set; } // X Coordinate for Snake Part
        public int Y { get; set; } // Y Coordinate for Snake Part

        List<SnakePart> snake = new List<SnakePart>();

        const int tile_width = 16;
        const int tile_height = 16;

        bool gameover = false;
        int score = 0;
        int direction = 0; // Down = 0, Left = 1, Right = 2, Up = 3
        SnakePart food_piece = new SnakePart();

        // Constructor
        public SnakePart()
        {
            X = 0;
            Y = 0;
        }

        private void StartGame()
        {
            gameover = false;
            score = 0;
            direction = 0;
            snake.Clear();
            SnakePart head = new SnakePart();
            head.X = 20;
            head.Y = 5;
            snake.Add(head);
            GenerateFood();
        }
    }
}

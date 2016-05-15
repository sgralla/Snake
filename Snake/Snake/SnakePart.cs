﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class SnakePart
    {
        public int X { get; set; } // X Coordinate for Snake Part
        public int Y { get; set; } // Y Coordinate for Snake Part

        // Constructor
        public SnakePart()
        {
            X = 0;
            Y = 0;
        }
    }
}

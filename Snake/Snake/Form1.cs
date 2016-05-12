using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Source: http://forum.chaos-project.com/index.php?topic=12210.0

namespace Snake
{
    public partial class Form1 : Form
    {
        // Variables
        int square_x = 10;
        int square_y = 10;

        public Form1()
        {
            InitializeComponent();

            gameTimer.Interval = 1000 / 60;
            gameTimer.Tick += new EventHandler(Update);
            gameTimer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            // Update Logic here
            if (Input.Pressed(Keys.Right))
                square_x += 4;
            if (Input.Pressed(Keys.Left))
                square_x -= 4;
            if (Input.Pressed(Keys.Up))
                square_y -= 4;
            if (Input.Pressed(Keys.Down))
                square_y += 4;

            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            for (int i = 0; i < snake.Count; i++)
            {
                Brush snake_color = i == 0 ? Brushes.Red : Brushes.Black;
                canvas.FillRectangle(snake_color, new Rectangle(snake[i].X * tile_width, snake[i].Y * tile_height, tile_width, tile_height));
            }

            canvas.FillRectangle(Brushes.Orange, new Rectangle(food_piece.X * tile_width, food_piece.Y * tile_height, tile_width, tile_height));
        }

        private void GenerateFood()
        {
            int max_tile_w = pbCanvas.Size.Width / tile_width;
            int max_tile_h = pbCanvas.Size.Height / tile_height;
            Random random = new Random();
            food_piece = new SnakePart();
            food_piece.X = random.Next(0, max_tile_w);
            food_piece.Y = random.Next(0, max_tile_h);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}

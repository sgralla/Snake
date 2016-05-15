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

        List<SnakePart> snake = new List<SnakePart>();

        const int tile_width = 16;
        const int tile_height = 16;

        bool gameover = false;
        int score = 0;
        int direction = 0; // Down = 0, Left = 1, Right = 2, Up = 3
        SnakePart food_piece = new SnakePart();

     
        public Form1()
        {
            InitializeComponent();

            gameTimer.Interval = 1000 / 20;
            gameTimer.Tick += new EventHandler(Update);
            gameTimer.Start();
            StartGame();
        }

        private void Update(object sender, EventArgs e)
        {
            if (gameover)
            {
                // Gameover Logic
                Font font = this.Font;
                string gameover_msg = "Gameover";
                string score_msg = "Score: " + score.ToString();
                string newgame_msg = "Press Enter to Start Over";

                //SizeF msg_size = canvas.MeasureString(gameover_msg, font);
                //PointF msg_point = new PointF(center_width - msg_size.Width / 2, 16);
                //canvas.DrawString(gameover_msg, font, Brushes.White, msg_point);

                //msg_size = canvas.MeasureString(score_msg, font);
                //msg_point = new PointF(center_width - msg_size.Width / 2, 32);
                //canvas.DrawString(score_msg, font, Brushes.White, msg_point);
                //msg_size = canvas.MeasureString(newgame_msg, font);
                //msg_point = new PointF(center_width - msg_size.Width / 2, 48);
                //canvas.DrawString(newgame_msg, font, Brushes.White, msg_point);
            }
            else
            {
                if (Input.Pressed(Keys.Right))
                {
                    if (snake.Count < 2 || snake[0].X == snake[1].X)
                        direction = 2;
                }
                else if (Input.Pressed(Keys.Left))
                {
                    if (snake.Count < 2 || snake[0].X == snake[1].X)
                        direction = 1;
                }
                else if (Input.Pressed(Keys.Up))
                {
                    if (snake.Count < 2 || snake[0].Y == snake[1].Y)
                        direction = 3;
                }
                else if (Input.Pressed(Keys.Down))
                {
                    if (snake.Count < 2 || snake[0].Y == snake[1].Y)
                        direction = 0;
                }
                UpdateSnake();
            }
            pbCanvas.Invalidate();
        }

        private void UpdateSnake()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    // Head Logic
                    switch (direction)
                    {
                        case 0: // Down
                            snake[i].Y++;
                            break;
                        case 1: // Left
                            snake[i].X--;
                            break;
                        case 2: // Right
                            snake[i].X++;
                            break;
                        case 3: // Up
                            snake[i].Y--;
                            break;
                    }

                    int max_tile_w = pbCanvas.Size.Width / tile_width;
                    int max_tile_h = pbCanvas.Size.Height / tile_height;
                    if (snake[i].X < 0 || snake[i].X >= max_tile_w || snake[i].Y < 0 || snake[i].Y >= max_tile_h)
                        gameover = true;

                }
                else
                {
                    // Body Logic
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;

                    for (int j = 1; j < snake.Count; j++)
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                            gameover = true;

                    if (snake[i].X == food_piece.X && snake[i].Y == food_piece.Y)
                    {
                        SnakePart part = new SnakePart();
                        part.X = snake[snake.Count - 1].X;
                        part.Y = snake[snake.Count - 1].Y;
                        snake.Add(part);
                        GenerateFood();
                        score++;
                    }
                }
            }
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

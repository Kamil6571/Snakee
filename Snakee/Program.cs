using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Snakee
{
    internal class Program
    {
        private static string highScoreFilePath = "highscore.txt";
        private static double frameRate = 1000 / 5.0; // Stała prędkość węża
        private static ConsoleColor snakeColor = ConsoleColor.Yellow; // Stały kolor węża

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.WriteLine("=== Snake Game ===");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. View High Score");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            StartGame();
                            break;
                        case 2:
                            ViewHighScore();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void StartGame()
        {
            Console.Clear();
            Console.CursorVisible = false;
            bool exit = false;
            DateTime lastDate = DateTime.Now;
            Meal meal = new Meal();
            Snake snake = new Snake();

            while (!exit)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo input = Console.ReadKey();

                    switch (input.Key)
                    {
                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.Down;
                            break;
                    }
                }

                if ((DateTime.Now - lastDate).TotalMilliseconds >= frameRate)
                {
                    snake.Move();

                    if (meal.CurrentTarget.X == snake.HeadPosition.X
                        && meal.CurrentTarget.Y == snake.HeadPosition.Y)
                    {
                        snake.EatMeal();
                        meal = new Meal();
                    }

                    if (snake.GameOver)
                    {
                        Console.Clear();
                        Console.WriteLine($"GAME OVER. YOUR SCORE: {snake.Length}");
                        SaveHighScore(snake.Length);
                        Thread.Sleep(2000);
                        exit = true;
                        break;
                    }

                    lastDate = DateTime.Now;
                }
            }

            Environment.Exit(0);
        }

        static void ViewHighScore()
        {
            Console.Clear();
            Console.WriteLine("=== High Score ===");
            int highScore = GetHighScore();
            Console.WriteLine($"High Score: {highScore}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void SaveHighScore(int score)
        {
            File.WriteAllText(highScoreFilePath, score.ToString());
        }

        static int GetHighScore()
        {
            if (File.Exists(highScoreFilePath))
            {
                string content = File.ReadAllText(highScoreFilePath);
                if (int.TryParse(content, out int score))
                {
                    return score;
                }
            }
            return 0;
        }
    }
}
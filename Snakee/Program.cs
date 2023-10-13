using System;
using System.Threading;

namespace Snakee
{
    internal class Program
    {

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
                Console.WriteLine("2. Exit");
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
            double frameRate = 1000 / 5.0;
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
                    //game action
                    snake.Move();

                    if (meal.CurrentTarget.X == snake.HeadPosition.X
                        && meal.CurrentTarget.Y == snake.HeadPosition.Y)
                    {
                        snake.EatMeal();
                        meal = new Meal();
                        frameRate /= 1.1;
                    } 

                    if (snake.GameOver)
                    {
                        Console.Clear();
                        Console.WriteLine($"GAME OVER. YOUR SCORE: {snake.Length}");
                        Thread.Sleep(2000); // Poczekaj 2 sekundy przed wyłączeniem
                        exit = true;
                        break; // Wyjdź z pętli gry po zakończeniu
                    }

                    lastDate = DateTime.Now;
                }
            }

            Environment.Exit(0); // Wyłącz program po zakończeniu gry
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Snakee
{
    public class Snake : ISnake
    {
        public int Length { get; set; } = 5;
        public Direction Direction { get; set; } = Direction.Right;
        public Coordinate HeadPosition { get; set; } = new Coordinate();
        List<Coordinate> Tail { get; set; } = new List<Coordinate>();
        private bool outOfRange = false;

        // Ustawienie stałego koloru węża na żółty
        private ConsoleColor snakeColor = ConsoleColor.Yellow;

        public bool GameOver
        {
            get
            {
                return (Tail.Where(c => c.X == HeadPosition.X
                  && c.Y == HeadPosition.Y).ToList().Count > 1) || outOfRange;
            }
        }

        public void EatMeal()
        {
            Length++;
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.Left:
                    HeadPosition.X--;
                    break;
                case Direction.Right:
                    HeadPosition.X++;
                    break;
                case Direction.Up:
                    HeadPosition.Y--;
                    break;
                case Direction.Down:
                    HeadPosition.Y++;
                    break;
            }

            try
            {
                Console.SetCursorPosition(HeadPosition.X, HeadPosition.Y);
                Console.ForegroundColor = snakeColor; // Ustawienie stałego koloru węża
                Console.Write(">");

                // Przy powiększonym jedzeniu, wąż nie będzie tracić ogonów po zjedzeniu
                Tail.Add(new Coordinate(HeadPosition.X, HeadPosition.Y));
                if (Tail.Count > this.Length)
                {
                    var endTail = Tail.First();
                    Console.SetCursorPosition(endTail.X, endTail.Y);
                    Console.Write(" ");
                    Tail.Remove(endTail);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                outOfRange = true;
            }
        }
    }

    public enum Direction { Left, Right, Up, Down }
}

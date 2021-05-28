using System;

namespace SnakeLaddersApi
{
    public class RandomGenerator
    {
        private readonly int _start;
        private readonly int _end;

        public RandomGenerator(int start, int end)
        {
            _start = start;
            _end = end;
        }

        public virtual int RandomNumber()
        {
            Random random = new Random();
            return random.Next(_start, _end);
        }
    }
}
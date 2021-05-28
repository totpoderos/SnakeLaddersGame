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
            throw new NotImplementedException();
        }
    }
}
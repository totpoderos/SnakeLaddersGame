using System;

namespace SnakeLaddersApi
{
    public class OneToSixDice
    {
        private readonly RandomGenerator _randomGenerator;

        public OneToSixDice(RandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public int Roll()
        {
            var number = _randomGenerator.RandomNumber();
            if (number < 1 || number > 6) throw new Exception("Out of range dice number");
            return number;
        }
    }
}
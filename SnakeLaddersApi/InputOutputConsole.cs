using System;

namespace SnakeLaddersApi
{
    public class InputOutputConsole
    {
        public virtual void Print(string output)
        {
            Console.WriteLine(output);
        }

        public virtual string Read()
        {
            return Console.ReadLine();
        }
    }
}
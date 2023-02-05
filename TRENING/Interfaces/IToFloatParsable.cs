namespace App.Interfaces
{
    internal interface IToFloatParsable
    {
        public static float ParseToFloat()
        {
            while (true)
            {
                if (float.TryParse(Console.ReadLine(), out var userInput))
                {
                    return userInput;
                }
                Console.WriteLine("Niepoprawne dane, proszę podać wartość liczbową.");
            }
        }
    }
}

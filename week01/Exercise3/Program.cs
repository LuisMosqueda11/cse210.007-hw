using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        string playAgain = "yes";
        
        while (playAgain.ToLower() == "yes")
        {
            int magicNumber = randomGenerator.Next(1, 101);
            int guessCount = 0;
            int guess = -1;
            
            Console.WriteLine("I've picked a magic number between 1 and 100. Try to guess it which number is!");
            
            while (guess != magicNumber)
            {
                Console.Write("What is your guess number? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;
                
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {guessCount} tries!");
                }
            }
            
            Console.Write("Would you like to play again? (yes/no) ");
            playAgain = Console.ReadLine();
        }
        
        Console.WriteLine("Thanks for playing!");
    }
}

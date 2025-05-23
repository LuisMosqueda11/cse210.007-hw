using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int gradePercentage = int.Parse(input);

        string letter;
        string sign = "";

        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        if (letter != "F") // No + or - for F grades
        {
            int lastDigit = gradePercentage % 10;
            
            if (lastDigit >= 7 && letter != "A") // No A+
            {
                sign = "+";
            }
            else if (lastDigit < 3 && gradePercentage != 100) // No A- for 100%
            {
                sign = "-";
            }
        }

        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! You'll do better next time.");
        }
    }
}

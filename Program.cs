using System;

class Program
{
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        // Subscribe to events
        stopwatch.OnStarted += message => Console.WriteLine(message);
        stopwatch.OnStopped += message => Console.WriteLine(message);
        stopwatch.OnReset += message => Console.WriteLine(message);

        Console.WriteLine("Welcome to the Stopwatch Application!");
        Console.WriteLine("Commands:");
        Console.WriteLine("'S': Start the stopwatch");
        Console.WriteLine("'T': Stop the stopwatch");
        Console.WriteLine("'R': Reset the stopwatch");
        Console.WriteLine("'Q': Quit the application");

        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\nEnter Command: ");
            string? input = Console.ReadLine()?.ToUpper();

            switch (input)
            {
                case "S":
                    stopwatch.Start();
                    break;
                case "T":
                    stopwatch.Stop();
                    break;
                case "R":
                    stopwatch.Reset();
                    break;
                case "Q":
                    isRunning = false;
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please press S, T, R, or Q.");
                    break;
            }

            // Display elapsed time at every interaction
            Console.WriteLine($"Time Elapsed: {stopwatch.TimeElapsed} seconds");
        }
    }
}
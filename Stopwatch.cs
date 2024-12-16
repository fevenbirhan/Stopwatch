using System;
using System.Threading;

public class Stopwatch
{
    // Fields
    private int _timeElapsed;
    private bool _isRunning;

    // Delegate and Events
    public delegate void StopwatchEventHandler(string message);
    public event StopwatchEventHandler? OnStarted;
    public event StopwatchEventHandler? OnStopped;
    public event StopwatchEventHandler? OnReset;

    public int TimeElapsed => _timeElapsed;
    public bool IsRunning => _isRunning;
    private readonly object _lock = new object(); // Synchronization object

    // Methods
    public void Start()
    {
        if (_isRunning)
        {
            Console.WriteLine("Stopwatch is already running.");
            return;
        }

        _isRunning = true;
        OnStarted?.Invoke("Stopwatch Started!");
        Tick();
    }

    public void Stop()
    {
        if (!_isRunning)
        {
            Console.WriteLine("Stopwatch is not running.");
            return;
        }

        lock (_lock)
        {
            _isRunning = false; // Stop the ticking loop
        }

        OnStopped?.Invoke("Stopwatch Stopped!");
    }

    public void Reset()
    {
        lock (_lock)
        {
            _timeElapsed = 0;
            _isRunning = false; // Ensure the stopwatch stops ticking
        }
        
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void Tick()
    {
        new Thread(() =>
        {
            while (true)
            {
                lock (_lock)
                {
                    if (!_isRunning) break; // Exit the loop if not running
                }

                Thread.Sleep(1000); // Simulate a tick every second

                lock (_lock)
                {
                    if (_isRunning)
                    {
                        _timeElapsed++;
                    }
                }

                // Update the elapsed time at the bottom of the console
                int currentCursorLeft = Console.CursorLeft;
                int currentCursorTop = Console.CursorTop;

                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine($"Time Elapsed: {_timeElapsed} seconds".PadRight(Console.WindowWidth));

                Console.SetCursorPosition(currentCursorLeft, currentCursorTop);
            }
        }).Start();
    }
}
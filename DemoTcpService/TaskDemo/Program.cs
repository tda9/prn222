
using System;
using System.Threading;
using System.Threading.Tasks;
class Program
{
    private static double DoComputation(double start)
    {
        double sum = 0;
        for (var value = start; value <= start + 10; value++)
        {
            sum += value;
        }
        return sum;
    }
    static void PrintNumber(string message)
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($" {message}:{i}"); Thread.Sleep(1000);

        }
    }//end PrintNumber
    static void Demo1()
    {
        Thread.CurrentThread.Name = "Main";
        // Create a task by using a lambda expression

        Task task01 = new Task(() => PrintNumber("Task 01"));
        task01.Start();
        // Create a task by using delegate and run the task
        Task task02 = Task.Run(delegate
        {
        });
        PrintNumber("Task 02");
        // Create a task by using a Action delegate
        Task task03 = new Task(new Action(() =>
        {
            PrintNumber("Task 03");
        }));

        task03.Start();
        Console.WriteLine($"Thread '{Thread.CurrentThread.Name}");
        Console.ReadKey();
    }
    static void Main()
    {
        //Demo1();
        //Demo2();
        //Demo3();
        Demo4();
    }

    public static void Demo2()
    {
        // Wait for all tasks to complete.

        Task[] tasks = new Task[5];

        String taskData = "Hello";

        for (int i = 0; i < 5; i++)
        {

            tasks[i] = Task.Run(() =>

            {

                Console.WriteLine($"Task={Task.CurrentId}, obj-{taskData}, " + $"ThreadId=[Thread.CurrentThread.ManagedThreadTd)");

                Thread.Sleep(1000);

            });
        }

        try { Task.WaitAll(tasks); }
        catch (AggregateException ae)
        {

            Console.WriteLine("One or more exceptions occurred: ");

            foreach (var ex in ae.Flatten().InnerExceptions)

                Console.WriteLine(" {0}", ex.Message);

            Console.WriteLine("Status of completed tasks:");

            foreach (var t in tasks)
            {
                Console.WriteLine(" Task #(0): (1)", t.Id, t.Status);
            }
        }
    }//end Main

    public static void Demo3()
    {

        Task<Double>[] taskArray = {Task<double>.Factory.StartNew(() => DoComputation(1.0)),

        Task<double>.Factory.StartNew(() => DoComputation(100.0)),

        Task<double>.Factory.StartNew(() => DoComputation(1000.0)) };

        var results = new double[taskArray.Length];

        double sum = 0;

        for (int i = 0; i < taskArray.Length; i++)
        {

            results[i] = taskArray[i].Result;

            Console.Write("{0:N1} {1}", results[i],

            i == taskArray.Length - 1 ? "" : "+");

            sum += results[i];

        }

        Console.WriteLine("{0:N1}", sum);

    }//end Main
    static void Demo4()
    {
        var tasks = new List<Task<int>>();
        var source = new CancellationTokenSource();
        var token = source.Token;
        int completedIterations = 0;

        for (int n = 1; n <= 20; n++)
        {
            tasks.Add(Task.Run(() =>
            {
                int iterations = 0;
                for (int ctr = 1; ctr <= 2_000_000; ctr++)
                {
                    token.ThrowIfCancellationRequested();
                    iterations++;
                }
                Interlocked.Increment(ref completedIterations);
                if (completedIterations >= 10)
                    source.Cancel();
                return iterations;
            }, token));
        }

        Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException)
        {
            Console.WriteLine("Status of tasks:\n");
            Console.WriteLine("{0,10} {1,20} {2,14}", "Task Id", "Status", "Iterations");
            foreach (var t in tasks)
            {
                Console.WriteLine("{0,10} {1,20} {2,14}",
                    t.Id, t.Status,
                    t.Status != TaskStatus.Canceled ? t.Result.ToString() : "n/a");
            }
        }

        Console.ReadLine();
    }
}//end Program

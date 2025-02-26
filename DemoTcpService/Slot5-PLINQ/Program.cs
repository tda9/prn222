using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;

class Program

{

    public static void Main()

    {

        var range = Enumerable.Range(1, 1000_000);

        //Here is Sequential version

        var resultList = range.Where(i => i % 3 == 0).ToList();

        Console.WriteLine($"Sequential: Total items are {resultList.Count}");

        //Here is Parallel version using. AsParallel method

        resultList = range.AsParallel().Where(i => i % 3 == 0).ToList();

        Console.WriteLine($"Parallel: Total items are {resultList.Count}");

        resultList = (from i in range.AsParallel()

                      where i % 3 == 0

                      select i).ToList();

        Console.WriteLine($"Parallel: Total items are {resultList.Count}");

        Console.ReadLine();

    }//end Main


    //demo 2
    // IsPrime returns true if number is Prime, else false.
    private static bool IsPrime(int number)
    {
        bool result = true;

        if (number < 2)
        {

            return false;

        }

        for (var divisor = 2; divisor <= Math.Sqrt(number) && result == true; divisor++)
        {

            if (number % divisor == 0)
            {

                result = false;

            }

        }

        return result;

    }//end IsPrime
     //GetPrimeList returns Prime numbers by using sequential ForEach

    private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();

    //GetPrimeListWithParallel returns Prime numbers by using Parallel.ForEach

    private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
    {

        var primeNumbers = new ConcurrentBag<int>();

        Parallel.ForEach(numbers, number =>
        {

            if (IsPrime(number))
            {

                primeNumbers.Add(number);

            }

        });

        return primeNumbers.ToList();

    }//end GetPrimeListWithParallel

    static void Demo2()
    {

        // 2 million

        var limit = 2_000_000;

        var numbers = Enumerable.Range(0, limit).ToList();

        var watch = Stopwatch.StartNew();

        var primeNumbersFromForeach = GetPrimeList(numbers);

        watch.Stop();

        var watchForParallel = Stopwatch.StartNew();

        var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);

        watchForParallel.Stop();

        Console.WriteLine($"Classical foreach loop Total prime numbers :" +

        $" {primeNumbersFromForeach.Count} | Time Taken : " +

$"{watch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Parallel. ForEach loop | Total prime numbers : " +

$"{primeNumbersFromParallelForeach.Count} | Time Taken : " +

$"{watchForParallel.ElapsedMilliseconds} ms.");

        Console.WriteLine("Press any key to exit.");

        Console.ReadLine();

    }//end Demo 2


}//end Program
using System;

using System.Net;

class Program
{

    //Using Event-Based Asynchronous Pattern (EAP)

    private static void DownloadAsynchronously()
    {

        WebClient client = new WebClient();

        client.DownloadStringCompleted +=

        new DownloadStringCompletedEventHandler(DownloadComplete);

        client.DownloadStringAsync(new Uri("http://www.aspnet.com"));

    }

    //-

    private static void DownloadComplete(object sender,

    DownloadStringCompletedEventArgs e)
    {

        if (e.Error != null)
        {

            Console.WriteLine("Some error has occured.");

            return;

        }

        //Print result

        Console.WriteLine(e.Result);

        Console.WriteLine(new string('*', 30));

        Console.WriteLine("Download completed.");

    }
    //-

    static void Main(string[] args)
    {

        DownloadAsynchronously();

        Console.WriteLine("Main thread: Done");

        Console.WriteLine(new string('*', 30));

        Console.ReadLine();

    }//end Demo1

    //Demo2
    

    public static async Task<int> Method1()
    {

        int count = 0;

        await Task.Run(() => {

            for (int i = 1; i <= 10; i++)
            {

                Console.WriteLine("Method 1");

                count += 1;

            }

        });

        return count;

    }

    //-

    public static void Method2()
    {

        for (int i = 1; i <= 5; i++)
        {

            Console.WriteLine("Method 2");

        }

    }

    //-

    public static void Method3(int count)
    {

        Console.WriteLine("Method 3 is called.");

        Console.WriteLine($"Total count is {count}");

    }
}
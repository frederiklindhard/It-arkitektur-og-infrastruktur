using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading;

//1 > dotnet new console
//2 > dotnet add package System.Diagnostics.DiagnosticSource
//3 > dotnet run (i en anden terminal)
// I denne terminal
//4 > dotnet tool update -g dotnet-counters
//5 > dotnet-counters ps
//6 > dotnet-counters monitor -p 19964 HatCo.HatStore (19964 skiftes ud med opgM01.03' ID)

/*
// Create a custom metric
class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
    static Counter<int> s_hatsSold = s_meter.CreateCounter<int>("hats-sold");

    static void Main(string[] args)
    {
        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has a transaction each second that sells 4 hats
            Thread.Sleep(1000);
            s_hatsSold.Add(4);
        }
    }
}
*/

/*
// Example of different instrument types
class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
    static Counter<int> s_hatsSold = s_meter.CreateCounter<int>("hats-sold");
    static Histogram<int> s_orderProcessingTimeMs = s_meter.CreateHistogram<int>("order-processing-time");
    static int s_coatsSold;
    static int s_ordersPending;

    static Random s_rand = new Random();

    static void Main(string[] args)
    {
        s_meter.CreateObservableCounter<int>("coats-sold", () => s_coatsSold);
        s_meter.CreateObservableGauge<int>("orders-pending", () => s_ordersPending);

        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has one transaction each 100ms that each sell 4 hats
            Thread.Sleep(100);
            s_hatsSold.Add(4);

            // Pretend we also sold 3 coats. For an ObservableCounter we track the value in our variable and report it
            // on demand in the callback
            s_coatsSold += 3;

            // Pretend we have some queue of orders that varies over time. The callback for the "orders-pending" gauge will report
            // this value on-demand.
            s_ordersPending = s_rand.Next(0, 20);

            // Last we pretend that we measured how long it took to do the transaction (for example we could time it with Stopwatch)
            s_orderProcessingTimeMs.Record(s_rand.Next(5, 15));
        }
    }
}
*/

/*
// Descriptions and units
class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
    static Counter<int> s_hatsSold = s_meter.CreateCounter<int>(name: "hats-sold",
                                                                unit: "Hats",
                                                                description: "The number of hats sold in our store");

    static void Main(string[] args)
    {
        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has a transaction each 100ms that sells 4 hats
            Thread.Sleep(100);
            s_hatsSold.Add(4);
        }
    }
}
*/

/*
// Multi-dimensional metrics
class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
    static Counter<int> s_hatsSold = s_meter.CreateCounter<int>("hats-sold");

    static void Main(string[] args)
    {
        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has a transaction, every 100ms, that sells 2 (size 12) red hats, and 1 (size 19) blue hat.
            Thread.Sleep(100);
            s_hatsSold.Add(2,
                           new KeyValuePair<string, object>("Color", "Red"),
                           new KeyValuePair<string, object>("Size", 12));
            s_hatsSold.Add(1,
                           new KeyValuePair<string, object>("Color", "Blue"),
                           new KeyValuePair<string, object>("Size", 19));
        }
    }
}
*/

/*
// Multi-dimensional metrics
class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");

    static void Main(string[] args)
    {
        s_meter.CreateObservableGauge<int>("orders-pending", GetOrdersPending);
        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }

    static IEnumerable<Measurement<int>> GetOrdersPending()
    {
        return new Measurement<int>[]
        {
            // pretend these measurements were read from a real queue somewhere
            new Measurement<int>(6, new KeyValuePair<string,object>("Country", "Italy")),
            new Measurement<int>(3, new KeyValuePair<string,object>("Country", "Spain")),
            new Measurement<int>(1, new KeyValuePair<string,object>("Country", "Mexico")),
        };
    }
}
*/
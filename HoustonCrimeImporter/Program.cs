using System;
using System.Reflection;
using Ninject;
using Timer = System.Timers.Timer;

namespace HoustonCrimeImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var csvFileName = args[0];
            //var dataSetIdentifier = args[1];
            var csvFileName = @"b:\source\SODAClient\HoustonCrimeImporter\Houston_Crime_Jan_2013.csv";
            var dataSetIdentifier = "4s5g-xhcq";


            var fileWalker = new HoustonCrimeFlatFileWalker(csvFileName, dataSetIdentifier);

            var timer = new Timer();

            timer.Elapsed += (o, i) =>
            {
                Console.Write($"\r{fileWalker.Progress:0.0}% {fileWalker.CurrentLine} / {fileWalker.FileLineCount}" );
            };
            timer.Interval = 5000;
            timer.Enabled = true;


            fileWalker.ProcessFileAsync().Wait();
        }
    }
}

using LiteDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace LiteDbTest
{
    class Program
    {
        public static string DbPath = "C:/LiteDbTest/";
        static void Main(string[] args)
        {
            //InitializeDatabase();
            RandomLoadTest();
        }
        static void RandomLoadTest()
        {
            ConcurrentQueue<LiteDatabase> ldb = new ConcurrentQueue<LiteDatabase>();
            Random rnd = new Random();
            Stopwatch sw = new Stopwatch();
            long totalCount = 0;
            sw.Start();

            Parallel.For(0, 1000, (i) => {
                int rndInt = rnd.Next(1, 9999);
                var db = new LiteDatabase($"{DbPath}{i}.tdb");

                var col = db.GetCollection<TestModel>("todoTestModel");
                for (int q = 0; q < 1000; q++)
                {
                    string str = col.FindById(rndInt).A;
                }

                Interlocked.Increment(ref totalCount);
                ldb.Enqueue(db);
            });
            sw.Stop();

            double perAction = ((double)sw.ElapsedMilliseconds) / ((double)totalCount);
            Console.WriteLine($"Load - {perAction}ms; queueCount = {ldb.Count}");
            Console.ReadLine();
        }
        static void InitializeDatabase()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                using (var db = new LiteDatabase($"{DbPath}{i}.tdb"))
                {
                    var col = db.GetCollection<TestModel>("todoTestModel");
                    var ltm = new List<TestModel>(10000);
                    for (int x = 0; x < 10000; x++)
                    {
                        TestModel tm = new TestModel();
                        tm.A = "Apple Absolute Accept Acceleration Acknowledge";
                        tm.B = "Beta Begin Beleive Bench Base Button";
                        tm.C = "Csharp Customer Cascade Coooooooper";
                        ltm.Add(tm);
                    }
                    col.InsertBulk(ltm);
                }
            }
            sw.Stop();
            Console.WriteLine($"Create - {sw.ElapsedMilliseconds}ms");
        }
    }
}

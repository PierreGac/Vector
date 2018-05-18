using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VectorTest
{
    class Program
    {
        const int __SIZE = 100000000;

        static void Main(string[] args)
        {
            /*WriteTitle("HASHING METHODS");
            WriteSubTitle("POSITIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHashShort, 10000000, 10, true);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHash64Short, 10000000, 10, true);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHashInt, 10000000, 10, true);

            WriteSubTitle("NEGATIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHashShort, 10000000, 10, false);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHash64Short, 10000000, 10, false);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkHashInt, 10000000, 10, false);

            GC.Collect();
            GC.Collect();*/


            
            //Console.WriteLine();
            //WriteTitle("EQUALITY METHODS");
            //WriteSubTitle("POSITIVE");

            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityShort, 10000000, 10, true);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityValueShort, 10000000, 100, true);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEquality64Short, 10000000, 100, true);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityInt, 10000000, 10, true);

            //WriteSubTitle("NEGATIVE");

            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityShort, 10000000, 10, false);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityValueShort, 10000000, 100, false);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEquality64Short, 10000000, 100, false);
            //Console.WriteLine();
            //Benchmarks.DoBenchmark(Benchmarks.BenchmarkEqualityInt, 10000000, 10, false);
            
            /*
            WriteTitle("ARRAY ALLOCATION");
            WriteSubTitle("POSITIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortArrayAllocation, 10000000, 10, true);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkIntArrayAllocation, 10000000, 10, true);

            WriteSubTitle("NEGATIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortArrayAllocation, 10000000, 10, false);

            Console.WriteLine();

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkIntArrayAllocation, 10000000, 10, false);
            */
            
            WriteTitle("LIST LOOKUPS");
            WriteSubTitle("POSITIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortListLookups, 10000000, 10, true);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortListFor64Lookups, 10000000, 10, true);

            WriteSubTitle("NEGATIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortListLookups, 10000000, 10, false);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortListFor64Lookups, 10000000, 10, false);

            /*WriteTitle("DICTIONARY LOOKUPS");
            WriteSubTitle("POSITIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsNoComp_VecVec, 10000000, 10, true);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsNoComp_LongVec, 10000000, 10, true);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsCompLong_VecVec, 10000000, 10, true);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsCompVec_VecVec, 10000000, 10, true);

            WriteSubTitle("NEGATIVE");

            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsNoComp_VecVec, 10000000, 10, false);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsNoComp_LongVec, 10000000, 10, false);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsCompLong_VecVec, 10000000, 10, false);
            Console.WriteLine();
            Benchmarks.DoBenchmark(Benchmarks.BenchmarkShortDictionaryLookupsCompVec_VecVec, 10000000, 10, false);
            */
            Console.Read();
        }

        private static void WriteTitle(string message)
        {
            Console.WriteLine("----------: {0} :----------", message);
        }

        private static void WriteSubTitle(string message)
        {
            Console.WriteLine("-----: {0} :-----", message);
        }
    }
}

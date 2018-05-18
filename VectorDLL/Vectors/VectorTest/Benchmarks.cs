using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorTest
{
    public static class Benchmarks
    {
        public delegate long BenchmarkMethod(int elementsToHash, bool positive);

        private static Vector3Short vShort = new Vector3Short(13, -7, 19);

        public static void DoBenchmark(BenchmarkMethod method, int elementsToHash, int loops, bool positive)
        {
            Console.WriteLine("[{0} > 0 == {3}] Number of elements: {1}, Loops: {2}", method.Method.Name, elementsToHash, loops, positive);
            long time = 0;
            for (int i = 0; i < loops; i++)
            {
                long temp = method(elementsToHash, positive);
                time += temp;
            }

            float average = (float)time / loops;
            Console.WriteLine("[{0}] Time: {1}ms elements/ms: {2}", method.Method.Name, average, elementsToHash / average);

        }

        #region Array generation

        private static Vector3Short[] GetShortArray(int elementsToHash, bool positive)
        {
            Vector3Short[] vectors = new Vector3Short[elementsToHash];

            int min = positive == true ? 0 : -511;
            int max = positive == true ? 511 : 0;
            Random r = new Random();
            // Populate array
            for (int i = 0; i < elementsToHash; i++)
            {
                vectors[i] = new Vector3Short((short)r.Next(min, max), (short)r.Next(min, max), (short)r.Next(min, max));
            }

            return vectors;
        }

        private static Vector3Short[] Get1E7ShortArray(bool positive)
        {
            Vector3Short[] vectors = new Vector3Short[10000000];

            short xmin = positive == true ? (short)0 : (short)-1000;
            short xmax = positive == true ? (short)1000 : (short)0;
            short zmin = positive == true ? (short)0 : (short)-10;
            short zmax = positive == true ? (short)10 : (short)0;

            int index = 0;
            Random r = new Random();
            // Populate array
            for (short x = xmin; x < xmax; x++)
            {
                for (short y = xmin; y < xmax; y++)
                {
                    for (short z = zmin; z < zmax; z++)
                    {
                        vectors[index] = new Vector3Short(x, y, z);
                        index++;
                    }
                }
            }

            return vectors;
        }
        #endregion

        #region Array/Collection allocation
        public static long BenchmarkShortArrayAllocation(int elements, bool positive)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            GetShortArray(elements, positive);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        #endregion

        #region Hash
        public static long BenchmarkHash64Short(int elementsToHash, bool positive)
        {
            Vector3Short[] vectors = GetShortArray(elementsToHash, positive);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < elementsToHash; i++)
            {
                vectors[i].ComputeHash64();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        #endregion

        #region Equality
        public static long BenchmarkEqualityShort(int elementsToHash, bool positive)
        {
            Vector3Short[] vectors = Get1E7ShortArray(positive);

            Vector3Short v = new Vector3Short(13, -7, 19);
            bool result;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < elementsToHash; i++)
            {
                result = vectors[i] == v;
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long BenchmarkEqualityValueShort(int elementsToHash, bool positive)
        {
            Vector3Short[] vectors = Get1E7ShortArray(positive);

            Vector3Short v = new Vector3Short(13, -7, 19);
            bool result;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < elementsToHash; i++)
            {
                result = vectors[i].Equals(v);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long BenchmarkEquality64Short(int elementsToHash, bool positive)
        {
            Vector3Short[] vectors = Get1E7ShortArray(positive);

            Vector3Short v = new Vector3Short(13, -7, 19);
            bool result;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < elementsToHash; i++)
            {
                result = vectors[i].Equals64(v);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        #endregion

        #region List lookups
        public static long BenchmarkShortListLookups(int elements, bool positive)
        {
            List<Vector3Short> list = new List<Vector3Short>(Get1E7ShortArray(positive));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            list.Contains(vShort);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        public static long BenchmarkShortListFor64Lookups(int elements, bool positive)
        {
            List<Vector3Short> list = new List<Vector3Short>(Get1E7ShortArray(positive));
            int count = list.Count;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                if (list[i].Equals64(vShort))
                {
                    break;
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        #endregion

        #region Dictionary
        public static long BenchmarkShortDictionaryLookupsNoComp_VecVec(int elements, bool positive)
        {
            Dictionary<Vector3Short, Vector3Short> dic = new Dictionary<Vector3Short, Vector3Short>();
            Vector3Short[] array = Get1E7ShortArray(positive);
            for (int i = 0; i < elements; i++)
            {
                dic.Add(array[i], array[i]);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            dic.ContainsKey(vShort);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long BenchmarkShortDictionaryLookupsNoComp_LongVec(int elements, bool positive)
        {
            Dictionary<long, Vector3Short> dic = new Dictionary<long, Vector3Short>();
            Vector3Short[] array = Get1E7ShortArray(positive);
            for (int i = 0; i < elements; i++)
            {
                dic.Add(array[i].ComputeHash64(), array[i]);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            dic.ContainsKey(vShort.ComputeHash64());

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long BenchmarkShortDictionaryLookupsCompLong_VecVec(int elements, bool positive)
        {
            Dictionary<Vector3Short, Vector3Short> dic = new Dictionary<Vector3Short, Vector3Short>(new Vector3ShortLongComparer());
            Vector3Short[] array = Get1E7ShortArray(positive);
            for (int i = 0; i < elements; i++)
            {
                dic.Add(array[i], array[i]);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            dic.ContainsKey(vShort);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long BenchmarkShortDictionaryLookupsCompVec_VecVec(int elements, bool positive)
        {
            Dictionary<Vector3Short, Vector3Short> dic = new Dictionary<Vector3Short, Vector3Short>(new Vector3ShortComparer());
            Vector3Short[] array = Get1E7ShortArray(positive);
            for (int i = 0; i < elements; i++)
            {
                dic.Add(array[i], array[i]);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            dic.ContainsKey(vShort);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        #endregion
    }
}

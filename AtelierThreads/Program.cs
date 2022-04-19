using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AtelierThreads
{
    class Program
    {
        private static Random rand = new Random();

        static int[] CopierTableau(int[] t)
        {
            int[] c = new int[t.Length];
            for (int i = 0; i < t.Length; i++)
                c[i] = t[i];
            return c;
        }

        static void ImprimerTableau(int[] t)
        {
            for (int i = 0; i < t.Length; i++)
                Console.Write(t[i] + (i < t.Length - 1 ? ", " : "."));
            Console.WriteLine();
        }

        static int[] GenererTableau(int taille)
        {
            int[] t = new int[taille];
            for (int i = 0; i < t.Length; i++)
                t[i] = rand.Next(int.MinValue, int.MaxValue);
            return t;
        }

        static Task DemarrerTri(int[] t, Func<int[], int[]> tri)
        {
            Stopwatch sw = new Stopwatch();

            Task<int[]> tacheTri = new Task<int[]>(() => tri(t));
            Task tacheImpression = tacheTri.ContinueWith((t) =>
            {
                Console.WriteLine($"{tri.Method.Name} terminé sur {t.Result.Length} entiers. ({sw.ElapsedMilliseconds} ms)");
            });

            // Permet d'afficher les exceptions s'il y a lieu
            tacheTri.ContinueWith((t) => Console.Error.WriteLine(t.Exception), TaskContinuationOptions.OnlyOnFaulted);

            // Démarre le Thread
            sw.Start();
            tacheTri.Start();
            return tacheImpression;
        }

        static void Main(string[] args)
        {
            int[] t = GenererTableau(100_000);
            Task[] tasks = new Task[] {
                DemarrerTri(CopierTableau(t), Tris.TriABulle),
                DemarrerTri(CopierTableau(t), Tris.TriInsertion),
                DemarrerTri(CopierTableau(t), Tris.TriSelection),
                DemarrerTri(CopierTableau(t), Tris.TriRapide),
                DemarrerTri(CopierTableau(t), Tris.TriFusion),
            };

            Task.WaitAll(tasks);
            Console.WriteLine("Fin des tris");
            Console.ReadLine();
        }
    }
}

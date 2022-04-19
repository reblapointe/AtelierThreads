using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierThreads
{
    public class Tris
    {
        public static int[] TriABulle(int[] t)
        {
            for (int i = 0; i < t.Length - 1; i++)
            {
                for (int j = 0; j < t.Length - 1; j++)
                {
                    if (t[j] > t[j + 1])
                    {
                        int temp = t[j];
                        t[j] = t[j + 1];
                        t[j + 1] = temp;
                    }
                }
            }
            return t;
        }

        public static int[] TriSelection(int[] t)
        {
            int min;
            for (int i = 0; i < t.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < t.Length; j++)
                {
                    if (t[j] < t[min])
                    {
                        min = j;
                    }
                }
                int temp = t[min];
                t[min] = t[i];
                t[i] = temp;
            }
            return t;
        }

        public static int[] TriInsertion(int[] t)
        {
            for (int i = 1; i < t.Length; i++)
            {
                int v = t[i];
                int j = i - 1;

                while (j >= 0 && t[j] > v)
                {
                    t[j + 1] = t[j];
                    j--;
                }
                t[j + 1] = v;
            }
            return t;
        }

        public static int[] TriRapide(int[] t)
        {
            return TriRapide(t, 0, t.Length - 1);
        }

        private static int[] TriRapide(int[] t, int gauche, int droite)
        {
            if (gauche < droite)
            {
                int pivot = Partition(t, gauche, droite);
                if (pivot > 1)
                    TriRapide(t, gauche, pivot - 1);
                if (pivot + 1 < droite)
                    TriRapide(t, pivot + 1, droite);
            }
            return t;
        }

        private static int Partition(int[] t, int gauche, int droite)
        {
            int pivot = t[gauche];
            while (true)
            {
                while (t[gauche] < pivot)
                    gauche++;

                while (t[droite] > pivot)
                    droite--;

                if (gauche < droite)
                {
                    if (t[gauche] == t[droite])
                        return droite;
                    int temp = t[gauche];
                    t[gauche] = t[droite];
                    t[droite] = temp;
                }
                else
                {
                    return droite;
                }
            }
        }

        public static int[] TriFusion(int[] t)
        {
            return TriFusion(t, 0, t.Length - 1);
        }

        private static int[] TriFusion(int[] t, int gauche, int droite)
        {
            if (gauche < droite)
            {
                int milieu = gauche + (droite - gauche) / 2;
                TriFusion(t, gauche, milieu);
                TriFusion(t, milieu + 1, droite);
                Fusionner(t, gauche, milieu, droite);
            }
            return t;
        }

        private static void Fusionner(int[] t, int gauche, int milieu, int droite)
        {
            var longueurGauche = milieu - gauche + 1;
            var longueurDroite = droite - milieu;
            var gaucheTemp = new int[longueurGauche];
            var droiteTemp = new int[longueurDroite];
            int i, j;
            for (i = 0; i < longueurGauche; ++i)
                gaucheTemp[i] = t[gauche + i];
            for (j = 0; j < longueurDroite; ++j)
                droiteTemp[j] = t[milieu + 1 + j];
            i = 0;
            j = 0;
            int k = gauche;
            while (i < longueurGauche && j < longueurDroite)
                if (gaucheTemp[i] <= droiteTemp[j])
                    t[k++] = gaucheTemp[i++];
                else
                    t[k++] = droiteTemp[j++];
            while (i < longueurGauche)
                t[k++] = gaucheTemp[i++];
            while (j < longueurDroite)
                t[k++] = droiteTemp[j++];
        }
    }
}

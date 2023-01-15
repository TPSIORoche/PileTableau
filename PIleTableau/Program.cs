using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileTableau
{
    class Program
    {
        struct Pile
        {
            public int maxElt;
            public int sommet;
            public int[] tablElem;

        }
        static void Main(string[] args)
        {
            TestePileVidePleine();
            TesteEmpilerDepiler();
            TesteConversion();
            //TesteConversion2();
            Console.WriteLine("\nFin de programme");
            Console.ReadKey();
        }


        static void InitPile(ref Pile pUnePile, int PNbElemt)
        {
            pUnePile.maxElt = PNbElemt;
            pUnePile.sommet = -1;
            pUnePile.tablElem = new int[pUnePile.maxElt];
        }
        static bool PileVide(Pile pUnePile)
        {
            return (pUnePile.sommet == -1);
        }

        static bool PilePleine(Pile pUnePile)
        {
            return (pUnePile.sommet == pUnePile.maxElt - 1);
        }
        static void Empiler(ref Pile pUnePile, int PNb)
        {
            if (!PilePleine(pUnePile))
            {
                pUnePile.sommet++;
                pUnePile.tablElem[pUnePile.sommet] = PNb;

            }
        }
        static int Depiler(ref Pile pUnePile)
        {
            if (PileVide(pUnePile))
            {
                return -999;
            }
            pUnePile.sommet--;
            return pUnePile.tablElem[pUnePile.sommet + 1];

        }
        static int Afficher(ref Pile pUnePile)
        {
            if (!PileVide(pUnePile))
            {
                return pUnePile.tablElem[pUnePile.sommet];
            }
            return -999;
        }
        static void TestePileVidePleine()
        {
            Pile unePile = new Pile();
            InitPile(ref unePile, 5);
            if (PileVide(unePile))
            {
                Console.WriteLine("la pile est vide");
            }
            else
            {
                Console.WriteLine("la pile n'est pas vide");
            }
            if (PilePleine(unePile))
            {
                Console.WriteLine("la pile est pleine");
            }
            else
                Console.WriteLine("la pile n'est pas pleine");
        }
        static void TesteEmpilerDepiler()
        {
            Pile unePile = new Pile();
            InitPile(ref unePile, 5);
            Empiler(ref unePile, 2);
            Empiler(ref unePile, 14);
            Empiler(ref unePile, 6);
            Empiler(ref unePile, 22);
            int valeurDepilee = Depiler(ref unePile);
            Console.WriteLine("valeur dépilée : " + valeurDepilee);
            Empiler(ref unePile, 17);
            Empiler(ref unePile, 81);
            Empiler(ref unePile, 34);
        }

        static string Convertir(int pNbElements, int pNbAConvertir, int pNewbase)
        {
            Pile unePile = new Pile();
           
            InitPile(ref unePile,pNbElements);
            int a = pNbAConvertir;
            int r;
            string resultat = "";
            
            while (a > 0)
            {
                r = a % pNewbase;
                Empiler(ref unePile, r);
                a /= pNewbase;
            }
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            while (!PileVide(unePile))
            {
                if (Afficher(ref unePile)>9)
                {
                    int alte = (Depiler(ref unePile) % pNewbase) - 10;
                    resultat += alpha[alte];

                }
                else
                {
                    resultat += Depiler(ref unePile).ToString();
                }
            }
            return resultat;
        }

        static void TesteConversion()
        {
            Console.Write("Entrez le nombre d'éléments du tableau : ");
            string  input = Console.ReadLine();
            int a = int.Parse(input);
            Console.Write("Entrez le nombre à convertir : ");
            string input2 = Console.ReadLine();
            int b = int.Parse(input2);
            Console.Write("Entrez la nouvelle base entre 2 et 16 : ");
            string input3 = Console.ReadLine();
            int c = int.Parse(input3);
            while (2 > int.Parse(input3) | (16 < int.Parse(input3)))
            {
                Console.Write("Entrez la nouvelle base entre 2 et 16 : ");
                input3 = Console.ReadLine();
                c = int.Parse(input3);
            }
            if (Math.Pow(c,a)<b)
            {
                Console.Write("Impossible de convertir, la pile est trop petite");
            }
            else
            {
                string d = Convertir(a, b, c);
                Console.Write($"La valeur de {b} (base 10) vaut {d} en base {c}\n");
            }  
        }

        static string ConvDecimToNewBase(int pNbAConvertir, int pNewbase)
        {
            int i = 1;
            while (Math.Pow(pNewbase, i) < pNbAConvertir)
            {
                i++;
            }
            Pile unePile = new Pile();
            InitPile(ref unePile, i);
            int a = pNbAConvertir;
            int r;
            if (a == 0)
            {
                return "0";
            }
            while (a > 0)
            {
                r = a % pNewbase;
                Empiler(ref unePile, r);
                a /= pNewbase;
            }
            string resultat = "";
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            while (!PileVide(unePile))
            {
                if (Afficher(ref unePile) > 9)
                {
                    int alte = (Depiler(ref unePile) % pNewbase) - 10;
                    resultat += alpha[alte];

                }
                else
                {
                    resultat += Depiler(ref unePile).ToString();
                }
            }
            return resultat;
        }

        static void TesteConversion2()
        {
            Console.Write("Entrez le nombre à convertir : ");
            string input2 = Console.ReadLine();
            int b = int.Parse(input2);
            Console.Write("Entrez la nouvelle base entre 2 et 16 : ");
            string input3 = Console.ReadLine();
            int c = int.Parse(input3);
            string d = ConvDecimToNewBase(b, c);
            Console.Write($"La valeur de {b} (base 10) vaut {d} en base {c}");
        }
    }
}

        

//Probleme avec ** donc Math.Pow et point d'arret pour comprendre l'erreur
// cas où pNbAConvertir==0
// convertion entier en chaine de caractere
// pile qui s'ajuste a la taille de ce qu'il va contenir
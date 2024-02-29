using System;
using System.Collections.Generic;

namespace DM_Probas_Janvier2023
{
    internal class Program
    {

        public static double arrondi_si_nombre_rond(double jetons)
        {
            if (jetons % 2 != 0)
            {
                return (Math.Truncate(jetons / 2.0)) + 1;
            }
            else
            {
                return jetons / 2.0;
            }

        }

        public static List<int> une_partie_de_roulettes(out int numero_tour_final, out double proba_finale, out bool victorieux)
        {

            List<int> evolution_jetons = new List<int>();

            double jetons = 5;

            double jetons_mises;

            jetons_mises = (Math.Truncate(jetons / 2)) + 1;

            //Math.Truncate(jetons/2)++ ne compile pas!!

            Random rand = new Random();

            int numero_operation = 0;

            double cumul_probabilites = 1;

            victorieux = true;

            while (jetons != 0 && jetons < 10)
            {

                numero_operation++;
                evolution_jetons.Add((int)jetons);

                bool victoire_pari;

                int res_random = rand.Next(37) + 1; // génère un nombre aléatoire entre 1 et 37

                if (res_random <= 18) // si le nombre est inférieur à 18 - donc en cas de victoire
                {
                    victoire_pari = true;
                }
                else
                {
                    victoire_pari = false;
                }


                double proba_actuelle;

                if (victoire_pari)
                {
                    proba_actuelle = 18.0 / 37.0;
                }
                else
                {
                    proba_actuelle = 19.0 / 37.0;
                }

                //Console.WriteLine($"Nombre de jetons misés : {jetons_mises}");

                if (victoire_pari)
                {

                    jetons = jetons + jetons_mises;

                    jetons_mises = arrondi_si_nombre_rond(jetons);

                }
                else
                {

                    jetons = jetons - jetons_mises;

                    jetons_mises = arrondi_si_nombre_rond(jetons);

                }



                cumul_probabilites = cumul_probabilites * proba_actuelle;

                
                /*Console.WriteLine($"Tour d'opération actuel : {numero_operation}");
                Console.WriteLine($"Jeu gagné? : {victoire_pari}");
                Console.WriteLine($"Nombre de jetons restants : {jetons}");
                Console.WriteLine($"Probabilité actuelle (depuis le début) que ce scénario se produise : {cumul_probabilites * 100}% \n");*/


                // stocker le tour d'opérations et la proba finale
            }

            if (jetons == 0)
            {
                victorieux = false;
            }

            evolution_jetons.Add((int)jetons);
            numero_tour_final = numero_operation;
            proba_finale = cumul_probabilites;

            return evolution_jetons;

        }
        static void Main(string[] args)
        {
            //Simulation à l'infini d'une séance de casino d'Aldo

            int k = 1000000; // combien de fois on va répeter l'expérience pour avoir des résultats statistiques

            double moyenne_operations = 0;
            double pourcentage_parigagnant = 0;
            double moyenne_probas = 0;

            for (int i = 0; i < k; i++)
            {
                int res_operations;
                double res_proba;
                bool partie_victorieuse;

                une_partie_de_roulettes(out res_operations, out res_proba, out partie_victorieuse);

                moyenne_operations = moyenne_operations + res_operations;
                moyenne_probas = moyenne_probas + res_proba;

                if (partie_victorieuse)
                {
                    pourcentage_parigagnant = pourcentage_parigagnant + 1;
                }


            }

            moyenne_operations = moyenne_operations / k;
            moyenne_probas = moyenne_probas / k;
            pourcentage_parigagnant = (pourcentage_parigagnant / k) * 100.0;

            Console.WriteLine($"Résultats statistiques sur {k} expériences :\n");
            Console.WriteLine($"En moyenne, il a fallu {moyenne_operations} mises avant qu'Aldo quitte le casino.");
            Console.WriteLine($"Sur {k} expériences aléatoires, {pourcentage_parigagnant}% d'entre elles ont permis à Aldo de sortir victorieux du casino.");
            Console.WriteLine($"Sur {k} expériences aléatoires, la probabilité moyenne d'arriver à cette issue était de {moyenne_probas * 100}%.");

            //Extraction de l'évolution du nombre de jetons au cours d'une partie

            /*List<int> variation_nbjetons = new List<int>();

            int res_operations;
            double res_proba;
            bool partie_victorieuse;

            variation_nbjetons = une_partie_de_roulettes(out res_operations, out res_proba, out partie_victorieuse);

            foreach(int etape in variation_nbjetons)
            {
                Console.WriteLine(etape);
            }*/
        }
    }
}

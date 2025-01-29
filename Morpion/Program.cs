using System;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Morpion
{
    class Program
    {
        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int j, int k)
        {
            Console.Write("+---+---+---+\n");
            for (var compteur = 0; compteur != 3; compteur++)
            {
                Console.Write("|");
                for (var i = 0; i < grille.GetLength(1); i++)
                {
                    string a;
                    if (grille[compteur, i] == 1) { a = "O"; }
                    else if (grille[compteur, i] == 2) { a = "X"; }
                    else { a = " "; }
                    Console.Write(" "+a.ToString()+" ");
                    Console.Write("|");
                }
                Console.Write("\n+---+---+---+\n");
            }
        }

        // Fonction permettant de vérifier que le joueur peut jouer, renvoie true si le coup est possible
        // Bien vérifier que le joueur ne sort pas du tableau et que la position n'est pas déjà jouée
        public static bool AJouer(int j, int k, int joueur)
        {
            // A compléter
            if ((grille[j, k] == 10) && (j >= 0 && j <= 2 && k >= 0 && k <= 2)) { return true; }
            else { return false; }
        }

        // Fonction permettant de vérifier si un joueur a gagné
        public static bool Gagner(int l, int c, int joueur)
        {
            if ((grille[l, 0] == grille[l, 1] && grille[l, 1] == grille[l, 2]) && grille[l, 0]!=10)
            { return true; } //Vérif la ligne l
            else if (grille[0, c] == grille[1, c] && grille[1, c] == grille[2, c] && grille[0,c]!= 10)
            { return true; } //verif la colonne c
            else if (  (grille[0, 0] == grille[1,1] && grille[1,1] == grille[2,2]) && grille[0,0] != 10   /*diagonale HG-->BD*/
                    || (grille[0, 2] == grille[1,1] && grille[1,1] == grille[2,0]) && grille[0,2] != 10 ) /*Diagonale HD-->BG*/
            {return true;}  //si condiion OU vraie alors renvoie true
            return false;   //Si on arrive au bout du code(essai=9), on renvoie false 
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations --
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre

            int essais = 0;    // compteur d'essais
	        int joueur = 2 ;   // 1 pour la premier joueur, 2 pour le second (commence à 2 car changé dès le dbut de while)
	        int l, c = 0;      // numéro de ligne et de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 
            bool bonnePosition = false; // Permet de vérifier si la position souhaité est disponible

            string erreurEntree = null;

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10
	        for (j=0; j < grille.GetLength(0); j++)
		        for (k=0; k < grille.GetLength(1); k++)
			        grille[j,k] = 10;
            while(gagner!=true && essais != 9)
            {
                // A compléter 
                try
                {
                    // Changement de joueur
                    if (erreurEntree == null)
                    {
                        if (joueur == 1) { joueur = 2; }
                        else { joueur = 1; }
                    }

                    //Affichage
                    Console.WriteLine("Tour du joueur " + joueur.ToString());
                    AfficherMorpion(j, k);

                    Console.WriteLine("Ligne   =    ");
                    Console.WriteLine("Colonne =    ");
                    Console.WriteLine("Coups restants : " + (9 - essais).ToString());
                    
                    if (erreurEntree != null)
                    {
                        Console.Write(erreurEntree);
                        erreurEntree = null;
                    }

                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 8); // Permet de manipuler le curseur dans la fenêtre 
                    l = int.Parse(Console.ReadLine()) - 1;
                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre 
                    c = int.Parse(Console.ReadLine()) - 1;

                    // A compléter 
                    if (AJouer(l, c, joueur) == true) 
                    {
                    //Increment essai
                    essais++;
                    //grid update
                    grille[l, c] = joueur;
                    gagner = Gagner(l, c, joueur);
                    }
                    else
                        {
                            if (grille[l,c]!=10 )
                            { erreurEntree="Vous ne pouvez pas placer votre marque sur une autre, réassayez."; }
                            else
                            { erreurEntree="Marque hors grille, n'entrez que des valeures entre 1 et 3."; }
                        }
                }
            
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }



                // A compléter 
                
                Console.Clear();//Efface la console pour que l'affichage soit fait au même endroit
            }; // Fin while

            // Fin de la partie
            // A compléter 
            if (gagner==true) 
                { Console.WriteLine("Victoire du joueur " + joueur.ToString()); }
            else 
                { Console.WriteLine("Égalité, pas de gagnant pour cette fois-ci."); }

            Console.ReadKey();
    }
  }
}
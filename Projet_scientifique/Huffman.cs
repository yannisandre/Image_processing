using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_scientifique
{
    public class Huffman
    // cette classe permet d'appliquer le traitement d'huffman à une image
    {
        public class Noeud
        // classe noeud qui permet de représenter les éléments de l'arbre d'Huffman
        {
            // L'arbre d'Huffman est un arbre binaire donc chaque noeud a un parent et potentiellement deux fils, un gauche et un droit
            // chaque noeud est également définit par un pixel et sa fréquence dans l'image associée
            private (uint,uint,uint) pixel; 
            private Noeud parent;
            private Noeud fils_gauche;
            private Noeud fils_droit;
            private uint frequence;
            

            public Noeud ((uint, uint, uint) pixel, Noeud fils_gauche, Noeud fils_droit, uint frequence, Noeud parent)
            // constructeur de la classe Noeud
            {
                this.pixel = pixel;
                this.fils_gauche = fils_gauche;
                this.fils_droit = fils_droit;
                this.frequence = frequence;
                this.parent = parent;
            }

            // propriété permettant de récupérer diverses informations sur le noeud courant
            public uint Frequence { get { return frequence; } }
            public Noeud Fils_gauche { get { return fils_gauche;  } set { fils_gauche = value; } }
            public Noeud Fils_droit { get { return fils_droit; } set { fils_droit = value; } }
            public Noeud Parent { get { return parent; } set { parent = value; } }
            public (uint,uint,uint) Pixel {  get { return pixel; } }
        }

        public class Arbre
        {
            private Noeud Root; // racine de l'arbre (dont la fréquence est égal au nombre de pixels de l'image)
            private Dictionary<(uint, uint, uint), uint> dictionnaire_frequences = new Dictionary<(uint,uint,uint), uint> (); // dictionnaire des fréquences des pixels de l'image, les clés sont les pixels et les valeurs associées sont leur fréquence
            private List<Noeud> liste_noeuds_feuilles = new List<Noeud>(); // liste des noeuds qui contiennent effectivement un pixel de l'image (ce seront les feuilles de l'arbre d'huffman)

            public Arbre(Program.Pixel[,] matrice_rgb)
            {
                
                dictionnaire_frequences[(matrice_rgb[0, 0].Bleu, matrice_rgb[0, 0].Vert, matrice_rgb[0,0].Rouge)] = 0; // pour chaque pixel on initie sa fréquence dans le dictionnaire
                
                for (int i = 0; i < matrice_rgb.GetLength(0); i++)
                {
                    
                    for (int j = 0; j < matrice_rgb.GetLength(1); j++)
                    // on parcourt tous les pixels de l'image
                    {
                        // si le pixel existe déjà dans le dictionnaire on augmente sa fréquence associée de 1
                        try { dictionnaire_frequences[(matrice_rgb[i, j].Bleu, matrice_rgb[i, j].Vert, matrice_rgb[i, j].Rouge)] += 1; }
                        // sinon on l'insère dans le dictionnaire et on initie sa fréquence à 1
                        catch { dictionnaire_frequences[(matrice_rgb[i, j].Bleu, matrice_rgb[i, j].Vert, matrice_rgb[i, j].Rouge)] = 1; }
                    }
                   
                }

                List <Noeud> liste_noeuds = new List<Noeud>(); // liste de noeuds qui va permettre de fusionner les noeuds contenant des pixels
                for (int k = 0; k < dictionnaire_frequences.Count; k++)
                {
                    // on crée les noeuds correspondants aux pixels de l'image associés à leur fréquence mais sans parents ni fils gauche et droit pour l'instant
                    Noeud noeud = new Noeud(dictionnaire_frequences.ElementAt(k).Key, null, null, dictionnaire_frequences.ElementAt(k).Value,null);
                    liste_noeuds.Add(noeud);
                }
                int indice_noeud_mini1; // indice représentant la position dans liste_noeuds du noeud contenant le pixel ayant la plus petite fréquence
                int indice_noeud_mini2; // indice représentant la position dans liste_noeuds du  noeud contenant le deuxième pixel ayant la plus petite fréquence

                // cette boucle while a pour objectif de fusionner à chaque itération les deux noeuds ayant les pixels avec la plus petite fréquence dans un nouveau noeud
                while (liste_noeuds.Count >= 2)
                // tant qu'il y a au moins deux noeuds
                {
                    // on initie d'abord les indice des noeuds avec les plus petites fréquence en faisant un test sur les deux premiers noeuds
                    if (liste_noeuds[0].Frequence < liste_noeuds[1].Frequence)
                    {
                        indice_noeud_mini1 = 0;
                        indice_noeud_mini2 = 1;
                    }
                    else
                    {
                        indice_noeud_mini1 = 1;
                        indice_noeud_mini2 = 0;
                    }
                    for (int i = 2; i < liste_noeuds.Count; i++)
                    // pour tous les noeuds de la liste
                    {
                        // si le noeud a la plus petite fréquence, l'indice du plus petit noeud est mit à jour
                        if (liste_noeuds[i].Frequence < liste_noeuds[indice_noeud_mini1].Frequence) { indice_noeud_mini1 = i; }
                        // si le noeud a la deuxième plus petite fréquence, l'indice du deuxième plus petit noeud est mit à jour
                        else if (liste_noeuds[i].Frequence < liste_noeuds[indice_noeud_mini2].Frequence) { indice_noeud_mini2 = i; }
                    }
                    // on crée un nouveau noeud dont on ne prend pas en compte la valeur de fréquence et de pixel, qui contient en fils gauche le noeud du pixel avec la plus petite fréquence
                    // et en fils droit le noeud du pixel ayant la deuxième plus petite fréquence
                    Noeud nouveau_noeud = new Noeud((256,256,256), liste_noeuds[indice_noeud_mini1], liste_noeuds[indice_noeud_mini2], liste_noeuds[indice_noeud_mini1].Frequence + liste_noeuds[indice_noeud_mini2].Frequence,null);
                    if (liste_noeuds[indice_noeud_mini1].Pixel != ((uint) 256, (uint) 256, (uint) 256))
                    // si le noeud à l'indice du plus petit s'avère être celui d'un pixel, on ajoute celui-ci à la liste crée auparavant
                    {
                        liste_noeuds_feuilles.Add(liste_noeuds[indice_noeud_mini1]);
                    }
                    if (liste_noeuds[indice_noeud_mini2].Pixel != ((uint)256, (uint)256, (uint)256))
                    // idem pour le noeud à l'indice du deuxième plus petit
                    {
                        liste_noeuds_feuilles.Add(liste_noeuds[indice_noeud_mini2]);
                    }
                    // on met à jour les noeuds concernés en leur donnant comme parent le nouveau noeud crée
                    liste_noeuds[indice_noeud_mini1].Parent = nouveau_noeud;
                    liste_noeuds[indice_noeud_mini2].Parent = nouveau_noeud;
                    liste_noeuds[indice_noeud_mini1] = nouveau_noeud; // on remplace le noeud le plus petit (cela pourrait aussi être le deuxième le plus petit, c'est arbitraire)
                    // on retire un des deux noeuds (ici le deuxième plus petit)
                    liste_noeuds.RemoveAt(indice_noeud_mini2);
                }
                Root = liste_noeuds[0]; // A la fin il ne reste qu'un seul noeud dans la liste, c'est la racine de l'arbre
                
            }

            public Dictionary<(uint, uint, uint), string> Compression_Huffman_binaire()
            // cette méthode retourne un dictionnaire qui associe à un pixel sa combinaison compressée en binaire (représentée en string ici) à l'aide de l'arbre d'huffman
            {
                Dictionary<(uint, uint, uint), string> liste_combinaisons = new Dictionary<(uint, uint, uint), string>(); // on crée le dictionnaire

                string combinaison_binaire = ""; // combinaison compressée binaire du pixel intitialisée
                Noeud noeud_courant;

                foreach (Noeud noeuds in liste_noeuds_feuilles)
                {
                    // pour chaque noeuds contenant un pixel
                    noeud_courant = noeuds;
                    combinaison_binaire = "";
                    while (noeud_courant.Parent != null)
                    {
                        // on remonte l'arbre et on ajoute un "0" à la combinaison si le noeud courant est le fils gauche de son parent
                        if (noeud_courant == noeud_courant.Parent.Fils_gauche)
                        {
                            combinaison_binaire = "0" + combinaison_binaire;
                        }
                        else
                        // on ajoute un "1" si c'est son fils droit
                        {
                            combinaison_binaire = "1" + combinaison_binaire;
                        }
                        noeud_courant = noeud_courant.Parent;
                    }
                    liste_combinaisons[noeuds.Pixel] = combinaison_binaire; // on ajoute au dictionnaire la combinaison binaire du pixel
                }

                return liste_combinaisons;
            }

            public List<string> obtenir_liste_combinaisons()
            // cette méthode permet simplement de retourner une liste de string contenant les combinaisons compressées générées lors de la compression d'huffman
            {
                Dictionary<(uint, uint, uint), string> dictionnaire_combinaisons = this.Compression_Huffman_binaire();
                List<string> liste_combinaisons = new List<string>();
                foreach ((uint,uint,uint) pixels in dictionnaire_combinaisons.Keys)
                {
                    liste_combinaisons.Add(dictionnaire_combinaisons[pixels]);
                }
                return liste_combinaisons;

            }

            public Dictionary<(uint,uint,uint),string> Decompression_Huffman(List<string> liste_combinaisons, Arbre arbre_huffman)
            // cette méthode permet de retrouver les pixels originaux de l'image à partir de leurs séquences compressées par huffman
            {
                Dictionary<(uint, uint, uint), string> liste_pixels_decompresses = new Dictionary<(uint, uint, uint), string>();
                foreach(string combinaisons in liste_combinaisons)
                {
                    Noeud parcourt_arbre = arbre_huffman.Root; // on se place sur la racine de l'arbre
                    
                    foreach(char bits in combinaisons)
                    // pour tous les bits de chaine compressée d'huffman
                    {
                        if (bits == '0') { parcourt_arbre = parcourt_arbre.Fils_gauche; } // si le bit est égal à 0 alors on se déplace sur le fils gauche du noeud courant
                        else { parcourt_arbre = parcourt_arbre.Fils_droit; } // si le bit est égal à 1 alors on se déplace sur le fils droit du noeud courant
                    }
                    (uint, uint, uint) pixel_decompresse = parcourt_arbre.Pixel; // quand on a finit de parcourir l'arbre avec la chaine compressée on récupère le pixel
                    liste_pixels_decompresses[pixel_decompresse] = combinaisons; // on associe dans le dictionnaire le pixel à sa compression huffman
                }
                return liste_pixels_decompresses;
            }


            public void affiche_combinaisons_binaires_compression()
            // méthode permettant d'afficher la liste des pixels ainsi que leur compression de huffman associée
            {
                Console.WriteLine("Pixel Rgb | code de compression");
                Console.WriteLine();
                Dictionary<(uint, uint, uint), string> dictionnairee_combinaisons = this.Compression_Huffman_binaire();
                for (int k = 0; k < dictionnairee_combinaisons.Count; k++) { ; Console.Write(dictionnairee_combinaisons.ElementAt(k).Key + " " + dictionnairee_combinaisons.ElementAt(k).Value); Console.WriteLine(); }
            }

            public void affiche_decompression()
            // méthode permettant d'afficher la liste des pixels décompressés en fonction des combinaisons de la compression
            {
                Console.WriteLine("Pixel Rgb | code de compression");
                Console.WriteLine();
                List<string> liste_combinaisons = this.obtenir_liste_combinaisons();
                Dictionary<(uint, uint, uint), string> decompression = this.Decompression_Huffman(liste_combinaisons, this);
                for (int k = 0; k < decompression.Count; k++) {; Console.Write(decompression.ElementAt(k).Key + " " + decompression.ElementAt(k).Value); Console.WriteLine(); }
            }

        }
        

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;


namespace projet_scientifique

{
    public class Program
    {
        
        static void affiche_mat(int[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // la liste des images disponible pour le traitement
            string[] liste_image =  { "coco", "lac", "lena", "oiseaux", "tour_eiffel", "cachees", "icone"};



            // les matrices de convolution qui vont permettre d'appliquer des filtres sur nos images

            float[,] flou_mat = { { 0.0625f, 0.125f, 0.0625f }, { 0.125f, 0.25f, 0.125f }, { 0.0625f, 0.125f, 0.0625f } };
            float[,] renforcement_contours_mat = { { 0f, -1f, 0f }, { -1f, 5f, -1f }, { 0f, -1f, 0f } };
            float[,] detecteur_contours_mat = { { 0f, 1f, 0f }, { 1f, -4f, 1f }, { 0f, 1f, 0f } };
            float[,] repoussage_mat = { { -2f, -1f, 0f }, { -1f, 1f, 1f }, { 0f, 1f, 2f } };
            float[,] augmenter_contraste_mat = { { 0f, -1f, 0f }, { -1f, 5f, -1f }, { 0f, -1f, 0f } };
            float[,] ameliorer_bord_mat = { { 0f, 0f, 0f }, { -1f, 1f, 0f }, { 0f, 0f, 0f } };
            float[,] filtre_de_sobel_mat = { { -1f, 0f, 1f }, { -2f, 0f, 2f }, { -1f, 0f, 0f } };
            List<float[,]> liste_filtres = new List<float[,]>();

            // on ajoute ces matrice à une liste

            liste_filtres.Add(flou_mat);
            liste_filtres.Add(renforcement_contours_mat);
            liste_filtres.Add(detecteur_contours_mat);
            liste_filtres.Add(repoussage_mat);
            liste_filtres.Add(augmenter_contraste_mat);
            liste_filtres.Add(ameliorer_bord_mat);
            liste_filtres.Add(filtre_de_sobel_mat);

            menu(liste_filtres, liste_image);

        }

        static void menu(List<float[,]> liste_filtres, string[] liste_image)
        // fonction principale permettant à l'utilisateur d'accéder aux différents traitement d'image possibles
        {
            Console.WriteLine("******Bienvenue sur le menu de notre éditeur d'image******");

            int choix_image = test_entree_entiere("Veuillez choisir une image parmis la liste suivante :" + "\n" + "1- coco : image de perroquet" + "\n" + "2- lac : une image d'un lac" + "\n" + "3- lena : l'image d'une femme" + "\n" + "4- oiseaux : image d'oiseaux" + "\n" + "5- tour_eiffel : une image de la tour eiffel" + "\n" + "6- une image contenant deux images cachées l'une dans l'autre");
            while (Convert.ToInt32(choix_image) - 1 < 0 || Convert.ToInt32(choix_image) - 1 >= liste_image.Length)
            {
                choix_image = test_entree_entiere("Veuillez choisir un numéro d'image valide");
            }
            Console.WriteLine("Quelle traitement voulez-vous effectuer ? : " + "\n" + "1-passage en nuance de gris" + "\n" + "2-passage en noir et blanc" + "\n" + "3-rotation d'un certain angle" + "\n" + "4-agrandissement/rétrécissement de l'image" + "\n" + "5-floutage par pixellisation" + "\n" + "6-application des filtres de convolution" + "\n" + "7-fractale de Mandelbrot" + "\n" + "8-coder une image dans une autre" + "\n" + "9-decoder deux images cachées l'une dans l'autre" + "\n" + "10-codage/décodage d'Huffman");
            string choix_traitement = "";
            do
            {
                Console.WriteLine("Veuillez choisir un numéro de traitement correcte");
                choix_traitement = Console.ReadLine();
            }
            while (choix_traitement != "1" && choix_traitement != "2" && choix_traitement != "3" && choix_traitement != "4" && choix_traitement != "5" && choix_traitement != "6" && choix_traitement != "7" && choix_traitement != "8" && choix_traitement != "9" && choix_traitement != "10");

            if (choix_traitement == "1") // on crée une image transoformée en nucances de gris
            {
                Myimage image_grise = new Myimage(liste_image[choix_image - 1]);
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                image_grise.image_en_nuances_de_gris(choix_nom);
                Console.WriteLine("L'image a bien été retranscrite en nuances de gris dans le fichier " + choix_nom);
            }

            else if (choix_traitement == "2") // on crée une image transformée en noir et blanc
            {
                Myimage image_grise = new Myimage(liste_image[choix_image - 1]);
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                image_grise.image_en_noir_et_blanc(choix_nom);
                Console.WriteLine("L'image a bien été retranscrite en noir et blanc dans le fichier " + choix_nom);
            }

            else if (choix_traitement == "3") // on crée une image transformée qui a subit une rotation de x degrés
            {
                Myimage image_rotation = new Myimage(liste_image[choix_image - 1]);
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                int angle = test_entree_entiere("Veuillez entrer un angle de rotation en degré (entier)");
                image_rotation.rotation_image(angle, choix_nom);
                Console.WriteLine("L'image a bien subie une rotation, le résutlat est disponible dans le fichier " + choix_nom);
            }

            else if (choix_traitement == "4") // on crée une image transformée qui a subit un agrandissement ou rétrécissement
            {
                Myimage image_taille_modifiee = new Myimage(liste_image[choix_image - 1]);
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                double angle = test_entree_reelle("Veuillez entrer un coefficient d'agrandissement (nombre réel)");
                image_taille_modifiee.agrandissement_image(angle, choix_nom);
                Console.WriteLine("La taille de l'image a bien été modifiée et cela dans le fichier " + choix_nom);
            }

            else if (choix_traitement == "5") // pixellisation méthode par blocs
            {
                Myimage image_pixelisee = new Myimage(liste_image[choix_image - 1]);
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                int ordre = test_entree_entiere("Veuillez choisir l'ordre (intensité) de la pixellisation (un entier)");
                image_pixelisee.floutage_image_pixelisation(ordre, choix_nom);
                Console.WriteLine("L'image a bien été foutée, le résultat se trouve dans le fichier " + choix_nom);
            }

            else if (choix_traitement == "6") // application des filtres de convolution
            {

                // on demande à l'utilisateur de choisir un des filtre

                Console.WriteLine("liste des filtres : " + "\n" + "1-floutage" + "\n" + "2-renforcement des contours" + "\n" + "3-detection des contours" + "\n" + "4-repoussage" + "\n" + "5-augmenter contraste" + "\n" + "6-ameliorer les bords" + "\n" + "7-filtre de sobel");
                int choix_filtre = test_entree_entiere("Veuillez entrer un numéro de filtre à appliquer");
                while (Convert.ToInt32(choix_filtre) - 1 < 0 || Convert.ToInt32(choix_filtre) - 1 >= liste_filtres.Count)
                {
                    choix_filtre = test_entree_entiere("Veuillez choisir un numéro de filtre valide");
                }

                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                Myimage image_filtree = new Myimage(liste_image[choix_image - 1]);
                // on l'applique
                image_filtree.produit_convolution(liste_filtres[choix_filtre - 1], choix_nom);
                Console.WriteLine("Le filtre a bien été appliqué à l'image, le résultat est disponible dans le fichier" + choix_nom);

            }
            else if (choix_traitement == "7")
            {
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                Myimage image_fractale = new Myimage(liste_image[choix_image - 1]);
                image_fractale.fractale_de_mandelbrot(choix_nom);
                Console.WriteLine("Une fractale de mandelbrot a bien été crée aux dimensions de l'image " + liste_image[choix_image - 1] + ", le résultat est disponible dans le fichier " + choix_nom);
            }
            else if (choix_traitement == "8")
            {
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer l'image traitée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);
                Console.WriteLine("Quelle image voulez-vous cacher parmis celles-ci ? : " + "\n" + "1-oiseaux" + "\n" + "2-tour eiffel");
                int choix_image_a_cachee;
                do { choix_image_a_cachee = test_entree_entiere("Veuillez entrer un numéro d'image valide (1 ou 2)"); }
                while(choix_image_a_cachee != 1 && choix_image_a_cachee != 2);
                Myimage image_support;
                Myimage image_a_cachee;
                if (choix_image_a_cachee == 1)
                {
                    image_a_cachee = new Myimage("oiseaux");
                    image_support = new Myimage("tour_eiffel");
                    Console.WriteLine("L'image des oiseaux a bien été cachée dans l'image tour_eiffel et tout cela dans le fichier image " + choix_nom);
                }
                else
                {
                    image_a_cachee = new Myimage("tour_eiffel");
                    image_support = new Myimage("oiseaux");
                    Console.WriteLine("L'image tour_eiffel a bien été cachée dans l'image des oiseaux et tout cela dans le fichier image " + choix_nom);
                }
                image_support.coder_image_dans_une_autre(image_a_cachee, choix_nom);
            }

            else if (choix_traitement == "9")
            {
                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer la première image cachée :");
                string choix_nom = Console.ReadLine();
                choix_nom = test_existe(choix_nom);

                Console.WriteLine("Veuillez choisir un nom de fichier dans lequel enregistrer la deuxième image cachée :");
                string choix_nom2 = Console.ReadLine();
                choix_nom2 = test_existe(choix_nom2);

                Myimage images_cachees = new Myimage(liste_image[choix_image - 1]);
                images_cachees.decoder_image(choix_nom, choix_nom2);
                Console.WriteLine("Les deux images cachées on bien été récupérées respectivement dans les fichiers " + choix_nom + " et " + choix_nom2);

            }

            else if (choix_traitement == "10")
            {
                Myimage image;
                Console.WriteLine("La compression d'huffman prend un temps raisonnable seulement pour des petites images et très peu colorées ");
                Console.WriteLine("Veuillez entrer un numéro d'image valide parmis celles-ci" + "\n" + "0-carré de pixels noirs et blancs" + "\n" + "1-image d'une femme");
                string choix_image2 = Console.ReadLine();
                while (choix_image2 != "0" && choix_image2 != "1")
                {
                    Console.WriteLine("Veuillez entrer un numéro d'image valide parmis celles-ci" + "\n" + "0-carré de pixels noirs et blancs" + "\n" + "1-image d'une femme");
                    choix_image2 = Console.ReadLine();
                }
                if (choix_image2 == "0")
                {
                    image = new Myimage("Test");
                }
                else
                {
                    image = new Myimage("lena");
                }

                Huffman.Arbre arbre = new Huffman.Arbre(image.Image);
                Console.WriteLine();
                Console.WriteLine("Compression... : ");
                arbre.affiche_combinaisons_binaires_compression();
                Console.WriteLine();
                Console.WriteLine("Décompression... : ");
                arbre.affiche_decompression();

            }
            menu(liste_filtres,liste_image);
        }

        static string test_existe(string choix_nom)
        {

            while (File.Exists("C:/Users/yanni/Desktop/Projet_scientifique/Projet_scientifique_Yannis_Andre_Emeric_Barbot_Come_Alexandre/bin/Debug/net6.0/Images/" + choix_nom + ".bmp"))
            {
                Console.WriteLine("Veuillez choisir un nom de fichier (non déjà utilisé) dans lequel enregistrer l'image traitée :");
                choix_nom = Console.ReadLine();
            }
            return choix_nom;
        }

        static double test_entree_reelle(string chaine)
        // cette méthode permet de forcer l'utilisateur à entrer une valeur réelle quand il le faut tout en ne faisant pas planter le programme
        {
            double entier;
            bool result;
            do
            {
                Console.WriteLine(chaine);
                string chaine_test = Console.ReadLine();
                result = double.TryParse(chaine_test, out entier);
            }
            while (!(result)); // tant que chaine_test n'est pas vérifiée en tant que nombre réel on redemande au client d'entrer un nombre réel
            return entier;
        }

        static int test_entree_entiere(string chaine)
        // cette méthode permet de forcer l'utilisateur à entrer une valeur entière quand il le faut tout en ne faisant pas planter le programme
        {
            int entier;
            bool result;
            do
            {
                Console.WriteLine(chaine);
                string chaine_test = Console.ReadLine();
                result = int.TryParse(chaine_test, out entier);
            }
            while (!(result)); // tant que chaine_test n'est pas vérifiée en tant qu'entier on redemande au client d'entrer un entier
            return entier;
        }

        static void affiche_tab(byte[] tab)
        // Pour afficher le contenu d'un tableau de byte utile pour vérifier les résultats
        {
            foreach (byte b in tab) { Console.Write(b + " "); }
            Console.WriteLine();
        }

        // Dans la suite du code toutes les variables et attributs seront initialisé(e)s en entiers non signés positifs (uint) afin de gagner en temps d'éxécution
        // En effet les valeurs étant définie positive sur un byte (les valeurs de couleur ne varient que entre 0 et 255) il n'est pas nécessaire d'allouer plus de place en mémoire pour ces attributs

        public class Pixel
        // Chaque pixel des images est représenté par un triplet de couleur bleu,vert,rouge, la matrice de couleur de l'image contient pour chaque pixel une instance de cette classe
        {
            private uint rouge;
            private uint vert;
            private uint bleu;

            public Pixel(uint bleu, uint vert, uint rouge)
            {
                this.rouge = rouge;
                this.vert = vert;
                this.bleu = bleu;
            }

            public void affiche_pixel() { Console.Write(Convert.ToString(rouge) + " " + Convert.ToString(vert) + " " + Convert.ToString(bleu) + " "); } // Permet d'afficher pour un pixel ses valeurs pour chaque couleur

            // Propriétés pour accéder en dehors de la classe aux valeurs des couleurs du pixel
            public uint Rouge { get { return rouge; } set { rouge = value; } }
            public uint Vert { get { return vert; } set { vert = value; } }
            public uint Bleu { get { return bleu; } set { bleu = value; } }
        }

        public class Myimage
        {
            private string type; // le type du fichier BM (Bitmap) par exemple coder sur deux byte
            private uint taille_fichier;
            private uint Offset;
            private uint largeur;
            private uint hauteur;
            private uint nombres_bits_par_couleurs;
            private uint taille_header;
            private uint nb_plans;
            private uint bitsCompression;
            private uint taille_image_octets;
            private uint resolution_horizontale;
            private uint resolution_verticale;
            private uint nombre_couleurs;
            private uint nombre_couleurs_importantes;
            private Pixel[,] image; // matrice de pixels RGB contenant tous les pixels de l'image

            public Myimage(string myfile) // Constructeur de la classe Myimage permettant de récolter toutes les informations du fichier bmp
            {
                byte[] mat = File.ReadAllBytes("./Images/" + myfile + ".bmp"); // On récupère les byte décrivant le fichier bmp de l'image
                string b = Convert.ToString((char)(int)mat[0]); // on récupère les deux premiers caractères indiquant le type du fichier sous forme de code ASCII
                string c = Convert.ToString((char)(int)mat[1]); // on cast d'abord le byte en int puis on le cast en char pour avoir la lettre correspondant au code ASCII puis on le convertir en string afin de pouvoir concaténer les deux lettres
                type = b + c; // le type est donc la concaténation des deux lettres récupérées ci-dessus
                taille_fichier = Convertir_Endian_To_Int(subtab(mat, 2, 5)); // on récupère les 4 prochains bytes écris en endian, on les convertit en int ce qui correspond à la taille en octet de l'image
                Offset = Convertir_Endian_To_Int(subtab(mat, 10, 13)); // offset de l'image récupéré
                taille_header = Convertir_Endian_To_Int(subtab(mat, 14, 17)); // taille du header
                largeur = Convertir_Endian_To_Int(subtab(mat, 18, 21)); // idem içi on récupère les 4 bytes écris en little endian décrivant la largeur de l'image
                hauteur = Convertir_Endian_To_Int(subtab(mat, 22, 25)); // même chose
                nb_plans = Convertir_Endian_To_Int(subtab(mat, 26, 27)); // nombres de plans de l'image
                nombres_bits_par_couleurs = Convertir_Endian_To_Int(subtab(mat, 28, 29)); // on récupère sur deux bytes le nombre bits par couleur situé deux bytes plus loin que la hauteur
                bitsCompression = Convertir_Endian_To_Int(subtab(mat, 30, 33)); // etc...
                taille_image_octets = Convertir_Endian_To_Int(subtab(mat, 34, 37));
                resolution_horizontale = Convertir_Endian_To_Int(subtab(mat, 38, 41));
                resolution_verticale = Convertir_Endian_To_Int(subtab(mat, 41, 44));
                nombre_couleurs = Convertir_Endian_To_Int(subtab(mat, 45, 48));
                nombre_couleurs_importantes = Convertir_Endian_To_Int(subtab(mat, 49, 52));

                // Ici on récupère tous les pixels de l'image et on les enregistre dans la matrice de Pixel qui contient donc hauteur * largeur pixels
                image = new Pixel[hauteur, largeur];
                int cpt_largeur = 0;
                int cpt_hauteur = 0;
                int cpt_nb_pixel = 0;
                Pixel new_pix;

                for (int i = 54; i < mat.Length; i += 3)
                {
                    if (cpt_nb_pixel == largeur * hauteur) { return; }
                    uint rouge = Convertir_Endian_To_Int(subtab(mat, i, i));
                    uint vert = Convertir_Endian_To_Int(subtab(mat, i + 1, i + 1));
                    uint bleu = Convertir_Endian_To_Int(subtab(mat, i + 2, i + 2));
                    new_pix = new Pixel(rouge, vert, bleu);
                    if (cpt_largeur == largeur)
                    {
                        cpt_hauteur++;
                        cpt_largeur = 0;
                    }
                    image[cpt_hauteur, cpt_largeur] = new_pix;
                    cpt_nb_pixel++;
                    cpt_largeur++;

                }



            }

            public static uint Convertir_Endian_To_Int(byte[] data)
            // Permet de convertir une liste de byte donnée en little endian en un entier correspondant
            {
                double result = 0;
                for (int i = 0; i < data.Length; i++)
                {

                    result += data[i] * Math.Pow(256, i);

                }
                return (uint)result;
            }

            public static byte[] Convertir_Int_To_Endian(uint data)
            // Permet de convertir un entier en un tableau de byte correspondant à la conversion de cet entier en format little endian
            {
                byte[] endian = new byte[4];
                for (int i = 3; i >= 0; i--)
                {
                    if (data > Math.Pow(256, i))
                    {
                        endian[i] = (byte)(data / Math.Pow(256, i));
                    }
                    else
                    {
                        endian[i] = 0;
                    }
                }
                return endian;
            }


            public void From_Image_To_File(string file)
            // cette méthode permet de générer un fichier .bmp à partir d'une instance de la classe MyImage
            {
                if (!File.Exists("C:/Users/yanni/Desktop/Projet_scientifique/Projet_scientifique_Yannis_Andre_Emeric_Barbot_Come_Alexandre/bin/Debug/net6.0/Images" + file + ".bmp"))
                {
                    byte[] texte = new byte[taille_fichier * 4]; // le fichier bmp va contenir autant de bits que l'image de l'instance c'est pourquoi le tableau contenant les bytes en aura autant
                    int indice = 0; // indice auquel placé le byte dans le tableau

                    // Header 
                    texte[indice] = (Convertir_Int_To_Endian((uint)type[0])[0]); // première lettre du type
                    indice++;
                    texte[indice] = (Convertir_Int_To_Endian((uint)type[1])[0]); // seconde lettre du type
                    indice++;
                    byte[] byte_temp = Convertir_Int_To_Endian(taille_fichier); // on récupère la conversion en byte de la taille de l'image
                    foreach (byte e in byte_temp) { texte[indice] = e; indice++; }; // on ajoute les bytes correspondants à la taille de l'image dans la liste
                    byte value0 = 0;
                    for (int i = 0; i < 4; i++) { texte[indice] = value0; indice++; } // on ajoute des bytes 0 pour les champs réservés
                    byte_temp = Convertir_Int_To_Endian(Offset); // on récupère les byte du offset
                    foreach (byte e in byte_temp) { texte[indice] = e; indice++; }; // on les ajoute au tableau de byte

                    // Header infos image
                    uint[] temp_tab = { taille_header, largeur, hauteur }; // pour simplifier le code on met les attributs dont on veut la conversion en little endian dans un tableau
                    foreach (uint attribut in temp_tab)
                    {
                        byte_temp = Convertir_Int_To_Endian(attribut); // on les convertit un à un
                        foreach (byte e in byte_temp) { texte[indice] = e; indice++; }; // puis on ajoute leurs bytes à l'indice correspondant dans le tableau
                    }

                    // les deux prochains attributs nb_plans et nombre_bits_par_couleurs ne sont pas présents dans le tableau car ils sont codés sur deux bytes
                    // et notre fonction de conversion convertit sur 4 bytes et on ne veut récupérer que les deux premiers dans ce cas car les deux autres ne sont pas utilisés
                    byte_temp = Convertir_Int_To_Endian(nb_plans);
                    texte[indice] = byte_temp[0]; // byte_temp est un tableau contenant les bytes de nb_plans on récupère le premier byte
                    indice++;
                    texte[indice] = byte_temp[1]; // on récupère le deuxième
                    indice++;
                    byte_temp = Convertir_Int_To_Endian(nombres_bits_par_couleurs);
                    texte[indice] = byte_temp[0]; // idem ici
                    indice++;
                    texte[indice] = byte_temp[1];
                    indice++;

                    uint[] temp_tab2 = { bitsCompression, taille_image_octets, resolution_horizontale, resolution_verticale, nombre_couleurs, nombre_couleurs_importantes }; // même chose qu'auparavant on fluidifie le code avec une boucle sur un tableau contenant les attributs


                    foreach (uint attribut in temp_tab2)
                    {
                        byte_temp = Convertir_Int_To_Endian(attribut);
                        foreach (byte e in byte_temp) { texte[indice] = e; indice++; };
                    }

                    // pixels de l'image
                    int cpt;
                    for (int i = 0; i < image.GetLength(0); i++)
                    // on parcourt chaque ligne de l'image
                    {
                        cpt = 0;
                        for (int j = 0; j < image.GetLength(1); j++)
                        // on parcourt chaque pixel de la ligne
                        {
                            texte[indice] = Convertir_Int_To_Endian(image[i, j].Bleu)[0];
                            indice++;
                            texte[indice] = Convertir_Int_To_Endian(image[i, j].Vert)[0];
                            indice++;
                            texte[indice] = Convertir_Int_To_Endian(image[i, j].Rouge)[0];
                            indice++;
                        }
                        if (cpt % 4 != 0) // si le nombre de pixel de la ligne courante n'est pas multiple de 4
                        {
                            while (cpt % 4 != 0) // on ajoute des pixels noirs tants que le nombre de pixel sur la ligne n'est pas un multiple de 4
                            {
                                texte[indice] = value0;
                                indice++;
                                cpt++;
                            }
                        }
                    }


                    File.WriteAllBytes("./Images/" + file + ".bmp", texte); // on écrit dans le dossier image du projet la ligne de byte ainsi crée ce qui a pour effet de créer un fichier bmp contenant l'image de l'instance Myimage
                    Process.Start("cmd", "/c start " + "./Images/" + file + ".bmp"); // ligne de code permettant d'ouvrir directement l'image ainsi crée dans la visionneuse image de l'ordinateur de l'utilisateur

                }
                else
                {
                    Console.WriteLine("nom de fichier " + file + " déjà utilisé, essayez-en un autre"); // si le nom de fichier souhaité existe déjà dans le dossier image on ne le remplace pas et on affiche un message d'erreur
                }
            }

            public void image_en_nuances_de_gris(string file)
            // cette méthode permet de convertir l'image de l'instance courante en nuances de gris
            {
                uint moyenne;
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        moyenne = (uint)(image[i, j].Bleu + image[i, j].Rouge + image[i, j].Vert) / 3; // on calcul la moyenne de la somme des valeurs du pixel en rouge,bleu et vert
                        image[i, j].Bleu = moyenne; // En effet cette moyenne va correspondre à une nuance de gris si on l'attribut à chaque couleur
                        image[i, j].Rouge = moyenne;
                        image[i, j].Vert = moyenne;

                    }
                }
                From_Image_To_File(file);
            }

            public void image_en_noir_et_blanc(string file)
            // cette méthode permet de convertir l'image de l'instance courante en noir et blanc
            {
                uint moyenne;
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        moyenne = (uint)(image[i, j].Bleu + image[i, j].Rouge + image[i, j].Vert) / 3; // Ici aussi on calcul la moyenne des trois couleurs du pixel
                        if (moyenne <= 128) // le palier 128 est choisit arbitrairement et comme 255 / 2 = 127.5 on aura un nombre à peu près égal de moyennes inférieures et supérieures à 128
                        {
                            // si la moyenne est inférieur à 128 on choisir de colorier le pixel en noir
                            image[i, j].Bleu = 0;
                            image[i, j].Rouge = 0;
                            image[i, j].Vert = 0;
                        }
                        else
                        {
                            // si c'est supérieur est on colorie le pixel en blanc
                            image[i, j].Bleu = 255;
                            image[i, j].Rouge = 255;
                            image[i, j].Vert = 255;

                        }

                    }
                }
                From_Image_To_File(file);
            }

            public void agrandissement_image(double coeff, string file)
            // méthode permettant de créer une image coeff fois plus grande que celle de l'instance courante dans le dossier image
            {
                if ((uint)(hauteur * coeff) == 256 || (uint)(largeur * coeff) == 256)
                {
                    coeff -= 0.1;
                }
                hauteur = (uint)(hauteur * coeff); // on multiplie la hauteur et la largeur par le coefficient d'agrandissement
                largeur = (uint)(largeur * coeff);
                taille_image_octets = hauteur * largeur * 3; // on met à jour le nombre d'octets et la taille du fichier
                taille_fichier = taille_image_octets + 54;
                Pixel[,] image_agrandie = new Pixel[hauteur, largeur]; // on crée une matrice de pixels rgb aux nouvelles dimensions
                for (int i = 0; i < image_agrandie.GetLength(0); i++)
                {
                    for (int j = 0; j < image_agrandie.GetLength(1); j++)
                    {
                        image_agrandie[i, j] = image[(int)(i / coeff), (int)(j / coeff)]; // le pixel situé à la ligne i et la colonne j de la nouvelle matrice va prendre pour valeur son plus proche voisin dans l'image avant agrandissement
                    }                                                                      // on calcul donc l'indice de ligne divisé par le ratio (coeff) ce qui va nous donner un nombre décimal que l'on arrondit en castant le tout en int ce qui va nous donner l'indice de ligne correspondant le plus proche, idem pour les colonnes
                }
                this.image = image_agrandie; // on remplace la matrice RGB de l'image d'origine par celle de la nouvelle (sans changer l'image initiale dans le dossier image bien sûr)
                From_Image_To_File(file);
            }

            public void rotation_image(int angle, string file)
            // méthode permettant de créer une image constituant la rotation d'un angle choisit de l'image de l'instance courante , le principe repose sur la recherche des indices de ligne et colonne finaux par multiplication par la matrice de rotation en deux dimensions des indices initiaux
            {
                double cosinus_angle = Math.Cos((angle - 90) * (Math.PI / 180)); // on commence par calculer le cosinus et le sinus de la matrice de rotation en dimension deux de l'angle choisit
                double sinus_angle = Math.Sin((angle - 90) * (Math.PI / 180)); // on soustrait 90 à l'angle choisit afin de tourner dans le bon sens car la matrice de pixel est définit sous le format bitmap (y comprit dans la matrice de pixel de l'image)
                uint decalage_centre_x = largeur / 2; // coordonnée en x (ligne) du point autour duquel on va tourner (en l'occurence le centre)
                uint decalage_centre_y = hauteur / 2; // idem mais coordonnée y (colonne) de ce même point
                Pixel pixel_noir = new Pixel(0, 0, 0); // pixel de couleur noir qui servira à remplir les éléments de l'image qui se retrouverait en dehors de l'image par rotation
                Pixel[,] new_image = new Pixel[hauteur, largeur]; // on crée une nouvelle matrice qui contiendra l'image ayant subie la rotation


                for (int i = 0; i < new_image.GetLength(0); i++)
                // dans la suite on multiplie les coordonnées de l'image initiale décalées par rapport au centre de l'image par la matrice de rotation de dimension 2 (voir document explicatif)
                {
                    double decalage_colonne1 = (i - decalage_centre_y) * sinus_angle;
                    double decalage_colonne2 = (i - decalage_centre_y) * cosinus_angle;

                    for (int j = 0; j < new_image.GetLength(1); j++)
                    {
                        int indice_ligne = (int)((j - decalage_centre_x) * cosinus_angle - decalage_colonne1 + decalage_centre_x);
                        int indice_colonne = (int)((j - decalage_centre_x) * sinus_angle + decalage_colonne2 + decalage_centre_y);
                        if (indice_ligne > 0 && indice_ligne < hauteur && indice_colonne > 0 && indice_colonne < largeur)
                        // si l'indice après rotation du pixel ne se trouve pas en dehors de l'image
                        {
                            new_image[i, j] = image[indice_ligne, indice_colonne]; // on affecte à cet indice dans la nouvelle matrice le pixel tourné correspondant de l'image initiale
                        }
                        else
                        // Sinon
                        {
                            new_image[i, j] = pixel_noir; // on affecte à l'indice correspondant un pixel de couleur noir (le pixel sortait de l'image)
                        }
                    }
                }
                this.image = new_image; // on remplace la matrice de l'image initiale pour laisser place à celle après rotation afin de pouvoir enregistrer cette nouvelle image (toujours sans supprimer l'image initiale
                From_Image_To_File(file);

            }

            public void floutage_image_pixelisation(int ordre, string file)
            // cette méthode permet de créer une image floutée à partir de l'image de l'instance actuelle
            {
                float somme_Rouge; // on 3 variables dans lesquels on va stocker toutes les valeurs respectivement de rouge, de vert de de bleu des pixels d'un bloc
                float somme_Vert;
                float somme_Bleu;
                for (int ligne = 0; ligne < hauteur / ordre; ligne++) // il y a hauteur/ordre blocs de surface ordre*ordre on boucle donc sur 0 hauteur/ordre - 1
                {
                    for (int colonne = 0; colonne < largeur / ordre; colonne++)
                    {
                        somme_Rouge = 0; // on remet à 0 les variables contenant les moyennes des couleurs rouge,verte et bleue
                        somme_Vert = 0;
                        somme_Bleu = 0;
                        for (int indice_i_pixel = 0; indice_i_pixel < ordre; indice_i_pixel++) // on parcourt les lignes du bloc 
                        {
                            for (int indice_j_pixel = 0; indice_j_pixel < ordre; indice_j_pixel++) // ses colonnes
                            {
                                Pixel pix = image[ligne * ordre + indice_i_pixel, colonne * ordre + indice_j_pixel]; // l'indice de ligne du pixel du bloc dans la matrice rgb correspond donc à indice de la ligne * ordre + indice de ligne courant reflexion analogue pour l'indice de colonne
                                somme_Rouge += pix.Rouge; // on somme la valeur de rouge du pixel
                                somme_Bleu += pix.Bleu; // idem pour le bleu et vert
                                somme_Vert += pix.Vert;
                            }

                        }
                        // une fois le bloc parcourut entièrement on calcule la moyenne des couleurs rouge,bleu et vert du bloc ( on divise par le nombre de pixels = ordre * ordre)
                        uint moyenne_rouge_entier = (uint)(somme_Rouge / (ordre * ordre)); // en entier bien sûr car les valeurs de couleurs sont définies en entier
                        uint moyenne_bleu_entier = (uint)(somme_Bleu / (ordre * ordre));
                        uint moyenne_vert_entier = (uint)(somme_Vert / (ordre * ordre));

                        // ensuite pour tous les pixels du bloc on leur affecte les valeurs moyennes de rouge, vert et bleu afin d'avoir un bloc plus ou moins uniforme ce qui donne un effet pixelisant de floutage
                        for (int indice_i_pixel = 0; indice_i_pixel < ordre; indice_i_pixel++)
                        {
                            for (int indice_j_pixel = 0; indice_j_pixel < ordre; indice_j_pixel++)
                            {

                                image[ligne * ordre + indice_i_pixel, colonne * ordre + indice_j_pixel].Rouge = moyenne_rouge_entier;
                                image[ligne * ordre + indice_i_pixel, colonne * ordre + indice_j_pixel].Bleu = moyenne_bleu_entier;
                                image[ligne * ordre + indice_i_pixel, colonne * ordre + indice_j_pixel].Vert = moyenne_vert_entier;

                            }

                        }
                    }
                }
                From_Image_To_File(file);
            }


            public void produit_convolution(float[,] motif, string file)
            // cette méthode permet d'appliquer un filtre de convolution 3*3 à l'image de l'instance courante en fonction du motif de convolution (matrice) que l'on donne en paramètre
            {
                double somme_rouge; // on initialise les sommes des différentes composantes rgb des pixels
                double somme_vert;
                double somme_bleu;
                int indice_matrice_ligne; // ces indices serviront à multiplier les bons coefficients de la matrice de convolution
                int indice_matrice_colonne;
                // on définit la matrice new_mat comme la matrice de pixel rgb de l'image de l'instante courante à laquelle on a rajouté deux lignes et deux colonnes afin de pouvoir traiter les bords ce qui serait impossible sinon avec un motif 3*3
                Pixel[,] new_mat = new Pixel[hauteur + 2, largeur + 2];
                Pixel[,] new_mat2 = new Pixel[hauteur + 2, largeur + 2]; // cette matrice sera une copie de la matrice new_mat afin de ne pas modifier cette dernière lors du traitement
                Pixel pixel_noir = new Pixel(0, 0, 0); // on va remplir les bords de new_mat avec des pixels ayant pour couleur le noir


                for (int i = 0; i < hauteur + 2; i++)
                {
                    for (int j = 0; j < largeur + 2; j++)
                    {

                        if (i == 0 || j == 0 || i == hauteur + 1 || j == largeur + 1) { new_mat[i, j] = pixel_noir; new_mat2[i, j] = pixel_noir; } // si le pixel se trouve sur la première ou dernière colonne ou ligne on le colorie en noir (0,0,0)
                        else { new_mat[i, j] = image[i - 1, j - 1]; new_mat2[i, j] = image[i - 1, j - 1]; } // sinon on lui affecte le pixel correspondant dans la matrice rgb de l'image de base (en faisant en sorte que les contours soient noirs)
                    }
                }

                for (int i = 1; i < hauteur + 1; i++) // on réalise une double boucle sur les pixels non noirs (tous sauf ceux du bord) de la nouvelle matrice
                {
                    for (int j = 1; j < largeur + 1; j++)
                    {
                        somme_rouge = 0; // on remet les sommes de couleur à 0
                        somme_vert = 0;
                        somme_bleu = 0;
                        indice_matrice_ligne = 0; // idem pour les indices de la matrice de convolution
                        indice_matrice_colonne = 0;
                        for (int k = i - 1; k <= i + 1; k++) // on parcourt à l'aide d'une double boucle allant de i-1 à i+1 et de j-1 à j+1 les 8 sommets adjacents au pixel traité
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                // on additionne les valeurs des couleurs multipliées par le coefficient correspondant dans le motif de ces pixels dans la variable somme_rouge, somme_vert et somme_bleu
                                somme_rouge += new_mat2[k, l].Rouge * motif[indice_matrice_ligne, indice_matrice_colonne];
                                somme_vert += new_mat2[k, l].Vert * motif[indice_matrice_ligne, indice_matrice_colonne];
                                somme_bleu += new_mat2[k, l].Bleu * motif[indice_matrice_ligne, indice_matrice_colonne];
                                indice_matrice_colonne++; // on passe à la colonne suivante de la matrice de convolution
                            }
                            indice_matrice_colonne = 0; // on se replace sur la première colonne du motif
                            indice_matrice_ligne++; // on passe à la ligne suivante de la matrice de convolution
                        }
                        Pixel new_pix = new Pixel((uint)somme_bleu, (uint)somme_vert, (uint)somme_rouge); // on crée un nouveau pixel à partir des sommes que l'on a calculé juste avant
                        new_mat[i, j] = new_pix; // on insère ce pixel dans la matrice new_mat

                    }
                }
                for (int i = 1; i < hauteur + 1; i++) // enfin içi on actualise la matrice de l'image afin de la faire correspondre à new_mat à laquelle on a appliqué le filtre
                {
                    for (int j = 1; j < largeur + 1; j++)
                    {
                        image[i - 1, j - 1] = new_mat[i, j];
                    }
                }

                From_Image_To_File(file);

            }


            public void fractale_de_mandelbrot(string file)
            // cette méthode permet de générer une image de même taille que celle de l'instance courante contenant la figure de Mandelbrot constitué des points appartenant à son ensemble, décrivant ainsi une fractale
            {
                int maxIterations = 100; // le nombre d'itérations à partir duquel on considère que le point considérer fait partie de l'ensemble de Mandelbrot (arbitraire)
                // on choisit de restreindre les bornes des calculs de pixels car les la figure de Mandelbrot est plus ou moins contenue dans cet intervalle
                double xMin = -2.5;  // borne inférieure des abscisses
                double xMax = 1; // borne supérieure des abscisses
                double yMin = -1; // idem pour les ordonnées
                double yMax = 1;
                int delta = 1; // choix du pas pour le calcul des points présents dans l'ensemble de Mandelbrot (arbitraire)

                Pixel pix_noir = new Pixel(0, 0, 0); // pixel de couleur noire

                // on remplit la matrice rgb de l'image de noir
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        image[i, j] = pix_noir;
                    }
                }

                // Calcul des valeurs pour chaque pixel de l'image
                for (int x = 0; x < largeur; x += delta)
                {
                    for (int y = 0; y < hauteur; y += delta)
                    {

                        double x0 = xMin + x * (xMax - xMin) / largeur; // partie réelle du nombre complexe c = x0 + i*y0
                        double y0 = yMin + y * (yMax - yMin) / hauteur; // partie imaginaire du nombre complexe c = x0 + i*y0

                        int iterations = 0; // on initialise le compteur

                        double x1 = 0; // partie réelle du nombre complexe z = (x1,y1)
                        double y1 = 0; // partie imaginaire du nombre complexe z = (x1,y1)
                        // dans la suite on peut considérer que le point reste proche de l'origine lorsque son module ne dépasse pas 2
                        // Or le module de z = sqrt(x1*x1+y1*y1), -> sqrt(x1*x1+y1*y1) <= 2 -> x1*x1+y1*y1 <= 4
                        while (x1 * x1 + y1 * y1 <= 4 && iterations < maxIterations) // tant que la distance à l'origine de z^2 + c (son module) est inférieur à 2 et que le nombre d'itérations est inférieur au seuil définit
                        {
                            // On calcul z^2 + c
                            double xtemp = x1 * x1 - y1 * y1 + x0; // partie réelle de z^2 + c 
                            y1 = 2 * x1 * y1 + y0; // partie imaginaire de z^2 + c
                            x1 = xtemp;
                            iterations++;
                        }


                        uint couleur = (uint)((255.0 * (double)iterations) / (double)maxIterations); // donne une nuance de couleur entre (0 et 255) en fonction du nombre d'itérations qu'il faut à un point du plan complexe pour que le module de z^2 + c diverge
                        Pixel pixe = new Pixel(couleur, couleur, couleur); // on attribue cette même valeur à chaque composante du pixel courant de l'image afin d'avoir une nuance de gris
                        image[y, x] = pixe;

                    }   // en fin de compte les points les plus blancs seront ceux dont le nombre d'itération se rapproche du nombre d'itérations max que l'on a définit et donc ceux qui sont les plus susceptible d'appartenir effectivement à l'ensemble de Mandelbrot
                }
                From_Image_To_File(file);

            }

            public uint[] Conversion_binaire(uint couleur)
            // cette méthode prend une valeur entière postive entre 0 et 255 et retourne son écriture binaire sur 8 bits (elle retourne un tableau d'entier non signés (0 ou 1)
            {
                uint[] resultat = new uint[8]; // on initialise le tableau de 8 entiers à {0,0,0,0,0,0,0,0}
                int i = 7; // dernier indice du tableau resultat
                do
                {
                    if (couleur % 2 != 0) // si couleur n'est pas un multiple de 2
                    {
                        resultat[i] = 1; // on met à 1 le i-ième élément du tableau
                    }
                    couleur = (uint)(couleur / 2.0); // on divise couleur par 2 et on prend par la valeur entière du résulat
                    i--;
                }
                while (couleur != 0); // on recommence tant que la valeur entière ne vaut pas 0
                return resultat; // on retourne le tableau d'entier
            }

            public uint Conversion_entier(uint[] nombre_binaire)
            // cette prend en argument un tableau d'entiers représentant l'écriture binaire d'un nombre, elle permet de le convertir en écriture décimale
            {
                uint somme = 0; // on initialise la somme
                for (int i = 0; i < 8; i++) // pour tous les éléments du tableau
                {
                    somme += (uint)(nombre_binaire[i] * Math.Pow(2, 8 - i - 1)); // comme on parcourt le tableau de gauche à droite et qu'on veut ajouter à la somme le i-ième élément * 2^i en partant de la gauche, on soustrait à 8 i et -1 pour décaler les indices
                }
                return somme; // on retourne le résultat
            }

            public void coder_image_dans_une_autre(Myimage image2, string file)
            // cette méthode permet de cacher image2 dans l'image de l'instance courante et enregistre le résultat dans le dossier image du projet sous le nom de fichier file
            // le principe repose sur le fait de ne conserver que les bits à gauche des composante rgb des pixels de la première image et d'y ajouter les bits à gauche de celles de l'image à caché.
            // en effet les bits à gauche des composantes sont de poids fort (ils ont plus de valeur numérique) ce qui fait que l'essentiel de l'information est conservée en gardant ces bits
            {

                Pixel[,] matrice_image2 = image2.Image; // Récupération de la matrice RGB de image2
                Pixel[,] new_image = new Pixel[hauteur, largeur]; // on crée la matrice de la nouvelle image 
                Pixel nul = new Pixel(0, 0, 0); // pixel noir
                for (int i = 0; i < hauteur; i++) // on remplit la matrice de la nouvelle image par des pixels noirs afin de pouvoir appliquer des méthodes sur les éléments de la matrice plus tard
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        nul = new Pixel(0, 0, 0);
                        new_image[i, j] = nul;
                    }
                }

                for (int i = 0; i < hauteur; i++) // pour tous les pixels de la nouvelle image
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        uint[] quatre_premiers_bits_rouges = this.subtab2(this.Conversion_binaire(image[i, j].Rouge), 0, 3); // on récupère dans un tableau d'entier les 4 premiers bits de l'écriture binaire de la composante rouge du pixel de l'image d'instance d'indice [i,j]
                        uint[] quatre_derniers_bits_rouges = this.subtab2(this.Conversion_binaire(matrice_image2[i, j].Rouge), 0, 3); // on récupère dans un tableau d'entier les 4 premiers bits de l'écriture binaire de la composante rouge du pixel de l'image à cacher d'indice [i,j]
                        uint[] huits_bits_rouges = this.fusion_tableaux(quatre_premiers_bits_rouges, quatre_derniers_bits_rouges); // on fusionne les deux tableaux obtenus pour avoir un tableau contenant les 4 bits du premiers suivis des 4 bits du second formant ainsi l'écriture binaire d'une composante rouge
                        uint valeur_rouge = this.Conversion_entier(huits_bits_rouges); // on convertit en base décimale la composante rouge ainsi obtenue
                        new_image[i, j].Rouge = valeur_rouge; // on l'affecte au pixel d'indice [i,j]

                        // même procédé pour la composante bleu du pixel
                        uint[] quatre_premiers_bits_bleus = this.subtab2(this.Conversion_binaire(image[i, j].Bleu), 0, 3);
                        uint[] quatre_derniers_bits_bleus = this.subtab2(this.Conversion_binaire(matrice_image2[i, j].Bleu), 0, 3);
                        uint[] huits_bits_bleus = this.fusion_tableaux(quatre_premiers_bits_bleus, quatre_derniers_bits_bleus);
                        uint valeur_bleue = this.Conversion_entier(huits_bits_bleus);
                        new_image[i, j].Bleu = valeur_bleue;

                        // idem pour la composante verte du pixel
                        uint[] quatre_premiers_bits_verts = this.subtab2(this.Conversion_binaire(image[i, j].Vert), 0, 3);
                        uint[] quatre_derniers_bits_verts = this.subtab2(this.Conversion_binaire(matrice_image2[i, j].Vert), 0, 3);
                        uint[] huits_bits_verts = this.fusion_tableaux(quatre_premiers_bits_verts, quatre_derniers_bits_verts);
                        uint valeur_verte = this.Conversion_entier(huits_bits_verts);
                        new_image[i, j].Vert = valeur_verte;

                    }
                }
                this.image = new_image; // on met à jour l'image avec la nouvelle matrice afin de créer une nouvelle image
                From_Image_To_File(file); // on enregistre l'image dans le dossier image du projet

            }

            public void decoder_image(string image1, string image2)
            // cette méthode prend en argument une image dans laquelle a été cachée une autre image (notamment avec la méthode coder_image_dans_une_autre ci-dessus) 
            // elle donne en sortie deux images, l'image cachée (image2), et celle qui a servit de cachette (image1) et enregistre les deux fichiers dans le dossier image du projet
            {
                Pixel[,] new_image1 = new Pixel[hauteur, largeur]; // on initie les matrices rgb qui vont permettre de récupérer les composantes de leurs pixels respectifs
                Pixel[,] new_image2 = new Pixel[hauteur, largeur];
                Pixel nul = new Pixel(0, 0, 0); // pixel noir
                for (int i = 0; i < hauteur; i++) // on remplit les deux matrices rgb des deux images de pixels noirs afin de pouvoir y appliquer des méthodes par la suite
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        nul = new Pixel(0, 0, 0);
                        new_image1[i, j] = nul;
                        nul = new Pixel(0, 0, 0);
                        new_image2[i, j] = nul;
                    }
                }
                uint[] tab_nul = { 0, 0, 0, 0 }; // tableau de 4 bits égales à 0
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++) // pour chaque pixels de l'image donnée en entrée
                    {
                        // pour chaque composante rouge,verte et bleue de l'image qui a servie de cachette on récupère la fusion des 4 premiers bits de l'écriture décimal (bits de poids fort de l'image cachette) de la composante avec 4 bits nuls
                        // puis on convertit cette écriture en base décimale pour obtenir la valeur de la composante
                        new_image1[i, j].Bleu = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Bleu), 0, 3), tab_nul));
                        new_image1[i, j].Rouge = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Rouge), 0, 3), tab_nul));
                        new_image1[i, j].Vert = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Vert), 0, 3), tab_nul));

                        // le principe est similaire pour l'image qui a été cachée à l'exception que l'on fusionne les 4 derniers bits de l'écriture binaire des composantes (bits de poids fort qui ont été placés ici par la méthode ci-dessus)
                        new_image2[i, j].Bleu = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Bleu), 4, 7), tab_nul));
                        new_image2[i, j].Rouge = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Rouge), 4, 7), tab_nul));
                        new_image2[i, j].Vert = this.Conversion_entier(this.fusion_tableaux(this.subtab2(this.Conversion_binaire(image[i, j].Vert), 4, 7), tab_nul));
                    }
                }

                this.image = new_image1;
                From_Image_To_File(image1); // on enregistre d'abord l'image qui a servie de cachette

                this.image = new_image2;
                From_Image_To_File(image2); // puis on enregistre l'image qui a été cachée dans l'image fournie en entrée

            }

            public uint[] fusion_tableaux(uint[] tab1, uint[] tab2)
            // cette méthode prend en paramètre deux tableaux de même taille et retourne leur fusion (un tableau deux fois plus grand contenant les éléments des deux tableaux)
            {
                uint[] new_tab = new uint[tab1.Length + tab2.Length];
                for (int i = 0; i < tab1.Length; i++)
                {
                    new_tab[i] = tab1[i];
                    new_tab[i + tab1.Length] = tab2[i];
                }
                return new_tab;
            }

            public uint[] subtab2(uint[] tab, int debut, int fin)
            // méthode permettant de récupérer une partie choisie du tableau de byte décrivant l'image bmp que l'on veut convertir en instance de Myimage.
            // la portion du tableau récupérée va de debut inclut à fin incluse
            {
                uint[] new_tab = new uint[fin - debut + 1];
                int cpt = 0;
                for (int i = debut; i <= fin; i++)
                {
                    new_tab[cpt] = tab[i];
                    cpt++;
                }
                return new_tab;
            }



            public static byte[] subtab(byte[] tab, int debut, int fin)
            // méthode permettant de récupérer une partie choisie du tableau de byte décrivant l'image bmp que l'on veut convertir en instance de Myimage.
            // la portion du tableau récupérée va de debut inclut à fin incluse
            {
                byte[] new_tab = new byte[fin - debut + 1];
                int cpt = 0;
                for (int i = debut; i <= fin; i++)
                {
                    new_tab[cpt] = tab[i];
                    cpt++;
                }
                return new_tab;
            }

            // propriétés permettant d'accéder à certains attributs de la classe en dehors de celle-ci
            public uint Taille { get { return this.taille_fichier; } }
            public uint Largeur { get { return this.largeur; } }
            public uint Hauteur { get { return this.hauteur; } }
            public Pixel[,] Image { get { return this.image; } set { this.image = value; } }

            public void affiche_image()
            // cette méthode permet d'afficher la description des bits (bleu,vert,rouge) de l'image dans la console
            {

                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        image[i, j].affiche_pixel();
                    }
                    Console.WriteLine();

                }
                Console.WriteLine();
            }
        }

    }
}

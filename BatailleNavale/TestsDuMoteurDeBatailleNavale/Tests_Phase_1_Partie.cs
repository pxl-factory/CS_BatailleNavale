using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using MoteurDeBatailleNavale;
using static MoteurDeBatailleNavale.Enumeration;

namespace TestsDuMoteurDeBatailleNavale
{
    [TestClass]
    public class Tests_PhasjoueurTeste_1_Partie
    {
        [TestMethod]
        public void Phase1_1_CoordonnéesDeBatailleNavale()
        {
            // vérification du constructeur par défaut non public
            Type t = typeof(CoordonnéesDeBatailleNavale);
            ConstructorInfo[] constructeursPubliques = t.GetConstructors();
            foreach (ConstructorInfo constructeur in constructeursPubliques)
            {
                // recherche du constructeur sans paramètre
                if (constructeur.GetParameters().Length == 0)
                {
                    // vérification de visibilité
                    Assert.IsFalse(constructeur.IsPublic, "Le constructeur par défaut ne doit pas être public.");
                }
            }
            // vérification que les paramètres hors plage valide produisent une exception IndexOutOfRangeException
            bool ThrowException = false;
            try
            {
                ThrowException = false;
                for (char c = char.MinValue; c < 'A'; c++)
                {
                    CoordonnéesDeBatailleNavale coordonnéeInvalide = new CoordonnéesDeBatailleNavale(c, 1);
                }
                for (char c = (char)('J' + 1); c < char.MaxValue; c++)
                {
                    CoordonnéesDeBatailleNavale coordonnéeInvalide = new
                   CoordonnéesDeBatailleNavale(c, 1);
                }
                CoordonnéesDeBatailleNavale coordonnéeInvalide2 = new
               CoordonnéesDeBatailleNavale('A', 0);
                for (byte l = 11; l < byte.MaxValue; l++)
                {
                    CoordonnéesDeBatailleNavale coordonnéeInvalide = new
                   CoordonnéesDeBatailleNavale('A', l);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ThrowException = true;
            }
            Assert.IsTrue(ThrowException, "La construction de CoordonnéesDeBatailleNavale ne doit accepter que des colonnes de 'A' à 'J' et des lignes de 1 à 10");
            // vérification que les paramètres dans la plage valide ne produisent pas d'exception
            try
            {
                ThrowException = false;
                for (char c = 'A'; c <= 'J'; c++)
                {
                    for (byte l = 1; l <= 10; l++)
                    {
                        CoordonnéesDeBatailleNavale coordonnéeInvalide = new
                       CoordonnéesDeBatailleNavale(c, l);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ThrowException = true;
            }
            Assert.IsFalse(ThrowException, "La construction de CoordonnéesDeBatailleNavale doit accepter des colonnes entre 'A' et 'J' et des lignes entre 1 et 10");
            // vérification de la méthode Equals
            try
            {
                CoordonnéesDeBatailleNavale uneInstance = new
               CoordonnéesDeBatailleNavale('C', 5);
                uneInstance.Equals(null);
                uneInstance.Equals(new object());
                uneInstance.Equals(uneInstance);
            }
            catch
            {
                Assert.Fail("Le test d'égalité ne doit pas provoquer d'exception");
            }
            Assert.IsTrue(new CoordonnéesDeBatailleNavale('C', 5).Equals(new
           CoordonnéesDeBatailleNavale('C', 5)), "L'égalité C5 avec C5 doit être vraie");
            Assert.IsFalse(new CoordonnéesDeBatailleNavale('A', 1).Equals(null),
           "L'égalité avec null doit être fausse");
            Assert.IsFalse(new CoordonnéesDeBatailleNavale('A', 1).Equals(new
           CoordonnéesDeBatailleNavale('A', 2)), "L'égalité A1 avec A2 doit être fausse");
            Assert.IsFalse(new CoordonnéesDeBatailleNavale('A', 1).Equals("A1"),
           "L'égalité entre deux types différents doit être fausse");
        }

        [TestMethod]

        public void Phase_2_1_UneSectionDeNavire()
        {
            Type t = typeof(UneSectionDeNavire);
            ConstructorInfo[] constructeursPubliques = t.GetConstructors();
            bool constructeurParDefautPublic = false;
            foreach (ConstructorInfo constructeur in constructeursPubliques)
            {
                // recherche du constructeur sans paramètre
                if (constructeur.GetParameters().Length == 0)
                {
                    constructeurParDefautPublic = true;
                }
            }
            Assert.IsTrue(constructeurParDefautPublic, "Le constructeur de UneSectionDeNavire par défaut doit être public.");
            UneSectionDeNavire section = new UneSectionDeNavire();
            Assert.AreEqual(section.Etat, EtatDeSectionDeNavire.Intact, "L'état doit être initialisé à Intact");
            Assert.IsNotNull(section.Position, "La position ne peut pas être null");
            Assert.AreEqual(section.Position, new CoordonnéesDeBatailleNavale('A', 1),
           "La position doit être initialisée avec A1 ");
        }


        public void Phase_2_2_UnNavire()
        {
            Type t = typeof(UnNavire);
            ConstructorInfo[] constructeursPubliques = t.GetConstructors();
            bool constructeurParDefautPublic = false;
            foreach (ConstructorInfo constructeur in constructeursPubliques)
            {
                // recherche du constructeur sans paramètre
                if (constructeur.GetParameters().Length == 0)
                {
                    constructeurParDefautPublic = true;
                }
            }
            Assert.IsFalse(constructeurParDefautPublic, "Le constructeur de UnNavire par défaut ne doit pas être public.");
            bool testConstructeurNomNull = false;
            try
            {
                UnNavire testConstructeur = new UnNavire(null, 5);
            }
            catch (ArgumentNullException)
            {
                testConstructeurNomNull = true;
            }
            Assert.IsTrue(testConstructeurNomNull, "Le nom du navire ne peut pas être null");

            bool testConstructeurNomVide = false;
            try
            {
                UnNavire testConstructeur = new UnNavire("", 5);
            }
            catch (ArgumentNullException)
            {
                testConstructeurNomVide = true;
            }
            Assert.IsTrue(testConstructeurNomVide, "Le nom du navire ne peut pas être vide");

            bool testNbSectionValide = false;
            try
            {
                for (byte s = 0; s < 2; s++)
                {
                    UnNavire testConstructeur = new UnNavire("NomValide", s);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                testNbSectionValide = true;
            }
            Assert.IsTrue(testNbSectionValide, "Le nombre de section ne peut être inférieur à 2");
            try
            {
                for (byte s = 6; s < byte.MaxValue; s++)
                {
                    UnNavire testConstructeur = new UnNavire("NomValide", s);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                testNbSectionValide = true;
            }
            Assert.IsTrue(testNbSectionValide, "Le nombre de section ne peut être supérieur à 5");
            for (byte taille = 2; taille < 6; taille++)
            {
                try
                {
                    UnNavire navireDeTailleCorrecte = new UnNavire("MonNavire",
                   taille);
                    Assert.AreEqual(navireDeTailleCorrecte.Sections.Length, taille,
                    "Le nombre de sections doit être initialisé par le constructeur");
                }
                catch
                {
                    Assert.Fail("La construction du navire doit accepter une taille entre 2 minimum et 5 maximum");
                }
            }
            UnNavire navire = new UnNavire("Nom_TEST", 5);
            Assert.AreEqual(navire.Nom, "Nom_TEST", "Le nom du navire doit être initialisé par le constructeur");
        }


        private string[] NomsDesNaviresDeLaFlotte
        {
            get
            {
                return new string[] { "porte avion", "croiseur", "contre torpilleur", "sous-marin", "torpilleur" };
            }
        }
        private byte[] TaillesDesNaviresDeLaFlotte
        {
            get
            { return new byte[] { 5, 4, 3, 3, 2 }; }
        }
        [TestMethod]
        public void Phase_2_3_UneFlotteDeNavires()
        {
            Type t = typeof(UneFlotteDeNavires);
            ConstructorInfo[] constructeursPubliques = t.GetConstructors();
            bool constructeurParDefautPublic = false;
            foreach (ConstructorInfo constructeur in constructeursPubliques)
            {
                // recherche du constructeur sans paramètre
                if (constructeur.GetParameters().Length == 0)
                {
                    constructeurParDefautPublic = true;
                }
            }
            Assert.IsTrue(constructeurParDefautPublic, "Le constructeur de UneFlotteDeNavires par défaut doit être public.");
            UneFlotteDeNavires flotte = new UneFlotteDeNavires();
            Assert.IsTrue(flotte.Navires.Length == 5, "La flotte de navire doit être composée de 5 navires exactement");
            int navireIndex = 0;
            foreach (UnNavire navire in flotte.Navires)
            {
                Assert.AreEqual(NomsDesNaviresDeLaFlotte[navireIndex], navire.Nom, "Ce navire ne porte pas le bon nom");
                Assert.AreEqual(TaillesDesNaviresDeLaFlotte[navireIndex],
               navire.Sections.Length, String.Format("Le navire {0} n'a pas le bon nombre de section", navire.Nom));
                navireIndex++;
            }
        }        private class joueurTestAvecUneFlotteDeNavires : UnJoueurAvecUneFlotteDeNavires
        {
            public joueurTestAvecUneFlotteDeNavires(string pseudo) : base(pseudo)
            {
            }
            public override CoordonnéesDeBatailleNavale
           Attaquant_ChoisirLesCoordonnéesDeTir()
            {
                throw new NotImplementedException();
            }
            public void Attaquant_GérerLeRésultatDuTir(CoordonnéesDeBatailleNavale coordonnéesDuTir, RésultatDeTir résultatDuTir)
            {
                throw new NotImplementedException();
            }

            public override void Attaquant_GérerLeRésultatDuTir(CoordonnéesDeBatailleNavale cbn, CoordonnéesDeBatailleNavale.RésultatDeTir rt)
            {
                throw new NotImplementedException();
            }

            

            public override CoordonnéesDeBatailleNavale.RésultatDeTir Défenseur_FournirLeRésultatDuTir(CoordonnéesDeBatailleNavale cbn)
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void Phase_2_4_UnJoueurAvecUneFlotteDeNavires()
        {
            Type t = typeof(UnJoueurAvecUneFlotteDeNavires);
            ConstructorInfo constructeurPubliqueAvecUnParametreString =
           t.GetConstructor(new Type[] { typeof(string) });

            Assert.IsNotNull(constructeurPubliqueAvecUnParametreString,
           "UnJoueurAvecUneFlotteDeNavires doit posséder un constructeur publique avec un paramètre de type string.");
           
            bool pseudoNullOrEmpty = false;
            try
            {
                pseudoNullOrEmpty = false;
                joueurTestAvecUneFlotteDeNavires joueur = new
               joueurTestAvecUneFlotteDeNavires(null);
            }
            catch (ArgumentNullException)
            {
                pseudoNullOrEmpty = true;
            }
            Assert.IsTrue(pseudoNullOrEmpty, "Le constructeur de UnJoueurAvecUneFlotteDeNavires ne doit pas accepter un paramètre null");
        try
            {
                pseudoNullOrEmpty = false;
                joueurTestAvecUneFlotteDeNavires joueur = new
               joueurTestAvecUneFlotteDeNavires("");
            }
            catch (ArgumentNullException)
            {
                pseudoNullOrEmpty = true;
            }
            Assert.IsTrue(pseudoNullOrEmpty, "Le constructeur de UnJoueurAvecUneFlotteDeNavires ne doit pas accepter un paramètre de chaîne vide");
        try
            {
                pseudoNullOrEmpty = false;
                joueurTestAvecUneFlotteDeNavires joueur = new
               joueurTestAvecUneFlotteDeNavires("Pseudo TEST");
            }
            catch (Exception)
            {
                pseudoNullOrEmpty = true;
            }
            Assert.IsFalse(pseudoNullOrEmpty, "Le constructeur de UnJoueurAvecUneFlotteDeNavires doit accepter une chaîne non vide");
        }


        [TestMethod]
        public void Phase_2_6_RéparerLaFlotteDeNavires()
        {
            UneFlotteDeNavires flotte = new UneFlotteDeNavires();
            foreach (UnNavire navire in flotte.Navires)
            {
                foreach (var section in navire.Sections)
                {
                    section.Etat = EtatDeSectionDeNavire.Touché;
                    
                }
            }
            flotte.RéparerTousLesNavires();
            bool aucuneSectionTouchée = true;
            foreach (UnNavire navire in flotte.Navires)
            {
                foreach (var section in navire.Sections)
                {
                    if (section.Etat == EtatDeSectionDeNavire.Touché)
                        aucuneSectionTouchée = false;
                }
            }
            Assert.IsTrue(aucuneSectionTouchée, "Après réparation, toutes les sections de tous les navires doivent être intactes!");
 }


        private void MettreTousLesNaviresAuPort(UneFlotteDeNavires flotte)
        {
            byte ligne = 1;
            foreach (UnNavire navire in flotte.Navires)
            {
                navire.Positionner(new CoordonnéesDeBatailleNavale('A', ligne++),
               OrientationNavire.Horizontal);
            }
        }
        [TestMethod]
        public void Phase_2_7_MettreTousLesNaviresAuPort()
        {
            UneFlotteDeNavires flotte = new UneFlotteDeNavires();
            MettreTousLesNaviresAuPort(flotte);
            Assert.IsTrue(flotte.Navires.Length == 5);
            byte ligne = 1;
            foreach (UnNavire navire in flotte.Navires)
            {
                Assert.IsNotNull(navire, "navire ne peut pas être null");
                Assert.IsTrue(navire.Orientation == OrientationNavire.Horizontal, "le navire n'est pas horizontal");
            for (int sectionNum = 0; sectionNum < navire.Sections.Length; sectionNum++)
                {
                    Assert.IsTrue(navire.Sections[sectionNum].Position.Colonne == 'A'
                   + sectionNum, "La colonne de cette section n'est pas correcte");
                    Assert.IsTrue(navire.Sections[sectionNum].Position.Ligne == ligne,
                   "La ligne de cette section n'est pas correcte");
                }
                ligne++;
            }
        }




        [TestMethod]
        public void Phase_2_8_VérifierLeRésultatDuTir()
        {
            UneFlotteDeNavires flotte = new UneFlotteDeNavires();
            MettreTousLesNaviresAuPort(flotte);
            byte ligne = 1;
            foreach (UnNavire navire in flotte.Navires)
            {
                int sectionNum = 0;
                for (; sectionNum < navire.Sections.Length - 1; sectionNum++)
                {
                    RésultatDeTir resultatTirTouché = navire.VérifierLeRésultatDuTir(new CoordonnéesDeBatailleNavale((char)('A' + sectionNum), ligne));
                    Assert.AreEqual(resultatTirTouché, RésultatDeTir.Touché, "Le navire situé ici devrait être touché");
                }
                RésultatDeTir resultatTirTouchéCoulé =
               navire.VérifierLeRésultatDuTir(new CoordonnéesDeBatailleNavale((char)('A' +
               sectionNum), ligne));
                Assert.AreEqual(resultatTirTouchéCoulé, RésultatDeTir.TouchéCoulé, "Le navire situé ici devrait être touché et coulé!");
               
                ligne++;
            }
            flotte.RéparerTousLesNavires();
            ligne = 1;
            foreach (UnNavire navire in flotte.Navires)
            {
                int sectionNum = 0;
                for (; sectionNum < navire.Sections.Length - 1; sectionNum++)
                {
                    RésultatDeTir resultatTirTouché = flotte.VérifierLeRésultatDuTir(new CoordonnéesDeBatailleNavale((char)('A' + sectionNum), ligne));
                    Assert.AreEqual(resultatTirTouché, RésultatDeTir.Touché, "Le navire situé ici devrait être touché");
                }
                if (ligne < 5) // dernière section , mais pas dernier navire
                {
                    RésultatDeTir resultatTirTouchéCoulé =
                   flotte.VérifierLeRésultatDuTir(new CoordonnéesDeBatailleNavale((char)('A' + sectionNum), ligne));
                    Assert.AreEqual(resultatTirTouchéCoulé, RésultatDeTir.TouchéCoulé, "Le navire situé ici devrait être touché et coulé !");
                }
                else // dernière section , du dernier navire
                {
                    RésultatDeTir resultatTirTouchéCouléFinal =
                   flotte.VérifierLeRésultatDuTir(new CoordonnéesDeBatailleNavale((char)('A' +
                   sectionNum), ligne));
                    Assert.AreEqual(resultatTirTouchéCouléFinal,
                   RésultatDeTir.TouchéCouléFinal, "Le navire situé ici devrait être le dernier navire touché et coulé!");
                }
                ligne++;
            }
        }




    }
}

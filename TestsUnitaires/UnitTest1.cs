using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace projet_scientifique
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Convertir_Endian_To_Int()
        {
            byte[] data = new byte[] { 213, 4 };
            uint result = Program.Myimage.Convertir_Endian_To_Int(data);
            uint compare = 1237;
            Assert.AreEqual(compare, result);
        }
        [TestMethod]
        public void Test_Convertir_Int_To_Endian()
        {

            uint data = 66773;
            byte[] result = Program.Myimage.Convertir_Int_To_Endian(data);
            string results = "";

            byte[] compare = new byte[] { 213, 4, 1 };
            string compares = "";
            for (int i = 0; i < compare.Length; i++)
            {


                compares += compare[i] + " ";

            }
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == 0 && results == compares)
                { }
                else
                {
                    results += result[i] + " ";
                }
            }
            Assert.AreEqual(compares, results);
        }
        [TestMethod]
        public void Test_subtab()
        {
            byte[] tab = new byte[] { 1, 2, 3, 4, 5 }; int debut = 1; int fin = 3;
            byte[] result = Program.Myimage.subtab(tab, debut, fin);
            string results = "";
            for (int i = 0; i < result.Length; i++)
            {
                results += result[i] + " ";
            }
            string compare = "2 3 4 ";
            Assert.AreEqual(compare, results);
        }
    }
}

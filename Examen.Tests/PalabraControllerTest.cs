using Examen.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examen.Tests
{
    [TestClass]
    public class PalabraControllerTest
    {
        [TestMethod]
        public void GetPalabraTest()
        {
            PalabrasController controller = new PalabrasController();
            var result = controller.GetPalabra("ABC");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Componente_UnaSolaLetra_Test()
        {
            BuscadorPalabra componente = new BuscadorPalabra();

            int[,] result = componente.getPosiciones("J");

            int[,] expect = new int[2, 2];
            expect[0, 0] = 2;
            expect[0, 1] = 2;

            Assert.AreEqual(expect[0,0], result[0,0]);
            Assert.AreEqual(expect[0,1], result[0,1]);
        }

        [TestMethod]
        public void Componente_NoEncontro_Test()
        {
            BuscadorPalabra componente = new BuscadorPalabra();

            int[,] result = componente.getPosiciones("ZJ");

            int[,] expect = new int[2, 2];
            expect[0, 0] = 0;
            expect[0, 1] = 0;
            expect[1, 0] = 0;
            expect[1, 1] = 0;


            Assert.AreEqual(expect[0, 0], result[0, 0]);
            Assert.AreEqual(expect[0, 1], result[0, 1]);
            Assert.AreEqual(expect[1, 0], result[1, 0]);
            Assert.AreEqual(expect[1, 1], result[1, 1]);
        }

        [TestMethod]
        public void Componente_Encontro_Test()
        {
            BuscadorPalabra componente = new BuscadorPalabra();

            int[,] result = componente.getPosiciones("TELEFE");

            int[,] expect = new int[6, 2];
            expect[0, 0] = 7;
            expect[0, 1] = 1;
            expect[1, 0] = 7;
            expect[1, 1] = 2;
            expect[2, 0] = 7;
            expect[2, 1] = 3;
            expect[3, 0] = 7;
            expect[3, 1] = 4;
            expect[4, 0] = 7;
            expect[4, 1] = 5;
            expect[5, 0] = 7;
            expect[5, 1] = 6;

            Assert.AreEqual(expect[0, 0], result[0, 0]);
            Assert.AreEqual(expect[0, 1], result[0, 1]);
            Assert.AreEqual(expect[1, 0], result[1, 0]);
            Assert.AreEqual(expect[1, 1], result[1, 1]);
            Assert.AreEqual(expect[2, 0], result[2, 0]);
            Assert.AreEqual(expect[2, 1], result[2, 1]);
            Assert.AreEqual(expect[3, 0], result[3, 0]);
            Assert.AreEqual(expect[3, 1], result[3, 1]);
            Assert.AreEqual(expect[4, 0], result[4, 0]);
            Assert.AreEqual(expect[4, 1], result[4, 1]);
            Assert.AreEqual(expect[5, 0], result[5, 0]);
            Assert.AreEqual(expect[5, 1], result[5, 1]);
        }
    }
}

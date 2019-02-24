using System;
using System.Collections.Generic;
using CodingChallenge.Data.Classes;
using CodingChallenge.Data.Classes.Formas;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class DataTests
    {
        #region Translator class
        [TestCase]
        public void TestTranslatorIngles()
        {
            Translator translatorForTest = Translator.GetInstance();

            var result = translatorForTest.ObtenerTraduccion("Cuadrado", "Ingles");

            Assert.AreEqual("Square", result);

        }
        [TestCase]
        public void TestTranslatorCastellano()
        {
            Translator translatorForTest = Translator.GetInstance();

            var result = translatorForTest.ObtenerTraduccion(Forma.Trapecio.ToString(), "Castellano");

            Assert.AreEqual("Trapecio", result);

        }
        [TestCase]
        public void TestTranslatorItalianoPlural()
        {
            Translator translatorForTest = Translator.GetInstance();

            var result = translatorForTest.ObtenerTraduccion(Forma.Circulo.ToString(), "Italiano",false);

            Assert.AreEqual("Cerchi", result);

        }
        [TestCase]
        public void TestTranslatorItalianoFooter()
        {
            Translator translatorForTest = Translator.GetInstance();

            string formatoFooter = translatorForTest.ObtenerFooter("Italiano");
            string result = string.Format(formatoFooter, 1, 2, 3);

            Assert.AreEqual("TOTALE:<br />1 figura Perimetro 2 Area 3", result);

        }
        [TestCase]
        public void TestTranslatorInglesLinea()
        {
            Translator translatorForTest = Translator.GetInstance();

            string formatoLine = translatorForTest.ObtenerFormatoLinea("Ingles");
            string result = string.Format(formatoLine,0, 1, 2, 3);

            Assert.AreEqual("0 1 | Area 2 | Perimeter 3 <br />", result);

        }        
        #endregion

        #region Tests Reporte y formas
        [TestCase]
        public void TestImprimirReporteCastellano()
        {
            var formas = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new Triangulo(4),
                new Cuadrado(2),
                new Triangulo(9),
                new Circulo(2.75m),
                new Triangulo( 4.2m)
            };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Castellano");

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br />2 Círculos | Area 52,03 | Perimetro 18,06 <br />3 Triángulos | Area 49,64 | Perimetro 51,6 <br />TOTAL:<br />7 formas Perimetro 97,66 Area 130,67",
                resumen);
        }
        [TestCase]
        public void TestImprimirReporteUnoDeCadafiguraIngles()
        {
            var formas = new List<IFormaGeometrica>
            {
                new Cuadrado(1),
                new Circulo(1),
                new Triangulo(1),
                new Rectangulo(2,1),
                new Trapecio(3,2,1,1)
            };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Ingles");
            
            Assert.AreEqual(
                "<h1>Shapes report</h1>1 Square | Area 1 | Perimeter 4 <br />1 Circle | Area 3,14 | Perimeter 3,14 <br />1 Triangle | Area 0,43 | Perimeter 3 <br />1 Rectangle | Area 2 | Perimeter 6 <br />1 Trapezoid | Area 2,17 | Perimeter 7 <br />TOTAL:<br />5 shapes Perimeter 23,14 Area 8,74",
                resumen);
        }
        [TestCase]
        public void TestImprimirReporteSinFigurasItaliano()
        {
            var formas = new List<IFormaGeometrica>();

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Italiano");

            Assert.AreEqual(
                "<h1>Elenco di forme vuote!</h1>",
                resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConUnCuadradoCastellano()
        {
            var cuadrados = new List<IFormaGeometrica> { new Cuadrado(5) };

            var resumen = FormaGeometrica.ImprimirReporte(cuadrados, "Castellano");

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br />TOTAL:<br />1 formas Perimetro 20 Area 25", resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConUnTrianguloItaliano()
        {
            var formas = new List<IFormaGeometrica> { new Triangulo(2) };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Italiano");

            Assert.AreEqual("<h1>Rapporto di figura</h1>1 Triangolo | Area 1,73 | Perimetro 6 <br />TOTALE:<br />1 figura Perimetro 6 Area 1,73", resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConUnCirculoIngles()
        {
            var formas = new List<IFormaGeometrica> { new Circulo(1) };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Ingles");

            Assert.AreEqual("<h1>Shapes report</h1>1 Circle | Area 3,14 | Perimeter 3,14 <br />TOTAL:<br />1 shapes Perimeter 3,14 Area 3,14", resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConUnTrapecioIngles()
        {
            var formas = new List<IFormaGeometrica> { new Trapecio(3, 2, 1, 1) };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Ingles");

            Assert.AreEqual("<h1>Shapes report</h1>1 Trapezoid | Area 2,17 | Perimeter 7 <br />TOTAL:<br />1 shapes Perimeter 7 Area 2,17", resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConUnRectanguloItaliano()
        {
            var formas = new List<IFormaGeometrica> { new Rectangulo(2,1) };

            var resumen = FormaGeometrica.ImprimirReporte(formas, "Italiano");

            Assert.AreEqual("<h1>Rapporto di figura</h1>1 Rettangolo | Area 2 | Perimetro 6 <br />TOTALE:<br />1 figura Perimetro 6 Area 2", resumen);
        }
        [TestCase]
        public void TestImprimirReporteListaConMasCuadradosIngles()
        {
            var cuadrados = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(1),
                new Cuadrado(3)
            };

            var resumen = FormaGeometrica.ImprimirReporte(cuadrados, "Ingles");

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br />TOTAL:<br />3 shapes Perimeter 36 Area 35", resumen);
        }

        #endregion
    }
}

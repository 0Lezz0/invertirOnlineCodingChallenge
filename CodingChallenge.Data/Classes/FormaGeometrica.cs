/*
 * Refactorización de la clase original FormaGeometrica
 * Se dicidio implementar una interface (IFormaGeometrica) que modela el comportamiento de todas las figuras
 * Para la traducción de los reportes, se dicidio utilizar una clase (Translator) que se encarga de la traducción 
 * completa del reporte. La misma utiliza un XML para almacenar los datos de la traducción
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Utilizamos esta clase para manejar los reportes de las figuras.
    /// </summary>
    public class FormaGeometrica
    {
        /// <summary>
        /// Esto resuelve el reporte completo, de forma completamente dinámica.  
        /// NOTA: Esto es útil para casos donde el nivel de mantenimiento debe ser MÁXIMO como se puede notar, no es de lo más eficiente. 
        /// Pero, solo requiere que se actualice el ENUM (el cual debe ser actualizado para inicializar la nueva figura) y el XML 
        /// este método obtiene un reporte por FORMA
        /// </summary>
        /// <param name="formas"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        /// <comentariosDeResolucion>
        /// Esta es la primer versión que había planteado para el reporte. La misma es, tambien, completamente desatendida de figuras e idiomas, pero tal vez menos performante.
        /// Como vemos, utiliza el enumerador para iterar sobre cada tipo de figura existente y luego, mediante LINQ busca cada tipo en la lista.
        /// Me parecio buena idea no borrar el método ya que el uso de LINQ es basante interesante.
        /// </comentariosDeResolucion>
        public static string ImprimirReportePorForma(List<IFormaGeometrica> formas, string idioma)
        {
            var sb = new StringBuilder();

            Translator traductor = Translator.GetInstance();
            if (!formas.Any())
            {
                //Si no hay nada
                sb.Append(traductor.ObtenerTraduccion("Header_vacio", idioma));
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                sb.Append(traductor.ObtenerTraduccion("Header", idioma));

                int totalFiguras = formas.Count;
                decimal perimetroTotal = 0;
                decimal areaTotal = 0;

                string formatoLinea = traductor.ObtenerFormatoLinea(idioma);

                foreach (int tipoForma in Enum.GetValues(typeof(Forma)))
                {

                    IEnumerable<IFormaGeometrica> listActual = null;
                    int cantidad = 0;
                    listActual = formas.Where(f => f.Id == (Forma)tipoForma); ///Obtengo todas las formas de ESE tipo
                    cantidad = listActual!= null? listActual.Count() : 0;

                    if (cantidad > 0)
                    {
                        string nombreForma = traductor.ObtenerTraduccion(Enum.GetName(typeof(Forma), tipoForma), idioma, cantidad == 1);
                        decimal perimetro = 0;
                        decimal area = 0;
                        foreach (IFormaGeometrica formaParaCalcular in listActual)
                        {
                            perimetro += formaParaCalcular.CalcularPerimetro();
                            area += formaParaCalcular.CalcularArea();
                        }
                        ///esto es un ejemplo interesante utilizando LINQ
                        ///cabe acalarar que se debe ejecutar la query por cada operación
                        ///utilizando un foreach por cada forma en la sublista, ahorramos algo de "tiempo"
                        ///perimetro = listActual.Sum(p => p.CalcularPerimetro());
                        ///area = listActual.Sum(f => f.CalcularArea());
                        ///cantidad = formas.Count(f => (int)f.Id == tipoForma);

                        areaTotal += area;
                        perimetroTotal += perimetro;

                        sb.AppendFormat(formatoLinea, cantidad, nombreForma, perimetro.ToString("0.##"), area.ToString("0.##"));
                    }
                }

                // FOOTER
                sb.AppendFormat(traductor.ObtenerFooter(idioma), totalFiguras, perimetroTotal.ToString("0.##"), areaTotal.ToString("0.##"));

            }

            return sb.ToString();
        }

        /// <summary>
        /// Este método resuelve el reporte Completo de forma dinámica, sin importas que tipo de figuras hay en el listado general
        /// </summary>
        /// <param name="formas"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        /// <comentariosDeResolucion>
        /// Despues de pensar un rato, se me ocurrio una forma para que el reporte quede completamente desatendido del manejo de figuras e idiomas.
        /// Esto nos permite agregar figuras y/o nuevos idiomas sin la necesidad de tocar este método!
        /// </comentariosDeResolucion>
        public static string ImprimirReporte(List<IFormaGeometrica> formas, string idioma)
        {
            var sb = new StringBuilder();

            Translator traductor = Translator.GetInstance();
            if (!formas.Any())
            {
                //Si no hay nada
                sb.Append(traductor.ObtenerTraduccion("Header_vacio", idioma));
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                sb.Append(traductor.ObtenerTraduccion("Header", idioma));

                int totalFiguras = formas.Count;
                decimal perimetroTotal = 0;
                decimal areaTotal = 0;

                string formatoLinea = traductor.ObtenerFormatoLinea(idioma); //Obtenemos el forato de la linea del traductor

                Dictionary<Forma, Contador> contadorGeneral = new Dictionary<Forma, Contador>(); //Utilizamos el dictionary para guardar los valores correspondientes a cada figura

                //Evaluamos cada figura de la lista generando el reporte
                foreach (IFormaGeometrica formaActual in formas)
                {
                    if (contadorGeneral.ContainsKey(formaActual.Id))
                    {
                        contadorGeneral[formaActual.Id].Cantidad++;
                        contadorGeneral[formaActual.Id].Perimetro += formaActual.CalcularPerimetro();
                        contadorGeneral[formaActual.Id].Area += formaActual.CalcularArea();
                    }
                    else
                    {
                        //Solo pasamos por aca una vez por cada forma nueva; entonces, inicializamos el contador con los datos de la primer figura del tipo
                        contadorGeneral.Add(formaActual.Id, new Contador { Cantidad = 1, Area = formaActual.CalcularArea(), Perimetro = formaActual.CalcularPerimetro() });
                    }
                }
                
                //buscamos los totales obtenidos en el reporte
                foreach (Forma tipoForma in contadorGeneral.Keys)
                {
                    //Obtenemos la traducción de la figura
                    string nombreForma = traductor.ObtenerTraduccion(tipoForma.ToString(), idioma, contadorGeneral[tipoForma].Cantidad == 1);
                    
                    //sumamos a los TOTALES GENERALES
                    areaTotal += contadorGeneral[tipoForma].Area;
                    perimetroTotal += contadorGeneral[tipoForma].Perimetro;

                    //agregamos la linea para el reporte
                    sb.AppendFormat(formatoLinea, contadorGeneral[tipoForma].Cantidad, nombreForma, contadorGeneral[tipoForma].Area.ToString("0.##"), contadorGeneral[tipoForma].Perimetro.ToString("0.##"));
                }

                // FOOTER
                sb.AppendFormat(traductor.ObtenerFooter(idioma), totalFiguras, perimetroTotal.ToString("0.##"), areaTotal.ToString("0.##"));

            }

            return sb.ToString();
        }

    }
}

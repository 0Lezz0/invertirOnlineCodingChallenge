using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Esta clase se encarga de manejar las traducciones.
    /// Requiere de un XML con los idiomas ubicado en la carpeta de configFiles
    /// </summary>
    /// <formatoDelArchivo>
    /// Formato básico del XML:
    /// -----------------------------------------
    ///    <Languages>
    ///      <Language>
    ///        <keyWord>translated keyWord</keyWord>
    ///        <keyWord_plural>translated keyWord... but im plural</keyWord_plural>
    ///      </Language>
    ///    </Languages>
    /// -----------------------------------------
    /// Donde:
    ///     Language = el idioma deseado (Ingles, Castellano, Italiano, Mandarin, Klingon, etc)
    ///     keyWord = la palabra clave a buscar, por conveción, la misma debe ser en español (el nombre de la forma, la palabra del reporte a traducir, etc)
    ///     keyWord_plural = opcional, nos brinda la versión en plural de la palabra
    /// -----------------------------------------     
    /// En caso de no encontrar alguna traducción, se retorna la palabra clave tal como llego
    /// </formatoDelArchivo>
    /// <comentariosDeResolucion>
    /// En un principio había encarado la traducción con algo parecido a lo hice con las figuras. Usar una interface, un enumerador y varias clases resolver cada idioma.
    /// Teniendo en cuenta principalmente el mantenimiento, decidí que agregar un nuevo idioma debía ser lo más rádipo y desatendido posible.
    /// Con esta clase, apuntamos a que un nuevo idioma requiere modificar SOLO el XML (obviamente, respetando el formato) sin necesidad de recompilar la solución!
    /// Esto es muy util para, digamos, una página web.
    /// A su vez, si se agrega una nueva figura, para lo que es las traducciones, solo sería necesario actualizar las lineas en el XML correspondientes a esta nueva figura.
    /// Finalmente, la clase se construyo de tal forma que permita generar el reporte sin problemas, de no encontrar alguna de las keys en el XML, se retorna una versión por defecto. 
    /// Ya sea la palabra tal como vino o una versión en español (para el footer y el formato de la linea)
    /// </comentariosDeResolucion>
    public class Translator
    {
        private const string CONFIG_PATH = @"ConfigFiles\Languages.xml"; //Ver que esto no parta al sacarlo del config file de

        private readonly XElement configFile;

        private static readonly Translator TRANSLATOR_INSTANCE = new Translator();

        private Translator()
        {
            string fullConfigPath = string.Empty;
            try
            {
                fullConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), CONFIG_PATH);
                this.configFile = XElement.Load($"{fullConfigPath}");

            }
            catch (Exception)
            {
                System.Console.WriteLine("File not found at " + fullConfigPath);
                throw;
            }
        }

        public static Translator GetInstance()
        {
            return TRANSLATOR_INSTANCE;
        }
    
        /// <summary>
        /// Obtiene la traducción de una palabra desde nuestro archivo de configuración
        /// Distingue entre singular y plural (se asume por defecto que se busca el singular)
        /// De no existir la versión en plural, se trae la versión en singular
        /// En caso de NO obtener la palabra buscada, se retorna la palabra original, en lugar de cortar la ejecución del programa.
        /// Se considera mejor un ligero error de reporte que la incapacidad de dar un reporte.
        /// </summary>
        /// <param name="palabraClave">palabra clave a traducir</param>
        /// <param name="idioma"> idioma de destino</param>
        /// <param name="esSingular">determina si la palbra buscado es o no singular. por defecto, se considera siempre singular</param>
        /// <returns></returns>
        public string ObtenerTraduccion(string palabraClave, string idioma, bool esSingular = true)
        {
            string traduccion = string.Empty;

            //Obtenemos todas las palbras del lenguaje
            try
            {
                //Obtenemos el diccionario para el idioma espécifico
                XElement palabras = this.configFile.Element(idioma);
                
                //obtenemos la palabra a buscar, determinando si la misma es o no plural
                if (esSingular)
                {
                    traduccion = string.Concat(palabras.Element(palabraClave).Nodes());
                }
                else
                {
                    //buscamos en plural, de NO EXISTIR, intentamos obtener la versión en singular
                    XElement nodoPlural = palabras.Element(palabraClave + "_plural");
                    if(nodoPlural != null)
                        traduccion = nodoPlural.Value;
                    else
                        traduccion = palabras.Element(palabraClave).Value;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No se encontró traducción de {0} para el idioma {1}. Se retorna la palabra original.", palabraClave, idioma);

                return palabraClave;
            }

            return string.IsNullOrWhiteSpace(traduccion)? palabraClave :  traduccion;
        }

        /// <summary>
        /// Obtiene la linea utilizada en el reporte. 
        /// En caso de no encontrarla, retorna una linea por defecto, en español 
        /// </summary>
        /// <param name="idiomaDeLaLinea"></param>
        /// <returns></returns>
        public string ObtenerFormatoLinea(string idiomaDeLaLinea)
        {
            string linea = string.Empty;
            try
            {
                linea = ObtenerTraduccion("Line", idiomaDeLaLinea);
            }
            catch (Exception)
            {
                Console.WriteLine("No se encontro el formato de linea para el lenguaje {0}. Se retorna una linea en español.", idiomaDeLaLinea);
                linea = @"{0} {1} | Area {2} | Perimetro {3} <br/>";
            }
            return linea;
        }

        /// <summary>
        /// Nos retorna el Footer del reporte
        /// </summary>
        /// <param name="idiomaDelFooter"></param>
        /// <returns></returns>
        public string ObtenerFooter(string idiomaDelFooter)
        {
            string footer = string.Empty;
            try
            {
                footer = ObtenerTraduccion("Footer", idiomaDelFooter);
            }
            catch (Exception)
            {
                Console.WriteLine("No se encontro el formato del footer para el lenguaje {0}. Se retorna un footer en español.", idiomaDelFooter);
                footer = @"TOTAL:<br/>{0} formas Perimetro {1} Area {2}";
            }
            return footer;
        }
    }

    

}
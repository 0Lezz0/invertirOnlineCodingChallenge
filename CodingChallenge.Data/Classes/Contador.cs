namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Utilizamos esta clase para facilitar la creación de reportes
    /// </summary>
    /// <comentariosDeResolucion>
    /// Esta pequeña clase nos ayuda (en conjunto con un Dictionary) a tener un reporte dinámico sin importar cuantas figuras existen en nuestra colleción de figuras.
    /// En un principio, había armado una solución más rebuscada, utilizando el enumerador de figuras para iterar por cada figura definida ahí y utilizando LINQ para
    /// sacar sub grupos dentro de la lista general y de ahí sacar los totales correspondientes.
    /// En un brote de inspiración, se me ocurrio que (utilizando un dictionary, cuya Key es el ID de una figura) podía agrupar de generar automática los totales 
    /// con una sola iteración de la lista general de figuras.
    /// Luego el reporte recorrería simplemente los valores del Dictionary, obteniendo el Value (Contador) para escribir cada linea.
    /// </comentariosDeResolucion>
    public class Contador
    {
        public int Cantidad { get; set; }
        public decimal Area { get; set; }
        public decimal Perimetro { get; set; }
    }
}

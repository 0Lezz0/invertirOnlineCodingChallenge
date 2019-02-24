namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Representa todas las formas disponibles en nuetra aplicación
    /// </summary>
    /// <comentariosDeResolucion>
    /// Me decidí por un enumerador para mantener cierto control con las figuras.
    /// Dado que para cada figura es necesario agregar una nueva clase, no nos cuesta mucho agregarla al enumerador y facilitarnos un poco la vida 
    /// Utilizamos el enum como Key en algunos procesos (reporte), como valor por defecto en otros (Traductor). Me parecio útil al costo de un poco de mantenimiento
    /// </comentariosDeResolucion>
    public enum Forma
    {
        Cuadrado = 1,
        Triangulo_Equilatero = 2,
        Circulo = 3,
        Trapecio = 4,
        Rectangulo = 5
    }
}

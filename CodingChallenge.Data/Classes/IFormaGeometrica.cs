namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Esta interface nos definira el comportamiento básico de una figura geometrica
    /// Cabe destacar que las propiedades (como por ejemplo, los lados) son únicos de cada figura y se definen de forma particular en la clase especifica de cada una
    /// </summary>
    /// <comentariosDeResolucion>
    /// No hay mucho que explicar, la mejor forma de implementar un comportamiento general con clases particulares es utilizando una interface.
    /// </comentariosDeResolucion>
    public interface IFormaGeometrica
    {
        Forma Id { get; }
        /// <summary>
        /// Calcula el prímetro de la figura
        /// </summary>
        /// <returns></returns>
        decimal CalcularArea();
        /// <summary>
        /// Calcula el Área de la figura
        /// </summary>
        /// <returns></returns>
        decimal CalcularPerimetro();
    }

}

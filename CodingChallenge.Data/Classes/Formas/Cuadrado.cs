using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes.Formas
{
    /// <summary>
    /// Representa un cuadrado
    /// </summary>
    public class Cuadrado : IFormaGeometrica
    {
        
        private decimal lado;
        //public Int Id { get { return 1; } }
        public Forma Id { get { return Forma.Cuadrado; } }

        #region constructores
        public Cuadrado(decimal lado)
        {
            this.lado = lado;
        }
        public Cuadrado()
        {
            this.lado = 1M;
        }
        #endregion

        public decimal CalcularArea()
        {
            return this.lado*this.lado;
        }

        public decimal CalcularPerimetro()
        {
            return this.lado*4;
        }
    }
}

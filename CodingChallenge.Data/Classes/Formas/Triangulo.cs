using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes.Formas
{
    /// <summary>
    /// Representa un triangulo equilatero (es decir, con todos sus lados iguales
    /// </summary>
    public class Triangulo : IFormaGeometrica
    {
        
        private decimal lado;
        //public Int Id { get { return 1; } }
        public Forma Id { get { return Forma.Triangulo_Equilatero; } }

        #region constructores
        public Triangulo(decimal lado)
        {
            this.lado = lado;
        }
        public Triangulo()
        {
            this.lado = 1M;
        }
        #endregion

        public decimal CalcularArea()
        {
            return ((decimal)Math.Sqrt(3) / 4) * this.lado * this.lado;
        }

        public decimal CalcularPerimetro()
        {
            return this.lado*3;
        }
    }
}

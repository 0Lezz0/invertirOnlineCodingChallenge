using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes.Formas
{
    /// <summary>
    /// Representa un Rectangulo
    /// </summary>
    public class Rectangulo : IFormaGeometrica
    {
        
        private decimal ladoMayor;
        private decimal ladoMenor;
        public Forma Id { get { return Forma.Rectangulo; } }

        #region constructores
        public Rectangulo(decimal ladoMayor, decimal ladoMenor)
        {
            this.ladoMayor = ladoMayor;
            this.ladoMenor = ladoMenor;
        }
        public Rectangulo()
        {
            this.ladoMenor = 1M;
            this.ladoMenor = 2M;
        }
        #endregion

        public decimal CalcularArea()
        {
            return this.ladoMenor * ladoMayor;
        }

        public decimal CalcularPerimetro()
        {
            return this.ladoMayor*2 + this.ladoMenor*2;
        }
    }
}

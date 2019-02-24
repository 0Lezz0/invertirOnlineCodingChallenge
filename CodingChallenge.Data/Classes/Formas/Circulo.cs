using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes.Formas
{
    /// <summary>
    /// Representa un Circulo
    /// </summary>
    public class Circulo : IFormaGeometrica
    {

        private decimal radio;
        //public Int Id { get { return 1; } }
        public Forma Id { get { return Forma.Circulo; } }

        #region constructores
        public Circulo(decimal radio)
        {
            this.radio = radio;
        }
        public Circulo()
        {
            this.radio = 1M;
        }
        #endregion

        public decimal CalcularArea()
        {
            return this.radio * this.radio * (decimal)Math.PI;
        }

        public decimal CalcularPerimetro()
        {
            return this.radio * (decimal)Math.PI;
        }
    }
}

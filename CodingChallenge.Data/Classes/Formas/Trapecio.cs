using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes.Formas
{
    /// <summary>
    /// Representa un Trapecio
    /// </summary>
    public class Trapecio : IFormaGeometrica
    {
        
        private decimal baseMayor;
        private decimal baseMenor;
        private decimal ladoA;
        private decimal ladoB;

        public Forma Id { get { return Forma.Trapecio; } }

        #region constructores
        public Trapecio(decimal baseMayor, decimal baseMenor, decimal ladoA, decimal ladoB)
        {
            if (baseMayor == baseMenor && ladoA != ladoB)
                throw new ArgumentException("Si las bases son iguales, los lados, DEBEN ser iguales");

            if (baseMayor > baseMenor)
            {
                this.baseMayor = baseMayor;
                this.baseMenor = baseMenor;
            }
            else
            {
                this.baseMayor = baseMenor;
                this.baseMenor = baseMayor;
            }

            this.ladoA = ladoA;
            this.ladoB = ladoB;
        }
        public Trapecio()
        {
            this.baseMenor = 1M;
            this.baseMayor = 2M;
            this.ladoA = 3M;
            this.ladoB = 3M;
        }
        #endregion

        public decimal CalcularArea()
        {
            double cuadradoDeA = (double)(ladoA * ladoA);
            double cuadradoDeB = (double)(ladoB * ladoB);
            double restaDeBases = (double)(baseMayor - baseMenor);
            double bardo = (cuadradoDeA - cuadradoDeB + restaDeBases * restaDeBases) / (2 * restaDeBases);
            return (((baseMenor + baseMayor)/2) * (decimal)Math.Sqrt(cuadradoDeA - bardo*bardo)); 
        }

        public decimal CalcularPerimetro()
        {
            return this.baseMayor + this.baseMenor + this.ladoA + this.ladoB;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.BusinessObjects
{
    public class Enums
    {
        public enum CurrencyBehavior
        {
            Directa = 1,
            Indirecta = 2
        }

        public enum EnumPortfolioNoveltyType
        {
            IngresoPortafolio = 1,
            SalidaPortafolio = 2,
            ConstitucionGarantia = 3,
            LiberacionGarantia = 4,
            RendimientoGarantia = 5
        }

        public enum EnumPortfolioNoveltyStatus
        {
            Ingresado = 1,
            Anulado = 2
        }

        public enum BankingConcept
        {
            DepositoDeCliente = 1,
            CamaraDeCompensacion = 2,
            Traslado = 3,
            IngresoPortafolio = 4,
            SalidaPortafolio = 5,
            ConstitucionGarantia = 6,
            LiberacionGarantia = 7,
            RendimientoGarantia = 8,
            CreditoCliente = 9
        }
    }
}

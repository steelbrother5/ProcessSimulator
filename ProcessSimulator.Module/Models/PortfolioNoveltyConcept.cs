using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioNoveltyConcept : EnterpriseBaseObject
    {
        public PortfolioNoveltyConcept(Session session) : base(session) { }

        private bool applyRegisteredRate;
        /// <summary>
        /// Aplica Registro de Tasas
        /// </summary>
        public bool ApplyRegisteredRate
        {
            get => applyRegisteredRate;
            set => SetPropertyValue("ApplyRegisteredRate", ref applyRegisteredRate, value);
        }

        private string name;
        /// <summary>
        /// Nombre del Concepto de Novedad
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("UniqueConceptName", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
            CustomMessageTemplate = "Nombre del concepto ya existe.")]
        public string Name
        {
            get => name;
            set => SetPropertyValue("Name", ref name, value);
        }

        private bool affectsBalancesPortfolio;
        /// <summary>
        /// Afecta Saldos en Portafolio
        /// </summary>
        public bool AffectsBalancesPortfolio
        {
            get => affectsBalancesPortfolio;
            set => SetPropertyValue("AffectsBalancesPortfolio", ref affectsBalancesPortfolio, value);
        }
    }
}

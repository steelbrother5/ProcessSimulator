using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ProfitandLossesReportParameters : EnterpriseBaseObject
    {
        public ProfitandLossesReportParameters(Session session) : base(session) { }

        /// <summary>
        /// Asociacion propiedad Descripción.
        /// </summary>
        private string description;
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "La descripción es obligatoria")]
        [RuleUniqueValue("UniqueDescription",
                         DefaultContexts.Save,
                         CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
                         CustomMessageTemplate = "La descripción ya existe.")]
        public string Description
        {
            get => description;
            set => SetPropertyValue("Description", ref description, value);
        }

        /// <summary>
        /// Boleano que controla si se genera el reporte PyG o el Reporte PyG Global
        /// </summary>
        private bool generateGlobalReport;
        public bool GenerateGlobalReport
        {
            get => generateGlobalReport;
            set => SetPropertyValue("GenerateGlobalReport", ref generateGlobalReport, value);
        }

        /// <summary>
        /// Asociacion propiedad Fecha.
        /// </summary>
        private DateTime profitandLossesDate;
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "La fecha es obligatoria")]
        public DateTime ProfitandLossesDate
        {
            get => profitandLossesDate;
            set => SetPropertyValue("ProfitandLossesDate", ref profitandLossesDate, value);
        }

        /// <summary>
        /// Asociacion propiedad UEI.
        /// </summary>
        [Association("EIU_PnLRepParameters", typeof(ExternalInvestmentUnit), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<ExternalInvestmentUnit> ExternalInvestmentUnits => GetCollection<ExternalInvestmentUnit>("ExternalInvestmentUnits");

        /// <summary>
        /// Asociacion propiedad Trader.
        /// </summary>
        [Association("Traders_PnLRepParameters", typeof(Trader), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Trader> Traders => GetCollection<Trader>("Traders");

        /// <summary>
        /// Asociacion propiedad Portafolio.
        /// </summary>
        [Association("Portf_PnLRepParameters", typeof(Portfolio), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Portfolio> Portfolios => GetCollection<Portfolio>("Portfolios");
    }
}

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessSimulator.Module.BusinessObjects.Enums;

namespace ProcessSimulator.Module.Models
{
    public class Currency : EnterpriseBaseObject
    {
        private bool isLocal;
        private string code;
        private string description;
        private DateTime modificationDate;
        private bool isUSDollar;
        private CurrencyBehavior behavior;
        //Redmine 1203 : John J Garcia 29-12-2016
        private int numericCode;
        private string abbreviation;

        public Currency(Session session) : base(session) { }

        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
            CustomMessageTemplate = "Código de Moneda ya existe.")]
        public string Code
        {
            get { return code; }
            set { SetPropertyValue("Code", ref code, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }

        public bool IsLocal
        {
            get { return isLocal; }
            set { SetPropertyValue("IsLocal", ref isLocal, value); }
        }

        public bool IsUSDollar
        {
            get { return isUSDollar; }
            set { SetPropertyValue("IsUSDollar", ref isUSDollar, value); }
        }

        public CurrencyBehavior Behavior
        {
            get { return behavior; }
            set { SetPropertyValue("Behavior", ref behavior, value); }
        }

        //Redmine 1203 : John J Garcia 29-12-2016
        [RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
            CustomMessageTemplate = "El código numérico asignado ya existe")]
        public int NumericCode
        {
            get { return numericCode; }
            set { SetPropertyValue("NumericCode", ref numericCode, value); }
        }

        [Appearance("", Visibility = ViewItemVisibility.Hide)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { SetPropertyValue("ModificationDate", ref modificationDate, value); }
        }

        public string Abbreviation
        {
            get { return abbreviation; }
            set { SetPropertyValue("Abbreviation", ref abbreviation, value); }
        }

        [Association("Currency-ExchangeRate", typeof(ExchangeRate)), Aggregated]
        public XPCollection<ExchangeRate> CurrencyExchangeRates
        {
            get { return GetCollection<ExchangeRate>("CurrencyExchangeRates"); }
        }
    }
}

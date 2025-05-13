using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExchangeRate : EnterpriseBaseObject
    {

        private DateTime date;
        private Currency referenceCurrency;
        private Currency currency;
        private Decimal valueForDate;
        private DateTime modificationDate;

        public ExchangeRate(Session session) : base(session) { }

        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Date
        {
            get { return date; }
            set { SetPropertyValue("Date", ref date, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Currency ReferenceCurrency
        {
            get { return referenceCurrency; }
            set { SetPropertyValue("ReferenceCurrency", ref referenceCurrency, value); }
        }

        [Association("Currency-ExchangeRate", typeof(Currency))]
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        public Decimal ValueForDate
        {
            get { return valueForDate; }
            set { SetPropertyValue("ValueForDate", ref valueForDate, value); }
        }

        [Appearance("", Visibility = ViewItemVisibility.Hide)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { SetPropertyValue("ModificationDate", ref modificationDate, value); }
        }

    }
}

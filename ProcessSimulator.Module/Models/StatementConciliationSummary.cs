using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class StatementConciliationSummary : EnterpriseBaseObject
    {
        private int numberOfMovements;
        private int numberOfDebits;
        private int numberOfCredits;
        private decimal debitsValue;
        private decimal creditsValue;
        private int numberOfConciledMovements;
        private int numberOfConciledDebits;
        private int numberOfConciledCredits;
        private decimal conciledDebitsValue;
        private decimal conciliationDebitsValueDifference;
        private decimal conciledCreditsValue;
        private decimal conciliationCreditsValueDifference;
        private decimal totalConciliationValueDifference;
        private int numberOfUnconciledMovements;
        private bool isConciled;

        public StatementConciliationSummary(Session session) : base(session) { }

        public int NumberOfMovements
        {
            get { return numberOfMovements; }
            set { SetPropertyValue("NumberOfMovements", ref numberOfMovements, value); }
        }

        public int NumberOfDebits
        {
            get { return numberOfDebits; }
            set { SetPropertyValue("NumberOfDebits", ref numberOfDebits, value); }
        }

        public int NumberOfCredits
        {
            get { return numberOfCredits; }
            set { SetPropertyValue("NumberOfMovements", ref numberOfCredits, value); }
        }

        public decimal DebitsValue
        {
            get { return debitsValue; }
            set { SetPropertyValue("DebitsValue", ref debitsValue, value); }
        }

        public decimal CreditsValue
        {
            get { return creditsValue; }
            set { SetPropertyValue("CreditsValue", ref creditsValue, value); }
        }

        public int NumberOfConciledMovements
        {
            get { return numberOfConciledMovements; }
            set { SetPropertyValue("NumberOfConciledMovements", ref numberOfConciledMovements, value); }
        }

        public int NumberOfConciledDebits
        {
            get { return numberOfConciledDebits; }
            set { SetPropertyValue("NumberOfConciledDebits", ref numberOfConciledDebits, value); }
        }

        public int NumberOfConciledCredits
        {
            get { return numberOfConciledCredits; }
            set { SetPropertyValue("NumberOfConciledMovements", ref numberOfConciledCredits, value); }
        }

        public decimal ConciledDebitsValue
        {
            get { return conciledDebitsValue; }
            set { SetPropertyValue("ConciledDebitsValue", ref conciledDebitsValue, value); }
        }

        public decimal ConciliationDebitsValueDifference
        {
            get { return conciliationDebitsValueDifference; }
            set { SetPropertyValue("ConciliationDebitsValueDifference", ref conciliationDebitsValueDifference, value); }
        }

        public decimal ConciledCreditsValue
        {
            get { return conciledCreditsValue; }
            set { SetPropertyValue("ConciledCreditsValue", ref conciledCreditsValue, value); }
        }

        public decimal ConciliationCreditsValueDifference
        {
            get { return conciliationCreditsValueDifference; }
            set { SetPropertyValue("conciliationCreditsValueDifference", ref conciliationCreditsValueDifference, value); }
        }

        public decimal TotalConciliationValueDifference
        {
            get { return totalConciliationValueDifference; }
            set { SetPropertyValue("TotalConciliationValueDifference", ref totalConciliationValueDifference, value); }
        }

        public int NumberOfUnconciledMovements
        {
            get { return numberOfUnconciledMovements; }
            set { SetPropertyValue("NumberOfUnconciledMovements", ref numberOfUnconciledMovements, value); }
        }

        public bool IsConciled
        {
            get { return isConciled; }
            set { SetPropertyValue("IsConciled", ref isConciled, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}

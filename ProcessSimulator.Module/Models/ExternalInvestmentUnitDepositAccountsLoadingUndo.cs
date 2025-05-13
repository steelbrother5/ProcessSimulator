using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExternalInvestmentUnitDepositAccountsLoadingUndo : EnterpriseBaseObject
    {
        private DateTime closingDate;
        private ExternalInvestmentUnitDepositAccountsLoadingMaster externalInvestmentUnitDepositAccountsLoadingMaster;
        private bool isActive;

        public ExternalInvestmentUnitDepositAccountsLoadingUndo(Session session) : base(session) { }

        [Association("ExternalInvestmentUnitDepositAccountsLoadingMaster-ExternalInvestmentUnitDepositAccountsLoadingUndo",
                     typeof(ExternalInvestmentUnitDepositAccountsLoadingMaster))]
        public ExternalInvestmentUnitDepositAccountsLoadingMaster ExternalInvestmentUnitDepositAccountsLoadingMaster
        {
            get { return externalInvestmentUnitDepositAccountsLoadingMaster; }
            set { SetPropertyValue("ExternalInvestmentUnitDepositAccountsLoadingMaster", ref externalInvestmentUnitDepositAccountsLoadingMaster, value); }
        }

        public DateTime ClosingDate
        {
            get { return closingDate; }
            set { SetPropertyValue("ClosingDate", ref closingDate, value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code (check out http://documentation.devexpress.com/#Xaf/CustomDocument2834 for more details).
        }
    }
}

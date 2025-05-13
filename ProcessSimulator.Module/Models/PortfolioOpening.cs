using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioOpening : EnterpriseBaseObject
    {
        private DateTime openedClosingDate;
        private PortfolioClosingMaster portfolioClosingMaster;
        private bool isActive;

        public PortfolioOpening(Session session) : base(session) { }

        [Association("PortfolioClosingMaster-PortfolioOpening", typeof(PortfolioClosingMaster))]
        public PortfolioClosingMaster PortfolioClosingMaster
        {
            get { return portfolioClosingMaster; }
            set { SetPropertyValue("PortfolioClosingMaster", ref portfolioClosingMaster, value); }
        }

        public DateTime OpenedClosingDate
        {
            get { return openedClosingDate; }
            set { SetPropertyValue("OpenedClosingDate", ref openedClosingDate, value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

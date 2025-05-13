using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioClosing : EnterpriseBaseObject
    {
        private DateTime closingDate;
        private PortfolioClosingMaster portfolioClosingMaster;
        private bool isActive;

        public PortfolioClosing(Session session) : base(session) { }

        [Association("PortfolioClosingMaster-PortfolioClosing", typeof(Portfolio))]
        public PortfolioClosingMaster PortfolioClosingMaster
        {
            get { return portfolioClosingMaster; }
            set { SetPropertyValue("PortfolioClosingMaster", ref portfolioClosingMaster, value); }
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

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

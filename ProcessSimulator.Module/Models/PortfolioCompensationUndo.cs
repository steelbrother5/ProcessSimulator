using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioCompensationUndo : EnterpriseBaseObject
    {
        private DateTime undonedCompensationDate;
        private PortfolioClosingMaster portfolioClosingMaster;
        private bool isActive;

        public PortfolioCompensationUndo(Session session) : base(session) { }

        [Association("PortfolioClosingMaster-PortfolioCompensationUndo", typeof(PortfolioClosingMaster))]
        public PortfolioClosingMaster PortfolioClosingMaster
        {
            get { return portfolioClosingMaster; }
            set { SetPropertyValue("PortfolioClosingMaster", ref portfolioClosingMaster, value); }
        }

        public DateTime UndonedCompensationDate
        {
            get { return undonedCompensationDate; }
            set { SetPropertyValue("UndonedCompensationDate", ref undonedCompensationDate, value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

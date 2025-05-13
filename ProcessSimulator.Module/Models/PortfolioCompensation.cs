using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioCompensation : EnterpriseBaseObject
    {
        private DateTime compensationDate;
        private PortfolioClosingMaster portfolioClosingMaster;
        private bool isActive;

        public PortfolioCompensation(Session session) : base(session) { }

        [Association("PortfolioClosingMaster-PortfolioCompensation", typeof(PortfolioClosingMaster))]
        public PortfolioClosingMaster PortfolioClosingMaster
        {
            get { return portfolioClosingMaster; }
            set { SetPropertyValue("PortfolioClosingMaster", ref portfolioClosingMaster, value); }
        }

        public DateTime CompensationDate
        {
            get { return compensationDate; }
            set { SetPropertyValue("CompensationDate", ref compensationDate, value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExternalInvestmentUnitItem : EnterpriseBaseObject
    {
        private string iD;
        private ExternalInvestmentUnit externalInvestmentUnit;
        private PortfolioItemType portfolioItemType;
        private DateTime entryDate;
        private DateTime exitDate;
        private bool isInExternalInvestmentUnit;
        private Guid originOid;
        private String originType;

        public ExternalInvestmentUnitItem(Session session) : base(session) { }

        [Association("ExternalInvestmentUnit-ExternalInvestmentUnitItem", typeof(ExternalInvestmentUnit))]
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get { return externalInvestmentUnit; }
            set { SetPropertyValue("externalInvestmentUnit", ref externalInvestmentUnit, value); }
        }

        public PortfolioItemType PortfolioItemType
        {
            get { return portfolioItemType; }
            set { SetPropertyValue("PortfolioItemType", ref portfolioItemType, value); }
        }

        public string ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public DateTime EntryDate
        {
            get { return entryDate; }
            set { SetPropertyValue("EntryDate", ref entryDate, value); }
        }

        public DateTime ExitDate
        {
            get { return exitDate; }
            set { SetPropertyValue("ExitDate", ref exitDate, value); }
        }

        public bool IsInExternalInvestmentUnit
        {
            get { return isInExternalInvestmentUnit; }
            set { SetPropertyValue("IsInExternalInvestmentUnit", ref isInExternalInvestmentUnit, value); }
        }

        public Guid OriginOid
        {
            get { return originOid; }
            set { SetPropertyValue("OriginOid", ref originOid, value); }
        }

        public String OriginType
        {
            get { return originType; }
            set { SetPropertyValue("OriginType", ref originType, value); }
        }
    }
}

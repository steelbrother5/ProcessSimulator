using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioItem : EnterpriseBaseObject
    {
        private string iD;
        private Portfolio portfolio;
        private PortfolioItemType portfolioItemType;
        private DateTime entryDate;
        private DateTime exitDate;
        private bool isInPortfolio;
        private Guid originOid;
        private String originType;

        public PortfolioItem(Session session) : base(session) { }

        [Association("Portfolio-PortfolioItem", typeof(Portfolio))]
        public Portfolio Portfolio
        {
            get { return portfolio; }
            set { SetPropertyValue("Portfolio", ref portfolio, value); }
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

        public bool IsInPortfolio
        {
            get { return isInPortfolio; }
            set { SetPropertyValue("IsInPortfolio", ref isInPortfolio, value); }
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

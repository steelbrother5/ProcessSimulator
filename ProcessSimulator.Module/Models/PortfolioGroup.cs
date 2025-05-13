using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioGroup : EnterpriseBaseObject
    {
        private int iD;
        private string name;

        public PortfolioGroup(Session session) : base(session) { }

        public int ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        [Association("Portfolio_PortfolioGroup", typeof(Portfolio), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Portfolio> Portfolios
        {
            get { return GetCollection<Portfolio>("Portfolios"); }
        }

        [Association("Trader_SelectedPortfolioGroups", typeof(Trader), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Trader> SelectedsPortfolioGroupTraders
        {
            get { return GetCollection<Trader>("SelectedsPortfolioGroupTraders"); }
        }
    }
}

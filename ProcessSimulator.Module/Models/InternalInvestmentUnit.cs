using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class InternalInvestmentUnit : EnterpriseBaseObject
    {
        private int iD;
        private string name;
        private ExternalInvestmentUnit externalInvestmentUnit;

        public InternalInvestmentUnit(Session session) : base(session) { }

        [Association("ExternalInvestmentUnit-InternalInvestmentUnit", typeof(ExternalInvestmentUnit))]
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get { return externalInvestmentUnit; }
            set { SetPropertyValue("ExternalInvestmentUnit", ref externalInvestmentUnit, value); }
        }

        [Association("InternalInvestmentUnit-Portfolio", typeof(Portfolio)), Aggregated]
        public XPCollection<Portfolio> Portfolios => GetCollection<Portfolio>("Portfolios");


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

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

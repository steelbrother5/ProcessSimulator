using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class CurrencyPack : EnterpriseBaseObject
    {
        private Currency currency;

        public CurrencyPack(Session session) : base(session) { }

        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private PortfolioNovelty _portfolioNovelty;
        [Association("PortfolioNovelty-CurrencyPack")]
        public PortfolioNovelty PortfolioNovelty
        {
            get { return _portfolioNovelty; }
            set { SetPropertyValue("PortfolioNovelty", ref _portfolioNovelty, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}

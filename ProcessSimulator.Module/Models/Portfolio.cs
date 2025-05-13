using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class Portfolio : EnterpriseBaseObject
    {
        private string iD;
        private string name;
        private InternalInvestmentUnit internalInvestmentUnit;

        public Portfolio(Session session) : base(session) { }

        [Association("InternalInvestmentUnit-Portfolio", typeof(InternalInvestmentUnit))]
        public InternalInvestmentUnit InternalInvestmentUnit
        {
            get { return internalInvestmentUnit; }
            set { SetPropertyValue("InternalInvestmentUnit", ref internalInvestmentUnit, value); }
        }

        [Association("Portfolio-PortfolioItem", typeof(PortfolioItem))]
        [Appearance("PortfolioItemsHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<PortfolioItem> PortfolioItems
        {
            get { return GetCollection<PortfolioItem>("PortfolioItems"); }
        }

        [Association("Portfolio_PortfolioGroup", typeof(PortfolioGroup), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<PortfolioGroup> PortfolioGroups
        {
            get { return GetCollection<PortfolioGroup>("PortfolioGroups"); }
        }

        [Association("Trader_SelectedPortfolios", typeof(Trader), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Trader> Traders
        {
            get { return GetCollection<Trader>("Traders"); }
        }

        public string ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        PortfolioClosingMaster portfolioClosingMaster = null;
        [Appearance("PortfolioClosingMasterHide", Visibility = ViewItemVisibility.Hide)]
        public PortfolioClosingMaster PortfolioClosingMaster
        {
            get { return portfolioClosingMaster; }
            set
            {
                if (portfolioClosingMaster == value)
                    return;

                PortfolioClosingMaster prevPortfolioClosingMaster = portfolioClosingMaster;
                portfolioClosingMaster = value;

                if (IsLoading) return;

                if (prevPortfolioClosingMaster != null && prevPortfolioClosingMaster.Portfolio == this)
                    prevPortfolioClosingMaster.Portfolio = null;

                if (portfolioClosingMaster != null)
                    portfolioClosingMaster.Portfolio = this;
                OnChanged("PortfolioClosingMaster");
            }
        }

        /// <summary>
        /// Asociacion propiedad Portafolio en parametros PyG.
        /// </summary>
        [Association("Portf_PnLRepParameters", typeof(ProfitandLossesReportParameters), UseAssociationNameAsIntermediateTableName = true)]
        [Appearance("ProfitandLossesReportParametersHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<ProfitandLossesReportParameters> ProfitandLossesReportParameters => GetCollection<ProfitandLossesReportParameters>("ProfitandLossesReportParameters");
        protected override void OnSaving()
        {
            base.OnSaving();
            XCreateDateTime = DateTime.Now;
        }
    }
}

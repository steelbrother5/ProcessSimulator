using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExternalInvestmentUnit : EnterpriseBaseObject
    {
        private string iD;
        private string name;
        private CompanyBaseObject company;

        public ExternalInvestmentUnit(Session session) : base(session) { }

        [Association("ExternalInvestmentUnit-CompanyBaseObject", typeof(CompanyBaseObject))]
        [RuleRequiredField(DefaultContexts.Save)]
        public CompanyBaseObject Company
        {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }

        [Association("ExternalInvestmentUnit-InternalInvestmentUnit", typeof(InternalInvestmentUnit)), Aggregated]
        public XPCollection<InternalInvestmentUnit> InternalInvestmentUnits => GetCollection<InternalInvestmentUnit>("InternalInvestmentUnits");

        [Association("ExternalInvestmentUnit-DepositAccount", typeof(DepositAccount)), Aggregated]
        public XPCollection DepositAccounts => GetCollection("DepositAccounts");

        [Association("ExternalInvestmentUnit-ExternalInvestmentUnitItem", typeof(ExternalInvestmentUnitItem))]
        [Appearance("ExternalInvestmentUnitItemsHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<ExternalInvestmentUnitItem> ExternalInvestmentUnitItems => GetCollection<ExternalInvestmentUnitItem>("ExternalInvestmentUnitItems");

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

        ExternalInvestmentUnitClosingMaster externalInvestmentUnitClosingMaster = null;
        [Appearance("ExternalInvestmentUnitClosingMasterHide", Visibility = ViewItemVisibility.Hide)]
        public ExternalInvestmentUnitClosingMaster ExternalInvestmentUnitClosingMaster
        {
            get { return externalInvestmentUnitClosingMaster; }
            set
            {
                if (externalInvestmentUnitClosingMaster == value)
                    return;

                ExternalInvestmentUnitClosingMaster prevExternalInvestmentUnitClosingMaster = externalInvestmentUnitClosingMaster;
                externalInvestmentUnitClosingMaster = value;

                if (IsLoading) return;

                if (prevExternalInvestmentUnitClosingMaster != null && prevExternalInvestmentUnitClosingMaster.ExternalInvestmentUnit == this)
                    prevExternalInvestmentUnitClosingMaster.ExternalInvestmentUnit = null;

                if (externalInvestmentUnitClosingMaster != null)
                    externalInvestmentUnitClosingMaster.ExternalInvestmentUnit = this;
                OnChanged("ExternalInvestmentUnitClosingMaster");
            }
        }

        ExternalInvestmentUnitDepositAccountsLoadingMaster externalInvestmentUnitDepositAccountsLoadingMaster = null;
        [Appearance("Hide2", Visibility = ViewItemVisibility.Hide)]
        public ExternalInvestmentUnitDepositAccountsLoadingMaster ExternalInvestmentUnitDepositAccountsLoadingMaster
        {
            get { return externalInvestmentUnitDepositAccountsLoadingMaster; }
            set
            {
                if (externalInvestmentUnitDepositAccountsLoadingMaster == value)
                    return;

                // Store a reference to the former owner. 
                ExternalInvestmentUnitDepositAccountsLoadingMaster prevExternalInvestmentUnitDepositAccountsLoadingMaster =
                    externalInvestmentUnitDepositAccountsLoadingMaster;
                externalInvestmentUnitDepositAccountsLoadingMaster = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists. 
                if (prevExternalInvestmentUnitDepositAccountsLoadingMaster != null && prevExternalInvestmentUnitDepositAccountsLoadingMaster.ExternalInvestmentUnit == this)
                    prevExternalInvestmentUnitDepositAccountsLoadingMaster.ExternalInvestmentUnit = null;

                // Specify that the building is a new owner's house. 
                if (externalInvestmentUnitDepositAccountsLoadingMaster != null)

                    externalInvestmentUnitDepositAccountsLoadingMaster.ExternalInvestmentUnit = this;

                OnChanged("ExternalInvestmentUnitDepositAccountsLoadingMaster");
            }
        }

        private XPCollection<Portfolio> portfolios;
        [Browsable(false)]
        public XPCollection<Portfolio> Portfolios
        {
            get
            {
                if (portfolios == null)
                {
                    portfolios = new XPCollection<Portfolio>(Session);
                    RefreshAvailablePortfolios(Name);
                }
                return portfolios;
            }
        }
        private void RefreshAvailablePortfolios(string Name)
        {
            if (portfolios == null)
                return;
            portfolios.Criteria = CriteriaOperator.Parse("[InternalInvestmentUnit.ExternalInvestmentUnit.Name] = ? ", Name);

        }

        /// <summary>
        /// Asociacion propiedad UEI en parametros PyG.
        /// </summary>
        [Association("EIU_PnLRepParameters", typeof(ProfitandLossesReportParameters), UseAssociationNameAsIntermediateTableName = true)]
        [Appearance("ProfitandLossesReportParametersHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<ProfitandLossesReportParameters> ProfitandLossesReportParameters => GetCollection<ProfitandLossesReportParameters>("ProfitandLossesReportParameters");

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

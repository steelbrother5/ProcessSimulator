using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ProcessSimulator.Module.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class Trader : Persons
    {
        private string tradercode;
        private string name;
        private bool isActive;

        public Trader(Session session) : base(session) { }

        [RuleRequiredField(DefaultContexts.Save)]
        [Size(13)]
        public string TraderCode
        {
            get { return tradercode; }
            set { SetPropertyValue("TraderCode", ref tradercode, value); }
        }

        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenApostrophe, CustomMessageTemplate = "El Codigo de Trader solo puede contener letras, números, guiones y apostrofe. ( -, ' y espacios)")]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
        //[Association("Trader_TraderGroup", typeof(TraderGroup), UseAssociationNameAsIntermediateTableName = true)]
        //public XPCollection<TraderGroup> TraderGroups
        //{
        //    get { return GetCollection<TraderGroup>("TraderGroups"); }
        //}

        [Association("Trader_SelectedPortfolios", typeof(Portfolio), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Portfolio> SelectedPortfolios
        {
            get { return GetCollection<Portfolio>("SelectedPortfolios"); }
        }

        [Association("Trader_SelectedPortfolioGroups", typeof(PortfolioGroup), UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<PortfolioGroup> SelectedPortfolioGroups
        {
            get { return GetCollection<PortfolioGroup>("SelectedPortfolioGroups"); }
        }

        //[Association("Trader_SelectedCompanies", typeof(Company), UseAssociationNameAsIntermediateTableName = true)]
        //public XPCollection<Company> SelectedCompanies
        //{
        //    get { return GetCollection<Company>("SelectedCompanies"); }
        //}
        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public new class Fields
        {
            private Fields() { }
            public static OperandProperty ID
            {
                get { return new OperandProperty("ID"); }
            }
        }

        /// <summary>
        /// Código asignado en SETFX
        /// </summary>
        private string _setFXCode;
        public string SetFXCode
        {
            get { return _setFXCode; }
            set { SetPropertyValue<string>("SetFXCode", ref _setFXCode, value); }
        }

        /// <summary>
        /// Código empleado en SETFX
        /// </summary>
        private string employeeCode;
        public string EmployeeCode
        {
            get { return employeeCode; }
            set { SetPropertyValue<string>("EmployeeCode", ref employeeCode, value); }
        }

        private string userId;
        /// <summary>
        /// Id del Usuario
        /// </summary>
        public string UserId
        {
            get => userId;
            set => SetPropertyValue(nameof(UserId), ref userId, value);
        }

        /// <summary>
        /// Asociacion propiedad Trader en parametros PyG.
        /// </summary>
        [Association("Traders_PnLRepParameters", typeof(ProfitandLossesReportParameters), UseAssociationNameAsIntermediateTableName = true)]
        [Appearance("ProfitandLossesReportParametersHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<ProfitandLossesReportParameters> ProfitandLossesReportParameters
            => GetCollection<ProfitandLossesReportParameters>("ProfitandLossesReportParameters");
    }
}

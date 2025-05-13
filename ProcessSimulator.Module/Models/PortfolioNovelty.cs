using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ProcessSimulator.Module.BusinessMethods;
using ProcessSimulator.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioNovelty : EnterpriseBaseObject
    {

        #region Propiedades no persistentes

        /// <summary>
        /// Colección de días festivos posteriores a la fecha de cierre.
        /// </summary>
        [NonPersistent]
        public DateTime[] HolidaysAfterOperationDate { get; set; }


        /// <summary>
        /// Concepto de novedad obligatorio
        /// </summary>
        [NonPersistent]
        public bool IsNoveltyConceptRequired { get; set; }

        /// <summary>
        /// Persona/Cliente obligatorio
        /// </summary>
        [NonPersistent]
        public bool IsClientCounterpartyRequired { get; set; }

        /// <summary>
        /// Cantidad de conceptos de novedades activados.
        /// </summary>
        [Appearance("AvailableNoveltyConceptsInvisible", Visibility = ViewItemVisibility.Hide)]
        [NonPersistent]
        public int AvailableNoveltyConcepts { get; set; }
        #endregion

        public PortfolioNovelty(Session session) : base(session)
            => Status = Enums.EnumPortfolioNoveltyStatus.Ingresado;

        private string id;
        /// <summary>
        /// ID
        /// </summary>
        [Appearance("IDDisabled", Enabled = false)]
        public string ID
        {
            get => id;
            set => SetPropertyValue("ID", ref id, value);
        }

        private ExternalInvestmentUnit externalInvestmentUnit;
        /// <summary>
        /// Unidad Externa de Inversión
        /// </summary>
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Unidad Externa de Inversión es obligatoria")]
        [Appearance("ExternalInvestmentUnitDisabled", Enabled = false)]
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get { return externalInvestmentUnit; }
            set
            {
                if (SetPropertyValue("ExternalInvestmentUnit", ref externalInvestmentUnit, value) && !IsLoading && !IsSaving)
                {
                    if (ExternalInvestmentUnit?.ExternalInvestmentUnitClosingMaster == null) Portfolio = null;
                    else
                    {
                        DateTime InitialDate = ExternalInvestmentUnit.ExternalInvestmentUnitClosingMaster.LastClosingDate.Date;

                        if (InitialDate != DateTime.MinValue)
                        {
                            DateTime ResponseComplianceDate =
                                new BusinessDaysClass().GetComplianceDate(0,
                                                                            InitialDate.AddDays(1),
                                                                            Session,
                                                                            HolidaysAfterOperationDate);

                            ComplianceDate = ResponseComplianceDate;
                        }
                    }
                }
            }
        }

        private Portfolio portfolio;
        /// <summary>
        /// Portafolio
        /// </summary>
        [DataSourceProperty(nameof(AvailablePortFolios))]
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Portafolio es obligatorio")]
        [Appearance("PortFolioDisabled", Enabled = false,
                    Criteria = "AvailablePortFolios.Count() <= 1")]
        public Portfolio Portfolio
        {
            get => portfolio;
            set => SetPropertyValue("Portfolio", ref portfolio, value);
        }

        private XPCollection<Portfolio> availablePortFolios;
        /// <summary>
        /// Portafolios Disponibles
        /// </summary>
        [Browsable(false)]
        public XPCollection<Portfolio> AvailablePortFolios
        {
            get => availablePortFolios;
            set
            {
                SetPropertyValue("AvailablePortFolios", ref availablePortFolios, value);

                if (!IsSaving && !IsLoading && AvailablePortFolios?.Count() <= 1)
                    Portfolio = AvailablePortFolios.FirstOrDefault();
            }
        }

        private DateTime complianceDate;
        /// <summary>
        /// Fecha de Cumplimiento
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Fecha de Cumplimiento es obligatoria")]
        [Appearance("ComplianceDateDisabled", Enabled = false)]
        public DateTime ComplianceDate
        {
            get => complianceDate;
            set => SetPropertyValue<DateTime>("ComplianceDate", ref complianceDate, value);
        }

        private PortfolioNoveltyType portfolioNoveltyType;
        /// <summary>
        /// Tipo de Novedad de Portafolio
        /// </summary>
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Tipo de Novedad de Portafolio es obligatoria")]
        public PortfolioNoveltyType PortfolioNoveltyType
        {
            get => portfolioNoveltyType;
            set
            {
                if (SetPropertyValue("PortfolioNoveltyType", ref portfolioNoveltyType, value) && !IsLoading && !IsSaving)
                {
                    NoveltyConcept = null;
                    ClientCounterparty = null;
                    Currency = null;

                    if (PortfolioNoveltyType != null)
                    {
                        if (PortfolioNoveltyType.ID == Enums.EnumPortfolioNoveltyType.RendimientoGarantia)
                            GenerateDepositAccountMovement = false;

                        if (PortfolioNoveltyType.ID == Enums.EnumPortfolioNoveltyType.IngresoPortafolio ||
                            PortfolioNoveltyType.ID == Enums.EnumPortfolioNoveltyType.SalidaPortafolio)
                            PortfolioNoveltyEntity = null;
                    }
                }
            }
        }

        private PortfolioNoveltyEntity portfolioNoveltyEntity;
        /// <summary>
        /// Entidad Novedad de Portafolio
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save,
                           TargetCriteria = "PortfolioNoveltyType.ID == 3 || PortfolioNoveltyType.ID == 4 || PortfolioNoveltyType.ID == 5",
                           CustomMessageTemplate = "Entidad Novedad de Portafolio es obligatoria en el tipo de novedad seleccionado")]
        [Appearance("PortfolioNoveltyEntityInvisible", Visibility = ViewItemVisibility.Hide,
                    Criteria = "PortfolioNoveltyType.ID != 3 && PortfolioNoveltyType.ID != 4 && PortfolioNoveltyType.ID != 5")]
        public PortfolioNoveltyEntity PortfolioNoveltyEntity
        {
            get => portfolioNoveltyEntity;
            set => SetPropertyValue("PortfolioNoveltyEntity", ref portfolioNoveltyEntity, value);
        }

        private Enums.EnumPortfolioNoveltyStatus status;
        /// <summary>
        /// Estado
        /// </summary>
        [Appearance("StatusDisabled", Enabled = false)]
        public Enums.EnumPortfolioNoveltyStatus Status
        {
            get => status;
            set => SetPropertyValue("Status", ref status, value);
        }

        private Currency currency;
        /// <summary>
        /// Moneda Negociada
        /// </summary>
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Moneda Negociada es obligatoria")]
        public Currency Currency
        {
            get => currency;
            set
            {
                if (SetPropertyValue("Currency", ref currency, value) && !IsLoading && !IsSaving)
                {
                    DepositAccount = null;
                    FaceValue = 0;
                    NegociatedOverUSDRate = (Currency?.IsUSDollar ?? false) && (NoveltyConcept?.ApplyRegisteredRate ?? false) ? 1 : 0;
                    TotalAmountUSD = 0;
                    LocalOverUSDRate = 0;
                }
            }
        }

        private bool generateDepositAccountMovement;
        /// <summary>
        /// Generar Movimiento Bancario
        /// </summary>
        [ImmediatePostData(true)]
        [Appearance("GenerateDepositAccountMovementInvisible", Visibility = ViewItemVisibility.Hide,
                    Criteria = "PortfolioNoveltyType.ID == 5")]
        public bool GenerateDepositAccountMovement
        {
            get => generateDepositAccountMovement;
            set
            {
                if (SetPropertyValue("GenerateDepositAccountMovement", ref generateDepositAccountMovement, value)
                    && !IsLoading && !IsSaving && !GenerateDepositAccountMovement)
                    DepositAccount = null;
            }
        }

        private DepositAccount depositAccount;
        /// <summary>
        /// Cuenta Bancaria
        /// </summary>
        [DataSourceCriteria("Currency = '@This.Currency'")]
        [RuleRequiredField(DefaultContexts.Save,
                           TargetCriteria = "GenerateDepositAccountMovement",
                           CustomMessageTemplate = "Cuenta Bancaria es obligatoria cuando está marcado Generar Movimiento Bancario")]
        [Appearance("DepositAccountDisabledHide", Visibility = ViewItemVisibility.Hide,
                    Criteria = "!GenerateDepositAccountMovement")]
        public DepositAccount DepositAccount
        {
            get => depositAccount;
            set => SetPropertyValue("DepositAccount", ref depositAccount, value);
        }

        private decimal faceValue;
        /// <summary>
        /// Valor en Moneda Original
        /// </summary>
        [ImmediatePostData(true)]

        [RuleValueComparison("FaceValuePositive", DefaultContexts.Save,
                             ValueComparisonType.GreaterThan, "0",
                             CustomMessageTemplate = "Valor en Moneda Original debe ser mayor a 0")]
        public decimal FaceValue
        {
            get => faceValue;
            set
            {
                if (SetPropertyValue("FaceValue", ref faceValue, value) && !IsLoading && !IsSaving)
                    TotalAmountUSD = FaceValue * NegociatedOverUSDRate;
            }
        }

        private decimal settlementAmount;
        /// <summary>
        /// Valor Moneda Local
        /// </summary>
        [Appearance("SettlementAmountInvisible", Visibility = ViewItemVisibility.Hide)]
        public decimal SettlementAmount
        {
            get => settlementAmount;
            set => SetPropertyValue<decimal>("SettlementAmount", ref settlementAmount, value);
        }

        [Association("PortfolioNovelty-CurrencyPack"), Aggregated]
        public XPCollection<CurrencyPack> CurrencyPacks
            => GetCollection<CurrencyPack>("CurrencyPacks");

        [Association("PortfolioNovelty-PortfolioInventoryItem"), Aggregated]
        public XPCollection<PortfolioInventoryItem> PortfolioInventoryItems
            => GetCollection<PortfolioInventoryItem>("PortfolioInventoryItems");

        private DepositAccountMovements depositAccountMovement;
        /// <summary>
        /// Movimiento de Cuenta Bancaria
        /// </summary>
        [Appearance("DepositAccountMovementInvisible", Visibility = ViewItemVisibility.Hide)]
        public DepositAccountMovements DepositAccountMovement
        {
            get => depositAccountMovement;
            set => SetPropertyValue("DepositAccountMovement", ref depositAccountMovement, value);
        }

        private Trader trader;
        /// <summary>
        /// Trader
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save,
                           CustomMessageTemplate = "Trader es obligatorio")]
        [Appearance("TraderDisabled", Enabled = false)]
        public Trader Trader
        {
            get => trader;
            set => SetPropertyValue("Trader", ref trader, value);
        }

        private PortfolioNoveltyConcept noveltyConcept;
        /// <summary>
        /// Concepto Novedad
        /// </summary>
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save,
                           TargetCriteria = "IsNoveltyConceptRequired &&" +
                                            "(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2)",
                           CustomMessageTemplate = "Concepto Novedad es obligatorio cuando está marcado en la parametrización")]
        [Appearance("NoveltyConceptDisabled", Enabled = false,
                    Criteria = "!(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2) || AvailableNoveltyConcepts < 1")]
        public PortfolioNoveltyConcept NoveltyConcept
        {
            get => noveltyConcept;
            set
            {
                if (SetPropertyValue<PortfolioNoveltyConcept>("NoveltyConcept", ref noveltyConcept, value) && !IsLoading && !IsSaving)
                {
                    ClientCounterparty = null;
                    Currency = null;
                    AffectsBalancesPortfolioNovelty = false;
                }

                if (NoveltyConcept?.AffectsBalancesPortfolio == true)
                    AffectsBalancesPortfolioNovelty = true;
            }
        }

        private Clients clientCounterparty;
        /// <summary>
        /// Cliente/Contraparte
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save,
                           TargetCriteria = "IsClientCounterpartyRequired &&" +
                                            "(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2)",
                           CustomMessageTemplate = "Cliente/Contraparte es obligatorio cuando está marcado en la parametrización")]
        [Appearance("PersonsClientDisabled", Enabled = false,
                    Criteria = "!(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2)")]
        public Clients ClientCounterparty
        {
            get => clientCounterparty;
            set => SetPropertyValue<Clients>("ClientCounterparty", ref clientCounterparty, value);
        }

        private decimal negociatedOverUSDRate;
        /// <summary>
        /// Tasa Moneda Negociada a USD
        /// </summary>
        [ImmediatePostData(true)]

        [RuleValueComparison("NegociatedOverUSDRatePositive", DefaultContexts.Save,
                             ValueComparisonType.GreaterThan, "0",
                             TargetCriteria = "NoveltyConcept != null && NoveltyConcept.ApplyRegisteredRate",
                             CustomMessageTemplate = "Tasa Moneda Negociada a USD debe ser mayor a 0")]
        [Appearance("NegociatedOverUSDRateDisabled", Enabled = false,
                    Criteria = "NoveltyConcept == null || !NoveltyConcept.ApplyRegisteredRate ||" +
                               "!(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2) ||" +
                               "AvailableNoveltyConcepts < 1 || Currency.IsUSDollar")]
        [ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        [ModelDefault("EditMask", "##################,##0.000000")]
        [DbType("decimal(23,8)")]
        public decimal NegociatedOverUSDRate
        {
            get => negociatedOverUSDRate;
            set
            {
                if (SetPropertyValue("NegociatedOverUSDRate", ref negociatedOverUSDRate, value) && !IsLoading && !IsSaving)
                    TotalAmountUSD = FaceValue * NegociatedOverUSDRate;
            }
        }

        private decimal localOverUSDRate;
        /// <summary>
        /// Tasa Moneda Local a USD
        /// </summary>

        [RuleValueComparison("LocalOverUSDRatePositive", DefaultContexts.Save,
                             ValueComparisonType.GreaterThan, "0",
                             TargetCriteria = "NoveltyConcept != null && NoveltyConcept.ApplyRegisteredRate",
                             CustomMessageTemplate = "Tasa Moneda Local a USD debe ser mayor a 0")]
        [Appearance("LocalOverUSDRateDisabled", Enabled = false,
                    Criteria = "NoveltyConcept == null || !NoveltyConcept.ApplyRegisteredRate ||" +
                               "!(PortfolioNoveltyType.ID == 1 || PortfolioNoveltyType.ID == 2) ||" +
                               "AvailableNoveltyConcepts < 1")]
        [ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        [ModelDefault("EditMask", "##################,##0.000000")]
        [DbType("decimal(23,8)")]
        public decimal LocalOverUSDRate
        {
            get => localOverUSDRate;
            set => SetPropertyValue("LocalOverUSDRate", ref localOverUSDRate, value);
        }

        private decimal totalAmountUSD;
        /// <summary>
        /// Monto Total en USD
        /// </summary>

        [RuleValueComparison("TotalAmountUSDPositive", DefaultContexts.Save,
                             ValueComparisonType.GreaterThan, "0",
                             TargetCriteria = "NoveltyConcept != null && NoveltyConcept.ApplyRegisteredRate",
                             CustomMessageTemplate = "Monto total en USD debe ser mayor a 0")]
        [Appearance("TotalAmountUSDDisabled", Enabled = false)]
        [ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        [ModelDefault("EditMask", "##################,##0.00")]
        public decimal TotalAmountUSD
        {
            get => totalAmountUSD;
            set => SetPropertyValue("TotalAmountUSD", ref totalAmountUSD, value);
        }

        private bool affectsBalancesPortfolioNovelty;
        /// <summary>
        /// Afecta Saldos en Portafolio
        /// </summary>
        [ImmediatePostData(true)]
        [Appearance("AffectsBalancesPortfolioNoveltyDisabled", Enabled = false)]
        public bool AffectsBalancesPortfolioNovelty
        {
            get => affectsBalancesPortfolioNovelty;
            set => SetPropertyValue("AffectsBalancesPortfolioNovelty", ref affectsBalancesPortfolioNovelty, value);
        }
    }
}

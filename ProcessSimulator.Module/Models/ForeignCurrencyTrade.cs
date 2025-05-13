using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ProcessSimulator.Module.BusinessMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ForeignCurrencyTrade : BaseObject, IQuotable
    {
        public ForeignCurrencyTrade(Session session) : base(session) { }

        /// <summary>
        /// Colección de días festivos posteriores a la fecha de operación.
        /// </summary>
        [NonPersistent]
        public DateTime[] HolidaysAfterOperationDate { get; set; }

        /// <summary>
        /// Parámetros necesarios para el registro de operaciones MEME.
        /// </summary>
        //[NonPersistent]
        //public ForeignExchangeOperationParameter[] OperationsParameters { get; set; }

        /// <summary>
        /// Diccionario que contiene los valores de intercambio para las monedas identificadas por código
        /// las cuales tienen relación con la moneda USD,cada moneda contiene un diccionario con las fechas 
        /// en la que se registro el valor de intercambio y el respectivo valor.
        /// </summary>

        [NonPersistent]
        public Dictionary<string, Dictionary<DateTime, double>> ExchangeValuesToUSD { get; set; }

        /// <summary>
        /// Cantidad de números decimales para el redondeo de las tasas.
        /// </summary>
        [NonPersistent]
        public string RateRounding { get; set; }

        /// <summary>
        /// Cantidad de números decimales para el redondeo de los montos.
        /// </summary>
        [NonPersistent]
        public string AmountRounding { get; set; }

        /// <summary>
        /// Propiedad no persistente que almacena información del país que se especifica en la clase 
        /// ApplicationsParameters donde su propiedad "Property" es "CodigoPais".
        /// </summary>
        [NonPersistent]
        public Countries NPCountry { get; set; }

        /// <summary>
        /// Información de la moneda USD.
        /// </summary>
        [NonPersistent]
        public Currency NPUSDCurrency { get; set; }

        /// <summary>
        /// Diccionario que almacena los valores de intercambio para las monedas identificadas por código
        /// que estan relacionadas con la moneda local, cada moneda contiene un diccionario con las fechas 
        /// de registro de las tasas de cambio y sus respectivos valores.
        /// </summary>
        [NonPersistent]
        public Dictionary<string, Dictionary<DateTime, double>> ExchangeValuesForLocalCurrency { get; set; }

        /// <summary>
        /// Diccionario que contiene las diferentes fechas de cierre
        /// </summary>
        [NonPersistent]
        public Dictionary<string, DateTime> ClosingDates { get; set; }

        /// <summary>
        ///  Variable privada que maneja los parametros de Registro de Operaciones
        /// </summary>
        //private ForeignExchangeOperationParameter operationParameters;

        private string _OperationNumber;
        /// <summary>
        /// Número Operación
        /// </summary>
        [Size(100)]
        [Appearance("OperationNumberDisabled", Enabled = false)]
        public string OperationNumber
        {
            get => _OperationNumber;
            set => SetPropertyValue("OperationNumber ", ref _OperationNumber, value);
        }

        //private OperationStatus _OperationStatus;
        ///// <summary>
        ///// Estado Operación
        ///// </summary>
        //[Appearance("OperationStatusDisabled", Enabled = false)]
        //public OperationStatus OperationStatus
        //{
        //    get => _OperationStatus;
        //    set => SetPropertyValue("OperationStatus", ref _OperationStatus, value);
        //}

        //private ComplementedStatus _ComplementedStatus;
        ///// <summary>
        ///// Estado Complementada
        ///// </summary>
        //[Appearance("ComplementedStatusDisabled", Enabled = false)]
        //public ComplementedStatus ComplementedStatus
        //{
        //    get => _ComplementedStatus;
        //    set => SetPropertyValue("ComplementedStatus", ref _ComplementedStatus, value);
        //}

        private ExternalInvestmentUnit _ExternalInvestmentUnit;
        /// <summary>
        /// Unidad Externa de Inversión
        /// </summary>
        //[ImmediatePostData(true)]
        //public ExternalInvestmentUnit ExternalInvestmentUnit
        //{
        //    get => _ExternalInvestmentUnit;
        //    set
        //    {
        //        if (SetPropertyValue("ExternalInvestmentUnit", ref _ExternalInvestmentUnit, value) && !IsLoading && !IsSaving)
        //        {
        //            if (ExternalInvestmentUnit != null)
        //            {
        //                if (OperationDate == DateTime.MinValue)
        //                {
        //                    if (ClosingDates.ContainsKey(_ExternalInvestmentUnit.Name))
        //                    {
        //                        DateTime ProcessDate = ClosingDates[_ExternalInvestmentUnit.Name];

        //                        if (ProcessDate == DateTime.MinValue)
        //                            if (ClosingDates.ContainsKey(BusinessExpressions.ClosingDate))
        //                                OperationDate = ClosingDates[BusinessExpressions.ClosingDate];

        //                        OperationDate = ProcessDate;
        //                    }
        //                    else if (ClosingDates.ContainsKey(BusinessExpressions.ClosingDate))
        //                        OperationDate = ClosingDates[BusinessExpressions.ClosingDate];
        //                }
        //                if (ExternalInvestmentUnit.Company != null)
        //                {
        //                    ForeignExchangeOperationParameter OperationParameter =
        //                        OperationsParameters.FirstOrDefault(operationParameters => operationParameters?.Company?.Oid == ExternalInvestmentUnit?.Company?.Oid);

        //                    if (OperationParameter != null && OperationParameter.AutofillHour)
        //                        OperationTime = DateTime.Now.TimeOfDay;
        //                }
        //            }
        //            else
        //            {
        //                Portfolio = null;
        //            }
        //        }
        //    }
        //}

        private Portfolio _Portfolio;
        /// <summary>
        /// Portafolio
        /// </summary>        
        [DataSourceProperty(nameof(AvailablePortfolios))]
        [Appearance("PortfolioDisabled", Enabled = false, Criteria = "AvailablePortfolios.Count() <= 1")]
        public Portfolio Portfolio
        {
            get => _Portfolio;
            set => SetPropertyValue("Portfolio", ref _Portfolio, value);
        }

        private XPCollection<Portfolio> availablePortfolios;
        /// <summary>
        /// Colección fuente de datos para la propiedad Portfolio.
        /// </summary>
        [Browsable(false)]
        public XPCollection<Portfolio> AvailablePortfolios
        {
            get => availablePortfolios;
            set
            {
                SetPropertyValue("AvailablePortfolios", ref availablePortfolios, value);

                if (!IsSaving && !IsLoading && AvailablePortfolios?.Count() <= 1)
                    Portfolio = AvailablePortfolios.FirstOrDefault();
            }
        }

        //private OperationType _OperationType;
        ///// <summary>
        ///// Tipo de Operación
        ///// </summary>
        //[Appearance("OperationTypeDisabled", Enabled = false, Criteria = "CrossOperation == True")]
        //public OperationType OperationType
        //{
        //    get => _OperationType;
        //    set => SetPropertyValue("OperationType", ref _OperationType, value);
        //}

        //private AgreedTermTypes _Market;
        ///// <summary>
        ///// Mercado: Spot, T+1, T+2, T+3
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("MarketDisabled", Enabled = false, Criteria = "CrossOperation == True")]
        //[Appearance("MarketEnabled", Criteria = "IsMarketModifiable", Enabled = false)]
        //public AgreedTermTypes Market
        //{
        //    get => _Market;
        //    set
        //    {
        //        if (SetPropertyValue("Market", ref _Market, value) && !IsLoading && !IsSaving)
        //        {
        //            if (Market != null && OperationDate != null && Country != null)
        //            {
        //                Response<DateTime> ComplianceDateResponse = new BusinessDaysClass().GetComplianceDate(Market.days, OperationDate, HolidaysAfterOperationDate);
        //                ComplianceDate = ComplianceDateResponse.IsValid ? ComplianceDateResponse.Value : OperationDate;
        //            }
        //            else
        //                ComplianceDate = OperationDate;
        //        }
        //    }
        //}

        private DateTime _OperationDate;
        /// <summary>
        /// Fecha Operación
        /// </summary>
        [Appearance("OperationDateDisabled", Enabled = false)]
        public DateTime OperationDate
        {
            get => _OperationDate;
            set => SetPropertyValue("OperationDate", ref _OperationDate, value);
        }

        private TimeSpan? _OperationTime;
        /// <summary>
        /// Hora de la operación
        /// </summary>
        [ImmediatePostData(true)]
        [Appearance("OperationTimeDisabled", Enabled = false, Criteria = "OperationStatus.StatusName != 'Ingresada'")]
        public TimeSpan? OperationTime
        {
            get { return _OperationTime; }
            set { SetPropertyValue<TimeSpan?>("OperationTime", ref _OperationTime, value); }
        }

        private DateTime _ComplianceDate;
        /// <summary>
        /// Fecha Cumplimiento
        /// </summary>
        [Appearance("ComplianceDateDisabled", Enabled = false)]
        public DateTime ComplianceDate
        {
            get => _ComplianceDate;
            set => SetPropertyValue("ComplianceDate", ref _ComplianceDate, value);
        }

        private Trader _Trader;
        /// <summary>
        /// Trader
        /// </summary>    
        [Appearance("TraderDisabled", Enabled = false)]
        public Trader Trader
        {
            get => _Trader;
            set => SetPropertyValue("Trader", ref _Trader, value);
        }

        //private Clients _Client;
        ///// <summary>
        ///// Contraparte
        ///// </summary>        
        //[ImmediatePostData(true)]
        //public Clients Client
        //{
        //    get => _Client;
        //    set
        //    {
        //        if (SetPropertyValue("Client", ref _Client, value) && !IsLoading && !IsSaving)
        //        {
        //            if (Client != null)
        //                City = Client.City;
        //            else
        //                City = null;
        //        }
        //    }
        //}

        //private CurrencyMarket _NegotiationSystem;
        ///// <summary>
        ///// Sistema de Negociación
        ///// </summary>                
        //public CurrencyMarket NegotiationSystem
        //{
        //    get => _NegotiationSystem;
        //    set => SetPropertyValue("NegotiationSystem", ref _NegotiationSystem, value);
        //}

        //private CurrencyMarket _RegistrationSystem;
        ///// <summary>
        ///// Sistema de Registro
        ///// </summary>                
        //public CurrencyMarket RegistrationSystem
        //{
        //    get => _RegistrationSystem;
        //    set => SetPropertyValue("RegistrationSystem", ref _RegistrationSystem, value);
        //}

        //private MarketSector _Marketsector;
        ///// <summary>
        ///// Sector
        ///// </summary>                
        //public MarketSector MarketSector
        //{
        //    get => _Marketsector;
        //    set => SetPropertyValue("MarketSector", ref _Marketsector, value);
        //}

        //private Cities _City;
        ///// <summary>
        ///// Ciudad
        ///// </summary>        
        //public Cities City
        //{
        //    get => _City;
        //    set => SetPropertyValue("City", ref _City, value);
        //}

        private Currency _NegotiatedCurrency;
        /// <summary>
        /// Moneda Negociada
        /// </summary>        
        [ImmediatePostData(true)]
        [Appearance("NegotiatedCurrencyDisabled", Enabled = false, Criteria = "CrossOperation == True")]
        public Currency NegotiatedCurrency
        {
            get => _NegotiatedCurrency;
            set
            {
                if (SetPropertyValue("NegotiatedCurrency", ref _NegotiatedCurrency, value) && !IsLoading && !IsSaving)
                {
                    NegotiatedCurrencyAmount = 0;
                    //PaymentCurrency = null;
                    RateNegotiatedToPayment = 0;
                    RatePaymentToNegotiated = 0;
                }
            }
        }

        private decimal _NegotiatedCurrencyAmount;
        /// <summary>
        /// Monto Moneda Negociada
        /// </summary>       
        [ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        [ModelDefault("EditMask", "##################,##0.00")]
        [ImmediatePostData(true)]
        [Appearance("NegotiatedCurrencyAmountDisabled", Enabled = false, Criteria = "CrossOperation == True")]
        public decimal NegotiatedCurrencyAmount
        {
            get => _NegotiatedCurrencyAmount;
            set
            {
                if (SetPropertyValue("NegotiatedCurrencyAmount", ref _NegotiatedCurrencyAmount, value) && !IsLoading && !IsSaving)
                    UpdatePaymentCurrencyAmount();
            }
        }

        //private Currency _PaymentCurrency;
        /// <summary>
        /// Moneda de Pago
        /// </summary>     
        //[ImmediatePostData(true)]
        //public Currency PaymentCurrency
        //{
        //    get => _PaymentCurrency;
        //    set
        //    {
        //        RateNegotiatedToPayment = 0;
        //        RatePaymentToNegotiated = 0;
        //        PaymentCurrencyAmount = 0;
        //        USDAmount = 0;
        //        if (SetPropertyValue("PaymentCurrency", ref _PaymentCurrency, value) && !IsLoading && !IsSaving && PaymentCurrency != null && Country != null)
        //        {

        //            if (PaymentCurrency.IsUSDollar)
        //            {
        //                RatePaymentToUSD = 1;
        //                USDAmount = PaymentCurrencyAmount * RatePaymentToUSD;
        //            }
        //            else
        //            {
        //                Response<decimal?> ExchangeValueResponse =
        //                    new PriceVendorDataManager().GetExchangeValueToUSD(OperationDate.Date.AddDays(-1), PaymentCurrency.Code, ExchangeValuesToUSD, true);

        //                if (ExchangeValueResponse.IsValid && ExchangeValueResponse.Value != null)
        //                {
        //                    RatePaymentToUSD = (decimal)ExchangeValueResponse.Value;
        //                    USDAmount = PaymentCurrencyAmount * RatePaymentToUSD;
        //                }
        //            }
        //        }
        //        else if (!IsLoading && !IsSaving)
        //        {
        //            RateNegotiatedToPayment = 0;
        //            RatePaymentToNegotiated = 0;
        //            PaymentCurrencyAmount = 0;
        //            USDAmount = 0;
        //        }
        //    }
        //}

        private decimal _RateNegotiatedToPayment;
        /// <summary>
        /// Tasa Moneda Negociada a Moneda de Pago
        /// </summary>    
        [ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        [ModelDefault("EditMask", "##################,##0.000000")]
        [DbType("decimal(23,8)")]
        [ImmediatePostData(true)]
        public decimal RateNegotiatedToPayment
        {
            get => _RateNegotiatedToPayment;
            set
            {
                if (SetPropertyValue("RateNegotiatedToPayment", ref _RateNegotiatedToPayment, value) && !IsLoading && !IsSaving)
                {
                    UpdatePaymentCurrencyAmount();
                    if (RateNegotiatedToPayment > 0)
                    {
                        RatePaymentToNegotiated = Math.Round(1 / RateNegotiatedToPayment, RateRound, MidpointRounding.AwayFromZero);
                        USDAmount = PaymentCurrencyAmount * RatePaymentToUSD;
                    }
                }
            }
        }

        private decimal _RatePaymentToNegotiated;
        /// <summary>
        /// Tasa Moneda de Pago a Moneda Negociada
        /// </summary>    
        [ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        [ModelDefault("EditMask", "##################,##0.000000")]
        [DbType("decimal(23,8)")]
        [ImmediatePostData(true)]
        public decimal RatePaymentToNegotiated
        {
            get => _RatePaymentToNegotiated;
            set
            {
                if (SetPropertyValue("RatePaymentToNegotiated", ref _RatePaymentToNegotiated, value) && !IsLoading && !IsSaving)
                {
                    UpdatePaymentCurrencyAmount();
                    if (RatePaymentToNegotiated > 0)
                    {
                        RateNegotiatedToPayment = Math.Round(1 / RatePaymentToNegotiated, RateRound, MidpointRounding.AwayFromZero);
                        USDAmount = PaymentCurrencyAmount * RatePaymentToUSD;
                    }
                }
            }
        }

        private decimal _PaymentCurrencyAmount;
        /// <summary>
        /// Monto Moneda de Pago
        /// </summary>       
        [ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        [ModelDefault("EditMask", "##################,##0.00")]
        public decimal PaymentCurrencyAmount
        {
            get => _PaymentCurrencyAmount;
            set => SetPropertyValue("PaymentCurrencyAmount", ref _PaymentCurrencyAmount, value);
        }

        private decimal _RatePaymentToUSD;
        /// <summary>
        /// Tasa Moneda de Pago a USD
        /// </summary>    
        [ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        [ModelDefault("EditMask", "##################,##0.000000")]
        [DbType("decimal(23,8)")]
        [ImmediatePostData(true)]
        [Appearance("RatePaymentToUSDisabled", Enabled = false)]
        public decimal RatePaymentToUSD
        {
            get => _RatePaymentToUSD;
            set => SetPropertyValue("RatePaymentToUSD", ref _RatePaymentToUSD, value);
        }

        private decimal _UsdAmount;
        /// <summary>
        /// Monto Moneda de Pago
        /// </summary>       
        [ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        [ModelDefault("EditMask", "##################,##0.00")]
        public decimal USDAmount
        {
            get => _UsdAmount;
            set => SetPropertyValue("USDAmount", ref _UsdAmount, value);
        }

        private CurrencyVehicle _CurrencyVehicle;
        /// <summary>
        /// Forma de Pago
        /// </summary>           
        [Appearance("CurrencyVehicleDisabled", Visibility = ViewItemVisibility.Hide, Criteria = "OperationStatus != 'Aprobada' && OperationStatus != 'Complementada'")]
        public CurrencyVehicle CurrencyVehicle
        {
            get => _CurrencyVehicle;
            set => SetPropertyValue("CurrencyVehicle", ref _CurrencyVehicle, value);
        }

        private DepositAccount _NegotiatedCurrencyAccount;
        /// <summary>
        /// Cuenta Bancaria Moneda Negociada
        /// </summary>                
        [Appearance("NegotiatedCurrencyAccountVisibility",
                    Visibility = ViewItemVisibility.Hide, Criteria = "OperationStatus != 'Aprobada' && OperationStatus != 'Complementada'")]
        [DataSourceCriteria("Currency = '@This.NegotiatedCurrency'")]
        [Appearance("NegotiatedCurrencyAccountDisabled", Enabled = false, Criteria = "CrossOperation == True")]
        public DepositAccount NegotiatedCurrencyAccount
        {
            get => _NegotiatedCurrencyAccount;
            set => SetPropertyValue("NegotiatedCurrencyAccount", ref _NegotiatedCurrencyAccount, value);
        }

        private DepositAccount _PaymentCurrencyAccount;
        /// <summary>
        /// Cuenta Bancaria Moneda de Pago
        /// </summary>                
        [Appearance("PaymentCurrencyAccountDisabled",
                    Visibility = ViewItemVisibility.Hide, Criteria = "OperationStatus != 'Aprobada' && OperationStatus != 'Complementada'")]
        [DataSourceCriteria("Currency = '@This.PaymentCurrency'")]
        public DepositAccount PaymentCurrencyAccount
        {
            get => _PaymentCurrencyAccount;
            set => SetPropertyValue("PaymentCurrencyAccount", ref _PaymentCurrencyAccount, value);
        }

        private bool _ShowNextDayInPyG;
        /// <summary>
        /// Mostrar los NextDay el dia de la operación en el PyG.
        /// </summary>
        [ImmediatePostData(true)]
        [Appearance("ShowNextDayInPyGDisabled", Enabled = false, Criteria = "Market.days == 0 || IsNullOrEmpty(Market)")]
        [Appearance("ShowNextDayInPyGEnabled", Enabled = true, Criteria = "Market.days > 0")]
        public bool ShowNextDayInPyG
        {
            get => _ShowNextDayInPyG;
            set => SetPropertyValue("ShowNextDayInPyG", ref _ShowNextDayInPyG, value);
        }

        private string _AnnulObservations;
        /// <summary>
        /// Observaciones de Anulación
        /// </summary> 
        [Appearance("AnnulObservationsDisabled", Enabled = false)]
        public string AnnulObservations
        {
            get => _AnnulObservations;
            set => SetPropertyValue("AnnulObservations", ref _AnnulObservations, value);
        }

        private bool _CrossOperation;
        /// <summary>
        /// Operación Cruzada
        /// </summary> 
        [Appearance("CrossOperationDisabled", Enabled = false)]
        public bool CrossOperation
        {
            get => _CrossOperation;
            set => SetPropertyValue("CrossOperation", ref _CrossOperation, value);
        }

        private ForeignExchangeOperation _ForeignExchangeOperation;
        /// <summary>
        /// Operación de Divisa
        /// </summary>
        [Association("ForeignExchangeOperation_ForeignCurrencyTrade")]
        public ForeignExchangeOperation ForeignExchangeOperation
        {
            get => _ForeignExchangeOperation;
            set => SetPropertyValue("ForeignExchangeOperation", ref _ForeignExchangeOperation, value);
        }

        //private ForeignExchangePurchaseOperationHistoric _ForeignExchangePurchaseOperationHistoric;
        ///// <summary>
        ///// Operación Histórica de Compra
        ///// </summary>
        //[Association("ForeignExchangePurchaseOperationHistoric_ForeignCurrencyTrade")]
        //public ForeignExchangePurchaseOperationHistoric ForeignExchangePurchaseOperationHistoric
        //{
        //    get => _ForeignExchangePurchaseOperationHistoric;
        //    set => SetPropertyValue("ForeignExchangePurchaseOperationHistoric", ref _ForeignExchangePurchaseOperationHistoric, value);
        //}

        //private ForeignExchangeSalesOperationHistoric _ForeignExchangeSalesOperationHistoric;
        ///// <summary>
        ///// Operación Histórica de Venta
        ///// </summary>
        //[Association("ForeignExchangeSalesOperationHistoric_ForeignCurrencyTrade")]
        //public ForeignExchangeSalesOperationHistoric ForeignExchangeSalesOperationHistoric
        //{
        //    get => _ForeignExchangeSalesOperationHistoric;
        //    set => SetPropertyValue("ForeignExchangeSalesOperationHistoric", ref _ForeignExchangeSalesOperationHistoric, value);
        //}

        private int? _RateRound = null;
        private int RateRound
        {
            get
            {
                if (_RateRound != null)
                    return _RateRound.Value;
                else
                {
                    _RateRound = int.TryParse(RateRounding, out int value) ? value : 0;
                    return _RateRound.Value;
                }
            }
        }

        private int? _AmountRound = null;
        private int AmountRound
        {
            get
            {
                if (_AmountRound != null)
                    return _AmountRound.Value;
                else
                {
                    _AmountRound = int.TryParse(AmountRounding, out int value) ? value : 0;
                    return _AmountRound.Value;
                }
            }
        }

        private Countries _Country;
        private Countries Country
        {
            get
            {
                if (_Country != null)
                    return _Country;
                else
                {
                    _Country = NPCountry;
                    return _Country;
                }
            }
        }

        private Currency _USDCurrency = null;
        private Currency USDCurrency
        {
            get
            {
                if (_USDCurrency != null)
                    return _USDCurrency;
                else
                {
                    _USDCurrency = NPUSDCurrency;
                    return _USDCurrency;
                }
            }
        }

        //private Decimal? _TRMUSD = null;
        //[Appearance("TRMUSDHide", Visibility = ViewItemVisibility.Hide)]
        //public Decimal? TRMUSD
        //{
        //    get
        //    {
        //        if (_TRMUSD != null)
        //            return _TRMUSD;
        //        else
        //        {
        //            Response<decimal?> TRMUSDResponse =
        //                new PriceVendorDataManager().GetExchangeValue(OperationDate.Date, USDCurrency.Code, ExchangeValuesForLocalCurrency);
        //            _TRMUSD = TRMUSDResponse.Value;
        //            return _TRMUSD;
        //        }
        //    }
        //}

        private QuotaApprovalStatus _QuotaApprovalStatus;
        public QuotaApprovalStatus QuotaApprovalStatus
        {
            get => _QuotaApprovalStatus;
            set => SetPropertyValue("QuotaApprovalStatus", ref _QuotaApprovalStatus, value);
        }

        private QuotaValidationStatus _QuotaValidationStatus;
        public QuotaValidationStatus QuotaValidationStatus
        {
            get => _QuotaValidationStatus;
            set => SetPropertyValue("QuotaValidationStatus", ref _QuotaValidationStatus, value);
        }

        //private CommunicationMedia communicationMedia;

        //[ImmediatePostData(true)]
        //public CommunicationMedia CommunicationMedia
        //{
        //    get => communicationMedia;
        //    set
        //    {
        //        if (SetPropertyValue("CommunicationMedia", ref communicationMedia, value) && !IsLoading && !IsSaving)
        //            CommunicationMediaObservations = null;
        //    }
        //}

        private string communicationMediaObservations;
        /// <summary>
        /// Observaciones Medio de Comunicación
        /// </summary>        
        [Appearance("CommunicationMediaObservationsDisabled",
                    Enabled = false,
                    Criteria = "CommunicationMedia == null ||" +
                               "(CommunicationMedia != null && CommunicationMedia.ApplyObservations == False)")]
        public string CommunicationMediaObservations
        {
            get => communicationMediaObservations;
            set => SetPropertyValue("CommunicationMediaObservations", ref communicationMediaObservations, value);
        }

        private string prometeoRecordId;
        /// <summary>
        /// Id de la operación dado por el sistema Prometeo
        /// </summary>
        public string PrometeoRecordId
        {
            get => prometeoRecordId;
            set => SetPropertyValue(nameof(PrometeoRecordId), ref prometeoRecordId, value);
        }

        private string prometeoSendingMessage;
        /// <summary>
        /// Mensaje recibido en el envío a Prometeo
        /// </summary>
        [Size(500)]
        public string PrometeoSendingMessage
        {
            get => prometeoSendingMessage;
            set => SetPropertyValue(nameof(PrometeoSendingMessage), ref prometeoSendingMessage, value);
        }

        private bool prometeoSuccessfulSendingResult;
        /// <summary>
        /// Resultado exitoso en el envío a Prometeo
        /// </summary>
        public bool PrometeoSuccessfulSendingResult
        {
            get => prometeoSuccessfulSendingResult;
            set => SetPropertyValue(nameof(PrometeoSuccessfulSendingResult), ref prometeoSuccessfulSendingResult, value);
        }

        public QuotaApprovalStatus GetQuotaApprovalStatus()
            => QuotaApprovalStatus;

        public QuotaValidationStatus GetQuotaValidationStatus()
           => QuotaValidationStatus;

        public QuotaApprovalStatus SetQuotaApprovalStatus(QuotaApprovalStatus quotaApprovalStatus)
            => QuotaApprovalStatus = quotaApprovalStatus;

        public QuotaValidationStatus SetQuotaValidationStatus(QuotaValidationStatus quotaValidationStatus)
            => QuotaValidationStatus = quotaValidationStatus;

        private void UpdatePaymentCurrencyAmount()
        {
            if (RatePaymentToNegotiated != 0 && NegotiatedCurrency != null)
            {
                PaymentCurrencyAmount = Math.Round(NegotiatedCurrencyAmount * RateNegotiatedToPayment, AmountRound, MidpointRounding.AwayFromZero);
                USDAmount = PaymentCurrencyAmount * RatePaymentToUSD;
            }
            else
                PaymentCurrencyAmount = 0;
        }

        #region Envio SetFx

        //private TransmissionStatus transmissionStatus;

        ///// <summary>
        ///// Estado de la transmision del archivo XML para SetFX
        ///// </summary>
        //[Appearance("TransmissionStatusDisabled", Enabled = false)]
        //public TransmissionStatus TransmissionStatus
        //{
        //    get => transmissionStatus;
        //    set => SetPropertyValue(nameof(TransmissionStatus), ref transmissionStatus, value);
        //}

        private string errortransmitting;

        /// <summary>
        /// Mensajes de error enviados por SetFX por medio de log enviado al momento
        /// de transmitir una operación
        /// </summary>
        [Size(1000)]
        [Appearance("ErrortransmittingHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        [Appearance("ErrortransmittingDisabled", Enabled = false)]
        public string Errortransmitting
        {
            get => errortransmitting;
            set => SetPropertyValue(nameof(Errortransmitting), ref errortransmitting, value);
        }

        private string withOutXMLObservations;
        /// <summary>
        /// Motivos de aprobacion sin XML.
        /// </summary>
        [Size(1000)]
        [Appearance("WithOutXMLObservationsHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        [Appearance("WithOutXMLObservationsDisabled", Enabled = false)]
        public string WithOutXMLObservations
        {
            get => withOutXMLObservations;
            set => SetPropertyValue(nameof(WithOutXMLObservations), ref withOutXMLObservations, value);
        }

        private bool wasTrasmittedExecuted;

        /// <summary>
        /// Informa cuando la operación ya ha sido transmitida ejecutada
        /// </summary>
        [Appearance("WasTrasmittedExecutedDisabled", Enabled = false)]
        public bool WasTrasmittedExecuted
        {
            get => wasTrasmittedExecuted;
            set => SetPropertyValue(nameof(WasTrasmittedExecuted), ref wasTrasmittedExecuted, value);
        }

        private bool notifiedByEmail;

        /// <summary>
        /// Notificado por Email
        /// </summary>
        public bool NotifiedByEmail
        {
            get => notifiedByEmail;
            set => SetPropertyValue(nameof(NotifiedByEmail), ref notifiedByEmail, value);
        }

        private bool errorsByUncreatedUser;

        /// <summary>
        /// Tiene errores de Usuario no creado en SetFX
        /// </summary>
        public bool ErrorsByUncreatedUser
        {
            get => errorsByUncreatedUser;
            set => SetPropertyValue(nameof(ErrorsByUncreatedUser), ref errorsByUncreatedUser, value);
        }

        #endregion

        ///// <summary>
        ///// Checkbox que permite editar o no el Mercado de una operación
        ///// </summary>
        //[NonPersistent]
        //public bool IsMarketModifiable
        //{
        //    get
        //    {
        //        if (OperationNumber != null)
        //        {
        //            if (operationParameters == null && ExternalInvestmentUnit?.Company != null)
        //                operationParameters = ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvestmentUnit?.Company);

        //            if (operationParameters != null) return operationParameters.IsMarketEditableOperation;
        //            else return false;
        //        }
        //        else
        //            return false;
        //    }
        //}
    }
}

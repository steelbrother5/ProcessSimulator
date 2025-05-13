using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ForeignExchangeOperation : EnterpriseBaseObject /*IQuotable*/
    {
        public ForeignExchangeOperation(Session session) : base(session) { /*Locked = false;*/ }

        private bool _isOwnOperation;
        /// <summary>
        /// Indica si es una operación propia.
        /// </summary>
        //[ImmediatePostData(true)]
        //public bool IsOwnOperation
        //{
        //    get { return _isOwnOperation; }
        //    set
        //    {
        //        if (SetPropertyValue<bool>("IsOwnOperation", ref _isOwnOperation, value) && (!IsSaving) && (!IsLoading))
        //        {
        //            if (_isOwnOperation)
        //            {
        //                ClearingHouse = false;
        //                SetClientOwnOperation();
        //                MarketType = (from marketType in new XPQuery<MarketType>(Session) where marketType.MartketTypeId == enumMarketType.Posicion select marketType).FirstOrDefault();
        //                NegotiationSystem = (from market in new XPQuery<CurrencyMarket>(Session) where market.Name == "Interno" select market).FirstOrDefault();
        //                CurrencyVehicle = (from currencyVehicle in new XPQuery<CurrencyVehicle>(Session) where currencyVehicle.ID == "30" select currencyVehicle).FirstOrDefault();
        //                CustomerAccountDeposit = null;
        //                FixOperation = false;
        //            }
        //            else
        //            {
        //                MarketType = null;
        //                Client = null;
        //                NegotiationSystem = (from market in new XPQuery<CurrencyMarket>(Session) where market.Name == "SetFX" select market).FirstOrDefault();
        //                CurrencyVehicle = null;
        //                BankingConcept = null;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Llena el campo cliente con el cliente asociado a la UEI.
        /// Solo para operaciones propias.
        /// </summary>
        //private void SetClientOwnOperation()
        //{
        //    Clients clientOwnOperation = null;

        //    if (ExternalInvesmentUnit.Company is Company company)
        //    {
        //        Persons legalPerson = company.LegalPerson;
        //        if (legalPerson != null)
        //        {
        //            clientOwnOperation = (from client in new XPQuery<Clients>(Session)
        //                                  where client.Person.Oid == legalPerson.Oid
        //                                  select client).FirstOrDefault();
        //        }
        //    }

        //    Client = clientOwnOperation;
        //}

        //private OperationType _operationType;
        /// <summary>
        /// Tipo de operación (1. Ventas o 2. Compras)
        /// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("OperationTypeDisabled", Enabled = false,
        //    Criteria = "OperationStatus.StatusName != 'Ingresada' OR (TransmissionStatus.Name == 'Transmitida' OR TransmissionStatus.Id == 2)")]
        //public OperationType OperationType
        //{
        //    get
        //    { return _operationType; }
        //    set
        //    {
        //        if (SetPropertyValue("OperationType", ref _operationType, value) && (!IsSaving) && (!IsLoading))
        //        {
        //            if (_operationType != null)
        //            {
        //                // Aldemar Velásquez - Redmine  - Febrero 25/2016
        //                OperationClass = _operationType.Id;
        //                UpdateForeignExchangeOperationProfit();
        //                //Redmine 903 : John J Garcia 12-12-2016
        //                UpdateForeignExchangeOperationAmountUSD();

        //                if (CurrencyOperation != null)
        //                {
        //                    if (CurrencyOperation.Code != null)
        //                    {
        //                        CurrentCurrencyExchangeRate CurrencyNegotiatedRateFx = (from currency in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                      where currency.CurrencyCode.Oid == CurrencyOperation.Oid
        //                                                                                      && currency.ReceivedDate.Date == OperationDate.Date
        //                                                                                      select currency).FirstOrDefault();
        //                        if ((CurrencyNegotiatedRateFx != null) && (OperationType != null))
        //                        {
        //                            if (_operationType.Id == 2) // Compra
        //                            {
        //                                _RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setPurchaseRate;
        //                                _RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setFxRate;
        //                            }
        //                            else if (_operationType.Id == 1) // Venta
        //                            {
        //                                _RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setSaleRate;
        //                                _RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setTRM;
        //                            }

        //                            RateApprovedCurrencyNegotiatedToUSD = _RateSuggestedCurrencyNegotiatedToUSD;
        //                            RateApprovedCurrencyLocalToUSD = _RateSuggestedCurrencyLocalToUSD;
        //                            OnChanged("OperationValueUSD");
        //                            OnChanged("OperationValueCurrencyNegotiated");

        //                            CurrentCurrencyExchangeRate CurrencyLocalToUSDRateFx = (from eiu in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                          where eiu.CurrencyCode.IsLocal == true && eiu.ReceivedDate.Date == OperationDate.Date
        //                                                                                          select eiu).FirstOrDefault();
        //                            if (CurrencyLocalToUSDRateFx != null)
        //                            {
        //                                if (_operationType.Id == 2) // Compra
        //                                {
        //                                    RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setPurchaseRate;
        //                                }
        //                                else if (_operationType.Id == 1) // Venta
        //                                {
        //                                    RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setSaleRate;

        //                                }

        //                                RateApprovedCurrencyLocalToUSD = RateSuggestedCurrencyLocalToUSD;
        //                                OnChanged("OperationValueCurrencyNegotiated");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //                InternalTradeProfit = 0;
        //        }
        //    }
        //}

        //private int _OperationClass;
        ///// <summary>
        ///// 1 Venta, 2 Compra
        ///// </summary>
        //public int OperationClass
        //{
        //    get { return _OperationClass; }
        //    set
        //    {
        //        if (SetPropertyValue<int>("OperationClass", ref _OperationClass, value))
        //        {
        //            if ((_CurrencyOperation != null) && (!IsSaving) && (!IsLoading))
        //            {
        //                if (_CurrencyOperation.Code != null)
        //                {
        //                    CurrentCurrencyExchangeRate CurrencyNegotiatedRateFx = (from eiu in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                  where eiu.CurrencyCode.Oid == _CurrencyOperation.Oid && eiu.ReceivedDate.Date == _OperationDate.Date
        //                                                                                  select eiu).FirstOrDefault();
        //                    if (CurrencyNegotiatedRateFx != null)
        //                    {
        //                        if (_OperationClass == 1) // VENTA        
        //                        {
        //                            RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setSaleRate;
        //                            RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setTRM;
        //                        }
        //                        else if (_OperationClass == 2) // COMPRA
        //                        {
        //                            RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setPurchaseRate;
        //                            RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setFxRate;
        //                        }
        //                        if (_RateApprovedCurrencyNegotiatedToUSD == 0)
        //                        {
        //                            RateApprovedCurrencyNegotiatedToUSD = _RateSuggestedCurrencyNegotiatedToUSD;
        //                            InternalTradeRateCurrencyNegotiatedToUSD = _RateSuggestedCurrencyNegotiatedToUSD;
        //                            OnChanged("OperationValueUSD");
        //                            OnChanged("OperationValueCurrencyNegotiated");

        //                        }
        //                        CurrentCurrencyExchangeRate CurrencyLocalToUSDRateFx = (from eiu in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                      where eiu.CurrencyCode.IsLocal == true && eiu.ReceivedDate.Date == _OperationDate.Date
        //                                                                                      select eiu).FirstOrDefault();
        //                        if (CurrencyLocalToUSDRateFx != null)
        //                        {
        //                            if (_OperationClass == 1) // VENTA
        //                            {
        //                                _RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setSaleRate;

        //                            }
        //                            else if (_OperationClass == 2) // COMPRA
        //                            {
        //                                _RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setPurchaseRate;

        //                            }

        //                            if (_RateApprovedCurrencyLocalToUSD == 0)
        //                            {
        //                                _RateApprovedCurrencyLocalToUSD = _RateSuggestedCurrencyLocalToUSD;
        //                                OnChanged("OperationValueCurrencyNegotiated");

        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private string _OperationNumber;
        /// <summary>
        /// Número de la operación
        /// </summary>
        [Size(100)]
        [Appearance("OperationNumberDisabled", Enabled = false)]
        public string OperationNumber
        {
            get { return _OperationNumber; }
            set { SetPropertyValue<string>("OperationNumber ", ref _OperationNumber, value); }
        }

        private string _operationNumberSetFX;
        /// <summary>
        /// Número de la operación en el sistema SetFX
        /// </summary>
        [Size(100)]
        [Appearance("OperationNumberSetFXDisabled", Enabled = false)]
        public string OperationNumberSetFX
        {
            get { return _operationNumberSetFX; }
            set { SetPropertyValue<string>("OperationNumberSetFX", ref _operationNumberSetFX, value); }
        }

        //private Clients _client;
        ///// <summary>
        ///// Cliente
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[Appearance("ClientDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public Clients Client
        //{
        //    get { return _client; }
        //    set
        //    {
        //        if (SetPropertyValue("Client", ref _client, value) && !IsLoading && !IsSaving)
        //        {
        //            ClientBankAccount = null;
        //            if (Client != null)
        //            {
        //                decimal? ClientBalanceValue = null;
        //                City = Client.City;
        //                MarketSector = Client.MarketSector;
        //                Finac.Cornerstone.Response<decimal?> ClientBalanceResponse = null;

        //                bool IsOyDActive =
        //                    new XPQuery<InterfacePermissionsParameters>(Session)
        //                        .SingleOrDefault(interfacePermissions =>
        //                        interfacePermissions.Interface.Id == (int)EnumInterfaces.OyDSaldos &&
        //                        interfacePermissions.ParameterCode.Id == (int)EnumCodesForInterfaces.GeneralOyD)?
        //                        .IsActive ?? false;

        //                if ((ClassInfo.FullName == "ComEx.Module.BusinessObjects.ForeignExchangeOperationsClasses.ForeignExchangeOperation" ||
        //                    ((ClassInfo.FullName == "ComEx.Module.BusinessObjects.ForeignExchangeOperationsClasses.ForeignExchangePurchaseOperation" ||
        //                    ClassInfo.FullName == "ComEx.Module.BusinessObjects.ForeignExchangeOperationsClasses.ForeignExchangeSalesOperation") &&
        //                    OperationStatus?.StatusName == nameof(enumOperationStatus.Ingresada))) &&
        //                    string.IsNullOrEmpty(Client.IMCCode) &&
        //                    IsOyDActive &&
        //                    !IsFromTransactionalPortal)
        //                {
        //                    ClientBalanceResponse = BalanceManagerOyD.GetClientBalanceInOyD(Client, true, Session);

        //                    if (ClientBalanceResponse?.Value == null)
        //                    {
        //                        ClientBalanceValue = 0;

        //                        PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                                    $"Cliente con novedad, comunicarse con fábrica de clientes.",
        //                                                    "Saldo Cliente/Contraparte",
        //                                                    windowsMessageBoxIcon: MessageBoxIcon.Warning,
        //                                                    webMessageType: InformationType.Warning,
        //                                                    webHeight: 10,
        //                                                    webWidth: 70);
        //                    }

        //                    Log.WriteLog($"Se Consumió el servicio OyD desde el DetailView del Registro de Operaciones");
        //                }
        //                ClientBalance = ClientBalanceValue == 0 ? ClientBalanceValue : ClientBalanceResponse?.Value;
        //                IsGuardedOperation = false;
        //            }
        //            else
        //            {
        //                City = null;
        //                MarketSector = null;
        //                ClientBalance = null;
        //            }
        //        }
        //    }
        //}


        //private decimal? clientBalance;
        //[Appearance("ClientBalanceDisabled", Enabled = false)]
        //public decimal? ClientBalance
        //{
        //    get => clientBalance;
        //    set => SetPropertyValue("ClientBalance", ref clientBalance, value);
        //}

        //private CommunicationMedia communicationMedia;
        ///// <summary>
        ///// Medio de comunicación
        ///// </summary>  
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

        //private string communicationMediaObservations;
        ///// <summary>
        ///// Observaciones Medio de Comunicación
        ///// </summary>        
        //[Appearance("CommunicationMediaObservationsDisabled",
        //            Enabled = false,
        //            Criteria = "CommunicationMedia == null ||" +
        //                       "(CommunicationMedia != null && CommunicationMedia.ApplyObservations == False)")]
        //public string CommunicationMediaObservations
        //{
        //    get => communicationMediaObservations;
        //    set => SetPropertyValue("CommunicationMediaObservations", ref communicationMediaObservations, value);
        //}

        //private DateTime _OperationDate;
        ///// <summary>
        ///// Fecha de la operación
        ///// </summary>
        //[Appearance("OperationDateDisabled", Enabled = false)]
        //public DateTime OperationDate
        //{
        //    get { return _OperationDate; }
        //    set { SetPropertyValue<DateTime>("OperationDate", ref _OperationDate, value); }
        //}

        //private TimeSpan? _OperationTime;
        ///// <summary>
        ///// Hora de la operación
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("OperationTimeDisabled", Enabled = false, Criteria = "OperationStatus.StatusName != 'Ingresada'")]
        //public TimeSpan? OperationTime
        //{
        //    get { return _OperationTime; }
        //    set
        //    {
        //        if (SetPropertyValue("OperationTime", ref _OperationTime, value) && !IsLoading && !IsSaving && GenerateInternalTrade)
        //            InternalOperationTime = OperationTime;
        //    }
        //}

        //private bool isGuardedOperation;
        ///// <summary>
        ///// Vigilado
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("IsGuardedOperationEnabled", Enabled = false,
        //            Criteria = "Client.EntityType.EntityCode != 85 && " +
        //                       "Client.Administrator.EntityType.EntityCode != 85 && " +
        //                       "OperationStatus.StatusName = 'Ingresada'")]
        //public bool IsGuardedOperation
        //{
        //    get => isGuardedOperation;
        //    set => SetPropertyValue(nameof(IsGuardedOperation), ref isGuardedOperation, value);
        //}

        //private TimeSpan? _InternalOperationTime;
        ///// <summary>
        ///// Hora de la operación interna
        ///// </summary>
        //[Appearance("InternalOperationTimeHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalOperationTimeDisabled",
        //            Enabled = false,
        //            Criteria = "OperationStatus.StatusName != 'Ingresada' OR TransmissionStatus.Name == 'Transmitida' OR AssociateExistingInternalTrade")]
        //public TimeSpan? InternalOperationTime
        //{
        //    get { return _InternalOperationTime; }
        //    set { SetPropertyValue("InternalOperationTime", ref _InternalOperationTime, value); }
        //}

        //// JCV 20180802. Redmine 6578(6474) Se adicona el campo calculado "OperationTimeSec" 
        //// para resolver los problemas encontrados con el campo OperationTime por ser de tipo  
        //// TimeSpan en el filtro del XPQuery de los reportes SFC102 y SFC395 .
        //[Appearance("OperationTimeInSecsHide", Visibility = ViewItemVisibility.Hide)]
        //[PersistentAlias("OperationTime")]
        ///// <summary>
        ///// Hora de la operación en segundos.
        ///// </summary>
        //public double OperationTimeInSecs
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("OperationTimeInSecs");
        //        if (tempObject != null)
        //        {
        //            return ((TimeSpan)tempObject).TotalSeconds;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        //private OperationStatus _operationstatus;
        ///// <summary>
        ///// Estado de la operación.
        ///// </summary>
        //[Appearance("OperationStatusDisabled", Enabled = false)]
        //[Indexed("OperationDate;ComplianceDate;CurrencyOperation;ShowNextDayInProfitsAndLosses;ExternalInvesmentUnit;SwiftMessage;Market;CurrencyTrade", Unique = false)]
        //public OperationStatus OperationStatus
        //{
        //    get { return _operationstatus; }
        //    set { SetPropertyValue<OperationStatus>("OperationStatus", ref _operationstatus, value); }
        //}

        //private TransmissionStatus transmissionStatus;
        ///// <summary>
        ///// Estado de la transmision del archivo XML para SetFX
        ///// </summary>
        //[Appearance("TransmissionStatusDisabled", Enabled = false)]
        //public TransmissionStatus TransmissionStatus
        //{
        //    get => transmissionStatus;
        //    set => SetPropertyValue("TransmissionStatus", ref transmissionStatus, value);
        //}

        //private Swift_MT_Message _SwiftMessage;
        ///// <summary>
        ///// Swift Message.
        ///// </summary>
        //[Appearance("SwiftMessageHide", Visibility = ViewItemVisibility.Hide)]
        //public Swift_MT_Message SwiftMessage
        //{
        //    get { return _SwiftMessage; }
        //    set { SetPropertyValue<Swift_MT_Message>("SwiftMessage", ref _SwiftMessage, value); }
        //}

        //private string _Origin;
        ///// <summary>
        ///// Origen
        ///// </summary>        
        //public string Origin
        //{
        //    get { return _Origin; }
        //    set { SetPropertyValue<string>("Origin", ref _Origin, value); }
        //}

        //private Cities _City;
        ///// <summary>
        ///// Identificador de la ciudad del tercero.
        ///// </summary>        
        //public Cities City
        //{
        //    get { return _City; }
        //    set { SetPropertyValue<Cities>("City", ref _City, value); }
        //}

        //private BranchOffice _BranchOffice;
        ///// <summary>
        ///// Sucursal
        ///// </summary>
        //public BranchOffice BranchOffice
        //{
        //    get { return _BranchOffice; }
        //    set { SetPropertyValue<BranchOffice>("BranchOffice", ref _BranchOffice, value); }
        //}

        //private PurposeOfOperation _PurposeofOperation;
        ///// <summary>
        ///// Propósito de la operación
        ///// </summary>
        //public PurposeOfOperation PurposeOfOperation
        //{
        //    get { return _PurposeofOperation; }
        //    set { SetPropertyValue<PurposeOfOperation>("PurposeOfOperation ", ref _PurposeofOperation, value); }
        //}

        //private Currency _CurrencyOperation;
        ///// <summary>
        ///// Moneda de la operación
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[Indexed("ShowNextDayInProfitsAndLosses;ExternalInvesmentUnit;OperationStatus;SwiftMessage;CurrencyTrade", Unique = false)]
        //public Currency CurrencyOperation
        //{
        //    get
        //    { return _CurrencyOperation; }
        //    set
        //    {
        //        if (SetPropertyValue<Currency>("CurrencyOperation", ref _CurrencyOperation, value) && (!IsSaving) && (!IsLoading) && (OperationNumberSetFX == "" || OperationNumberSetFX == null))
        //        {
        //            if (_CurrencyOperation != null && _OperationDate.Date != DateTime.MinValue)
        //            {
        //                if (_CurrencyOperation.Code != null)
        //                {
        //                    IsComplementedTransactionalPortal = false;
        //                    CurrentCurrencyExchangeRate CurrencyNegotiatedRateFx = (from eiu in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                  where eiu.CurrencyCode.Oid.ToString().ToUpper() == _CurrencyOperation.Oid.ToString().ToUpper() &&
        //                                                                                        eiu.ReceivedDate.Date == _OperationDate.Date
        //                                                                                  select eiu).FirstOrDefault();
        //                    if (CurrencyNegotiatedRateFx != null)
        //                    {
        //                        if (_OperationClass == 1) // VENTA
        //                        {
        //                            RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setSaleRate;
        //                            RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setTRM;
        //                        }
        //                        else if (_OperationClass == 2) // COMPRA
        //                        {
        //                            RateSuggestedCurrencyNegotiatedToUSD = CurrencyNegotiatedRateFx.setPurchaseRate;
        //                            RateSuggestedCurrencyLocalToUSD = CurrencyNegotiatedRateFx.setFxRate;
        //                        }

        //                        RateApprovedCurrencyNegotiatedToUSD = _RateSuggestedCurrencyNegotiatedToUSD;
        //                        RateApprovedCurrencyLocalToUSD = _RateSuggestedCurrencyLocalToUSD;
        //                        InternalTradeRateCurrencyNegotiatedToUSD = _RateSuggestedCurrencyNegotiatedToUSD;
        //                        OnChanged("OperationValueUSD");
        //                        OnChanged("OperationValueCurrencyNegotiated");

        //                        CurrentCurrencyExchangeRate CurrencyLocalToUSDRateFx = (from eiu in new XPQuery<CurrentCurrencyExchangeRate>(Session)
        //                                                                                      where eiu.CurrencyCode.IsLocal == true && eiu.ReceivedDate.Date == _OperationDate.Date
        //                                                                                      select eiu).FirstOrDefault();
        //                        if (CurrencyLocalToUSDRateFx != null)
        //                        {
        //                            if (_OperationClass == 1) // VENTA
        //                            {
        //                                RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setSaleRate;
        //                            }
        //                            else if (_OperationClass == 2) // COMPRA
        //                            {
        //                                RateSuggestedCurrencyLocalToUSD = CurrencyLocalToUSDRateFx.setPurchaseRate;
        //                            }
        //                            RateApprovedCurrencyLocalToUSD = _RateSuggestedCurrencyLocalToUSD;
        //                            OnChanged("OperationValueCurrencyNegotiated");
        //                        }
        //                    }

        //                    else
        //                    {
        //                        if (!IsFromTransactionalPortal)
        //                        {
        //                            if (_OperationClass == 1 && !ClientCurrencyDepositPayments.Any()) // VENTA
        //                                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                                            $"No existe Tasa de Negociación Actual registrada para la fecha {_OperationDate.ToShortDateString()}",
        //                                                            "Venta Divisas",
        //                                                            webHeight: 10,
        //                                                            webWidth: 70);
        //                            else if (_OperationClass == 2 && !ClientCurrencyDepositPayments.Any()) // COMPRA
        //                                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                                            $"No existe Tasa de Negociación Actual registrada para la fecha {_OperationDate.ToShortDateString()}",
        //                                                            "Compra Divisas",
        //                                                            webHeight: 10,
        //                                                            webWidth: 70);
        //                        }


        //                        GenerateInternalTrade = false;
        //                        RateSuggestedCurrencyNegotiatedToUSD = 0;
        //                        RateSuggestedCurrencyLocalToUSD = 0;
        //                        if (CurrencyOperation.IsUSDollar)
        //                        {
        //                            RateApprovedCurrencyNegotiatedToUSD = 1;
        //                            InternalTradeRateCurrencyNegotiatedToUSD = 1;
        //                            RateApprovedUSDToCurrencyNegotiated = 1;
        //                            InternalRateApprovedUSDToCurrencyNegotiated = 1;

        //                        }
        //                        else
        //                        {
        //                            RateApprovedCurrencyNegotiatedToUSD = 0;
        //                            InternalTradeRateCurrencyNegotiatedToUSD = 0;
        //                            RateApprovedUSDToCurrencyNegotiated = 0;
        //                            InternalRateApprovedUSDToCurrencyNegotiated = 0;
        //                        }
        //                        RateApprovedCurrencyLocalToUSD = 0;
        //                        InternalTradeRateCurrencyLocalToUSD = 0;
        //                    }
        //                }
        //            }

        //            else
        //            {
        //                GenerateInternalTrade = false;
        //                RateSuggestedCurrencyNegotiatedToUSD = 0;
        //                RateSuggestedCurrencyLocalToUSD = 0;
        //                RateApprovedCurrencyNegotiatedToUSD = 0;
        //                RateApprovedCurrencyLocalToUSD = 0;
        //                InternalTradeRateCurrencyLocalToUSD = 0;
        //                InternalTradeRateCurrencyNegotiatedToUSD = 0;
        //                RateApprovedUSDToCurrencyNegotiated = 0;
        //                InternalRateApprovedUSDToCurrencyNegotiated = 0;
        //                DepositAccount = null;
        //            }
        //        }
        //    }
        //}

        //private decimal _OperationValue;
        ///// <summary>
        ///// Valor de la operación
        ///// </summary>
        //[ImmediatePostData(true)]
        //public decimal OperationValue
        //{
        //    get { return _OperationValue; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("OperationValue", ref _OperationValue, value))
        //        {
        //            // Aldemar Velásquez - Redmine 29186 - Febrero 25/2016  
        //            // Llamado a función de actualización de Utilidad
        //            //Redmine 903 : John J Garcia 12-12-2016
        //            UpdateForeignExchangeOperationAmountUSD();
        //            UpdateForeignExchangeInternalTradeValues("OperationValue");
        //            UpdateForeignExchangeOperationProfit();
        //            OnChanged("OperationValueUSD");
        //            OnChanged("OperationValueCurrencyNegotiated");
        //            //OnChanged("ServiceCharge");
        //            OnChanged("VAT");
        //        };
        //    }
        //}

        //private decimal _OperationValueUSD;
        ///// <summary>
        ///// Valor de la operación en USD
        ///// </summary>        
        //[Appearance("OperationValueUSDEnabled", Enabled = false)]
        //[ImmediatePostData(true)]
        //public decimal OperationValueUSD
        //{
        //    //Redmine 903 : John J Garcia 12-12-2016
        //    get { return _OperationValueUSD; }
        //    set { SetPropertyValue<decimal>("OperationValueUSD", ref _OperationValueUSD, value); }
        //}

        //private decimal _RateSuggestedCurrencyLocalToUSD;
        ///// <summary>
        ///// Tasa sugerida de moneda local a USD
        ///// </summary>
        //[Appearance("RateSuggestedCurrencyLocalToUSDEnabled", Enabled = false)]
        //[DbType("decimal(23,8)")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //public decimal RateSuggestedCurrencyLocalToUSD
        //{
        //    get { return _RateSuggestedCurrencyLocalToUSD; }
        //    set { SetPropertyValue<decimal>("RateSuggestedCurrencyLocalToUSD", ref _RateSuggestedCurrencyLocalToUSD, value); }
        //}

        //private decimal _RateApprovedCurrencyLocalToUSD;
        ///// <summary>
        ///// Tasa aprobada de moneda local a USD
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[DbType("decimal(23,8)")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //public decimal RateApprovedCurrencyLocalToUSD
        //{
        //    get { return _RateApprovedCurrencyLocalToUSD; }
        //    set
        //    {
        //        // Aldemar Velásquez - Redmine 29186 - Febrero 25/2016  
        //        // Llamado a función de actualización de Utilidad

        //        //Redmine 903 : John J Garcia 12-12-2016
        //        //UpdateForeignExchangeOperationAmountUSD();
        //        IsRateApprovedCurrencyLocalToUSDModified = true;
        //        SetPropertyValue<decimal>("RateApprovedCurrencyLocalToUSD", ref _RateApprovedCurrencyLocalToUSD, value);
        //        UpdateForeignExchangeOperationProfit();
        //        UpdateTax4x1000Value();
        //    }
        //}

        ///// <summary>
        ///// Propiedad encargada de informar si la tasa fue alterada
        ///// </summary>
        //[NonPersistent]
        //public bool IsRateApprovedCurrencyLocalToUSDModified { get; set; }

        //private decimal _RateSuggestedCurrencyNegotiatedToUSD;
        ///// <summary>
        ///// Tasa sugerida de moneda negociada a USD
        ///// </summary>
        //[Appearance("RateSuggestedCurrencyNegotiatedToUSDEnabled", Enabled = false)]
        //[DbType("decimal(23,8)")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //public decimal RateSuggestedCurrencyNegotiatedToUSD
        //{
        //    get { return _RateSuggestedCurrencyNegotiatedToUSD; }
        //    set { SetPropertyValue<decimal>("RateSuggestedCurrencyNegotiatedToUSD", ref _RateSuggestedCurrencyNegotiatedToUSD, value); }
        //}

        //private decimal _RateApprovedCurrencyNegotiatedToUSD;
        ///// <summary>
        ///// Tasa aprobada de moneda negociada a USD
        ///// </summary>
        //[Appearance("RateApprovedCurrencyNegotiatedToUSDDisabled", Enabled = false, Criteria = "CurrencyOperation.IsUSDollar")]
        //[ImmediatePostData(true)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal RateApprovedCurrencyNegotiatedToUSD
        //{
        //    get { return _RateApprovedCurrencyNegotiatedToUSD; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("RateApprovedCurrencyNegotiatedToUSD", ref _RateApprovedCurrencyNegotiatedToUSD, value) && !IsLoading && !IsSaving)
        //        {
        //            UpdateForeignExchangeOperationAmountUSD();
        //            if (RateApprovedCurrencyNegotiatedToUSD > 0 && !this.Locked)
        //                RateApprovedUSDToCurrencyNegotiated = Math.Round(1 / RateApprovedCurrencyNegotiatedToUSD, RateRound, MidpointRounding.AwayFromZero);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Tasa Aprobada USD - Moneda Negociada
        ///// </summary>
        //private decimal _rateApprovedUSDToCurrencyNegotiated;
        //[ImmediatePostData(true)]
        //[Appearance("RateApprovedUSDToCurrencyNegotiatedDisabled", Enabled = false, Criteria = "CurrencyOperation.IsUSDollar")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal RateApprovedUSDToCurrencyNegotiated
        //{
        //    get { return _rateApprovedUSDToCurrencyNegotiated; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("RateApprovedUSDToCurrencyNegotiated", ref _rateApprovedUSDToCurrencyNegotiated, value) && !IsLoading && !IsSaving)
        //        {
        //            UpdateForeignExchangeOperationProfit();
        //            UpdateForeignExchangeOperationAmountUSD();
        //            if (RateApprovedUSDToCurrencyNegotiated > 0 && !this.Locked)
        //                RateApprovedCurrencyNegotiatedToUSD = Math.Round(1 / RateApprovedUSDToCurrencyNegotiated, RateRound, MidpointRounding.AwayFromZero);
        //        }
        //    }
        //}

        //private decimal _rateNegotiatedCurrencyToLocal;
        //[Appearance("RateNegotiatedCurrencyToLocalDisabled", Enabled = false)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal RateNegotiatedCurrencyToLocal
        //{
        //    get { return _rateNegotiatedCurrencyToLocal; }
        //    set { SetPropertyValue<decimal>("RateNegotiatedCurrencyToLocal", ref _rateNegotiatedCurrencyToLocal, value); }
        //}

        //private decimal vat;
        ///// <summary>
        ///// IVA Comisión
        ///// </summary>
        //public decimal VAT
        //{
        //    get => vat;
        //    set => SetPropertyValue(nameof(VAT), ref vat, value);
        //}

        //private decimal _VATParameter
        //    => decimal.TryParse(Session.FindObject<ApplicationParameters>(CriteriaOperator.Parse("Property == 'IVA'"))?
        //                               .Value.ToString(), out decimal VATParameterValue) ? VATParameterValue : 0;

        //private Commissions _Commission;
        ///// <summary>
        ///// Comisión
        ///// </summary>       
        //[ImmediatePostData(true)]
        //public Commissions Commission
        //{
        //    get { return _Commission; }
        //    set
        //    {
        //        if (SetPropertyValue("Commission", ref _Commission, value) && !IsLoading && !IsSaving)
        //        {
        //            CommissionQuantity = 0;
        //            ServiceCharge = 0;

        //            if (_Commission?.Currency != null)
        //            {
        //                CommissionExchangeRate = PriceVendorDataManager.getExchangeValue(OperationDate.AddDays(-1), Commission.Currency, Session);
        //                if (CommissionExchangeRate == 0 && !IsFromTransactionalPortal)
        //                    PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                                $"No existe Tasa de Cambio para la moneda {Commission.Currency} a la fecha {OperationDate.AddDays(-1).ToShortDateString()}.",
        //                                                webHeight: 10,
        //                                                webWidth: 70);
        //            }
        //        }
        //    }
        //}

        //private decimal _CommissionQuantity;
        ///// <summary>
        ///// Cantidad de comisión
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[Appearance("CommissionQuantityHide", Visibility = ViewItemVisibility.Hide, Criteria = "Commission == null", Context = "DetailView")]
        //public decimal CommissionQuantity
        //{
        //    get => _CommissionQuantity;
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("CommissionQuantity", ref _CommissionQuantity, value) && CommissionExchangeRate != null && !IsLoading && !IsSaving)
        //            ServiceCharge = CommissionQuantity * (Commission?.CommissionBase ?? 0) * CommissionExchangeRate.Value;
        //    }
        //}

        //private decimal? _CommissionExchangeRate;
        ///// <summary>
        ///// Tasa de Cambio de la modeda de Comisión. 
        ///// </summary>                
        //[VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //public decimal? CommissionExchangeRate
        //{
        //    get => _CommissionExchangeRate;
        //    set => SetPropertyValue<decimal?>("CommissionExchangeRate", ref _CommissionExchangeRate, value);
        //}

        //private decimal serviceCharge;
        ///// <summary>
        ///// Total comisión
        ///// </summary>
        //[ImmediatePostData(true)]
        //public decimal ServiceCharge
        //{
        //    get => serviceCharge;
        //    set
        //    {
        //        if (SetPropertyValue(nameof(ServiceCharge), ref serviceCharge, value) && !IsLoading && !IsSaving)
        //        {
        //            VAT = ServiceCharge * (_VATParameter / 100);
        //            UpdateTax4x1000Value();

        //            if (CommissionExchangeRate != null && Commission != null && Commission.CommissionBase != 0)
        //                CommissionQuantity = (Commission.CommissionBase * CommissionExchangeRate.Value != 0) ?
        //                    ServiceCharge / (Commission.CommissionBase * CommissionExchangeRate.Value) : 0;
        //        }
        //    }
        //}

        //[PersistentAlias("iif(RateApprovedCurrencyNegotiatedToUSD == 0, " +
        //                        "0, " +
        //                        "iif(OperationClass == 1, " +
        //                                "OperationValueUSD * RateApprovedCurrencyLocalToUSD + VAT + ServiceCharge, " +
        //                                "OperationValueUSD * RateApprovedCurrencyLocalToUSD - VAT - ServiceCharge))")]
        ///// <summary>
        ///// Valor de la operación de la moneda negociada
        ///// </summary>
        //public decimal OperationValueCurrencyNegotiated
        //    => Math.Round(Convert.ToDecimal(EvaluateAlias(nameof(OperationValueCurrencyNegotiated))), AmountRound, MidpointRounding.AwayFromZero);

        //private decimal subtotalTradingValueCurrencyNegotiated;

        ///// <summary>
        ///// Subutotal de la operación de la moneda negociada
        ///// </summary>
        //[PersistentAlias("OperationValueUSD * RateApprovedCurrencyLocalToUSD")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //[ImmediatePostData(true)]
        //public decimal SubtotalTradingValueCurrencyNegotiated
        //{
        //    get => Math.Round(Convert.ToDecimal(EvaluateAlias(nameof(SubtotalTradingValueCurrencyNegotiated))), AmountRound, MidpointRounding.AwayFromZero);
        //    set
        //    {
        //        if (SetPropertyValue(nameof(SubtotalTradingValueCurrencyNegotiated), ref subtotalTradingValueCurrencyNegotiated, value)
        //            && FixType != null && FixType?.Id == 2 && RateApprovedCurrencyLocalToUSD != 0 && subtotalTradingValueCurrencyNegotiated != 0 && !IsLoading && !IsSaving)
        //        {
        //            OperationValue = subtotalTradingValueCurrencyNegotiated / RateApprovedCurrencyLocalToUSD;
        //        }
        //    }
        //}

        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[PersistentAlias("OperationValueCurrencyNegotiated*1")]
        ///// <summary>
        ///// Tasa todo costo
        ///// </summary>
        //public decimal RateAllCosts
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("RateAllCosts");
        //        if (tempObject != null)
        //        {
        //            if (OperationValueCurrencyNegotiated == 0)
        //            {
        //                return 0;
        //            }
        //            else
        //            {
        //                if (OperationValueUSD != 0)
        //                {
        //                    // Redmine 2562 (2428) : John J Garcia 12-MAY-2017	
        //                    return Math.Round(OperationValueCurrencyNegotiated / OperationValueUSD, RateRound, MidpointRounding.AwayFromZero);
        //                }
        //                else
        //                {
        //                    return 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        //private Int64 _autorizationNumber;
        ///// <summary>
        ///// Número de autorización
        ///// </summary>
        //public Int64 AutorizationNumber
        //{
        //    get
        //    { return _autorizationNumber; }
        //    set
        //    { SetPropertyValue<Int64>("AutorizationNumber ", ref _autorizationNumber, value); }
        //}

        //private Trader trader;
        ///// <summary>
        ///// Trader
        ///// </summary>    
        //[Appearance("TraderDisabled", Enabled = false)]
        //public Trader Trader
        //{
        //    get => trader;
        //    set => SetPropertyValue("Trader", ref trader, value);
        //}

        //private CurrencyVehicle currencyVehicle;
        ///// <summary>
        ///// Tipo de pago (Forma de Pago)
        ///// </summary>        
        //[Appearance("CurrencyVehicleDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public CurrencyVehicle CurrencyVehicle
        //{
        //    get { return currencyVehicle; }
        //    set { SetPropertyValue<CurrencyVehicle>("CurrencyVehicle", ref currencyVehicle, value); }
        //}

        //private CurrencyMarket negotiationsystem;
        ///// <summary>
        ///// Sistema de Negociación
        ///// </summary>        
        //[Appearance("NegotiationSystemDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public CurrencyMarket NegotiationSystem
        //{
        //    get { return negotiationsystem; }
        //    set { SetPropertyValue<CurrencyMarket>("NegotiationSystem", ref negotiationsystem, value); }
        //}

        //private CurrencyMarket negotiationSystemRegistered;
        ///// <summary>
        ///// Sistema de Negociación Operación Registrada
        ///// </summary>        
        //[Appearance("NegotiationSystemRegisteredDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public CurrencyMarket NegotiationSystemRegistered
        //{
        //    get { return negotiationSystemRegistered; }
        //    set { SetPropertyValue<CurrencyMarket>("NegotiationSystemRegistered", ref negotiationSystemRegistered, value); }
        //}

        //private bool extemporaneousBankAssignment;
        ///// <summary>
        ///// Asignación Extemporánea de Banco
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("ExtemporaneousBankAssignmentDisabled", Enabled = false, Criteria = "IsNullOrEmpty(CurrencyOperation) ||" +
        //                                                                                "ClientCurrencyDepositPayments.Count != 0 ||" +
        //                                                                                "ClearingHouse == true || CrossOperation == true")]
        //public bool ExtemporaneousBankAssignment
        //{
        //    get => extemporaneousBankAssignment;
        //    set
        //    {
        //        if (SetPropertyValue("ExtemporaneousBankAssignment", ref extemporaneousBankAssignment, value) &&
        //            !IsSaving && !IsLoading && ExtemporaneousBankAssignment && extemporaneousBankAssignmentDate == null)
        //        {
        //            PaymentMethod = null;
        //            DepositAccount = null;
        //        }
        //    }
        //}

        //private DateTime? extemporaneousBankAssignmentDateFixed;
        ///// <summary>
        ///// Fecha de Asignación extemporánea de Banco fijada cuando se asigna el banco
        ///// </summary>
        //[Persistent("ExtempBankAssigDateFixed")]
        //[Appearance("ExtempBankAssigDateFixedHide", Visibility = ViewItemVisibility.Hide)]
        //public DateTime? ExtemporaneousBankAssignmentDateFixed
        //{
        //    get => extemporaneousBankAssignmentDateFixed;
        //    set => SetPropertyValue(nameof(ExtemporaneousBankAssignmentDateFixed), ref extemporaneousBankAssignmentDateFixed, value);
        //}

        //private DateTime? extemporaneousBankAssignmentDate;
        ///// <summary>
        ///// Fecha de Asignación Extemporánea de Banco
        ///// </summary>
        //public DateTime? ExtemporaneousBankAssignmentDate
        //{
        //    get => extemporaneousBankAssignmentDate;
        //    set => SetPropertyValue("ExtemporaneousBankAssignmentDate", ref extemporaneousBankAssignmentDate, value);
        //}

        //private CurrencyVehicle paymentmethod;
        ///// <summary>
        ///// Forma de Pago
        ///// </summary>
        //[Appearance("PaymentMethodDisabled", Enabled = false, Criteria = "ClearingHouse == true || ExtemporaneousBankAssignment")]
        //public CurrencyVehicle PaymentMethod
        //{
        //    get { return paymentmethod; }
        //    set { SetPropertyValue<CurrencyVehicle>("PaymentMethod", ref paymentmethod, value); }
        //}

        //private MarketSector marketsector;
        ///// <summary>
        ///// Sector del mercado
        ///// </summary>        
        //[ImmediatePostData(true)]
        //public MarketSector MarketSector
        //{
        //    get { return marketsector; }
        //    set { SetPropertyValue<MarketSector>("MarketSector", ref marketsector, value); }
        //}

        //private DateTime complianceDate;
        ///// <summary>
        ///// Fecha de cumplimiento
        ///// </summary>
        //[Appearance("ComplianceDateEnabled", Enabled = true, Criteria = "market.days == 366")]
        //[Appearance("ComplianceDateDisabled", Enabled = false, Criteria = "market.days != 366")]
        //[Indexed("OperationStatus;ExternalInvesmentUnit", Unique = false)]
        //public DateTime ComplianceDate
        //{
        //    get { return complianceDate; }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("ComplianceDate", ref complianceDate, value);
        //        OnChanged("DepositAccount");
        //    }
        //}

        //private AgreedTermTypes market;
        ///// <summary>
        ///// Tipo de plazo pactado
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Indexed("ComplianceDate;OperationStatus;ExternalInvesmentUnit", Unique = false)]
        //[Appearance("MarketEnabled", Criteria = "IsMarketModifiable", Enabled = false)]
        //public AgreedTermTypes Market
        //{
        //    get { return market; }
        //    set
        //    {
        //        if (SetPropertyValue<AgreedTermTypes>("Market", ref market, value) && (!IsSaving) && (!IsLoading))
        //        {
        //            if (OperationNumberSetFX == null || OperationNumberSetFX == "")
        //                complianceDate = DateTime.MinValue;
        //            if (market != null && OperationStatus != null && OperationStatus.StatusName != "Aprobada")
        //            {
        //                if (market.AgreedTerm != "Plazo")
        //                {
        //                    if (OperationDate != null)
        //                    {
        //                        if (complianceDate == null || complianceDate.Date == DateTime.MinValue)
        //                        {
        //                            string CountryCode = ApplicationParametersManager.GetApplicationParameterValue("CodigoPais", Session);
        //                            Countries Country = new XPQuery<Countries>(Session).FirstOrDefault(country => country.Code == CountryCode);
        //                            if (market.days > 0)
        //                            {
        //                                ComplianceDate = BusinessDaysClass.AddBussinessDaysByCountries(market.days, OperationDate, Session, Country);
        //                                IsComplementedTransactionalPortal = false;
        //                            }
        //                            else if (market.days == 0)
        //                            {
        //                                ComplianceDate = BusinessDaysClass.AddBussinessDaysByCountries(market.days, OperationDate, Session, Country); ;
        //                                ShowNextDayInProfitsAndLosses = false;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    ComplianceDate = new DateTime();
        //                }
        //            }
        //        };
        //    }
        //}

        //private DateTime validityDate;
        ///// <summary>
        ///// Fecha de validación
        ///// </summary>
        //[Appearance("ValidityDateHide", Visibility = ViewItemVisibility.Hide)]
        //public DateTime ValidityDate
        //{
        //    get { return validityDate; }
        //    set { SetPropertyValue<DateTime>("ValidityDate", ref validityDate, value); }
        //}

        //private Portfolio portfolio;
        ///// <summary>
        ///// Portafolio
        ///// </summary>
        //[DataSourceProperty(nameof(AvailablePortfolios))]
        //[Appearance("PortFolioDisabled", Enabled = false, Criteria = "AvailablePortfolios.Count() <= 1")]
        //public Portfolio PortFolio
        //{
        //    get { return portfolio; }
        //    set { SetPropertyValue("Portfolio", ref portfolio, value); }
        //}

        //private XPCollection<Portfolio> availablePortfolios;
        ///// <summary>
        ///// Colección fuente de datos para la propiedad Portfolio.
        ///// </summary>
        //[Browsable(false)]
        //public XPCollection<Portfolio> AvailablePortfolios
        //{
        //    get => availablePortfolios;
        //    set
        //    {
        //        if (SetPropertyValue("AvailablePortfolios", ref availablePortfolios, value) && !IsSaving && !IsLoading)
        //        {
        //            _OperationParameters = ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company);
        //            if (PortFolio == null && _OperationParameters?.Portfolio != null && AvailablePortfolios.Contains(_OperationParameters.Portfolio))
        //                PortFolio = _OperationParameters.Portfolio;
        //            else if (AvailablePortfolios.Count == 1)
        //                PortFolio = AvailablePortfolios.FirstOrDefault();
        //            else if (!AvailablePortfolios.Contains(PortFolio))
        //                PortFolio = null;
        //        }
        //        else
        //            PortFolio = null;
        //    }
        //}

        //private ExternalInvestmentUnit externalInvesmentUnit;
        ///// <summary>
        ///// Unidad Externa de Inversión
        ///// </summary>
        //[ImmediatePostData(true)]
        //public ExternalInvestmentUnit ExternalInvesmentUnit
        //{
        //    get { return externalInvesmentUnit; }
        //    set
        //    {
        //        if (SetPropertyValue("ExternalInvesmentUnit", ref externalInvesmentUnit, value) && (!IsSaving) && (!IsLoading))
        //        {
        //            if (externalInvesmentUnit != null)
        //            {
        //                if (externalInvesmentUnit.Name != null &&
        //                    OperationStatus != null &&
        //                    OperationStatus.StatusName != null &&
        //                    OperationStatus.StatusName != "Aprobada" &&
        //                    !IsSaving)
        //                {
        //                    DateTime processDate = GetClosingDateClass.FindClosingDate(Session, externalInvesmentUnit, 1);
        //                    OperationDate = processDate;
        //                    ValidityDate = processDate;
        //                    // Aldemar Velásquez - Redmine 29101 - Febrero 18/2016
        //                    // Consulta de Parámetros de Registro Operaciones
        //                    if (externalInvesmentUnit.Company != null)
        //                    {
        //                        _OperationParameters =
        //                            ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, externalInvesmentUnit.Company);
        //                        if (_OperationParameters != null)
        //                        {
        //                            MarketType = _OperationParameters.MarketType;
        //                            NegotiationSystem = _OperationParameters.NegotiationSystem;
        //                            NegotiationSystemRegistered = _OperationParameters.NegotiationSystemRegistered;
        //                            CurrencyOperation = _OperationParameters.Currency;
        //                            if (_OperationParameters.AutofillHour)
        //                                OperationTime = DateTime.Now.TimeOfDay;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                PortFolio = null;
        //                MarketType = null;
        //                NegotiationSystem = null;
        //                NegotiationSystemRegistered = null;
        //                CurrencyOperation = null;
        //            }
        //            if (IsOwnOperation)
        //            {
        //                SetClientOwnOperation();
        //            }
        //        }
        //    }
        //}

        //private string observations;
        ///// <summary>
        ///// Observaciones
        ///// </summary>
        //[Size(500)]
        //public string Observations
        //{
        //    get { return observations; }
        //    set { SetPropertyValue<string>("Observations", ref observations, value); }
        //}

        //[Association("ForeignExchangeOperation-FXOpers_ExcDecs",
        //             typeof(ForeignExchangeOperation_Declaration)),
        // Aggregated]
        //public XPCollection<ForeignExchangeOperation_Declaration> ExchangeDeclarations
        //    => GetCollection<ForeignExchangeOperation_Declaration>("ExchangeDeclarations");

        //private bool withoutExchangeDeclarations;
        ///// <summary>
        ///// Sin declaración de cambio
        ///// </summary>        
        //[Appearance("WithoutExchangeDeclarationsHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //[Appearance("WithoutExchangeDeclarationsDisabled", Enabled = false)]
        //public bool WithoutExchangeDeclarations
        //{
        //    get => withoutExchangeDeclarations;
        //    set => SetPropertyValue<bool>("WithoutExchangeDeclarations", ref withoutExchangeDeclarations, value);
        //}

        //private DateTime? extemporaneousDate;
        ///// <summary>
        ///// Fecha extemporánea
        ///// </summary>
        //[Appearance("ExtemporaneousDateHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //[Appearance("ExtemporaneousDateDisabled", Enabled = false)]
        //public DateTime? ExtemporaneousDate
        //{
        //    get => extemporaneousDate;
        //    set => SetPropertyValue<DateTime?>("ExtemporaneousDate", ref extemporaneousDate, value);
        //}

        //private QuotaApprovalStatus quotaApprovalStatus;
        //[Appearance("QuotaApprovalStatusDisabled", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //public QuotaApprovalStatus QuotaApprovalStatus
        //{
        //    get { return quotaApprovalStatus; }
        //    set { SetPropertyValue<QuotaApprovalStatus>("QuotaApprovalStatus", ref quotaApprovalStatus, value); }
        //}

        //private QuotaValidationStatus quotaValidationStatus;
        //[Appearance("QuotaValidationStatusDisabled", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //public QuotaValidationStatus QuotaValidationStatus
        //{
        //    get { return quotaValidationStatus; }
        //    set { SetPropertyValue<QuotaValidationStatus>("QuotaValidationStatus", ref quotaValidationStatus, value); }
        //}

        //private AcceptedRateStatus acceptedRateStatus;

        //[Appearance("AcceptedRateStatusDisabled", Visibility = ViewItemVisibility.Hide)]
        //public AcceptedRateStatus AcceptedRateStatus
        //{
        //    get { return acceptedRateStatus; }
        //    set { SetPropertyValue<AcceptedRateStatus>("AcceptedRateStatus", ref acceptedRateStatus, value); }
        //}

        //private ComplementedStatus complementedStatus;

        //[Appearance("ComplementedStatusDisabled", Enabled = false)]
        //public ComplementedStatus ComplementedStatus
        //{
        //    get { return complementedStatus; }
        //    set { SetPropertyValue<ComplementedStatus>("ComplementedStatus", ref complementedStatus, value); }
        //}

        //private DepositAccount depositAccount;
        //[ImmediatePostData(true)]
        //[Appearance("DepositAccountDisabled", Enabled = false, Criteria = "IsNullOrEmpty(CurrencyOperation) || ClearingHouse == true ||" +
        //                                                                  "CrossOperation == true || ExtemporaneousBankAssignment")]
        //[DataSourceCriteria("Currency = '@This.CurrencyOperation'")]
        //public DepositAccount DepositAccount
        //{
        //    get { return depositAccount; }
        //    set
        //    {
        //        if (SetPropertyValue<DepositAccount>("DepositAccount", ref depositAccount, value) && !IsSaving && !IsLoading)
        //        {
        //            //complianceDate = DateTime.MinValue;
        //            if (market != null && OperationStatus != null && OperationStatus.StatusName != "Aprobada")
        //            {
        //                if (market.AgreedTerm != "Plazo")
        //                {
        //                    if (OperationDate != null && DepositAccount != null)
        //                    {
        //                        if (complianceDate == null || complianceDate.Date == DateTime.MinValue)
        //                        {
        //                            if (market.days > 0)
        //                            {
        //                                ComplianceDate = BusinessDaysClass.AddBusinessDaysCountry(market.days, OperationDate, Session, DepositAccount.Country);
        //                            }
        //                            else
        //                            {
        //                                if (market.days == 0)
        //                                {
        //                                    ComplianceDate = OperationDate;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //ComplianceDate = new DateTime();
        //                }
        //            }
        //        }
        //    }
        //}

        //private bool _clearingHouse;
        ///// <summary>
        ///// Compensado por Cámara
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("ClearingHouseDisabled", Enabled = false, Criteria = "MarketType == 'Distribución'")]
        //[Appearance("ClearingHouseDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public bool ClearingHouse
        //{
        //    get { return _clearingHouse; }
        //    set
        //    {
        //        if (SetPropertyValue<bool>("ClearingHouse", ref _clearingHouse, value) && !IsSaving && !IsLoading && _clearingHouse == true)
        //        {
        //            DepositAccount = null;
        //            PaymentMethod = (from item in new XPQuery<CurrencyVehicle>(Session) where item.ID == "30" select item).FirstOrDefault();
        //        }
        //        OnChanged("MarketType");
        //    }
        //}

        //private MarketType _marketType;
        ///// <summary>
        ///// Posición o Distribución
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("MarketTypeDisabled", Enabled = false,
        //    Criteria = "OperationStatus.StatusName != 'Ingresada' OR (TransmissionStatus.Name == 'Transmitida' OR TransmissionStatus.Id == 2)")]
        //[Appearance("MarketTypeDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //public MarketType MarketType
        //{
        //    get { return _marketType; }
        //    set
        //    {
        //        if (SetPropertyValue("MarketType", ref _marketType, value) && !IsSaving && !IsLoading && _marketType != null)
        //        {
        //            if (_marketType.MartketTypeId == enumMarketType.Distribucion) ClearingHouse = false;
        //            AssociateExistingInternalTrade = _IsMandatoryInternalTrade;
        //        }
        //    }
        //}

        //private bool _ShowNextDayInProfitsAndLosses;
        ///// <summary>
        ///// Mostrar los NextDay el dia de la operación en el PyG.
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("ShowNextDayInProfitsAndLossesDisabled1", Enabled = false, Criteria = "Market.days == 0 || IsNullOrEmpty(Market)")]
        //[Appearance("ShowNextDayInProfitsAndLossesEnabled", Enabled = true, Criteria = "Market.days > 0")]
        //public bool ShowNextDayInProfitsAndLosses
        //{
        //    get { return _ShowNextDayInProfitsAndLosses; }
        //    set
        //    {
        //        SetPropertyValue<bool>("ShowNextDayInProfitsAndLosses", ref _ShowNextDayInProfitsAndLosses, value);
        //    }
        //}

        ///// <summary>
        ///// Tipo Operación.
        ///// </summary>
        //[PersistentAlias("OperationClass")]
        //[Appearance("OperationClassNameShow", Visibility = ViewItemVisibility.Show, Context = "ForeingExchangeNextDayOperation_ListView")]
        //public string OperationClassName
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("OperationClassName");
        //        if (tempObject != null)
        //        {
        //            if ((int)tempObject == 1)
        //                return "Venta";
        //            else
        //                return "Compra";
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}

        //private string customerAccountDeposit;
        ///// <summary>
        ///// Cuenta Depósito o Bancaria de Cliente
        ///// </summary>
        ///// Aldemar Velásquez - Redmine 27187 - Septiembre 8/2015        
        //[Appearance("CustomerAccountDepositDisabledByOwnOper", Enabled = false, Criteria = "IsOwnOperation")]
        //[ImmediatePostData(true)]
        //[Appearance("CustomerAccountDepositDisabled", Enabled = false, Criteria = "!IsNullOrEmpty(ClientBankAccount) || BankingConcept.Concept == 'Crédito Cliente'")]
        //public string CustomerAccountDeposit
        //{
        //    get { return customerAccountDeposit; }
        //    set
        //    {
        //        if (SetPropertyValue<string>("CustomerAccountDeposit", ref customerAccountDeposit, value) && !IsLoading && !IsSaving)
        //            ClientBankAccount = null;
        //    }
        //}

        //private bool isFMTApplied = true;
        ///// <summary>
        ///// Aplica Impuesto 4 * 1000
        ///// FMT: Financial Movement Tax (Gravamen al Movimiento Financiero)
        ///// </summary>
        //[ImmediatePostData(true)]
        //[Appearance("IsFMTAppliedHide", Visibility = ViewItemVisibility.Hide, Criteria = "_operationType == null || _operationType.Id != 1")]
        //public bool IsFMTApplied
        //{
        //    get => isFMTApplied;
        //    set
        //    {
        //        if (SetPropertyValue(nameof(IsFMTApplied), ref isFMTApplied, value) && !IsLoading && !IsSaving) UpdateTax4x1000Value();
        //    }
        //}

        ///// <summary>
        ///// Monto impuesto 4 * 1000
        ///// </summary>
        //private decimal tax4x1000;
        //[Appearance("Tax4x1000Hide", Visibility = ViewItemVisibility.Hide,
        //                             Criteria = "_operationType == null || _operationType.Id != 1",
        //                             Context = "ForeignExchangeOperationApprove_DetailView;ForeignExchangeOperation_DetailView")]
        //public decimal Tax4x1000
        //{
        //    get => tax4x1000;
        //    set => SetPropertyValue(nameof(Tax4x1000), ref tax4x1000, value);
        //}

        ///// <summary>
        ///// Monto Total operacion mas impuesto 4 * 1000
        ///// </summary>
        //[PersistentAlias("iif(OperationType is not null && OperationType.Id == 1 && OperationValueCurrencyNegotiated <> 0, " +
        //                        "OperationValueCurrencyNegotiated + Tax4x1000, 0)")]
        //[Appearance("OperationValueTax4x1000Hide", Visibility = ViewItemVisibility.Hide,
        //                                           Criteria = "_operationType.Id != 1 || IsNullOrEmpty(_operationType)",
        //                                           Context = "ForeignExchangeOperationApprove_DetailView;ForeignExchangeOperation_DetailView")]
        //[Appearance("OperationValueTax4x1000Show", Visibility = ViewItemVisibility.Show,
        //                                           Criteria = "_operationType.Id == 1",
        //                                           Context = "ForeignExchangeOperationApprove_DetailView;ForeignExchangeOperation_DetailView")]
        //public decimal OperationValueTax4x1000
        //    => Convert.ToDecimal(EvaluateAlias(nameof(OperationValueTax4x1000)));

        //private bool isUndoApprove;
        //[Appearance("IsUndoApproveHide", Visibility = ViewItemVisibility.Hide)]
        //public bool IsUndoApprove
        //{
        //    get { return isUndoApprove; }
        //    set { SetPropertyValue<bool>("IsUndoApprove", ref isUndoApprove, value); }
        //}

        //private CurrencyTrade currencyTrade;

        //[Appearance("CurrencyTradeHide", Visibility = ViewItemVisibility.Hide)]
        //public CurrencyTrade CurrencyTrade
        //{
        //    get { return currencyTrade; }
        //    set { SetPropertyValue<CurrencyTrade>("CurrencyTrade", ref currencyTrade, value); }
        //}

        //private string _userThatAnnuled;
        //public string UserThatAnnuled
        //{
        //    get { return _userThatAnnuled; }
        //    set { SetPropertyValue<string>("UserThatAnnuled", ref _userThatAnnuled, value); }
        //}

        //private string _annulObservations;
        //public string AnnulObservations
        //{
        //    get { return _annulObservations; }
        //    set { SetPropertyValue<string>("AnnulObservations", ref _annulObservations, value); }
        //}

        //private string withOutXMLObservations;
        ///// <summary>
        ///// Motivos de aprobacion sin XML.
        ///// </summary>
        //[Appearance("WithOutXMLObservationsHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //[Appearance("WithOutXMLObservationsDisabled", Enabled = false)]
        //public string WithOutXMLObservations
        //{
        //    get => withOutXMLObservations;
        //    set => SetPropertyValue("WithOutXMLObservations", ref withOutXMLObservations, value);
        //}

        //private string errortransmitting;
        ///// <summary>
        ///// Mensajes de error enviados por SetFX por medio de log enviado al momento
        ///// de transmitir una operación
        ///// </summary>
        //[Size(1000)]
        //[Appearance("ErrortransmittingHide", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        //[Appearance("ErrortransmittingDisabled", Enabled = false)]
        //public string Errortransmitting
        //{
        //    get => errortransmitting;
        //    set => SetPropertyValue("Errortransmitting", ref errortransmitting, value);
        //}

        //private DateTime _annulmentDate;
        ///// <summary>
        ///// Fecha y Hora de Anulación
        ///// </summary>        
        //public DateTime AnnulmentDate
        //{
        //    get { return _annulmentDate; }
        //    set { SetPropertyValue<DateTime>("AnnulmentDate", ref _annulmentDate, value); }
        //}

        //private bool _generateInternalTrade;
        //[ImmediatePostData(true)]
        //[Appearance("GenerateInternalTradeDisabled",
        //            Enabled = false,
        //            Criteria = @"CurrencyOperation == null || RateApprovedCurrencyLocalToUSD == 0 || RateApprovedCurrencyNegotiatedToUSD == 0 || 
        //                       OperationValueUSD == 0 || SubtotalTradingValueCurrencyNegotiated == 0 || OperationType == null")]
        //[Appearance("GenerateInternalTradeDisabledFromParameters",
        //            Enabled = false,
        //            Criteria = "!EnableGenerateInternalTradeCheckBox")]
        //public bool GenerateInternalTrade
        //{
        //    get { return _generateInternalTrade; }
        //    set
        //    {
        //        if (SetPropertyValue<bool>("GenerateInternalTrade", ref _generateInternalTrade, value) && !IsLoading && !IsSaving)
        //        {
        //            if (!GenerateInternalTrade)
        //                UpdateForeignExchangeOperationProfit(isRemovingInternalTrade: true);
        //            else if (GenerateInternalTrade)
        //            {
        //                AssociateExistingInternalTrade = false;
        //                if (ShowAllInternalTrade)
        //                {
        //                    InternalTradeMarket = (from market in new XPQuery<CurrencyMarket>(Session) where market.Name == "Interno" select market).FirstOrDefault();
        //                    InternalRateApprovedUSDToCurrencyNegotiated = RateApprovedUSDToCurrencyNegotiated;
        //                    InternalTradeRateCurrencyLocalToUSD = RateApprovedCurrencyLocalToUSD;
        //                    InternalTradeRateCurrencyNegotiatedToUSD = RateApprovedCurrencyNegotiatedToUSD;
        //                    InternalTradeUSDAmount = OperationValueUSD;
        //                    InternalTradeLocalAmount = SubtotalTradingValueCurrencyNegotiated;
        //                    if (InternalTradePortfolio == null && _OperationParameters != null)
        //                        InternalTradePortfolio = _OperationParameters.InternalTradePortfolio;
        //                    if (InternalTradeTrader == null && _OperationParameters != null)
        //                        InternalTradeTrader = _OperationParameters.InternalTradeTrader;
        //                    if (OperationTime != DateTime.MinValue.TimeOfDay && OperationStatus?.StatusName == "Ingresada")
        //                        InternalOperationTime = OperationTime;
        //                }
        //            }
        //        }
        //    }
        //}

        //private bool _IsMandatoryInternalTrade
        //    => ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company)?
        //       .MarketTypesMandatoryInternalTrade.Contains(_marketType) ?? false;

        //private bool associateExistingInternalTrade;
        //[ImmediatePostData(true)]
        //[Appearance("AssociateExistingInternalOperationDisabled",
        //             Enabled = false,
        //             Criteria = @"CurrencyOperation == null || RateApprovedCurrencyLocalToUSD == 0 || 
        //                          RateApprovedCurrencyNegotiatedToUSD == 0 || OperationValueUSD == 0 || 
        //                          SubtotalTradingValueCurrencyNegotiated == 0 || OperationType == null ||
        //                          (OperationStatus.StatusName == 'Aprobada' && GenerateInternalTrade)")]
        //public bool AssociateExistingInternalTrade
        //{
        //    get => associateExistingInternalTrade;
        //    set
        //    {
        //        if (SetPropertyValue(nameof(AssociateExistingInternalTrade), ref associateExistingInternalTrade, value) && !IsLoading && !IsSaving)
        //        {
        //            if (AssociateExistingInternalTrade)
        //                GenerateInternalTrade = false;
        //            UpdateForeignExchangeOperationProfit(isRemovingInternalTrade: true, associateExistingInternalTradeChange: true);
        //        }
        //    }
        //}

        //private bool isUnlinkingInterntalTrade;
        //[Appearance("IsUnlinkingInterntalTradeHide", Visibility = ViewItemVisibility.Hide)]
        //[NonPersistent]
        //public bool IsUnlinkingInterntalTrade
        //{
        //    get => isUnlinkingInterntalTrade;
        //    set => SetPropertyValue(nameof(IsUnlinkingInterntalTrade), ref isUnlinkingInterntalTrade, value);
        //}

        //private Portfolio internalTradePortfolio;
        //[Appearance("internalTradePortfolioHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradePortfolioDisabled", Enabled = false, Criteria = "AssociateExistingInternalTrade")]
        //[DataSourceProperty("InternalTradeTrader.SelectedPortfolios", DataSourcePropertyIsNullMode.SelectAll)]
        //public Portfolio InternalTradePortfolio
        //{
        //    get { return internalTradePortfolio; }
        //    set { SetPropertyValue("InternalTradePortfolio", ref internalTradePortfolio, value); }
        //}

        //private Trader internalTradeTrader;
        //[Appearance("InternalTradeTraderHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeTraderDisabled", Enabled = false, Criteria = "AssociateExistingInternalTrade")]
        //[DataSourceProperty("InternalTradePortfolio.Traders", DataSourcePropertyIsNullMode.SelectAll)]
        //public Trader InternalTradeTrader
        //{
        //    get { return internalTradeTrader; }
        //    set { SetPropertyValue("InternalTradeTrader", ref internalTradeTrader, value); }
        //}

        //private string internalTradeID;
        ///// <summary>
        ///// ID Operación Interna
        ///// </summary>
        //[Appearance("InternalTradeIDHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "!AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeIDDisabled", Enabled = false)]
        //public string InternalTradeID
        //{
        //    get => internalTradeID;
        //    set => SetPropertyValue(nameof(InternalTradeID), ref internalTradeID, value);
        //}

        //private CurrencyMarket internalTradeMarket;
        //[Appearance("InternalTradeMarketHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "!GenerateInternalTrade || !ShowAllInternalTrade")]
        //[Appearance("InternalTradeMarketDisabled", Enabled = false)]
        //public CurrencyMarket InternalTradeMarket
        //{
        //    get { return internalTradeMarket; }
        //    set { SetPropertyValue("InternalTradeMarket", ref internalTradeMarket, value); }
        //}

        //private CommunicationMedia internalCommunicationMedia;
        ///// <summary>
        ///// Medio de comunicación Operación interna
        ///// </summary>      
        //[ImmediatePostData(true)]
        //[Appearance("InternalCommunicationMediaHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "!GenerateInternalTrade && !AssociateExistingInternalTrade")]
        //public CommunicationMedia InternalCommunicationMedia
        //{
        //    get { return internalCommunicationMedia; }
        //    set
        //    {
        //        if (SetPropertyValue("InternalCommunicationMedia", ref internalCommunicationMedia, value) && !IsLoading && !IsSaving)
        //            InternalCommunicationMediaObservations = null;
        //    }
        //}

        //private string internalCommunicationMediaObservations;
        ///// <summary>
        ///// Observaciones Medio de Comunicación Operación Interna
        ///// </summary>
        //[Appearance("InternalCommunicationMediaObservationsHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "!GenerateInternalTrade && !AssociateExistingInternalTrade")]
        //[Appearance("InternalCommunicationMediaObservationsDisabled",
        //            Enabled = false,
        //            Criteria = "InternalCommunicationMedia == null ||" +
        //                       "(InternalCommunicationMedia != null && InternalCommunicationMedia.ApplyObservations == False)")]
        //public string InternalCommunicationMediaObservations
        //{
        //    get => internalCommunicationMediaObservations;
        //    set => SetPropertyValue("InternalCommunicationMediaObservations", ref internalCommunicationMediaObservations, value);
        //}

        //private decimal internalTradeRateCurrencyLocalToUSD;
        //[Appearance("InternalTradeRateCurrencyLocalToUSDHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeRateCurrencyLocalToUSDDisabled",
        //            Enabled = false,
        //            Criteria = "AssociateExistingInternalTrade")]
        //[ImmediatePostData(true)]
        //[DbType("decimal(23,8)")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //public decimal InternalTradeRateCurrencyLocalToUSD
        //{
        //    get { return internalTradeRateCurrencyLocalToUSD; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("InternalTradeRateCurrencyLocalToUSD", ref internalTradeRateCurrencyLocalToUSD, value) && !IsLoading && !IsSaving)
        //        {
        //            UpdateForeignExchangeInternalTradeValues("InternalTradeRateCurrencyLocalToUSD");
        //            UpdateForeignExchangeOperationProfit();
        //        }
        //    }
        //}

        //private decimal internalTradeRateCurrencyNegotiatedToUSD;
        //[Appearance("InternalTradeRateCurrencyNegotiatedToUSDHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeRateCurrencyNegotiatedToUSDDisabled",
        //            Enabled = false,
        //            Criteria = "CurrencyOperation.IsUSDollar OR AssociateExistingInternalTrade")]
        //[ImmediatePostData(true)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal InternalTradeRateCurrencyNegotiatedToUSD
        //{
        //    get { return internalTradeRateCurrencyNegotiatedToUSD; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("InternalTradeRateCurrencyNegotiatedToUSD", ref internalTradeRateCurrencyNegotiatedToUSD, value) && !IsLoading && !IsSaving)
        //        {
        //            UpdateForeignExchangeInternalTradeValues("InternalTradeRateCurrencyNegotiatedToUSD");
        //            UpdateForeignExchangeOperationProfit();
        //        }
        //    }
        //}

        //private decimal _internalRateApprovedUSDToCurrencyNegotiated;
        ///// <summary>
        ///// Tasa USD - Moneda Negociada
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[Appearance("InternalRateApprovedUSDToCurrencyNegotiatedHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalRateApprovedUSDToCurrencyNegotiatedDisabled",
        //            Enabled = false,
        //            Criteria = "CurrencyOperation.IsUSDollar OR AssociateExistingInternalTrade")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal InternalRateApprovedUSDToCurrencyNegotiated
        //{
        //    get { return _internalRateApprovedUSDToCurrencyNegotiated; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("InternalRateApprovedUSDToCurrencyNegotiated", ref _internalRateApprovedUSDToCurrencyNegotiated, value) && !IsLoading && !IsSaving)
        //            UpdateForeignExchangeInternalTradeValues("InternalRateApprovedUSDToCurrencyNegotiated");
        //    }
        //}

        //private decimal _internalRateNegotiatedCurrencyToLocal;
        //[Appearance("InternalRateNegotiatedCurrencyToLocalHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalRateNegotiatedCurrencyToLocalDisabled", Enabled = false)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.000000}")]
        //[ModelDefault("EditMask", "##################,##0.000000")]
        //[DbType("decimal(23,8)")]
        //public decimal InternalRateNegotiatedCurrencyToLocal
        //{
        //    get { return _internalRateNegotiatedCurrencyToLocal; }
        //    set { SetPropertyValue<decimal>("InternalRateNegotiatedCurrencyToLocal", ref _internalRateNegotiatedCurrencyToLocal, value); }
        //}

        //private decimal internalTradeUSDAmount;
        //[Appearance("InternalTradeUSDAmountHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeUSDAmountDisabled", Enabled = false)]
        //public decimal InternalTradeUSDAmount
        //{
        //    get { return internalTradeUSDAmount; }
        //    set { SetPropertyValue<decimal>("InternalTradeUSDAmount", ref internalTradeUSDAmount, value); }
        //}

        //private decimal internalTradeLocalAmount;
        //[Appearance("InternalTradeLocalAmountHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeLocalAmountDisabled", Enabled = false)]
        //public decimal InternalTradeLocalAmount
        //{
        //    get { return internalTradeLocalAmount; }
        //    set { SetPropertyValue<decimal>("InternalTradeLocalAmount", ref internalTradeLocalAmount, value); }
        //}

        //private decimal internalTradeProfit;
        //[Appearance("InternalTradeProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[Appearance("InternalTradeProfitDisabled", Enabled = false)]
        //public decimal InternalTradeProfit
        //{
        //    get { return internalTradeProfit; }
        //    set { SetPropertyValue<decimal>("InternalTradeProfit", ref internalTradeProfit, value); }
        //}

        //// Parámetro 'Concepto Bancario sólo en Operaciones Propias' proveniente de los Parámetros de Registro de Operaciones
        //private bool IsBankingConceptOnlyOwnOperations
        //    => ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company)?
        //       .IsBankingConceptOnlyOwnOperations ?? false;

        //private BankingConcept _bankingConcept;
        ///// <summary>
        ///// Concepto Bancario
        ///// </summary>        
        //[ImmediatePostData(true)]
        //[Appearance("BankingConceptDisabled", Enabled = false, Criteria = "IsBankingConceptOnlyOwnOperations = true && IsOwnOperation = false")]
        //public BankingConcept BankingConcept
        //{
        //    get
        //    { return _bankingConcept; }
        //    set
        //    {
        //        if (SetPropertyValue<BankingConcept>("BankingConcept", ref _bankingConcept, value) && !IsLoading && !IsSaving && BankingConcept.Concept == "Crédito Cliente")
        //        {
        //            CustomerAccountDeposit = null;
        //            CustomerAccountDepositBank = null;
        //            ClientBankAccount = null;
        //        }
        //    }
        //}

        ///// <summary>
        /////  Variable privada que maneja los parametros de Registro de Operaciones
        ///// </summary>
        //private ForeignExchangeOperationParameter _OperationParameters;

        //private decimal _profitUSD;
        ///// <summary>
        ///// Utilidad USD
        ///// </summary>        
        //[Appearance("ProfitUSDDisabled", Enabled = false)]
        //[Appearance("ProfitUSDHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.0000}")]
        //[ModelDefault("EditMask", "##################,##0.0000")]
        //public decimal ProfitUSD
        //{
        //    get { return _profitUSD; }
        //    set { SetPropertyValue<decimal>("ProfitUSD", ref _profitUSD, value); }
        //}

        //private decimal _profitUSDLocalCurrency;
        ///// <summary>
        ///// Utilidad USD - Moneda Local
        ///// </summary>        
        //[Appearance("ProfitUSDLocalCurrencyDisabled", Enabled = false)]
        //[Appearance("ProfitUSDLocalCurrencyHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal ProfitUSDLocalCurrency
        //{
        //    get { return _profitUSDLocalCurrency; }
        //    set { SetPropertyValue<decimal>("ProfitUSDLocalCurrency", ref _profitUSDLocalCurrency, value); }
        //}

        //private decimal _spreadLocalCurrency;
        ///// <summary>
        ///// Spread Moneda Local
        ///// </summary>        
        //[Appearance("SpreadLocalCurrencyDisabled", Enabled = false)]
        //[Appearance("SpreadLocalCurrencyHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.0000}")]
        //[ModelDefault("EditMask", "##################,##0.0000")]
        //public decimal SpreadLocalCurrency
        //{
        //    get { return _spreadLocalCurrency; }
        //    set { SetPropertyValue<decimal>("SpreadLocalCurrency", ref _spreadLocalCurrency, value); }
        //}

        //private decimal _totalExternalProfit;
        ///// <summary>
        ///// Total Utilidad o Pérdida Externa
        ///// </summary>        
        //[Appearance("TotalExternalProfitDisabled", Enabled = false)]
        //[Appearance("TotalExternalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "(!GenerateInternalTrade || !ShowAllInternalTrade) && !AssociateExistingInternalTrade")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal TotalExternalProfit
        //{
        //    get { return _totalExternalProfit; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("TotalExternalProfit", ref _totalExternalProfit, value) && !IsLoading && !IsSaving)
        //        {
        //            if (TotalExternalProfit == 0)
        //            {
        //                ExternalPercentage = 0;
        //                DistributedExtenalProfit = 0;
        //            }
        //            FinalExternalProfit = TotalExternalProfit;
        //        }
        //    }
        //}

        //private decimal _externalPercentage;
        ///// <summary>
        ///// Porcentaje de Utilidad o Pérdida Externa a Distribuir.
        ///// </summary>        
        //[Appearance("ExternalPercentageHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || TotalExternalProfit == 0 || ShowAllInternalTrade == False")]
        //[ImmediatePostData(true)]
        //public decimal ExternalPercentage
        //{
        //    get { return _externalPercentage; }
        //    set
        //    {
        //        if (_externalPercentage != value && SetPropertyValue<decimal>("ExternalPercentage", ref _externalPercentage, value) && !IsLoading && !IsSaving)
        //        {
        //            if (ExternalPercentage >= 0 && ExternalPercentage <= 1)
        //            {
        //                DistributedExtenalProfit = Math.Round(TotalExternalProfit * ExternalPercentage,
        //                                                      AmountRound,
        //                                                      MidpointRounding.AwayFromZero);
        //                FinalExternalProfit = Math.Round(TotalExternalProfit - DistributedExtenalProfit,
        //                                                 AmountRound,
        //                                                 MidpointRounding.AwayFromZero);
        //            }
        //            else if (!IsFromTransactionalPortal)
        //                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                            "El Porcentaje de Utilidad debe encontrase entre 0-100%",
        //                                            webHeight: 10,
        //                                            webWidth: 70);
        //        }
        //    }
        //}

        //private decimal _distributedExtenalProfit;
        ///// <summary>
        ///// Utilidad o Pérdida Externa Distribuida
        ///// </summary>        
        //[Appearance("DistributedExtenalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || TotalExternalProfit == 0 || ShowAllInternalTrade == False")]
        //[ImmediatePostData(true)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal DistributedExtenalProfit
        //{
        //    get { return _distributedExtenalProfit; }
        //    set
        //    {
        //        if (_distributedExtenalProfit != value && SetPropertyValue<decimal>("DistributedExtenalProfit", ref _distributedExtenalProfit, value) && !IsLoading && !IsSaving)
        //        {
        //            if (TotalExternalProfit != 0) ExternalPercentage = DistributedExtenalProfit / TotalExternalProfit;
        //            else ExternalPercentage = 0;
        //            FinalExternalProfit = Math.Round(TotalExternalProfit - DistributedExtenalProfit,
        //                                             AmountRound,
        //                                             MidpointRounding.AwayFromZero);
        //        }
        //    }
        //}

        //private decimal _finalExternalProfit;
        ///// <summary>
        ///// Utilidad o Pérdida Externa Final
        ///// </summary>        
        //[Appearance("FinalExternalProfitDisabled", Enabled = false)]
        //[Appearance("FinalExternalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || TotalExternalProfit == 0 || ShowAllInternalTrade == False")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal FinalExternalProfit
        //{
        //    get { return _finalExternalProfit; }
        //    set { SetPropertyValue<decimal>("FinalExternalProfit", ref _finalExternalProfit, value); }
        //}

        //private decimal _totalInternalProfit;
        ///// <summary>
        ///// Total Utilidad o Pérdida Interna
        ///// </summary>        
        //[Appearance("TotalInternalProfitDisabled", Enabled = false)]
        //[Appearance("TotalInternalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || EnabledInternalProfit == False || ShowAllInternalTrade == False")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal TotalInternalProfit
        //{
        //    get { return _totalInternalProfit; }
        //    set
        //    {
        //        if (SetPropertyValue<decimal>("TotalInternalProfit", ref _totalInternalProfit, value) && !IsLoading && !IsSaving)
        //        {
        //            if (TotalInternalProfit == 0)
        //            {
        //                InternalPercentage = 0;
        //                DistributedInternalProfit = 0;
        //            }
        //            FinalInternalProfit = TotalInternalProfit;
        //        }
        //    }
        //}

        //private decimal _internalPercentage;
        ///// <summary>
        ///// Porcenaje de Utilidad o Pérdida Interna a Distribuir.
        ///// </summary>        
        //[Appearance("InternalPercentageHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False  || TotalInternalProfit == 0 || EnabledInternalProfit == False || ShowAllInternalTrade == False")]
        //[ImmediatePostData(true)]
        //public decimal InternalPercentage
        //{
        //    get { return _internalPercentage; }
        //    set
        //    {
        //        if (_internalPercentage != value && SetPropertyValue<decimal>("InternalPercentage", ref _internalPercentage, value) && !IsLoading && !IsSaving)
        //        {
        //            if (InternalPercentage >= 0 && InternalPercentage <= 1)
        //            {
        //                DistributedInternalProfit = Math.Round(TotalInternalProfit * InternalPercentage,
        //                                                       AmountRound,
        //                                                       MidpointRounding.AwayFromZero);
        //                FinalInternalProfit = Math.Round(TotalInternalProfit - DistributedInternalProfit,
        //                                                 AmountRound,
        //                                                 MidpointRounding.AwayFromZero);

        //            }
        //            else if (!IsFromTransactionalPortal)
        //                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                            "El Porcentaje de Utilidad debe encontrase entre 0-100%",
        //                                            webHeight: 10,
        //                                            webWidth: 70);
        //        }
        //    }
        //}

        //private decimal _distributedInternalProfit;
        ///// <summary>
        ///// Utilidad o Pérdida interna Distribuida
        ///// </summary>        
        //[Appearance("DistributedInternalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || TotalInternalProfit == 0 || EnabledInternalProfit == False || ShowAllInternalTrade == False")]
        //[ImmediatePostData(true)]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal DistributedInternalProfit
        //{
        //    get { return _distributedInternalProfit; }
        //    set
        //    {
        //        if (_distributedInternalProfit != value && SetPropertyValue<decimal>("DistributedInternalProfit", ref _distributedInternalProfit, value) && !IsLoading && !IsSaving)
        //        {
        //            if (TotalInternalProfit != 0) InternalPercentage = DistributedInternalProfit / TotalInternalProfit;
        //            else InternalPercentage = 0;
        //            FinalInternalProfit = Math.Round(TotalInternalProfit - DistributedInternalProfit,
        //                                             AmountRound,
        //                                             MidpointRounding.AwayFromZero);
        //        }
        //    }
        //}

        //private decimal _finalInternalProfit;
        ///// <summary>
        ///// Utilidad o Pérdida Interna Final
        ///// </summary>        
        //[Appearance("FinalInternalProfitDisabled", Enabled = false)]
        //[Appearance("FinalInternalProfitHide",
        //            Visibility = ViewItemVisibility.Hide,
        //            Criteria = "GenerateInternalTrade == False || TotalInternalProfit == 0 || EnabledInternalProfit == False || ShowAllInternalTrade == False")]
        //[ModelDefault("DisplayFormat", "{0:##################,##0.00}")]
        //[ModelDefault("EditMask", "##################,##0.00")]
        //public decimal FinalInternalProfit
        //{
        //    get { return _finalInternalProfit; }
        //    set { SetPropertyValue<decimal>("FinalInternalProfit", ref _finalInternalProfit, value); }
        //}

        //private void UpdateTotalInternalProfit()
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork(Session.ObjectLayer))
        //    {
        //        Currency LocalCurrency = new XPQuery<Currency>(unitOfWork).FirstOrDefault(currency => currency.IsLocal);

        //        Currency DollarCurrency = new XPQuery<Currency>(unitOfWork).FirstOrDefault(currency => currency.IsUSDollar);

        //        if (LocalCurrency == null || DollarCurrency == null)
        //        {
        //            if (!IsFromTransactionalPortal)
        //                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                            "La Utilidad Interna no se puede calcular debido a que no existe una parametrización de moneda local o dólar.",
        //                                            "Cálculo Utilidad Interna",
        //                                            windowsMessageBoxIcon: MessageBoxIcon.Warning,
        //                                            webMessageType: InformationType.Warning,
        //                                            webHeight: 10,
        //                                            webWidth: 70);
        //            TotalInternalProfit = 0;
        //            return;
        //        }

        //        decimal? CurrencyNegociatedToDollar = PriceVendorDataManager.getReferenceExchangeValue(OperationDate.AddDays(-1), CurrencyOperation, DollarCurrency, Session);
        //        if (CurrencyOperation.Code == DollarCurrency.Code)
        //            CurrencyNegociatedToDollar = 1;

        //        if (CurrencyNegociatedToDollar == null)
        //        {
        //            if (!IsFromTransactionalPortal)
        //                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                            $"No se puede calcular la Utilidad Interna debido a que no existe una tasa {CurrencyOperation.Code} - " +
        //                                            $"{DollarCurrency.Code} Parametrizada para la fecha {OperationDate.AddDays(-1)}",
        //                                            "Cálculo Utilidad Interna",
        //                                            windowsMessageBoxIcon: MessageBoxIcon.Warning,
        //                                            webMessageType: InformationType.Warning);
        //            TotalInternalProfit = 0;
        //            return;
        //        }

        //        decimal? DollarToLocalCurrency = PriceVendorDataManager.getReferenceExchangeValue(OperationDate.AddDays(-1), DollarCurrency, LocalCurrency, Session);

        //        if (DollarToLocalCurrency == null)
        //        {
        //            if (!IsFromTransactionalPortal)
        //                PopUpMessages.CreateMessage(ParametersBridge.Application,
        //                                            $"No se puede calcular la Utilidad Interna debido a que no existe una tasa {DollarCurrency.Code} - " +
        //                                            $"{LocalCurrency.Code} Parametrizada para la fecha {OperationDate.AddDays(-1)}",
        //                                            "Cálculo Utilidad Interna",
        //                                            windowsMessageBoxIcon: MessageBoxIcon.Warning,
        //                                            webMessageType: InformationType.Warning);
        //            TotalInternalProfit = 0;
        //            return;
        //        }
        //        TotalInternalProfit = OperationType.Id == 1 ?
        //            Math.Round((InternalRateNegotiatedCurrencyToLocal - (decimal)DollarToLocalCurrency * (decimal)CurrencyNegociatedToDollar) * OperationValue,
        //                       AmountRound,
        //                       MidpointRounding.AwayFromZero) :
        //            Math.Round((InternalRateNegotiatedCurrencyToLocal - (decimal)DollarToLocalCurrency * (decimal)CurrencyNegociatedToDollar) * (-1) * OperationValue,
        //                       AmountRound,
        //                       MidpointRounding.AwayFromZero);
        //    }
        //}

        ///// <summary>
        ///// Método que actualiza las propiedades generales de Utilidad
        ///// </summary>
        //private void UpdateForeignExchangeOperationProfit(bool isRemovingInternalTrade = false,
        //                                                  bool associateExistingInternalTradeChange = false)
        //{
        //    if (!IsLoading && !IsSaving)
        //    {
        //        if (AssociateExistingInternalTrade)
        //        {
        //            if (CurrencyInternalTrades.Any() || associateExistingInternalTradeChange)
        //            {
        //                CurrencyInternalTrade CurrencyInternalTrade =
        //                CurrencyInternalTrades.Select(association => association.InternalTrade)
        //                                      .FirstOrDefault(trade => trade.ReferenceCurrencyTrade?.Position?.ID == OperationType?.Id);

        //                new ForeignExchangeOperationManager()
        //                    .UpdateInternalTradeInfo(this, CurrencyInternalTrade, isRemovingInternalTrade);
        //            }
        //        }
        //        else
        //        {
        //            if (GenerateInternalTrade)
        //            {
        //                if (EnabledInternalProfit)
        //                    UpdateTotalInternalProfit();
        //                else
        //                    TotalInternalProfit = 0;
        //            }

        //            new ForeignExchangeOperationManager()
        //                .UpdateInternalTradeInfo(this, isRemovingInternalTrade: isRemovingInternalTrade);
        //        }

        //        RateNegotiatedCurrencyToLocal = OperationValue > 0 ?
        //            Math.Round(SubtotalTradingValueCurrencyNegotiated / OperationValue, RateRound, MidpointRounding.AwayFromZero) : 0;
        //    }
        //}

        ////Redmine 903 : John J Garcia 12-12-2016
        //private void UpdateForeignExchangeOperationAmountUSD()
        //{
        //    if (!IsLoading && !IsSaving)
        //    {
        //        if (RateApprovedUSDToCurrencyNegotiated != 0 && CurrencyOperation != null && CurrencyOperation.Behavior == CurrencyBehavior.Indirecta)
        //            OperationValueUSD = Math.Round(OperationValue / RateApprovedUSDToCurrencyNegotiated, AmountRound);
        //        else if (RateApprovedCurrencyNegotiatedToUSD != 0 && CurrencyOperation != null && (CurrencyOperation.Behavior == CurrencyBehavior.Directa || CurrencyOperation.Behavior == 0))
        //            OperationValueUSD = Math.Round(OperationValue * RateApprovedCurrencyNegotiatedToUSD, AmountRound, MidpointRounding.AwayFromZero);
        //        else
        //            OperationValueUSD = 0;
        //        RateNegotiatedCurrencyToLocal = OperationValue > 0 ? Math.Round(SubtotalTradingValueCurrencyNegotiated / OperationValue, RateRound, MidpointRounding.AwayFromZero) : 0;
        //        InternalRateNegotiatedCurrencyToLocal = OperationValue > 0 ? Math.Round(InternalTradeLocalAmount / OperationValue, RateRound, MidpointRounding.AwayFromZero) : 0;

        //        UpdateTax4x1000Value();
        //    }
        //}

        //private void UpdateTax4x1000Value()
        //{
        //    if (!IsLoading && !IsSaving)
        //    {
        //        Tax4x1000 = 0;

        //        if (isFMTApplied && SubtotalTradingValueCurrencyNegotiated != 0)
        //        {
        //            if (_OperationParameters == null && ExternalInvesmentUnit?.Company != null)
        //                _OperationParameters = ForeignExchangeOperationParameterClass
        //                    .GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company);

        //            if (_operationType?.Id == 1)
        //                Tax4x1000 = _OperationParameters?.FMTOnLocalCurrencySubtotal == true ?
        //                    SubtotalTradingValueCurrencyNegotiated * 4 / 1000 : OperationValueCurrencyNegotiated * 4 / 1000;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Actualiza las propiedades de la operación interna
        ///// </summary>
        ///// <param name="property">Propiedad desde donde se instancia el método</param>
        ///// Redmine 2829 (2817) : John J Garcia 13/06/2017
        //private void UpdateForeignExchangeInternalTradeValues(string property)
        //{
        //    if (!IsLoading && !IsSaving && GenerateInternalTrade && !Locked)
        //    {
        //        if (property != "InternalTradeRateCurrencyNegotiatedToUSD")
        //            if (InternalRateApprovedUSDToCurrencyNegotiated != 0)
        //                InternalTradeRateCurrencyNegotiatedToUSD = Math.Round(1 / InternalRateApprovedUSDToCurrencyNegotiated, RateRound, MidpointRounding.AwayFromZero);
        //            else
        //                InternalTradeRateCurrencyNegotiatedToUSD = 0;
        //        if (property != "InternalRateApprovedUSDToCurrencyNegotiated")
        //            if (CurrencyOperation != null)
        //                if (InternalTradeRateCurrencyNegotiatedToUSD != 0)
        //                    InternalRateApprovedUSDToCurrencyNegotiated = Math.Round(1 / InternalTradeRateCurrencyNegotiatedToUSD, RateRound, MidpointRounding.AwayFromZero);
        //                else
        //                    InternalRateApprovedUSDToCurrencyNegotiated = 0;
        //        if (property != "InternalTradeUSDAmount")
        //            if (RateApprovedUSDToCurrencyNegotiated != 0 && CurrencyOperation != null && CurrencyOperation.Behavior == CurrencyBehavior.Indirecta
        //                 && InternalRateApprovedUSDToCurrencyNegotiated != 0)
        //                InternalTradeUSDAmount = Math.Round(OperationValue / InternalRateApprovedUSDToCurrencyNegotiated, AmountRound);
        //            else if (RateApprovedCurrencyNegotiatedToUSD != 0 && CurrencyOperation != null && (CurrencyOperation.Behavior == CurrencyBehavior.Directa || CurrencyOperation.Behavior == 0))
        //                InternalTradeUSDAmount = Math.Round(OperationValue * InternalTradeRateCurrencyNegotiatedToUSD, AmountRound, MidpointRounding.AwayFromZero);
        //            else
        //                InternalTradeUSDAmount = 0;
        //        if (property != "InternalTradeLocalAmount")
        //            InternalTradeLocalAmount = InternalTradeUSDAmount * InternalTradeRateCurrencyLocalToUSD;
        //    }
        //}

        //public QuotaApprovalStatus GetQuotaApprovalStatus()
        //    => QuotaApprovalStatus;

        //public QuotaValidationStatus GetQuotaValidationStatus()
        //    => QuotaValidationStatus;

        //public QuotaApprovalStatus SetQuotaApprovalStatus(QuotaApprovalStatus quotaApprovalStatus)
        //    => QuotaApprovalStatus = quotaApprovalStatus;

        //public QuotaValidationStatus SetQuotaValidationStatus(QuotaValidationStatus quotaValidationStatus)
        //    => QuotaValidationStatus = quotaValidationStatus;

        //private bool _fixOperation;
        ///// <summary>
        ///// Operación Fix
        ///// </summary>        
        //[Appearance("FixOperationDisabled2", Enabled = false, Criteria = "IsOwnOperation")]
        //[ImmediatePostData]
        //public bool FixOperation
        //{
        //    get { return _fixOperation; }
        //    set
        //    {
        //        if (SetPropertyValue<bool>("FixOperation", ref _fixOperation, value) && !IsLoading && !IsSaving)
        //        {
        //            FixType = null;
        //            FixComplianceRate = null;
        //            FixPoints = 0;
        //        }
        //    }
        //}

        //private string _sourceBank;
        ///// <summary>
        ///// Banco Origen Escrito
        ///// </summary>
        //[Appearance("SourceBankShow", Visibility = ViewItemVisibility.Show, Criteria = "OperationClass == 2 && !Client.IsGuarded")]
        //[Appearance("SourceBankHide", Visibility = ViewItemVisibility.Hide, Criteria = "!(OperationClass == 2 && !Client.IsGuarded)")]
        //public string SourceBank
        //{
        //    get { return _sourceBank; }
        //    set { SetPropertyValue("SourceBank", ref _sourceBank, value); }
        //}

        //private Countries _originCountry;
        ///// <summary>
        ///// País Origen
        ///// </summary>
        //[Appearance("OriginCountryShow", Visibility = ViewItemVisibility.Show, Criteria = "OperationClass == 2 && !Client.IsGuarded")]
        //[Appearance("OriginCountryHide", Visibility = ViewItemVisibility.Hide, Criteria = "!(OperationClass == 2 && !Client.IsGuarded)")]
        //public Countries OriginCountry
        //{
        //    get { return _originCountry; }
        //    set { SetPropertyValue("OriginCountry", ref _originCountry, value); }
        //}

        //private string _sourceCity;
        ///// <summary>
        ///// Banco Origen Escrito
        ///// </summary>
        //[Appearance("SourceCityShow", Visibility = ViewItemVisibility.Show, Criteria = "OperationClass == 2 && !Client.IsGuarded")]
        //[Appearance("SourceCityHide", Visibility = ViewItemVisibility.Hide, Criteria = "!(OperationClass == 2 && !Client.IsGuarded)")]
        //public string SourceCity
        //{
        //    get { return _sourceCity; }
        //    set { SetPropertyValue("SourceCity", ref _sourceCity, value); }
        //}

        //private bool _isForwardOperation;
        ///// <summary>
        ///// Indicador si la operación es Forward
        ///// </summary>     
        //[ImmediatePostData(true)]
        //public bool IsForwardOperation
        //{
        //    get { return _isForwardOperation; }
        //    set
        //    {
        //        if (SetPropertyValue("IsForwardOperation", ref _isForwardOperation, value) && !IsLoading && !IsSaving)
        //            if (!IsForwardOperation)
        //                ForwardOperationId = "";
        //    }
        //}

        //private string _forwardOperationId;
        ///// <summary>
        ///// Numero de Operación Forward que generó la operación
        ///// </summary>    
        //[Appearance("ForwardOperationIdEnabled", Enabled = true, Criteria = "IsForwardOperation")]
        //[Appearance("ForwardOperationIdDisabled", Enabled = false, Criteria = "!IsForwardOperation")]
        //public string ForwardOperationId
        //{
        //    get { return _forwardOperationId; }
        //    set { SetPropertyValue("ForwardOperationId", ref _forwardOperationId, value); }
        //}

        //private string _userThatApproved;
        ///// <summary>
        ///// Usuario que Aprobó la Operación
        ///// </summary>
        //[Appearance("UserThatApprovedHide", Visibility = ViewItemVisibility.Hide)]
        //public string UserThatApproved
        //{
        //    get { return _userThatApproved; }
        //    set { SetPropertyValue<string>("UserThatApproved", ref _userThatApproved, value); }
        //}

        //private DateTime _approvalDate;
        ///// <summary>
        ///// Fecha y Hora de Aprobación
        ///// </summary>
        //[Appearance("ApprovalDateHide", Visibility = ViewItemVisibility.Hide)]
        //public DateTime ApprovalDate
        //{
        //    get { return _approvalDate; }
        //    set { SetPropertyValue<DateTime>("ApprovalDate", ref _approvalDate, value); }
        //}

        //private string _userThatComplemented;
        ///// <summary>
        ///// Usuario que Complementó la Operación
        ///// </summary>
        //[Appearance("UserThatComplementedHide", Visibility = ViewItemVisibility.Hide)]
        //public string UserThatComplemented
        //{
        //    get { return _userThatComplemented; }
        //    set { SetPropertyValue<string>("UserThatComplemented", ref _userThatComplemented, value); }
        //}

        //private DateTime _complementedDate;
        ///// <summary>
        ///// Fecha y Hora de Complementación
        ///// </summary>
        //[Appearance("ComplementedDateHide", Visibility = ViewItemVisibility.Hide)]
        //public DateTime ComplementedDate
        //{
        //    get { return _complementedDate; }
        //    set { SetPropertyValue<DateTime>("ComplementedDate", ref _complementedDate, value); }
        //}

        //private string _customerAccountDepositBank;
        ///// <summary>
        ///// Banco de la Cuenta Bancaria del Cliente
        ///// </summary>                
        //[Appearance("CustomerAccountDepositBankDisabled", Enabled = false, Criteria = "!IsNullOrEmpty(ClientBankAccount) || BankingConcept.Concept == 'Crédito Cliente'")]
        //[ImmediatePostData(true)]
        //public string CustomerAccountDepositBank
        //{
        //    get { return _customerAccountDepositBank; }
        //    set
        //    {
        //        if (SetPropertyValue<string>("CustomerAccountDepositBank", ref _customerAccountDepositBank, value) && !IsLoading && !IsSaving)
        //            ClientBankAccount = null;

        //    }
        //}

        //private ForeignExchangeOperationNovelty operationNovelty;
        ///// <summary>
        ///// Novedad de la operación, usada para depósitos de clientes
        ///// </summary>
        //[Appearance("OperationNoveltyDisabled", Enabled = false)]
        //[Appearance("OperationNoveltyHide", Visibility = ViewItemVisibility.Hide, Criteria = "OperationType.Id = 1")]
        //public ForeignExchangeOperationNovelty OperationNovelty
        //{
        //    get { return operationNovelty; }
        //    set { SetPropertyValue<ForeignExchangeOperationNovelty>("OperationNovelty", ref operationNovelty, value); }
        //}

        //private ForeignExchangeBalanceNovelty balanceNovelty;
        //[Appearance("BalanceNoveltyDisabled", Enabled = false)]
        //public ForeignExchangeBalanceNovelty BalanceNovelty
        //{
        //    get { return balanceNovelty; }
        //    set { SetPropertyValue("BalanceNovelty", ref balanceNovelty, value); }
        //}

        //private EnumBillNovelty billNovelty;
        //[Appearance("BillNoveltyDisabled", Enabled = false)]
        //public EnumBillNovelty BillNovelty
        //{
        //    get { return billNovelty; }
        //    set { SetPropertyValue("BillNovelty", ref billNovelty, value); }
        //}

        //private EnumBillStatus billStatus;
        //[Appearance("BillStatusDisabled", Enabled = false)]
        //public EnumBillStatus BillStatus
        //{
        //    get { return billStatus; }
        //    set { SetPropertyValue("BillStatus", ref billStatus, value); }
        //}

        //private string sourceSystemCode;
        ///// <summary>
        ///// Código de la operación en sistema externo
        ///// </summary>
        //[Appearance("SourceSystemCodeDisabled", Enabled = false)]
        //[Appearance("SourceSystemCodeHide", Visibility = ViewItemVisibility.Hide, Criteria = "OperationType.Id = 1")]
        //public string SourceSystemCode
        //{
        //    get { return sourceSystemCode; }
        //    set { SetPropertyValue<string>("SourceSystemCode", ref sourceSystemCode, value); }
        //}

        //private string sourceSystemError;
        ///// <summary>
        ///// Errores en novedades de la operación
        ///// </summary>
        //[Size(300)]
        //[Appearance("OperationNoveltyErrorDisabled", Enabled = false)]
        //[Appearance("OperationNoveltyErrorHide", Visibility = ViewItemVisibility.Hide, Criteria = "OperationType.Id = 1")]
        //public string SourceSystemError
        //{
        //    get { return sourceSystemError; }
        //    set { SetPropertyValue<string>("SourceSystemError", ref sourceSystemError, value); }
        //}

        //private string billMessages;
        ///// <summary>
        ///// Errores en el envio de Facturas
        ///// </summary>
        //[Size(500)]
        //[Appearance("BillMessagesDisabled", Enabled = false)]
        //public string BillMessages
        //{
        //    get => billMessages;
        //    set => SetPropertyValue("BillMessages", ref billMessages, value);
        //}

        //private ClientBankAccounts _clientBankAccount;
        ///// <summary>
        ///// Cuenta bancaria del cliente
        ///// </summary>
        //[DataSourceCriteria("Client = '@This.Client'")]
        //[ImmediatePostData(true)]
        //[Appearance("ClientBankAccountDisabled", Enabled = false, Criteria = "!IsNullOrEmpty(CustomerAccountDeposit) || !IsNullOrEmpty(CustomerAccountDepositBank) || BankingConcept.Concept == 'Crédito Cliente'")]
        //public ClientBankAccounts ClientBankAccount
        //{
        //    get { return _clientBankAccount; }
        //    set
        //    {
        //        if (SetPropertyValue<ClientBankAccounts>("ClientBankAccount", ref _clientBankAccount, value) && !IsLoading && !IsSaving)
        //        {
        //            CustomerAccountDeposit = null;
        //            CustomerAccountDepositBank = null;
        //        }
        //    }
        //}

        //private bool isExternalSystem;
        ///// <summary>
        ///// Generada en Sistema Externo
        ///// </summary>            
        //[Appearance("IsExternalSystemHide", Visibility = ViewItemVisibility.Hide)]
        //public bool IsExternalSystem
        //{
        //    get { return isExternalSystem; }
        //    set { SetPropertyValue("IsExternalSystem", ref isExternalSystem, value); }
        //}
        //private bool crossOperation;
        ///// <summary>
        ///// Operación Cruzada
        ///// </summary>                    
        //[ImmediatePostData(true)]
        //[Appearance("CrossOperationDisabled", Enabled = false, Criteria = @"CurrencyOperation == null || OperationType == null || OperationValue == 0")]
        //public bool CrossOperation
        //{
        //    get { return crossOperation; }
        //    set { SetPropertyValue("CrossOperation", ref crossOperation, value); }
        //}

        ////private SWAPOriginTypes _SWAPOriginType;
        /////// <summary>
        /////// Tipo Origen SWAP
        /////// </summary>                 
        ////public SWAPOriginTypes SWAPOriginType
        ////{
        ////    get { return _SWAPOriginType; }
        ////    set { SetPropertyValue("_SWAPOriginType", ref _SWAPOriginType, value); }
        ////}

        /// <summary>
        /// Lista de Operaciones ME/ME
        /// </summary>
        [Association("ForeignExchangeOperation_ForeignCurrencyTrade"), Aggregated]
        [Appearance("ForeignCurrencyTradesEnabled", Enabled = false, Criteria = "!CrossOperation")]
        public XPCollection<ForeignCurrencyTrade> ForeignCurrencyTrades
            => GetCollection<ForeignCurrencyTrade>("ForeignCurrencyTrades");

        //private int? rateRound = null;
        //public int RateRound
        //{
        //    get
        //    {
        //        if (rateRound != null)
        //            return rateRound.Value;
        //        else
        //        {
        //            string round = ApplicationParametersManager.GetApplicationParameterValue("RedondeoTasas", Session);
        //            rateRound = int.TryParse(round, out int value) ? value : 0;
        //            return rateRound.Value;
        //        }
        //    }
        //}

        //private int? amountRound = null;
        //public int AmountRound
        //{
        //    get
        //    {
        //        if (amountRound != null)
        //            return amountRound.Value;
        //        else
        //        {
        //            string round = ApplicationParametersManager.GetApplicationParameterValue("RedondeoMontos", Session);
        //            amountRound = int.TryParse(round, out int value) ? value : 0;
        //            return amountRound.Value;
        //        }
        //    }
        //}

        //[NonPersistent]
        //[Appearance("", Visibility = ViewItemVisibility.Hide)]
        //public bool Locked { set; get; }

        ///// <summary>
        ///// Habilitar Cálculo de Utilidad Interna
        ///// </summary>
        //[Appearance("EnabledInternalProfitHide", Visibility = ViewItemVisibility.Hide)]
        //public bool EnabledInternalProfit
        //{
        //    get
        //    {
        //        if (_OperationParameters != null) return _OperationParameters.CalculateInternalProfit;

        //        if (ExternalInvesmentUnit?.Company == null) return false;

        //        _OperationParameters = ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit.Company);

        //        return _OperationParameters?.CalculateInternalProfit ?? false;
        //    }
        //}

        ///// <summary>
        ///// Muestra todos los campos de la operación interna
        ///// </summary>
        //[Appearance("ShowAllInternalTradeHide", Visibility = ViewItemVisibility.Hide)]
        //public bool ShowAllInternalTrade
        //{
        //    get
        //    {
        //        if (_OperationParameters != null) return _OperationParameters.GenerateInternalTrade;

        //        if (ExternalInvesmentUnit?.Company == null) return false;

        //        _OperationParameters = ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit.Company);

        //        return _OperationParameters?.GenerateInternalTrade ?? false;
        //    }
        //}

        ///// <summary>
        ///// Parámetro para activar checkbox "Generar Operación Interna"
        ///// </summary>
        //private bool EnableGenerateInternalTradeCheckBox =>
        //    _OperationParameters == null ?
        //        ForeignExchangeOperationParameterClass
        //            .GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company)?.EnableGenerateInternalTradeCheckBox ?? false :
        //        _OperationParameters.EnableGenerateInternalTradeCheckBox;

        ///// <summary>
        ///// Pagos de Depósitos de Clientes
        ///// </summary>
        ////[Association("ClientCurrencyDepositPayment-ForeignExchangeOperation")]
        ////public XPCollection<ClientCurrencyDepositPayments> ClientCurrencyDepositPayments
        ////    => GetCollection<ClientCurrencyDepositPayments>("ClientCurrencyDepositPayments");

        /////// <summary>
        /////// Operaciones Internas
        /////// </summary>
        ////[Association("ForeignExchangeOperation-FXOper_CurrIntTrade", typeof(ForeignExchangeOperation_CurrencyInternalTrade)), Aggregated]
        ////[Appearance("CurrencyInternalTradesHide",
        ////            Visibility = ViewItemVisibility.Hide,
        ////            Criteria = "!AssociateExistingInternalTrade && (!GenerateInternalTrade || !ShowAllInternalTrade)")]
        ////public XPCollection<ForeignExchangeOperation_CurrencyInternalTrade> CurrencyInternalTrades
        ////    => GetCollection<ForeignExchangeOperation_CurrencyInternalTrade>(nameof(CurrencyInternalTrades));

        //private string prometeoRecordId;
        ///// <summary>
        ///// Id de la operación dado por el sistema Prometeo
        ///// </summary>
        //public string PrometeoRecordId
        //{
        //    get => prometeoRecordId;
        //    set => SetPropertyValue(nameof(PrometeoRecordId), ref prometeoRecordId, value);
        //}

        //private string prometeoSendingMessage;
        ///// <summary>
        ///// Mensaje recibido en el envío a Prometeo
        ///// </summary>
        //[Size(500)]
        //public string PrometeoSendingMessage
        //{
        //    get => prometeoSendingMessage;
        //    set => SetPropertyValue(nameof(PrometeoSendingMessage), ref prometeoSendingMessage, value);
        //}

        //private bool prometeoSuccessfulSendingResult;
        ///// <summary>
        ///// Resultado exitoso en el envío a Prometeo
        ///// </summary>
        //public bool PrometeoSuccessfulSendingResult
        //{
        //    get => prometeoSuccessfulSendingResult;
        //    set => SetPropertyValue(nameof(PrometeoSuccessfulSendingResult), ref prometeoSuccessfulSendingResult, value);
        //}

        //private bool isFromTransactionalPortal;
        ///// <summary>
        ///// Proviene del Portal Transaccional
        ///// </summary>
        //public bool IsFromTransactionalPortal
        //{
        //    get => isFromTransactionalPortal;
        //    set => SetPropertyValue(nameof(IsFromTransactionalPortal), ref isFromTransactionalPortal, value);
        //}

        //private Countries sendingCountry;
        ///// <summary>
        ///// País donde se ubica la entidad que envía/recibe (UIAF)
        ///// </summary>
        //public Countries SendingCountry
        //{
        //    get => sendingCountry;
        //    set => SetPropertyValue(nameof(SendingCountry), ref sendingCountry, value);
        //}

        //private string sendingCity;
        ///// <summary>
        ///// Ciudad donde se ubica la entidad que envía/recibe (UIAF)
        ///// </summary>
        //[Size(30)]
        //public string SendingCity
        //{
        //    get => sendingCity;
        //    set => SetPropertyValue(nameof(SendingCity), ref sendingCity, value);
        //}

        //private string sendingBank;
        ///// <summary>
        ///// Entidad que envía/recibe (UIAF)
        ///// </summary>
        //[Size(90)]
        //public string SendingBank
        //{
        //    get => sendingBank;
        //    set => SetPropertyValue(nameof(SendingBank), ref sendingBank, value);
        //}

        //private bool isWithoutIntermediary;
        ///// <summary>
        ///// Sin intermediario que envía/recibe (UIAF)
        ///// </summary>
        //public bool IsWithoutIntermediary
        //{
        //    get => isWithoutIntermediary;
        //    set => SetPropertyValue(nameof(IsWithoutIntermediary), ref isWithoutIntermediary, value);
        //}

        //private Countries intermediaryCountry;
        ///// <summary>
        ///// País donde se ubica el intermediario que envía/recibe (UIAF)
        ///// </summary>
        //[Appearance("IntermediaryCountryDisabled", Enabled = false, Criteria = "IsWithoutIntermediary")]
        //public Countries IntermediaryCountry
        //{
        //    get => intermediaryCountry;
        //    set => SetPropertyValue(nameof(IntermediaryCountry), ref intermediaryCountry, value);
        //}

        //private string intermediaryCity;
        ///// <summary>
        ///// Ciudad donde se ubica el intermediario que envía/recibe (UIAF)
        ///// </summary>
        //[Appearance("IntermediaryCityDisabled", Enabled = false, Criteria = "IsWithoutIntermediary")]
        //[Size(30)]
        //public string IntermediaryCity
        //{
        //    get => intermediaryCity;
        //    set => SetPropertyValue(nameof(IntermediaryCity), ref intermediaryCity, value);
        //}

        //private string intermediaryBank;
        ///// <summary>
        ///// Intermediario que envía/recibe (UIAF)
        ///// </summary>
        //[Appearance("IntermediaryBankDisabled", Enabled = false, Criteria = "IsWithoutIntermediary")]
        //[Size(255)]
        //public string IntermediaryBank
        //{
        //    get => intermediaryBank;
        //    set => SetPropertyValue(nameof(IntermediaryBank), ref intermediaryBank, value);
        //}

        //private bool isComplementedTransactionalPortal;
        ///// <summary>
        ///// Complementada desde el Portal Transaccional
        ///// </summary>
        //[Appearance("IsComplementedTransactionalPortalDisabled",
        //            Enabled = false,
        //            Criteria = "OperationStatus.StatusName == 'Anulada' && ComplementedStatus.ComplementedStatusName != 'NoComplementada'")]
        //[Appearance("IsComplementedTransactionalPortalDaysDisabled", Enabled = false, Criteria = "Market.days != 0")]
        //public bool IsComplementedTransactionalPortal
        //{
        //    get => isComplementedTransactionalPortal;
        //    set => SetPropertyValue(nameof(IsComplementedTransactionalPortal), ref isComplementedTransactionalPortal, value);
        //}

        ///// <summary>
        ///// Propiedad que indica cuando se a cambiado la complemnentación desde el portal para el envio
        ///// de notificaciones al cliente y Trader de la operación
        ///// </summary>
        //[NonPersistent]
        //public bool IsComplementedTransactionalPortalChangeStatus { get; set; }

        //private bool isUpdated;
        ///// <summary>
        ///// Es actualizado por primera vez.
        ///// </summary>
        //public bool IsUpdated
        //{
        //    get => isUpdated;
        //    set => SetPropertyValue(nameof(IsUpdated), ref isUpdated, value);
        //}

        //private bool notifiedByEmail;

        ///// <summary>
        ///// Notificado por Email
        ///// </summary>
        //public bool NotifiedByEmail
        //{
        //    get => notifiedByEmail;
        //    set => SetPropertyValue(nameof(NotifiedByEmail), ref notifiedByEmail, value);
        //}

        //private bool errorsByUncreatedUser;

        ///// <summary>
        ///// Tiene errores de Usuario no creado en SetFX
        ///// </summary>
        //public bool ErrorsByUncreatedUser
        //{
        //    get => errorsByUncreatedUser;
        //    set => SetPropertyValue(nameof(ErrorsByUncreatedUser), ref errorsByUncreatedUser, value);
        //}

        //private bool wasTrasmittedExecuted;
        ///// <summary>
        ///// Informa cuando la operación ya ha sido transmitida ejecutada
        ///// </summary>
        //[Appearance("WasTrasmittedExecutedDisabled", Enabled = false)]
        //public bool WasTrasmittedExecuted
        //{
        //    get => wasTrasmittedExecuted;
        //    set => SetPropertyValue(nameof(WasTrasmittedExecuted), ref wasTrasmittedExecuted, value);
        //}

        //#region Caracteristicas Operaciones Fix

        //private decimal fixRate;

        ////private FixType fixType;

        /////// <summary>
        /////// Tipo Fix
        /////// </summary>
        ////[ImmediatePostData(true)]
        ////public FixType FixType
        ////{
        ////    get { return fixType; }
        ////    set
        ////    {
        ////        if (SetPropertyValue(nameof(FixType), ref fixType, value) && !IsLoading && !IsSaving)
        ////        {
        ////            if (fixType == null)
        ////            {
        ////                RateApprovedCurrencyLocalToUSD = 0;
        ////                return;
        ////            }

        ////            if (fixRate == 0 && _OperationNumber == null)
        ////                fixRate = new OyDFixRateManager().GetRatesFix((UnitOfWork)Session, OperationDate);

        ////            if (_OperationNumber == null)
        ////                RateApprovedCurrencyLocalToUSD = fixRate;
        ////        }
        ////    }
        ////}

        ////private AgreedTermTypes fixComplianceRate;

        /////// <summary>
        /////// Tasa Fix Cumplimiento
        /////// </summary>
        ////public AgreedTermTypes FixComplianceRate
        ////{
        ////    get => fixComplianceRate;
        ////    set => SetPropertyValue(nameof(FixComplianceRate), ref fixComplianceRate, value);
        ////}

        //private decimal fixPoints;

        ///// <summary>
        ///// Puntos Fix
        ///// </summary>
        //[ModelDefault("DisplayFormat", "{0:##################,##0.0000}")]
        //[ModelDefault("EditMask", "##################,##0.0000")]
        //[DbType("decimal(23,8)")]
        //public decimal FixPoints
        //{
        //    get => fixPoints;
        //    set => SetPropertyValue(nameof(FixPoints), ref fixPoints, value);
        //}

        //private bool assignedFixRate;

        ///// <summary>
        ///// Tasa Fix Asignada
        ///// </summary>
        //public bool AssignedFixRate
        //{
        //    get => assignedFixRate;
        //    set => SetPropertyValue(nameof(AssignedFixRate), ref assignedFixRate, value);
        //}

        //private DateTime assignedDateFix;

        ///// <summary>
        ///// Fecha de Asignación Fix
        ///// </summary>
        //public DateTime AssignedDateFix
        //{
        //    get => assignedDateFix;
        //    set => SetPropertyValue(nameof(AssignedDateFix), ref assignedDateFix, value);
        //}

        //#endregion

        //[NonPersistent]
        //public bool IsTrasmitionStatusChangedBySendSetFx { get; set; }

        ///// <summary>
        ///// Administrador asociado al cliente
        ///// </summary>
        //[NonPersistent]
        //[ImmediatePostData(true)]
        //public string Administrator
        //{
        //    get
        //    {
        //        return Client != null ? Client.Administrator != null ? Client.Administrator.ClientName : string.Empty : string.Empty;
        //    }
        //}

        ///// <summary>
        ///// Indica si la Nota fue procesada correctamente
        ///// </summary>
        //[NonPersistent]
        //[ImmediatePostData(true)]
        //public bool IsNoteProcessed
        //{
        //    get
        //    {
        //        if (OperationNovelty == ForeignExchangeOperationNovelty.NotaRechazada ||
        //            OperationNovelty == ForeignExchangeOperationNovelty.ErrorGenerandoNota ||
        //            OperationNovelty == ForeignExchangeOperationNovelty.Ninguna)
        //            return false;
        //        else
        //            return true;
        //    }
        //}

        ///// <summary>
        ///// Indica si la Factura fue procesada correctamente
        ///// </summary>
        //[NonPersistent]
        //[ImmediatePostData(true)]
        //public bool IsBillProcessed
        //{
        //    get
        //    {
        //        if (BillStatus == EnumBillStatus.FacturasEnviadas)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

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
        //            if (_OperationParameters == null && ExternalInvesmentUnit?.Company != null)
        //                _OperationParameters = ForeignExchangeOperationParameterClass.GetForeignExchangeOperationParameter(Session, ExternalInvesmentUnit?.Company);

        //            if (_OperationParameters != null) return _OperationParameters.IsMarketEditableOperation;
        //            else return false;
        //        }
        //        else
        //            return false;
        //    }
        //}
    }
}

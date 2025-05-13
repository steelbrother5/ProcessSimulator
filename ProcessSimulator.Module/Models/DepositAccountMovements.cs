using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ProcessSimulator.Module.BusinessMethods;
using ProcessSimulator.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccountMovements : EnterpriseBaseObject
    {
        public DepositAccountMovements(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();


        /// <summary>
        /// Unidad Externa de Inversión
        /// </summary>
        private ExternalInvestmentUnit _externalInvestmentUnit;
        [ImmediatePostData(true)]
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get { return _externalInvestmentUnit; }
            set
            {
                if (SetPropertyValue<ExternalInvestmentUnit>("ExternalInvestmentUnit", ref _externalInvestmentUnit, value) && (!IsSaving) && (!IsLoading)
                    && _externalInvestmentUnit != null)
                {
                    OperationDate = GetClosingDateClass.GetClosingDate(Session, _externalInvestmentUnit, 1);
                }
            }
        }

        /// <summary>
        /// Portafolio
        /// </summary>        
        private Portfolio _portfolio;
        [DataSourceProperty("ExternalInvestmentUnit.Portfolios")]
        public Portfolio Portfolio
        {
            get => _portfolio;
            set => SetPropertyValue("Portfolio", ref _portfolio, value);
        }

        /// <summary>
        /// Número de Movimiento
        /// </summary> 
        private string _id;
        [Appearance("IdDisabled", Enabled = false, Criteria = "BankingConcept.Concept = 'Traslado' Or Concept = 'Traslado'")]
        [Appearance("IdEnabled", Enabled = true, Criteria = "BankingConcept.Concept != 'Traslado' Or Concept != 'Traslado'")]
        public string Id
        {
            get { return _id; }
            set { SetPropertyValue<String>("Id", ref _id, value); }
        }

        /// <summary>
        /// Posición (Venta o Compra)
        /// </summary>
        private Position _position;
        [Appearance("PositionShow", Visibility = ViewItemVisibility.Show, Criteria = "AutomaticCreation = True and Concept != 'Cámara de Compensación'")]
        [Appearance("PositionHide", Visibility = ViewItemVisibility.Hide, Criteria = "AutomaticCreation = False or Concept = 'Cámara de Compensación' ")]
        public Position Position
        {
            get { return _position; }
            set { SetPropertyValue<Position>("Position", ref _position, value); }
        }

        /// <summary>
        /// Concepto Bancario
        /// </summary>
        private BankingConcept _bankingConcept;
        [Appearance("BankingConceptShow", Visibility = ViewItemVisibility.Show, Criteria = "AutomaticCreation = False or Concept = 'Cámara de Compensación' ")]
        [Appearance("BankingConceptHide", Visibility = ViewItemVisibility.Hide, Criteria = "AutomaticCreation = True and Concept != 'Cámara de Compensación' ")]
        [Appearance("BankingConceptDisabled", Enabled = false, Criteria = "Oid != '00000000-0000-0000-0000-000000000000'")]
        [DataSourceCriteria("!AutomaticCreation or  (ShowInSearch and AutomaticCreation)")]
        [ImmediatePostData(true)]
        public BankingConcept BankingConcept
        {
            get { return _bankingConcept; }
            set
            {
                if (SetPropertyValue<BankingConcept>("BankingConcept", ref _bankingConcept, value) && (!IsSaving) && (!IsLoading)
                    && _bankingConcept != null)
                {
                    if (_bankingConcept.BankingConceptEnum == Enums.BankingConcept.Traslado)
                    {
                        MovementNature = (from movNat in new XPQuery<MovementKind>(Session)
                                          where movNat.ID == 2
                                          select movNat).FirstOrDefault();
                        Id = "";
                    }
                    else
                    {
                        if (_targetDepositAccount != null)
                            TargetDepositAccount = null;
                    }
                }
            }
        }

        /// <summary>
        /// Concepto del Movimiento
        /// </summary>
        private string _concept;
        public string Concept
        {
            get { return _concept; }
            set { SetPropertyValue<String>("Concept", ref _concept, value); }
        }

        /// <summary>
        /// Naturaleza del Movimiento
        /// </summary>
        private MovementKind _movementNature;
        [Appearance("MovementNatureDisabled", Enabled = false, Criteria = "BankingConcept.Concept = 'Traslado' || Oid != '00000000-0000-0000-0000-000000000000'")]
        [Appearance("MovementNatureEnabled", Enabled = true, Criteria = "BankingConcept.Concept != 'Traslado'")]
        public MovementKind MovementNature
        {
            get { return _movementNature; }
            set
            { SetPropertyValue<MovementKind>("MovementNature", ref _movementNature, value); }
        }

        /// <summary>
        /// Fecha de Cumplimiento
        /// </summary> 
        private DateTime _operationDate;
        public DateTime OperationDate
        {
            get { return _operationDate; }
            set
            {
                SetPropertyValue<DateTime>("OperationDate", ref _operationDate, value);
                OnChanged("ExternalInvestmentUnit");
            }
        }

        /// <summary>
        /// Cliente
        /// </summary>
        private Clients _client;
        [Appearance("ClientHide", Visibility = ViewItemVisibility.Hide)]
        public Clients Client
        {
            get { return _client; }
            set { SetPropertyValue<Clients>("Client", ref _client, value); }
        }

        /// <summary>
        /// Moneda Negociada
        /// </summary> 
        private Currency _currency;
        [Appearance("CurrencyDisabled", Enabled = false, Criteria = "Oid != '00000000-0000-0000-0000-000000000000'")]
        [ImmediatePostData(true)]
        public Currency Currency
        {
            get { return _currency; }
            set
            {
                Currency oldValue = _currency;
                if (SetPropertyValue<Currency>("Currency", ref _currency, value) && !IsLoading && !IsSaving)
                {
                    if (Currency == null)
                    {
                        DepositAccount = null;
                        TargetDepositAccount = null;
                    }
                    if (oldValue != _currency)
                    {
                        DepositAccount = null;
                        TargetDepositAccount = null;
                    }
                }
            }
        }

        /// <summary>
        /// Forma de Pago
        /// </summary>  
        private CurrencyVehicle _currencyVehicle;
        public CurrencyVehicle CurrencyVehicle
        {
            get { return _currencyVehicle; }
            set { SetPropertyValue<CurrencyVehicle>("CurrencyVehicle", ref _currencyVehicle, value); }
        }

        /// <summary>
        /// Valor del Movimiento
        /// </summary>
        private decimal _faceValue;
        [Appearance("FaceValueDisabled", Enabled = false, Criteria = "!AutomaticCreation && !IsNullOrEmpty(DepositAccount) && Oid != null")]
        public decimal FaceValue
        {
            get { return _faceValue; }
            set { SetPropertyValue<Decimal>("FaceValue", ref _faceValue, value); }
        }

        /// <summary>
        /// Cuenta Bancaria
        /// </summary>  
        private DepositAccount _depositAccount;
        [DataSourceCriteria("Currency = '@This.Currency'")]
        [Appearance("DepositAccountDisabledFalse", Enabled = false, Criteria = "IsNullOrEmpty(Currency)")]
        [ImmediatePostData(true)]
        public DepositAccount DepositAccount
        {
            get { return _depositAccount; }
            set
            {
                DepositAccount oldValue = _depositAccount;
                if (SetPropertyValue<DepositAccount>("DepositAccount", ref _depositAccount, value) && !IsLoading && !IsSaving)
                {
                    if (oldValue != _depositAccount)
                    {
                        TargetDepositAccount = null;
                    }
                }
            }
        }

        /// <summary>
        /// Cuenta Bancaria Anterior
        /// </summary>
        private DepositAccount _oldDepositAccount;
        [Appearance("OldDepositAccountHide", Visibility = ViewItemVisibility.Hide)]
        public DepositAccount OldDepositAccount
        {
            get { return _oldDepositAccount; }
            set { SetPropertyValue<DepositAccount>("OldDepositAccount", ref _oldDepositAccount, value); }
        }

        /// <summary>
        /// Cuenta Bancaria Destino
        /// </summary>
        private DepositAccount _targetDepositAccount;
        [DataSourceCriteria("Currency == '@This.Currency' && Oid != '@This.DepositAccount.Oid'")]
        public DepositAccount TargetDepositAccount
        {
            get { return _targetDepositAccount; }
            set { SetPropertyValue<DepositAccount>("TargetDepositAccount", ref _targetDepositAccount, value); }
        }

        /// <summary>
        /// Cuenta Bancaria Destino Anterior
        /// </summary>
        private DepositAccount _oldTargetDepositAccount;
        [Appearance("OldTargetDepositAccountHide", Visibility = ViewItemVisibility.Hide)]
        public DepositAccount OldTargetDepositAccount
        {
            get { return _oldTargetDepositAccount; }
            set { SetPropertyValue<DepositAccount>("OldTargetDepositAccount", ref _oldTargetDepositAccount, value); }
        }

        /// <summary>
        /// Operación de Divisas
        /// </summary>
        private ForeignExchangeOperation _foreignExchangeOperation;
        public ForeignExchangeOperation ForeignExchangeOperation
        {
            get { return _foreignExchangeOperation; }
            set { SetPropertyValue<ForeignExchangeOperation>("ForeignExchangeOperation", ref _foreignExchangeOperation, value); }
        }

        /// <summary>
        /// Indica si el movimiento fue creado automáticamente
        /// </summary>
        private bool _automaticCreation;
        [Appearance("AutomaticCreationHide", Visibility = ViewItemVisibility.Hide)]
        public bool AutomaticCreation
        {
            get { return _automaticCreation; }
            set { SetPropertyValue<Boolean>("AutomaticCreation", ref _automaticCreation, value); }
        }

        /// <summary>
        /// Estatus del deposito
        /// </summary>
        private DepositAccStatus _depositAccStatus;
        [Appearance("DepositAccStatusDisabledFalse", Enabled = false)]
        public DepositAccStatus DepositAccStatus
        {
            get { return _depositAccStatus; }
            set { SetPropertyValue<DepositAccStatus>("DepositAccStatus", ref _depositAccStatus, value); }
        }

        /// <summary>
        /// Operación ME/ME
        /// </summary>
        private ForeignCurrencyTrade _foreignCurrencyTrade;
        public ForeignCurrencyTrade ForeignCurrencyTrade
        {
            get { return _foreignCurrencyTrade; }
            set { SetPropertyValue("ForeignCurrencyTrade", ref _foreignCurrencyTrade, value); }
        }
    }
}

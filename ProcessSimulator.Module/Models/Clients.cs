using DevExpress.Persistent.Base;
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
    public class Clients : EnterpriseBaseObject
    {
        public Clients(Session session) : base(session) { }

        private Persons person;
        /// <summary>
        /// Persona asociada
        /// </summary>
        //[Association("Person-Clients")]
        [ImmediatePostData(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Persons Person
        {
            get => person;
            set
            {
                if (SetPropertyValue("Person", ref person, value) && !IsLoading && !IsSaving)
                {
                    ClientName = person?.FullName?.Trim();
                    Address = person?.Address;
                    //City = person?.City;
                    PhoneNumber = person?.PhoneNumber;
                    Email = person?.Email;
                    MobilePhoneNumber = person?.MobilePhoneNumber;
                }
            }
        }

        private string clientName;
        /// <summary>
        /// Nombre
        /// </summary>
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccentsAndPoint, CustomMessageTemplate = "El Nombre solo puede contener letras, números, guiones, tilde, punto y apostrofe. ( &,-, ' y espacios)")]
        public string ClientName
        {
            get => clientName?.Trim();
            set => SetPropertyValue("ClientName", ref clientName, value);
        }

        private string address;
        /// <summary>
        /// Dirección
        /// </summary>
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenDotSlashComma, CustomMessageTemplate = "La Direccion solo puede contener letras, números, numeral, slash, puntos, guiones y apostrofe. ( #, -, ., / ,. Y espacios )")]
        public string Address
        {
            get => address;
            set => SetPropertyValue("Address", ref address, value);
        }

        //private Cities city;
        ///// <summary>
        ///// Identificador de la ciudad
        ///// </summary>
        //public Cities City
        //{
        //    get => city;
        //    set => SetPropertyValue("City", ref city, value);
        //}

        private string phoneNumber;
        /// <summary>
        /// Número de teléfono
        /// </summary>
        [Size(25)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHypenMost, CustomMessageTemplate = "El Número de teléfono solo puede contener números, más y menos.  ( + y -)")]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetPropertyValue("PhoneNumber", ref phoneNumber, value);
        }

        private string email;
        /// <summary>
        /// Correo electrónico
        /// </summary>
        [Size(200)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHyphenMostUnderlineDotAt, CustomMessageTemplate = "El Email solo puede contener letras, números, más y menos.  ( @, ., _, -  Y + )")]
        public string Email
        {
            get => email;
            set => SetPropertyValue("Email", ref email, value);
        }

        private string mobilePhoneNumber;
        /// <summary>
        /// Número de celular
        /// </summary>
        [Size(25)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHyphenMostUnderline, CustomMessageTemplate = "El Número de Celular solo puede contener números, más y menos.  ( _ + y -)")]
        public string MobilePhoneNumber
        {
            get => mobilePhoneNumber;
            set => SetPropertyValue("MobilePhoneNumber", ref mobilePhoneNumber, value);
        }

        private DateTime lastUpdateDate;
        /// <summary>
        /// Fecha ultima actualizaci´pn
        /// </summary>
        public DateTime LastUpdateDate
        {
            get => lastUpdateDate;
            set => SetPropertyValue<DateTime>("LastUpdateDate", ref lastUpdateDate, value);
        }

        private string accountNumber;
        /// <summary>
        /// Número de cuenta
        /// </summary>
        [Size(50)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Indexed(Unique = true)]
        [RuleUniqueValue("UniqueAccountNumber",
                         DefaultContexts.Save,
                         "El Número de cuenta diligenciado ya está asociado a un cliente.",
                         CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHypenUnderline, CustomMessageTemplate = "El Número de Cuenta solo puede contener números, guión y ghuión bajo.  ( _ y -)")]
        public string AccountNumber
        {
            get => accountNumber;
            set => SetPropertyValue("AccountNumber", ref accountNumber, value);
        }

        private DateTime openingDate;
        /// <summary>
        /// Fecha de apertura
        /// </summary>
        public DateTime OpeningDate
        {
            get => openingDate;
            set => SetPropertyValue<DateTime>("OpeningDate", ref openingDate, value);
        }

        //private BranchOffice branchOffice;
        ///// <summary>
        ///// Sucursal
        ///// </summary>
        //public BranchOffice BranchOffice
        //{
        //    get => branchOffice;
        //    set => SetPropertyValue("BranchOffice", ref branchOffice, value);
        //}

        //private MarketSector marketSector;
        ///// <summary>
        ///// Sector del mercado
        ///// </summary>
        //public MarketSector MarketSector
        //{
        //    get => marketSector;
        //    set => SetPropertyValue("MarketSector", ref marketSector, value);
        //}

        private string setFXCode;
        /// <summary>
        /// Código asignado en SETFX
        /// </summary>
        public string SetFXCode
        {
            get => setFXCode;
            set => SetPropertyValue("SetFXCode", ref setFXCode, value);
        }

        //private CIIU cIIUCode;
        ///// <summary>
        ///// Código CIUU
        ///// </summary>
        //public CIIU CIIUCode
        //{
        //    get => cIIUCode;
        //    set => SetPropertyValue("CIIUCode", ref cIIUCode, value);
        //}

        private string iMCCode;
        /// <summary>
        /// Código de intermediario del mercado cambiario
        /// </summary>
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphen, CustomMessageTemplate = "El Código IMC solo puede contener letras, números y menos. ( -  Y Espacios )")]
        public string IMCCode
        {
            get => iMCCode;
            set => SetPropertyValue("IMCCode", ref iMCCode, value);
        }

        //private EntityTypes entityType;
        ///// <summary>
        ///// Tipo de entidad
        ///// </summary>
        //[RuleRequiredField("EntityTypeRequired", DefaultContexts.Save,
        //                   TargetCriteria = "Person.PersonType.PersonTypeCode == '02'")]
        //public EntityTypes EntityType
        //{
        //    get => entityType;
        //    set => SetPropertyValue("EntityType", ref entityType, value);
        //}

        private bool isGuarded;
        /// <summary>
        /// Vigilado
        /// </summary>
        public bool IsGuarded
        {
            get => isGuarded;
            set => SetPropertyValue("IsGuarded", ref isGuarded, value);
        }

        private Clients administrator;
        /// <summary>
        /// Administrador
        /// </summary>
        [DataSourceCriteria("Oid != '@This.Oid'")]
        public Clients Administrator
        {
            get => administrator;
            set => SetPropertyValue<Clients>(nameof(Administrator), ref administrator, value);
        }

        //[Association("Clients-BankAccountsClient"), Aggregated]
        //public XPCollection<BankAccountsClient> BankAccounts
        //    => GetCollection<BankAccountsClient>("BankAccounts");

        //[Association("Clients-ClientBankAccounts"), Aggregated]
        //public XPCollection<ClientBankAccounts> ClientBankAccounts
        //    => GetCollection<ClientBankAccounts>("ClientBankAccounts");

        //[Association("Clients_Declarants", UseAssociationNameAsIntermediateTableName = true)]
        //public XPCollection<Declarant> Declarants
        //    => GetCollection<Declarant>("Declarants");

        //[Association("Clients_Ordenants", UseAssociationNameAsIntermediateTableName = true)]
        //[DataSourceCriteria("Person.PersonType.PersonTypeCode = '01'")]
        //public XPCollection<Persons> Ordenants
        //    => GetCollection<Persons>("Ordenants");

        //private enumStatusClient enumStatusClient;
        ///// <summary>
        ///// Estado del Cliente Activo, Inactivo, Bloqueado
        ///// </summary>
        //public enumStatusClient EnumStatusClient
        //{
        //    get { return enumStatusClient; }
        //    set { SetPropertyValue(nameof(EnumStatusClient), ref enumStatusClient, value); }
        //}

        ///// <summary>
        ///// Trader asociados al cliente
        ///// </summary>
        //[Association("Client-Clients_Traders"), Aggregated]
        //public XPCollection<Traders_Clients> Traders => GetCollection<Traders_Clients>(nameof(Traders));

        //#region Información Adicional

        //private TypeOfBusiness typesOfBusiness;

        ///// <summary>
        ///// Tipo de empresa
        ///// </summary>
        //public TypeOfBusiness TypesOfBusiness
        //{
        //    get => typesOfBusiness;
        //    set => SetPropertyValue(nameof(TypesOfBusiness), ref typesOfBusiness, value);
        //}

        //private SupervisingAuthority supervisingAuthority;

        ///// <summary>
        ///// Superintendencia Vigilante
        ///// </summary>
        //public SupervisingAuthority SupervisingAuthority
        //{
        //    get => supervisingAuthority;
        //    set => SetPropertyValue(nameof(SupervisingAuthority), ref supervisingAuthority, value);
        //}

        //private RegulatoryFrameworkType regulatoryFrameworkTypes;

        ///// <summary>
        ///// Regimen regulatorio
        ///// </summary>
        //public RegulatoryFrameworkType RegulatoryFrameworkTypes
        //{
        //    get => regulatoryFrameworkTypes;
        //    set => SetPropertyValue(nameof(RegulatoryFrameworkTypes), ref regulatoryFrameworkTypes, value);
        //}

        //private ForeignBranchActivity foreignBranchActivity;

        ///// <summary>
        ///// Actividad de Sucursal Extranjera
        ///// </summary>
        //public ForeignBranchActivity ForeignBranchActivity
        //{
        //    get => foreignBranchActivity;
        //    set => SetPropertyValue(nameof(ForeignBranchActivity), ref foreignBranchActivity, value);
        //}

        //private FinancialInstitutionTypes financialInstitutionTypes;

        ///// <summary>
        ///// Entidad Financiera
        ///// </summary>
        //public FinancialInstitutionTypes FinancialInstitutionTypes
        //{
        //    get => financialInstitutionTypes;
        //    set => SetPropertyValue(nameof(FinancialInstitutionTypes), ref financialInstitutionTypes, value);
        //}

        //private EntitySector entitySector;

        ///// <summary>
        ///// Sector
        ///// </summary>
        //public EntitySector EntitySector
        //{
        //    get => entitySector;
        //    set => SetPropertyValue(nameof(EntitySector), ref entitySector, value);
        //}

        //#endregion

        private string setFxId;

        /// <summary>
        /// Id del SetFX
        /// </summary>
        [Size(100)]
        public string SetFxId
        {
            get => setFxId;
            set => SetPropertyValue(nameof(SetFxId), ref setFxId, value);
        }

        /// <summary>
        /// Propiedad no persistente que indica si el cliente ingresado puede registrar el id setFx
        /// </summary>
        //[NonPersistent]
        //public bool SetFxIdEnable
        //{
        //    get => ValidationHelper.ActiveParameter(Session, new List<string> { "Rol Administrador ComEx", "Registrar Id Set Fx" });
        //}
    }
}

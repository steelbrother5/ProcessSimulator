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
    public class Persons : EnterpriseBaseObject
    {
        public Persons(Session session) : base(session) { }

        private string identificationNumber;
        /// <summary>
        /// Número de Identificación
        /// </summary>
        [Size(30)]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenBackslash, CustomMessageTemplate = "El Número de Identificación solo puede contener letras, números, guiones y slash. (- y /)")]
        public string IdentificationNumber
        {
            get => identificationNumber;
            set
            {
                if (SetPropertyValue("IdentificationNumber", ref identificationNumber, value) && !IsLoading && !IsSaving) { }

            }
        }

        private int? dV;
        /// <summary>
        /// Digito de Verificación
        /// </summary>
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DocumentType.DocumentTypeCode == 2")]
        public int? DV
        {
            get => dV;
            set => SetPropertyValue("DV", ref dV, value);
        }

        private string legalName;
        /// <summary>
        /// Razon Social
        /// </summary>
        [ImmediatePostData(true)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenDotSlashCommaWithAccents, CustomMessageTemplate = "La Razón Social solo puede contener letras, números, guiones, slash y &. (&, -, ., , ñ, tildes y espacios)")]
        public string LegalName
        {
            get => legalName?.Trim();
            set => SetPropertyValue("LegalName", ref legalName, value);
        }

        private string firstName;
        /// <summary>
        /// Primer Nombre
        /// </summary>
        [ImmediatePostData(true)]
        [Size(50)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccents, CustomMessageTemplate = "El Primer Nombre solo puede contener letras, números, guiones, tildes y apostrofe. ( -, ' y espacios)")]
        public string FirstName
        {
            get => firstName?.Trim();
            set => SetPropertyValue("FirstName", ref firstName, value);
        }

        private string secondName;
        /// <summary>
        /// Segundo Nombre
        /// </summary>
        [ImmediatePostData(true)]
        [Size(50)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccents, CustomMessageTemplate = "El Segundo Nombre solo puede contener letras, números, guiones, tildes y apostrofe. ( -, ' y espacios)")]
        public string SecondName
        {
            get => secondName?.Trim();
            set => SetPropertyValue("SecondName", ref secondName, value);
        }

        private string firstSurname;
        /// <summary>
        /// Primer Apellido
        /// </summary>
        [ImmediatePostData(true)]
        [Size(50)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccents, CustomMessageTemplate = "El Primer Apellido solo puede contener letras, números, guiones, tildes y apostrofe. ( -, ' y espacios)")]
        public string FirstSurname
        {
            get => firstSurname?.Trim();
            set => SetPropertyValue("FirstSurname", ref firstSurname, value);
        }

        private string secondSurname;
        /// <summary>
        /// Segundo Apellido
        /// </summary>
        [ImmediatePostData(true)]
        [Size(50)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccents, CustomMessageTemplate = "El Segundo Apellido solo puede contener letras, números, guiones, tildes y apostrofe. ( -, ' y espacios)")]
        public string SecondSurname
        {
            get
            { return secondSurname?.Trim(); }
            set => SetPropertyValue("SecondSurname", ref secondSurname, value);
        }

        private string fullName;
        /// <summary>
        /// Nombre Completo
        /// </summary>
        [ImmediatePostData(true)]
        public string FullName
        {
            get
            {
                fullName = GetFullName();
                return fullName;
            }
            set => SetPropertyValue("FullName", ref fullName, value);
        }

        private string address;
        /// <summary>
        /// Dirección
        /// </summary>
        [Size(100)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenDotSlashComma, CustomMessageTemplate = "La Direccion solo puede contener letras, números, numeral, slash, puntos, guiones y apostrofe. (#, -, ., / ,. Y espacios )")]
        public string Address
        {
            get => address;
            set => SetPropertyValue("Address", ref address, value);
        }

        private Countries country;
        /// <summary>
        /// Identificador del País
        /// </summary>
        public Countries Country
        {
            get => country;
            set => SetPropertyValue("Country", ref country, value);
        }

        private string phoneNumber;
        /// <summary>
        /// Número de teléfono
        /// </summary>
        [Size(25)]
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHyphenMostUnderline, CustomMessageTemplate = "El Número de teléfono solo puede contener números, más y menos.  ( _  + y -)")]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetPropertyValue("PhoneNumber", ref phoneNumber, value);
        }

        private string email;
        /// <summary>
        /// Número de teléfono
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
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.NumbersHypenMost, CustomMessageTemplate = "El Número de Celular solo puede contener números, más y menos.  ( + y -)")]
        public string MobilePhoneNumber
        {
            get => mobilePhoneNumber;
            set => SetPropertyValue("MobilePhoneNumber", ref mobilePhoneNumber, value);
        }

        private DateTime lastUpdateDate;
        /// <summary>
        /// Última fecha de actualización
        /// </summary>
        public DateTime LastUpdateDate
        {
            get => lastUpdateDate;
            set => SetPropertyValue<DateTime>("LastUpdateDate", ref lastUpdateDate, value);
        }



        /// <summary>
        /// Método que concatena los nombres para obtener el Nombre Completo de la persona
        /// </summary>
        private string GetFullName()
            => string.Join(" ",
                           new string[5]
                           {
                               FirstSurname,
                               SecondSurname,
                               FirstName,
                               SecondName,
                               LegalName
                           }
                           .Where(_string => !string.IsNullOrEmpty(_string)))
                           .Trim();
    }
}

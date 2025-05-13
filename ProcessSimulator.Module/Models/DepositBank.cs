using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositBank : EnterpriseBaseObject
    {
        private string code;
        private string name;
        private string city;
        private Countries country;
        private string swiftBankCode;
        private string operationAgentCode;
        private Persons registeredBank;

        public DepositBank(Session session) : base(session) { }

        public string Code
        {
            get { return code; }
            set { SetPropertyValue("Code", ref code, value); }
        }

        [Appearance("NameDisabled", Enabled = false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Name
        {
            get { return name; }
            set
            {
                SetPropertyValue("Name", ref name, value);
                OnChanged("RegisteredBank");
            }
        }

        public string City
        {
            get { return city; }
            set { SetPropertyValue("City", ref city, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Countries Country
        {
            get { return country; }
            set { SetPropertyValue("Country", ref country, value); }
        }

        public string SwiftBankCode
        {
            get { return swiftBankCode; }
            set { SetPropertyValue("SwiftBankCode", ref swiftBankCode, value); }
        }

        /// <summary>
        /// Codigo Agente Operador - Entidades del Exterior SFC.
        /// </summary>
        [Size(3)]
        public string OperationAgentCode
        {
            get { return operationAgentCode; }
            set { SetPropertyValue("OperationAgentCode", ref operationAgentCode, value); }
        }

        [Association("DepositBank-DepositAccounts")]
        public XPCollection<DepositAccount> DepositAccounts
        {
            get { return GetCollection<DepositAccount>("DepositAccounts"); }
        }

        [ImmediatePostData(true)]
        public Persons RegisteredBank
        {
            get { return registeredBank; }
            set
            {
                if (SetPropertyValue("RegisteredBank", ref registeredBank, value)
                    && registeredBank != null && registeredBank.FullName != null && registeredBank.FullName != "" && !IsSaving)
                {
                    Name = registeredBank.FullName;
                }
            }
        }
    }
}

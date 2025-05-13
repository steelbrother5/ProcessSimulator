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
    public class DepositAccount : EnterpriseBaseObject
    {
        public DepositAccount(Session session) : base(session) { }

        public override void AfterConstruction() => base.AfterConstruction();

        public string AccountLabel
        {
            get
            {
                if (DepositBank != null && Country != null && BankAccountType != null && DepositAccountCode != null && Currency != null)
                    return DepositBank.Name + "/" + Country.Name + "/" + BankAccountType + "/" + DepositAccountCode + "/" + Currency.Code;
                else
                    return "";
            }
        }

        private string depositAccountCode;
        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersHyphenApostrophe, CustomMessageTemplate = "El número de cuenta solo puede contener letras, números, guiones y apostrofe. ( -, ' y espacios)")]
        public string DepositAccountCode
        {
            get => depositAccountCode;
            set
            {
                if (SetPropertyValue("DepositAccountCode", ref depositAccountCode, value))
                    OnChanged("AccountLabel");
            }
        }

        private DateTime lastLoadedClosingDate;
        public DateTime LastLoadedClosingDate
        {
            get => lastLoadedClosingDate;
            set => SetPropertyValue("LastLoadedClosingDate", ref lastLoadedClosingDate, value);
        }

        private decimal lastClosingCalculatedBalance;
        public decimal LastClosingCalculatedBalance
        {
            get => lastClosingCalculatedBalance;
            set => SetPropertyValue("LastClosingCalculatedBalance", ref lastClosingCalculatedBalance, value);
        }

        private decimal openingBalance;
        public decimal OpeningBalance
        {
            get => openingBalance;
            set => SetPropertyValue("OpeningBalance", ref openingBalance, value);
        }

        private bool excludeProfitsAndLossesCalculations;
        public bool ExcludeProfitsAndLossesCalculations
        {
            get => excludeProfitsAndLossesCalculations;
            set => SetPropertyValue("ExcludeProfitsAndLossesCalculations", ref excludeProfitsAndLossesCalculations, value);
        }

        private Currency currency;
        [RuleRequiredField(DefaultContexts.Save)]
        public Currency Currency
        {
            get => currency;
            set => SetPropertyValue("Currency", ref currency, value);
        }

        DepositAccountLoadingMaster depositAccountLoadingMaster = null;
        public DepositAccountLoadingMaster DepositAccountLoadingMaster
        {
            get => depositAccountLoadingMaster;
            set
            {
                if (depositAccountLoadingMaster == value)
                    return;

                DepositAccountLoadingMaster prevDepositAccountLoadingMaster = depositAccountLoadingMaster;
                depositAccountLoadingMaster = value;

                if (IsLoading) return;
                if (prevDepositAccountLoadingMaster != null && prevDepositAccountLoadingMaster.DepositAccount == this)
                    prevDepositAccountLoadingMaster.DepositAccount = null;
                if (depositAccountLoadingMaster != null)
                    depositAccountLoadingMaster.DepositAccount = this;
                OnChanged("DepositAccountLoadingMaster");

            }
        }

        private BankAccountType bankAccountType;
        [RuleRequiredField(DefaultContexts.Save)]
        public BankAccountType BankAccountType
        {
            get => bankAccountType;
            set => SetPropertyValue("BankAccountType", ref bankAccountType, value);
        }

        private Countries country;
        public Countries Country
        {
            get => country;
            set => SetPropertyValue("Country", ref country, value);
        }

        private ExternalInvestmentUnit externalInvestmentUnit;
        [Association("ExternalInvestmentUnit-DepositAccount", typeof(ExternalInvestmentUnit))]
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get => externalInvestmentUnit;
            set => SetPropertyValue("ExternalInvestmentUnit", ref externalInvestmentUnit, value);
        }

        private DepositBank depositBank;
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("DepositBank-DepositAccounts")]
        [ImmediatePostData(true)]
        public DepositBank DepositBank
        {
            get => depositBank;
            set
            {
                if (SetPropertyValue("DepositBank", ref depositBank, value))
                    OnChanged("AccountLabel");
            }
        }
    }
}

using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccountStatementMovement : EnterpriseBaseObject
    {
        private string iD;
        private DepositAccountStatement fDepositAccountStatement;
        private StatementStandardConcept standardConcept;
        private string bankConcept;
        private string documentNumber;
        private decimal debit;
        private decimal credit;
        private decimal valueInCash;
        private decimal valueInCheck;
        private bool isConciled;

        public DepositAccountStatementMovement(Session session) : base(session) { }

        public string ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        [Association("DepositAccountStatement-DepositAccountStatementMovement", typeof(DepositAccountStatement))]
        public DepositAccountStatement DepositAccountStatement
        {
            get { return fDepositAccountStatement; }
            set
            {
                DepositAccountStatement oldDepositAccountStatement =
                    fDepositAccountStatement;
                SetPropertyValue("DepositAccountStatement", ref fDepositAccountStatement, value);
                if (!IsLoading && !IsSaving && oldDepositAccountStatement != fDepositAccountStatement)
                {
                    oldDepositAccountStatement =
                        oldDepositAccountStatement ?? fDepositAccountStatement;
                    oldDepositAccountStatement.UpdateStatementConciliationSummary(true);
                }
            }
        }

        public StatementStandardConcept StandardConcept
        {
            get { return standardConcept; }
            set { SetPropertyValue("StandardConcept", ref standardConcept, value); }
        }

        public string BankConcept
        {
            get { return bankConcept; }
            set { SetPropertyValue("BankConcept", ref bankConcept, value); }
        }

        public string DocumentNumber
        {
            get { return documentNumber; }
            set { SetPropertyValue("DocumentNumber", ref documentNumber, value); }
        }

        public Decimal Debit
        {
            get { return debit; }
            set { SetPropertyValue("Debit", ref debit, value); }
        }

        public Decimal Credit
        {
            get { return credit; }
            set { SetPropertyValue("Credit", ref credit, value); }
        }

        public Decimal ValueInCash
        {
            get { return valueInCash; }
            set { SetPropertyValue("ValueInCash", ref valueInCash, value); }
        }

        public Decimal ValueInCheck
        {
            get { return valueInCheck; }
            set { SetPropertyValue("ValueInCheck", ref valueInCheck, value); }
        }

        public bool IsConciled
        {
            get { return isConciled; }
            set { SetPropertyValue("IsConciled", ref isConciled, value); }
        }

        /// <summary>
        /// Relación 1-1 con ConciliationRecord 
        /// </summary>
        ConciliationRecord conciliationRecord = null;
        public ConciliationRecord ConciliationRecord
        {
            get { return conciliationRecord; }
            set
            {
                if (conciliationRecord == value)
                    return;

                ConciliationRecord prevConciliationRecord = conciliationRecord;
                conciliationRecord = value;

                if (IsLoading) return;

                if (prevConciliationRecord != null && prevConciliationRecord.DepositAccountStatementMovement == this)
                    prevConciliationRecord.DepositAccountStatementMovement = null;

                if (conciliationRecord != null)
                    conciliationRecord.DepositAccountStatementMovement = this;
                OnChanged("ConciliationRecord");
            }
        }
    }
}

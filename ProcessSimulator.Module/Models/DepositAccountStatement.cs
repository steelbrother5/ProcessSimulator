using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccountStatement : EnterpriseBaseObject
    {
        private string iD;
        private string description;
        private DepositAccount depositAccount;
        private DateTime openingDate;
        private DateTime closingDate;
        private Decimal openingBalance;
        private Decimal closingBalance;
        private bool isLoaded;
        private bool isRead;
        private bool isOnlyClosingBalance;
        private FileData statementFile;
        private StatementConciliationSummary fStatementConciliationSummary = null;


        public DepositAccountStatement(Session session) : base(session) { }

        [Association("DepositAccountStatement-DepositAccountStatementMovement"), Aggregated]
        public XPCollection<DepositAccountStatementMovement> DepositAccountStatementMovements
        {
            get { return GetCollection<DepositAccountStatementMovement>("DepositAccountStatementMovements"); }
        }

        public string ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }

        public DepositAccount DepositAccount
        {
            get { return depositAccount; }
            set { SetPropertyValue("DepositAccount", ref depositAccount, value); }
        }

        public DateTime OpeningDate
        {
            get { return openingDate; }
            set { SetPropertyValue("OpeningDate", ref openingDate, value); }
        }

        public DateTime ClosingDate
        {
            get { return closingDate; }
            set { SetPropertyValue("ClosingDate", ref closingDate, value); }
        }

        public Decimal OpeningBalance
        {
            get { return openingBalance; }
            set { SetPropertyValue("OpeningBalance", ref openingBalance, value); }
        }

        public Decimal ClosingBalance
        {
            get { return closingBalance; }
            set { SetPropertyValue("ClosingBalance", ref closingBalance, value); }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { SetPropertyValue("IsLoaded", ref isLoaded, value); }
        }

        public bool IsRead
        {
            get { return isRead; }
            set { SetPropertyValue("IsRead", ref isRead, value); }
        }

        public bool IsOnlyClosingBalance
        {
            get { return isOnlyClosingBalance; }
            set { SetPropertyValue("IsOnlyClosingBalance", ref isOnlyClosingBalance, value); }
        }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData StatementFile
        {
            get { return statementFile; }
            set
            {
                SetPropertyValue("StatementFile", ref statementFile, value);
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfMovements")]
        public int NumberOfMovements
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfMovements");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfDebits")]
        public int NumberOfDebits
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfDebits");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfCredits")]
        public int NumberOfCredits
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfCredits");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.DebitsValue")]
        public decimal DebitsValue
        {
            get
            {
                object tempObject = EvaluateAlias("DebitsValue");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.CreditsValue")]
        public decimal CreditsValue
        {
            get
            {
                object tempObject = EvaluateAlias("CreditsValue");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfConciledMovements")]
        public int NumberOfConciledMovements
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfConciledMovements");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfConciledDebits")]
        public int NumberOfConciledDebits
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfConciledDebits");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfConciledCredits")]
        public int NumberOfConciledCredits
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfConciledCredits");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.ConciledDebitsValue")]
        public decimal ConciledDebitsValue
        {
            get
            {
                object tempObject = EvaluateAlias("ConciledDebitsValue");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.ConciliationDebitsValueDifference")]
        public decimal ConciliationDebitsValueDifference
        {
            get
            {
                object tempObject = EvaluateAlias("ConciliationDebitsValueDifference");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.ConciledCreditsValue")]
        public decimal ConciledCreditsValue
        {
            get
            {
                object tempObject = EvaluateAlias("ConciledCreditsValue");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.ConciliationCreditsValueDifference")]
        public decimal ConciliationCreditsValueDifference
        {
            get
            {
                object tempObject = EvaluateAlias("ConciliationCreditsValueDifference");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.TotalConciliationValueDifference")]
        public decimal TotalConciliationValueDifference
        {
            get
            {
                object tempObject = EvaluateAlias("TotalConciliationValueDifference");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.NumberOfUnconciledMovements")]
        public int NumberOfUnconciledMovements
        {
            get
            {
                object tempObject = EvaluateAlias("NumberOfUnconciledMovements");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("StatementConciliationSummary.IsConciled")]
        public bool IsConciled
        {
            get
            {
                object tempObject = EvaluateAlias("IsConciled");
                if (tempObject != null)
                {
                    return (bool)tempObject;
                }
                else
                {
                    return false;
                }
            }
        }



        /// <summary>
        /// Relación 1-1 con el DepositAccountStatement que se cargó
        /// </summary>
        DepositAccountLoading depositAccountLoading = null;
        public DepositAccountLoading DepositAccountLoading
        {
            get { return depositAccountLoading; }
            set
            {
                if (depositAccountLoading == value)
                    return;

                DepositAccountLoading prevDepositAccountLoading = depositAccountLoading;
                depositAccountLoading = value;

                if (IsLoading) return;

                if (prevDepositAccountLoading != null && prevDepositAccountLoading.DepositAccountStatement == this)
                    prevDepositAccountLoading.DepositAccountStatement = null;

                if (depositAccountLoading != null)
                    depositAccountLoading.DepositAccountStatement = this;
                OnChanged("DepositAccountLoading");


            }
        }

        [Appearance("Hide", Visibility = ViewItemVisibility.Hide)]
        public StatementConciliationSummary StatementConciliationSummary
        {
            get
            {
                if (!IsLoading && !IsSaving && fStatementConciliationSummary == null)
                    UpdateStatementConciliationSummary(false);
                return fStatementConciliationSummary;
            }
        }

        public void UpdateStatementConciliationSummary(bool forceChangeEvents)
        {
            StatementConciliationSummary oldStatementConciliationSummary = fStatementConciliationSummary;

            fStatementConciliationSummary = new StatementConciliationSummary(Session);

            int numberOfDebits = 0;
            int numberOfCredits = 0;
            decimal debitsValue = 0.0m;
            decimal creditsValue = 0.0m;
            int numberOfConciledMovements = 0;
            int numberOfConciledDebits = 0;
            int numberOfConciledCredits = 0;
            decimal conciledDebitsValue = 0.0m;
            decimal conciliationDebitsValueDifference = 0.0m;
            decimal conciledCreditsValue = 0.0m;
            decimal conciliationCreditsValueDifference = 0.0m;
            decimal totalConciliationValueDifference = 0.0m;

            fStatementConciliationSummary.NumberOfMovements = DepositAccountStatementMovements.Count;

            if (DepositAccountStatementMovements.Count > 0)
            {
                foreach (DepositAccountStatementMovement depositAccountStatementMovement in DepositAccountStatementMovements)
                {
                    if (depositAccountStatementMovement.Debit > 0.0m)
                    {
                        numberOfDebits += 1;
                        debitsValue += depositAccountStatementMovement.Debit;
                        if (depositAccountStatementMovement.IsConciled)
                        {
                            numberOfConciledMovements += 1;
                            numberOfConciledDebits += 1;
                            conciledDebitsValue += depositAccountStatementMovement.Debit;
                            conciliationDebitsValueDifference += depositAccountStatementMovement.ConciliationRecord.ConciliationDifference;
                            totalConciliationValueDifference += depositAccountStatementMovement.ConciliationRecord.ConciliationDifference;
                        }
                    }
                    else
                    {
                        numberOfCredits += 1;
                        creditsValue += depositAccountStatementMovement.Credit;
                        if (depositAccountStatementMovement.IsConciled)
                        {
                            if (depositAccountStatementMovement.ConciliationRecord != null)
                            {
                                numberOfConciledMovements += 1;
                                numberOfConciledCredits += 1;
                                conciledCreditsValue += depositAccountStatementMovement.Credit;
                                conciliationCreditsValueDifference += depositAccountStatementMovement.ConciliationRecord.ConciliationDifference;
                                totalConciliationValueDifference += depositAccountStatementMovement.ConciliationRecord.ConciliationDifference;
                            }
                            else
                            {
                                throw new Exception("Error de Integridad (DepositAccountStatement - UpdateStatementConciliationSummary)"
                                                     + " hay un depositAccountStatementMovement.IsConciled con "
                                                     + "depositAccountStatementMovement.ConciliationRecord = null. ID: "
                                                     + depositAccountStatementMovement.ID);
                            }

                        }
                    }
                }

                fStatementConciliationSummary.NumberOfDebits = numberOfDebits;
                fStatementConciliationSummary.NumberOfCredits = numberOfCredits;
                fStatementConciliationSummary.DebitsValue = debitsValue;
                fStatementConciliationSummary.CreditsValue = creditsValue;
                fStatementConciliationSummary.NumberOfConciledMovements = numberOfConciledMovements;
                fStatementConciliationSummary.NumberOfConciledDebits = numberOfConciledDebits;
                fStatementConciliationSummary.NumberOfConciledCredits = numberOfConciledCredits;
                fStatementConciliationSummary.ConciledDebitsValue = conciledDebitsValue;
                fStatementConciliationSummary.ConciliationDebitsValueDifference = conciliationDebitsValueDifference;
                fStatementConciliationSummary.ConciledCreditsValue = conciledCreditsValue;
                fStatementConciliationSummary.ConciliationCreditsValueDifference = conciliationCreditsValueDifference;
                fStatementConciliationSummary.TotalConciliationValueDifference = totalConciliationValueDifference;

                fStatementConciliationSummary.NumberOfUnconciledMovements =
                    fStatementConciliationSummary.NumberOfMovements - fStatementConciliationSummary.NumberOfConciledMovements;
                fStatementConciliationSummary.IsConciled = fStatementConciliationSummary.NumberOfUnconciledMovements == 0;
            }

            if (forceChangeEvents)
            {
                OnChanged("StatementConciliationSummary", oldStatementConciliationSummary, fStatementConciliationSummary);
                OnChanged("NumberOfMovements");
                OnChanged("NumberOfDebits");
                OnChanged("NumberOfCredits");
                OnChanged("DebitsValue");
                OnChanged("CreditsValue");
                OnChanged("NumberOfConciledMovements");
                OnChanged("NumberOfConciledDebits");
                OnChanged("NumberOfConciledCredits");
                OnChanged("ConciledDebitsValue");
                OnChanged("ConciliationDebitsValueDifference");
                OnChanged("ConciledCreditsValue");
                OnChanged("ConciliationCreditsValueDifference");
                OnChanged("TotalConciliationValueDifference");
                OnChanged("NumberOfUnconciledMovements");
                OnChanged("IsConciled");
            }
        }
    }
}

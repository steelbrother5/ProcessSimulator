using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ConciliationRecord : EnterpriseBaseObject
    {
        private string code;
        private DateTime closingDate;
        private DateTime executionDate;

        public ConciliationRecord(Session session) : base(session) { }

        public string Code
        {
            get { return code; }
            set { SetPropertyValue("Code", ref code, value); }
        }

        public DateTime ClosingDate
        {
            get { return closingDate; }
            set { SetPropertyValue("ClosingDate", ref closingDate, value); }
        }

        public DateTime ExecutionDate
        {
            get { return executionDate; }
            set { SetPropertyValue("ExecutionDate", ref executionDate, value); }
        }


        [PersistentAlias("CashMovement.Amount-(DepositAccountStatementMovement.Debit * -1.0m)-DepositAccountStatementMovement.Credit")]
        public decimal ConciliationDifference
        {
            get
            {
                object tempObject = EvaluateAlias("ConciliationDifference");
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

        //CashMovement cashMovement = null;
        //public CashMovement CashMovement
        //{
        //    get { return cashMovement; }
        //    set
        //    {
        //        if (cashMovement == value)
        //            return;

        //        CashMovement prevCashMovement = cashMovement;
        //        cashMovement = value;

        //        if (IsLoading) return;

        //        if (prevCashMovement != null && prevCashMovement.ConciliationRecord == this)
        //            prevCashMovement.ConciliationRecord = null;

        //        if (cashMovement != null)
        //            //owner.House = this;
        //            cashMovement.ConciliationRecord = this;
        //        OnChanged("ConciliationRecord");
        //        OnChanged("ConciliationDifference");
        //    }
        //}

        DepositAccountStatementMovement depositAccountStatementMovement = null;
        public DepositAccountStatementMovement DepositAccountStatementMovement
        {
            get { return depositAccountStatementMovement; }
            set
            {
                if (depositAccountStatementMovement == value)
                    return;

                DepositAccountStatementMovement prevDepositAccountStatementMovement = depositAccountStatementMovement;
                depositAccountStatementMovement = value;

                if (IsLoading) return;

                if (prevDepositAccountStatementMovement != null && prevDepositAccountStatementMovement.ConciliationRecord == this)
                    prevDepositAccountStatementMovement.ConciliationRecord = null;

                if (depositAccountStatementMovement != null)
                    //owner.House = this;
                    depositAccountStatementMovement.ConciliationRecord = this;
                OnChanged("DepositAccountStatementMovement");
                OnChanged("ConciliationDifference");
            }
        }


    }
}

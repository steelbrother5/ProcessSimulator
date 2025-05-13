using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccountLoading : EnterpriseBaseObject
    {
        private DateTime cutOffDate;
        private DepositAccountLoadingMaster depositAccountLoadingMaster;
        private Decimal closingBalance;
        private bool isActive;
        private bool isOnlyBalanceLoaded;

        public DepositAccountLoading(Session session) : base(session) { }

        [Association("DepositAccountLoadingMaster-DepositAccountLoading", typeof(DepositAccountLoadingMaster))]
        public DepositAccountLoadingMaster DepositAccountLoadingMaster
        {
            get { return depositAccountLoadingMaster; }
            set { SetPropertyValue("DepositAccountLoadingMaster", ref depositAccountLoadingMaster, value); }
        }

        public DateTime CutOffDate
        {
            get { return cutOffDate; }
            set { SetPropertyValue("CutOffDate", ref cutOffDate, value); }
        }

        public Decimal ClosingBalance
        {
            get { return closingBalance; }
            set { SetPropertyValue("ClosingBalance", ref closingBalance, value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }

        public bool IsOnlyBalanceLoaded
        {
            get { return isOnlyBalanceLoaded; }
            set { SetPropertyValue("IsOnlyBalanceLoaded", ref isOnlyBalanceLoaded, value); }
        }

        /// <summary>
        /// Relación 1-1 con el DepositAccountStatement que se cargó
        /// </summary>
        DepositAccountStatement depositAccountStatement = null;
        public DepositAccountStatement DepositAccountStatement
        {
            get { return depositAccountStatement; }
            set
            {
                if (depositAccountStatement == value)
                    return;

                // Store a reference to the former owner. 
                DepositAccountStatement prevDepositAccountStatement = depositAccountStatement;
                depositAccountStatement = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists. 
                if (prevDepositAccountStatement != null && prevDepositAccountStatement.DepositAccountLoading == this)
                    prevDepositAccountStatement.DepositAccountLoading = null;

                // Specify that the building is a new owner's house. 
                if (depositAccountStatement != null)
                    //owner.House = this;
                    depositAccountStatement.DepositAccountLoading = this;
                OnChanged("DepositAccountStatement");
                //OnChanged("SettlementCurrencyVsUSDollarRate");
                //OnChanged("SettlementAmount");
                //OnChanged("FaceValue");
                //OnChanged("Currency");
                //OnChanged("Sign");

            }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

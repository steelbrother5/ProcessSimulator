using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccountLoadingMaster : EnterpriseBaseObject
    {
        private DateTime beginningDate;
        private DateTime lastLoadingDate;
        private bool isLoaded;
        private bool isReLoaded;
        private int reLoadedLevel;
        private DepositAccountsControl depositAccountsControl;

        public DepositAccountLoadingMaster(Session session) : base(session) { }
        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }

        [Association("DepositAccountsControl-DepositAccountLoadingMaster",
            typeof(DepositAccountsControl))]
        public DepositAccountsControl DepositAccountsControl
        {
            get { return depositAccountsControl; }
            set { SetPropertyValue("DepositAccountsControl", ref depositAccountsControl, value); }
        }

        [Association("DepositAccountLoadingMaster-DepositAccountLoading"), Aggregated]
        public XPCollection<DepositAccountLoading> DepositAccountLoadings
        {
            get { return GetCollection<DepositAccountLoading>("DepositAccountLoadings"); }
        }

        //[Association("DepositAccountLoadingMaster-DepositAccountLoadingUndo"), Aggregated]
        //public XPCollection<DepositAccountLoadingUndo> DepositAccountLoadingUndos
        //{
        //    get { return GetCollection<DepositAccountLoadingUndo>("DepositAccountLoadingUndos"); }
        //}

        public DateTime BeginningDate
        {
            get { return beginningDate; }
            set { SetPropertyValue("BeginningDate", ref beginningDate, value); }
        }

        public DateTime LastLoadingDate
        {
            get { return lastLoadingDate; }
            set { SetPropertyValue("LastLoadingDate", ref lastLoadingDate, value); }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { SetPropertyValue("IsLoaded", ref isLoaded, value); }
        }

        public bool IsReLoaded
        {
            get { return isReLoaded; }
            set { SetPropertyValue("IsReLoaded", ref isReLoaded, value); }
        }

        public int ReLoadedLevel
        {
            get { return reLoadedLevel; }
            set { SetPropertyValue("ReLoadedLevel", ref reLoadedLevel, value); }
        }

        DepositAccount depositAccount = null;
        public DepositAccount DepositAccount
        {
            get { return depositAccount; }
            set
            {
                if (depositAccount == value)
                    return;

                // Store a reference to the former owner. 
                DepositAccount prevDepositAccount = depositAccount;
                depositAccount = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists. 
                if (prevDepositAccount != null && prevDepositAccount.DepositAccountLoadingMaster == this)
                    prevDepositAccount.DepositAccountLoadingMaster = null;

                // Specify that the building is a new owner's house. 
                if (depositAccount != null)
                    //owner.House = this;
                    depositAccount.DepositAccountLoadingMaster = this;
                OnChanged("DepositAccount");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code (check out http://documentation.devexpress.com/#Xaf/CustomDocument2834 for more details).
            DepositAccountsControl depositAccountsControl =
                (from dac in new XPQuery<DepositAccountsControl>(Session)
                 select dac).FirstOrDefault();
            this.DepositAccountsControl = depositAccountsControl;
            depositAccountsControl.DepositAccountLoadingMasters.Add(this);

            //this.IsLoaded = true;
        }

    }
}

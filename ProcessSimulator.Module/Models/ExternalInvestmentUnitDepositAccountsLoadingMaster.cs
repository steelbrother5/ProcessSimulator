using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExternalInvestmentUnitDepositAccountsLoadingMaster : EnterpriseBaseObject
    {
        private DateTime beginningDate;
        private DateTime lastLoadingDate;
        private bool isLoaded;
        private bool isReLoaded;
        private int reLoadedLevel;
        private ExternalInvestmentUnitDepositAccountsControl externalInvestmentUnitDepositAccountsControl;

        public ExternalInvestmentUnitDepositAccountsLoadingMaster(Session session) : base(session) { }
        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                return auditTrail;
            }
        }

        [Association("ExternalInvestmentUnitDepositAccountsControl-ExternalInvestmentUnitDepositAccountsLoadingMaster",
            typeof(ExternalInvestmentUnitDepositAccountsControl))]
        public ExternalInvestmentUnitDepositAccountsControl ExternalInvestmentUnitDepositAccountsControl
        {
            get { return externalInvestmentUnitDepositAccountsControl; }
            set { SetPropertyValue("ExternalInvestmentUnitDepositAccountsControl", ref externalInvestmentUnitDepositAccountsControl, value); }
        }

        [Association("ExternalInvestmentUnitDepositAccountsLoadingMaster-ExternalInvestmentUnitDepositAccountsLoading"), Aggregated]
        public XPCollection<ExternalInvestmentUnitDepositAccountsLoading> ExternalInvestmentUnitDepositAccountsLoadings
        {
            get { return GetCollection<ExternalInvestmentUnitDepositAccountsLoading>("ExternalInvestmentUnitDepositAccountsLoadings"); }
        }

        [Association("ExternalInvestmentUnitDepositAccountsLoadingMaster-ExternalInvestmentUnitDepositAccountsLoadingUndo"), Aggregated]
        public XPCollection<ExternalInvestmentUnitDepositAccountsLoadingUndo> ExternalInvestmentUnitDepositAccountsLoadingUndos
        {
            get { return GetCollection<ExternalInvestmentUnitDepositAccountsLoadingUndo>("ExternalInvestmentUnitDepositAccountsLoadingUndos"); }
        }

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

        ExternalInvestmentUnit externalInvestmentUnit = null;
        public ExternalInvestmentUnit ExternalInvestmentUnit
        {
            get { return externalInvestmentUnit; }
            set
            {
                if (externalInvestmentUnit == value)
                    return;
                // Store a reference to the former owner. 
                ExternalInvestmentUnit prevExternalInvestmentUnit = externalInvestmentUnit;
                externalInvestmentUnit = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists. 
                if (prevExternalInvestmentUnit != null && prevExternalInvestmentUnit.ExternalInvestmentUnitDepositAccountsLoadingMaster == this)
                    prevExternalInvestmentUnit.ExternalInvestmentUnitDepositAccountsLoadingMaster = null;

                // Specify that the building is a new owner's house. 
                if (externalInvestmentUnit != null)
                    externalInvestmentUnit.ExternalInvestmentUnitDepositAccountsLoadingMaster = this;

                OnChanged("ExternalInvestmentUnit");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ExternalInvestmentUnitDepositAccountsControl externalInvestmentUnitDepositAccountsControl =
                new XPQuery<ExternalInvestmentUnitDepositAccountsControl>(Session).FirstOrDefault();
            this.ExternalInvestmentUnitDepositAccountsControl = externalInvestmentUnitDepositAccountsControl;
            externalInvestmentUnitDepositAccountsControl.ExternalInvestmentUnitDepositAccountsLoadingMasters.Add(this);
            this.IsLoaded = true;
        }
    }
}

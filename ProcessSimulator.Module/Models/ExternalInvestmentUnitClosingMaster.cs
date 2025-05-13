using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ExternalInvestmentUnitClosingMaster : EnterpriseBaseObject
    {
        //private PortfolioClosingFrequency portfolioClosingFrequency;
        private DateTime beginningDate;
        private DateTime lastClosingDate;
        private DateTime lastCompensationDate;
        private DateTime lastValuationDate;
        private DateTime lastAccountingDate;
        private bool isOpen;
        private bool isReOpen;
        private int reOpenedLevel;
        private ExternalInvestmentUnitControl externalInvestmentUnitControl;

        public ExternalInvestmentUnitClosingMaster(Session session) : base(session) { }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        [Appearance("AuditTrailHide", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)

                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);

                return auditTrail;
            }
        }

        [Association("ExternalInvestmentUnitControl-ExternalInvestmentUnitClosingMaster", typeof(ExternalInvestmentUnitControl))]
        [Appearance("ExternalInvestmentUnitControlDisabled", Enabled = false)]
        public ExternalInvestmentUnitControl ExternalInvestmentUnitControl
        {
            get { return externalInvestmentUnitControl; }
            set { SetPropertyValue("ExternalInvestmentUnitControl", ref externalInvestmentUnitControl, value); }
        }


        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitClosing"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitClosing> ExternalInvestmentUnitClosings
        //    => GetCollection<ExternalInvestmentUnitClosing>("ExternalInvestmentUnitClosings");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitOpening"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitOpening> ExternalInvestmentUnitOpenings
        //    => GetCollection<ExternalInvestmentUnitOpening>("ExternalInvestmentUnitOpenings");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitCompensation"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitCompensation> ExternalInvestmentUnitCompensations
        //    => GetCollection<ExternalInvestmentUnitCompensation>("ExternalInvestmentUnitCompensations");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitCompensationUndo"), Aggregated]
        //[Appearance("ExternalInvestmentUnitCompensationUndosHide", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<ExternalInvestmentUnitCompensationUndo> ExternalInvestmentUnitCompensationUndos
        //    => GetCollection<ExternalInvestmentUnitCompensationUndo>("ExternalInvestmentUnitCompensationUndos");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitValuation"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitValuation> ExternalInvestmentUnitValuations
        //    => GetCollection<ExternalInvestmentUnitValuation>("ExternalInvestmentUnitValuations");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitValuationUndo"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitValuationUndo> ExternalInvestmentUnitValuationUndos
        //    => GetCollection<ExternalInvestmentUnitValuationUndo>("ExternalInvestmentUnitValuationUndos");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitAccounting"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitAccounting> ExternalInvestmentUnitAccountings
        //    => GetCollection<ExternalInvestmentUnitAccounting>("ExternalInvestmentUnitAccountings");
        //[Association("ExternalInvestmentUnitClosingMaster-ExternalInvestmentUnitAccountingUndo"), Aggregated]
        //public XPCollection<ExternalInvestmentUnitAccountingUndo> ExternalInvestmentUnitAccountingUndos
        //    => GetCollection<ExternalInvestmentUnitAccountingUndo>("ExternalInvestmentUnitAccountingUndos");
        //[Association("ExternalInvestmentUnitClosingMaster-EIUClosingMasterMessage"), Aggregated]
        //public XPCollection<EIUClosingMasterMessage> EIUClosingMasterMessages
        //    => GetCollection<EIUClosingMasterMessage>("EIUClosingMasterMessages");

        //[Appearance("PortfolioClosingFrequencyDisabled", Enabled = false)]
        //public PortfolioClosingFrequency PortfolioClosingFrequency
        //{
        //    get { return portfolioClosingFrequency; }
        //    set { SetPropertyValue("PortfolioClosingFrequency", ref portfolioClosingFrequency, value); }
        //}

        [Appearance("BeginningDateDisabled", Enabled = false)]
        public DateTime BeginningDate
        {
            get { return beginningDate; }
            set { SetPropertyValue("BeginningDate", ref beginningDate, value); }
        }

        [Appearance("LastClosingDateDisabled", Enabled = false)]
        public DateTime LastClosingDate
        {
            get { return lastClosingDate; }
            set { SetPropertyValue("LastClosingDate", ref lastClosingDate, value); }
        }

        [Appearance("LastCompensationDateDisabled", Enabled = false)]
        public DateTime LastCompensationDate
        {
            get { return lastCompensationDate; }
            set { SetPropertyValue("LastCompensationDate", ref lastCompensationDate, value); }
        }

        [Appearance("LastValuationDateDisabled", Enabled = false)]
        public DateTime LastValuationDate
        {
            get { return lastValuationDate; }
            set { SetPropertyValue("LastValuationDate", ref lastValuationDate, value); }
        }

        [Appearance("LastAccountingDateDisabled", Enabled = false)]
        public DateTime LastAccountingDate
        {
            get { return lastAccountingDate; }
            set { SetPropertyValue("LastAccountingDate", ref lastAccountingDate, value); }
        }

        [Appearance("IsOpenDisabled", Enabled = false)]
        public bool IsOpen
        {
            get { return isOpen; }
            set { SetPropertyValue("IsOpen", ref isOpen, value); }
        }

        [Appearance("IsReOpenDisabled", Enabled = false)]
        public bool IsReOpen
        {
            get { return isReOpen; }
            set { SetPropertyValue("IsReOpen", ref isReOpen, value); }
        }

        [Appearance("ReOpenedLevelDisabled", Enabled = false)]
        public int ReOpenedLevel
        {
            get { return reOpenedLevel; }
            set { SetPropertyValue("ReOpenedLevel", ref reOpenedLevel, value); }
        }


        ExternalInvestmentUnit externalInvestmentUnit = null;
        [Appearance("ExternalInvestmentUnitDisabled", Enabled = false)]
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
                if (prevExternalInvestmentUnit != null && prevExternalInvestmentUnit.ExternalInvestmentUnitClosingMaster == this)
                    prevExternalInvestmentUnit.ExternalInvestmentUnitClosingMaster = null;

                // Specify that the building is a new owner's house. 
                if (externalInvestmentUnit != null)
                    externalInvestmentUnit.ExternalInvestmentUnitClosingMaster = this;
                OnChanged("ExternalInvestmentUnit");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ExternalInvestmentUnitControl externalInvestmentUnitControl = new XPQuery<ExternalInvestmentUnitControl>(Session).FirstOrDefault();
            this.ExternalInvestmentUnitControl = externalInvestmentUnitControl;
            externalInvestmentUnitControl.ExternalInvestmentUnitClosingMasters.Add(this);
            this.IsOpen = true;
        }
    }
}

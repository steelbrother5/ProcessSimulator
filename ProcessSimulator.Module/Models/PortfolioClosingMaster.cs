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
    public class PortfolioClosingMaster : EnterpriseBaseObject
    {
        //private PortfolioClosingFrequency portfolioClosingFrequency;
        private DateTime beginningDate;
        private DateTime lastClosingDate;
        private DateTime lastCompensationDate;
        private DateTime lastValuationDate;
        private bool isOpen;
        private bool isReOpen;
        private int reOpenedLevel;
        private PortfolioControl portfolioControl;


        public PortfolioClosingMaster(Session session) : base(session) { }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        [Appearance("AuditTrailHide", Visibility = ViewItemVisibility.Hide)]
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

        /// <summary>
        /// forma estándar de establecer relación 1-1 con Portfolio
        /// </summary>
        Portfolio portfolio = null;
        public Portfolio Portfolio
        {
            get { return portfolio; }
            set
            {
                if (portfolio == value)
                    return;

                Portfolio prevPortfolio = portfolio;
                portfolio = value;

                if (IsLoading) return;

                if (prevPortfolio != null && prevPortfolio.PortfolioClosingMaster == this)
                    prevPortfolio.PortfolioClosingMaster = null;

                if (portfolio != null)
                    portfolio.PortfolioClosingMaster = this;
                OnChanged("Task");

            }
        }


        [Association("PortfolioControl-PortfolioClosingMaster", typeof(PortfolioControl))]
        public PortfolioControl PortfolioControl
        {
            get { return portfolioControl; }
            set { SetPropertyValue("PortfolioControl", ref portfolioControl, value); }
        }

        [Association("PortfolioClosingMaster-PortfolioClosing"), Aggregated]
        public XPCollection<PortfolioClosing> PortfolioClosings
        {
            get { return GetCollection<PortfolioClosing>("PortfolioClosings"); }
        }

        [Association("PortfolioClosingMaster-PortfolioOpening"), Aggregated]
        public XPCollection<PortfolioOpening> PortfolioOpenings
        {
            get { return GetCollection<PortfolioOpening>("PortfolioOpenings"); }
        }

        [Association("PortfolioClosingMaster-PortfolioCompensation"), Aggregated]
        public XPCollection<PortfolioCompensation> PortfolioCompensations
        {
            get { return GetCollection<PortfolioCompensation>("PortfolioCompensations"); }
        }

        [Association("PortfolioClosingMaster-PortfolioCompensationUndo"), Aggregated]
        public XPCollection<PortfolioCompensationUndo> PortfolioCompensationUndos
        {
            get { return GetCollection<PortfolioCompensationUndo>("PortfolioCompensationUndos"); }
        }

        //[Association("PortfolioClosingMaster-PortfolioValuation"), Aggregated]
        //public XPCollection<PortfolioValuation> PortfolioValuations
        //{
        //    get { return GetCollection<PortfolioValuation>("PortfolioValuations"); }
        //}

        //[Association("PortfolioClosingMaster-PortfolioValuationUndo"), Aggregated]
        //public XPCollection<PortfolioValuationUndo> PortfolioValuationUndos
        //{
        //    get { return GetCollection<PortfolioValuationUndo>("PortfolioValuationUndos"); }
        //}

        //public PortfolioClosingFrequency PortfolioClosingFrequency
        //{
        //    get { return portfolioClosingFrequency; }
        //    set { SetPropertyValue("PortfolioClosingFrequency", ref portfolioClosingFrequency, value); }
        //}

        public DateTime BeginningDate
        {
            get { return beginningDate; }
            set { SetPropertyValue("BeginningDate", ref beginningDate, value); }
        }

        public DateTime LastClosingDate
        {
            get { return lastClosingDate; }
            set { SetPropertyValue("LastClosingDate", ref lastClosingDate, value); }
        }

        public DateTime LastCompensationDate
        {
            get { return lastCompensationDate; }
            set { SetPropertyValue("LastCompensationDate", ref lastCompensationDate, value); }
        }

        public DateTime LastValuationDate
        {
            get { return lastValuationDate; }
            set { SetPropertyValue("LastValuationDate", ref lastValuationDate, value); }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { SetPropertyValue("IsOpen", ref isOpen, value); }
        }

        public bool IsReOpen
        {
            get { return isReOpen; }
            set { SetPropertyValue("IsReOpen", ref isReOpen, value); }
        }

        public int ReOpenedLevel
        {
            get { return reOpenedLevel; }
            set { SetPropertyValue("ReOpenedLevel", ref reOpenedLevel, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            PortfolioControl portfolioControl = (from s in new XPQuery<PortfolioControl>(Session)
                                                       select s).FirstOrDefault();
            this.PortfolioControl = portfolioControl;
            portfolioControl.PortfolioClosingMasters.Add(this);

            this.IsOpen = true;
        }
    }
}

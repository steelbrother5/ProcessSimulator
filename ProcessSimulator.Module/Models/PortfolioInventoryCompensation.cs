using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioInventoryCompensation : PortfolioItem
    {
        private PortfolioInventoryItem portfolioInventoryItem;
        private PortfolioClosing portfolioClosing;
        private String compensationOriginID;
        private decimal unitsCompensated;
        private decimal compensatedValueInLocalCurrency;

        public PortfolioInventoryCompensation(Session session) : base(session) { }

        [Association("PortfolioInventoryItem-PortfolioInventoryCompensation", typeof(PortfolioInventoryItem))]
        public PortfolioInventoryItem PortfolioInventoryItem
        {
            get { return portfolioInventoryItem; }
            set { SetPropertyValue("PortfolioInventoryItem", ref portfolioInventoryItem, value); }
        }

        public PortfolioClosing PortfolioClosing
        {
            get { return portfolioClosing; }
            set
            {
                if (SetPropertyValue("PortfolioClosing", ref portfolioClosing, value))
                    OnChanged("IsActive");
            }
        }

        [PersistentAlias("PortfolioClosing.IsActive")]
        public bool IsActive
        {
            get
            {
                object tempObject = EvaluateAlias("IsActive");
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

        public String CompensationOriginID
        {
            get { return compensationOriginID; }
            set { SetPropertyValue("CompensationOriginID", ref compensationOriginID, value); }
        }

        public decimal UnitsCompensated
        {
            get { return unitsCompensated; }
            set { SetPropertyValue("UnitsCompensated", ref unitsCompensated, value); }
        }

        public decimal CompensatedValueInLocalCurrency
        {
            get { return compensatedValueInLocalCurrency; }
            set { SetPropertyValue("CompensatedValueInLocalCurrency", ref compensatedValueInLocalCurrency, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}

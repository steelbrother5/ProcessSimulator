using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioControl : EnterpriseBaseObject
    {
        internal PortfolioControl(Session session) : base(session) { }
        public static PortfolioControl GetInstance(IObjectSpace objectSpace)
        {
            PortfolioControl result = objectSpace.FindObject<PortfolioControl>(null);
            if (result == null)
            {
                result = new PortfolioControl(((XPObjectSpace)objectSpace).Session);
                result.Name = "Portfolio Control";
                result.Description = "Portfolio Closing Control";
                result.Save();
            }
            return result;
        }

        [Association("PortfolioControl-PortfolioClosingMaster", typeof(PortfolioClosingMaster))]
        public XPCollection<PortfolioClosingMaster> PortfolioClosingMasters
        {
            get { return GetCollection<PortfolioClosingMaster>("PortfolioClosingMasters"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetPropertyValue("Name", ref name, value);
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                SetPropertyValue("Description", ref description, value);
            }
        }
        protected override void OnDeleting()
        {
            throw new UserFriendlyException("This object cannot be deleted.");
        }
    }
}

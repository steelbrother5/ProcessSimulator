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
    public class ExternalInvestmentUnitDepositAccountsControl : EnterpriseBaseObject
    {
        internal ExternalInvestmentUnitDepositAccountsControl(Session session) : base(session) { }
        public static ExternalInvestmentUnitDepositAccountsControl GetInstance(IObjectSpace objectSpace)
        {
            ExternalInvestmentUnitDepositAccountsControl result = objectSpace.FindObject<ExternalInvestmentUnitDepositAccountsControl>(null);
            if (result == null)
            {
                result = new ExternalInvestmentUnitDepositAccountsControl(((XPObjectSpace)objectSpace).Session);
                result.Name = "Bank Control";
                result.Description = "Bank Opening Balance Control";
                result.Save();
            }
            return result;
        }

        [Association("ExternalInvestmentUnitDepositAccountsControl-ExternalInvestmentUnitDepositAccountsLoadingMaster",
                       typeof(ExternalInvestmentUnitDepositAccountsLoadingMaster))]
        public XPCollection ExternalInvestmentUnitDepositAccountsLoadingMasters
        {
            get { return GetCollection("ExternalInvestmentUnitDepositAccountsLoadingMasters"); }
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

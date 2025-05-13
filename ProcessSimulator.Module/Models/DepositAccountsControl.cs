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
    public class DepositAccountsControl : EnterpriseBaseObject
    {
        internal DepositAccountsControl(Session session) : base(session) { }
        public static DepositAccountsControl GetInstance(IObjectSpace objectSpace)
        {
            DepositAccountsControl result = objectSpace.FindObject<DepositAccountsControl>(null);
            if (result == null)
            {
                result = new DepositAccountsControl(((XPObjectSpace)objectSpace).Session);
                result.Name = "Deposit Account Control";
                result.Description = "Deposit Account Opening Balance Control";
                result.Save();
            }
            return result;
        }

        [Association("DepositAccountsControl-DepositAccountLoadingMaster",
                       typeof(DepositAccountLoadingMaster))]
        public XPCollection DepositAccountLoadingMasters
        {
            get { return GetCollection("DepositAccountLoadingMasters"); }
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

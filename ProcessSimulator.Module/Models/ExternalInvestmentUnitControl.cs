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
    public class ExternalInvestmentUnitControl : EnterpriseBaseObject
    {
        internal ExternalInvestmentUnitControl(Session session) : base(session) { }
        public static ExternalInvestmentUnitControl GetInstance(IObjectSpace objectSpace)
        {
            ExternalInvestmentUnitControl result = objectSpace.FindObject<ExternalInvestmentUnitControl>(null);
            if (result == null)
            {
                result = new ExternalInvestmentUnitControl(((XPObjectSpace)objectSpace).Session);
                result.Name = "Control de Unidad Externa de Inversión";
                result.Description = "Control de Cierres de Unidad Externa de Inversión";
                result.Save();
            }
            return result;
        }

        [Association("ExternalInvestmentUnitControl-ExternalInvestmentUnitClosingMaster", typeof(ExternalInvestmentUnitClosingMaster))]
        public XPCollection ExternalInvestmentUnitClosingMasters
        {
            get { return GetCollection("ExternalInvestmentUnitClosingMasters"); }
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

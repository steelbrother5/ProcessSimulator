using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class TradeAttribute : EnterpriseBaseObject
    {
        private int iD;
        private string name;

        public TradeAttribute(Session session) : base(session) { }

        public int ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

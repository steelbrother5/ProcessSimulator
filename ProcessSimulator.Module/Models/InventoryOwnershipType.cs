using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class InventoryOwnershipType : EnterpriseBaseObject
    {
        private int iD;
        private string name;
        private bool isAvailable;

        public InventoryOwnershipType(Session session) : base(session) { }

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

        public bool IsAvailable
        {
            get { return isAvailable; }
            set { SetPropertyValue("IsAvailable", ref isAvailable, value); }
        }

        public override void AfterConstruction() => base.AfterConstruction();
    }
}

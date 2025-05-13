using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class ItemSign : EnterpriseBaseObject
    {
        private string iD;
        private string name;
        private bool isPositive;
        private decimal multiplier;

        public ItemSign(Session session) : base(session) { }

        public string ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        public bool IsPositive
        {
            get { return isPositive; }
            set { SetPropertyValue("IsPositive", ref isPositive, value); }
        }

        public decimal Multiplier
        {
            get { return multiplier; }
            set { SetPropertyValue("Multiplier", ref multiplier, value); }
        }
    }
}

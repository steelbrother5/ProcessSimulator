using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class StatementStandardConcept : EnterpriseBaseObject
    {
        private string iD;
        private string name;

        public StatementStandardConcept(Session session) : base(session) { }

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
    }
}

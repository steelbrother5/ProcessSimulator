using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioNoveltyEntity : BaseObject
    {
        public PortfolioNoveltyEntity(Session session) : base(session) { }

        private string id;
        public string ID
        {
            get => id;
            set => SetPropertyValue("ID", ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetPropertyValue("Name", ref name, value);
        }
    }
}

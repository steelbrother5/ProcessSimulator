using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessSimulator.Module.BusinessObjects.Enums;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioNoveltyType : BaseObject
    {
        public PortfolioNoveltyType(Session session) : base(session) { }

        private EnumPortfolioNoveltyType id;
        public EnumPortfolioNoveltyType ID
        {
            get => id;
            set => SetPropertyValue("ID", ref id, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetPropertyValue("Description", ref description, value);
        }
    }
}

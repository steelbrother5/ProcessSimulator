using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class Position : TradeAttribute
    {
        private ItemSign sign;

        public Position(Session session) : base(session) { }

        public ItemSign Sign
        {
            get { return sign; }
            set { SetPropertyValue("Sign", ref sign, value); }
        }
    }
}

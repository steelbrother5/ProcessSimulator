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
    public class InitialClosingDate : EnterpriseBaseObject
    {
        public InitialClosingDate(Session session) : base(session) { }

        private DateTime initialCloseDate;
        public DateTime InitialCloseDate
        {
            get { return initialCloseDate; }
            set { SetPropertyValue<DateTime>("InitialCloseDate", ref initialCloseDate, value); }
        }

        public static InitialClosingDate GetInstance(IObjectSpace objectSpace)
        {

            InitialClosingDate result = objectSpace.FindObject<InitialClosingDate>(null);


            if (result == null)
            {

                result = new InitialClosingDate(((XPObjectSpace)objectSpace).Session);
            }

            result.Save();
            return result;
        }
    }
}

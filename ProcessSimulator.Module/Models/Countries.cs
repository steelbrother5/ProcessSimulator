using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public partial class Countries : XPBaseObject
    {
        private System.String _isoCode;
        private System.String _name;
        private System.String _code;
        public Countries(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [Key, Size(3)]
        [RuleRequiredField(DefaultContexts.Save)]
        public System.String Code
        {
            get
            {
                return _code;
            }
            set
            {
                SetPropertyValue("Code", ref _code, value);
            }
        }
        [Size(30)]
        public System.String IsoCode
        {
            get
            {
                return _isoCode;
            }
            set
            {
                SetPropertyValue("IsoCode", ref _isoCode, value);
            }
        }
        [Size(255)]
        [RuleRequiredField(DefaultContexts.Save)]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
            }
        }
    }
}

using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class MovementKind : BaseObject
    {
        public MovementKind(Session session) : base(session) { }

        private int _iD;
        private string _name;

        public int ID
        {
            get { return _iD; }
            set { SetPropertyValue("ID", ref _iD, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

    }
}

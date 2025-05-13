using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class DepositAccStatus : BaseObject
    {
        public DepositAccStatus(Session session) : base(session) { }

        private int _iD;
        private string _nameStatus;

        public int ID
        {
            get { return _iD; }
            set { SetPropertyValue("ID", ref _iD, value); }
        }

        public string NameStatus
        {
            get { return _nameStatus; }
            set { SetPropertyValue("NameStatus", ref _nameStatus, value); }
        }
    }
}

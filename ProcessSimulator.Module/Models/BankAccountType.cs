using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class BankAccountType : BaseObject
    {
        public BankAccountType(Session session)
            : base(session)
        {
        }

        private string _accountType;

        /// <summary>
        /// Tipo de Cuenta 
        /// </summary>
        /// 
        [Size(50)]
        public string AccountType
        {
            get
            { return _accountType; ; }
            set
            { SetPropertyValue<string>("AccountType", ref _accountType, value); }
        }

    }
}

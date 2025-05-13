using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    /// <summary>
    /// Base Class for Regular Persistent Classes
    /// </summary>
    [DefaultClassOptions]
    [DeferredDeletion(false)]
    [NonPersistent]
    public abstract class EnterpriseBaseObject : DevExpress.Persistent.BaseImpl.BaseObject
    {
        public EnterpriseBaseObject(Session session) : base(session) { }


        private DateTime xCreateDateTime;
        public DateTime XCreateDateTime
        {
            get => xCreateDateTime;
            set => SetPropertyValue("XCreateDateTime", ref xCreateDateTime, value);
        }

        private DateTime xRowVersion;
        public DateTime XRowVersion
        {
            get => xRowVersion;
            set => SetPropertyValue("XRowVersion", ref xRowVersion, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (SecuritySystem.CurrentUser != null)
            {
                xCreateDateTime = DateTime.Now;
            }
        }
    }
}

using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class QuotaValidationStatus : XPBaseObject
    {
        public QuotaValidationStatus(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private int quotaValidationStatusId;

        [Key]
        public int QuotaValidationStatusId
        {
            get { return quotaValidationStatusId; }
            set { SetPropertyValue("QuotaValidationStatusId", ref quotaValidationStatusId, value); }
        }

        private string quotaValidationStatusName;

        public string QuotaValidationStatusName
        {
            get { return quotaValidationStatusName; }
            set { SetPropertyValue("QuotaValidationStatusName", ref quotaValidationStatusName, value); }
        }
    }
}

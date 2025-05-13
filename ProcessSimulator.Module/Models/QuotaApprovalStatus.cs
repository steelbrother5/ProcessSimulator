using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class QuotaApprovalStatus : XPBaseObject
    {
        public QuotaApprovalStatus(Session session) : base(session) { }

        private int quotaApprovalStatusId;

        [Key]
        public int QuotaApprovalStatusId
        {
            get { return quotaApprovalStatusId; }
            set { SetPropertyValue("QuotaApprovalStatusId", ref quotaApprovalStatusId, value); }
        }

        private string quotaApprovalStatusName;

        public string QuotaApprovalStatusName
        {
            get { return quotaApprovalStatusName; }
            set { SetPropertyValue("QuotaApprovalStatusName", ref quotaApprovalStatusName, value); }
        }
    }
}

using ProcessSimulator.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.BusinessMethods
{
    public interface IQuotable
    {
        QuotaApprovalStatus GetQuotaApprovalStatus();
        QuotaValidationStatus GetQuotaValidationStatus();
        QuotaApprovalStatus SetQuotaApprovalStatus(QuotaApprovalStatus quotaApprovalStatus);
        QuotaValidationStatus SetQuotaValidationStatus(QuotaValidationStatus quotaValidationStatus);
    }
}

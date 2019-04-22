using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportApplication.Core.Exception
{
    public enum SupportApplicationErrors
    {
        UnexpectedException = -5000,
        ConcurrencyException = -5001,
        DatabaseUpdateException = -5002,
        DatabaseCommitException = -5003,
    }
}

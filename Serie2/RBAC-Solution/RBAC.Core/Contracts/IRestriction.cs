using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;

namespace RBAC.Core.Contracts
{
    public interface IRestriction
    {
        Boolean isValid(Role[] roles);
    }
}

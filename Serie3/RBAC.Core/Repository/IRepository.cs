using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;

namespace RBAC.Core.Repository
{
    public interface IRepository<M>
    {

        List<M> LoadAll();

    }
}

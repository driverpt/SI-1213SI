using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBAC.Core.Entities
{
    public class Permission : Model, IEqualityComparer<Permission>
    {
        public Int32 Id { get; set; }
        public String Description { get; set; }

        public bool Equals(Permission x, Permission y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Permission obj)
        {
            return obj.Id;
        }
    }
}

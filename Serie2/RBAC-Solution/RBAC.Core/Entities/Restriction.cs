using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Contracts;

namespace RBAC.Core.Entities
{
    public class Restriction : IRestriction
    {
        private readonly Int32[] restrictedRoleIds;

        public Restriction(params Int32[] restrictedRoleIds)
        {
            this.restrictedRoleIds = restrictedRoleIds;
        }

        public bool isValid(params Role[] roles)
        {
            Boolean[] isRolePresentFlag = new Boolean[restrictedRoleIds.Length];

            for (Int32 i = 0; i < restrictedRoleIds.Length; i++)
            {
                isRolePresentFlag[i] = roles.Count((v) => { return v.Id == restrictedRoleIds[i]; }) > 0;
            }

            return isRolePresentFlag.Count((b) => { return b == true; }) <= 1;
        }
    }
}

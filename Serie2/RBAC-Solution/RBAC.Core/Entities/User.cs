using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBAC.Core.Entities
{
    public class User : Model
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }
}

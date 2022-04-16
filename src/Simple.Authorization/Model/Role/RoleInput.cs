using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Model.Role
{
    public class RoleInput : RoleBase
    {
        public override int ID { get; set; }
        public override string Name { get; set; } = string.Empty;
        public override string Description { get; set; } = string.Empty;
        public override string Permission { get; set; } = string.Empty;
    }
}

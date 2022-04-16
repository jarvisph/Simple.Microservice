using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Model.Admin
{
    public class AdminInput : AdminBase
    {
        public override int ID { get; set; }
        public override string AdminName { get; set; } = string.Empty;
        public override string NickName { get; set; } = string.Empty;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class CMSModel : BaseModel
    {
        public string Page { get; set; }
        public string Content { get; set; }
        public bool IsEdit { get; set; }
    }
}

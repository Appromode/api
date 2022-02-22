using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.DTOs
{
    public class LinkDTO
    {
        public Int64 LinkId { get; set; }
        public string LinkName { get; set; }
        public string LinkIcon { get; set; }
        public string LinkURL { get; set; }
        public Int64 LinkPosition { get; set; }
        public LinkDTO LinkParent { get; set; }
        public virtual ICollection<LinkDTO> LinkChildren { get; set; }
    }
}

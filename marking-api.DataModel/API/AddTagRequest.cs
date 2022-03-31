using System.Collections.Generic;
using marking_api.DataModel.Project;
using System;

namespace marking_api.DataModel.API
{
    public class AddTagRequest
    {
        public string UserId { get; set; }
        public List<TagDM> tags { get; set; }
    }
}

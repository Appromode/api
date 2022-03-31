using System;

namespace marking_api.DataModel.API
{
    public class RemoveTagRequest
    {
        public string UserId { get; set; }
        public Int64 TagId { get; set; }
    }
}

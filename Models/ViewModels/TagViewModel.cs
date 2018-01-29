using System.Collections.Generic;

namespace dojoQA.Models
{
    public class TagViewModel
    {
        public int tagId { get; set; }
        public string name { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}
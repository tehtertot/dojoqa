using System.Collections.Generic;

namespace dojoQA.Models
{
    public class CategoryWithTagsViewModel
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public List<SimpleTagViewModel> tags { get; set; }

        public CategoryWithTagsViewModel() {
            tags = new List<SimpleTagViewModel>();
        }

        public CategoryWithTagsViewModel(Category category) {
            tags = new List<SimpleTagViewModel>();
            categoryId = category.CategoryId;
            categoryName = category.Name;
            foreach (Tag t in category.AssociatedTags)
            {
                SimpleTagViewModel stvm = new SimpleTagViewModel(t);
                tags.Add(stvm);
            }
        }
    }

    public class SimpleTagViewModel
    {
        public int tagId { get; set; }
        public string tagName { get; set; }

        public SimpleTagViewModel(Tag tag) {
            tagId = tag.TagId;
            tagName = tag.Name;
        }
    }
}
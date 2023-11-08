using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModel
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }


        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        // collect tag
        public string SelectedTag { get; set; }
    }
}

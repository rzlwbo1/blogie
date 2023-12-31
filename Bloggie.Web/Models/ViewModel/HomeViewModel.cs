﻿using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPosts> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}

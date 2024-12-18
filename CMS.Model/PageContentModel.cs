using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class PageContentModel
    {
        public int? ContentId { get; set; }
        public string? PageTitleEnglish { get; set; }
        public string? PageTitleHindi { get; set; }
        public string? PageAlias { get; set; }
        public string? ReadMore { get; set; }
        public string? LinkType { get; set; }
        public string? WindowType { get; set; }
        public string? SnippetEnglish { get; set; }
        public string? SnippetHindi { get; set; }
        public string? ContentEnglish { get; set; }
        public string? ContentHindi { get; set; }
        public string? FeatureImage { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}

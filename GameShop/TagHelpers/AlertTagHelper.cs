using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GameShopTagHelpers
{
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : TagHelper
    {
        public string Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "alert alert-info");
        }
    }
}
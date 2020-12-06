using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.TagHelpers
{
    [HtmlTargetElement("autocomplete",TagStructure =TagStructure.WithoutEndTag)]
    public class AutocompleteTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null && string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Url))
            {
                output.SuppressOutput();
            }
            else
            {
                output.Content.Clear();
                output.TagName = null;
                output.Content.AppendHtml(Utils.Js("/js/awesomplete.min.js"));

                string ID = For == null ? Id : Utils.GetID(For);

                output.Content.AppendHtml(Utils.JsRaw($@"
                var ap = new Awesomplete(document.getElementById(""{ID}""), {{ minChars: 1 }});
                $.post('{Url}',function(data) {{ ap.list = data; }}); "));
            }
        }
    }
}

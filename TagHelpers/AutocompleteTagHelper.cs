using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.TagHelpers
{
    [HtmlTargetElement("autocomplete", Attributes ="Url", TagStructure =TagStructure.WithoutEndTag)]
    public class AutocompleteAjaxTagHelper : TagHelper
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

                output.Content.AppendHtml(Utils.CssRaw(".awesomplete {flex: 1;} .visually-hidden{display: none;}"));
                output.Content.AppendHtml(Utils.Js("/js/awesomplete.min.js"));

                string ID = For == null ? Id : Utils.GetID(For);

                output.Content.AppendHtml(Utils.JsRaw($@"
                var ap = new Awesomplete(document.getElementById(""{ID}""), {{ minChars: 1 }});
                $.post('{Url}',function(data) {{ ap.list = data; }}); "));
            }
        }
    }

    [HtmlTargetElement("autocomplete", Attributes ="Json", TagStructure = TagStructure.WithoutEndTag)]
    public class AutocompleteJsonTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }
        public string Id { get; set; }
        public string Json { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null && string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Json))
            {
                output.SuppressOutput();
            }
            else
            {
                output.Content.Clear();
                output.TagName = null;

                output.Content.AppendHtml(Utils.Css("/css/awesomplete.css"));
                output.Content.AppendHtml(Utils.CssRaw(".awesomplete {flex: 1;} .visually-hidden{display: none;}"));
                
                output.Content.AppendHtml(Utils.Js("/js/awesomplete.min.js"));

                string ID = For == null ? Id : Utils.GetID(For);

                output.Content.AppendHtml(Utils.JsRaw($@"
                var ap = new Awesomplete(document.getElementById(""{ID}""), {{ minChars: 1 }});
                ap.list = {Json};"));
            }
        }
    }
}

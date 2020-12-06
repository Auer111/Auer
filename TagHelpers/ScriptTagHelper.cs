using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.TagHelpers
{
    [HtmlTargetElement("script", Attributes = "on-complete")]
    public class ScriptTagHelper : TagHelper
    {
        /// <summary>
        /// Execute script only once document is loaded.
        /// </summary>
        public bool OnComplete { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!OnComplete)
            {
                base.Process(context, output);
            }
            else
            {
                output.Content.Clear();
                output.TagName = null;
                output.Content.AppendHtml(Utils.JsRaw(output.GetChildContentAsync().Result.GetContent()));
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Auer.Extensions;

namespace Auer.TagHelpers
{
    /// <summary>
    /// noUIslider: https://refreshless.com/nouislider/
    /// <para>OnChange, Step, Min, Max, MinStart, MaxStart</para>
    /// </summary>
    [HtmlTargetElement("slider", TagStructure = TagStructure.WithoutEndTag)]
    public class SliderTagHelper : TagHelper
    {
        public int? Step { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public int MinStart { get; set; }
        public int MaxStart { get; set; }

        /// <summary>
        /// Use like: MyFunction()
        /// </summary>
        public string OnChange { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            Step = Step ?? 1;
            Min = Min ?? 0;
            Max = Max ?? 10;
            MinStart = MinStart == 0 ? (int)Min : MinStart.Clamp((int)Min, (int)Max);
            MaxStart = MaxStart == 0 ? (int)Max : MaxStart.Clamp((int)Min, (int)Max);

            output.Content.Clear();
            output.TagName = null;

            output.Content.AppendHtml(Utils.Css("/css/nouislider.min.css"));
            output.Content.AppendHtml(Utils.CssRaw(".noUi-connect {background: #09aaff;}"));

            output.Content.AppendHtml(Utils.Js("/js/nouislider.min.js"));
            output.Content.AppendHtml(Utils.Js("/js/wNumb.min.js"));
            output.Content.AppendHtml(Utils.Js("/js/nouisliderTTmerge.js"));

            output.Content.AppendHtml(@"<div id='slider'></div>");

            output.Content.AppendHtml(Utils.JsRaw($@"

                var slider = document.getElementById('slider');
                var FormatUS = wNumb({{prefix: '$ ', decimals: 0, thousand: ','}});
                noUiSlider.create(slider, {{
                    start: [{MinStart}, {MaxStart}],
                    connect: true,
                    margin: {Step * 2},
                    step: {Step},
                    tooltips: [FormatUS, FormatUS],
                    range: {{
                        'min': {Min},
                        'max': {Max}
                    }}
                }}).on('change',()=>{OnChange});

                mergeTooltips(slider, 10, ' - ', FormatUS);
            "));
            
        }
    }
}

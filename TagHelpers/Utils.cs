using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.TagHelpers
{
    public static class Utils
    {
        /// <summary>
        /// <para>Must set output.TagName = null;</para>
        /// <para>Use like: output.Content.AppendHtml(Utils.Js("/file.js"));</para>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Js(string url)
        {
            int UniqueScriptID = Math.Abs(url.GetHashCode());
            return $@"<script>var script = document.createElement('script');
                                if (!window[""{UniqueScriptID}""]){{ 
                                    window.Script_{UniqueScriptID} = 'loaded:{url}';
                                    script.type = 'text/javascript';
                                    script.src = '{url}';
                                    document.body.appendChild(script);
                                }}
                                document.currentScript.remove();
                                </script>";
        }

        /// <summary>
        /// <para>Must set output.TagName = null;</para>
        /// <para>Use like: output.Content.AppendHtml(Utils.JsRaw("console.log('Hi')"));</para>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string JsRaw(string js)
        {
            return $@"<script>var script = document.createElement('script');
                                script.type = 'text/javascript';
                                script.innerHTML = `document.addEventListener('readystatechange', event => {{if (event.target.readyState === ""complete"") {{ {js} }}}});`;
                                document.body.appendChild(script);
                                document.currentScript.remove();
                                </script>";
        }

        /// <summary>
        /// <para>Must set output.TagName = null;</para>
        /// <para>Use like: output.Content.AppendHtml(Utils.Css("/file.css"));</para>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Css(string url)
        {
            //var link = document.createElement('link');
            
            //    window.Stylesheet_12312312 = 'loaded';
            //    link.rel = 'stylesheet';
            //    link.type = 'text/css';
            //    link.href = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css';
            //    link.media = 'all';
            //    document.head.appendChild(link);
            //}
            //document.currentScript.remove();


            int UniqueStylesheetID = Math.Abs(url.GetHashCode());
            return $@"<script>var link = document.createElement('link');
                                if (!window[""{UniqueStylesheetID}""]){{ 
                                    window.Stylesheet_{UniqueStylesheetID} = 'loaded:{url}';
                                    link.rel  = 'stylesheet';
                                    link.type = 'text/css';
                                    link.href = '{url}';
                                    link.media = 'all';
                                    document.head.appendChild(link);
                                }}
                                document.currentScript.remove();
                                </script>";
        }


        public static string CssRaw(string css)
        {
            return $@"<script>var style = document.createElement('style');
                                style.type = 'text/css';
                                style.innerHTML = '{css}';
                                document.head.appendChild(style);
                                document.currentScript.remove();
                                </script>";
        }


        public static string GetID(ModelExpression expression)
        {
            return expression.Name.Replace(".","_");
        }
    }
}

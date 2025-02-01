using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;

namespace MadStickWebAppTester.Pages.CustomTagHelpers
{
    public class EnvInfoTagHelper : TagHelper
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webEnv;

        public EnvInfoTagHelper(HtmlEncoder htmlEncoder, IHostEnvironment env, IConfiguration config, IWebHostEnvironment webEnv)
        {
            _htmlEncoder = htmlEncoder;
            _env = env;
            _config = config;
            _webEnv = webEnv;
        }

        [HtmlAttributeName("add-machine")]
        public bool AddMachine { get; set; }
        [HtmlAttributeName("add-os")]
        public bool AddOs { get; set; }
        [HtmlAttributeName("add-env-var")]
        public bool AddEnvironmentVariable { get; set; }
        [HtmlAttributeName("add-proxy")]
        public bool AddRProxyInfo { get; set; }

        public override int Order => base.Order;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("id","env-info");
            
            StringBuilder sb = new StringBuilder();
            
            if (AddMachine)
            {
                sb.Append("<strong>Machine:</strong>");
                sb.Append($"<span>{_htmlEncoder.Encode(Environment.MachineName)}</span>");
            }
            if (AddOs)
            {
                sb.Append("<strong>OS:</strong>");
                sb.Append($"<span>{_htmlEncoder.Encode(Environment.OSVersion.ToString())}</span>");
            }
            if (AddEnvironmentVariable)
            {
                sb.Append("<strong>Environment:</strong>");
                sb.Append($"<span>{_htmlEncoder.Encode(_env.EnvironmentName)}</span>");
            }
            if (AddEnvironmentVariable)
            {
                sb.Append("<strong>Listenning on:</strong>");
                sb.Append($"<span>{_config["ASPNETCORE_URLS"]}</span>");
            }
            
            //if (AddRProxyInfo)
            //{
            //    sb.Append("<strong>Proxy:</strong>");
            //    sb.Append(_htmlEncoder.Encode(_config.));
            //}
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}

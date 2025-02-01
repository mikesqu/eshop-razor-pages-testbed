using MadStickWebAppTester.Extensions;

namespace MadStickWebAppTester.Utilities
{
    public class SlugParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value?.ToString().ToSlug();
        }
    }
}

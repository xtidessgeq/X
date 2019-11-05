using X.Core.Packs;

namespace X.Core.Builders
{
    /// <summary>
    /// IOSharpBuilder扩展方法
    /// </summary>
    public static class OsharpBuilderExtensions
    {
        /// <summary>
        /// 添加CorePack
        /// </summary>
        public static IOsharpBuilder AddCorePack(this IOsharpBuilder builder)
        {
            return builder.AddPack<OsharpCorePack>();
        }
    }
}
using System.ComponentModel;

using X.AspNetCore;
using X.Core.Packs;


namespace X.Swagger
{
    /// <summary>
    /// SwaggerApi模块
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    [Description("SwaggerApi模块 ")]
    public class SwaggerPack : SwaggerPackBase
    { }
}
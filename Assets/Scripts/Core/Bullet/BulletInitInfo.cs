using UnityEngine;

namespace Core.Bullet
{
    public record BulletInitInfo(Vector2 position, Vector2 direction);
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// 手动定义此类型以支持在旧版本 .NET 中使用 C# 的 init 和 record 特性
    /// </summary>
    internal static class IsExternalInit {}
}
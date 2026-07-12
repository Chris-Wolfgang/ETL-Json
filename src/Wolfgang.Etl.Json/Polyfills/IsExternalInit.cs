// Required for 'init' property setters on .NET Framework and netstandard2.0 targets.
// The compiler emits a reference to this type, but the BCL only defines it in net5.0+.

#if !NET5_0_OR_GREATER
using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    /// <summary>Reserved for compiler use.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}
#endif

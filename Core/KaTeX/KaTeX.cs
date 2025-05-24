using Markdig.Renderers;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlHell_per.Core.KaTeX;

internal static class KaTeX
{
    public static async Task RenderKatexAsync(IJSRuntime jSRuntime, ElementReference formulaContainer, string latex) => 
        await jSRuntime.InvokeVoidAsync("renderKaTeX", formulaContainer, latex);
}

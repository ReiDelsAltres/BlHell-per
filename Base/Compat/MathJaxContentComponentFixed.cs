using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BlHell_per.Base.Compat;
public class MathJaxContentComponentFixed : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;

    [Inject]
    private IJSRuntime jsRuntime { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder?.AddContent(0, ChildContent);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JSRuntimeExtensions.InvokeAsync<IJSObjectReference>(jsRuntime, "import", new object[1] { "./_content/MathJaxBlazor/mathJaxBlazor.js" });
        }
        while (module == null) module = await JSRuntimeExtensions.InvokeAsync<IJSObjectReference>(jsRuntime, "import", new object[1] { "./_content/MathJaxBlazor/mathJaxBlazor.js" });
        if (module != null) await module.InvokeVoidAsync("typesetPromise");

        await base.OnAfterRenderAsync(firstRender);
    }

    public async ValueTask DisposeAsync()
    {
        if (module != null)
        {
            await module.InvokeVoidAsync("typesetClear");
            await module.DisposeAsync();
        }
    }
}

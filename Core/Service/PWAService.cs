using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Core.Service;

public class PWAService
{
    public bool IsOnline { get; set; }
    private object? Handler { get; set; }

    public event Action<bool>? OnOnlineChange;

    public async Task Listen(IJSRuntime jSRuntime)
    {
       Handler = await jSRuntime.InvokeAsync<object>("PWA_Initialize", DotNetObjectReference.Create(this));
    }

    public async ValueTask DisposeAsync(IJSRuntime jSRuntime)
    {
        await jSRuntime.InvokeVoidAsync("PWA_Dispose", Handler);
        Handler = null;
        OnOnlineChange = null;
    }

    [JSInvokable("PWAService.StatusChanged")]
    public void OnConnectionStatusChanged(bool isOnline)
    {
        if (IsOnline != isOnline)
        {
            IsOnline = isOnline;
        }
        OnOnlineChange?.Invoke(IsOnline);
    }
}

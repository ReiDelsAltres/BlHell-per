using BlHell_per.Core.Questions;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace BlHell_per.Core;

public static class PWA
{
    public static async Task<string> DeserializeResource(IJSRuntime jSRuntime,string resource)
    {
        // Передаем URL JSON-файла в нашу функцию loadJSON
        return await jSRuntime.InvokeAsync<string>(
            "loadJSON",
            $"https://reidelsaltres.github.io/BlHell-per/{resource}");
    }
    public static async Task<TResult> AutoDeserialize<TResult>()
    {

    }

}

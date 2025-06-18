using BlHell_per.Core.Questions;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace BlHell_per.Core;

public static class PWA
{
    public static async Task<TResult> UniversalDeserializeJson<TResult>(string path,IJSRuntime jSRuntime, HttpClient client,JsonSerializerOptions options)
    {
        byte[] buffer;

        buffer = await client.GetByteArrayAsync(path + ".br");

        Console.WriteLine("Before Brotli");
        Console.WriteLine(buffer);
        buffer = await PWA.DecompressBrotli(buffer, jSRuntime);
        Console.WriteLine(buffer);
        Console.WriteLine("After Brotli");

        string str = Encoding.UTF8.GetString(buffer);
        Console.WriteLine("String");
        Console.WriteLine(str);

        return JsonSerializer.Deserialize<TResult>(str, options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");
    }
    public static async Task<bool> DoesUrlExist(string url, HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        } catch
        {
            return false;
        }
    }
    public static async Task Alert(string str, IJSRuntime jSRuntime) =>
            await jSRuntime.InvokeAsync<byte[]>("alert1", str);
    public static async Task<byte[]> DecompressBrotli(byte[] bytes, IJSRuntime jSRuntime) =>
            await jSRuntime.InvokeAsync<string>("decompressBrotli", bytes).AsTask().FromBase64ToArray();
    public static async Task<byte[]> LoadFromCache(string url, IJSRuntime jSRuntime) =>
        await jSRuntime.InvokeAsync<string>("loadFromCache", $"https://reidelsaltres.github.io/BlHell-per/{url}").AsTask().FromBase64ToArray();
}

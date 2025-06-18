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

        if (await PWA.DoesUrlExistIncludingCache(path + ".br", client,jSRuntime))
        {
            Console.WriteLine("Brotli");
            buffer = await PWA.GetBytesArrayAsyncOrCache(path + ".br",client,jSRuntime);

            buffer = await Brotli.TryDecompressBrotli(buffer, jSRuntime);

        } else
        {
            Console.WriteLine("NO Brotli");
            buffer = await PWA.GetBytesArrayAsyncOrCache(path, client, jSRuntime);
        }


        string str = Encoding.UTF8.GetString(buffer);

        return JsonSerializer.Deserialize<TResult>(str, options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");
    }

    public static async Task<bool> DoesUrlExistIncludingCache(string url, HttpClient client, IJSRuntime jSRuntime)
    {
        try
        {
            if (await PWA.DoesUrlExist(url, client))
            {
                return true;
            }
            else
            {
                await PWA.LoadFromCache(url, jSRuntime);
                return true;
            }
        } catch
        {
            return false;
        }
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
    public static async Task<byte[]> GetBytesArrayAsyncOrCache(string path, HttpClient client, IJSRuntime jSRuntime)
    {
        try
        {
            return await client.GetByteArrayAsync(path);
        }
        catch
        {
            return await PWA.LoadFromCache(path, jSRuntime);
        }
    }

    public static async Task Alert(string str, IJSRuntime jSRuntime) =>
            await jSRuntime.InvokeAsync<byte[]>("alert1", str);
    public static async Task<byte[]> LoadFromCache(string url, IJSRuntime jSRuntime) =>
    await jSRuntime.InvokeAsync<string>("loadFromCache", $"https://reidelsaltres.github.io/BlHell-per/{url}").AsTask().FromBase64ToArray();
}

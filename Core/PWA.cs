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
        bool isBrotli = await PWA.DoesUrlExist(path + ".br",client);
        Console.WriteLine("IsBrotli: " + isBrotli);

        byte[] buffer;
        try
        {
            if (isBrotli)
            {
                buffer = await client.GetByteArrayAsync(path + ".br");
                buffer = await PWA.Decompress(buffer, jSRuntime);
            } else
            {
                buffer = await client.GetByteArrayAsync(path);
            }

        } catch (Exception _) 
        {
            if (isBrotli)
            {
                buffer = await PWA.LoadFromCache(path + ".br", jSRuntime);
                buffer = await PWA.Decompress(buffer, jSRuntime);
            } else
            {
                buffer = await PWA.LoadFromCache(path, jSRuntime);
            }
        }
        string str = Encoding.UTF8.GetString(buffer);

        return JsonSerializer.Deserialize<TResult>(str, options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");
    }
    public static async Task<bool> DoesInternetExist()
    {
        try
        {
            Ping myPing = new Ping();
            String host = "google.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            PingReply reply = await myPing.SendPingAsync(host, timeout, buffer, pingOptions);
            return (reply.Status == IPStatus.Success);
        }
        catch (Exception)
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
    public static async Task<byte[]> Decompress(byte[] bytes, IJSRuntime jSRuntime) =>
    await jSRuntime.InvokeAsync<byte[]>("decompressWithFflate", bytes);
    public static async Task<byte[]> LoadFromCache(string url, IJSRuntime jSRuntime) =>
        await jSRuntime.InvokeAsync<byte[]>("loadFromCache", url);
}

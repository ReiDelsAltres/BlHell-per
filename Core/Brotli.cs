using Microsoft.JSInterop;

namespace BlHell_per.Core;

public class Brotli
{
    public static async Task<byte[]> TryCompressBrotli(byte[] bytes, IJSRuntime jSRuntime)
    {
        try
        {
            return await Brotli.CompressBrotli(bytes, jSRuntime);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Compression failed: {ex.Message}");
            return bytes;
        }
    }
    public static async Task<byte[]> TryDecompressBrotli(byte[] bytes, IJSRuntime jSRuntime)
    {
        try
        {
            return await Brotli.DecompressBrotli(bytes, jSRuntime);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decompression failed: {ex.Message}");
            return bytes;
        }
    }
    public static async Task<byte[]> DecompressBrotli(byte[] bytes, IJSRuntime jSRuntime) =>
        await jSRuntime.InvokeAsync<string>("decompressBrotli", bytes).AsTask().FromBase64ToArray();
    public static async Task<byte[]> CompressBrotli(byte[] bytes, IJSRuntime jSRuntime) =>
        await jSRuntime.InvokeAsync<string>("compressBrotli", bytes).AsTask().FromBase64ToArray();
}

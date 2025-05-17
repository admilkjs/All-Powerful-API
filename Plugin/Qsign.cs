using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;

namespace QQBot_Jump.Plugin;

public class TokenResponse
{
    public int Code { get; set; }
    public DataPayload Data { get; set; } = new();
    public string Msg { get; set; } = "";
}
public class DataPayload
{
    public string? Token { get; set; }
    public string? Extra { get; set; }
    public string? Sign { get; set; }
}


[ApiController]
[Route("[controller]")]
public class QsignController : ControllerBase
{
    [HttpGet(Name = "Qsing")]
    public IActionResult Get()
    {
        return Ok(CryptoHelper.GenerateHexTokenResponse());
    }
}
public static class CryptoHelper
{
    public static string GenerateHexTokenResponse()
    {
        var response = new TokenResponse
        {
            Code = 0,
            Data = new DataPayload
            {
                Token = GenerateRandomHex(12),
                Extra = GenerateRandomHex(108),
                Sign = GenerateRandomHex(72)
            },
            Msg = "success"
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(response, options);
    }

    private static string GenerateRandomHex(int byteLength)
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(byteLength);
        return Convert.ToHexString(bytes).ToLower();
    }
}
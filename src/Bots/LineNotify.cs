using System.Text;
using System.Text.Json;

namespace app.Bots;

class TokenResponseDTO
{
    public int status { get; set; }
    public required string message { get; set; }
    public required string access_token { get; set; }
}

public class LineNotify
{
    private readonly HttpClient _client;

    public LineNotify(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public String GetLineNotifyAuthorizeLink(String state)
    {
        var clientId = Environment.GetEnvironmentVariable("LINE_NOTIFY_CLIENT_ID");
        var publicUrl = Environment.GetEnvironmentVariable("PUBLIC_URL");

        var link = "https://notify-bot.line.me/oauth/authorize";
        link += "?response_type=code";
        link += $"&client_id={clientId}";
        link += $"&redirect_uri={publicUrl}/line/notify/callback";
        link += "&scope=notify";
        link += $"&state={state}";

        return link;
    }

    public async Task<String?> GetTokenAsync(String code)
    {
        var link = "https://notify-bot.line.me/oauth/token";

        var clientId = Environment.GetEnvironmentVariable("LINE_NOTIFY_CLIENT_ID")!;
        var clientSecret = Environment.GetEnvironmentVariable("LINE_NOTIFY_CLIENT_SECRET")!;
        var publicUrl = Environment.GetEnvironmentVariable("PUBLIC_URL");

        var payload = new Dictionary<String, String>();
        payload.Add("code", code);
        payload.Add("client_id", clientId);
        payload.Add("client_secret", clientSecret);
        payload.Add("grant_type", "authorization_code");
        payload.Add("redirect_uri", $"{publicUrl}/line/notify/callback");

        var content = new FormUrlEncodedContent(payload);
        HttpResponseMessage res = await _client.PostAsync(link, content);
        if (res.IsSuccessStatusCode)
        {
            var body = await res.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TokenResponseDTO>(body)!;
            return data.access_token;
        }

        return null;
    }

    public async Task<Boolean> SendMessageAsync(String token, String message)
    {
        var link = "https://notify-api.line.me/api/notify";

        var request = new HttpRequestMessage(HttpMethod.Post, link);
        request.Headers.Add("Authorization", $"Bearer {token}");

        var payload = new Dictionary<String, String>();
        payload.Add("message", message);

        request.Content = new FormUrlEncodedContent(payload);

        HttpResponseMessage res = await _client.SendAsync(request);

        return res.IsSuccessStatusCode;
    }
}
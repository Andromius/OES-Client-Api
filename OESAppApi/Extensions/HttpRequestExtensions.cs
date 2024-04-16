namespace OESAppApi.Extensions;

public static class HttpRequestExtensions
{
    public static string ExtractToken(this HttpRequest request) => request.Headers.Authorization.Single()!.Replace("Bearer ", "");
}

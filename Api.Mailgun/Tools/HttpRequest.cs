namespace Api.Mailgun
{
    /// <summary>
    /// Http request message with the specified response type
    /// </summary>
    /// <typeparam name="T">Type of the expected response</typeparam>
    class HttpRequest<T> : System.Net.Http.HttpRequestMessage
    {
    }
}

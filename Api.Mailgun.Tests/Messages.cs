using Xunit;

namespace Api.Mailgun.Tests
{
    public class Messages
    {
        [Fact]
        public async void Test()
        {
            var mg = new Mailgun(Settings.WorkDomain, Settings.ApiKey);

            var send = await mg.SendMessageAsync(new EmailAddress(Settings.Sender), new EmailAddress(Settings.Recipient), "Test message", "<h1>Hello!</h1><p>This is a test message.</p>");
            Assert.True(send.Successful, send.ErrorMessage);
            Assert.NotEmpty(send.Response.MessageId);
        }
    }
}

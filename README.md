## .NET Mailgun API wrapper

<a href="https://www.nuget.org/packages/PureApi.Mailgun/"><img src="https://img.shields.io/nuget/v/PureApi.Mailgun.svg" /></a>
<a href="https://www.nuget.org/packages/PureApi.Mailgun/"><img src="https://img.shields.io/nuget/dt/PureApi.Mailgun.svg" /></a>
<a href="https://github.com/Groxan/Api.Mailgun/blob/master/LICENSE"><img src="https://img.shields.io/github/license/groxan/Api.Mailgun.svg" /></a>

This is a library for working with Mailgun API. It is compatible with both .Net Core and .Net Framework.

Currently supports:
- Messaging (see [examples](#sending-email));
- Mailing lists (see [examples](#working-with-mailing-lists));
- Routes (see [examples](#working-with-routes)).

#### Feature request
The current version of the library satisfies all my needs. Additional features will be implemented when someone ask for it. So if you need some feature, you may create an "Issue" and describe what you want.

## Install
`PM> Install-Package PureApi.Mailgun`
## How to use
First of all, you need to create a [Mailgun account](https://www.mailgun.com/), create a domain that will be used to send emails, and get an API key for your account. Then you can use this library to send emails from your domain.
#### Create a Mailgun instanse
```csharp
var mailgun = new Mailgun("sending-domain.com", "api-key");
```
### Sending email
You can send email messages with any parameters, that supported by [Mailgun API.](https://documentation.mailgun.com/en/latest/api-sending.html#sending)
#### Send simple message
```csharp
var result = await mailgun.SendMessageAsync(
                new EmailAddress("support@your-domain.com", "Support Team"), // From
                new EmailAddress("user@gmail.com"),                          // To
                "Welcome",                                                   // Subject
                "Welcome, dear user!");                                      // Message

if (!result.Successful)
    Console.WriteLine(result.ErrorMessage);
```
#### Build message with required parameters
```csharp
var message = new Message()
{
    From = new EmailAddress("support@your-domain.com", "Support Team"),
    To = new List<EmailAddress>()
    {
        new EmailAddress("user1@gmail.com"),
        new EmailAddress("user2@gmail.com")
    },
    Cc = new List<EmailAddress>()
    {
        new EmailAddress("user3@gmail.com")
    },
    Attachments = new List<Attachment>()
    {
        new Attachment("/images/photo-1.jpg")
    },
    Subject = "Hello",
    Html = "<h1>Hello, dear user!</h1>",
    Dkim = true,
    RequireTls = true,
    Tracking = true
};

var result = await mailgun.SendMessageAsync(message);
```
### Working with mailing lists
Mailing Lists provide a convenient way to send to multiple recipients by using an alias email address. Mailgun sends a copy of the message sent to the alias address to each subscribed member of the Mailing List.

Mailing Lists API methods are available in the `MailingListManager` class.
#### Create new mailing list
```csharp
var result = await mailgun.Lists.CreateMailingListAsync("news");
```
#### Subscribe member to list
```csharp
var result = await mailgun.Lists.AddMemberToListAsync("news", "user@gmail.com");
```
#### Send simple message to all members in list
```csharp
var result = await mailgun.SendMessageToListAsync(
                "news",                                                      // Mailing list
                new EmailAddress("support@your-domain.com", "Support Team"), // From
                "Welcome",                                                   // Subject
                "Welcome, dear users!");                                     // Message
```
#### Unsubscribe member from list
```csharp
var result = await mailgun.Lists.UpdateMemberStatusAsync("news", "user@gmail.com", false);
```
### Working with routes
Mailgun Routes are a powerful way to handle the incoming traffic. 

Routes API methods are available in the `RouteManager` class. It's recommended to use the `RouteFilters` helper class to create filter expressions and the `RouteActions` helper class to create a set of actions.
```csharp
var result = await mailgun.Routes.CreateRouteAsync(
                RouteFilters.MatchRecipient(".*@bar.com"), 
                RouteActions.AddForward("http://callback.com").AddStore());

if (result.Successful)
    Console.WriteLine(result.Response.Route.Id);
```

# Msky

Misskey library for .NET Standard 2.0

Under development yet.

## Usage

### Authentication / Authorization

Method1: No Authentication. For use API as anonymous.

``` Csharp
var misskey = new Misskey("https://misskey.example.com");
```

Method2: Authenticate with predefined API key, or past authorized API key.

``` Csharp
var misskey = new Misskey("https://misskey.example.com", "API Key");
```

Method3: Authenticate/Authorize by Authorize feature

``` Csharp
var misskey = new Misskey("https://misskey.example.com");

AuthSession session = await misskey.Auth.Session.GenerateAsync("App's secret key");

Console.WriteLine("Open URL: {0}", session.Url);
try { System.Diagnostics.Process.Start(session.Url); }  // Fail on .NET Core
catch { Console.WriteLine("Failed to open url. Please manually open it."); }

Console.WriteLine("Please authorize in your browser, then push Enter.");
Console.ReadLine();

UserKey userkey = await misskey.Auth.Session.UserkeyAsync(appSecret, session.Token);

string apiKey = misskey.UpdateApiKey(userkey.AccessToken, appSecret);
// This apikey may use on method2.
```

### API example

Get server meta data

``` Csharp
Meta meta = await misskey.MetaAsync();
Console.WriteLine(string.Format("Version: {0}", meta.Version));
Console.WriteLine(string.Format("ClientVersion: {0}", meta.ClientVersion));
```

Posting

``` Csharp
await misskey.Notes.CreateAsync("Note text");
```

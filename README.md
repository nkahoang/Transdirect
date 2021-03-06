# Transdirect Library for .NET Standard

This library is MIT Licensed

[![Build status](https://ci.appveyor.com/api/projects/status/j22642256e9q5fef?svg=true&passingText=master%20-%20OK)](https://ci.appveyor.com/project/nkahoang/transdirect)
[![MyGet](https://img.shields.io/myget/nkahoang/dt/Transdirect-NetStandard.svg?label=MyGet%20downloads)](https://www.myget.org/feed/nkahoang/package/nuget/Transdirect-NetStandard)

## 1. Introduction

The Transdirect Library is a .NET Standard wrapper for Transdirect API v4 (https://www.transdirect.com.au/education/developers-centre/rest-api/, document at http://docs.transdirectapiv4.apiary.io/). The library is compatible with .NET Standard 1.4+ (.NET Core 1.0+, .NETFramework 4.5+).

At the moment the following endpoints are implemented:

- Simple Quote
- Bookings (CURD, Confirm, Track)
- Booking Items (CURD)
- Members (Get current member)
- Couriers (Get all supported couriers)

Additional endpoints are in development.

## 2. Adding reference

NuGet.org package shall be available in the future. At the moment, please either add MyGet package source (built straight from `master`) or add direct reference to the repo.

### 2A. Add MyGet package
Following instructions: https://www.myget.org/feed/nkahoang/package/nuget/Transdirect-NetStandard

### 2B. Add direct reference
1. Clone this repo:

```bash
git clone https://github.com/nkahoang/Transdirect.git
```

2. Add preference to this project in your .csproj file:

```xml
  <ItemGroup>
    <ProjectReference Include="..\Transdirect\Transdirect.csproj" />
    <!-- [Other preferences]-->
  </ItemGroup>
```

## 3. Getting the API Key

Access https://www.transdirect.com.au/members/api/apimodules, create a "Custom Site" module for your project.

## 4. Using the library

There are two methods to use the library:

### 4A. Quick and dirty way

```csharp
var options = new TransdirectOptions() {
    ApiKey = "api-key-goes-here";
    // Other configs
};

var transdirectService = new TransdirectService(options);

// All endpoints are exposed via transdirectService object.
```

### 4B. Use DI container and Application startup (Recommended)

Put the following in your `appsettings.json` (or your application's config file):

```json
{
  // [...] your other settings
  // Put TransDirect section in
  "Transdirect": {
    "ApiKey": "your-api-key",
    "BaseUrl": "https://www.transdirect.com.au/api",
    "CouriersCacheDuration": "00:05:00"
  }
}
```

Then in your application initialization:

```csharp

// in your Startup.cs
using Transdirect.Extensions;

/* This should come with a standard ASP.NET Core / Owin startup */
public Startup (IHostingEnvironment env) {
    _currentEnvironment = env;

    var builder = new ConfigurationBuilder ()
        .SetBasePath (env.ContentRootPath)
        .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
        .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
        .AddEnvironmentVariables ();
    Configuration = builder.Build ();
}

/* Then register the PinPayments service */
public void ConfigureServices (IServiceCollection services) {
    services.AddTransdirect(Configuration);
}

```

From here onward, simply inject in `ITransdirectService` in your constructor.


### 5. Examples / Running Test app.

Working examples for Transdirect are in the PinPayments.Test app, specifically under `TransdirectTest.cs` file. 

To run the Test project:

- Clone this repo.
- Copy `Transdirect.Test\config.sample.json` to `Transdirect.Test\config.json` and set the correct API.
- Perform a `dotnet restore` inside Transdirect.Test folder.
- Run the sample console app: `dotnet run`.

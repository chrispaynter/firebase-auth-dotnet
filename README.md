# .NET Firebase Rest Authentication API Client
A .NET API client for Firebase Authentication REST API that aims to follow the Google Firebase Auth API and documentation exactly.

The official documentation can be found [here](https://firebase.google.com/docs/reference/rest/auth/), and I link to the appropriate sections of Firebase's docs as we discuss the endpoints below.

**NOTE: This is a work in progress**
It's a work in progress, but the foundation of this library is clean and well tested, so adding endpoints should be straightforward and pull requests are welcome over at the project repository on GitHub.

## Getting Started

### Installation
You can add *firebase-auth-dotnet* to your solution by using Nuget (https://www.nuget.org/packages/Firebase.Auth.Rest)

On the command line use:
~~~~
dotnet add package Firebase.Auth.Rest
~~~~

In Nuget package manager use:
~~~~
Install-Package Firebase.Auth.Rest
~~~~

### Creating The API Service
The `FirebaseAuthService` class will contains all the endpoints that the Firebase Rest API offers. This class requires a `FirebaseAuthOptions` object to be passed through in it's constructor, which contians keys required to connect and authentiate with Firebase API.

~~~~
var authOptions = new FirebaseAuthOptions()
{
    WebApiKey = "<YOUR PROJECT'S WEB API KEY>"
}

var firebase = new FirebaseAuthService(authOptions);
~~~~

### Using .NET Core
If you are using **dotnet core**, you can configure this at the configuration level, so that `FirebaseAuthService` is injected into your classes automatically configured.

In your **Startup.cs** file you might do something like this.
~~~~
public void ConfigureServices(IServiceCollection services)
{
    var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();
    services.AddScoped<FirebaseAuthProvider>(u => new FirebaseAuthProvider(new FirebaseConfig(authOptions.WebApiKey)));
}
~~~~
Then in your **appsettings.json** or your secrets configuration, add this.
~~~~
{
  "AuthOptions": {
    "WebApiKey": "<YOUR PROJECT'S WEB API KEY>"
  }
}
~~~~


# Endpoints


>**Only some endpoints are available (for now).**
>
>The SDK is currently being implemented on an endpoint by endpoint basis (as I need them).
All code examples use the `FirebaseAuthService` that we saw created in the [Getting Started](#getting-started) section. 
> * [Sign up with email and password](#Sign-up-with-email-and-password)
> * [Sign in with email and password](#Sign-in-with-email-and-password)

>**You can follow Firebase's Official documentation in conjunction with this.**
>
>This client follows the official Firebase Auth Rest API documentation as closely as possible. Thus, for more detailed info there is a link for each endpoint to it's official documentation. 


>**Only Async method calls are supported** 
>
>All endpoints are implemented as standard .NET asynchronous methods.


## Sign up with email and password
>Creates a new email and password user.

|||
|-----|-----|
| **Firebase API Endpoint** | `signupNewUser` |
| **Request C# Type** | `Firebase.Auth.Payloads.SignUpNewUserRequest` |
| **Response C# Type**| `Firebase.Auth.Payloads.SignUpNewUserResponse` |
| **Endpoint Official Documentation** | [Go to Firebase](https://firebase.google.com/docs/reference/rest/auth/#section-create-email-password) |

### Example
~~~~
var request = new SignUpNewUserRequest()
{
    Email = "valid@test.com",
    Password = "validpassword"
};

try
{
    var response  await firebase.SignUpNewUser(request);
}
catch(FirebaseAuthException e)
{
    // App specific error handling.
}

~~~~

## Sign in with email and password
>Signs an existing user in with their email and password.

|||
|-----|-----|
| **Firebase API Endpoint** | `verifyPassword` |
| **Request C# Type** | `Firebase.Auth.Payloads.VerifyPasswordRequest` |
| **Response C# Type**| `Firebase.Auth.Payloads.VerifyPasswordResponse` |
| **Endpoint Official Documentation** | [Go to Firebase](https://firebase.google.com/docs/reference/rest/auth/#section-create-email-password) |

### Example
~~~~
var request = new VerifyPasswordRequest()
{
    Email = "valid@test.com",
    Password = "validpassword"
};

try
{
    var response  await firebase.VerifyPassword(request);
}
catch(FirebaseAuthException e)
{
    // App specific error handling.
}

~~~~

## Unit Tests
Each endpoint comes with a comprehensive suite of tests to ensure the SDK passes and receives data as expected per the documentation, as well as handles error circumstances.
### How to configure secrets.json for unit testing

It is expected by the test suite that a file called `secrets.json` exists in the root directory of the `Firebase.Auth.IntegrationTests` project. This file contains the secret keys needed to access Firebase for these integration tests.

There is a `.gitignore` file at the root of this project which prevents the `settings.json` file from accidently being committed to the repository with the secrets.
~~~~
{
  "firebaseWebApiKey": "<YOUR FIREBASE PROJECT'S WEB API KEY>",
  "validUserEmail": "<THE EMAIL OF A VALID USER>",
  "validUserPassword":  "<THE PASSWORD OF THE SAME VALID USER>",
  "validDisabledEmail": "<THE EMAIL OF A DISABLED USER>",
  "validDisabledPassword": "<THE PASSWORD OF THE SAME DISABLED USER>"
}
~~~~
## Publishing Nuget Package
https://docs.microsoft.com/en-us/nuget/guides/create-net-standard-packages-vs2017

You'll need the MSBuild executable, likely located here:
`C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin`

I generally add that to my path as I don't need any other version of MSBuild available on the command line.

The project has been set to generate a nuget packag on build, but to package manually, run this command from the `Firebase.Auth` project directoy.
~~~~
msbuild /t:pack /p:Configuration=Release
~~~~

Then, once you have the package, to push to nuget
~~~~
nuget push Firebase.Auth.Rest.1.0.1.nupkg -Source https://www.nuget.org/api/v2/package
~~~~

# firebase-auth-dotnet
A .NET API client for Firebase Authentication REST API that aims to follow the Google Firebase Auth API and documentation exactly.

The official documentation can be found [here](https://firebase.google.com/docs/reference/rest/auth/), and I link to the appropriate sections of Firebase's docs as we discuss the endpoints below.

**NOTE: This is a work in progress** - I'm currently only implementing the end points I need in my current projects. If you'd like to submit a pull request, please do!

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


## Endpoints
The SDK is currently being implemented on an endpoint by endpoint basis, with the following Firebase Auth endpoints having been implemented thus far.

All the following example code follows on from the instantiation of the `FirebaseAuthService` that we saw in the Getting Started section.

Finally, the documentation of each endpoint's requests and responses are light here, because it follows the Firebase API exactly. Thus, each section will link through to the appropriate Firebase endpoint docs.


**NOTE ON ASYNC:** All endpoints are implemented as asynchronous.
***
### Sign up with email / password
Creates a new email and password user.

|||
|-----|-----|
| **Firebase API Endpoint** | `signupNewUser` |
| **Request C# Type** | `SignUpNewUserRequest` |
| **Response C# Type**| `SignUpNewUserResponse` |
| **Official Documentation** | [Go to Firebase](https://firebase.google.com/docs/reference/rest/auth/#section-create-email-password) |

#### Example
~~~~
var request = new SignUpNewUserRequest()
{
    Email = "valid@test.com",
    Password = "validpassword"
};

var response = await firebase.SignUpNewUser(request);
~~~~

## Unit Tests
Each endpoint comes with a comprehensive suite of tests to ensure the SDK passes and receives data as expected per the documentation, as well as handles error circumstances.
### Firebase.Auth.IntegrationTests

#### secrets.json
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

I add that to my path.

Run this command from the `Firebase.Auth` project directoy.
~~~~
msbuild /t:pack /p:Configuration=Release
~~~~

Then to push to nuget
~~~~
nuget push Firebase.Auth.Rest.1.0.1.nupkg -Source https://www.nuget.org/api/v2/package
~~~~

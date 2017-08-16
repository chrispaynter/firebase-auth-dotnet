# firebase-auth-dotnet
A dotnet standard2.0 SDK for Firebase Authentication that aims to follow the Google Firebase Auth API exactly, as per the official documentation - https://firebase.google.com/docs/reference/rest/auth/.

**This is a work in progress - **
I'm currently only implementing the end points I need in my current projects. If you'd like to submit a pull request, please do!

## Getting Started
The `FirebaseAuthService` class will contains all the endpoints that the Firebase Rest API offers. This class requires a `FirebaseAuthOptions` object to be passed through in it's constructor, which contians keys required to connect and authentiate with Firebase API.

~~~~
var authOptions = new FirebaseAuthOptions()
{
    WebApiKey = "<YOUR PROJECT'S WEB API KEY>"
}

var authService = new FirebaseAuthService(authOptions);
~~~~

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
The SDK is currently being implemented on an endpoint by endpoint basis. The following Firebase Auth endpoints have been implemented thus far.

### [Sign up with email / password](https://firebase.google.com/docs/reference/rest/auth/#section-create-email-password)


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

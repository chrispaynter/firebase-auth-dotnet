# firebase-auth-dotnet
A dotnet standard2.0 client for Firebase Authentication that aims to follow the Google Firebase Auth API exactly, as per the official documentation - https://firebase.google.com/docs/reference/rest/auth/.

## Running Unit Tests
### Firebase.Auth.IntegrationTests

#### secrets.json
It is expected by the test suite that a file called `secrets.json` exists in the root directory of the `Firebase.Auth.IntegrationTests` project. This file contains the secret keys needed to access Firebase for these integration tests.

There is a `.gitignore` file at the root of this project which prevents the `settings.json` file from accidently being committed to the repository with the key.
~~~~
{
  "firebaseWebApiKey": "<YOUR FIREBASE PROJECT'S WEB API KEY>",
  "validUserEmail": "<THE EMAIL OF A VALID USER>",
  "validUserPassword":  "<THE PASSWORD OF THE SAME VALID USER>",
  "validDisabledEmail": "<THE EMAIL OF A DISABLED USER>",
  "validDisabledPassword": "<THE PASSWORD OF THE SAME DISABLED USER>"
}
~~~~
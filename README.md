This is a diff/merge app for C# model classes to SQL Server. You can also merge from database to database. Here's my main [product page](https://aosoftware.net/modelsync/).

[![download](https://img.shields.io/badge/Download-Installer-blue.svg)](https://aosoftware.blob.core.windows.net/install/ModelSyncSetup.exe)

Icon looks like this:

![img](https://adamosoftware.blob.core.windows.net/images/R6CAG0JHJQ.png)

The app has a 30-day fully functional free trial. After that, a perpetual license is $50 USD.

[![paypal](https://www.paypalobjects.com/webstatic/mktg/logo/pp_cc_mark_74x46.jpg)](https://paypal.me/adamosoftware?locale.x=en_US)

You're welcome to clone and examine this repo of course. If you use Model Sync for real, I do ask you to please buy a license. You may not repackage and redistribute ModelSync outside your organization, however.

If you buy a license, the PayPal notification will make me [create a key for you](https://github.com/adamfoneil/AOLicensing/blob/master/AOLicensing.KeyManager/Program.cs#L22), and you'll [receive](https://github.com/adamfoneil/AOLicensing/blob/master/AOLicensing.Functions/CreateKey.cs#L65) it by email.

## Limitations
- ModelSync works only with .NET Standard assemblies that have no dependencies, except for [AO.Models](https://www.nuget.org/packages/AO.Models)
- Works only with SQL Server
- Requires some finesse in complex cases, meaning you have to run script blocks in a manual order or in multiple passes sometimes

## About the Repo
- The WinForms UI is the [App](https://github.com/adamfoneil/ModelSync.WinForms/tree/master/ModelSync.App) project, this repo. Note there is a [post build event](https://github.com/adamfoneil/ModelSync.WinForms/blob/master/ModelSync.App/ModelSync.App.csproj#L194) using my [AzDeploy](https://github.com/adamfoneil/AzDeploy) project that won't work on your machine that you will need to remove.
- You will also need to clone the [ModelSync library](https://github.com/adamfoneil/ModelSync) project because the App references the `ModelSync` library as a project within the solution. This allowed me to get the best debug experience.

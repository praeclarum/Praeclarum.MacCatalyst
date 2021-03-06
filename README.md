# Praeclarum.MacCatalyst

[[nuget](https://www.nuget.org/packages/Praeclarum.MacCatalyst/)]

Mac Catalyst support for Xamarin.iOS by Frank A. Krueger.

I am very pleased to provide this tool that will convert your iOS apps to
Mac Catalyst apps. It is still an early version and some things may not
work, but I have had a lot of success with my own apps.



## Requirements

* macOS 10.15
* .NET Core 3.1
* Xamarin.iOS

Verify that you have a Xcode's command line tools installed and the appropriate Xcode
selected using:

```bash
xcode-select --print-path
```

If that fails or is incorrect, you can set your version with:

```bash
sudo xcode-select --switch /Applications/Xcode.app
```

## Installation

Add the nuget [Praeclarum.MacCatalyst](https://www.nuget.org/packages/Praeclarum.MacCatalyst/) to your iOS project.

```xml
<PackageReference Include="Praeclarum.MacCatalyst" Version="1.0.0-beta3" />
```

That's it! Whenever you build, a Mac Catalyst version of your app will be put in:

```
bin/Platform/Configuration/Praeclarum.MacCatalyst/App.app
```

### Autorun

To automatically run the Mac Catalyst app whenever you build your app, set:

```xml
<MacCatalystAutoRun>true</MacCatalystAutoRun>
```

in a `PropertyGroup` in your project file.

### Enabling and disabling

Mac Catalyst is enabled by default. To disable it set:

```xml
<MacCatalystEnabled>false</MacCatalystEnabled>
```

in a `PropertyGroup` in your project file.

### Crash Details

If your app crashes when running, it can be useful to see its output from the command line.
You can do this by running its executable directly:

```bash
/App.app/Contents/MacOS/App
```


## App Store

Archiving and signing are not yet supported, but I hope to get that working soon.

You can probably do it manually at the command line, but I haven't figured the incantations out yet.


## Known Issues

* **AOT is not supported** Your code will run using the mono JIT.

* **Static registrar is not supported** Startup times are a little slow because the ObjC bridge has to be dynamically created.

* **Native code is not supported** The native binaries for Catalyst apps do not exactly match iOS or Mac binaries. This means libraries that ship with native code do not work.

* **AppCenter doesn't work** This is due to native SDKs not working. You may see an error like: `System.Exception: Could not create an native instance of the type 'Microsoft.AppCenter.iOS.Bindings.MSWrapperSdk': the native class hasn't been loaded.`

* **Extensions do not work**

* **Signing and Entitlements do not work**


## How it Works

This tool takes your already-compiled Xamarin.iOS .app and converts it to a macOS app by:

1. Recompiling your main.m linking against Mac Catalyst builds of Xamarin and mono
2. Modify external native library dependencies to run on macOS.
3. Repackaging your .app to match the layout of a macOS .app

Step 2 is only performed for libraries compiled with old versions of Xcode.
It's only possible thanks to the wonderful work of [Steven Troughton-Smith and his Marzipanify tool](https://github.com/steventroughtonsmith/marzipanify).


## Contributing

You are welcome to contribute! Please open a PR.

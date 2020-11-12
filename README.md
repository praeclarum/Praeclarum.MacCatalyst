# maccat

Mac Catalyst support for Xamarin.iOS by Frank Krueger.

I am very pleased to provide this tool that will convert your iOS apps to
Mac Catalyst apps. It is still an early version and some things may not
work, but I have had a lot of success with my own apps.

Thank you for supporting me, and let me know if it works for you!


## Requirements

* macOS 10.15
* .NET Core 3.1
* Xamarin.iOS


## Usage

To convert your iOS app to a Mac Catalyst app, run the `maccat` tool (script or .NET Core app):

```bash
cd maccat-sdk-YYYYmmdd
./maccat.sh /Path/To/Your/App.csproj
```

The tool will output a new `App.app` in the `$(ProjectDirectory)/bin/MacCatalyst/Release` directory. 

### Specify Configuration and Platform to convert

You can specify to different configurations and platforms on the command line:

```bash
./maccat.sh /Path/To/Your/App.csproj -c Debug -p iPhoneSimulator
```

### Crash Details

If your app crashes when running, it can be useful to see its output from the command line.
You can do this by running it's executable directly:

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


## Providing Feedback

Please join the Discord: [https://discord.gg/R3zVzg6M3d](https://discord.gg/R3zVzg6M3d)

You can contact me via:

* Twitter at [@praeclarum](https://twitter.com/praeclarum)
* Email at [fak@praeclarum.org](mailto:fak@praeclarum.org)




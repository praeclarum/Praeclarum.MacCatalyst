

RELEASE_DIR = ./release_out

SDK_NAME = maccat-sdk-$(shell date "+%Y%m%d").zip

XAMARIN_MACIOS = /Users/fak/Projects/xamarin-macios

IN_XAMARIN_DLL = $(XAMARIN_MACIOS)/_maccat-build/Library/Frameworks/Xamarin.iOS.framework/Versions/git/lib/64bits/Xamarin.iOS.dll
HACKED_XAMARIN_DLL = $(TMPDIR)/Xamarin.iOS.dll

all:

release: $(RELEASE_DIR) $(HACKED_XAMARIN_DLL)
	rm -rf $(RELEASE_DIR)/*
	dotnet publish Sdk/maccat.csproj
	cp -a README.md maccat.sh ./Sdk/catmain.m ./Sdk/bin/Debug/netcoreapp3.1/publish/maccat.dll ./Sdk/bin/Debug/netcoreapp3.1/publish/maccat.runtimeconfig.json $(RELEASE_DIR)
	mkdir -p $(RELEASE_DIR)/mono-maccat/lib
	cp -a ./Sdk/mono-maccat/lib/libmonosgen-2.0.a ./Sdk/mono-maccat/lib/libmono-native.a $(RELEASE_DIR)/mono-maccat/lib
	mkdir -p $(RELEASE_DIR)/xamarin-maccat
	cp -a $(XAMARIN_MACIOS)/_maccat-build/Library/Frameworks/Xamarin.Mac.framework/Versions/git/SDKs/Xamarin.macOSCatalyst.sdk $(RELEASE_DIR)/xamarin-maccat
	cp -a $(HACKED_XAMARIN_DLL) $(RELEASE_DIR)/xamarin-maccat
	mkdir -p ./Releases
	rm -rf maccat-sdk ./Releases/$(SDK_NAME)

release_zip: release
	cd $(RELEASE_DIR) && zip -r ../Releases/$(SDK_NAME) *

$(HACKED_XAMARIN_DLL): $(IN_XAMARIN_DLL)
	fsharpi --exec ./Sdk/FixEnums.fsx

$(RELEASE_DIR):
	mkdir -p $@






#!/bin/bash

set -e
set -x
set -u

echo Catify

CLANG=$(xcrun -f clang)
CFLAGS="-mmacosx-version-min=10.15 -arch x86_64 -fobjc-runtime=macosx-10.15 -Wno-unguarded-availability-new -std=c++14 -ObjC"
CFLAGS2="-lz -liconv -lc++ -x objective-c++ -stdlib=libc++"
CFLAGS3="-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot /Applications/Xcode_12GM.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX10.15.sdk"
DEFINES="-D_THREAD_SAFE"
FRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS"
XAMMACLIB="/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2/lib/libxammac.a"
US="-u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor"
OUT="/Users/fak/Dropbox/Projects/Catalyst/Research/Hello.macOS/bin/Release/Hello.macOS.app/Contents/MacOS/Hello.macOS"
INCLUDES="-I/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2/lib/pkgconfig/../../include/mono-2.0 -I/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2/include"
LINKS="/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2/lib/pkgconfig/../../lib/libmonosgen-2.0.a /Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2/lib/pkgconfig/../../lib/libmono-native-unified.a"
COMPILES="/Users/fak/Dropbox/Projects/Catalyst/Research/Hello.macOS/obj/Release/mmp-cache/registrar.m /Users/fak/Dropbox/Projects/Catalyst/Research/Hello.macOS/obj/Release/mmp-cache/main.m"

$CLANG $CFLAGS $FRAMEWORKS $US $XAMMACLIB -o $OUT $DEFINES $INCLUDES $LINKS $CFLAGS2 $COMPILES $CFLAGS3

#!/bin/bash

set -e
# set -x
set -u

# INPUT VARIABLES
APPNAME="Hello.macOS"
PROJDIR="/Users/fak/Dropbox/Projects/Catalyst/Research/Hello.macOS"

# ENVIRONMENT VARIABLES
CLANG=$(xcrun -f clang)
XAMMACDIR="/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2"

# COMPUTED VARIABLED
GENCODEDIR="$PROJDIR/obj/Release/mmp-cache"
OUTDIR="$PROJDIR/bin/Release/$APPNAME.app/Contents/MacOS"
CFLAGS="-mmacosx-version-min=10.15 -arch x86_64 -fobjc-runtime=macosx-10.15 -Wno-unguarded-availability-new -std=c++14 -ObjC"
CFLAGS2="-lz -liconv -lc++ -x objective-c++ -stdlib=libc++"
CFLAGS3="-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot /Applications/Xcode_12GM.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX10.15.sdk"
DEFINES="-D_THREAD_SAFE"
FRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS"
XAMMACLIB="$XAMMACDIR/lib/libxammac.a"
US="-u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor"
OUT="$OUTDIR/Hello.macOS"
INCLUDES="-I$XAMMACDIR/include/mono-2.0 -I$XAMMACDIR/include"
LINKS="$XAMMACDIR/lib/libmonosgen-2.0.a $XAMMACDIR/lib/libmono-native-unified.a"
COMPILES="$GENCODEDIR/registrar.m $GENCODEDIR/main.m"

# COMPILE AND LINK
echo Building $APPNAME

$CLANG $CFLAGS $FRAMEWORKS $US $XAMMACLIB -o $OUT $DEFINES $INCLUDES $LINKS $CFLAGS2 $COMPILES $CFLAGS3

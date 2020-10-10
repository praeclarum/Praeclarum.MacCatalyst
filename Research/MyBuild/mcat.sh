#!/bin/bash

set -e
# set -x
set -u

# INPUT VARIABLES
APPNAME="Hello.iOS"
PROJDIR="/Users/fak/Dropbox/Projects/Catalyst/Research/Hello.iOS"

REGISTRAR_SOURCE="/Users/fak/Dropbox/Projects/Catalyst/Research/Hello.macOS/obj/Release/mmp-cache/registrar.m"

# ENVIRONMENT VARIABLES
CLANG=$(xcrun -f clang)
XAMMACDIR="/Library/Frameworks/Xamarin.Mac.framework/Versions/6.20.2.2"
XAMMACCATDIR="/Users/fak/Projects/xamarin-macios/_maccat-build/Library/Frameworks/Xamarin.Mac.framework/Versions/git/SDKs/Xamarin.macOSCatalyst.sdk"
MACSDK="/Applications/Xcode_12.0.0.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX10.15.sdk"

# COMPUTED VARIABLED
GENCODEDIR="$PROJDIR/obj/iPhone/Release/mtouch-cache"
OUTDIR="."

CFLAGS="-target x86_64-apple-ios13.6-macabi -Wno-unguarded-availability-new -std=c++14 -ObjC"
CFLAGS2="-lz -liconv -lc++ -x objective-c++ -stdlib=libc++"
CFLAGS3="-fno-caret-diagnostics -fno-diagnostics-fixit-info -isysroot $MACSDK"
DEFINES="-D_THREAD_SAFE"
# MACFRAMEWORKS="-framework AppKit -framework Foundation -framework Security -framework Carbon -framework GSS"
FRAMEWORKS="-iframework $MACSDK/System/iOSSupport/System/Library/Frameworks"
XAMMACLIB="$XAMMACCATDIR/lib/libxammaccat.a"
US="-u _SystemNative_ConvertErrorPlatformToPal -u _SystemNative_ConvertErrorPalToPlatform -u _SystemNative_StrErrorR -u _SystemNative_GetNonCryptographicallySecureRandomBytes -u _SystemNative_Stat2 -u _SystemNative_LStat2 -u _xamarin_timezone_get_local_name -u _xamarin_timezone_get_data -u _xamarin_find_protocol_wrapper_type -u _xamarin_get_block_descriptor"
OUT="$OUTDIR/$APPNAME"
INCLUDES="-I$XAMMACCATDIR/include/mono-2.0 -I$XAMMACCATDIR/include"
LINKS="$XAMMACCATDIR/lib/libmonosgen-2.0.a $XAMMACCATDIR/lib/libmono-native-unified.a"
COMPILES="catmain.m"

# COMPILE AND LINK
echo Building $APPNAME

$CLANG $CFLAGS $FRAMEWORKS $US $XAMMACLIB -o $OUT $DEFINES $INCLUDES $LINKS $CFLAGS2 $COMPILES $CFLAGS3

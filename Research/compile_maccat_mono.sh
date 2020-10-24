#!/bin/bash

set -e
set -x

cd /Users/fak/Projects/mono

export XCODE_DIR=/Applications/Xcode_12.0.0.app/Contents/Developer
export MACOS_VERSION=10.15

# Configure

# echo "ENABLE_MACCAT=1" > sdks/Make.config
# rm sdks/builds/.stamp* && rm -r sdks/builds/maccat-mac64-release  && rm -r sdks/builds/maccat-mac64-release.config.cache
# make -C sdks/builds configure-maccat

# make V=1 -C sdks/builds build-maccat
# make -j 10 -C sdks/builds build-maccat

make -C sdks/builds archive-maccat


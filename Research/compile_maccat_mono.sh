#!/bin/bash

set -e
set -x

cd /Users/fak/Projects/mono

# From run-jenkins.sh

export XCODE_DIR=/Applications/Xcode_12.0.0.app/Contents/Developer
export MACOS_VERSION=10.15

pwd
# echo "ENABLE_MAC=1" > sdks/Make.config
# make -C sdks/builds configure-mac

# make -C sdks/builds build-mac

make -C sdks/builds archive-mac


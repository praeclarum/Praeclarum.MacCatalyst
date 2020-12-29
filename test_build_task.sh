#!/bin/bash

set -x
set -e


PROJDIR=/Users/fak/Dropbox/Projects/Microcasts/Microcasts.iOS

dotnet build Praeclarum.MacCatalyst/Praeclarum.MacCatalyst.BuildTask.csproj
cp Praeclarum.MacCatalyst/bin/Debug/netstandard2.1/Praeclarum.MacCatalyst.BuildTask.dll $PROJDIR
cp Praeclarum.MacCatalyst/bin/Debug/netstandard2.1/Praeclarum.MacCatalyst.targets $PROJDIR

cd $PROJDIR
touch AppDelegate.cs
msbuild




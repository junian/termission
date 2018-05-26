#!/bin/bash

. load-env.sh

CURRENT_DIR=$PWD

cd $CURRENT_DIR/src/

nuget restore

msbuild ${PROJECT_NAME}.sln \
    /t:"Desktop\\${PROJECT_NAME}_XamMac:rebuild" \
    /p:Configuration="Release"

cd $CURRENT_DIR/tools/create-dmg/

mkdir -p $CURRENT_DIR/setup/app

rm -rf $CURRENT_DIR/setup/*.dmg

cp -r "$CURRENT_DIR/src/${PROJECT_NAME}.XamMac/bin/Release/${APP_NAME}.app" "$CURRENT_DIR/setup/app/"

./create-dmg \
    --volname "$APP_NAME Installer" \
    --background $CURRENT_DIR/resources/dmg-background.png \
    --window-size 660 400 \
    --icon-size 160 \
    --app-drop-link 480 170 \
    --icon "$APP_NAME.app" 180 170 \
    "$CURRENT_DIR/setup/${APP_NAME}-v${APP_VERSION}.dmg" \
    $CURRENT_DIR/setup/app

rm -rf $CURRENT_DIR/setup/app/

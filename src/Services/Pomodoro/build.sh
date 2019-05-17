#!/bin/sh

PROJECT="Pomodoro.API"
CSPROJ="src/Services/Pomodoro/$PROJECT/Pomodoros.API.csproj"

echo "Restoring $PROJECT"
dotnet restore "$CSPROJ"
#TODO: Should I restore to .nuget folder to have cache?

echo "Building $PROJECT"
dotnet build --no-restore $CSPROJ

echo "Publishing $PROJECT"
dotnet publish $CSPROJ -c Release


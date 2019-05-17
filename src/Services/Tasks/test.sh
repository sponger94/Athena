#!/bin/sh

PROJECT="Tasks.API"
CSPROJ="src/Services/Tasks/tests/Tasks.UnitTests/Tasks.UnitTests.csproj"

echo "Restoring $PROJECT"
dotnet restore $CSPROJ

echo "Building $PROJECT"
dotnet build --no-restore $CSPROJ -c Release

echo "Testing $PROJECT"
dotnet test $CSPROJ
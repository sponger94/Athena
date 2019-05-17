#/bin/sh

PROJECT="Pomodoro.API"
CSPROJ="src/Services/Pomodoro/tests/Pomodoro.UnitTests/Pomodoro.UnitTests.csproj"

echo "Restoring $PROJECT"
dotnet restore $CSPROJ

echo "Building $PROJECT"
dotnet build --no-restore $CSPROJ -c Release

echo "Testing $PROJECT"
dotnet test $CSPROJ
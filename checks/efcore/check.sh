#!/usr/bin/env bash
# dotnet build, with every warning an error, of the entities and DbContext hekireki-efcore wrote,
# against the real EF Core.
source "$(dirname "$0")/../common.sh"
require dotnet

cp -RL "$generated/efcore" "$work/Models"
cd "$work"
export NUGET_PACKAGES="$cache/nuget" DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1 DOTNET_CLI_HOME="$cache/home"

step "dotnet build ($provider)"
# One provider per restore: see Check.csproj.
dotnet build --nologo -warnaserror -p:HekirekiProvider="$provider"

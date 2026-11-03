#!/usr/bin/env bash
# Kotlin compiled with every warning an error, against the real Exposed, for the tables and
# entities hekireki-exposed wrote. Gradle comes from PATH (CI installs it); without one, the
# distribution named below is fetched once into the cache and checked against its SHA-256.
source "$(dirname "$0")/../common.sh"
require java

GRADLE_VERSION=9.8.0
GRADLE_SHA256=bafd5ce9cfaea0fbccfdc8439a1ac42fbd4cd9c89dc9a988228d8a2639a58e6c

mkdir -p "$work/src/main/kotlin"
cp -RL "$generated/exposed" "$work/src/main/kotlin/models"
cd "$work"
export GRADLE_USER_HOME="$cache/home"

if command -v gradle >/dev/null; then
  gradle=gradle
else
  dist="$cache/dist/gradle-$GRADLE_VERSION"
  if [[ ! -x "$dist/bin/gradle" ]]; then
    step "fetching Gradle $GRADLE_VERSION"
    mkdir -p "$cache/dist"
    zip="$cache/dist/gradle-$GRADLE_VERSION-bin.zip"
    curl -fsSL -o "$zip" "https://services.gradle.org/distributions/gradle-$GRADLE_VERSION-bin.zip"
    echo "$GRADLE_SHA256  $zip" | sha256sum -c - >/dev/null
    (cd "$cache/dist" && jar xf "$zip")
    chmod +x "$dist/bin/gradle"
    rm -f "$zip"
  fi
  gradle="$dist/bin/gradle"
fi

step "gradle compileKotlin"
"$gradle" --console=plain --quiet --project-cache-dir "$work/.gradle" compileKotlin

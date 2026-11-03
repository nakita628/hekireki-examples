#!/usr/bin/env bash
# php -l on every model hekireki-eloquent wrote, then loading them all against the real Eloquent
# (illuminate/database) when composer is there, which it is on CI.
source "$(dirname "$0")/../common.sh"
require php

cp -RL "$generated/eloquent" "$work/models"
cd "$work"

step "php -l"
for file in models/*.php; do
  php -l "$file" >/dev/null
done

if command -v composer >/dev/null; then
  export COMPOSER_HOME="$cache/composer" COMPOSER_CACHE_DIR="$cache/composer-cache"
  step "composer install"
  composer install --quiet --no-interaction
  step "load every model against Eloquent"
  php smoke.php models
elif [[ -n "${CI:-}" ]]; then
  echo "composer is not installed" >&2
  exit 1
else
  echo "composer is not installed: skipping the load against Eloquent" >&2
fi

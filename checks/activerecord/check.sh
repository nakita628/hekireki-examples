#!/usr/bin/env bash
# ruby -c on every model hekireki-activerecord wrote, then loading them all against the real
# Active Record.
source "$(dirname "$0")/../common.sh"
require ruby
require bundle

cp -RL "$generated/activerecord" "$work/models"
cd "$work"
export BUNDLE_PATH="$cache/bundle" BUNDLE_GEMFILE="$work/Gemfile"

step "bundle install"
bundle install --quiet

step "ruby -c"
for file in models/*.rb; do
  ruby -c "$file" >/dev/null
done

step "load every model against Active Record"
bundle exec ruby smoke.rb models

#!/usr/bin/env bash
# Parses and compiles, with every warning an error, the schemas hekireki-ecto wrote against the
# real Ecto.
source "$(dirname "$0")/../common.sh"
require mix
require elixir

mkdir -p "$work/lib"
cp -RL "$generated/ecto"/. "$work/lib/"
cd "$work"
export MIX_HOME="$cache/mix" HEX_HOME="$cache/hex" MIX_BUILD_PATH="$cache/_build/$schema" MIX_DEPS_PATH="$cache/deps"

# telemetry is an Erlang dep; without rebar3 mix blocks on an install prompt.
mix local.hex --force --if-missing >/dev/null
mix local.rebar --force --if-missing >/dev/null

step "mix deps.get"
mix deps.get

step "parse"
elixir -e 'for f <- Path.wildcard("lib/**/*.ex"), do: Code.string_to_quoted!(File.read!(f), file: f)'

step "mix compile --warnings-as-errors"
mix compile --force --warnings-as-errors

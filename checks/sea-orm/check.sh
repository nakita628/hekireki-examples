#!/usr/bin/env bash
# rustfmt --check and cargo check on the entities hekireki-sea-orm wrote, against the real SeaORM.
source "$(dirname "$0")/../common.sh"
require cargo
require rustfmt

cp -RL "$generated/sea-orm" "$work/src/entities"
cd "$work"
export CARGO_HOME="$cache/cargo" CARGO_TARGET_DIR="$cache/target"

step "rustfmt --check"
rustfmt --edition 2021 --check src/entities/*.rs

step "cargo check"
cargo check --quiet

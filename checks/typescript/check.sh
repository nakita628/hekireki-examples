#!/usr/bin/env bash
# tsc --strict on the drizzle schema and the kysely DB interface hekireki wrote, against the real
# drizzle-orm and kysely of pnpm-lock.yaml (in runfiles, linked by rules_js).
source "$(dirname "$0")/../common.sh"
require node

cp -RL "$generated/drizzle" "$work/drizzle"
cp -RL "$generated/kysely" "$work/kysely"
ln -s "$PWD/node_modules" "$work/node_modules"
cd "$work"

step "tsc --noEmit"
node node_modules/typescript/bin/tsc -p tsconfig.json

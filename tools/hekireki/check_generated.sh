#!/usr/bin/env bash
# Compares a fresh hekireki generation (first argument, a directory in bazel-bin) with the
# committed copy (second argument, a directory in the source tree), both in runfiles.
set -euo pipefail

fresh=$1
committed=$2

if [[ ! -d "$committed" ]]; then
  echo "no committed copy at $committed: run 'bazel run' on the .write target" >&2
  exit 1
fi

if ! diff -r "$committed" "$fresh"; then
  echo >&2
  echo "the committed output under $committed is stale: run 'bazel run' on the .write target" >&2
  exit 1
fi

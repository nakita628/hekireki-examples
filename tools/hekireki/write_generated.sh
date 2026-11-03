#!/usr/bin/env bash
# Copies a fresh hekireki generation (first argument, a directory in runfiles) over the committed
# copy (second argument, relative to the workspace root). Run through `bazel run`, which sets
# BUILD_WORKSPACE_DIRECTORY.
#
# Bazel leaves its outputs read-only, and `cp -R` would carry that over (and fails outright on a
# file system that refuses to set a directory's mode, as a container's bind mount can); the tree
# is streamed through tar with the modes rewritten to the usual 755 and 644.
set -euo pipefail

fresh=$1
committed=${BUILD_WORKSPACE_DIRECTORY:?run this through bazel run}/$2

if [[ ! -d "$fresh" ]]; then
  echo "no generated output at $fresh" >&2
  exit 1
fi

chmod -R u+w "$committed" 2>/dev/null || true
rm -rf "$committed"
mkdir -p "$committed"
(cd "$fresh" && tar -cf - --mode='a-x,u+rwX,go+rX' .) | tar -C "$committed" -xf - --no-same-permissions
echo "wrote $2"

# Sourced by every checks/*/check.sh: the arguments, a work directory, and a cache directory
# the toolchain's downloads survive in between runs.
#
#   check.sh <harness package> <schema> <generated dir> <schema.prisma>
set -euo pipefail

harness=$1
schema=$2
generated=$3
schema_file=$4
# The datasource's provider: postgresql, mysql, sqlite, ...
provider=$(sed -n 's/^\s*provider\s*=\s*"\([a-z]*\)".*/\1/p' "$schema_file" | head -1)
lang=$(basename "$harness")

if [[ ! -d "$generated" ]]; then
  echo "no generated output at $generated" >&2
  exit 1
fi

# Runfiles are read-only; the harness is copied where the toolchain can write beside it.
work=$(mktemp -d "${TEST_TMPDIR:-/tmp}/check-$lang-$schema.XXXXXX")
cp -RL "$harness"/. "$work"/
rm -f "$work/check.sh" "$work/BUILD.bazel"

cache="${HEKIREKI_CHECK_CACHE:-${HOME:-/tmp}/.cache/hekireki-examples}/$lang"
mkdir -p "$cache"

# `require <tool>`: the toolchain has to be there; on CI (the workflow sets CI) a missing one
# fails the test rather than skipping it, which would be a false green.
require() {
  if ! command -v "$1" >/dev/null; then
    echo "$1 is not installed" >&2
    exit 1
  fi
}

step() {
  echo
  echo "== $*"
}

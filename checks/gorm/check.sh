#!/usr/bin/env bash
# gofmt and go vet on the structs hekireki-gorm wrote, against the real GORM.
source "$(dirname "$0")/../common.sh"
require go
require gofmt

cp -RL "$generated/gorm" "$work/model"
cd "$work"
export GOMODCACHE="$cache/mod" GOCACHE="$cache/build" GOFLAGS=-mod=mod

step "gofmt"
if [[ -n "$(gofmt -l -e model)" ]]; then
  gofmt -d -e model
  echo "the generated Go is not gofmt-ed" >&2
  exit 1
fi

step "go mod tidy"
go mod tidy

step "go vet ./model/"
go vet ./model/

step "go build ./model/"
go build ./model/

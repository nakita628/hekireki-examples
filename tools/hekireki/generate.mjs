#!/usr/bin/env node
// Runs `prisma generate` on one schema with hekireki's generators on PATH, then moves what the
// generators wrote into the output directories Bazel declared.
//
//   generate.mjs --schema <path> --out <dir>:<bazel-bin dir> [--out <dir>:<bazel-bin dir>]...
//
// Prisma resolves `provider = "hekireki-zod"` as a shell command, so a throwaway bin directory
// holding one wrapper per generator is prepended to PATH. Every `output` in the schema is
// relative to the schema file, so the schema is copied into a temporary directory first and
// each top-level directory it produced (`generated`, `images`, ...) is moved to its declared
// place. Both paths are relative to the working directory, which the js_binary launcher sets to
// BAZEL_BINDIR, where rules_js also copies the schema.
import { spawnSync } from 'node:child_process'
import { chmodSync, cpSync, existsSync, mkdirSync, mkdtempSync, readdirSync, rmSync, writeFileSync } from 'node:fs'
import { createRequire } from 'node:module'
import { tmpdir } from 'node:os'
import { dirname, join, resolve } from 'node:path'

const require = createRequire(import.meta.url)
const prismaCli = require.resolve('prisma/build/index.js')
const hekirekiDir = dirname(require.resolve('hekireki/package.json'))
const hekirekiBins = Object.entries(require('hekireki/package.json').bin)

const args = process.argv.slice(2)
let schema
const outs = []
for (let i = 0; i < args.length; i += 2) {
  if (args[i] === '--schema') schema = resolve(args[i + 1])
  else if (args[i] === '--out') {
    const [from, to] = args[i + 1].split(':')
    outs.push({ from, to: resolve(to) })
  } else throw new Error(`unknown argument ${args[i]}`)
}
if (!schema || outs.length === 0) throw new Error('usage: --schema <file> --out <dir>:<bazel-bin dir>')

const work = mkdtempSync(join(tmpdir(), 'hekireki-generate-'))
try {
  const bin = join(work, 'bin')
  mkdirSync(bin)
  mkdirSync(join(work, 'home'))
  for (const [name, file] of hekirekiBins) {
    const wrapper = join(bin, name)
    writeFileSync(wrapper, `#!/bin/sh\nexec "${process.execPath}" "${join(hekirekiDir, file)}" "$@"\n`)
    chmodSync(wrapper, 0o755)
  }
  const schemaCopy = join(work, 'schema.prisma')
  cpSync(schema, schemaCopy)

  const result = spawnSync(process.execPath, [prismaCli, 'generate', '--schema', schemaCopy], {
    cwd: work,
    stdio: 'inherit',
    env: {
      ...process.env,
      PATH: `${bin}:${process.env.PATH ?? ''}`,
      // Prisma touches the engine cache under $HOME/.cache/prisma when it finds one, and the
      // sandbox mounts the home directory read-only; `prisma generate` needs no engine binary.
      HOME: join(work, 'home'),
      CHECKPOINT_DISABLE: '1',
      PRISMA_HIDE_UPDATE_MESSAGE: '1',
      DATABASE_URL: process.env.DATABASE_URL ?? 'postgresql://localhost:5432/hekireki_examples',
    },
  })
  if (result.status !== 0) process.exit(result.status ?? 1)

  for (const { from, to } of outs) {
    const produced = join(work, from)
    if (!existsSync(produced)) throw new Error(`the schema wrote nothing under ${from}/`)
    rmSync(to, { recursive: true, force: true })
    mkdirSync(dirname(to), { recursive: true })
    cpSync(produced, to, { recursive: true })
  }
  const leftovers = readdirSync(work).filter((f) => !['bin', 'home', 'schema.prisma', ...outs.map((o) => o.from)].includes(f))
  if (leftovers.length > 0) throw new Error(`generators wrote outside the declared outputs: ${leftovers.join(', ')}`)
} finally {
  rmSync(work, { recursive: true, force: true })
}

#!/usr/bin/env node
// Imports the public Prisma schemas named in schemas/sources.json into schemas/<name>/schema.prisma:
//
//   node tools/import/import-schema.mjs <name>...   one or more schemas
//   node tools/import/import-schema.mjs --all       every schema in sources.json
//
// Each schema is fetched at the commit pinned in sources.json. `path` is either one schema.prisma
// or, with a trailing `/`, the directory of a multi-file schema (`prismaSchemaFolder`), whose
// `.prisma` files, subdirectories included, are joined in path order: Prisma reads such a
// directory as one schema, so the joined file is the same schema. The file is then rewritten:
//   - its own `generator` blocks are dropped (Prisma Client, zod-prisma-types, ...: they are not
//     installed here and are not what this repository checks), except that the preview features
//     the schema itself needs (`views`, ...) are carried over to the first hekireki block, since
//     Prisma validates the schema against the union of every generator's `previewFeatures`;
//   - `url`, `directUrl` and `shadowDatabaseUrl` are dropped from the datasource, so no `.env`
//     is needed to run `prisma generate`;
//   - a header names where the file came from, and hekireki's ORM, ER and docs generator blocks
//     are added after the datasource, except those `skip` in sources.json names (a generator that
//     refuses the schema, with the reason), which a comment stands in for.
// Everything else, the `///` comments included, is kept as the project wrote it: a schema as it
// is in the wild is what hekireki is being checked against.
import { mkdirSync, readFileSync, writeFileSync } from 'node:fs'
import { dirname, join, resolve } from 'node:path'

const root = resolve(import.meta.dirname, '../..')

// The preview features that change what Prisma accepts in the schema, as opposed to what Prisma
// Client does with it. Any other one of the original block's (`metrics`, `driverAdapters`, ...) is
// dropped with the block: Prisma refuses a name it no longer knows.
const SCHEMA_PREVIEW_FEATURES = ['views', 'postgresqlExtensions', 'fullTextSearchPostgres', 'partialIndexes', 'shardKeys']
const sources = JSON.parse(readFileSync(join(root, 'schemas/sources.json'), 'utf8'))
const args = process.argv.slice(2)
const names = args.includes('--all') ? Object.keys(sources) : args
if (names.length === 0) {
  console.error(`usage: import-schema.mjs <name>... | --all; names: ${Object.keys(sources).join(', ')}`)
  process.exit(1)
}
for (const name of names) {
  const source = sources[name]
  if (!source) {
    console.error(`unknown schema ${name}; one of: ${Object.keys(sources).join(', ')}`)
    process.exit(1)
  }
  await importSchema(name, source)
}

async function fetchText(url) {
  const response = await fetch(url, { headers: { 'user-agent': 'hekireki-examples' } })
  if (!response.ok) throw new Error(`${url}: ${response.status}`)
  return response.text()
}

/** The schema's text at the pinned commit, and the files it came from. */
async function fetchSchema(source) {
  const raw = `https://raw.githubusercontent.com/${source.repo}/${source.commit}/`
  if (!source.path.endsWith('/')) return { text: await fetchText(raw + source.path), files: [source.path] }
  const tree = JSON.parse(
    await fetchText(`https://api.github.com/repos/${source.repo}/git/trees/${source.commit}?recursive=1`),
  )
  const files = tree.tree
    .map((entry) => entry.path)
    .filter((path) => path.startsWith(source.path) && path.endsWith('.prisma'))
    .sort()
  if (files.length === 0) throw new Error(`no .prisma files under ${source.path} in ${source.repo}@${source.commit}`)
  const parts = []
  for (const file of files) parts.push(`// ---- ${file.slice(source.path.length)}\n\n${(await fetchText(raw + file)).trim()}\n`)
  return { text: parts.join('\n'), files }
}

async function importSchema(name, source) {
  const { text: original, files } = await fetchSchema(source)

  // Drop every top-level `generator ... { ... }` block and the connection lines of the datasource.
  const blocks = []
  const previewFeatures = new Set()
  // Read from the datasource block itself. A commented-out example above it (AnythingLLM ships a
  // PostgreSQL datasource in comments over the live SQLite one) must not decide the provider.
  let datasourceProvider
  const lines = original.split('\n')
  let i = 0
  while (i < lines.length) {
    const line = lines[i]
    if (/^generator\s+[\w-]+\s*\{/.test(line)) {
      while (i < lines.length && !/^\}/.test(lines[i])) {
        const features = lines[i].match(/^\s*previewFeatures\s*=\s*\[([^\]]*)\]/)?.[1]
        for (const feature of features?.match(/\w+/g) ?? []) {
          if (SCHEMA_PREVIEW_FEATURES.includes(feature)) previewFeatures.add(feature)
        }
        i++
      }
      i++
      while (i < lines.length && lines[i].trim() === '') i++
      continue
    }
    if (/^datasource\s+\w+\s*\{/.test(line)) {
      blocks.push(line)
      i++
      while (i < lines.length && !/^\}/.test(lines[i])) {
        if (!/^\s*(url|directUrl|shadowDatabaseUrl)\s*=/.test(lines[i])) blocks.push(lines[i])
        const providerLine = lines[i].match(/^\s*provider\s*=\s*"(\w+)"/)
        if (providerLine) datasourceProvider = providerLine[1]
        i++
      }
      blocks.push(lines[i])
      i++
      continue
    }
    blocks.push(line)
    i++
  }
  const body = blocks.join('\n').replace(/^\n+/, '')
  const provider = datasourceProvider
  if (!provider) throw new Error('no datasource provider found')
  // Prisma takes `postgres` for `postgresql`.
  const postgres = provider === 'postgresql' || provider === 'postgres'

  const origin =
    files.length === 1
      ? `https://github.com/${source.repo}/blob/${source.commit}/${source.path}`
      : `https://github.com/${source.repo}/tree/${source.commit}/${source.path}`
  const header = `// ${source.app}: ${source.description}
//
// Imported from ${origin}
// (${source.license} license) by tools/import/import-schema.mjs, which dropped the original
// generator blocks and the datasource's connection URLs and added the hekireki generators below.${
    files.length === 1
      ? ''
      : `
// The project keeps its schema in ${files.length} files (prismaSchemaFolder), joined here in name order.`
  }
// The models, enums and \`///\` comments are the project's own, as they are at that commit.
`
  const skip = source.skip ?? {}
  const generators = `
// hekireki: every ORM generator, the ER diagram as Mermaid, DBML and SVG, and the schema docs. Every output is
// under generated/, which the Bazel target //schemas/${name}:generate writes and
// //schemas/${name}:generate.test holds the committed copy to.

generator Hekireki-Drizzle {
  provider = "hekireki-drizzle"
  output   = "generated/drizzle/schema.ts"${
    previewFeatures.size === 0
      ? ''
      : `
  // What the schema needs from the original generator block's previewFeatures.
  previewFeatures = [${[...previewFeatures].map((f) => `"${f}"`).join(', ')}]`
  }
}

generator Hekireki-Kysely {
  provider = "hekireki-kysely"
  output   = "generated/kysely/types.ts"
}

generator Hekireki-SQLAlchemy {
  provider = "hekireki-sqlalchemy"
  output   = "generated/sqlalchemy"
}

generator Hekireki-Django {
  provider = "hekireki-django"
  output   = "generated/django"
}

generator Hekireki-GORM {
  provider = "hekireki-gorm"
  output   = "generated/gorm"
  package  = "model"
}

generator Hekireki-SeaORM {
  provider = "hekireki-sea-orm"
  output   = "generated/sea-orm"
}

generator Hekireki-Ecto {
  provider = "hekireki-ecto"
  output   = "generated/ecto"
  app      = "${source.app}"
}

generator Hekireki-ActiveRecord {
  provider = "hekireki-activerecord"
  output   = "generated/activerecord"
}

generator Hekireki-EFCore {
  provider  = "hekireki-efcore"
  output    = "generated/efcore"
  namespace = "${source.app}.Models"
  context   = "${source.app}Context"
}

generator Hekireki-Eloquent {
  provider  = "hekireki-eloquent"
  output    = "generated/eloquent"
  namespace = "App\\\\Models"
}

${
  postgres
    ? `generator Hekireki-Exposed {
  provider = "hekireki-exposed"
  output   = "generated/exposed"
  package  = "${name}.models"
}
`
    : `// hekireki-exposed is left out: it supports postgresql only, and this datasource is ${provider}.
`
}
generator Hekireki-ER {
  provider = "hekireki-er"
  outputs  = ["generated/er/er.md", "generated/er/er.dbml", "generated/er/er.svg"]
}

generator Hekireki-Docs {
  provider = "hekireki-docs"
  output   = "generated/docs"
}
`

  // Put the generators right after the datasource block, a comment in place of a skipped one.
  const kept = generators.replaceAll(/generator Hekireki-(\w+) \{\n[^]*?\n\}\n/gu, (block, generator) => {
    const reason = skip[generator.toLowerCase()]
    return reason === undefined ? block : `// hekireki-${generator.toLowerCase()} is left out: ${reason}.\n`
  })
  const out = body.replace(/(datasource\s+\w+\s*\{[^]*?\n\})\n/, `$1\n${kept}`)
  const target = join(root, 'schemas', name, 'schema.prisma')
  mkdirSync(dirname(target), { recursive: true })
  writeFileSync(target, header + '\n' + out.trimEnd() + '\n')
  console.log(`wrote ${target} (${files.length} file${files.length === 1 ? '' : 's'}, ${provider})`)
}

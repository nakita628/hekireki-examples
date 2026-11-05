import {
  customType,
  foreignKey,
  index,
  integer,
  sqliteTable,
  text,
  uniqueIndex,
} from 'drizzle-orm/sqlite-core'
import { defineRelations } from 'drizzle-orm'

const utcDateTime = customType<{ data: Date; driverData: string | number }>({
  dataType: () => 'datetime',
  toDriver: (value) => value.toISOString().replace('Z', '+00:00'),
  fromDriver: (value) => {
    if (typeof value === 'number' || /^-?\d+$/u.test(value)) return new Date(Number(value))
    const iso = value.replace(' ', 'T').replace(/ (?=[+-]\d\d:?\d\d$)/u, '')
    return new Date(/T[\d:.]+$/u.test(iso) ? `${iso}Z` : iso)
  },
})

const utcNow = (() => {
  let now: Date | undefined
  return () => {
    if (now === undefined) {
      now = new Date()
      queueMicrotask(() => {
        now = undefined
      })
    }
    return now
  }
})()

export const users = sqliteTable(
  'users',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    username: text('username').notNull(),
    password: text('password').notNull(),
    enabled: integer('enabled', { mode: 'boolean' }).notNull().default(true),
    storageQuota: text('storageQuota').notNull().default('0'),
    apiKey: text('apiKey'),
    passwordEditedAt: utcDateTime('passwordEditedAt'),
    apiKeyEditedAt: utcDateTime('apiKeyEditedAt'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
  },
  (table) => [
    uniqueIndex('users_uuid_key').on(table.uuid),
    uniqueIndex('users_username_key').on(table.username),
    uniqueIndex('users_apiKey_key').on(table.apiKey),
  ],
)

export const roles = sqliteTable(
  'roles',
  { id: integer('id').primaryKey({ autoIncrement: true }), name: text('name').notNull() },
  (table) => [uniqueIndex('roles_name_key').on(table.name)],
)

export const files = sqliteTable(
  'files',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId'),
    name: text('name').notNull(),
    original: text('original').notNull(),
    type: text('type').notNull(),
    size: text('size').notNull(),
    hash: text('hash').notNull(),
    ip: text('ip').notNull(),
    sourceUrl: text('sourceUrl'),
    isS3: integer('isS3', { mode: 'boolean' }).notNull().default(false),
    isWatched: integer('isWatched', { mode: 'boolean' }).notNull().default(false),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
    quarantine: integer('quarantine', { mode: 'boolean' }).notNull().default(false),
  },
  (table) => [
    uniqueIndex('files_uuid_key').on(table.uuid),
    foreignKey({ name: 'files_userId_fkey', columns: [table.userId], foreignColumns: [users.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const quarantineFiles = sqliteTable(
  'quarantineFiles',
  { fileId: text('fileId').notNull(), name: text('name').notNull(), reason: text('reason') },
  (table) => [
    uniqueIndex('quarantineFiles_fileId_key').on(table.fileId),
    foreignKey({
      name: 'quarantineFiles_fileId_fkey',
      columns: [table.fileId],
      foreignColumns: [files.uuid],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const snippets = sqliteTable(
  'snippets',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId'),
    identifier: text('identifier').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    content: text('content').notNull(),
    language: text('language').notNull(),
    parentUuid: text('parentUuid'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
  },
  (table) => [
    uniqueIndex('snippets_uuid_key').on(table.uuid),
    uniqueIndex('snippets_identifier_key').on(table.identifier),
  ],
)

export const shortenedLinks = sqliteTable(
  'shortenedLinks',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId'),
    identifier: text('identifier').notNull(),
    destination: text('destination').notNull(),
    views: integer('views').notNull().default(0),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('shortenedLinks_uuid_key').on(table.uuid),
    uniqueIndex('shortenedLinks_identifier_key').on(table.identifier),
    foreignKey({
      name: 'shortenedLinks_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const albums = sqliteTable(
  'albums',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    zippedAt: utcDateTime('zippedAt'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
    nsfw: integer('nsfw', { mode: 'boolean' }).notNull().default(false),
    sortOrder: text('sortOrder'),
  },
  (table) => [uniqueIndex('albums_uuid_key').on(table.uuid)],
)

export const bans = sqliteTable('bans', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  ip: text('ip').notNull(),
  reason: text('reason'),
  createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
})

export const links = sqliteTable(
  'links',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId').notNull(),
    albumId: integer('albumId').notNull(),
    identifier: text('identifier').notNull(),
    views: integer('views').notNull().default(0),
    enabled: integer('enabled', { mode: 'boolean' }).notNull().default(true),
    enableDownload: integer('enableDownload', { mode: 'boolean' }).notNull().default(false),
    expiresAt: utcDateTime('expiresAt'),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
  },
  (table) => [
    uniqueIndex('links_uuid_key').on(table.uuid),
    uniqueIndex('links_identifier_key').on(table.identifier),
  ],
)

export const settings = sqliteTable('settings', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  rateLimitWindow: integer('rateLimitWindow').notNull(),
  rateLimitMax: integer('rateLimitMax').notNull(),
  secret: text('secret').notNull(),
  serviceName: text('serviceName').notNull(),
  chunkSize: text('chunkSize').notNull(),
  chunkedUploadsTimeout: integer('chunkedUploadsTimeout').notNull(),
  maxSize: text('maxSize').notNull(),
  generateZips: integer('generateZips', { mode: 'boolean' }).notNull(),
  generateOriginalFileNameWithIdentifier: integer('generateOriginalFileNameWithIdentifier', {
    mode: 'boolean',
  })
    .notNull()
    .default(false),
  generatedFilenameLength: integer('generatedFilenameLength').notNull(),
  generatedAlbumLength: integer('generatedAlbumLength').notNull(),
  generatedLinksLength: integer('generatedLinksLength').notNull().default(8),
  saveDuplicatesToAlbum: integer('saveDuplicatesToAlbum', { mode: 'boolean' })
    .notNull()
    .default(false),
  blockedExtensions: text('blockedExtensions').notNull(),
  blockNoExtension: integer('blockNoExtension', { mode: 'boolean' }).notNull(),
  publicMode: integer('publicMode', { mode: 'boolean' }).notNull(),
  userAccounts: integer('userAccounts', { mode: 'boolean' }).notNull(),
  disableStatisticsCron: integer('disableStatisticsCron', { mode: 'boolean' }).notNull(),
  disableUpdateCheck: integer('disableUpdateCheck', { mode: 'boolean' }).notNull().default(false),
  backgroundImageURL: text('backgroundImageURL').notNull(),
  logoURL: text('logoURL').notNull(),
  metaDescription: text('metaDescription').notNull(),
  metaKeywords: text('metaKeywords').notNull(),
  metaTwitterHandle: text('metaTwitterHandle').notNull(),
  metaDomain: text('metaDomain').notNull().default(''),
  serveUploadsFrom: text('serveUploadsFrom').notNull().default(''),
  enableMixedCaseFilenames: integer('enableMixedCaseFilenames', { mode: 'boolean' })
    .notNull()
    .default(true),
  usersStorageQuota: text('usersStorageQuota').notNull().default('0'),
  useNetworkStorage: integer('useNetworkStorage', { mode: 'boolean' }).notNull().default(false),
  useMinimalHomepage: integer('useMinimalHomepage', { mode: 'boolean' }).notNull().default(false),
  useUrlShortener: integer('useUrlShortener', { mode: 'boolean' }).notNull().default(false),
  generateThumbnails: integer('generateThumbnails', { mode: 'boolean' }).notNull().default(true),
  privacyPolicyPageContent: text('privacyPolicyPageContent').notNull().default(''),
  termsOfServicePageContent: text('termsOfServicePageContent').notNull().default(''),
  rulesPageContent: text('rulesPageContent').notNull().default(''),
  S3Region: text('S3Region').notNull().default(''),
  S3Bucket: text('S3Bucket').notNull().default(''),
  S3AccessKey: text('S3AccessKey').notNull().default(''),
  S3SecretKey: text('S3SecretKey').notNull().default(''),
  S3Endpoint: text('S3Endpoint').notNull().default(''),
  S3PathStyle: integer('S3PathStyle', { mode: 'boolean' }).notNull().default(false),
  S3PublicUrl: text('S3PublicUrl').notNull().default(''),
  defaultSortOrder: text('defaultSortOrder').notNull().default('createdAt:desc'),
})

export const statistics = sqliteTable('statistics', {
  id: integer('id').primaryKey({ autoIncrement: true }),
  batchId: integer('batchId'),
  type: text('type'),
  data: text('data'),
  createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
})

export const tags = sqliteTable(
  'tags',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    uuid: text('uuid').notNull(),
    userId: integer('userId').notNull(),
    name: text('name').notNull(),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
  },
  (table) => [uniqueIndex('tags_uuid_key').on(table.uuid)],
)

export const invites = sqliteTable(
  'invites',
  {
    id: integer('id').primaryKey({ autoIncrement: true }),
    createdBy: text('createdBy').notNull(),
    usedBy: text('usedBy'),
    code: text('code').notNull(),
    used: integer('used', { mode: 'boolean' }).notNull().default(false),
    createdAt: utcDateTime('createdAt').notNull().$defaultFn(utcNow),
    editedAt: utcDateTime('editedAt'),
  },
  (table) => [uniqueIndex('invites_code_key').on(table.code)],
)

export const rolesTousers = sqliteTable(
  '_rolesTousers',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    uniqueIndex('_rolesTousers_AB_unique').on(table.A, table.B),
    index('_rolesTousers_B_index').on(table.B),
    foreignKey({ name: '_rolesTousers_A_fkey', columns: [table.A], foreignColumns: [roles.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_rolesTousers_B_fkey', columns: [table.B], foreignColumns: [users.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const albumsTofiles = sqliteTable(
  '_albumsTofiles',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    uniqueIndex('_albumsTofiles_AB_unique').on(table.A, table.B),
    index('_albumsTofiles_B_index').on(table.B),
    foreignKey({ name: '_albumsTofiles_A_fkey', columns: [table.A], foreignColumns: [albums.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_albumsTofiles_B_fkey', columns: [table.B], foreignColumns: [files.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const filesTotags = sqliteTable(
  '_filesTotags',
  { A: integer('A').notNull(), B: integer('B').notNull() },
  (table) => [
    uniqueIndex('_filesTotags_AB_unique').on(table.A, table.B),
    index('_filesTotags_B_index').on(table.B),
    foreignKey({ name: '_filesTotags_A_fkey', columns: [table.A], foreignColumns: [files.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: '_filesTotags_B_fkey', columns: [table.B], foreignColumns: [tags.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    users,
    roles,
    files,
    quarantineFiles,
    snippets,
    shortenedLinks,
    albums,
    bans,
    links,
    settings,
    statistics,
    tags,
    invites,
    rolesTousers,
    albumsTofiles,
    filesTotags,
  },
  (r) => ({
    users: {
      files: r.many.files(),
      roles: r.many.roles({
        from: r.users.id.through(r.rolesTousers.B),
        to: r.roles.id.through(r.rolesTousers.A),
      }),
      links: r.many.shortenedLinks(),
    },
    roles: {
      users: r.many.users({
        from: r.roles.id.through(r.rolesTousers.A),
        to: r.users.id.through(r.rolesTousers.B),
      }),
    },
    files: {
      user: r.one.users({ from: r.files.userId, to: r.users.id }),
      albums: r.many.albums({
        from: r.files.id.through(r.albumsTofiles.B),
        to: r.albums.id.through(r.albumsTofiles.A),
      }),
      tags: r.many.tags({
        from: r.files.id.through(r.filesTotags.A),
        to: r.tags.id.through(r.filesTotags.B),
      }),
      quarantineFile: r.one.quarantineFiles({ from: r.files.uuid, to: r.quarantineFiles.fileId }),
    },
    quarantineFiles: {
      file: r.one.files({ from: r.quarantineFiles.fileId, to: r.files.uuid, optional: false }),
    },
    shortenedLinks: { user: r.one.users({ from: r.shortenedLinks.userId, to: r.users.id }) },
    albums: {
      files: r.many.files({
        from: r.albums.id.through(r.albumsTofiles.A),
        to: r.files.id.through(r.albumsTofiles.B),
      }),
    },
    tags: {
      files: r.many.files({
        from: r.tags.id.through(r.filesTotags.B),
        to: r.files.id.through(r.filesTotags.A),
      }),
    },
    rolesTousers: {
      roles: r.one.roles({ from: r.rolesTousers.A, to: r.roles.id, optional: false }),
      users: r.one.users({ from: r.rolesTousers.B, to: r.users.id, optional: false }),
    },
    albumsTofiles: {
      albums: r.one.albums({ from: r.albumsTofiles.A, to: r.albums.id, optional: false }),
      files: r.one.files({ from: r.albumsTofiles.B, to: r.files.id, optional: false }),
    },
    filesTotags: {
      files: r.one.files({ from: r.filesTotags.A, to: r.files.id, optional: false }),
      tags: r.one.tags({ from: r.filesTotags.B, to: r.tags.id, optional: false }),
    },
  }),
)

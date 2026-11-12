import {
  boolean,
  customType,
  foreignKey,
  index,
  integer,
  json,
  numeric,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
  varchar,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'

const bytea = customType<{ data: Uint8Array }>({
  dataType: () => 'bytea',
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

export const accounts = pgTable('accounts', {
  id: serial('id').primaryKey(),
  name: varchar('name').notNull().default(''),
  nickname: varchar('nickname').notNull().default(''),
  password: varchar('password').notNull().default(''),
  image: varchar('image').notNull().default(''),
  apiToken: varchar('apiToken').notNull().default(''),
  description: text('description').notNull().default(''),
  note: integer('note').notNull().default(0),
  role: varchar('role').notNull().default(''),
  loginType: varchar('loginType').notNull().default(''),
  linkAccountId: integer('linkAccountId'),
  createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
    .notNull()
    .$onUpdate(utcNow),
})

export const attachments = pgTable(
  'attachments',
  {
    id: serial('id').primaryKey(),
    isShare: boolean('isShare').notNull().default(false),
    sharePassword: varchar('sharePassword').notNull().default(''),
    name: varchar('name').notNull().default(''),
    path: varchar('path').notNull().default(''),
    size: numeric('size').notNull().default('0'),
    type: varchar('type').notNull().default(''),
    noteId: integer('noteId'),
    accountId: integer('accountId'),
    sortOrder: integer('sortOrder').notNull().default(0),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
    perfixPath: varchar('perfixPath').default(''),
    depth: integer('depth'),
    metadata: json('metadata'),
  },
  (table) => [
    foreignKey({
      name: 'attachments_noteId_fkey',
      columns: [table.noteId],
      foreignColumns: [notes.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'attachments_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const config = pgTable(
  'config',
  {
    id: serial('id').primaryKey(),
    key: varchar('key').notNull().default(''),
    config: json('config'),
    userId: integer('userId'),
  },
  (table) => [
    foreignKey({
      name: 'config_userId_fkey',
      columns: [table.userId],
      foreignColumns: [accounts.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const notes = pgTable(
  'notes',
  {
    id: serial('id').primaryKey(),
    type: integer('type').notNull().default(0),
    content: varchar('content').notNull().default(''),
    isArchived: boolean('isArchived').notNull().default(false),
    isRecycle: boolean('isRecycle').notNull().default(false),
    isShare: boolean('isShare').notNull().default(false),
    isTop: boolean('isTop').notNull().default(false),
    isReviewed: boolean('isReviewed').notNull().default(false),
    sharePassword: varchar('sharePassword').notNull().default(''),
    shareEncryptedUrl: varchar('shareEncryptedUrl'),
    shareExpiryDate: timestamp('shareExpiryDate', { withTimezone: true, precision: 6 }),
    shareMaxView: integer('shareMaxView').default(0),
    shareViewCount: integer('shareViewCount').default(0),
    metadata: json('metadata'),
    accountId: integer('accountId'),
    sortOrder: integer('sortOrder').notNull().default(0),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'notes_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const comments = pgTable(
  'comments',
  {
    id: serial('id').primaryKey(),
    content: text('content').notNull(),
    accountId: integer('accountId'),
    guestName: varchar('guestName'),
    guestIP: varchar('guestIP'),
    guestUA: varchar('guestUA'),
    noteId: integer('noteId').notNull(),
    parentId: integer('parentId'),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'comments_noteId_fkey',
      columns: [table.noteId],
      foreignColumns: [notes.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'comments_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'comments_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const tag = pgTable(
  'tag',
  {
    id: serial('id').primaryKey(),
    name: varchar('name').notNull().default(''),
    icon: varchar('icon').notNull().default(''),
    parent: integer('parent').notNull().default(0),
    accountId: integer('accountId'),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
    sortOrder: integer('sortOrder').notNull().default(0),
  },
  (table) => [
    foreignKey({
      name: 'tag_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const tagsToNote = pgTable(
  'tagsToNote',
  {
    id: serial('id'),
    noteId: integer('noteId').notNull().default(0),
    tagId: integer('tagId').notNull().default(0),
  },
  (table) => [
    primaryKey({ name: 'tagsToNote_pkey', columns: [table.noteId, table.tagId] }),
    foreignKey({
      name: 'tagsToNote_noteId_fkey',
      columns: [table.noteId],
      foreignColumns: [notes.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({ name: 'tagsToNote_tagId_fkey', columns: [table.tagId], foreignColumns: [tag.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const noteReference = pgTable(
  'noteReference',
  {
    id: serial('id').primaryKey(),
    fromNoteId: integer('fromNoteId').notNull(),
    toNoteId: integer('toNoteId').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('noteReference_fromNoteId_toNoteId_key').on(table.fromNoteId, table.toNoteId),
    foreignKey({
      name: 'noteReference_fromNoteId_fkey',
      columns: [table.fromNoteId],
      foreignColumns: [notes.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'noteReference_toNoteId_fkey',
      columns: [table.toNoteId],
      foreignColumns: [notes.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const follows = pgTable(
  'follows',
  {
    id: serial('id').primaryKey(),
    siteName: varchar('siteName'),
    siteUrl: varchar('siteUrl').notNull(),
    siteAvatar: varchar('siteAvatar'),
    description: text('description'),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
    followType: varchar('followType').notNull().default('following'),
    accountId: integer('accountId').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'follows_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const notifications = pgTable(
  'notifications',
  {
    id: serial('id').primaryKey(),
    type: varchar('type').notNull(),
    title: varchar('title').notNull(),
    content: text('content').notNull(),
    metadata: json('metadata'),
    isRead: boolean('isRead').notNull().default(false),
    accountId: integer('accountId').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'notifications_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const cache = pgTable(
  'cache',
  {
    id: serial('id').primaryKey(),
    key: varchar('key').notNull(),
    value: json('value').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('cache_key_key').on(table.key)],
)

export const plugin = pgTable('plugin', {
  id: serial('id').primaryKey(),
  metadata: json('metadata').notNull(),
  path: varchar('path').notNull(),
  isUse: boolean('isUse').notNull().default(true),
  isDev: boolean('isDev').notNull().default(false),
  createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
    .notNull()
    .$onUpdate(utcNow),
})

export const conversation = pgTable(
  'conversation',
  {
    id: serial('id').primaryKey(),
    title: varchar('title', { length: 255 }).notNull().default(''),
    isShare: boolean('isShare').notNull().default(false),
    accountId: integer('accountId').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('conversation_accountId_idx').on(table.accountId),
    foreignKey({
      name: 'conversation_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const message = pgTable(
  'message',
  {
    id: serial('id').primaryKey(),
    content: text('content').notNull(),
    role: varchar('role', { length: 50 }).notNull(),
    conversationId: integer('conversationId').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
    metadata: json('metadata'),
  },
  (table) => [
    index('message_conversationId_idx').on(table.conversationId),
    foreignKey({
      name: 'message_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const noteHistory = pgTable(
  'noteHistory',
  {
    id: serial('id').primaryKey(),
    noteId: integer('noteId').notNull(),
    content: text('content').notNull(),
    metadata: json('metadata'),
    version: integer('version').notNull(),
    accountId: integer('accountId'),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('noteHistory_noteId_idx').on(table.noteId),
    index('noteHistory_accountId_idx').on(table.accountId),
    foreignKey({
      name: 'noteHistory_noteId_fkey',
      columns: [table.noteId],
      foreignColumns: [notes.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const noteInternalShare = pgTable(
  'noteInternalShare',
  {
    id: serial('id').primaryKey(),
    noteId: integer('noteId').notNull(),
    accountId: integer('accountId').notNull(),
    canEdit: boolean('canEdit').notNull().default(true),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('noteInternalShare_noteId_idx').on(table.noteId),
    index('noteInternalShare_accountId_idx').on(table.accountId),
    uniqueIndex('noteInternalShare_noteId_accountId_key').on(table.noteId, table.accountId),
    foreignKey({
      name: 'noteInternalShare_noteId_fkey',
      columns: [table.noteId],
      foreignColumns: [notes.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'noteInternalShare_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const session = pgTable(
  'session',
  {
    id: text('id').primaryKey(),
    sid: text('sid').notNull(),
    data: text('data').notNull(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('session_sid_key').on(table.sid),
    index('session_expiresAt_idx').on(table.expiresAt),
  ],
)

export const aiProviders = pgTable('aiProviders', {
  id: serial('id').primaryKey(),
  title: varchar('title', { length: 255 }).notNull(),
  provider: varchar('provider', { length: 50 }).notNull(),
  baseURL: varchar('baseURL', { length: 500 }),
  apiKey: varchar('apiKey', { length: 500 }),
  config: json('config'),
  sortOrder: integer('sortOrder').notNull().default(0),
  createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
    .notNull()
    .$onUpdate(utcNow),
})

export const aiModels = pgTable(
  'aiModels',
  {
    id: serial('id').primaryKey(),
    providerId: integer('providerId').notNull(),
    title: varchar('title', { length: 255 }).notNull(),
    modelKey: varchar('modelKey', { length: 255 }).notNull(),
    capabilities: json('capabilities').notNull(),
    config: json('config'),
    sortOrder: integer('sortOrder').notNull().default(0),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('aiModels_providerId_idx').on(table.providerId),
    foreignKey({
      name: 'aiModels_providerId_fkey',
      columns: [table.providerId],
      foreignColumns: [aiProviders.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const aiScheduledTask = pgTable(
  'aiScheduledTask',
  {
    id: serial('id').primaryKey(),
    name: varchar('name', { length: 255 }).notNull(),
    prompt: text('prompt').notNull(),
    schedule: varchar('schedule', { length: 100 }).notNull(),
    isEnabled: boolean('isEnabled').notNull().default(true),
    lastRun: timestamp('lastRun', { withTimezone: true, precision: 6 }),
    lastResult: json('lastResult'),
    accountId: integer('accountId').notNull(),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [
    index('aiScheduledTask_accountId_idx').on(table.accountId),
    foreignKey({
      name: 'aiScheduledTask_accountId_fkey',
      columns: [table.accountId],
      foreignColumns: [accounts.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const mcpServers = pgTable('mcpServers', {
  id: serial('id').primaryKey(),
  name: varchar('name', { length: 255 }).notNull(),
  description: text('description'),
  type: varchar('type', { length: 50 }).notNull(),
  command: text('command'),
  args: json('args'),
  url: varchar('url', { length: 500 }),
  env: json('env'),
  headers: json('headers'),
  isEnabled: boolean('isEnabled').notNull().default(true),
  createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
    .notNull()
    .$onUpdate(utcNow),
})

export const fonts = pgTable(
  'fonts',
  {
    id: serial('id').primaryKey(),
    name: varchar('name', { length: 255 }).notNull(),
    displayName: varchar('displayName', { length: 255 }).notNull(),
    url: text('url'),
    fileData: bytea('fileData'),
    isLocal: boolean('isLocal').notNull().default(false),
    isSystem: boolean('isSystem').notNull().default(false),
    weights: json('weights').notNull(),
    category: varchar('category', { length: 50 }).notNull().default('sans-serif'),
    sortOrder: integer('sortOrder').notNull().default(0),
    createdAt: timestamp('createdAt', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { withTimezone: true, precision: 6 })
      .notNull()
      .$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('fonts_name_key').on(table.name)],
)

export const relations = defineRelations(
  {
    accounts,
    attachments,
    config,
    notes,
    comments,
    tag,
    tagsToNote,
    noteReference,
    follows,
    notifications,
    cache,
    plugin,
    conversation,
    message,
    noteHistory,
    noteInternalShare,
    session,
    aiProviders,
    aiModels,
    aiScheduledTask,
    mcpServers,
    fonts,
  },
  (r) => ({
    accounts: {
      notes: r.many.notes(),
      configs: r.many.config(),
      tags: r.many.tag(),
      comments: r.many.comments(),
      attachments: r.many.attachments(),
      follows: r.many.follows(),
      notifications: r.many.notifications(),
      conversations: r.many.conversation(),
      sharedNotes: r.many.noteInternalShare(),
      aiScheduledTasks: r.many.aiScheduledTask(),
    },
    attachments: {
      note: r.one.notes({ from: r.attachments.noteId, to: r.notes.id }),
      account: r.one.accounts({ from: r.attachments.accountId, to: r.accounts.id }),
    },
    config: { user: r.one.accounts({ from: r.config.userId, to: r.accounts.id }) },
    notes: {
      attachments: r.many.attachments(),
      tags: r.many.tagsToNote(),
      account: r.one.accounts({ from: r.notes.accountId, to: r.accounts.id }),
      referencedBy: r.many.noteReference({ alias: 'ReferencedNote' }),
      references: r.many.noteReference({ alias: 'ReferencingNote' }),
      comments: r.many.comments(),
      histories: r.many.noteHistory(),
      internalShares: r.many.noteInternalShare(),
    },
    comments: {
      note: r.one.notes({ from: r.comments.noteId, to: r.notes.id, optional: false }),
      account: r.one.accounts({ from: r.comments.accountId, to: r.accounts.id }),
      parent: r.one.comments({
        from: r.comments.parentId,
        to: r.comments.id,
        alias: 'CommentReplies',
      }),
      replies: r.many.comments({ alias: 'CommentReplies' }),
    },
    tag: {
      tagsToNote: r.many.tagsToNote(),
      account: r.one.accounts({ from: r.tag.accountId, to: r.accounts.id }),
    },
    tagsToNote: {
      note: r.one.notes({ from: r.tagsToNote.noteId, to: r.notes.id, optional: false }),
      tag: r.one.tag({ from: r.tagsToNote.tagId, to: r.tag.id, optional: false }),
    },
    noteReference: {
      fromNote: r.one.notes({
        from: r.noteReference.fromNoteId,
        to: r.notes.id,
        optional: false,
        alias: 'ReferencingNote',
      }),
      toNote: r.one.notes({
        from: r.noteReference.toNoteId,
        to: r.notes.id,
        optional: false,
        alias: 'ReferencedNote',
      }),
    },
    follows: {
      account: r.one.accounts({ from: r.follows.accountId, to: r.accounts.id, optional: false }),
    },
    notifications: {
      account: r.one.accounts({
        from: r.notifications.accountId,
        to: r.accounts.id,
        optional: false,
      }),
    },
    conversation: {
      messages: r.many.message(),
      account: r.one.accounts({
        from: r.conversation.accountId,
        to: r.accounts.id,
        optional: false,
      }),
    },
    message: {
      conversation: r.one.conversation({
        from: r.message.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
    },
    noteHistory: {
      note: r.one.notes({ from: r.noteHistory.noteId, to: r.notes.id, optional: false }),
    },
    noteInternalShare: {
      note: r.one.notes({ from: r.noteInternalShare.noteId, to: r.notes.id, optional: false }),
      account: r.one.accounts({
        from: r.noteInternalShare.accountId,
        to: r.accounts.id,
        optional: false,
      }),
    },
    aiProviders: { models: r.many.aiModels() },
    aiModels: {
      provider: r.one.aiProviders({
        from: r.aiModels.providerId,
        to: r.aiProviders.id,
        optional: false,
      }),
    },
    aiScheduledTask: {
      account: r.one.accounts({
        from: r.aiScheduledTask.accountId,
        to: r.accounts.id,
        optional: false,
      }),
    },
  }),
)

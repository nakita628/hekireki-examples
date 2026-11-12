import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export interface accounts {
  id: Generated<number>
  name: Generated<string>
  nickname: Generated<string>
  password: Generated<string>
  image: Generated<string>
  apiToken: Generated<string>
  description: Generated<string>
  note: Generated<number>
  role: Generated<string>
  loginType: Generated<string>
  linkAccountId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface attachments {
  id: Generated<number>
  isShare: Generated<boolean>
  sharePassword: Generated<string>
  name: Generated<string>
  path: Generated<string>
  size: Generated<string>
  type: Generated<string>
  noteId: number | null
  accountId: number | null
  sortOrder: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  perfixPath: Generated<string | null>
  depth: number | null
  metadata: unknown
}

export interface config {
  id: Generated<number>
  key: Generated<string>
  config: unknown
  userId: number | null
}

export interface notes {
  id: Generated<number>
  type: Generated<number>
  content: Generated<string>
  isArchived: Generated<boolean>
  isRecycle: Generated<boolean>
  isShare: Generated<boolean>
  isTop: Generated<boolean>
  isReviewed: Generated<boolean>
  sharePassword: Generated<string>
  shareEncryptedUrl: string | null
  shareExpiryDate: Timestamp | null
  shareMaxView: Generated<number | null>
  shareViewCount: Generated<number | null>
  metadata: unknown
  accountId: number | null
  sortOrder: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface comments {
  id: Generated<number>
  content: string
  accountId: number | null
  guestName: string | null
  guestIP: string | null
  guestUA: string | null
  noteId: number
  parentId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface tag {
  id: Generated<number>
  name: Generated<string>
  icon: Generated<string>
  parent: Generated<number>
  accountId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  sortOrder: Generated<number>
}

export interface tagsToNote {
  id: Generated<number>
  noteId: Generated<number>
  tagId: Generated<number>
}

export interface noteReference {
  id: Generated<number>
  fromNoteId: number
  toNoteId: number
  createdAt: Generated<Timestamp>
}

export interface follows {
  id: Generated<number>
  siteName: string | null
  siteUrl: string
  siteAvatar: string | null
  description: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  followType: Generated<string>
  accountId: number
}

export interface notifications {
  id: Generated<number>
  type: string
  title: string
  content: string
  metadata: unknown
  isRead: Generated<boolean>
  accountId: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface cache {
  id: Generated<number>
  key: string
  value: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface plugin {
  id: Generated<number>
  metadata: unknown
  path: string
  isUse: Generated<boolean>
  isDev: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface conversation {
  id: Generated<number>
  title: Generated<string>
  isShare: Generated<boolean>
  accountId: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface message {
  id: Generated<number>
  content: string
  role: string
  conversationId: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  metadata: unknown
}

export interface noteHistory {
  id: Generated<number>
  noteId: number
  content: string
  metadata: unknown
  version: number
  accountId: number | null
  createdAt: Generated<Timestamp>
}

export interface noteInternalShare {
  id: Generated<number>
  noteId: number
  accountId: number
  canEdit: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface session {
  id: string
  sid: string
  data: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface aiProviders {
  id: Generated<number>
  title: string
  provider: string
  baseURL: string | null
  apiKey: string | null
  config: unknown
  sortOrder: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface aiModels {
  id: Generated<number>
  providerId: number
  title: string
  modelKey: string
  capabilities: unknown
  config: unknown
  sortOrder: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface aiScheduledTask {
  id: Generated<number>
  name: string
  prompt: string
  schedule: string
  isEnabled: Generated<boolean>
  lastRun: Timestamp | null
  lastResult: unknown
  accountId: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface mcpServers {
  id: Generated<number>
  name: string
  description: string | null
  type: string
  command: string | null
  args: unknown
  url: string | null
  env: unknown
  headers: unknown
  isEnabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface fonts {
  id: Generated<number>
  name: string
  displayName: string
  url: string | null
  fileData: Buffer | null
  isLocal: Generated<boolean>
  isSystem: Generated<boolean>
  weights: unknown
  category: Generated<string>
  sortOrder: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DB {
  accounts: accounts
  attachments: attachments
  config: config
  notes: notes
  comments: comments
  tag: tag
  tagsToNote: tagsToNote
  noteReference: noteReference
  follows: follows
  notifications: notifications
  cache: cache
  plugin: plugin
  conversation: conversation
  message: message
  noteHistory: noteHistory
  noteInternalShare: noteInternalShare
  session: session
  aiProviders: aiProviders
  aiModels: aiModels
  aiScheduledTask: aiScheduledTask
  mcpServers: mcpServers
  fonts: fonts
}

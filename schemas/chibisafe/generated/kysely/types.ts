import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export interface users {
  id: Generated<number>
  uuid: string
  username: string
  password: string
  enabled: Generated<number>
  storageQuota: Generated<string>
  apiKey: string | null
  passwordEditedAt: string | null
  apiKeyEditedAt: string | null
  createdAt: string
  editedAt: string | null
}

export interface roles {
  id: Generated<number>
  name: string
}

export interface files {
  id: Generated<number>
  uuid: string
  userId: number | null
  name: string
  original: string
  type: string
  size: string
  hash: string
  ip: string
  sourceUrl: string | null
  isS3: Generated<number>
  isWatched: Generated<number>
  createdAt: string
  editedAt: string | null
  quarantine: Generated<number>
}

export interface quarantineFiles {
  fileId: string
  name: string
  reason: string | null
}

export interface snippets {
  id: Generated<number>
  uuid: string
  userId: number | null
  identifier: string
  name: string
  description: string | null
  content: string
  language: string
  parentUuid: string | null
  createdAt: string
  editedAt: string | null
}

export interface shortenedLinks {
  id: Generated<number>
  uuid: string
  userId: number | null
  identifier: string
  destination: string
  views: Generated<number>
  createdAt: string
}

export interface albums {
  id: Generated<number>
  uuid: string
  userId: number
  name: string
  description: string | null
  zippedAt: string | null
  createdAt: string
  editedAt: string | null
  nsfw: Generated<number>
  sortOrder: string | null
}

export interface bans {
  id: Generated<number>
  ip: string
  reason: string | null
  createdAt: string
}

export interface links {
  id: Generated<number>
  uuid: string
  userId: number
  albumId: number
  identifier: string
  views: Generated<number>
  enabled: Generated<number>
  enableDownload: Generated<number>
  expiresAt: string | null
  createdAt: string
  editedAt: string | null
}

export interface settings {
  id: Generated<number>
  rateLimitWindow: number
  rateLimitMax: number
  secret: string
  serviceName: string
  chunkSize: string
  chunkedUploadsTimeout: number
  maxSize: string
  generateZips: number
  generateOriginalFileNameWithIdentifier: Generated<number>
  generatedFilenameLength: number
  generatedAlbumLength: number
  generatedLinksLength: Generated<number>
  saveDuplicatesToAlbum: Generated<number>
  blockedExtensions: string
  blockNoExtension: number
  publicMode: number
  userAccounts: number
  disableStatisticsCron: number
  disableUpdateCheck: Generated<number>
  backgroundImageURL: string
  logoURL: string
  metaDescription: string
  metaKeywords: string
  metaTwitterHandle: string
  metaDomain: Generated<string>
  serveUploadsFrom: Generated<string>
  enableMixedCaseFilenames: Generated<number>
  usersStorageQuota: Generated<string>
  useNetworkStorage: Generated<number>
  useMinimalHomepage: Generated<number>
  useUrlShortener: Generated<number>
  generateThumbnails: Generated<number>
  privacyPolicyPageContent: Generated<string>
  termsOfServicePageContent: Generated<string>
  rulesPageContent: Generated<string>
  S3Region: Generated<string>
  S3Bucket: Generated<string>
  S3AccessKey: Generated<string>
  S3SecretKey: Generated<string>
  S3Endpoint: Generated<string>
  S3PathStyle: Generated<number>
  S3PublicUrl: Generated<string>
  defaultSortOrder: Generated<string>
}

export interface statistics {
  id: Generated<number>
  batchId: number | null
  type: string | null
  data: string | null
  createdAt: string
}

export interface tags {
  id: Generated<number>
  uuid: string
  userId: number
  name: string
  createdAt: string
  editedAt: string | null
}

export interface invites {
  id: Generated<number>
  createdBy: string
  usedBy: string | null
  code: string
  used: Generated<number>
  createdAt: string
  editedAt: string | null
}

export interface RolesTousers {
  A: number
  B: number
}

export interface AlbumsTofiles {
  A: number
  B: number
}

export interface FilesTotags {
  A: number
  B: number
}

export interface DB {
  users: users
  roles: roles
  files: files
  quarantineFiles: quarantineFiles
  snippets: snippets
  shortenedLinks: shortenedLinks
  albums: albums
  bans: bans
  links: links
  settings: settings
  statistics: statistics
  tags: tags
  invites: invites
  _rolesTousers: RolesTousers
  _albumsTofiles: AlbumsTofiles
  _filesTotags: FilesTotags
}

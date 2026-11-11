import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type Theme = 'dark' | 'light' | 'auto'

export type AiTaggingMethod = 'DISABLED' | 'GENERATE' | 'EXISTING' | 'PREDEFINED'

export type LinksRouteTo = 'ORIGINAL' | 'PDF' | 'READABLE' | 'MONOLITH' | 'SCREENSHOT' | 'DETAILS'

export type SubscriptionProvider = 'STRIPE' | 'APPLE' | 'GOOGLE'

export type DashboardSectionType = 'STATS' | 'RECENT_LINKS' | 'PINNED_LINKS' | 'COLLECTION'

export type AppMigrationStatus = 'APPLIED' | 'PENDING' | 'FAILED'

export interface Account {
  id: string
  userId: number
  type: string
  provider: string
  providerAccountId: string
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
}

export interface User {
  id: Generated<number>
  uuid: string
  name: string | null
  username: string | null
  email: string | null
  emailVerified: Timestamp | null
  unverifiedNewEmail: string | null
  image: string | null
  password: string | null
  locale: Generated<string>
  parentSubscriptionId: number | null
  collectionOrder: Generated<number[]>
  linksRouteTo: Generated<LinksRouteTo>
  aiTaggingMethod: Generated<AiTaggingMethod>
  aiPredefinedTags: Generated<string[]>
  aiTagExistingLinks: Generated<boolean>
  theme: Generated<Theme>
  dismissedAnnouncementId: string | null
  readableFontFamily: Generated<string | null>
  readableFontSize: Generated<string | null>
  readableLineHeight: Generated<string | null>
  readableLineWidth: Generated<string | null>
  preventDuplicateLinks: Generated<boolean>
  archiveAsScreenshot: Generated<boolean>
  archiveAsMonolith: Generated<boolean>
  archiveAsPDF: Generated<boolean>
  archiveAsReadable: Generated<boolean>
  archiveAsWaybackMachine: Generated<boolean>
  referredBy: string | null
  lastPickedAt: Timestamp | null
  acceptPromotionalEmails: Generated<boolean>
  shareUsageAnalytics: Generated<boolean>
  trialEndEmailSent: Generated<boolean>
  sessionVersion: Generated<number>
  passwordRetries: Generated<number>
  passwordRetryAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface EmailVerificationCode {
  identifier: string
  code: string
  attempts: Generated<number>
  expires: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface PasswordResetToken {
  identifier: string
  token: string
  expires: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Collection {
  id: Generated<number>
  name: string
  description: Generated<string>
  icon: string | null
  iconWeight: string | null
  color: Generated<string>
  parentId: number | null
  isPublic: Generated<boolean>
  ownerId: number
  createdById: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface UsersAndCollections {
  userId: number
  collectionId: number
  canCreate: boolean
  canUpdate: boolean
  canDelete: boolean
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Link {
  id: Generated<number>
  name: Generated<string>
  type: Generated<string>
  description: Generated<string>
  createdById: number | null
  collectionId: number
  collectionOrder: number
  icon: string | null
  iconWeight: string | null
  color: string | null
  url: string | null
  textContent: string | null
  preview: string | null
  image: string | null
  pdf: string | null
  readable: string | null
  monolith: string | null
  clientSide: Generated<boolean>
  aiTagged: Generated<boolean>
  metaDescription: string | null
  indexVersion: number | null
  lastPreserved: Timestamp | null
  importDate: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Tag {
  id: Generated<number>
  name: string
  ownerId: number
  archiveAsScreenshot: boolean | null
  archiveAsMonolith: boolean | null
  archiveAsPDF: boolean | null
  archiveAsReadable: boolean | null
  archiveAsWaybackMachine: boolean | null
  aiTag: boolean | null
  aiGenerated: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Subscription {
  id: Generated<number>
  active: boolean
  provider: SubscriptionProvider
  currentPeriodStart: Timestamp
  currentPeriodEnd: Timestamp
  quantity: Generated<number>
  storeOriginalTransactionId: string | null
  storeProductId: string | null
  googlePurchaseToken: string | null
  storeMetadata: unknown
  stripeSubscriptionId: string | null
  userId: number
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface AccessToken {
  id: Generated<number>
  name: string
  userId: number
  token: string
  revoked: Generated<boolean>
  isSession: Generated<boolean>
  expires: Timestamp
  lastUsedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface RssSubscription {
  id: Generated<number>
  url: string
  name: string
  lastBuildDate: Timestamp | null
  collectionId: number
  ownerId: number
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Highlight {
  id: Generated<number>
  color: string
  comment: string | null
  linkId: number
  userId: number
  startOffset: number
  endOffset: number
  text: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface DashboardSection {
  id: Generated<number>
  userId: number
  collectionId: number | null
  type: DashboardSectionType
  order: number
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface AppMigration {
  id: Generated<number>
  name: string
  status: AppMigrationStatus
  finishedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface PinnedLinks {
  A: number
  B: number
}

export interface LinkToTag {
  A: number
  B: number
}

export interface DB {
  Account: Account
  User: User
  VerificationToken: VerificationToken
  EmailVerificationCode: EmailVerificationCode
  PasswordResetToken: PasswordResetToken
  Collection: Collection
  UsersAndCollections: UsersAndCollections
  Link: Link
  Tag: Tag
  Subscription: Subscription
  AccessToken: AccessToken
  RssSubscription: RssSubscription
  Highlight: Highlight
  DashboardSection: DashboardSection
  AppMigration: AppMigration
  _PinnedLinks: PinnedLinks
  _LinkToTag: LinkToTag
}

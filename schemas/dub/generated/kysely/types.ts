import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type FolderType = 'default' | 'mega'

export type FolderAccessLevel = 'read' | 'write'

export type FolderUserRole = 'owner' | 'editor' | 'viewer'

export type InvoiceStatus = 'processing' | 'completed' | 'failed'

export type InvoiceType = 'domainRenewal'

export type PaymentMethod = 'card' | 'ach' | 'ach_fast' | 'sepa' | 'acss'

export type WebhookReceiver = 'user' | 'zapier' | 'make' | 'slack' | 'segment'

export type LinkScope = 'workspace' | 'folders' | 'links'

export type PlanPeriod = 'monthly' | 'yearly' | 'quarterly' | 'biweekly'

export type StripeConnectMode = 'live' | 'test' | 'sandbox'

export type WorkspaceRole = 'owner' | 'member' | 'viewer' | 'billing'

export interface Customer {
  id: string
  name: string | null
  email: string | null
  normalizedEmail: string | null
  avatar: string | null
  externalId: string | null
  stripeCustomerId: string | null
  linkId: string | null
  clickId: string | null
  clickedAt: Timestamp | null
  country: string | null
  sales: Generated<number>
  saleAmount: Generated<bigint>
  firstSaleAt: Timestamp | null
  subscriptionCanceledAt: Timestamp | null
  projectId: string
  projectConnectId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Dashboard {
  id: string
  linkId: string | null
  folderId: string | null
  projectId: string | null
  userId: string | null
  doIndex: Generated<boolean>
  password: string | null
  showConversions: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Domain {
  id: string
  slug: string
  verified: Generated<boolean>
  placeholder: string | null
  expiredUrl: string | null
  notFoundUrl: string | null
  primary: Generated<boolean>
  archived: Generated<boolean>
  lastChecked: Generated<Timestamp>
  logo: string | null
  appleAppSiteAssociation: unknown
  assetLinks: unknown
  deepviewData: Generated<unknown>
  linkRetentionDays: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  projectId: string | null
}

export interface RegisteredDomain {
  id: string
  slug: string
  projectId: string
  domainId: string | null
  autoRenewalDisabledAt: Timestamp | null
  renewalFee: Generated<number>
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DefaultDomains {
  id: string
  dubsh: Generated<boolean>
  chatgpt: Generated<boolean>
  sptifi: Generated<boolean>
  gitnew: Generated<boolean>
  callink: Generated<boolean>
  amznid: Generated<boolean>
  ggllink: Generated<boolean>
  figpage: Generated<boolean>
  loooooooong: Generated<boolean>
  projectId: string
}

export interface Folder {
  id: string
  name: string
  description: string | null
  projectId: string
  type: Generated<FolderType>
  accessLevel: FolderAccessLevel | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface FolderUser {
  id: string
  folderId: string
  userId: string
  role: FolderUserRole | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface FolderAccessRequest {
  id: string
  folderId: string
  userId: string
  createdAt: Generated<Timestamp>
}

export interface Integration {
  id: string
  userId: string | null
  projectId: string
  name: string
  slug: string
  description: string | null
  readme: string | null
  developer: string
  website: string
  logo: string | null
  screenshots: unknown
  verified: Generated<boolean>
  installUrl: string | null
  guideUrl: string | null
  category: string | null
  comingSoon: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface InstalledIntegration {
  id: string
  userId: string
  integrationId: string
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  credentials: unknown
  settings: unknown
}

export interface Invoice {
  id: string
  workspaceId: string
  number: string | null
  status: Generated<InvoiceStatus>
  type: Generated<InvoiceType>
  paymentMethod: PaymentMethod | null
  amount: Generated<number>
  fee: Generated<number>
  refundedAmount: Generated<number>
  total: Generated<number>
  receiptUrl: string | null
  refundReason: string | null
  failedReason: string | null
  registeredDomains: unknown
  stripeChargeMetadata: unknown
  failedAttempts: Generated<number>
  createdAt: Generated<Timestamp>
  paidAt: Timestamp | null
}

export interface jackson_index {
  id: Generated<number>
  key: string
  storeKey: string
}

export interface jackson_store {
  key: string
  value: string
  iv: string | null
  tag: string | null
  namespace: string | null
  createdAt: Generated<Timestamp>
  modifiedAt: Timestamp | null
}

export interface jackson_ttl {
  key: string
  expiresAt: bigint
}

export interface Job {
  id: string
  name: string
  payload: unknown
  options: unknown
  scheduledAt: Generated<Timestamp>
  attempts: Generated<number>
  lastError: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Link {
  id: string
  domain: string
  key: string
  url: string
  shortLink: string
  archived: Generated<boolean>
  expiresAt: Timestamp | null
  expiredUrl: string | null
  disabledAt: Timestamp | null
  password: string | null
  trackConversion: Generated<boolean>
  proxy: Generated<boolean>
  title: string | null
  description: string | null
  image: string | null
  video: string | null
  utm_source: string | null
  utm_medium: string | null
  utm_campaign: string | null
  utm_term: string | null
  utm_content: string | null
  rewrite: Generated<boolean>
  linkRetentionCleanupDisabledAt: Timestamp | null
  doIndex: Generated<boolean>
  ios: string | null
  android: string | null
  geo: unknown
  testVariants: unknown
  testStartedAt: Timestamp | null
  testCompletedAt: Timestamp | null
  userId: string | null
  projectId: string | null
  folderId: string | null
  externalId: string | null
  tenantId: string | null
  publicStats: Generated<boolean>
  clicks: Generated<number>
  leads: Generated<number>
  conversions: Generated<number>
  sales: Generated<number>
  saleAmount: Generated<bigint>
  lastClicked: Timestamp | null
  lastLeadAt: Timestamp | null
  lastConversionAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  comments: string | null
}

export interface YearInReview {
  id: string
  year: number
  totalLinks: number
  totalClicks: number
  topLinks: unknown
  topCountries: unknown
  workspaceId: string
  createdAt: Generated<Timestamp>
  sentAt: Timestamp | null
}

export interface UserNotificationPreferences {
  id: string
  userId: string
  dubLinks: Generated<boolean>
}

export interface NotificationPreference {
  id: string
  projectUserId: string
  linkUsageSummary: Generated<boolean>
  domainConfigurationUpdates: Generated<boolean>
}

export interface OAuthApp {
  id: string
  integrationId: string
  clientId: string
  hashedClientSecret: string
  partialClientSecret: string
  redirectUris: unknown
  pkce: Generated<boolean>
}

export interface OAuthCode {
  id: string
  clientId: string
  userId: string
  projectId: string
  code: string
  scopes: string | null
  redirectUri: string
  codeChallenge: string | null
  codeChallengeMethod: string | null
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface OAuthRefreshToken {
  id: string
  installationId: string
  accessTokenId: string
  hashedRefreshToken: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface User {
  id: string
  name: string | null
  email: string | null
  emailVerified: Timestamp | null
  image: string | null
  isMachine: Generated<boolean>
  createdAt: Generated<Timestamp>
  passwordHash: string | null
  invalidLoginAttempts: Generated<number>
  lockedAt: Timestamp | null
  defaultWorkspace: string | null
  source: string | null
  sentMail: Generated<boolean>
}

export interface Account {
  id: string
  userId: string
  type: string
  provider: string
  providerAccountId: string
  refresh_token: string | null
  refresh_token_expires_in: number | null
  access_token: string | null
  expires_at: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
}

export interface Tag {
  id: string
  name: string
  color: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  projectId: string | null
}

export interface LinkTag {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  linkId: string
  tagId: string
}

export interface Token {
  id: string
  name: string
  hashedKey: string
  partialKey: string
  expires: Timestamp | null
  lastUsed: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: string
}

export interface RestrictedToken {
  id: string
  name: string
  hashedKey: string
  partialKey: string
  scopes: string | null
  expires: Timestamp | null
  lastUsed: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: string
  projectId: string
  installationId: string | null
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface EmailVerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface PasswordResetToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface UtmTemplate {
  id: string
  name: string
  utm_source: string | null
  utm_medium: string | null
  utm_campaign: string | null
  utm_term: string | null
  utm_content: string | null
  ref: string | null
  userId: string | null
  projectId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Webhook {
  id: string
  projectId: string
  installationId: string | null
  receiver: Generated<WebhookReceiver>
  name: string
  url: string
  secret: string
  triggers: unknown
  linkScope: LinkScope | null
  consecutiveFailures: Generated<number>
  lastFailedAt: Timestamp | null
  disabledAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface LinkWebhook {
  id: string
  linkId: string
  webhookId: string
  createdAt: Generated<Timestamp>
}

export interface FolderWebhook {
  id: string
  folderId: string
  webhookId: string
  createdAt: Generated<Timestamp>
}

export interface Project {
  id: string
  name: string
  slug: string
  logo: string | null
  inviteCode: string | null
  plan: Generated<string>
  planTier: Generated<number>
  planPeriod: PlanPeriod | null
  stripeId: string | null
  billingCycleStart: number
  billingCycleEndsAt: Timestamp | null
  trialEndsAt: Timestamp | null
  subscriptionCanceledAt: Timestamp | null
  paymentFailedAt: Timestamp | null
  invoicePrefix: string | null
  stripeConnectId: string | null
  stripeConnectMode: StripeConnectMode | null
  shopifyStoreId: string | null
  totalLinks: Generated<number>
  totalClicks: Generated<number>
  usage: Generated<number>
  usageLimit: Generated<number>
  linksUsage: Generated<number>
  linksLimit: Generated<number>
  domainsLimit: Generated<number>
  tagsLimit: Generated<number>
  foldersUsage: Generated<number>
  foldersLimit: Generated<number>
  usersLimit: Generated<number>
  aiUsage: Generated<number>
  aiLimit: Generated<number>
  store: unknown
  siteVisitTrackingSettings: unknown
  allowedHostnames: unknown
  publishableKey: string | null
  conversionEnabled: Generated<boolean>
  webhookEnabled: Generated<boolean>
  dotLinkClaimed: Generated<boolean>
  ssoEmailDomain: string | null
  ssoEnforcedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  usageLastChecked: Generated<Timestamp>
  disabledAt: Timestamp | null
}

export interface ProjectInvite {
  email: string
  expires: Timestamp
  projectId: string
  role: Generated<WorkspaceRole>
  createdAt: Generated<Timestamp>
}

export interface ProjectUsers {
  id: string
  role: Generated<WorkspaceRole>
  userId: string
  projectId: string
  workspacePreferences: unknown
  defaultFolderId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SentEmail {
  id: string
  type: string
  createdAt: Generated<Timestamp>
  projectId: string | null
}

export interface DB {
  Customer: Customer
  Dashboard: Dashboard
  Domain: Domain
  RegisteredDomain: RegisteredDomain
  DefaultDomains: DefaultDomains
  Folder: Folder
  FolderUser: FolderUser
  FolderAccessRequest: FolderAccessRequest
  Integration: Integration
  InstalledIntegration: InstalledIntegration
  Invoice: Invoice
  jackson_index: jackson_index
  jackson_store: jackson_store
  jackson_ttl: jackson_ttl
  Job: Job
  Link: Link
  YearInReview: YearInReview
  UserNotificationPreferences: UserNotificationPreferences
  NotificationPreference: NotificationPreference
  OAuthApp: OAuthApp
  OAuthCode: OAuthCode
  OAuthRefreshToken: OAuthRefreshToken
  User: User
  Account: Account
  Session: Session
  Tag: Tag
  LinkTag: LinkTag
  Token: Token
  RestrictedToken: RestrictedToken
  VerificationToken: VerificationToken
  EmailVerificationToken: EmailVerificationToken
  PasswordResetToken: PasswordResetToken
  UtmTemplate: UtmTemplate
  Webhook: Webhook
  LinkWebhook: LinkWebhook
  FolderWebhook: FolderWebhook
  Project: Project
  ProjectInvite: ProjectInvite
  ProjectUsers: ProjectUsers
  SentEmail: SentEmail
}

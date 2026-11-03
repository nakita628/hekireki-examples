import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ChatProvider = 'WHATSAPP' | 'DIALOG360'

export type WorkspaceRole = 'ADMIN' | 'MEMBER' | 'GUEST'

export type GraphNavigation = 'MOUSE' | 'TRACKPAD'

export type Plan =
  | 'FREE'
  | 'STARTER'
  | 'PRO'
  | 'LIFETIME'
  | 'OFFERED'
  | 'CUSTOM'
  | 'UNLIMITED'
  | 'ENTERPRISE'

export type CollaborationType = 'READ' | 'WRITE' | 'FULL_ACCESS'

export interface Account {
  id: string
  userId: string
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
  oauth_token_secret: string | null
  oauth_token: string | null
  refresh_token_expires_in: number | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
}

export interface User {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastActivityAt: Generated<Timestamp>
  name: string | null
  email: string | null
  emailVerified: Timestamp | null
  image: string | null
  company: string | null
  onboardingCategories: unknown
  referral: string | null
  graphNavigation: GraphNavigation | null
  preferredAppAppearance: string | null
  displayedInAppNotifications: unknown
  groupTitlesAutoGeneration: unknown
  preferredLanguage: string | null
  termsAcceptedAt: Timestamp | null
}

export interface ApiToken {
  id: string
  createdAt: Generated<Timestamp>
  token: string
  name: string
  ownerId: string
}

export interface Workspace {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  icon: string | null
  plan: Generated<Plan>
  stripeId: string | null
  additionalChatsIndex: Generated<number>
  additionalStorageIndex: Generated<number>
  chatsLimitFirstEmailSentAt: Timestamp | null
  storageLimitFirstEmailSentAt: Timestamp | null
  chatsLimitSecondEmailSentAt: Timestamp | null
  storageLimitSecondEmailSentAt: Timestamp | null
  customChatsLimit: number | null
  customStorageLimit: number | null
  customSeatsLimit: number | null
  isQuarantined: Generated<boolean>
  isSuspended: Generated<boolean>
  isPastDue: Generated<boolean>
  isVerified: boolean | null
  chatsHardLimit: number | null
  settings: unknown
  lastActivityAt: Timestamp | null
}

export interface Space {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  icon: string | null
  workspaceId: string
}

export interface MemberInWorkspace {
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: string
  workspaceId: string
  role: WorkspaceRole
}

export interface WorkspaceInvitation {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  email: string
  workspaceId: string
  type: WorkspaceRole
}

export interface CustomDomain {
  name: string
  createdAt: Generated<Timestamp>
  workspaceId: string
}

export interface Credentials {
  id: string
  createdAt: Generated<Timestamp>
  workspaceId: string
  data: string
  name: string
  type: string
  iv: string
}

export interface UserCredentials {
  id: string
  createdAt: Generated<Timestamp>
  userId: string
  data: string
  name: string
  type: string
  iv: string
}

export interface VerificationToken {
  identifier: string
  token: string
  value: string | null
  failedAttempts: Generated<number>
  expires: Timestamp
}

export interface DashboardFolder {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  parentFolderId: string | null
  workspaceId: string
}

export interface Typebot {
  id: string
  version: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  icon: string | null
  name: string
  folderId: string | null
  groups: unknown
  events: unknown
  variables: unknown
  edges: unknown
  theme: unknown
  selectedThemeTemplateId: string | null
  settings: unknown
  publicId: string | null
  customDomain: string | null
  workspaceId: string
  resultsTablePreferences: unknown
  isArchived: Generated<boolean>
  isClosed: Generated<boolean>
  whatsAppCredentialsId: string | null
  riskLevel: number | null
  spaceId: string | null
}

export interface Invitation {
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  email: string
  typebotId: string
  type: CollaborationType
}

export interface CollaboratorsOnTypebots {
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: string
  typebotId: string
  type: CollaborationType
}

export interface PublicTypebot {
  id: string
  version: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastActivityAt: Timestamp | null
  typebotId: string
  groups: unknown
  events: unknown
  variables: unknown
  edges: unknown
  theme: unknown
  settings: unknown
}

export interface RuntimeMediaIdCache {
  provider: ChatProvider
  url: string
  mediaId: string
  expiresAt: Timestamp | null
  publicTypebotId: string
}

export interface Result {
  id: string
  createdAt: Generated<Timestamp>
  typebotId: string
  variables: unknown
  isCompleted: boolean
  hasStarted: boolean | null
  isArchived: Generated<boolean | null>
  lastChatSessionId: string | null
}

export interface SetVariableHistoryItem {
  resultId: string
  index: number
  variableId: string
  blockId: string
  blockIndex: number | null
  value: unknown
}

export interface VisitedEdge {
  resultId: string
  edgeId: string
  index: number
}

export interface Log {
  id: string
  createdAt: Generated<Timestamp>
  resultId: string
  status: string
  context: string | null
  description: string
  details: string | null
}

export interface Answer {
  createdAt: Generated<Timestamp>
  resultId: string
  blockId: string
  groupId: string
  variableId: string | null
  content: string
}

export interface AnswerV2 {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  blockId: string
  content: string
  attachedFileUrls: unknown
  resultId: string
}

export interface Coupon {
  userPropertiesToUpdate: unknown
  code: string
  dateRedeemed: Timestamp | null
}

export interface Webhook {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  url: string | null
  method: string
  queryParams: unknown
  headers: unknown
  body: string | null
  typebotId: string
}

export interface ClaimableCustomPlan {
  id: string
  createdAt: Generated<Timestamp>
  claimedAt: Timestamp | null
  name: string
  description: string | null
  price: number
  currency: string
  workspaceId: string
  chatsLimit: number
  storageLimit: number
  seatsLimit: number
  isYearly: Generated<boolean>
  companyName: string | null
  vatType: string | null
  vatValue: string | null
}

export interface ChatSession {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  state: unknown
  isReplying: boolean | null
}

export interface ThemeTemplate {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  name: string
  theme: unknown
  workspaceId: string
}

export interface BannedIp {
  id: string
  createdAt: Generated<Timestamp>
  ip: string
  responsibleTypebotId: string
  userId: string
}

export interface SuppressedEmail {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  email: string
  transientGeneralBounceCount: Generated<number>
  suppressedAt: Timestamp | null
  lastWebhookId: string | null
}

export interface DB {
  Account: Account
  Session: Session
  User: User
  ApiToken: ApiToken
  Workspace: Workspace
  Space: Space
  MemberInWorkspace: MemberInWorkspace
  WorkspaceInvitation: WorkspaceInvitation
  CustomDomain: CustomDomain
  Credentials: Credentials
  UserCredentials: UserCredentials
  VerificationToken: VerificationToken
  DashboardFolder: DashboardFolder
  Typebot: Typebot
  Invitation: Invitation
  CollaboratorsOnTypebots: CollaboratorsOnTypebots
  PublicTypebot: PublicTypebot
  RuntimeMediaIdCache: RuntimeMediaIdCache
  Result: Result
  SetVariableHistoryItem: SetVariableHistoryItem
  VisitedEdge: VisitedEdge
  Log: Log
  Answer: Answer
  AnswerV2: AnswerV2
  Coupon: Coupon
  Webhook: Webhook
  ClaimableCustomPlan: ClaimableCustomPlan
  ChatSession: ChatSession
  ThemeTemplate: ThemeTemplate
  BannedIp: BannedIp
  SuppressedEmail: SuppressedEmail
}

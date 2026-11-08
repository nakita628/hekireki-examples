import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ProjectType = 'website' | 'app' | 'backend'

export type ProjectScope = 'all' | 'selected'

export type AccessLevel = 'read' | 'write' | 'admin'

export type ClientType = 'read' | 'write' | 'root'

export type Interval = 'hour' | 'day' | 'month' | 'minute' | 'week'

export type ChartType =
  | 'linear'
  | 'bar'
  | 'histogram'
  | 'pie'
  | 'metric'
  | 'area'
  | 'map'
  | 'funnel'
  | 'retention'
  | 'conversion'
  | 'sankey'
  | 'distribution'

export type Metric = 'sum' | 'average' | 'min' | 'max' | 'count'

export type ImportStatus = 'pending' | 'processing' | 'completed' | 'failed'

export type InsightState = 'active' | 'suppressed' | 'closed'

export interface CodeMigration {
  id: Generated<string>
  name: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Conversation {
  id: Generated<string>
  projectId: string
  organizationId: string
  userId: string
  title: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ChatMessage {
  id: Generated<string>
  conversationId: string
  role: string
  parts: unknown
  metadata: unknown
  createdAt: Generated<Timestamp>
}

export interface Organization {
  id: Generated<string>
  name: string
  slug: string | null
  createdByUserId: string | null
  timezone: string | null
  onboarding: Generated<string | null>
  attribution: unknown
  subscriptionId: string | null
  subscriptionCustomerId: string | null
  subscriptionPriceId: string | null
  subscriptionProductId: string | null
  subscriptionStatus: string | null
  subscriptionStartsAt: Timestamp | null
  subscriptionEndsAt: Timestamp | null
  subscriptionCanceledAt: Timestamp | null
  subscriptionCreatedByUserId: string | null
  subscriptionPeriodEventsCount: Generated<number>
  subscriptionPeriodEventsCountExceededAt: Timestamp | null
  subscriptionPeriodEventsLimit: Generated<number>
  subscriptionInterval: string | null
  subscriptionCancelReason: string | null
  subscriptionCancelComment: string | null
  subscriptionSaveDiscountAppliedAt: Timestamp | null
  subscriptionPauseAtPeriodEnd: Generated<boolean>
  subscriptionResumesAt: Timestamp | null
  subscriptionFirstStartedAt: Timestamp | null
  usageWarningSentAt: Timestamp | null
  usageExceededSentAt: Timestamp | null
  subscriptionDiscount: unknown
  windDownStartedAt: Timestamp | null
  windDownStep: string | null
  deleteAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface User {
  id: Generated<string>
  email: string
  firstName: string | null
  lastName: string | null
  name: string | null
  emailVerified: Generated<boolean>
  image: string | null
  twoFactorEnabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  deletedAt: Timestamp | null
}

export interface UserTotp {
  id: Generated<string>
  userId: string
  secret: string
  recoveryCodes: string[]
  enabledAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TwoFactor {
  id: Generated<string>
  userId: string
  secret: string
  backupCodes: string
  verified: Generated<boolean>
  failedVerificationCount: Generated<number>
  lockedUntil: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface TwoFactorChallenge {
  id: string
  userId: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface Account {
  id: Generated<string>
  userId: string
  email: string | null
  provider: string
  providerId: string | null
  accountId: string | null
  idToken: string | null
  accessToken: string | null
  refreshToken: string | null
  accessTokenExpiresAt: Timestamp | null
  refreshTokenExpiresAt: Timestamp | null
  scope: string | null
  password: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Session {
  id: string
  token: string | null
  userId: string
  expiresAt: Timestamp
  ipAddress: string | null
  userAgent: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Verification {
  id: string
  identifier: string
  value: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Member {
  id: Generated<string>
  role: string
  email: string
  userId: string | null
  invitedById: string | null
  organizationId: string
  projectScope: Generated<ProjectScope>
  meta: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface MemberProject {
  memberId: string
  projectId: string
  level: AccessLevel | null
  createdAt: Generated<Timestamp>
}

export interface Invite {
  id: string
  email: string
  createdById: string
  organizationId: string
  projectAccess: Generated<unknown>
  projectScope: Generated<ProjectScope>
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  role: string
}

export interface Project {
  id: Generated<string>
  name: string
  organizationId: string
  eventsCount: Generated<number>
  types: Generated<ProjectType[]>
  domain: string | null
  cors: Generated<string[]>
  crossDomain: Generated<boolean>
  allowUnsafeRevenueTracking: Generated<boolean>
  aiStoreContent: Generated<boolean>
  filters: Generated<unknown>
  firstEventAt: Timestamp | null
  noDataNotifiedAt: Timestamp | null
  dataStoppedNotifiedAt: Timestamp | null
  deleteAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Cohort {
  id: Generated<string>
  name: string
  description: string | null
  projectId: string
  definition: Generated<unknown>
  isStatic: Generated<boolean>
  profileCount: Generated<number>
  lastComputedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Salt {
  salt: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Client {
  id: Generated<string>
  name: string
  secret: string | null
  type: Generated<ClientType>
  projectId: string | null
  organizationId: string
  ignoreCorsAndSecret: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Dashboard {
  id: Generated<string>
  name: string
  organizationId: string
  projectId: string
  filters: Generated<unknown>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Report {
  id: Generated<string>
  name: string
  interval: Interval
  range: Generated<string>
  chartType: ChartType
  lineType: Generated<string>
  breakdowns: unknown
  events: unknown
  globalFilters: Generated<unknown>
  formula: string | null
  unit: string | null
  metric: Generated<Metric>
  projectId: string
  previous: Generated<boolean>
  criteria: string | null
  funnelGroup: string | null
  funnelWindow: number | null
  options: unknown
  display: unknown
  visibleSeries: string[]
  startDate: string | null
  endDate: string | null
  dashboardId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ReportLayout {
  id: Generated<string>
  reportId: string
  gridColumns: Generated<number>
  x: Generated<number>
  y: Generated<number>
  w: Generated<number>
  h: Generated<number>
  minW: Generated<number | null>
  minH: Generated<number | null>
  maxW: number | null
  maxH: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ShareOverview {
  id: string
  projectId: string
  organizationId: string
  public: Generated<boolean>
  password: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ShareDashboard {
  id: string
  dashboardId: string
  organizationId: string
  projectId: string
  public: Generated<boolean>
  password: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ShareReport {
  id: string
  reportId: string
  organizationId: string
  projectId: string
  public: Generated<boolean>
  password: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ShareWidget {
  id: string
  projectId: string
  organizationId: string
  public: Generated<boolean>
  options: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface EventMeta {
  id: Generated<string>
  name: string
  conversion: boolean | null
  color: string | null
  icon: string | null
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface CustomEvent {
  id: Generated<string>
  name: string
  items: unknown
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Reference {
  id: Generated<string>
  title: string
  description: string | null
  date: Generated<Timestamp>
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface NotificationRule {
  id: Generated<string>
  name: string
  projectId: string
  sendToApp: Generated<boolean>
  sendToEmail: Generated<boolean>
  config: unknown
  template: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Notification {
  id: Generated<string>
  projectId: string
  title: string
  message: string
  isReadAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  sendToApp: Generated<boolean>
  sendToEmail: Generated<boolean>
  integrationId: string | null
  notificationRuleId: string | null
  payload: unknown
}

export interface Integration {
  id: Generated<string>
  name: string
  config: unknown
  organizationId: string
  projectId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ExportWatermark {
  id: Generated<string>
  projectId: string
  integrationId: string
  lastInsertedAt: Timestamp
  lastEventId: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface ResetPassword {
  id: string
  accountId: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface Import {
  id: Generated<string>
  projectId: string
  jobId: string | null
  status: ImportStatus
  statusMessage: string | null
  errorMessage: string | null
  config: unknown
  totalEvents: Generated<number>
  processedEvents: Generated<number>
  currentStep: string | null
  currentBatch: string | null
  createdAt: Generated<Timestamp>
  completedAt: Timestamp | null
  updatedAt: Generated<Timestamp>
}

export interface ProjectInsight {
  id: Generated<string>
  projectId: string
  moduleKey: string
  dimensionKey: string
  windowKind: string
  state: Generated<InsightState>
  title: string
  summary: string | null
  displayName: Generated<string>
  payload: Generated<unknown>
  direction: string | null
  impactScore: Generated<number>
  severityBand: string | null
  relevanceScore: number | null
  aiSummary: string | null
  aiCategory: string | null
  emailWorthy: Generated<boolean>
  referenceWorthy: Generated<boolean>
  enrichedAt: Timestamp | null
  enrichVersion: number | null
  version: Generated<number>
  threadId: Generated<string>
  windowStart: Timestamp | null
  windowEnd: Timestamp | null
  firstDetectedAt: Generated<Timestamp>
  lastUpdatedAt: Generated<Timestamp>
  lastSeenAt: Generated<Timestamp>
}

export interface InsightEvent {
  id: Generated<string>
  insightId: string
  eventKind: string
  changeFrom: unknown
  changeTo: unknown
  createdAt: Generated<Timestamp>
}

export interface GscConnection {
  id: Generated<string>
  projectId: string
  siteUrl: Generated<string>
  accessToken: string
  refreshToken: string
  accessTokenExpiresAt: Timestamp | null
  lastSyncedAt: Timestamp | null
  lastSyncStatus: string | null
  lastSyncError: string | null
  backfillStatus: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
}

export interface EmailUnsubscribe {
  id: Generated<string>
  email: string
  category: string
  createdAt: Generated<Timestamp>
}

export interface IntegrationToNotificationRule {
  A: string
  B: string
}

export interface DB {
  __code_migrations: CodeMigration
  conversations: Conversation
  chat_messages: ChatMessage
  organizations: Organization
  users: User
  user_totp: UserTotp
  two_factor: TwoFactor
  two_factor_challenges: TwoFactorChallenge
  accounts: Account
  sessions: Session
  verifications: Verification
  members: Member
  member_projects: MemberProject
  invites: Invite
  projects: Project
  cohorts: Cohort
  salts: Salt
  clients: Client
  dashboards: Dashboard
  reports: Report
  report_layouts: ReportLayout
  shares: ShareOverview
  share_dashboards: ShareDashboard
  share_reports: ShareReport
  share_widgets: ShareWidget
  event_meta: EventMeta
  custom_events: CustomEvent
  references: Reference
  notification_rules: NotificationRule
  notifications: Notification
  integrations: Integration
  export_watermarks: ExportWatermark
  reset_password: ResetPassword
  imports: Import
  project_insights: ProjectInsight
  insight_events: InsightEvent
  gsc_connections: GscConnection
  email_unsubscribes: EmailUnsubscribe
  _IntegrationToNotificationRule: IntegrationToNotificationRule
}

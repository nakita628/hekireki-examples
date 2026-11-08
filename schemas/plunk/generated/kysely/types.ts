import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ProjectDisabledReason =
  | 'PAYMENT_FAILED'
  | 'EMAIL_REPUTATION'
  | 'PHISHING_DETECTED'
  | 'CARD_VERIFICATION_FAILED'
  | 'CARD_VERIFICATION_REQUIRED'
  | 'MANUAL'

export type CardVerificationStatus = 'PENDING' | 'VERIFIED' | 'REQUIRES_ACTION' | 'FAILED'

export type AuthMethod = 'PASSWORD' | 'GOOGLE_OAUTH' | 'GITHUB_OAUTH'

export type Role = 'OWNER' | 'ADMIN' | 'MEMBER'

export type TemplateType = 'TRANSACTIONAL' | 'MARKETING' | 'HEADLESS'

export type TrackingMode = 'ENABLED' | 'DISABLED' | 'MARKETING_ONLY'

export type CampaignStatus = 'DRAFT' | 'SCHEDULED' | 'SENDING' | 'SENT' | 'CANCELLED'

export type CampaignAudienceType = 'ALL' | 'FILTERED' | 'SEGMENT'

export type WorkflowTriggerType = 'EVENT' | 'MANUAL' | 'SCHEDULE'

export type WorkflowStepType =
  | 'TRIGGER'
  | 'SEND_EMAIL'
  | 'DELAY'
  | 'WAIT_FOR_EVENT'
  | 'CONDITION'
  | 'EXIT'
  | 'WEBHOOK'
  | 'UPDATE_CONTACT'

export type WorkflowExecutionStatus =
  | 'RUNNING'
  | 'WAITING'
  | 'COMPLETED'
  | 'EXITED'
  | 'FAILED'
  | 'CANCELLED'

export type StepExecutionStatus =
  | 'PENDING'
  | 'SCHEDULED'
  | 'WAITING'
  | 'RUNNING'
  | 'COMPLETED'
  | 'SKIPPED'
  | 'FAILED'

export type SegmentType = 'DYNAMIC' | 'STATIC'

export type EmailSourceType = 'TRANSACTIONAL' | 'CAMPAIGN' | 'WORKFLOW' | 'INBOUND'

export type EmailStatus =
  | 'SCHEDULED'
  | 'CANCELLED'
  | 'PENDING'
  | 'SENDING'
  | 'SENT'
  | 'DELIVERED'
  | 'RECEIVED'
  | 'OPENED'
  | 'CLICKED'
  | 'BOUNCED'
  | 'COMPLAINED'
  | 'FAILED'

export interface User {
  id: string
  email: string
  password: string | null
  type: AuthMethod
  emailVerified: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Project {
  id: string
  name: string
  public: string
  secret: string
  disabled: Generated<boolean>
  disabledReason: ProjectDisabledReason | null
  customer: string | null
  subscription: string | null
  cardVerification: CardVerificationStatus | null
  cardVerificationIntent: string | null
  cardVerificationAt: Timestamp | null
  cardVerificationSession: string | null
  cardVerificationPaymentMethod: string | null
  cardVerificationFingerprint: string | null
  cardVerificationAttempts: Generated<number>
  billingLimitWorkflows: number | null
  billingLimitCampaigns: number | null
  billingLimitTransactional: number | null
  billingLimitInbound: number | null
  tracking: Generated<TrackingMode>
  language: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Membership {
  userId: string
  projectId: string
  role: Generated<Role>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Domain {
  id: string
  domain: string
  verified: Generated<boolean>
  dkimTokens: unknown
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Contact {
  id: string
  email: string
  data: unknown
  subscribed: Generated<boolean>
  snoozedUntil: Timestamp | null
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Template {
  id: string
  name: string
  description: string | null
  subject: string
  body: string
  from: string
  fromName: string | null
  replyTo: string | null
  type: Generated<TemplateType>
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Segment {
  id: string
  name: string
  description: string | null
  type: Generated<SegmentType>
  condition: unknown
  trackMembership: Generated<boolean>
  memberCount: Generated<number>
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface SegmentMembership {
  contactId: string
  segmentId: string
  enteredAt: Generated<Timestamp>
  exitedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Campaign {
  id: string
  name: string
  description: string | null
  status: Generated<CampaignStatus>
  subject: string
  body: string
  from: string
  fromName: string | null
  replyTo: string | null
  type: Generated<TemplateType>
  audienceType: Generated<CampaignAudienceType>
  audienceCondition: unknown
  segmentId: string | null
  scheduledFor: Timestamp | null
  totalRecipients: Generated<number>
  sentCount: Generated<number>
  deliveredCount: Generated<number>
  openedCount: Generated<number>
  clickedCount: Generated<number>
  bouncedCount: Generated<number>
  complainedCount: Generated<number>
  unsubscribedCount: Generated<number>
  archived: Generated<boolean>
  projectId: string
  sentAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Workflow {
  id: string
  name: string
  description: string | null
  enabled: Generated<boolean>
  triggerType: WorkflowTriggerType
  triggerConfig: unknown
  allowReentry: Generated<boolean>
  throttleWindowSeconds: number | null
  throttleKey: string | null
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowStep {
  id: string
  type: WorkflowStepType
  name: string
  position: unknown
  config: unknown
  workflowId: string
  templateId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowTransition {
  id: string
  fromStepId: string
  toStepId: string
  condition: unknown
  priority: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowExecution {
  id: string
  workflowId: string
  contactId: string
  status: Generated<WorkflowExecutionStatus>
  currentStepId: string | null
  exitReason: string | null
  context: unknown
  startedAt: Generated<Timestamp>
  completedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowStepExecution {
  id: string
  executionId: string
  stepId: string
  status: Generated<StepExecutionStatus>
  scheduledFor: Timestamp | null
  executeAfter: Timestamp | null
  output: unknown
  error: string | null
  startedAt: Timestamp | null
  completedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Email {
  id: string
  contactId: string
  toName: string | null
  subject: string
  body: string
  from: string
  fromName: string | null
  replyTo: string | null
  headers: unknown
  attachments: unknown
  messageId: string | null
  sourceType: EmailSourceType
  templateId: string | null
  campaignId: string | null
  workflowExecutionId: string | null
  workflowStepExecutionId: string | null
  status: Generated<EmailStatus>
  scheduledFor: Timestamp | null
  sentAt: Timestamp | null
  deliveredAt: Timestamp | null
  openedAt: Timestamp | null
  clickedAt: Timestamp | null
  bouncedAt: Timestamp | null
  complainedAt: Timestamp | null
  unsubscribedAt: Timestamp | null
  opens: Generated<number>
  clicks: Generated<number>
  simulated: Generated<boolean>
  error: string | null
  projectId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Event {
  id: string
  name: string
  data: unknown
  projectId: string
  contactId: string | null
  emailId: string | null
  createdAt: Generated<Timestamp>
}

export interface IdempotencyKey {
  id: string
  key: string
  projectId: string
  method: string
  path: string
  statusCode: number | null
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
}

export interface ThrottleClaim {
  id: string
  projectId: string
  key: string
  recipientKey: string
  contactId: string | null
  emailId: string | null
  workflowId: string | null
  windowSeconds: number | null
  expiresAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ApiRequest {
  id: string
  method: string
  path: string
  statusCode: number
  duration: number
  projectId: string | null
  userId: string | null
  authType: string | null
  ip: string | null
  userAgent: string | null
  errorCode: string | null
  errorMessage: string | null
  requestSize: number | null
  responseSize: number | null
  createdAt: Generated<Timestamp>
}

export interface DB {
  users: User
  projects: Project
  memberships: Membership
  domains: Domain
  contacts: Contact
  templates: Template
  segments: Segment
  segment_memberships: SegmentMembership
  campaigns: Campaign
  workflows: Workflow
  workflow_steps: WorkflowStep
  workflow_transitions: WorkflowTransition
  workflow_executions: WorkflowExecution
  workflow_step_executions: WorkflowStepExecution
  emails: Email
  events: Event
  idempotency_keys: IdempotencyKey
  throttle_claims: ThrottleClaim
  api_requests: ApiRequest
}

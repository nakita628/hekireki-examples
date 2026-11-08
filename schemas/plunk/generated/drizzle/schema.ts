import {
  boolean,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'

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

export const projectDisabledReasonEnum = pgEnum('ProjectDisabledReason', [
  'PAYMENT_FAILED',
  'EMAIL_REPUTATION',
  'PHISHING_DETECTED',
  'CARD_VERIFICATION_FAILED',
  'CARD_VERIFICATION_REQUIRED',
  'MANUAL',
])

export const cardVerificationStatusEnum = pgEnum('CardVerificationStatus', [
  'PENDING',
  'VERIFIED',
  'REQUIRES_ACTION',
  'FAILED',
])

export const authMethodEnum = pgEnum('AuthMethod', ['PASSWORD', 'GOOGLE_OAUTH', 'GITHUB_OAUTH'])

export const roleEnum = pgEnum('Role', ['OWNER', 'ADMIN', 'MEMBER'])

export const templateTypeEnum = pgEnum('TemplateType', ['TRANSACTIONAL', 'MARKETING', 'HEADLESS'])

export const trackingModeEnum = pgEnum('TrackingMode', ['ENABLED', 'DISABLED', 'MARKETING_ONLY'])

export const campaignStatusEnum = pgEnum('CampaignStatus', [
  'DRAFT',
  'SCHEDULED',
  'SENDING',
  'SENT',
  'CANCELLED',
])

export const campaignAudienceTypeEnum = pgEnum('CampaignAudienceType', [
  'ALL',
  'FILTERED',
  'SEGMENT',
])

export const workflowTriggerTypeEnum = pgEnum('WorkflowTriggerType', [
  'EVENT',
  'MANUAL',
  'SCHEDULE',
])

export const workflowStepTypeEnum = pgEnum('WorkflowStepType', [
  'TRIGGER',
  'SEND_EMAIL',
  'DELAY',
  'WAIT_FOR_EVENT',
  'CONDITION',
  'EXIT',
  'WEBHOOK',
  'UPDATE_CONTACT',
])

export const workflowExecutionStatusEnum = pgEnum('WorkflowExecutionStatus', [
  'RUNNING',
  'WAITING',
  'COMPLETED',
  'EXITED',
  'FAILED',
  'CANCELLED',
])

export const stepExecutionStatusEnum = pgEnum('StepExecutionStatus', [
  'PENDING',
  'SCHEDULED',
  'WAITING',
  'RUNNING',
  'COMPLETED',
  'SKIPPED',
  'FAILED',
])

export const segmentTypeEnum = pgEnum('SegmentType', ['DYNAMIC', 'STATIC'])

export const emailSourceTypeEnum = pgEnum('EmailSourceType', [
  'TRANSACTIONAL',
  'CAMPAIGN',
  'WORKFLOW',
  'INBOUND',
])

export const emailStatusEnum = pgEnum('EmailStatus', [
  'SCHEDULED',
  'CANCELLED',
  'PENDING',
  'SENDING',
  'SENT',
  'DELIVERED',
  'RECEIVED',
  'OPENED',
  'CLICKED',
  'BOUNCED',
  'COMPLAINED',
  'FAILED',
])

export const users = pgTable(
  'users',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    email: text('email').notNull(),
    password: text('password'),
    type: authMethodEnum('type').notNull(),
    emailVerified: boolean('emailVerified').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('users_email_key').on(table.email)],
)

export const projects = pgTable(
  'projects',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    public: text('public').notNull(),
    secret: text('secret').notNull(),
    disabled: boolean('disabled').notNull().default(false),
    disabledReason: projectDisabledReasonEnum('disabledReason'),
    customer: text('customer'),
    subscription: text('subscription'),
    cardVerification: cardVerificationStatusEnum('cardVerification'),
    cardVerificationIntent: text('cardVerificationIntent'),
    cardVerificationAt: timestamp('cardVerificationAt', { precision: 3 }),
    cardVerificationSession: text('cardVerificationSession'),
    cardVerificationPaymentMethod: text('cardVerificationPaymentMethod'),
    cardVerificationFingerprint: text('cardVerificationFingerprint'),
    cardVerificationAttempts: integer('cardVerificationAttempts').notNull().default(0),
    billingLimitWorkflows: integer('billingLimitWorkflows'),
    billingLimitCampaigns: integer('billingLimitCampaigns'),
    billingLimitTransactional: integer('billingLimitTransactional'),
    billingLimitInbound: integer('billingLimitInbound'),
    tracking: trackingModeEnum('tracking').notNull().default('ENABLED'),
    language: text('language').notNull().default('en'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('projects_public_key').on(table.public),
    uniqueIndex('projects_secret_key').on(table.secret),
    uniqueIndex('projects_customer_key').on(table.customer),
    uniqueIndex('projects_subscription_key').on(table.subscription),
    index('projects_cardVerification_cardVerificationAt_idx').on(
      table.cardVerification,
      table.cardVerificationAt,
    ),
  ],
)

export const memberships = pgTable(
  'memberships',
  {
    userId: text('userId').notNull(),
    projectId: text('projectId').notNull(),
    role: roleEnum('role').notNull().default('MEMBER'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'memberships_pkey', columns: [table.userId, table.projectId] }),
    foreignKey({
      name: 'memberships_userId_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'memberships_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const domains = pgTable(
  'domains',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    domain: text('domain').notNull(),
    verified: boolean('verified').notNull().default(false),
    dkimTokens: jsonb('dkimTokens'),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('domains_projectId_idx').on(table.projectId),
    index('domains_projectId_verified_idx').on(table.projectId, table.verified),
    uniqueIndex('domains_projectId_domain_key').on(table.projectId, table.domain),
    foreignKey({
      name: 'domains_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const contacts = pgTable(
  'contacts',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    email: text('email').notNull(),
    data: jsonb('data'),
    subscribed: boolean('subscribed').notNull().default(true),
    snoozedUntil: timestamp('snoozedUntil', { precision: 3 }),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('contacts_projectId_idx').on(table.projectId),
    index('contacts_projectId_subscribed_idx').on(table.projectId, table.subscribed),
    index('contacts_snoozedUntil_idx').on(table.snoozedUntil),
    index('contacts_data_idx').on(table.data),
    uniqueIndex('contacts_projectId_email_key').on(table.projectId, table.email),
    foreignKey({
      name: 'contacts_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const templates = pgTable(
  'templates',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    description: text('description'),
    subject: text('subject').notNull(),
    body: text('body').notNull(),
    from: text('from').notNull(),
    fromName: text('fromName'),
    replyTo: text('replyTo'),
    type: templateTypeEnum('type').notNull().default('MARKETING'),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('templates_projectId_idx').on(table.projectId),
    index('templates_projectId_type_idx').on(table.projectId, table.type),
    foreignKey({
      name: 'templates_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const segments = pgTable(
  'segments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    description: text('description'),
    type: segmentTypeEnum('type').notNull().default('DYNAMIC'),
    condition: jsonb('condition'),
    trackMembership: boolean('trackMembership').notNull().default(false),
    memberCount: integer('memberCount').notNull().default(0),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('segments_projectId_idx').on(table.projectId),
    index('segments_condition_idx').on(table.condition),
    foreignKey({
      name: 'segments_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const segmentMemberships = pgTable(
  'segment_memberships',
  {
    contactId: text('contactId').notNull(),
    segmentId: text('segmentId').notNull(),
    enteredAt: timestamp('enteredAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    exitedAt: timestamp('exitedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'segment_memberships_pkey', columns: [table.contactId, table.segmentId] }),
    index('segment_memberships_segmentId_exitedAt_idx').on(table.segmentId, table.exitedAt),
    index('segment_memberships_contactId_exitedAt_idx').on(table.contactId, table.exitedAt),
    index('segment_memberships_enteredAt_idx').on(table.enteredAt),
    foreignKey({
      name: 'segment_memberships_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contacts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'segment_memberships_segmentId_fkey',
      columns: [table.segmentId],
      foreignColumns: [segments.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const campaigns = pgTable(
  'campaigns',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    description: text('description'),
    status: campaignStatusEnum('status').notNull().default('DRAFT'),
    subject: text('subject').notNull(),
    body: text('body').notNull(),
    from: text('from').notNull(),
    fromName: text('fromName'),
    replyTo: text('replyTo'),
    type: templateTypeEnum('type').notNull().default('MARKETING'),
    audienceType: campaignAudienceTypeEnum('audienceType').notNull().default('ALL'),
    audienceCondition: jsonb('audienceCondition'),
    segmentId: text('segmentId'),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }),
    totalRecipients: integer('totalRecipients').notNull().default(0),
    sentCount: integer('sentCount').notNull().default(0),
    deliveredCount: integer('deliveredCount').notNull().default(0),
    openedCount: integer('openedCount').notNull().default(0),
    clickedCount: integer('clickedCount').notNull().default(0),
    bouncedCount: integer('bouncedCount').notNull().default(0),
    complainedCount: integer('complainedCount').notNull().default(0),
    unsubscribedCount: integer('unsubscribedCount').notNull().default(0),
    archived: boolean('archived').notNull().default(false),
    projectId: text('projectId').notNull(),
    sentAt: timestamp('sentAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('campaigns_projectId_status_idx').on(table.projectId, table.status),
    index('campaigns_projectId_archived_createdAt_idx').on(
      table.projectId,
      table.archived,
      table.createdAt,
    ),
    index('campaigns_scheduledFor_idx').on(table.scheduledFor),
    index('campaigns_segmentId_idx').on(table.segmentId),
    index('campaigns_audienceCondition_idx').on(table.audienceCondition),
    foreignKey({
      name: 'campaigns_segmentId_fkey',
      columns: [table.segmentId],
      foreignColumns: [segments.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'campaigns_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflows = pgTable(
  'workflows',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    description: text('description'),
    enabled: boolean('enabled').notNull().default(false),
    triggerType: workflowTriggerTypeEnum('triggerType').notNull(),
    triggerConfig: jsonb('triggerConfig'),
    allowReentry: boolean('allowReentry').notNull().default(false),
    throttleWindowSeconds: integer('throttleWindowSeconds'),
    throttleKey: text('throttleKey'),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('workflows_projectId_idx').on(table.projectId),
    index('workflows_projectId_enabled_idx').on(table.projectId, table.enabled),
    foreignKey({
      name: 'workflows_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowSteps = pgTable(
  'workflow_steps',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    type: workflowStepTypeEnum('type').notNull(),
    name: text('name').notNull(),
    position: jsonb('position').notNull(),
    config: jsonb('config').notNull(),
    workflowId: text('workflowId').notNull(),
    templateId: text('templateId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('workflow_steps_workflowId_idx').on(table.workflowId),
    index('workflow_steps_templateId_idx').on(table.templateId),
    foreignKey({
      name: 'workflow_steps_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflows.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workflow_steps_templateId_fkey',
      columns: [table.templateId],
      foreignColumns: [templates.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const workflowTransitions = pgTable(
  'workflow_transitions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    fromStepId: text('fromStepId').notNull(),
    toStepId: text('toStepId').notNull(),
    condition: jsonb('condition'),
    priority: integer('priority').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('workflow_transitions_fromStepId_idx').on(table.fromStepId),
    index('workflow_transitions_toStepId_idx').on(table.toStepId),
    foreignKey({
      name: 'workflow_transitions_fromStepId_fkey',
      columns: [table.fromStepId],
      foreignColumns: [workflowSteps.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workflow_transitions_toStepId_fkey',
      columns: [table.toStepId],
      foreignColumns: [workflowSteps.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowExecutions = pgTable(
  'workflow_executions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    workflowId: text('workflowId').notNull(),
    contactId: text('contactId').notNull(),
    status: workflowExecutionStatusEnum('status').notNull().default('RUNNING'),
    currentStepId: text('currentStepId'),
    exitReason: text('exitReason'),
    context: jsonb('context'),
    startedAt: timestamp('startedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('workflow_executions_workflowId_contactId_idx').on(table.workflowId, table.contactId),
    index('workflow_executions_workflowId_status_idx').on(table.workflowId, table.status),
    index('workflow_executions_contactId_status_idx').on(table.contactId, table.status),
    index('workflow_executions_status_currentStepId_idx').on(table.status, table.currentStepId),
    foreignKey({
      name: 'workflow_executions_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflows.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workflow_executions_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contacts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workflow_executions_currentStepId_fkey',
      columns: [table.currentStepId],
      foreignColumns: [workflowSteps.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const workflowStepExecutions = pgTable(
  'workflow_step_executions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    executionId: text('executionId').notNull(),
    stepId: text('stepId').notNull(),
    status: stepExecutionStatusEnum('status').notNull().default('PENDING'),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }),
    executeAfter: timestamp('executeAfter', { precision: 3 }),
    output: jsonb('output'),
    error: text('error'),
    startedAt: timestamp('startedAt', { precision: 3 }),
    completedAt: timestamp('completedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('workflow_step_executions_executionId_status_idx').on(table.executionId, table.status),
    index('workflow_step_executions_stepId_idx').on(table.stepId),
    index('workflow_step_executions_status_scheduledFor_idx').on(table.status, table.scheduledFor),
    index('workflow_step_executions_scheduledFor_idx').on(table.scheduledFor),
    foreignKey({
      name: 'workflow_step_executions_executionId_fkey',
      columns: [table.executionId],
      foreignColumns: [workflowExecutions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workflow_step_executions_stepId_fkey',
      columns: [table.stepId],
      foreignColumns: [workflowSteps.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const emails = pgTable(
  'emails',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    contactId: text('contactId').notNull(),
    toName: text('toName'),
    subject: text('subject').notNull(),
    body: text('body').notNull(),
    from: text('from').notNull(),
    fromName: text('fromName'),
    replyTo: text('replyTo'),
    headers: jsonb('headers'),
    attachments: jsonb('attachments'),
    messageId: text('messageId'),
    sourceType: emailSourceTypeEnum('sourceType').notNull(),
    templateId: text('templateId'),
    campaignId: text('campaignId'),
    workflowExecutionId: text('workflowExecutionId'),
    workflowStepExecutionId: text('workflowStepExecutionId'),
    status: emailStatusEnum('status').notNull().default('PENDING'),
    scheduledFor: timestamp('scheduledFor', { precision: 3 }),
    sentAt: timestamp('sentAt', { precision: 3 }),
    deliveredAt: timestamp('deliveredAt', { precision: 3 }),
    openedAt: timestamp('openedAt', { precision: 3 }),
    clickedAt: timestamp('clickedAt', { precision: 3 }),
    bouncedAt: timestamp('bouncedAt', { precision: 3 }),
    complainedAt: timestamp('complainedAt', { precision: 3 }),
    unsubscribedAt: timestamp('unsubscribedAt', { precision: 3 }),
    opens: integer('opens').notNull().default(0),
    clicks: integer('clicks').notNull().default(0),
    simulated: boolean('simulated').notNull().default(false),
    error: text('error'),
    projectId: text('projectId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('emails_messageId_key').on(table.messageId),
    index('emails_projectId_contactId_idx').on(table.projectId, table.contactId),
    index('emails_contactId_idx').on(table.contactId),
    index('emails_campaignId_idx').on(table.campaignId),
    index('emails_campaignId_status_idx').on(table.campaignId, table.status),
    index('emails_workflowExecutionId_idx').on(table.workflowExecutionId),
    index('emails_workflowStepExecutionId_idx').on(table.workflowStepExecutionId),
    index('emails_status_idx').on(table.status),
    index('emails_createdAt_idx').on(table.createdAt),
    index('emails_projectId_sourceType_createdAt_idx').on(
      table.projectId,
      table.sourceType,
      table.createdAt,
    ),
    index('emails_contactId_openedAt_idx').on(table.contactId, table.openedAt),
    index('emails_contactId_clickedAt_idx').on(table.contactId, table.clickedAt),
    index('emails_contactId_bouncedAt_idx').on(table.contactId, table.bouncedAt),
    index('emails_contactId_complainedAt_idx').on(table.contactId, table.complainedAt),
    foreignKey({
      name: 'emails_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contacts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'emails_templateId_fkey',
      columns: [table.templateId],
      foreignColumns: [templates.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'emails_campaignId_fkey',
      columns: [table.campaignId],
      foreignColumns: [campaigns.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'emails_workflowExecutionId_fkey',
      columns: [table.workflowExecutionId],
      foreignColumns: [workflowExecutions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'emails_workflowStepExecutionId_fkey',
      columns: [table.workflowStepExecutionId],
      foreignColumns: [workflowStepExecutions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'emails_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const events = pgTable(
  'events',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    data: jsonb('data'),
    projectId: text('projectId').notNull(),
    contactId: text('contactId'),
    emailId: text('emailId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('events_projectId_name_idx').on(table.projectId, table.name),
    index('events_contactId_idx').on(table.contactId),
    index('events_emailId_idx').on(table.emailId),
    index('events_createdAt_idx').on(table.createdAt),
    index('events_projectId_contactId_name_createdAt_idx').on(
      table.projectId,
      table.contactId,
      table.name,
      table.createdAt,
    ),
    index('events_contactId_name_createdAt_idx').on(table.contactId, table.name, table.createdAt),
    index('events_projectId_name_createdAt_idx').on(table.projectId, table.name, table.createdAt),
    index('events_contactId_createdAt_idx').on(table.contactId, table.createdAt),
    index('events_data_idx').on(table.data),
    foreignKey({
      name: 'events_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'events_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contacts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'events_emailId_fkey',
      columns: [table.emailId],
      foreignColumns: [emails.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const idempotencyKeys = pgTable(
  'idempotency_keys',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    key: text('key').notNull(),
    projectId: text('projectId').notNull(),
    method: text('method').notNull(),
    path: text('path').notNull(),
    statusCode: integer('statusCode'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('idempotency_keys_expiresAt_idx').on(table.expiresAt),
    uniqueIndex('idempotency_keys_projectId_key_key').on(table.projectId, table.key),
    foreignKey({
      name: 'idempotency_keys_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const throttleClaims = pgTable(
  'throttle_claims',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    projectId: text('projectId').notNull(),
    key: text('key').notNull(),
    recipientKey: text('recipientKey').notNull(),
    contactId: text('contactId'),
    emailId: text('emailId'),
    workflowId: text('workflowId'),
    windowSeconds: integer('windowSeconds'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('throttle_claims_emailId_idx').on(table.emailId),
    index('throttle_claims_workflowId_idx').on(table.workflowId),
    index('throttle_claims_contactId_idx').on(table.contactId),
    index('throttle_claims_expiresAt_idx').on(table.expiresAt),
    uniqueIndex('throttle_claims_projectId_key_recipientKey_key').on(
      table.projectId,
      table.key,
      table.recipientKey,
    ),
    foreignKey({
      name: 'throttle_claims_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'throttle_claims_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contacts.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'throttle_claims_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflows.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiRequests = pgTable(
  'api_requests',
  {
    id: text('id').primaryKey(),
    method: text('method').notNull(),
    path: text('path').notNull(),
    statusCode: integer('statusCode').notNull(),
    duration: integer('duration').notNull(),
    projectId: text('projectId'),
    userId: text('userId'),
    authType: text('authType'),
    ip: text('ip'),
    userAgent: text('userAgent'),
    errorCode: text('errorCode'),
    errorMessage: text('errorMessage'),
    requestSize: integer('requestSize'),
    responseSize: integer('responseSize'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('api_requests_projectId_createdAt_idx').on(table.projectId, table.createdAt),
    index('api_requests_userId_createdAt_idx').on(table.userId, table.createdAt),
    index('api_requests_statusCode_createdAt_idx').on(table.statusCode, table.createdAt),
    index('api_requests_path_createdAt_idx').on(table.path, table.createdAt),
    index('api_requests_createdAt_idx').on(table.createdAt),
    index('api_requests_projectId_statusCode_createdAt_idx').on(
      table.projectId,
      table.statusCode,
      table.createdAt,
    ),
    foreignKey({
      name: 'api_requests_projectId_fkey',
      columns: [table.projectId],
      foreignColumns: [projects.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    users,
    projects,
    memberships,
    domains,
    contacts,
    templates,
    segments,
    segmentMemberships,
    campaigns,
    workflows,
    workflowSteps,
    workflowTransitions,
    workflowExecutions,
    workflowStepExecutions,
    emails,
    events,
    idempotencyKeys,
    throttleClaims,
    apiRequests,
  },
  (r) => ({
    users: { memberships: r.many.memberships() },
    projects: {
      members: r.many.memberships(),
      contacts: r.many.contacts(),
      templates: r.many.templates(),
      segments: r.many.segments(),
      workflows: r.many.workflows(),
      campaigns: r.many.campaigns(),
      emails: r.many.emails(),
      events: r.many.events(),
      domains: r.many.domains(),
      apiRequests: r.many.apiRequests(),
      idempotencyKeys: r.many.idempotencyKeys(),
      throttleClaims: r.many.throttleClaims(),
    },
    memberships: {
      user: r.one.users({ from: r.memberships.userId, to: r.users.id, optional: false }),
      project: r.one.projects({
        from: r.memberships.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    domains: {
      project: r.one.projects({ from: r.domains.projectId, to: r.projects.id, optional: false }),
    },
    contacts: {
      project: r.one.projects({ from: r.contacts.projectId, to: r.projects.id, optional: false }),
      emails: r.many.emails(),
      workflowExecutions: r.many.workflowExecutions(),
      events: r.many.events(),
      segmentMemberships: r.many.segmentMemberships(),
      throttleClaims: r.many.throttleClaims(),
    },
    templates: {
      project: r.one.projects({ from: r.templates.projectId, to: r.projects.id, optional: false }),
      emails: r.many.emails(),
      workflowSteps: r.many.workflowSteps(),
    },
    segments: {
      project: r.one.projects({ from: r.segments.projectId, to: r.projects.id, optional: false }),
      memberships: r.many.segmentMemberships(),
      campaigns: r.many.campaigns(),
    },
    segmentMemberships: {
      contact: r.one.contacts({
        from: r.segmentMemberships.contactId,
        to: r.contacts.id,
        optional: false,
      }),
      segment: r.one.segments({
        from: r.segmentMemberships.segmentId,
        to: r.segments.id,
        optional: false,
      }),
    },
    campaigns: {
      segment: r.one.segments({ from: r.campaigns.segmentId, to: r.segments.id }),
      project: r.one.projects({ from: r.campaigns.projectId, to: r.projects.id, optional: false }),
      emails: r.many.emails(),
    },
    workflows: {
      project: r.one.projects({ from: r.workflows.projectId, to: r.projects.id, optional: false }),
      steps: r.many.workflowSteps(),
      executions: r.many.workflowExecutions(),
      throttleClaims: r.many.throttleClaims(),
    },
    workflowSteps: {
      workflow: r.one.workflows({
        from: r.workflowSteps.workflowId,
        to: r.workflows.id,
        optional: false,
      }),
      template: r.one.templates({ from: r.workflowSteps.templateId, to: r.templates.id }),
      outgoingTransitions: r.many.workflowTransitions({ alias: 'FromStep' }),
      incomingTransitions: r.many.workflowTransitions({ alias: 'ToStep' }),
      executions: r.many.workflowStepExecutions(),
      currentWorkflowExecutions: r.many.workflowExecutions(),
    },
    workflowTransitions: {
      fromStep: r.one.workflowSteps({
        from: r.workflowTransitions.fromStepId,
        to: r.workflowSteps.id,
        optional: false,
        alias: 'FromStep',
      }),
      toStep: r.one.workflowSteps({
        from: r.workflowTransitions.toStepId,
        to: r.workflowSteps.id,
        optional: false,
        alias: 'ToStep',
      }),
    },
    workflowExecutions: {
      workflow: r.one.workflows({
        from: r.workflowExecutions.workflowId,
        to: r.workflows.id,
        optional: false,
      }),
      contact: r.one.contacts({
        from: r.workflowExecutions.contactId,
        to: r.contacts.id,
        optional: false,
      }),
      currentStep: r.one.workflowSteps({
        from: r.workflowExecutions.currentStepId,
        to: r.workflowSteps.id,
      }),
      stepExecutions: r.many.workflowStepExecutions(),
      emails: r.many.emails(),
    },
    workflowStepExecutions: {
      execution: r.one.workflowExecutions({
        from: r.workflowStepExecutions.executionId,
        to: r.workflowExecutions.id,
        optional: false,
      }),
      step: r.one.workflowSteps({
        from: r.workflowStepExecutions.stepId,
        to: r.workflowSteps.id,
        optional: false,
      }),
      emails: r.many.emails(),
    },
    emails: {
      contact: r.one.contacts({ from: r.emails.contactId, to: r.contacts.id, optional: false }),
      template: r.one.templates({ from: r.emails.templateId, to: r.templates.id }),
      campaign: r.one.campaigns({ from: r.emails.campaignId, to: r.campaigns.id }),
      workflowExecution: r.one.workflowExecutions({
        from: r.emails.workflowExecutionId,
        to: r.workflowExecutions.id,
      }),
      workflowStepExecution: r.one.workflowStepExecutions({
        from: r.emails.workflowStepExecutionId,
        to: r.workflowStepExecutions.id,
      }),
      project: r.one.projects({ from: r.emails.projectId, to: r.projects.id, optional: false }),
      events: r.many.events(),
    },
    events: {
      project: r.one.projects({ from: r.events.projectId, to: r.projects.id, optional: false }),
      contact: r.one.contacts({ from: r.events.contactId, to: r.contacts.id }),
      email: r.one.emails({ from: r.events.emailId, to: r.emails.id }),
    },
    idempotencyKeys: {
      project: r.one.projects({
        from: r.idempotencyKeys.projectId,
        to: r.projects.id,
        optional: false,
      }),
    },
    throttleClaims: {
      project: r.one.projects({
        from: r.throttleClaims.projectId,
        to: r.projects.id,
        optional: false,
      }),
      contact: r.one.contacts({ from: r.throttleClaims.contactId, to: r.contacts.id }),
      workflow: r.one.workflows({ from: r.throttleClaims.workflowId, to: r.workflows.id }),
    },
    apiRequests: { project: r.one.projects({ from: r.apiRequests.projectId, to: r.projects.id }) },
  }),
)

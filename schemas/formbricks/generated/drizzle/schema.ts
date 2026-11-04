import {
  bigint,
  boolean,
  customType,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  numeric,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  unique,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'
import { createId } from '@paralleldrive/cuid2'

const utcDate = customType<{ data: Date; driverData: string }>({
  dataType: () => 'date',
  toDriver: (value) => value.toISOString().slice(0, 10),
  fromDriver: (value) => new Date(value),
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

export const creativeTypeEnum = pgEnum('CreativeType', ['feedbackButton', 'banner'])

export const creativeStatusEnum = pgEnum('CreativeStatus', [
  'draft',
  'inProgress',
  'paused',
  'completed',
])

export const creativeShowOnEnum = pgEnum('CreativeShowOn', ['pageView', 'action'])

export const creativeDeviceTypeEnum = pgEnum('CreativeDeviceType', ['phone', 'tablet', 'desktop'])

export const pipelineTriggersEnum = pgEnum('PipelineTriggers', [
  'responseCreated',
  'responseUpdated',
  'responseFinished',
])

export const webhookSourceEnum = pgEnum('WebhookSource', [
  'user',
  'zapier',
  'make',
  'n8n',
  'activepieces',
])

export const contactAttributeTypeEnum = pgEnum('ContactAttributeType', ['default', 'custom'])

export const contactAttributeDataTypeEnum = pgEnum('ContactAttributeDataType', [
  'string',
  'number',
  'date',
])

export const responseOutcomeEnum = pgEnum('ResponseOutcome', ['lowQuality'])

export const surveyStatusEnum = pgEnum('SurveyStatus', [
  'draft',
  'inProgress',
  'paused',
  'completed',
])

export const surveyAttributeFilterConditionEnum = pgEnum('SurveyAttributeFilterCondition', [
  'equals',
  'notEquals',
])

export const surveyQuotaActionEnum = pgEnum('SurveyQuotaAction', ['endSurvey', 'continueSurvey'])

export const responseQuotaLinkStatusEnum = pgEnum('ResponseQuotaLinkStatus', [
  'screenedIn',
  'screenedOut',
])

export const surveyTypeEnum = pgEnum('SurveyType', ['link', 'app'])

export const displayOptionsEnum = pgEnum('displayOptions', [
  'displayOnce',
  'displayMultiple',
  'displaySome',
  'respondMultiple',
])

export const surveyVisibilityEnum = pgEnum('SurveyVisibility', ['restricted', 'workspace'])

export const surveyScriptModeEnum = pgEnum('SurveyScriptMode', ['add', 'replace'])

export const actionTypeEnum = pgEnum('ActionType', ['code', 'noCode'])

export const integrationTypeEnum = pgEnum('IntegrationType', [
  'googleSheets',
  'notion',
  'airtable',
  'slack',
])

export const dataMigrationStatusEnum = pgEnum('DataMigrationStatus', [
  'pending',
  'applied',
  'failed',
])

export const widgetPlacementEnum = pgEnum('WidgetPlacement', [
  'bottomLeft',
  'bottomRight',
  'topLeft',
  'topRight',
  'center',
])

export const surveyOverlayEnum = pgEnum('SurveyOverlay', ['none', 'light', 'dark'])

export const bootstrapAdminMarkerEnum = pgEnum('BootstrapAdminMarker', ['bootstrapAdmin'])

export const organizationRoleEnum = pgEnum('OrganizationRole', [
  'owner',
  'manager',
  'member',
  'billing',
])

export const apiKeyPermissionEnum = pgEnum('ApiKeyPermission', ['read', 'write', 'manage'])

export const identityProviderEnum = pgEnum('IdentityProvider', [
  'email',
  'github',
  'google',
  'azuread',
  'openid',
  'saml',
])

export const embeddedDataSourceEnum = pgEnum('EmbeddedDataSource', [
  'computed',
  'ingested',
  'reserved',
])

export const embeddedDataTypeEnum = pgEnum('EmbeddedDataType', [
  'string',
  'number',
  'boolean',
  'date',
])

export const teamUserRoleEnum = pgEnum('TeamUserRole', ['admin', 'contributor'])

export const workspaceTeamPermissionEnum = pgEnum('WorkspaceTeamPermission', [
  'read',
  'readWrite',
  'manage',
])

export const chartTypeEnum = pgEnum('ChartType', ['area', 'bar', 'pie', 'big_number', 'matrix'])

export const feedbackSourceTypeEnum = pgEnum('FeedbackSourceType', ['formbricks_survey', 'csv'])

export const feedbackSourceStatusEnum = pgEnum('FeedbackSourceStatus', [
  'active',
  'paused',
  'error',
])

export const feedbackSourceImportModeEnum = pgEnum('FeedbackSourceImportMode', [
  'completedOnly',
  'all',
])

export const feedbackSourceElementScopeEnum = pgEnum('FeedbackSourceElementScope', [
  'all',
  'specific',
])

export const hubFieldTypeEnum = pgEnum('HubFieldType', [
  'text',
  'categorical',
  'nps',
  'csat',
  'ces',
  'rating',
  'number',
  'boolean',
  'date',
])

export const deletionCleanupKindEnum = pgEnum('DeletionCleanupKind', [
  'hubSurvey',
  'hubResponses',
  'storageFiles',
  'storageSurveyFolder',
  'hubResponseOutcome',
])

export const retentionEntityEnum = pgEnum('RetentionEntity', ['responses', 'surveys', 'members'])

export const retentionSurveyConditionEnum = pgEnum('RetentionSurveyCondition', [
  'noResponse',
  'noChange',
  'createdBefore',
])

export const retentionRunKindEnum = pgEnum('RetentionRunKind', ['sweep', 'purge'])

export const retentionExemptionSourceEnum = pgEnum('RetentionExemptionSource', [
  'manual',
  'restore',
])

export const retentionTargetTypeEnum = pgEnum('RetentionTargetType', ['survey', 'user'])

export const retentionRunItemActionEnum = pgEnum('RetentionRunItemAction', [
  'notified',
  'archived',
  'deactivated',
  'deleted',
  'skipped',
])

export const retentionSkipReasonEnum = pgEnum('RetentionSkipReason', [
  'exempt',
  'lastOwner',
  'otherOrganization',
  'noRecipient',
])

export const workflowStatusEnum = pgEnum('WorkflowStatus', [
  'draft',
  'enabled',
  'disabled',
  'archived',
])

export const workflowRunStatusEnum = pgEnum('WorkflowRunStatus', [
  'queued',
  'running',
  'completed',
  'failed',
  'canceled',
])

export const workflowRunLogStatusEnum = pgEnum('WorkflowRunLogStatus', [
  'pending',
  'running',
  'succeeded',
  'failed',
  'skipped',
])

export const creative = pgTable(
  'Creative',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    type: creativeTypeEnum('type').notNull(),
    status: creativeStatusEnum('status').notNull().default('draft'),
    publishedAt: timestamp('publishedAt', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    workspaceId: text('workspaceId').notNull(),
    createdBy: text('createdBy'),
    content: jsonb('content').notNull(),
    styling: jsonb('styling').notNull(),
    surveyId: text('surveyId'),
    linkUrl: text('linkUrl'),
    showOn: creativeShowOnEnum('showOn').notNull().default('pageView'),
    showConditions: jsonb('showConditions'),
    displayPercentage: doublePrecision('displayPercentage'),
    startAt: timestamp('startAt', { precision: 3 }),
    endAt: timestamp('endAt', { precision: 3 }),
    autoSelectLanguage: boolean('autoSelectLanguage'),
  },
  (table) => [
    index('Creative_workspaceId_updated_at_idx').on(table.workspaceId, table.updatedAt),
    index('Creative_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    index('Creative_workspaceId_name_idx').on(table.workspaceId, table.name),
    index('Creative_surveyId_idx').on(table.surveyId),
    foreignKey({
      name: 'Creative_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Creative_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Creative_surveyId_workspaceId_fkey',
      columns: [table.surveyId, table.workspaceId],
      foreignColumns: [survey.id, survey.workspaceId],
    })
      .onDelete('no action')
      .onUpdate('no action'),
  ],
)

export const creativeDailyStat = pgTable(
  'CreativeDailyStat',
  {
    creativeId: text('creativeId').notNull(),
    date: utcDate('date').notNull(),
    deviceType: creativeDeviceTypeEnum('deviceType').notNull(),
    impressions: integer('impressions').notNull().default(0),
    clicks: integer('clicks').notNull().default(0),
  },
  (table) => [
    primaryKey({
      name: 'CreativeDailyStat_pkey',
      columns: [table.creativeId, table.date, table.deviceType],
    }),
    foreignKey({
      name: 'CreativeDailyStat_creativeId_fkey',
      columns: [table.creativeId],
      foreignColumns: [creative.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creativeDailyPageStat = pgTable(
  'CreativeDailyPageStat',
  {
    creativeId: text('creativeId').notNull(),
    date: utcDate('date').notNull(),
    page: text('page').notNull(),
    impressions: integer('impressions').notNull().default(0),
    clicks: integer('clicks').notNull().default(0),
  },
  (table) => [
    primaryKey({
      name: 'CreativeDailyPageStat_pkey',
      columns: [table.creativeId, table.date, table.page],
    }),
    foreignKey({
      name: 'CreativeDailyPageStat_creativeId_fkey',
      columns: [table.creativeId],
      foreignColumns: [creative.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creativeTrigger = pgTable(
  'CreativeTrigger',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    creativeId: text('creativeId').notNull(),
    actionClassId: text('actionClassId').notNull(),
  },
  (table) => [
    index('CreativeTrigger_actionClassId_idx').on(table.actionClassId),
    uniqueIndex('CreativeTrigger_creativeId_actionClassId_key').on(
      table.creativeId,
      table.actionClassId,
    ),
    foreignKey({
      name: 'CreativeTrigger_creativeId_fkey',
      columns: [table.creativeId],
      foreignColumns: [creative.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CreativeTrigger_actionClassId_fkey',
      columns: [table.actionClassId],
      foreignColumns: [actionClass.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const creativeLanguage = pgTable(
  'CreativeLanguage',
  {
    languageId: text('languageId').notNull(),
    creativeId: text('creativeId').notNull(),
    default: boolean('default').notNull().default(false),
    enabled: boolean('enabled').notNull().default(true),
  },
  (table) => [
    primaryKey({ name: 'CreativeLanguage_pkey', columns: [table.languageId, table.creativeId] }),
    index('CreativeLanguage_creativeId_idx').on(table.creativeId),
    foreignKey({
      name: 'CreativeLanguage_languageId_fkey',
      columns: [table.languageId],
      foreignColumns: [language.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'CreativeLanguage_creativeId_fkey',
      columns: [table.creativeId],
      foreignColumns: [creative.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const outboundEmailAudit = pgTable(
  'OutboundEmailAudit',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    contentExpiresAt: timestamp('contentExpiresAt', { precision: 3 }).notNull(),
    environment: text('environment').notNull(),
    emailType: text('emailType').notNull(),
    trigger: text('trigger').notNull(),
    actorKind: text('actorKind').notNull(),
    actorUserId: text('actorUserId'),
    configuredByUserId: text('configuredByUserId'),
    actorUserHash: text('actorUserHash'),
    configuredByUserHash: text('configuredByUserHash'),
    organizationId: text('organizationId'),
    surveyId: text('surveyId'),
    responseId: text('responseId'),
    followUpId: text('followUpId'),
    workflowId: text('workflowId'),
    workflowRunId: text('workflowRunId'),
    inviteId: text('inviteId'),
    recipientMac: text('recipientMac'),
    sesMessageHash: text('sesMessageHash'),
    snapshotKey: text('snapshotKey'),
    snapshotSha256: text('snapshotSha256'),
    status: text('status').notNull().default('prepared'),
    statusRecordedAt: timestamp('statusRecordedAt', { precision: 3 }),
    auditGap: boolean('auditGap').notNull().default(false),
    erasureRequestedAt: timestamp('erasureRequestedAt', { precision: 3 }),
    erasedAt: timestamp('erasedAt', { precision: 3 }),
    contentDeleteRetryAt: timestamp('contentDeleteRetryAt', { precision: 3 }),
    contentDeleteFailures: integer('contentDeleteFailures').notNull().default(0),
  },
  (table) => [
    index('OutboundEmailAudit_environment_createdAt_id_idx').on(
      table.environment,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_organizationId_createdAt_id_idx').on(
      table.organizationId,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_surveyId_createdAt_id_idx').on(
      table.surveyId,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_responseId_createdAt_id_idx').on(
      table.responseId,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_actorUserId_createdAt_id_idx').on(
      table.actorUserId,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_configuredByUserId_createdAt_id_idx').on(
      table.configuredByUserId,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_actorUserHash_createdAt_id_idx').on(
      table.actorUserHash,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_configuredByUserHash_createdAt_id_idx').on(
      table.configuredByUserHash,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_recipientMac_createdAt_id_idx').on(
      table.recipientMac,
      table.createdAt,
      table.id,
    ),
    index('OutboundEmailAudit_sesMessageHash_idx').on(table.sesMessageHash),
    index('OutboundEmailAudit_status_createdAt_id_idx').on(table.status, table.createdAt, table.id),
    index('OutboundEmailAudit_contentExpiresAt_id_idx').on(table.contentExpiresAt, table.id),
    index('OutboundEmailAudit_expiresAt_id_idx').on(table.expiresAt, table.id),
    index('OutboundEmailAudit_erasureRequestedAt_id_idx').on(table.erasureRequestedAt, table.id),
    index('OutboundEmailAudit_contentDeleteRetryAt_id_idx').on(
      table.contentDeleteRetryAt,
      table.id,
    ),
  ],
)

export const outboundEmailAuditErasureIntent = pgTable(
  'OutboundEmailAuditErasureIntent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    userId: text('userId'),
    userHash: text('userHash').notNull(),
    recipientMac: text('recipientMac').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    activatedAt: timestamp('activatedAt', { precision: 3 }),
    checkedAt: timestamp('checkedAt', { precision: 3 }),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  },
  (table) => [
    index('OutboundEmailAuditErasureIntent_userId_idx').on(table.userId),
    index('OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx').on(
      table.recipientMac,
      table.activatedAt,
    ),
    index('OutboundEmailAuditErasureIntent_userHash_activatedAt_idx').on(
      table.userHash,
      table.activatedAt,
    ),
    index('OutboundEmailAuditErasureIntent_checkedAt_id_idx').on(table.checkedAt, table.id),
    index('OutboundEmailAuditErasureIntent_expiresAt_idx').on(table.expiresAt),
  ],
)

export const webhook = pgTable(
  'Webhook',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    url: text('url').notNull(),
    source: webhookSourceEnum('source').notNull().default('user'),
    workspaceId: text('workspaceId').notNull(),
    triggers: pipelineTriggersEnum('triggers').array(),
    surveyIds: text('surveyIds').array(),
    secret: text('secret'),
  },
  (table) => [
    index('Webhook_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'Webhook_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const contactAttribute = pgTable(
  'ContactAttribute',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    attributeKeyId: text('attributeKeyId').notNull(),
    contactId: text('contactId').notNull(),
    value: text('value').notNull(),
    valueNumber: doublePrecision('valueNumber'),
    valueDate: timestamp('valueDate', { precision: 3 }),
  },
  (table) => [
    index('ContactAttribute_attributeKeyId_value_idx').on(table.attributeKeyId, table.value),
    index('ContactAttribute_attributeKeyId_valueNumber_idx').on(
      table.attributeKeyId,
      table.valueNumber,
    ),
    index('ContactAttribute_attributeKeyId_valueDate_idx').on(
      table.attributeKeyId,
      table.valueDate,
    ),
    uniqueIndex('ContactAttribute_contactId_attributeKeyId_key').on(
      table.contactId,
      table.attributeKeyId,
    ),
    foreignKey({
      name: 'ContactAttribute_attributeKeyId_fkey',
      columns: [table.attributeKeyId],
      foreignColumns: [contactAttributeKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ContactAttribute_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contact.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const contactAttributeKey = pgTable(
  'ContactAttributeKey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    isUnique: boolean('isUnique').notNull().default(false),
    key: text('key').notNull(),
    name: text('name'),
    description: text('description'),
    type: contactAttributeTypeEnum('type').notNull().default('custom'),
    dataType: contactAttributeDataTypeEnum('dataType').notNull().default('string'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('ContactAttributeKey_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    uniqueIndex('ContactAttributeKey_key_workspaceId_key').on(table.key, table.workspaceId),
    foreignKey({
      name: 'ContactAttributeKey_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const contact = pgTable(
  'Contact',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('Contact_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'Contact_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const response = pgTable(
  'Response',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    finished: boolean('finished').notNull().default(false),
    surveyId: text('surveyId').notNull(),
    contactId: text('contactId'),
    endingId: text('endingId'),
    data: jsonb('data').notNull().default({}),
    variables: jsonb('variables').notNull().default({}),
    ttc: jsonb('ttc').notNull().default({}),
    meta: jsonb('meta').notNull().default({}),
    ingestFlags: jsonb('ingest_flags'),
    outcome: responseOutcomeEnum('outcome'),
    contactAttributes: jsonb('contactAttributes'),
    singleUseId: text('singleUseId'),
    language: text('language'),
    displayId: text('displayId'),
  },
  (table) => [
    uniqueIndex('Response_displayId_key').on(table.displayId),
    index('Response_created_at_idx').on(table.createdAt),
    index('Response_created_at_id_idx').on(table.createdAt, table.id),
    index('Response_surveyId_created_at_idx').on(table.surveyId, table.createdAt),
    index('Response_surveyId_created_at_id_idx').on(table.surveyId, table.createdAt, table.id),
    index('Response_contactId_created_at_idx').on(table.contactId, table.createdAt),
    index('Response_contactId_created_at_id_idx').on(table.contactId, table.createdAt, table.id),
    index('Response_surveyId_finished_idx').on(table.surveyId, table.finished),
    uniqueIndex('Response_surveyId_singleUseId_key').on(table.surveyId, table.singleUseId),
    foreignKey({
      name: 'Response_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Response_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contact.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Response_displayId_fkey',
      columns: [table.displayId],
      foreignColumns: [display.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const tag = pgTable(
  'Tag',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('Tag_workspaceId_idx').on(table.workspaceId),
    uniqueIndex('Tag_workspaceId_name_key').on(table.workspaceId, table.name),
    foreignKey({
      name: 'Tag_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tagsOnResponses = pgTable(
  'TagsOnResponses',
  { responseId: text('responseId').notNull(), tagId: text('tagId').notNull() },
  (table) => [
    primaryKey({ name: 'TagsOnResponses_pkey', columns: [table.responseId, table.tagId] }),
    foreignKey({
      name: 'TagsOnResponses_responseId_fkey',
      columns: [table.responseId],
      foreignColumns: [response.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TagsOnResponses_tagId_fkey',
      columns: [table.tagId],
      foreignColumns: [tag.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const display = pgTable(
  'Display',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    surveyId: text('surveyId').notNull(),
    contactId: text('contactId'),
    creativeId: text('creativeId'),
  },
  (table) => [
    index('Display_surveyId_idx').on(table.surveyId),
    index('Display_contactId_created_at_idx').on(table.contactId, table.createdAt),
    foreignKey({
      name: 'Display_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Display_contactId_fkey',
      columns: [table.contactId],
      foreignColumns: [contact.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveyTrigger = pgTable(
  'SurveyTrigger',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    surveyId: text('surveyId').notNull(),
    actionClassId: text('actionClassId').notNull(),
  },
  (table) => [
    uniqueIndex('SurveyTrigger_surveyId_actionClassId_key').on(table.surveyId, table.actionClassId),
    foreignKey({
      name: 'SurveyTrigger_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SurveyTrigger_actionClassId_fkey',
      columns: [table.actionClassId],
      foreignColumns: [actionClass.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveyAttributeFilter = pgTable(
  'SurveyAttributeFilter',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    attributeKeyId: text('attributeKeyId').notNull(),
    surveyId: text('surveyId').notNull(),
    condition: surveyAttributeFilterConditionEnum('condition').notNull(),
    value: text('value').notNull(),
  },
  (table) => [
    index('SurveyAttributeFilter_attributeKeyId_idx').on(table.attributeKeyId),
    uniqueIndex('SurveyAttributeFilter_surveyId_attributeKeyId_key').on(
      table.surveyId,
      table.attributeKeyId,
    ),
    foreignKey({
      name: 'SurveyAttributeFilter_attributeKeyId_fkey',
      columns: [table.attributeKeyId],
      foreignColumns: [contactAttributeKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SurveyAttributeFilter_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const survey = pgTable(
  'Survey',
  {
    hiddenFields: jsonb('hiddenFields').notNull().default({ enabled: false }),
    variables: jsonb('variables').notNull().default([]),
    ownershipInitialized: boolean('ownershipInitialized').notNull().default(false),
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    redirectUrl: text('redirectUrl'),
    type: surveyTypeEnum('type').notNull().default('app'),
    workspaceId: text('workspaceId').notNull(),
    createdBy: text('createdBy'),
    visibility: surveyVisibilityEnum('visibility').notNull().default('workspace'),
    ownerId: text('ownerId'),
    visibilityVersion: integer('visibilityVersion').notNull().default(0),
    visibilityProjectedVersion: integer('visibilityProjectedVersion').notNull().default(0),
    visibilityPending: boolean('visibilityPending').notNull().default(false),
    visibilityChangedAt: timestamp('visibilityChangedAt', { precision: 3 }),
    visibilityChangedById: text('visibilityChangedById'),
    status: surveyStatusEnum('status').notNull().default('draft'),
    welcomeCard: jsonb('welcomeCard').notNull().default({ enabled: false }),
    questions: jsonb('questions').notNull().default([]),
    blocks: jsonb('blocks').array().default([]),
    endings: jsonb('endings').array().default([]),
    displayOption: displayOptionsEnum('displayOption').notNull().default('displayOnce'),
    recontactDays: integer('recontactDays'),
    displayLimit: integer('displayLimit'),
    inlineTriggers: jsonb('inlineTriggers'),
    autoClose: integer('autoClose'),
    keepEndingCardOpen: boolean('keepEndingCardOpen').notNull().default(false),
    autoComplete: integer('autoComplete'),
    delay: integer('delay').notNull().default(0),
    publishOn: timestamp('publishOn', { precision: 3 }),
    closeOn: timestamp('closeOn', { precision: 3 }),
    archivedAt: timestamp('archivedAt', { precision: 3 }),
    surveyClosedMessage: jsonb('surveyClosedMessage'),
    segmentId: text('segmentId'),
    workspaceOverwrites: jsonb('workspaceOverwrites'),
    styling: jsonb('styling'),
    singleUse: jsonb('singleUse').default({ enabled: false, isEncrypted: true }),
    isVerifyEmailEnabled: boolean('isVerifyEmailEnabled').notNull().default(false),
    isBackButtonHidden: boolean('isBackButtonHidden').notNull().default(false),
    isAutoProgressingEnabled: boolean('isAutoProgressingEnabled').notNull().default(false),
    isCaptureIpEnabled: boolean('isCaptureIpEnabled').notNull().default(false),
    isAnonymizeResponsesEnabled: boolean('is_anonymize_responses_enabled').notNull().default(false),
    pin: text('pin'),
    displayPercentage: numeric('displayPercentage', { precision: 65, scale: 30 }),
    showLanguageSwitch: boolean('showLanguageSwitch'),
    autoSelectLanguage: boolean('autoSelectLanguage'),
    recaptcha: jsonb('recaptcha').default({ enabled: false, threshold: 0.1 }),
    metadata: jsonb('metadata').notNull().default({}),
    slug: text('slug'),
    customHeadScripts: text('customHeadScripts'),
    customHeadScriptsMode: surveyScriptModeEnum('customHeadScriptsMode').default('add'),
    customCss: jsonb('customCss'),
    showConditions: jsonb('showConditions'),
  },
  (table) => [
    uniqueIndex('Survey_slug_key').on(table.slug),
    index('Survey_workspaceId_updated_at_idx').on(table.workspaceId, table.updatedAt),
    index('Survey_workspaceId_ownerId_idx').on(table.workspaceId, table.ownerId),
    index('Survey_segmentId_idx').on(table.segmentId),
    index('Survey_status_publishOn_idx').on(table.status, table.publishOn),
    index('Survey_status_closeOn_idx').on(table.status, table.closeOn),
    index('Survey_archivedAt_idx').on(table.archivedAt),
    unique('Survey_id_workspaceId_key').on(table.id, table.workspaceId),
    foreignKey({
      name: 'Survey_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Survey_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Survey_ownerId_fkey', columns: [table.ownerId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Survey_segmentId_fkey',
      columns: [table.segmentId],
      foreignColumns: [segment.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const surveyQuota = pgTable(
  'SurveyQuota',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    surveyId: text('surveyId').notNull(),
    name: text('name').notNull(),
    limit: integer('limit').notNull(),
    logic: jsonb('logic').notNull().default({}),
    action: surveyQuotaActionEnum('action').notNull(),
    endingCardId: text('endingCardId'),
    countPartialSubmissions: boolean('countPartialSubmissions').notNull().default(false),
  },
  (table) => [
    uniqueIndex('SurveyQuota_surveyId_name_key').on(table.surveyId, table.name),
    foreignKey({
      name: 'SurveyQuota_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const responseQuotaLink = pgTable(
  'ResponseQuotaLink',
  {
    responseId: text('responseId').notNull(),
    quotaId: text('quotaId').notNull(),
    status: responseQuotaLinkStatusEnum('status').notNull(),
  },
  (table) => [
    primaryKey({ name: 'ResponseQuotaLink_pkey', columns: [table.responseId, table.quotaId] }),
    index('ResponseQuotaLink_quotaId_status_idx').on(table.quotaId, table.status),
    foreignKey({
      name: 'ResponseQuotaLink_responseId_fkey',
      columns: [table.responseId],
      foreignColumns: [response.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ResponseQuotaLink_quotaId_fkey',
      columns: [table.quotaId],
      foreignColumns: [surveyQuota.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveyFollowUp = pgTable(
  'SurveyFollowUp',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    surveyId: text('surveyId').notNull(),
    name: text('name').notNull(),
    trigger: jsonb('trigger').notNull(),
    action: jsonb('action').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'SurveyFollowUp_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const actionClass = pgTable(
  'ActionClass',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    description: text('description'),
    type: actionTypeEnum('type').notNull(),
    key: text('key'),
    noCodeConfig: jsonb('noCodeConfig'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('ActionClass_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    uniqueIndex('ActionClass_key_workspaceId_key').on(table.key, table.workspaceId),
    uniqueIndex('ActionClass_name_workspaceId_key').on(table.name, table.workspaceId),
    foreignKey({
      name: 'ActionClass_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const integration = pgTable(
  'Integration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: integrationTypeEnum('type').notNull(),
    config: jsonb('config').notNull(),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('Integration_workspaceId_idx').on(table.workspaceId),
    uniqueIndex('Integration_type_workspaceId_key').on(table.type, table.workspaceId),
    foreignKey({
      name: 'Integration_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dataMigration = pgTable(
  'DataMigration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    startedAt: timestamp('started_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    finishedAt: timestamp('finished_at', { precision: 3 }),
    name: text('name').notNull(),
    status: dataMigrationStatusEnum('status').notNull(),
  },
  (table) => [uniqueIndex('DataMigration_name_key').on(table.name)],
)

export const workspace = pgTable(
  'Workspace',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    legacyEnvironmentId: text('legacyEnvironmentId'),
    organizationId: text('organizationId').notNull(),
    styling: jsonb('styling').notNull().default({ allowStyleOverwrite: true }),
    config: jsonb('config').notNull().default({}),
    recontactDays: integer('recontactDays').notNull().default(7),
    linkSurveyBranding: boolean('linkSurveyBranding').notNull().default(true),
    inAppSurveyBranding: boolean('inAppSurveyBranding').notNull().default(true),
    placement: widgetPlacementEnum('placement').notNull().default('bottomRight'),
    clickOutsideClose: boolean('clickOutsideClose').notNull().default(true),
    overlay: surveyOverlayEnum('overlay').notNull().default('none'),
    overlayColor: text('overlayColor'),
    overlayOpacity: integer('overlayOpacity'),
    logo: jsonb('logo'),
    appSetupCompleted: boolean('appSetupCompleted').notNull().default(false),
    customHeadScripts: text('customHeadScripts'),
    customCss: jsonb('customCss'),
    customCssPrevious: jsonb('customCssPrevious'),
  },
  (table) => [
    uniqueIndex('Workspace_legacyEnvironmentId_key').on(table.legacyEnvironmentId),
    uniqueIndex('Workspace_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'Workspace_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const organization = pgTable('Organization', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  createdAt: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  name: text('name').notNull(),
  whitelabel: jsonb('whitelabel').notNull().default({}),
  isAISmartToolsEnabled: boolean('isAISmartToolsEnabled').notNull().default(false),
  displayTimeZone: text('displayTimeZone'),
})

export const organizationBilling = pgTable(
  'OrganizationBilling',
  {
    organizationId: text('organization_id').primaryKey(),
    stripeCustomerId: text('stripe_customer_id'),
    limits: jsonb('limits').notNull(),
    usageCycleAnchor: timestamp('usage_cycle_anchor', { precision: 3 }),
    stripe: jsonb('stripe'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('OrganizationBilling_stripe_customer_id_key').on(table.stripeCustomerId),
    foreignKey({
      name: 'OrganizationBilling_organization_id_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const membership = pgTable(
  'Membership',
  {
    organizationId: text('organizationId').notNull(),
    userId: text('userId').notNull(),
    accepted: boolean('accepted').notNull().default(false),
    role: organizationRoleEnum('role').notNull().default('member'),
  },
  (table) => [
    primaryKey({ name: 'Membership_pkey', columns: [table.userId, table.organizationId] }),
    index('Membership_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'Membership_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Membership_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const invite = pgTable(
  'Invite',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    email: text('email').notNull(),
    name: text('name'),
    organizationId: text('organizationId').notNull(),
    creatorId: text('creatorId').notNull(),
    acceptorId: text('acceptorId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    role: organizationRoleEnum('role').notNull().default('member'),
    teamIds: text('teamIds').array().default([]),
  },
  (table) => [
    index('Invite_email_organizationId_idx').on(table.email, table.organizationId),
    index('Invite_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'Invite_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Invite_creatorId_fkey',
      columns: [table.creatorId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Invite_acceptorId_fkey',
      columns: [table.acceptorId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiKey = pgTable(
  'ApiKey',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('createdBy'),
    lastUsedAt: timestamp('lastUsedAt', { precision: 3 }),
    label: text('label').notNull(),
    hashedKey: text('hashedKey').notNull(),
    lookupHash: text('lookupHash'),
    organizationId: text('organizationId').notNull(),
    organizationAccess: jsonb('organizationAccess').notNull().default({}),
  },
  (table) => [
    uniqueIndex('ApiKey_lookupHash_key').on(table.lookupHash),
    index('ApiKey_organizationId_idx').on(table.organizationId),
    foreignKey({
      name: 'ApiKey_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const apiKeyWorkspace = pgTable(
  'ApiKeyWorkspace',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    apiKeyId: text('apiKeyId').notNull(),
    workspaceId: text('workspaceId').notNull(),
    permission: apiKeyPermissionEnum('permission').notNull(),
  },
  (table) => [
    index('ApiKeyWorkspace_workspaceId_idx').on(table.workspaceId),
    uniqueIndex('ApiKeyWorkspace_apiKeyId_workspaceId_key').on(table.apiKeyId, table.workspaceId),
    foreignKey({
      name: 'ApiKeyWorkspace_apiKeyId_fkey',
      columns: [table.apiKeyId],
      foreignColumns: [apiKey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ApiKeyWorkspace_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const account = pgTable(
  'Account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
    type: text('type'),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    access_token: text('access_token'),
    refresh_token: text('refresh_token'),
    expires_at: integer('expires_at'),
    ext_expires_in: integer('ext_expires_in'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
    password: text('password'),
    accessTokenExpiresAt: timestamp('accessTokenExpiresAt', { precision: 3 }),
    refreshTokenExpiresAt: timestamp('refreshTokenExpiresAt', { precision: 3 }),
    issuer: text('issuer'),
  },
  (table) => [
    index('Account_userId_idx').on(table.userId),
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
    uniqueIndex('Account_issuer_providerAccountId_key').on(table.issuer, table.providerAccountId),
    foreignKey({ name: 'Account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = pgTable(
  'Session',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    index('Session_userId_idx').on(table.userId),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verificationToken = pgTable(
  'VerificationToken',
  {
    identifier: text('identifier').notNull(),
    token: text('token').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const twoFactor = pgTable(
  'TwoFactor',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    secret: text('secret').notNull(),
    backupCodes: text('backupCodes').notNull(),
    userId: text('userId').notNull(),
    verified: boolean('verified').notNull().default(true),
    failedVerificationCount: integer('failedVerificationCount').notNull().default(0),
    lockedUntil: timestamp('lockedUntil', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('TwoFactor_userId_key').on(table.userId),
    foreignKey({
      name: 'TwoFactor_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const jwks = pgTable('jwks', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  publicKey: text('publicKey').notNull(),
  privateKey: text('privateKey').notNull(),
  createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }),
  alg: text('alg'),
  crv: text('crv'),
})

export const oauthClient = pgTable(
  'oauthClient',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    clientSecret: text('clientSecret'),
    disabled: boolean('disabled').default(false),
    skipConsent: boolean('skipConsent'),
    enableEndSession: boolean('enableEndSession'),
    subjectType: text('subjectType'),
    scopes: text('scopes').array().default([]),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 }),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    name: text('name'),
    uri: text('uri'),
    icon: text('icon'),
    contacts: text('contacts').array().default([]),
    tos: text('tos'),
    policy: text('policy'),
    softwareId: text('softwareId'),
    softwareVersion: text('softwareVersion'),
    softwareStatement: text('softwareStatement'),
    redirectUris: text('redirectUris').array(),
    postLogoutRedirectUris: text('postLogoutRedirectUris').array().default([]),
    tokenEndpointAuthMethod: text('tokenEndpointAuthMethod'),
    grantTypes: text('grantTypes').array().default([]),
    responseTypes: text('responseTypes').array().default([]),
    public: boolean('public'),
    type: text('type'),
    requirePKCE: boolean('requirePKCE'),
    referenceId: text('referenceId'),
    metadata: jsonb('metadata'),
    clientDiscoveryId: text('clientDiscoveryId'),
    clientCredentialsScopes: text('clientCredentialsScopes').array().default([]),
    backchannelLogoutUri: text('backchannelLogoutUri'),
    backchannelLogoutSessionRequired: boolean('backchannelLogoutSessionRequired'),
    applicationType: text('applicationType'),
    jwks: text('jwks'),
    jwksUri: text('jwksUri'),
    dpopBoundAccessTokens: boolean('dpopBoundAccessTokens').default(false),
  },
  (table) => [
    unique('oauthClient_clientId_key').on(table.clientId),
    index('oauthClient_userId_idx').on(table.userId),
    foreignKey({
      name: 'oauthClient_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthAccessToken = pgTable(
  'oauthAccessToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    clientId: text('clientId').notNull(),
    sessionId: text('sessionId'),
    userId: text('userId'),
    referenceId: text('referenceId'),
    refreshId: text('refreshId'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    scopes: text('scopes').array(),
    authorizationCodeId: text('authorizationCodeId'),
    resources: text('resources').array().default([]),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array().default([]),
    revoked: timestamp('revoked', { precision: 3 }),
    confirmation: jsonb('confirmation'),
  },
  (table) => [
    uniqueIndex('oauthAccessToken_token_key').on(table.token),
    index('oauthAccessToken_clientId_idx').on(table.clientId),
    index('oauthAccessToken_sessionId_idx').on(table.sessionId),
    index('oauthAccessToken_userId_idx').on(table.userId),
    index('oauthAccessToken_refreshId_idx').on(table.refreshId),
    index('oauthAccessToken_authorizationCodeId_idx').on(table.authorizationCodeId),
    foreignKey({
      name: 'oauthAccessToken_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthAccessToken_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthAccessToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthAccessToken_refreshId_fkey',
      columns: [table.refreshId],
      foreignColumns: [oauthRefreshToken.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthRefreshToken = pgTable(
  'oauthRefreshToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    clientId: text('clientId').notNull(),
    sessionId: text('sessionId'),
    userId: text('userId').notNull(),
    referenceId: text('referenceId'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    revoked: timestamp('revoked', { precision: 3 }),
    authTime: timestamp('authTime', { precision: 3 }),
    scopes: text('scopes').array(),
    authorizationCodeId: text('authorizationCodeId'),
    resources: text('resources').array().default([]),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array().default([]),
    rotatedAt: timestamp('rotatedAt', { precision: 3 }),
    rotationReplayResponse: text('rotationReplayResponse'),
    rotationReplayExpiresAt: timestamp('rotationReplayExpiresAt', { precision: 3 }),
    confirmation: jsonb('confirmation'),
  },
  (table) => [
    uniqueIndex('oauthRefreshToken_token_key').on(table.token),
    index('oauthRefreshToken_clientId_idx').on(table.clientId),
    index('oauthRefreshToken_sessionId_idx').on(table.sessionId),
    index('oauthRefreshToken_userId_idx').on(table.userId),
    index('oauthRefreshToken_authorizationCodeId_idx').on(table.authorizationCodeId),
    foreignKey({
      name: 'oauthRefreshToken_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthRefreshToken_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [session.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthRefreshToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthConsent = pgTable(
  'oauthConsent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    userId: text('userId'),
    referenceId: text('referenceId'),
    scopes: text('scopes').array(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    resources: text('resources').array().default([]),
    requestedUserInfoClaims: text('requestedUserInfoClaims').array().default([]),
  },
  (table) => [
    index('oauthConsent_clientId_idx').on(table.clientId),
    index('oauthConsent_userId_idx').on(table.userId),
    foreignKey({
      name: 'oauthConsent_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthConsent_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthResource = pgTable(
  'oauthResource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    identifier: text('identifier').notNull(),
    name: text('name').notNull(),
    accessTokenTtl: integer('accessTokenTtl'),
    refreshTokenTtl: integer('refreshTokenTtl'),
    signingAlgorithm: text('signingAlgorithm'),
    signingKeyId: text('signingKeyId'),
    allowedScopes: text('allowedScopes').array().default([]),
    customClaims: jsonb('customClaims'),
    dpopBoundAccessTokensRequired: boolean('dpopBoundAccessTokensRequired').default(false),
    disabled: boolean('disabled').default(false),
    createdAt: timestamp('createdAt', { precision: 3 }),
    updatedAt: timestamp('updatedAt', { precision: 3 }),
    policyVersion: integer('policyVersion').default(1),
    metadata: jsonb('metadata'),
  },
  (table) => [unique('oauthResource_identifier_key').on(table.identifier)],
)

export const oauthClientResource = pgTable(
  'oauthClientResource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    resourceId: text('resourceId').notNull(),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 }),
  },
  (table) => [
    index('oauthClientResource_resourceId_idx').on(table.resourceId),
    uniqueIndex('oauthClientResource_clientId_resourceId_key').on(table.clientId, table.resourceId),
    foreignKey({
      name: 'oauthClientResource_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthClient.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauthClientResource_resourceId_fkey',
      columns: [table.resourceId],
      foreignColumns: [oauthResource.identifier],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthClientAssertion = pgTable(
  'oauthClientAssertion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  },
  (table) => [index('oauthClientAssertion_expiresAt_idx').on(table.expiresAt)],
)

export const passwordResetToken = pgTable(
  'PasswordResetToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    tokenHash: text('token_hash').notNull(),
    expiresAt: timestamp('expires_at', { precision: 3 }).notNull(),
    userId: text('userId').notNull(),
  },
  (table) => [
    uniqueIndex('PasswordResetToken_token_hash_key').on(table.tokenHash),
    uniqueIndex('PasswordResetToken_userId_key').on(table.userId),
    index('PasswordResetToken_expires_at_idx').on(table.expiresAt),
    foreignKey({
      name: 'PasswordResetToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const user = pgTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    email: text('email').notNull(),
    emailVerified: boolean('email_verified').notNull().default(false),
    twoFactorSecret: text('twoFactorSecret'),
    twoFactorEnabled: boolean('twoFactorEnabled').notNull().default(false),
    backupCodes: text('backupCodes'),
    password: text('password'),
    identityProvider: identityProviderEnum('identityProvider').notNull().default('email'),
    identityProviderAccountId: text('identityProviderAccountId'),
    groupId: text('groupId'),
    notificationSettings: jsonb('notificationSettings').notNull().default({}),
    locale: text('locale').notNull().default('en-US'),
    lastLoginAt: timestamp('lastLoginAt', { precision: 3 }),
    isActive: boolean('isActive').notNull().default(true),
    reactivatedAt: timestamp('reactivatedAt', { precision: 3 }),
    lastActiveAt: timestamp('lastActiveAt', { precision: 3 }),
    isBootstrapAdmin: bootstrapAdminMarkerEnum('isBootstrapAdmin'),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    uniqueIndex('User_isBootstrapAdmin_key').on(table.isBootstrapAdmin),
  ],
)

export const segment = pgTable(
  'Segment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    title: text('title').notNull(),
    description: text('description'),
    isPrivate: boolean('isPrivate').notNull().default(true),
    filters: jsonb('filters').notNull().default([]),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('Segment_workspaceId_idx').on(table.workspaceId),
    uniqueIndex('Segment_workspaceId_title_key').on(table.workspaceId, table.title),
    foreignKey({
      name: 'Segment_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const language = pgTable(
  'Language',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    code: text('code').notNull(),
    alias: text('alias'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    uniqueIndex('Language_workspaceId_code_key').on(table.workspaceId, table.code),
    foreignKey({
      name: 'Language_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveyLanguage = pgTable(
  'SurveyLanguage',
  {
    languageId: text('languageId').notNull(),
    surveyId: text('surveyId').notNull(),
    default: boolean('default').notNull().default(false),
    enabled: boolean('enabled').notNull().default(true),
  },
  (table) => [
    primaryKey({ name: 'SurveyLanguage_pkey', columns: [table.languageId, table.surveyId] }),
    index('SurveyLanguage_surveyId_idx').on(table.surveyId),
    foreignKey({
      name: 'SurveyLanguage_languageId_fkey',
      columns: [table.languageId],
      foreignColumns: [language.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SurveyLanguage_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const embeddedData = pgTable(
  'EmbeddedData',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    key: text('key'),
    name: text('name').notNull(),
    description: text('description'),
    source: embeddedDataSourceEnum('source').notNull(),
    dataType: embeddedDataTypeEnum('dataType').notNull().default('string'),
    defaultValue: jsonb('defaultValue'),
    locked: boolean('locked').notNull().default(false),
    surveyId: text('surveyId'),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    index('EmbeddedData_surveyId_idx').on(table.surveyId),
    uniqueIndex('EmbeddedData_workspaceId_key_key').on(table.workspaceId, table.key),
    unique('EmbeddedData_id_workspaceId_key').on(table.id, table.workspaceId),
    foreignKey({
      name: 'EmbeddedData_surveyId_workspaceId_fkey',
      columns: [table.surveyId, table.workspaceId],
      foreignColumns: [survey.id, survey.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'EmbeddedData_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const surveyEmbeddedData = pgTable(
  'SurveyEmbeddedData',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    workspaceId: text('workspaceId').notNull(),
    surveyId: text('surveyId').notNull(),
    embeddedDataId: text('embeddedDataId').notNull(),
    storageKey: text('storageKey').notNull(),
    order: integer('order').notNull(),
  },
  (table) => [
    index('SurveyEmbeddedData_embeddedDataId_idx').on(table.embeddedDataId),
    index('SurveyEmbeddedData_surveyId_order_idx').on(table.surveyId, table.order),
    uniqueIndex('SurveyEmbeddedData_surveyId_embeddedDataId_key').on(
      table.surveyId,
      table.embeddedDataId,
    ),
    uniqueIndex('SurveyEmbeddedData_surveyId_storageKey_key').on(table.surveyId, table.storageKey),
    foreignKey({
      name: 'SurveyEmbeddedData_surveyId_workspaceId_fkey',
      columns: [table.surveyId, table.workspaceId],
      foreignColumns: [survey.id, survey.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'SurveyEmbeddedData_embeddedDataId_workspaceId_fkey',
      columns: [table.embeddedDataId, table.workspaceId],
      foreignColumns: [embeddedData.id, embeddedData.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const team = pgTable(
  'Team',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    organizationId: text('organizationId').notNull(),
  },
  (table) => [
    uniqueIndex('Team_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'Team_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const teamUser = pgTable(
  'TeamUser',
  {
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    teamId: text('teamId').notNull(),
    userId: text('userId').notNull(),
    role: teamUserRoleEnum('role').notNull(),
  },
  (table) => [
    primaryKey({ name: 'TeamUser_pkey', columns: [table.teamId, table.userId] }),
    index('TeamUser_userId_idx').on(table.userId),
    foreignKey({ name: 'TeamUser_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'TeamUser_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workspaceTeam = pgTable(
  'WorkspaceTeam',
  {
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    workspaceId: text('workspaceId').notNull(),
    teamId: text('teamId').notNull(),
    permission: workspaceTeamPermissionEnum('permission').notNull().default('read'),
  },
  (table) => [
    primaryKey({ name: 'WorkspaceTeam_pkey', columns: [table.workspaceId, table.teamId] }),
    index('WorkspaceTeam_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'WorkspaceTeam_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkspaceTeam_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chart = pgTable(
  'Chart',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    type: chartTypeEnum('type').notNull(),
    workspaceId: text('workspaceId').notNull(),
    query: jsonb('query').notNull().default({}),
    config: jsonb('config').notNull().default({}),
    createdBy: text('createdBy'),
    feedbackDirectoryId: text('feedbackDirectoryId').notNull(),
  },
  (table) => [
    index('Chart_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    index('Chart_feedbackDirectoryId_idx').on(table.feedbackDirectoryId),
    uniqueIndex('Chart_workspaceId_name_key').on(table.workspaceId, table.name),
    foreignKey({
      name: 'Chart_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Chart_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Chart_feedbackDirectoryId_fkey',
      columns: [table.feedbackDirectoryId],
      foreignColumns: [feedbackDirectory.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const dashboard = pgTable(
  'Dashboard',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    workspaceId: text('workspaceId').notNull(),
    createdBy: text('createdBy'),
  },
  (table) => [
    index('Dashboard_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    uniqueIndex('Dashboard_workspaceId_name_key').on(table.workspaceId, table.name),
    foreignKey({
      name: 'Dashboard_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dashboard_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dashboardWidget = pgTable(
  'DashboardWidget',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    dashboardId: text('dashboardId').notNull(),
    chartId: text('chartId').notNull(),
    layout: jsonb('layout').notNull().default({ x: 0, y: 0, w: 4, h: 3 }),
    order: integer('order').notNull().default(0),
  },
  (table) => [
    index('DashboardWidget_dashboardId_order_idx').on(table.dashboardId, table.order),
    uniqueIndex('DashboardWidget_dashboardId_chartId_key').on(table.dashboardId, table.chartId),
    foreignKey({
      name: 'DashboardWidget_dashboardId_fkey',
      columns: [table.dashboardId],
      foreignColumns: [dashboard.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DashboardWidget_chartId_fkey',
      columns: [table.chartId],
      foreignColumns: [chart.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const deletionCleanup = pgTable(
  'DeletionCleanup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    kind: deletionCleanupKindEnum('kind').notNull(),
    organizationId: text('organizationId').notNull(),
    workspaceId: text('workspaceId').notNull(),
    surveyId: text('surveyId').notNull(),
    tenantIds: text('tenantIds').array().default([]),
    responseIds: text('responseIds').array().default([]),
    fileKeys: text('fileKeys').array().default([]),
    attempts: integer('attempts').notNull().default(0),
    nextAttemptAt: timestamp('nextAttemptAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastError: text('lastError'),
  },
  (table) => [
    index('DeletionCleanup_nextAttemptAt_idx').on(table.nextAttemptAt),
    index('DeletionCleanup_organizationId_created_at_idx').on(
      table.organizationId,
      table.createdAt,
    ),
  ],
)

export const feedbackSource = pgTable(
  'FeedbackSource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    type: feedbackSourceTypeEnum('type').notNull(),
    status: feedbackSourceStatusEnum('status').notNull().default('active'),
    importMode: feedbackSourceImportModeEnum('import_mode').notNull().default('completedOnly'),
    elementScope: feedbackSourceElementScopeEnum('elementScope').notNull().default('specific'),
    workspaceId: text('workspaceId').notNull(),
    feedbackDirectoryId: text('feedbackDirectoryId').notNull(),
    lastSyncAt: timestamp('last_sync_at', { precision: 3 }),
    createdBy: text('created_by'),
  },
  (table) => [
    index('FeedbackSource_type_idx').on(table.type),
    index('FeedbackSource_feedbackDirectoryId_workspaceId_idx').on(
      table.feedbackDirectoryId,
      table.workspaceId,
    ),
    unique('FeedbackSource_id_workspaceId_key').on(table.id, table.workspaceId),
    uniqueIndex('FeedbackSource_workspaceId_name_key').on(table.workspaceId, table.name),
    foreignKey({
      name: 'FeedbackSource_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackSource_feedbackDirectoryId_fkey',
      columns: [table.feedbackDirectoryId],
      foreignColumns: [feedbackDirectory.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackSource_created_by_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackSource_feedbackDirectoryId_workspaceId_fkey',
      columns: [table.feedbackDirectoryId, table.workspaceId],
      foreignColumns: [
        feedbackDirectoryWorkspace.feedbackDirectoryId,
        feedbackDirectoryWorkspace.workspaceId,
      ],
    })
      .onDelete('no action')
      .onUpdate('cascade'),
  ],
)

export const feedbackSourceFormbricksMapping = pgTable(
  'FeedbackSourceFormbricksMapping',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    feedbackSourceId: text('feedback_source_id').notNull(),
    workspaceId: text('workspaceId').notNull(),
    surveyId: text('surveyId').notNull(),
    elementId: text('elementId').notNull(),
    hubFieldType: hubFieldTypeEnum('hubFieldType').notNull(),
    customFieldLabel: text('custom_field_label'),
  },
  (table) => [
    index('FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx').on(
      table.workspaceId,
      table.surveyId,
    ),
    index('FeedbackSourceFormbricksMapping_surveyId_idx').on(table.surveyId),
    uniqueIndex('FeedbackSourceFormbricksMapping_workspaceId_feedbackSourceId_su').on(
      table.workspaceId,
      table.feedbackSourceId,
      table.surveyId,
      table.elementId,
    ),
    foreignKey({
      name: 'FeedbackSourceFormbricksMapping_feedback_source_id_workspa_fkey',
      columns: [table.feedbackSourceId, table.workspaceId],
      foreignColumns: [feedbackSource.id, feedbackSource.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackSourceFormbricksMapping_surveyId_workspaceId_fkey',
      columns: [table.surveyId, table.workspaceId],
      foreignColumns: [survey.id, survey.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feedbackSourceFieldMapping = pgTable(
  'FeedbackSourceFieldMapping',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    feedbackSourceId: text('feedback_source_id').notNull(),
    workspaceId: text('workspaceId').notNull(),
    sourceFieldId: text('source_field_id').notNull(),
    targetFieldId: text('target_field_id').notNull(),
    staticValue: text('static_value'),
  },
  (table) => [
    uniqueIndex('FeedbackSourceFieldMapping_workspaceId_feedbackSourceId_sourceF').on(
      table.workspaceId,
      table.feedbackSourceId,
      table.sourceFieldId,
      table.targetFieldId,
    ),
    foreignKey({
      name: 'FeedbackSourceFieldMapping_feedback_source_id_workspaceId_fkey',
      columns: [table.feedbackSourceId, table.workspaceId],
      foreignColumns: [feedbackSource.id, feedbackSource.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feedbackDirectory = pgTable(
  'FeedbackDirectory',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    isArchived: boolean('isArchived').notNull().default(false),
    organizationId: text('organizationId').notNull(),
  },
  (table) => [
    uniqueIndex('FeedbackDirectory_organizationId_name_key').on(table.organizationId, table.name),
    foreignKey({
      name: 'FeedbackDirectory_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feedbackDirectoryWorkspace = pgTable(
  'FeedbackDirectoryWorkspace',
  {
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    feedbackDirectoryId: text('feedbackDirectoryId').notNull(),
    workspaceId: text('workspaceId').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'FeedbackDirectoryWorkspace_pkey',
      columns: [table.feedbackDirectoryId, table.workspaceId],
    }),
    index('FeedbackDirectoryWorkspace_workspaceId_idx').on(table.workspaceId),
    foreignKey({
      name: 'FeedbackDirectoryWorkspace_feedbackDirectoryId_fkey',
      columns: [table.feedbackDirectoryId],
      foreignColumns: [feedbackDirectory.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackDirectoryWorkspace_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const authzedProjectionScopeState = pgTable('AuthzedProjectionScopeState', {
  scope: text('scope').primaryKey(),
  readyAt: timestamp('readyAt', { precision: 3 }),
  readyBy: text('readyBy'),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const authzedProjectionOutbox = pgTable('AuthzedProjectionOutbox', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  targetType: text('targetType').notNull(),
  primaryId: text('primaryId').notNull(),
  secondaryId: text('secondaryId'),
  isRevocation: boolean('isRevocation').notNull().default(false),
  attempts: integer('attempts').notNull().default(0),
  permanentFailures: integer('permanentFailures').notNull().default(0),
  availableAt: timestamp('availableAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  leasedAt: timestamp('leasedAt', { precision: 3 }),
  leaseExpiresAt: timestamp('leaseExpiresAt', { precision: 3 }),
  leaseOwner: text('leaseOwner'),
  processedAt: timestamp('processedAt', { precision: 3 }),
  deadLetteredAt: timestamp('deadLetteredAt', { precision: 3 }),
  lastAttemptAt: timestamp('lastAttemptAt', { precision: 3 }),
  lastErrorCode: text('lastErrorCode'),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const onlineUpgradeState = pgTable('OnlineUpgradeState', {
  release: text('release').primaryKey(),
  targetImage: text('targetImage').notNull(),
  phase: text('phase').notNull(),
  completedPhases: text('completedPhases').array().default([]),
  surveyCursor: text('surveyCursor'),
  surveyCount: bigint('surveyCount', { mode: 'bigint' })
    .notNull()
    .default(sql`0`),
  baselineCompletedAt: timestamp('baselineCompletedAt', { precision: 3 }),
  forwardOnlyAt: timestamp('forwardOnlyAt', { precision: 3 }),
  queuePausedAt: timestamp('queuePausedAt', { precision: 3 }),
  queuePauseDeadlineAt: timestamp('queuePauseDeadlineAt', { precision: 3 }),
  queueResumedAt: timestamp('queueResumedAt', { precision: 3 }),
  activatedAt: timestamp('activatedAt', { precision: 3 }),
  updatedAt: timestamp('updatedAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const retentionPolicy = pgTable(
  'RetentionPolicy',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    organizationId: text('organizationId').notNull(),
    entity: retentionEntityEnum('entity').notNull(),
    enabled: boolean('enabled').notNull().default(false),
    enabledAt: timestamp('enabledAt', { precision: 3 }),
    warnDays: integer('warnDays').notNull().default(60),
    periodDays: integer('periodDays').notNull(),
    conditions: retentionSurveyConditionEnum('conditions').array().default([]),
    deferredAt: timestamp('deferredAt', { precision: 3 }),
    updatedById: text('updatedById'),
  },
  (table) => [
    index('RetentionPolicy_updatedById_idx').on(table.updatedById),
    uniqueIndex('RetentionPolicy_organizationId_entity_key').on(table.organizationId, table.entity),
    foreignKey({
      name: 'RetentionPolicy_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionPolicy_updatedById_fkey',
      columns: [table.updatedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const retentionExemption = pgTable(
  'RetentionExemption',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    organizationId: text('organizationId').notNull(),
    entity: retentionEntityEnum('entity').notNull(),
    surveyId: text('surveyId'),
    until: timestamp('until', { precision: 3 }).notNull(),
    source: retentionExemptionSourceEnum('source').notNull().default('manual'),
    reason: text('reason'),
    createdById: text('createdById'),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    revokedById: text('revokedById'),
  },
  (table) => [
    index('RetentionExemption_organizationId_created_at_id_idx').on(
      table.organizationId,
      table.createdAt,
      table.id,
    ),
    index('RetentionExemption_surveyId_idx').on(table.surveyId),
    index('RetentionExemption_createdById_idx').on(table.createdById),
    index('RetentionExemption_revokedById_idx').on(table.revokedById),
    foreignKey({
      name: 'RetentionExemption_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionExemption_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionExemption_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionExemption_revokedById_fkey',
      columns: [table.revokedById],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const retentionRun = pgTable(
  'RetentionRun',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    organizationId: text('organizationId').notNull(),
    entity: retentionEntityEnum('entity').notNull(),
    kind: retentionRunKindEnum('kind').notNull().default('sweep'),
    startedAt: timestamp('startedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    finishedAt: timestamp('finishedAt', { precision: 3 }),
    notifiedCount: integer('notifiedCount').notNull().default(0),
    archivedCount: integer('archivedCount').notNull().default(0),
    deletedCount: integer('deletedCount').notNull().default(0),
    skippedCount: integer('skippedCount').notNull().default(0),
    hasChanges: boolean('hasChanges').notNull().default(false),
    scanCursor: text('scanCursor'),
  },
  (table) => [
    index('RetentionRun_organizationId_startedAt_id_idx').on(
      table.organizationId,
      table.startedAt,
      table.id,
    ),
    foreignKey({
      name: 'RetentionRun_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const retentionRunItem = pgTable(
  'RetentionRunItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    runId: text('runId').notNull(),
    targetType: retentionTargetTypeEnum('targetType').notNull(),
    targetId: text('targetId').notNull(),
    targetName: text('targetName'),
    action: retentionRunItemActionEnum('action').notNull(),
    count: integer('count').notNull().default(1),
    recipient: text('recipient'),
    skipReason: retentionSkipReasonEnum('skipReason'),
  },
  (table) => [
    index('RetentionRunItem_runId_id_idx').on(table.runId, table.id),
    index('RetentionRunItem_targetId_idx').on(table.targetId),
    foreignKey({
      name: 'RetentionRunItem_runId_fkey',
      columns: [table.runId],
      foreignColumns: [retentionRun.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const retentionNotice = pgTable(
  'RetentionNotice',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    organizationId: text('organizationId').notNull(),
    entity: retentionEntityEnum('entity').notNull(),
    surveyId: text('surveyId'),
    userId: text('userId'),
    sentAt: timestamp('sentAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deliveredAt: timestamp('deliveredAt', { precision: 3 }),
    emailSent: boolean('emailSent').notNull().default(false),
    claimToken: text('claimToken'),
    clockAt: timestamp('clockAt', { precision: 3 }),
  },
  (table) => [
    index('RetentionNotice_organizationId_idx').on(table.organizationId),
    uniqueIndex('RetentionNotice_surveyId_entity_key').on(table.surveyId, table.entity),
    uniqueIndex('RetentionNotice_userId_organizationId_entity_key').on(
      table.userId,
      table.organizationId,
      table.entity,
    ),
    foreignKey({
      name: 'RetentionNotice_organizationId_fkey',
      columns: [table.organizationId],
      foreignColumns: [organization.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionNotice_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RetentionNotice_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const retentionSurveyRestore = pgTable(
  'RetentionSurveyRestore',
  {
    surveyId: text('surveyId').primaryKey(),
    restoredAt: timestamp('restoredAt', { precision: 3 }).notNull(),
  },
  (table) => [
    foreignKey({
      name: 'RetentionSurveyRestore_surveyId_fkey',
      columns: [table.surveyId],
      foreignColumns: [survey.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflow = pgTable(
  'Workflow',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    name: text('name').notNull(),
    description: text('description'),
    status: workflowStatusEnum('status').notNull().default('draft'),
    workspaceId: text('workspaceId').notNull(),
    createdBy: text('createdBy'),
    definition: jsonb('definition').notNull(),
  },
  (table) => [
    index('Workflow_workspaceId_status_idx').on(table.workspaceId, table.status),
    index('Workflow_workspaceId_updated_at_idx').on(table.workspaceId, table.updatedAt),
    unique('Workflow_id_workspaceId_key').on(table.id, table.workspaceId),
    foreignKey({
      name: 'Workflow_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Workflow_createdBy_fkey',
      columns: [table.createdBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const workflowVersion = pgTable(
  'WorkflowVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    workflowId: text('workflowId').notNull(),
    workspaceId: text('workspaceId').notNull(),
    version: integer('version').notNull(),
    definition: jsonb('definition').notNull(),
    publishedAt: timestamp('publishedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    publishedBy: text('publishedBy'),
  },
  (table) => [
    index('WorkflowVersion_workflowId_workspaceId_idx').on(table.workflowId, table.workspaceId),
    index('WorkflowVersion_publishedBy_idx').on(table.publishedBy),
    uniqueIndex('WorkflowVersion_workflowId_version_key').on(table.workflowId, table.version),
    foreignKey({
      name: 'WorkflowVersion_workflowId_workspaceId_fkey',
      columns: [table.workflowId, table.workspaceId],
      foreignColumns: [workflow.id, workflow.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowVersion_publishedBy_fkey',
      columns: [table.publishedBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const workflowRun = pgTable(
  'WorkflowRun',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    workflowId: text('workflowId').notNull(),
    workspaceId: text('workspaceId').notNull(),
    workflowVersionId: text('workflowVersionId'),
    responseId: text('responseId'),
    status: workflowRunStatusEnum('status').notNull().default('queued'),
    triggerType: text('triggerType').notNull(),
    surveyId: text('surveyId'),
    isDryRun: boolean('isDryRun').notNull().default(false),
    idempotencyKey: text('idempotencyKey'),
    attempt: integer('attempt').notNull().default(0),
    nextAttemptAt: timestamp('nextAttemptAt', { precision: 3 }),
    lastErrorAt: timestamp('lastErrorAt', { precision: 3 }),
    triggerPayload: jsonb('triggerPayload').notNull(),
    data: jsonb('data').notNull().default({}),
    error: text('error'),
    dispatchedAt: timestamp('dispatchedAt', { precision: 3 }),
    startedAt: timestamp('startedAt', { precision: 3 }),
    finishedAt: timestamp('finishedAt', { precision: 3 }),
  },
  (table) => [
    index('WorkflowRun_workspaceId_created_at_idx').on(table.workspaceId, table.createdAt),
    index('WorkflowRun_workflowId_workspaceId_created_at_idx').on(
      table.workflowId,
      table.workspaceId,
      table.createdAt,
    ),
    index('WorkflowRun_status_created_at_idx').on(table.status, table.createdAt),
    index('WorkflowRun_responseId_idx').on(table.responseId),
    index('WorkflowRun_isDryRun_idx').on(table.isDryRun),
    index('WorkflowRun_nextAttemptAt_idx').on(table.nextAttemptAt),
    uniqueIndex('WorkflowRun_workflowId_idempotencyKey_key').on(
      table.workflowId,
      table.idempotencyKey,
    ),
    foreignKey({
      name: 'WorkflowRun_workflowId_workspaceId_fkey',
      columns: [table.workflowId, table.workspaceId],
      foreignColumns: [workflow.id, workflow.workspaceId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowRun_workspaceId_fkey',
      columns: [table.workspaceId],
      foreignColumns: [workspace.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowRun_workflowVersionId_fkey',
      columns: [table.workflowVersionId],
      foreignColumns: [workflowVersion.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowRun_responseId_fkey',
      columns: [table.responseId],
      foreignColumns: [response.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowRunLog = pgTable(
  'WorkflowRunLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => createId()),
    runId: text('runId').notNull(),
    sequence: integer('sequence').notNull(),
    stepId: text('stepId').notNull(),
    stepType: text('stepType').notNull(),
    status: workflowRunLogStatusEnum('status').notNull().default('pending'),
    input: jsonb('input').notNull().default({}),
    output: jsonb('output').notNull().default({}),
    error: text('error'),
    startedAt: timestamp('startedAt', { precision: 3 }),
    finishedAt: timestamp('finishedAt', { precision: 3 }),
  },
  (table) => [
    index('WorkflowRunLog_runId_sequence_idx').on(table.runId, table.sequence),
    uniqueIndex('WorkflowRunLog_runId_stepId_key').on(table.runId, table.stepId),
    foreignKey({
      name: 'WorkflowRunLog_runId_fkey',
      columns: [table.runId],
      foreignColumns: [workflowRun.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    creative,
    creativeDailyStat,
    creativeDailyPageStat,
    creativeTrigger,
    creativeLanguage,
    outboundEmailAudit,
    outboundEmailAuditErasureIntent,
    webhook,
    contactAttribute,
    contactAttributeKey,
    contact,
    response,
    tag,
    tagsOnResponses,
    display,
    surveyTrigger,
    surveyAttributeFilter,
    survey,
    surveyQuota,
    responseQuotaLink,
    surveyFollowUp,
    actionClass,
    integration,
    dataMigration,
    workspace,
    organization,
    organizationBilling,
    membership,
    invite,
    apiKey,
    apiKeyWorkspace,
    account,
    session,
    verificationToken,
    twoFactor,
    jwks,
    oauthClient,
    oauthAccessToken,
    oauthRefreshToken,
    oauthConsent,
    oauthResource,
    oauthClientResource,
    oauthClientAssertion,
    passwordResetToken,
    user,
    segment,
    language,
    surveyLanguage,
    embeddedData,
    surveyEmbeddedData,
    team,
    teamUser,
    workspaceTeam,
    chart,
    dashboard,
    dashboardWidget,
    deletionCleanup,
    feedbackSource,
    feedbackSourceFormbricksMapping,
    feedbackSourceFieldMapping,
    feedbackDirectory,
    feedbackDirectoryWorkspace,
    authzedProjectionScopeState,
    authzedProjectionOutbox,
    onlineUpgradeState,
    retentionPolicy,
    retentionExemption,
    retentionRun,
    retentionRunItem,
    retentionNotice,
    retentionSurveyRestore,
    workflow,
    workflowVersion,
    workflowRun,
    workflowRunLog,
  },
  (r) => ({
    creative: {
      workspace: r.one.workspace({
        from: r.creative.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      creator: r.one.user({ from: r.creative.createdBy, to: r.user.id }),
      survey: r.one.survey({
        from: [r.creative.surveyId, r.creative.workspaceId],
        to: [r.survey.id, r.survey.workspaceId],
      }),
      triggers: r.many.creativeTrigger(),
      languages: r.many.creativeLanguage(),
      dailyStats: r.many.creativeDailyStat(),
      dailyPageStats: r.many.creativeDailyPageStat(),
    },
    creativeDailyStat: {
      creative: r.one.creative({
        from: r.creativeDailyStat.creativeId,
        to: r.creative.id,
        optional: false,
      }),
    },
    creativeDailyPageStat: {
      creative: r.one.creative({
        from: r.creativeDailyPageStat.creativeId,
        to: r.creative.id,
        optional: false,
      }),
    },
    creativeTrigger: {
      creative: r.one.creative({
        from: r.creativeTrigger.creativeId,
        to: r.creative.id,
        optional: false,
      }),
      actionClass: r.one.actionClass({
        from: r.creativeTrigger.actionClassId,
        to: r.actionClass.id,
        optional: false,
      }),
    },
    creativeLanguage: {
      language: r.one.language({
        from: r.creativeLanguage.languageId,
        to: r.language.id,
        optional: false,
      }),
      creative: r.one.creative({
        from: r.creativeLanguage.creativeId,
        to: r.creative.id,
        optional: false,
      }),
    },
    webhook: {
      workspace: r.one.workspace({
        from: r.webhook.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    contactAttribute: {
      attributeKey: r.one.contactAttributeKey({
        from: r.contactAttribute.attributeKeyId,
        to: r.contactAttributeKey.id,
        optional: false,
      }),
      contact: r.one.contact({
        from: r.contactAttribute.contactId,
        to: r.contact.id,
        optional: false,
      }),
    },
    contactAttributeKey: {
      workspace: r.one.workspace({
        from: r.contactAttributeKey.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      attributes: r.many.contactAttribute(),
      attributeFilters: r.many.surveyAttributeFilter(),
    },
    contact: {
      workspace: r.one.workspace({
        from: r.contact.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      responses: r.many.response(),
      attributes: r.many.contactAttribute(),
      displays: r.many.display(),
    },
    response: {
      survey: r.one.survey({ from: r.response.surveyId, to: r.survey.id, optional: false }),
      contact: r.one.contact({ from: r.response.contactId, to: r.contact.id }),
      tags: r.many.tagsOnResponses(),
      quotaLinks: r.many.responseQuotaLink(),
      display: r.one.display({ from: r.response.displayId, to: r.display.id }),
      workflowRuns: r.many.workflowRun(),
    },
    tag: {
      responses: r.many.tagsOnResponses(),
      workspace: r.one.workspace({ from: r.tag.workspaceId, to: r.workspace.id, optional: false }),
    },
    tagsOnResponses: {
      response: r.one.response({
        from: r.tagsOnResponses.responseId,
        to: r.response.id,
        optional: false,
      }),
      tag: r.one.tag({ from: r.tagsOnResponses.tagId, to: r.tag.id, optional: false }),
    },
    display: {
      survey: r.one.survey({ from: r.display.surveyId, to: r.survey.id, optional: false }),
      contact: r.one.contact({ from: r.display.contactId, to: r.contact.id }),
      response: r.one.response({ from: r.display.id, to: r.response.displayId }),
    },
    surveyTrigger: {
      survey: r.one.survey({ from: r.surveyTrigger.surveyId, to: r.survey.id, optional: false }),
      actionClass: r.one.actionClass({
        from: r.surveyTrigger.actionClassId,
        to: r.actionClass.id,
        optional: false,
      }),
    },
    surveyAttributeFilter: {
      attributeKey: r.one.contactAttributeKey({
        from: r.surveyAttributeFilter.attributeKeyId,
        to: r.contactAttributeKey.id,
        optional: false,
      }),
      survey: r.one.survey({
        from: r.surveyAttributeFilter.surveyId,
        to: r.survey.id,
        optional: false,
      }),
    },
    survey: {
      workspace: r.one.workspace({
        from: r.survey.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      creator: r.one.user({ from: r.survey.createdBy, to: r.user.id, alias: 'SurveyCreator' }),
      owner: r.one.user({ from: r.survey.ownerId, to: r.user.id, alias: 'SurveyOwner' }),
      responses: r.many.response(),
      quotas: r.many.surveyQuota(),
      triggers: r.many.surveyTrigger(),
      attributeFilters: r.many.surveyAttributeFilter(),
      displays: r.many.display(),
      segment: r.one.segment({ from: r.survey.segmentId, to: r.segment.id }),
      languages: r.many.surveyLanguage(),
      embeddedData: r.many.embeddedData(),
      embeddedDataLinks: r.many.surveyEmbeddedData(),
      followUps: r.many.surveyFollowUp(),
      feedbackSourceMappings: r.many.feedbackSourceFormbricksMapping(),
      retentionExemptions: r.many.retentionExemption(),
      retentionNotices: r.many.retentionNotice(),
      retentionRestore: r.one.retentionSurveyRestore({
        from: r.survey.id,
        to: r.retentionSurveyRestore.surveyId,
      }),
      creatives: r.many.creative(),
    },
    surveyQuota: {
      survey: r.one.survey({ from: r.surveyQuota.surveyId, to: r.survey.id, optional: false }),
      quotaLinks: r.many.responseQuotaLink(),
    },
    responseQuotaLink: {
      response: r.one.response({
        from: r.responseQuotaLink.responseId,
        to: r.response.id,
        optional: false,
      }),
      quota: r.one.surveyQuota({
        from: r.responseQuotaLink.quotaId,
        to: r.surveyQuota.id,
        optional: false,
      }),
    },
    surveyFollowUp: {
      survey: r.one.survey({ from: r.surveyFollowUp.surveyId, to: r.survey.id, optional: false }),
    },
    actionClass: {
      workspace: r.one.workspace({
        from: r.actionClass.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      surveyTriggers: r.many.surveyTrigger(),
      creativeTriggers: r.many.creativeTrigger(),
    },
    integration: {
      workspace: r.one.workspace({
        from: r.integration.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    workspace: {
      organization: r.one.organization({
        from: r.workspace.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      languages: r.many.language(),
      embeddedData: r.many.embeddedData(),
      workspaceTeams: r.many.workspaceTeam(),
      feedbackDirectoryWorkspaces: r.many.feedbackDirectoryWorkspace(),
      charts: r.many.chart(),
      dashboards: r.many.dashboard(),
      creatives: r.many.creative(),
      surveys: r.many.survey(),
      contacts: r.many.contact(),
      actionClasses: r.many.actionClass(),
      contactAttributeKeys: r.many.contactAttributeKey(),
      webhooks: r.many.webhook(),
      tags: r.many.tag(),
      segments: r.many.segment(),
      integrations: r.many.integration(),
      apiKeyWorkspaces: r.many.apiKeyWorkspace(),
      feedbackSources: r.many.feedbackSource(),
      workflows: r.many.workflow(),
      workflowRuns: r.many.workflowRun(),
    },
    organization: {
      memberships: r.many.membership(),
      workspaces: r.many.workspace(),
      billing: r.one.organizationBilling({
        from: r.organization.id,
        to: r.organizationBilling.organizationId,
      }),
      invites: r.many.invite(),
      teams: r.many.team(),
      apiKeys: r.many.apiKey(),
      feedbackDirectories: r.many.feedbackDirectory(),
      retentionPolicies: r.many.retentionPolicy(),
      retentionExemptions: r.many.retentionExemption(),
      retentionRuns: r.many.retentionRun(),
      retentionNotices: r.many.retentionNotice(),
    },
    organizationBilling: {
      organization: r.one.organization({
        from: r.organizationBilling.organizationId,
        to: r.organization.id,
        optional: false,
      }),
    },
    membership: {
      organization: r.one.organization({
        from: r.membership.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      user: r.one.user({ from: r.membership.userId, to: r.user.id, optional: false }),
    },
    invite: {
      organization: r.one.organization({
        from: r.invite.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      creator: r.one.user({
        from: r.invite.creatorId,
        to: r.user.id,
        optional: false,
        alias: 'inviteCreatedBy',
      }),
      acceptor: r.one.user({ from: r.invite.acceptorId, to: r.user.id, alias: 'inviteAcceptedBy' }),
    },
    apiKey: {
      organization: r.one.organization({
        from: r.apiKey.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      apiKeyWorkspaces: r.many.apiKeyWorkspace(),
    },
    apiKeyWorkspace: {
      apiKey: r.one.apiKey({ from: r.apiKeyWorkspace.apiKeyId, to: r.apiKey.id, optional: false }),
      workspace: r.one.workspace({
        from: r.apiKeyWorkspace.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id }) },
    session: {
      user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }),
      oauthAccessTokens: r.many.oauthAccessToken(),
      oauthRefreshTokens: r.many.oauthRefreshToken(),
    },
    twoFactor: { user: r.one.user({ from: r.twoFactor.userId, to: r.user.id, optional: false }) },
    oauthClient: {
      user: r.one.user({ from: r.oauthClient.userId, to: r.user.id }),
      accessTokens: r.many.oauthAccessToken(),
      refreshTokens: r.many.oauthRefreshToken(),
      consents: r.many.oauthConsent(),
      resources: r.many.oauthClientResource(),
    },
    oauthAccessToken: {
      client: r.one.oauthClient({
        from: r.oauthAccessToken.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      session: r.one.session({ from: r.oauthAccessToken.sessionId, to: r.session.id }),
      user: r.one.user({ from: r.oauthAccessToken.userId, to: r.user.id }),
      refresh: r.one.oauthRefreshToken({
        from: r.oauthAccessToken.refreshId,
        to: r.oauthRefreshToken.id,
      }),
    },
    oauthRefreshToken: {
      client: r.one.oauthClient({
        from: r.oauthRefreshToken.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      session: r.one.session({ from: r.oauthRefreshToken.sessionId, to: r.session.id }),
      user: r.one.user({ from: r.oauthRefreshToken.userId, to: r.user.id, optional: false }),
      accessTokens: r.many.oauthAccessToken(),
    },
    oauthConsent: {
      client: r.one.oauthClient({
        from: r.oauthConsent.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      user: r.one.user({ from: r.oauthConsent.userId, to: r.user.id }),
    },
    oauthResource: { clients: r.many.oauthClientResource() },
    oauthClientResource: {
      client: r.one.oauthClient({
        from: r.oauthClientResource.clientId,
        to: r.oauthClient.clientId,
        optional: false,
      }),
      resource: r.one.oauthResource({
        from: r.oauthClientResource.resourceId,
        to: r.oauthResource.identifier,
        optional: false,
      }),
    },
    passwordResetToken: {
      user: r.one.user({ from: r.passwordResetToken.userId, to: r.user.id, optional: false }),
    },
    user: {
      memberships: r.many.membership(),
      accounts: r.many.account(),
      sessions: r.many.session(),
      twoFactor: r.one.twoFactor({ from: r.user.id, to: r.twoFactor.userId }),
      oauthClients: r.many.oauthClient(),
      oauthAccessTokens: r.many.oauthAccessToken(),
      oauthRefreshTokens: r.many.oauthRefreshToken(),
      oauthConsents: r.many.oauthConsent(),
      passwordResetToken: r.one.passwordResetToken({
        from: r.user.id,
        to: r.passwordResetToken.userId,
      }),
      invitesCreated: r.many.invite({ alias: 'inviteCreatedBy' }),
      invitesAccepted: r.many.invite({ alias: 'inviteAcceptedBy' }),
      surveys: r.many.survey({ alias: 'SurveyCreator' }),
      ownedSurveys: r.many.survey({ alias: 'SurveyOwner' }),
      charts: r.many.chart(),
      dashboards: r.many.dashboard(),
      creatives: r.many.creative(),
      feedbackSources: r.many.feedbackSource(),
      teamUsers: r.many.teamUser(),
      workflows: r.many.workflow(),
      workflowVersionsPublished: r.many.workflowVersion(),
      retentionPoliciesUpdated: r.many.retentionPolicy(),
      exemptionsCreated: r.many.retentionExemption({ alias: 'retentionExemptionCreatedBy' }),
      exemptionsRevoked: r.many.retentionExemption({ alias: 'retentionExemptionRevokedBy' }),
      retentionNotices: r.many.retentionNotice(),
    },
    segment: {
      workspace: r.one.workspace({
        from: r.segment.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      surveys: r.many.survey(),
    },
    language: {
      workspace: r.one.workspace({
        from: r.language.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      surveyLanguages: r.many.surveyLanguage(),
      creativeLanguages: r.many.creativeLanguage(),
    },
    surveyLanguage: {
      language: r.one.language({
        from: r.surveyLanguage.languageId,
        to: r.language.id,
        optional: false,
      }),
      survey: r.one.survey({ from: r.surveyLanguage.surveyId, to: r.survey.id, optional: false }),
    },
    embeddedData: {
      survey: r.one.survey({
        from: [r.embeddedData.surveyId, r.embeddedData.workspaceId],
        to: [r.survey.id, r.survey.workspaceId],
      }),
      workspace: r.one.workspace({
        from: r.embeddedData.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      surveyLinks: r.many.surveyEmbeddedData(),
    },
    surveyEmbeddedData: {
      survey: r.one.survey({
        from: [r.surveyEmbeddedData.surveyId, r.surveyEmbeddedData.workspaceId],
        to: [r.survey.id, r.survey.workspaceId],
        optional: false,
      }),
      embeddedData: r.one.embeddedData({
        from: [r.surveyEmbeddedData.embeddedDataId, r.surveyEmbeddedData.workspaceId],
        to: [r.embeddedData.id, r.embeddedData.workspaceId],
        optional: false,
      }),
    },
    team: {
      organization: r.one.organization({
        from: r.team.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      teamUsers: r.many.teamUser(),
      workspaceTeams: r.many.workspaceTeam(),
    },
    teamUser: {
      team: r.one.team({ from: r.teamUser.teamId, to: r.team.id, optional: false }),
      user: r.one.user({ from: r.teamUser.userId, to: r.user.id, optional: false }),
    },
    workspaceTeam: {
      workspace: r.one.workspace({
        from: r.workspaceTeam.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      team: r.one.team({ from: r.workspaceTeam.teamId, to: r.team.id, optional: false }),
    },
    chart: {
      workspace: r.one.workspace({
        from: r.chart.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      creator: r.one.user({ from: r.chart.createdBy, to: r.user.id }),
      feedbackDirectory: r.one.feedbackDirectory({
        from: r.chart.feedbackDirectoryId,
        to: r.feedbackDirectory.id,
        optional: false,
      }),
      widgets: r.many.dashboardWidget(),
    },
    dashboard: {
      workspace: r.one.workspace({
        from: r.dashboard.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      creator: r.one.user({ from: r.dashboard.createdBy, to: r.user.id }),
      widgets: r.many.dashboardWidget(),
    },
    dashboardWidget: {
      dashboard: r.one.dashboard({
        from: r.dashboardWidget.dashboardId,
        to: r.dashboard.id,
        optional: false,
      }),
      chart: r.one.chart({ from: r.dashboardWidget.chartId, to: r.chart.id, optional: false }),
    },
    feedbackSource: {
      workspace: r.one.workspace({
        from: r.feedbackSource.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      feedbackDirectory: r.one.feedbackDirectory({
        from: r.feedbackSource.feedbackDirectoryId,
        to: r.feedbackDirectory.id,
        optional: false,
      }),
      formbricksMappings: r.many.feedbackSourceFormbricksMapping(),
      fieldMappings: r.many.feedbackSourceFieldMapping(),
      creator: r.one.user({ from: r.feedbackSource.createdBy, to: r.user.id }),
      feedbackDirectoryWorkspace: r.one.feedbackDirectoryWorkspace({
        from: [r.feedbackSource.feedbackDirectoryId, r.feedbackSource.workspaceId],
        to: [
          r.feedbackDirectoryWorkspace.feedbackDirectoryId,
          r.feedbackDirectoryWorkspace.workspaceId,
        ],
        optional: false,
      }),
    },
    feedbackSourceFormbricksMapping: {
      feedbackSource: r.one.feedbackSource({
        from: [
          r.feedbackSourceFormbricksMapping.feedbackSourceId,
          r.feedbackSourceFormbricksMapping.workspaceId,
        ],
        to: [r.feedbackSource.id, r.feedbackSource.workspaceId],
        optional: false,
      }),
      survey: r.one.survey({
        from: [
          r.feedbackSourceFormbricksMapping.surveyId,
          r.feedbackSourceFormbricksMapping.workspaceId,
        ],
        to: [r.survey.id, r.survey.workspaceId],
        optional: false,
      }),
    },
    feedbackSourceFieldMapping: {
      feedbackSource: r.one.feedbackSource({
        from: [
          r.feedbackSourceFieldMapping.feedbackSourceId,
          r.feedbackSourceFieldMapping.workspaceId,
        ],
        to: [r.feedbackSource.id, r.feedbackSource.workspaceId],
        optional: false,
      }),
    },
    feedbackDirectory: {
      organization: r.one.organization({
        from: r.feedbackDirectory.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      workspaces: r.many.feedbackDirectoryWorkspace(),
      feedbackSources: r.many.feedbackSource(),
      charts: r.many.chart(),
    },
    feedbackDirectoryWorkspace: {
      feedbackDirectory: r.one.feedbackDirectory({
        from: r.feedbackDirectoryWorkspace.feedbackDirectoryId,
        to: r.feedbackDirectory.id,
        optional: false,
      }),
      workspace: r.one.workspace({
        from: r.feedbackDirectoryWorkspace.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      feedbackSources: r.many.feedbackSource(),
    },
    retentionPolicy: {
      organization: r.one.organization({
        from: r.retentionPolicy.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      updatedBy: r.one.user({ from: r.retentionPolicy.updatedById, to: r.user.id }),
    },
    retentionExemption: {
      organization: r.one.organization({
        from: r.retentionExemption.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      survey: r.one.survey({ from: r.retentionExemption.surveyId, to: r.survey.id }),
      createdBy: r.one.user({
        from: r.retentionExemption.createdById,
        to: r.user.id,
        alias: 'retentionExemptionCreatedBy',
      }),
      revokedBy: r.one.user({
        from: r.retentionExemption.revokedById,
        to: r.user.id,
        alias: 'retentionExemptionRevokedBy',
      }),
    },
    retentionRun: {
      organization: r.one.organization({
        from: r.retentionRun.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      items: r.many.retentionRunItem(),
    },
    retentionRunItem: {
      run: r.one.retentionRun({
        from: r.retentionRunItem.runId,
        to: r.retentionRun.id,
        optional: false,
      }),
    },
    retentionNotice: {
      organization: r.one.organization({
        from: r.retentionNotice.organizationId,
        to: r.organization.id,
        optional: false,
      }),
      survey: r.one.survey({ from: r.retentionNotice.surveyId, to: r.survey.id }),
      user: r.one.user({ from: r.retentionNotice.userId, to: r.user.id }),
    },
    retentionSurveyRestore: {
      survey: r.one.survey({
        from: r.retentionSurveyRestore.surveyId,
        to: r.survey.id,
        optional: false,
      }),
    },
    workflow: {
      workspace: r.one.workspace({
        from: r.workflow.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      creator: r.one.user({ from: r.workflow.createdBy, to: r.user.id }),
      versions: r.many.workflowVersion(),
      runs: r.many.workflowRun(),
    },
    workflowVersion: {
      workflow: r.one.workflow({
        from: [r.workflowVersion.workflowId, r.workflowVersion.workspaceId],
        to: [r.workflow.id, r.workflow.workspaceId],
        optional: false,
      }),
      publisher: r.one.user({ from: r.workflowVersion.publishedBy, to: r.user.id }),
      runs: r.many.workflowRun(),
    },
    workflowRun: {
      workflow: r.one.workflow({
        from: [r.workflowRun.workflowId, r.workflowRun.workspaceId],
        to: [r.workflow.id, r.workflow.workspaceId],
        optional: false,
      }),
      workspace: r.one.workspace({
        from: r.workflowRun.workspaceId,
        to: r.workspace.id,
        optional: false,
      }),
      workflowVersion: r.one.workflowVersion({
        from: r.workflowRun.workflowVersionId,
        to: r.workflowVersion.id,
      }),
      response: r.one.response({ from: r.workflowRun.responseId, to: r.response.id }),
      logs: r.many.workflowRunLog(),
    },
    workflowRunLog: {
      run: r.one.workflowRun({
        from: r.workflowRunLog.runId,
        to: r.workflowRun.id,
        optional: false,
      }),
    },
  }),
)

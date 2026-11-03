import {
  type PgTableExtraConfigValue,
  bigint,
  boolean,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

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

export const conversationVisibilityEnum = pgEnum('ConversationVisibility', [
  'PRIVATE',
  'PUBLIC_LINK',
  'PUBLIC_GROUP',
  'PUBLIC_DOCUMENT',
  'PUBLIC_DATAROOM',
])

export const participantRoleEnum = pgEnum('ParticipantRole', ['OWNER', 'PARTICIPANT'])

export const faqVisibilityEnum = pgEnum('FaqVisibility', [
  'PUBLIC_DATAROOM',
  'PUBLIC_LINK',
  'PUBLIC_DOCUMENT',
])

export const faqStatusEnum = pgEnum('FaqStatus', ['DRAFT', 'PUBLISHED', 'ARCHIVED'])

export const itemTypeEnum = pgEnum('ItemType', ['DATAROOM_DOCUMENT', 'DATAROOM_FOLDER'])

export const defaultPermissionStrategyEnum = pgEnum('DefaultPermissionStrategy', [
  'INHERIT_FROM_PARENT',
  'ASK_EVERY_TIME',
  'HIDDEN_BY_DEFAULT',
])

export const rootItemAccessEnum = pgEnum('RootItemAccess', [
  'VIEW_ONLY',
  'VIEW_AND_DOWNLOAD',
  'HIDDEN',
])

export const documentStorageTypeEnum = pgEnum('DocumentStorageType', ['S3_PATH', 'VERCEL_BLOB'])

export const linkTypeEnum = pgEnum('LinkType', ['DOCUMENT_LINK', 'DATAROOM_LINK', 'WORKFLOW_LINK'])

export const linkAudienceTypeEnum = pgEnum('LinkAudienceType', ['GENERAL', 'GROUP', 'TEAM'])

export const customFieldTypeEnum = pgEnum('CustomFieldType', [
  'SHORT_TEXT',
  'LONG_TEXT',
  'NUMBER',
  'PHONE_NUMBER',
  'URL',
  'CHECKBOX',
  'SELECT',
  'MULTI_SELECT',
])

export const referralTrackEnum = pgEnum('ReferralTrack', ['AFFILIATE', 'PARTNER_DISCOUNT'])

export const viewTypeEnum = pgEnum('ViewType', ['DOCUMENT_VIEW', 'DATAROOM_VIEW'])

export const downloadTypeEnum = pgEnum('DownloadType', ['SINGLE', 'BULK', 'FOLDER'])

export const emailTypeEnum = pgEnum('EmailType', [
  'FIRST_DAY_DOMAIN_REMINDER_EMAIL',
  'FIRST_DOMAIN_INVALID_EMAIL',
  'SECOND_DOMAIN_INVALID_EMAIL',
  'FIRST_TRIAL_END_REMINDER_EMAIL',
  'FINAL_TRIAL_END_REMINDER_EMAIL',
])

export const tagTypeEnum = pgEnum('TagType', ['LINK_TAG', 'DOCUMENT_TAG', 'DATAROOM_TAG'])

export const invitationStatusEnum = pgEnum('InvitationStatus', ['SENT', 'FAILED', 'BOUNCED'])

export const roleEnum = pgEnum('Role', ['ADMIN', 'MANAGER', 'MEMBER', 'DATAROOM_MEMBER'])

export const workflowStepTypeEnum = pgEnum('WorkflowStepType', ['ROUTER'])

export const executionStatusEnum = pgEnum('ExecutionStatus', [
  'PENDING',
  'IN_PROGRESS',
  'COMPLETED',
  'FAILED',
  'BLOCKED',
])

export const documentAnnotation = pgTable(
  'DocumentAnnotation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: text('title').notNull(),
    content: jsonb('content').notNull(),
    pages: integer('pages').array(),
    documentId: text('documentId').notNull(),
    teamId: text('teamId').notNull(),
    createdById: text('createdById').notNull(),
    isVisible: boolean('isVisible').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentAnnotation_documentId_idx').on(table.documentId),
    index('DocumentAnnotation_teamId_idx').on(table.teamId),
    index('DocumentAnnotation_createdById_idx').on(table.createdById),
    foreignKey({
      name: 'DocumentAnnotation_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentAnnotation_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentAnnotation_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const annotationImage = pgTable(
  'AnnotationImage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    filename: text('filename').notNull(),
    url: text('url').notNull(),
    size: integer('size'),
    mimeType: text('mimeType'),
    annotationId: text('annotationId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('AnnotationImage_annotationId_idx').on(table.annotationId),
    foreignKey({
      name: 'AnnotationImage_annotationId_fkey',
      columns: [table.annotationId],
      foreignColumns: [documentAnnotation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const brand = pgTable(
  'Brand',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    logo: text('logo'),
    hideLogo: boolean('hideLogo').notNull().default(false),
    banner: text('banner'),
    brandColor: text('brandColor'),
    accentColor: text('accentColor'),
    accentButtonColor: text('accentButtonColor'),
    applyAccentColorToDataroomView: boolean('applyAccentColorToDataroomView')
      .notNull()
      .default(false),
    welcomeMessage: text('welcomeMessage'),
    ctaLabel: text('ctaLabel'),
    ctaUrl: text('ctaUrl'),
    privacyPolicyUrl: text('privacyPolicyUrl'),
    cardLayout: text('cardLayout').notNull().default('LIST'),
    showFolderTree: boolean('showFolderTree').notNull().default(true),
    viewerLayoutPreset: text('viewerLayoutPreset').notNull().default('STANDARD'),
    viewerHeaderStyle: text('viewerHeaderStyle').notNull().default('DEFAULT'),
    hideFolderIconsInMain: boolean('hideFolderIconsInMain').notNull().default(false),
    customLinkPreviewEnabled: boolean('customLinkPreviewEnabled').notNull().default(false),
    linkPreviewTitle: text('linkPreviewTitle'),
    linkPreviewDescription: text('linkPreviewDescription'),
    linkPreviewImage: text('linkPreviewImage'),
    linkPreviewFavicon: text('linkPreviewFavicon'),
    defaultLanguage: text('defaultLanguage').notNull().default('en'),
    name: text('name').notNull().default('Default'),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table): PgTableExtraConfigValue[] => [
    index('Brand_teamId_idx').on(table.teamId),
    uniqueIndex('Brand_teamId_name_key').on(table.teamId, table.name),
    foreignKey({ name: 'Brand_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const conversation = pgTable(
  'Conversation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: text('title'),
    isEnabled: boolean('isEnabled').notNull().default(true),
    visibilityMode: conversationVisibilityEnum('visibilityMode').notNull().default('PRIVATE'),
    dataroomId: text('dataroomId').notNull(),
    dataroomDocumentId: text('dataroomDocumentId'),
    documentVersionNumber: integer('documentVersionNumber'),
    documentPageNumber: integer('documentPageNumber'),
    linkId: text('linkId'),
    viewerGroupId: text('viewerGroupId'),
    questionId: text('questionId'),
    origin: text('origin').notNull().default('visitor'),
    category: text('category'),
    priority: text('priority'),
    recipientViewerId: text('recipientViewerId'),
    initialViewId: text('initialViewId'),
    teamId: text('teamId').notNull(),
    lastMessageAt: timestamp('lastMessageAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Conversation_dataroomId_idx').on(table.dataroomId),
    index('Conversation_dataroomDocumentId_idx').on(table.dataroomDocumentId),
    index('Conversation_linkId_idx').on(table.linkId),
    index('Conversation_teamId_idx').on(table.teamId),
    index('Conversation_viewerGroupId_idx').on(table.viewerGroupId),
    index('Conversation_initialViewId_idx').on(table.initialViewId),
    index('Conversation_questionId_idx').on(table.questionId),
    index('Conversation_recipientViewerId_idx').on(table.recipientViewerId),
    uniqueIndex('Conversation_questionId_recipientViewerId_key').on(
      table.questionId,
      table.recipientViewerId,
    ),
    foreignKey({
      name: 'Conversation_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_dataroomDocumentId_fkey',
      columns: [table.dataroomDocumentId],
      foreignColumns: [dataroomDocument.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_viewerGroupId_fkey',
      columns: [table.viewerGroupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_questionId_fkey',
      columns: [table.questionId],
      foreignColumns: [dataroomQuestion.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_recipientViewerId_fkey',
      columns: [table.recipientViewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_initialViewId_fkey',
      columns: [table.initialViewId],
      foreignColumns: [view.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Conversation_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dataroomQuestion = pgTable(
  'DataroomQuestion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    content: text('content').notNull(),
    category: text('category'),
    priority: text('priority'),
    status: text('status').notNull().default('unanswered'),
    orderIndex: integer('orderIndex'),
    importBatchId: text('importBatchId'),
    dataroomId: text('dataroomId').notNull(),
    teamId: text('teamId').notNull(),
    createdByUserId: text('createdByUserId'),
    dataroomDocumentId: text('dataroomDocumentId'),
    documentPageNumber: integer('documentPageNumber'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DataroomQuestion_dataroomId_idx').on(table.dataroomId),
    index('DataroomQuestion_teamId_idx').on(table.teamId),
    index('DataroomQuestion_status_idx').on(table.status),
    index('DataroomQuestion_importBatchId_idx').on(table.importBatchId),
    index('DataroomQuestion_dataroomDocumentId_idx').on(table.dataroomDocumentId),
    index('DataroomQuestion_createdByUserId_idx').on(table.createdByUserId),
    foreignKey({
      name: 'DataroomQuestion_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestion_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestion_createdByUserId_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestion_dataroomDocumentId_fkey',
      columns: [table.dataroomDocumentId],
      foreignColumns: [dataroomDocument.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dataroomQuestionAssignment = pgTable(
  'DataroomQuestionAssignment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    questionId: text('questionId').notNull(),
    linkId: text('linkId'),
    groupId: text('groupId'),
    viewerId: text('viewerId'),
    email: text('email'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('DataroomQuestionAssignment_questionId_idx').on(table.questionId),
    index('DataroomQuestionAssignment_linkId_idx').on(table.linkId),
    index('DataroomQuestionAssignment_groupId_idx').on(table.groupId),
    index('DataroomQuestionAssignment_viewerId_idx').on(table.viewerId),
    uniqueIndex('DataroomQuestionAssignment_questionId_linkId_key').on(
      table.questionId,
      table.linkId,
    ),
    uniqueIndex('DataroomQuestionAssignment_questionId_groupId_key').on(
      table.questionId,
      table.groupId,
    ),
    uniqueIndex('DataroomQuestionAssignment_questionId_viewerId_key').on(
      table.questionId,
      table.viewerId,
    ),
    uniqueIndex('DataroomQuestionAssignment_questionId_email_key').on(
      table.questionId,
      table.email,
    ),
    foreignKey({
      name: 'DataroomQuestionAssignment_questionId_fkey',
      columns: [table.questionId],
      foreignColumns: [dataroomQuestion.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestionAssignment_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestionAssignment_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomQuestionAssignment_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const conversationParticipant = pgTable(
  'ConversationParticipant',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    conversationId: text('conversationId').notNull(),
    role: participantRoleEnum('role').notNull().default('PARTICIPANT'),
    viewerId: text('viewerId'),
    userId: text('userId'),
    receiveNotifications: boolean('receiveNotifications').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ConversationParticipant_conversationId_idx').on(table.conversationId),
    index('ConversationParticipant_viewerId_idx').on(table.viewerId),
    index('ConversationParticipant_userId_idx').on(table.userId),
    uniqueIndex('ConversationParticipant_conversationId_viewerId_key').on(
      table.conversationId,
      table.viewerId,
    ),
    uniqueIndex('ConversationParticipant_conversationId_userId_key').on(
      table.conversationId,
      table.userId,
    ),
    foreignKey({
      name: 'ConversationParticipant_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationParticipant_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationParticipant_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const message = pgTable(
  'Message',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    content: text('content').notNull(),
    conversationId: text('conversationId').notNull(),
    userId: text('userId'),
    viewerId: text('viewerId'),
    viewId: text('viewId'),
    isRead: boolean('isRead').notNull().default(false),
    isInternalNote: boolean('isInternalNote').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Message_conversationId_idx').on(table.conversationId),
    index('Message_userId_idx').on(table.userId),
    index('Message_viewerId_idx').on(table.viewerId),
    index('Message_viewId_idx').on(table.viewId),
    foreignKey({
      name: 'Message_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Message_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Message_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Message_viewId_fkey', columns: [table.viewId], foreignColumns: [view.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const conversationView = pgTable(
  'ConversationView',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    conversationId: text('conversationId').notNull(),
    viewId: text('viewId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ConversationView_conversationId_idx').on(table.conversationId),
    index('ConversationView_viewId_idx').on(table.viewId),
    uniqueIndex('ConversationView_conversationId_viewId_key').on(
      table.conversationId,
      table.viewId,
    ),
    foreignKey({
      name: 'ConversationView_conversationId_fkey',
      columns: [table.conversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ConversationView_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dataroomFaqItem = pgTable(
  'DataroomFaqItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: text('title'),
    editedQuestion: text('editedQuestion').notNull(),
    originalQuestion: text('originalQuestion'),
    answer: text('answer').notNull(),
    description: text('description'),
    dataroomId: text('dataroomId').notNull(),
    linkId: text('linkId'),
    dataroomDocumentId: text('dataroomDocumentId'),
    sourceConversationId: text('sourceConversationId'),
    questionMessageId: text('questionMessageId'),
    answerMessageId: text('answerMessageId'),
    teamId: text('teamId').notNull(),
    publishedByUserId: text('publishedByUserId').notNull(),
    visibilityMode: faqVisibilityEnum('visibilityMode').notNull().default('PUBLIC_DATAROOM'),
    status: faqStatusEnum('status').notNull().default('PUBLISHED'),
    isAnonymized: boolean('isAnonymized').notNull().default(true),
    viewCount: integer('viewCount').notNull().default(0),
    tags: text('tags').array().default([]),
    documentPageNumber: integer('documentPageNumber'),
    documentVersionNumber: integer('documentVersionNumber'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DataroomFaqItem_dataroomId_idx').on(table.dataroomId),
    index('DataroomFaqItem_linkId_idx').on(table.linkId),
    index('DataroomFaqItem_dataroomDocumentId_idx').on(table.dataroomDocumentId),
    index('DataroomFaqItem_sourceConversationId_idx').on(table.sourceConversationId),
    index('DataroomFaqItem_teamId_idx').on(table.teamId),
    index('DataroomFaqItem_publishedByUserId_idx').on(table.publishedByUserId),
    index('DataroomFaqItem_status_idx').on(table.status),
    index('DataroomFaqItem_visibilityMode_idx').on(table.visibilityMode),
    index('DataroomFaqItem_createdAt_idx').on(table.createdAt),
    foreignKey({
      name: 'DataroomFaqItem_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_dataroomDocumentId_fkey',
      columns: [table.dataroomDocumentId],
      foreignColumns: [dataroomDocument.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_sourceConversationId_fkey',
      columns: [table.sourceConversationId],
      foreignColumns: [conversation.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_questionMessageId_fkey',
      columns: [table.questionMessageId],
      foreignColumns: [message.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_answerMessageId_fkey',
      columns: [table.answerMessageId],
      foreignColumns: [message.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFaqItem_publishedByUserId_fkey',
      columns: [table.publishedByUserId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dataroom = pgTable(
  'Dataroom',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    pId: text('pId').notNull(),
    name: text('name').notNull(),
    internalName: text('internalName'),
    description: text('description'),
    teamId: text('teamId').notNull(),
    brandId: text('brandId'),
    conversationsEnabled: boolean('conversationsEnabled').notNull().default(false),
    agentsEnabled: boolean('agentsEnabled').notNull().default(false),
    vectorStoreId: text('vectorStoreId'),
    requestListEnabled: boolean('requestListEnabled').notNull().default(false),
    enableChangeNotifications: boolean('enableChangeNotifications').notNull().default(false),
    enableVisitorUploadChangeNotifications: boolean('enableVisitorUploadChangeNotifications')
      .notNull()
      .default(false),
    defaultPermissionStrategy: defaultPermissionStrategyEnum('defaultPermissionStrategy')
      .notNull()
      .default('INHERIT_FROM_PARENT'),
    defaultShowBanner: boolean('defaultShowBanner').notNull().default(true),
    defaultGroupPermissionStrategy: defaultPermissionStrategyEnum('defaultGroupPermissionStrategy')
      .notNull()
      .default('INHERIT_FROM_PARENT'),
    defaultRootItemAccess: rootItemAccessEnum('defaultRootItemAccess')
      .notNull()
      .default('VIEW_ONLY'),
    defaultGroupRootItemAccess: rootItemAccessEnum('defaultGroupRootItemAccess')
      .notNull()
      .default('VIEW_ONLY'),
    allowBulkDownload: boolean('allowBulkDownload').notNull().default(true),
    showLastUpdated: boolean('showLastUpdated').notNull().default(true),
    introductionEnabled: boolean('introductionEnabled').notNull().default(false),
    introductionContent: jsonb('introductionContent'),
    isFrozen: boolean('isFrozen').notNull().default(false),
    frozenAt: timestamp('frozenAt', { precision: 3 }),
    frozenBy: text('frozenBy'),
    freezeArchiveUrl: text('freezeArchiveUrl'),
    freezeArchiveHash: text('freezeArchiveHash'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Dataroom_pId_key').on(table.pId),
    index('Dataroom_teamId_idx').on(table.teamId),
    index('Dataroom_brandId_idx').on(table.brandId),
    foreignKey({ name: 'Dataroom_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dataroom_brandId_fkey',
      columns: [table.brandId],
      foreignColumns: [brand.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Dataroom_frozenBy_fkey',
      columns: [table.frozenBy],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const taskList = pgTable(
  'TaskList',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    orderIndex: integer('orderIndex'),
    dataroomId: text('dataroomId').notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('TaskList_dataroomId_idx').on(table.dataroomId),
    index('TaskList_teamId_idx').on(table.teamId),
    index('TaskList_dataroomId_orderIndex_idx').on(table.dataroomId, table.orderIndex),
    foreignKey({
      name: 'TaskList_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'TaskList_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const task = pgTable(
  'Task',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: text('title').notNull(),
    description: text('description'),
    type: text('type').notNull().default('TODO'),
    status: text('status').notNull().default('OPEN'),
    category: text('category'),
    dueDate: timestamp('dueDate', { precision: 3 }),
    orderIndex: integer('orderIndex'),
    taskListId: text('taskListId').notNull(),
    dataroomId: text('dataroomId').notNull(),
    teamId: text('teamId').notNull(),
    uploadFolderId: text('uploadFolderId'),
    createdByUserId: text('createdByUserId'),
    lastActivityNotifiedAt: timestamp('lastActivityNotifiedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Task_taskListId_idx').on(table.taskListId),
    index('Task_dataroomId_idx').on(table.dataroomId),
    index('Task_teamId_idx').on(table.teamId),
    index('Task_uploadFolderId_idx').on(table.uploadFolderId),
    index('Task_taskListId_orderIndex_idx').on(table.taskListId, table.orderIndex),
    foreignKey({
      name: 'Task_taskListId_fkey',
      columns: [table.taskListId],
      foreignColumns: [taskList.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Task_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Task_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Task_uploadFolderId_fkey',
      columns: [table.uploadFolderId],
      foreignColumns: [dataroomFolder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Task_createdByUserId_fkey',
      columns: [table.createdByUserId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const taskAssignment = pgTable(
  'TaskAssignment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    taskId: text('taskId').notNull(),
    viewerId: text('viewerId'),
    groupId: text('groupId'),
    linkId: text('linkId'),
    email: text('email'),
    notifiedAt: timestamp('notifiedAt', { precision: 3 }),
    lastReminderAt: timestamp('lastReminderAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('TaskAssignment_taskId_idx').on(table.taskId),
    index('TaskAssignment_viewerId_idx').on(table.viewerId),
    index('TaskAssignment_groupId_idx').on(table.groupId),
    index('TaskAssignment_linkId_idx').on(table.linkId),
    uniqueIndex('TaskAssignment_taskId_viewerId_key').on(table.taskId, table.viewerId),
    uniqueIndex('TaskAssignment_taskId_groupId_key').on(table.taskId, table.groupId),
    uniqueIndex('TaskAssignment_taskId_linkId_key').on(table.taskId, table.linkId),
    uniqueIndex('TaskAssignment_taskId_email_key').on(table.taskId, table.email),
    foreignKey({
      name: 'TaskAssignment_taskId_fkey',
      columns: [table.taskId],
      foreignColumns: [task.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskAssignment_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskAssignment_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskAssignment_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const taskActivity = pgTable(
  'TaskActivity',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    taskId: text('taskId').notNull(),
    type: text('type').notNull(),
    comment: text('comment'),
    fromStatus: text('fromStatus'),
    toStatus: text('toStatus'),
    userId: text('userId'),
    viewerId: text('viewerId'),
    viewId: text('viewId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('TaskActivity_taskId_idx').on(table.taskId),
    index('TaskActivity_userId_idx').on(table.userId),
    index('TaskActivity_viewerId_idx').on(table.viewerId),
    index('TaskActivity_viewId_idx').on(table.viewId),
    index('TaskActivity_taskId_createdAt_idx').on(table.taskId, table.createdAt),
    foreignKey({
      name: 'TaskActivity_taskId_fkey',
      columns: [table.taskId],
      foreignColumns: [task.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskActivity_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskActivity_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TaskActivity_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dataroomDocument = pgTable(
  'DataroomDocument',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    dataroomId: text('dataroomId').notNull(),
    documentId: text('documentId').notNull(),
    folderId: text('folderId'),
    orderIndex: integer('orderIndex'),
    hierarchicalIndex: text('hierarchicalIndex'),
    vectorStoreFileId: text('vectorStoreFileId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DataroomDocument_folderId_idx').on(table.folderId),
    index('DataroomDocument_dataroomId_folderId_orderIndex_idx').on(
      table.dataroomId,
      table.folderId,
      table.orderIndex,
    ),
    index('DataroomDocument_documentId_idx').on(table.documentId),
    uniqueIndex('DataroomDocument_dataroomId_documentId_key').on(
      table.dataroomId,
      table.documentId,
    ),
    foreignKey({
      name: 'DataroomDocument_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomDocument_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomDocument_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [dataroomFolder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const dataroomFolder = pgTable(
  'DataroomFolder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    path: text('path').notNull(),
    parentId: text('parentId'),
    icon: text('icon'),
    color: text('color'),
    dataroomId: text('dataroomId').notNull(),
    orderIndex: integer('orderIndex'),
    hierarchicalIndex: text('hierarchicalIndex'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DataroomFolder_parentId_idx').on(table.parentId),
    index('DataroomFolder_dataroomId_parentId_orderIndex_idx').on(
      table.dataroomId,
      table.parentId,
      table.orderIndex,
    ),
    uniqueIndex('DataroomFolder_dataroomId_path_key').on(table.dataroomId, table.path),
    foreignKey({
      name: 'DataroomFolder_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DataroomFolder_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dataroomBrand = pgTable(
  'DataroomBrand',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    logo: text('logo'),
    hideLogo: boolean('hideLogo'),
    banner: text('banner'),
    brandColor: text('brandColor'),
    accentColor: text('accentColor'),
    accentButtonColor: text('accentButtonColor'),
    applyAccentColorToDataroomView: boolean('applyAccentColorToDataroomView')
      .notNull()
      .default(false),
    welcomeMessage: text('welcomeMessage'),
    cardLayout: text('cardLayout').notNull().default('LIST'),
    showFolderTree: boolean('showFolderTree').notNull().default(true),
    viewerLayoutPreset: text('viewerLayoutPreset').notNull().default('STANDARD'),
    viewerHeaderStyle: text('viewerHeaderStyle').notNull().default('DEFAULT'),
    hideFolderIconsInMain: boolean('hideFolderIconsInMain').notNull().default(false),
    ctaLabel: text('ctaLabel'),
    ctaUrl: text('ctaUrl'),
    customLinkPreviewEnabled: boolean('customLinkPreviewEnabled').notNull().default(false),
    linkPreviewTitle: text('linkPreviewTitle'),
    linkPreviewDescription: text('linkPreviewDescription'),
    linkPreviewImage: text('linkPreviewImage'),
    linkPreviewFavicon: text('linkPreviewFavicon'),
    defaultLanguage: text('defaultLanguage').notNull().default('en'),
    dataroomId: text('dataroomId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('DataroomBrand_dataroomId_key').on(table.dataroomId),
    foreignKey({
      name: 'DataroomBrand_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const viewerGroup = pgTable(
  'ViewerGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    domains: text('domains').array(),
    allowAll: boolean('allowAll').notNull().default(false),
    dataroomId: text('dataroomId').notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ViewerGroup_dataroomId_idx').on(table.dataroomId),
    index('ViewerGroup_teamId_idx').on(table.teamId),
    index('ViewerGroup_dataroomId_createdAt_idx').on(table.dataroomId, table.createdAt),
    foreignKey({
      name: 'ViewerGroup_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ViewerGroup_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const viewerGroupMembership = pgTable(
  'ViewerGroupMembership',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    viewerId: text('viewerId').notNull(),
    groupId: text('groupId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ViewerGroupMembership_viewerId_idx').on(table.viewerId),
    index('ViewerGroupMembership_groupId_idx').on(table.groupId),
    uniqueIndex('ViewerGroupMembership_viewerId_groupId_key').on(table.viewerId, table.groupId),
    foreignKey({
      name: 'ViewerGroupMembership_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ViewerGroupMembership_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const viewerGroupAccessControls = pgTable(
  'ViewerGroupAccessControls',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    groupId: text('groupId').notNull(),
    itemId: text('itemId').notNull(),
    itemType: itemTypeEnum('itemType').notNull(),
    canView: boolean('canView').notNull().default(true),
    canDownload: boolean('canDownload').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('ViewerGroupAccessControls_groupId_idx').on(table.groupId),
    uniqueIndex('ViewerGroupAccessControls_groupId_itemId_key').on(table.groupId, table.itemId),
    foreignKey({
      name: 'ViewerGroupAccessControls_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const permissionGroup = pgTable(
  'PermissionGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    dataroomId: text('dataroomId').notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('PermissionGroup_dataroomId_idx').on(table.dataroomId),
    index('PermissionGroup_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'PermissionGroup_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'PermissionGroup_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const permissionGroupAccessControls = pgTable(
  'PermissionGroupAccessControls',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    groupId: text('groupId').notNull(),
    itemId: text('itemId').notNull(),
    itemType: itemTypeEnum('itemType').notNull(),
    canView: boolean('canView').notNull().default(true),
    canDownload: boolean('canDownload').notNull().default(false),
    canDownloadOriginal: boolean('canDownloadOriginal').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('PermissionGroupAccessControls_groupId_idx').on(table.groupId),
    uniqueIndex('PermissionGroupAccessControls_groupId_itemId_key').on(table.groupId, table.itemId),
    foreignKey({
      name: 'PermissionGroupAccessControls_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [permissionGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const document = pgTable(
  'Document',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    file: text('file').notNull(),
    originalFile: text('originalFile'),
    type: text('type'),
    contentType: text('contentType'),
    storageType: documentStorageTypeEnum('storageType').notNull().default('VERCEL_BLOB'),
    numPages: integer('numPages'),
    teamId: text('teamId').notNull(),
    ownerId: text('ownerId'),
    assistantEnabled: boolean('assistantEnabled').notNull().default(false),
    advancedExcelEnabled: boolean('advancedExcelEnabled').notNull().default(false),
    agentsEnabled: boolean('agentsEnabled').notNull().default(false),
    downloadOnly: boolean('downloadOnly').notNull().default(false),
    hiddenInAllDocuments: boolean('hiddenInAllDocuments').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    folderId: text('folderId'),
    isExternalUpload: boolean('isExternalUpload').notNull().default(false),
  },
  (table) => [
    index('Document_ownerId_idx').on(table.ownerId),
    index('Document_teamId_idx').on(table.teamId),
    index('Document_folderId_idx').on(table.folderId),
    index('Document_teamId_folderId_idx').on(table.teamId, table.folderId),
    index('Document_teamId_name_idx').on(table.teamId, table.name),
    index('Document_teamId_hiddenInAllDocuments_idx').on(table.teamId, table.hiddenInAllDocuments),
    foreignKey({
      name: 'Document_ownerId_fkey',
      columns: [table.ownerId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Document_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Document_folderId_fkey',
      columns: [table.folderId],
      foreignColumns: [folder.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const documentVersion = pgTable(
  'DocumentVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    versionNumber: integer('versionNumber').notNull(),
    documentId: text('documentId').notNull(),
    file: text('file').notNull(),
    originalFile: text('originalFile'),
    type: text('type'),
    contentType: text('contentType'),
    fileSize: bigint('fileSize', { mode: 'bigint' }),
    storageType: documentStorageTypeEnum('storageType').notNull().default('VERCEL_BLOB'),
    numPages: integer('numPages'),
    isPrimary: boolean('isPrimary').notNull().default(false),
    isVertical: boolean('isVertical').notNull().default(false),
    fileId: text('fileId'),
    vectorStoreFileId: text('vectorStoreFileId'),
    hasPages: boolean('hasPages').notNull().default(false),
    length: integer('length'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentVersion_documentId_idx').on(table.documentId),
    index('DocumentVersion_documentId_isPrimary_idx').on(table.documentId, table.isPrimary),
    index('DocumentVersion_documentId_createdAt_idx').on(table.documentId, table.createdAt),
    index('DocumentVersion_documentId_isPrimary_createdAt_idx').on(
      table.documentId,
      table.isPrimary,
      table.createdAt,
    ),
    uniqueIndex('DocumentVersion_versionNumber_documentId_key').on(
      table.versionNumber,
      table.documentId,
    ),
    foreignKey({
      name: 'DocumentVersion_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentPage = pgTable(
  'DocumentPage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    versionId: text('versionId').notNull(),
    pageNumber: integer('pageNumber').notNull(),
    embeddedLinks: text('embeddedLinks').array(),
    pageLinks: jsonb('pageLinks'),
    metadata: jsonb('metadata'),
    file: text('file').notNull(),
    storageType: documentStorageTypeEnum('storageType').notNull().default('VERCEL_BLOB'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentPage_versionId_idx').on(table.versionId),
    uniqueIndex('DocumentPage_pageNumber_versionId_key').on(table.pageNumber, table.versionId),
    foreignKey({
      name: 'DocumentPage_versionId_fkey',
      columns: [table.versionId],
      foreignColumns: [documentVersion.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const folder = pgTable(
  'Folder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    path: text('path').notNull(),
    parentId: text('parentId'),
    hiddenInAllDocuments: boolean('hiddenInAllDocuments').notNull().default(false),
    icon: text('icon'),
    color: text('color'),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Folder_parentId_idx').on(table.parentId),
    index('Folder_teamId_hiddenInAllDocuments_idx').on(table.teamId, table.hiddenInAllDocuments),
    uniqueIndex('Folder_teamId_path_key').on(table.teamId, table.path),
    foreignKey({
      name: 'Folder_parentId_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Folder_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const documentUpload = pgTable(
  'DocumentUpload',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    documentId: text('documentId').notNull(),
    teamId: text('teamId').notNull(),
    viewerId: text('viewerId'),
    viewId: text('viewId'),
    linkId: text('linkId').notNull(),
    dataroomId: text('dataroomId'),
    dataroomDocumentId: text('dataroomDocumentId'),
    taskId: text('taskId'),
    originalFilename: text('originalFilename'),
    fileSize: bigint('fileSize', { mode: 'bigint' }),
    numPages: integer('numPages'),
    mimeType: text('mimeType'),
    uploadedAt: timestamp('uploadedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentUpload_documentId_idx').on(table.documentId),
    index('DocumentUpload_viewerId_idx').on(table.viewerId),
    index('DocumentUpload_viewId_idx').on(table.viewId),
    index('DocumentUpload_linkId_idx').on(table.linkId),
    index('DocumentUpload_teamId_idx').on(table.teamId),
    index('DocumentUpload_dataroomId_idx').on(table.dataroomId),
    index('DocumentUpload_dataroomDocumentId_idx').on(table.dataroomDocumentId),
    index('DocumentUpload_taskId_idx').on(table.taskId),
    foreignKey({
      name: 'DocumentUpload_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_dataroomDocumentId_fkey',
      columns: [table.dataroomDocumentId],
      foreignColumns: [dataroomDocument.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentUpload_taskId_fkey',
      columns: [table.taskId],
      foreignColumns: [task.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const pendingDocumentUpload = pgTable(
  'PendingDocumentUpload',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    uploadId: text('uploadId').notNull(),
    teamId: text('teamId').notNull(),
    tokenId: text('tokenId').notNull(),
    key: text('key').notNull(),
    contentType: text('contentType').notNull(),
    storageType: documentStorageTypeEnum('storageType').notNull().default('S3_PATH'),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    consumedAt: timestamp('consumedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('PendingDocumentUpload_uploadId_key').on(table.uploadId),
    index('PendingDocumentUpload_teamId_expiresAt_idx').on(table.teamId, table.expiresAt),
    index('PendingDocumentUpload_tokenId_expiresAt_idx').on(table.tokenId, table.expiresAt),
  ],
)

export const integration = pgTable(
  'Integration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    slug: text('slug').notNull(),
    description: text('description'),
    readme: text('readme'),
    developer: text('developer').notNull(),
    website: text('website').notNull(),
    logo: text('logo'),
    screenshots: jsonb('screenshots'),
    verified: boolean('verified').notNull().default(false),
    installUrl: text('installUrl'),
    category: text('category'),
    comingSoon: boolean('comingSoon').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('Integration_slug_key').on(table.slug)],
)

export const installedIntegration = pgTable(
  'InstalledIntegration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    credentials: jsonb('credentials'),
    configuration: jsonb('configuration'),
    enabled: boolean('enabled').notNull().default(true),
    integrationId: text('integrationId').notNull(),
    userId: text('userId'),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('InstalledIntegration_teamId_idx').on(table.teamId),
    index('InstalledIntegration_integrationId_idx').on(table.integrationId),
    uniqueIndex('InstalledIntegration_teamId_integrationId_key').on(
      table.teamId,
      table.integrationId,
    ),
    foreignKey({
      name: 'InstalledIntegration_integrationId_fkey',
      columns: [table.integrationId],
      foreignColumns: [integration.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'InstalledIntegration_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'InstalledIntegration_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const jacksonIndex = pgTable(
  'jackson_index',
  {
    id: serial('id').primaryKey(),
    key: text('key').notNull(),
    storeKey: text('storeKey').notNull(),
  },
  (table) => [index('_jackson_index_key_store').on(table.key, table.storeKey)],
)

export const jacksonStore = pgTable(
  'jackson_store',
  {
    key: text('key').primaryKey(),
    value: text('value').notNull(),
    iv: text('iv'),
    tag: text('tag'),
    namespace: text('namespace'),
    createdAt: timestamp('createdAt', { precision: 0 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    modifiedAt: timestamp('modifiedAt', { precision: 0 }),
  },
  (table) => [index('_jackson_store_namespace').on(table.namespace)],
)

export const jacksonTtl = pgTable(
  'jackson_ttl',
  { key: text('key').primaryKey(), expiresAt: bigint('expiresAt', { mode: 'bigint' }).notNull() },
  (table) => [index('_jackson_ttl_expires_at').on(table.expiresAt)],
)

export const link = pgTable(
  'Link',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    documentId: text('documentId'),
    dataroomId: text('dataroomId'),
    linkType: linkTypeEnum('linkType').notNull().default('DOCUMENT_LINK'),
    url: text('url'),
    name: text('name'),
    slug: text('slug'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    password: text('password'),
    allowList: text('allowList').array(),
    denyList: text('denyList').array(),
    emailProtected: boolean('emailProtected').notNull().default(true),
    emailAuthenticated: boolean('emailAuthenticated').notNull().default(false),
    allowDownload: boolean('allowDownload').default(false),
    isArchived: boolean('isArchived').notNull().default(false),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    domainId: text('domainId'),
    domainSlug: text('domainSlug'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    enableNotification: boolean('enableNotification').default(true),
    enableFeedback: boolean('enableFeedback').default(false),
    enableQuestion: boolean('enableQuestion').default(false),
    enableScreenshotProtection: boolean('enableScreenshotProtection').default(false),
    enableConfidentialView: boolean('enableConfidentialView').default(false),
    enableAgreement: boolean('enableAgreement').default(false),
    agreementId: text('agreementId'),
    showBanner: boolean('showBanner').default(false),
    enableWatermark: boolean('enableWatermark').default(false),
    watermarkConfig: jsonb('watermarkConfig'),
    audienceType: linkAudienceTypeEnum('audienceType').notNull().default('GENERAL'),
    groupId: text('groupId'),
    permissionGroupId: text('permissionGroupId'),
    metaTitle: text('metaTitle'),
    metaDescription: text('metaDescription'),
    metaImage: text('metaImage'),
    metaFavicon: text('metaFavicon'),
    enableCustomMetatag: boolean('enableCustomMetatag').default(false),
    welcomeMessage: text('welcomeMessage'),
    enableConversation: boolean('enableConversation').notNull().default(false),
    enableAIAgents: boolean('enableAIAgents').default(false),
    enableUpload: boolean('enableUpload').default(false),
    isFileRequestOnly: boolean('isFileRequestOnly').default(false),
    uploadFolderId: text('uploadFolderId'),
    uploadFolderIds: text('uploadFolderIds').array().default([]),
    enableIndexFile: boolean('enableIndexFile').default(false),
    teamId: text('teamId'),
    brandId: text('brandId'),
    ownerId: text('ownerId'),
  },
  (table) => [
    uniqueIndex('Link_url_key').on(table.url),
    index('Link_documentId_idx').on(table.documentId),
    index('Link_teamId_idx').on(table.teamId),
    index('Link_documentId_isArchived_idx').on(table.documentId, table.isArchived),
    index('Link_permissionGroupId_idx').on(table.permissionGroupId),
    index('Link_deletedAt_idx').on(table.deletedAt),
    index('Link_ownerId_idx').on(table.ownerId),
    index('Link_brandId_idx').on(table.brandId),
    uniqueIndex('Link_domainSlug_slug_key').on(table.domainSlug, table.slug),
    foreignKey({
      name: 'Link_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_domainId_fkey',
      columns: [table.domainId],
      foreignColumns: [domain.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_agreementId_fkey',
      columns: [table.agreementId],
      foreignColumns: [agreement.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Link_permissionGroupId_fkey',
      columns: [table.permissionGroupId],
      foreignColumns: [permissionGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Link_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Link_brandId_fkey', columns: [table.brandId], foreignColumns: [brand.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Link_ownerId_fkey', columns: [table.ownerId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const linkPreset = pgTable(
  'LinkPreset',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    teamId: text('teamId').notNull(),
    pId: text('pId'),
    enableCustomMetaTag: boolean('enableCustomMetaTag').default(false),
    metaTitle: text('metaTitle'),
    metaDescription: text('metaDescription'),
    metaImage: text('metaImage'),
    metaFavicon: text('metaFavicon'),
    enableNotification: boolean('enableNotification').default(false),
    emailProtected: boolean('emailProtected').default(true),
    emailAuthenticated: boolean('emailAuthenticated').default(false),
    allowDownload: boolean('allowDownload').default(false),
    enableAllowList: boolean('enableAllowList').default(false),
    allowList: text('allowList').array(),
    enableDenyList: boolean('enableDenyList').default(false),
    denyList: text('denyList').array(),
    expiresIn: integer('expiresIn'),
    enableScreenshotProtection: boolean('enableScreenshotProtection').default(false),
    enableConfidentialView: boolean('enableConfidentialView').default(false),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    enablePassword: boolean('enablePassword').default(false),
    password: text('password'),
    enableWatermark: boolean('enableWatermark').default(false),
    watermarkConfig: jsonb('watermarkConfig'),
    enableAgreement: boolean('enableAgreement').default(false),
    agreementId: text('agreementId'),
    enableCustomFields: boolean('enableCustomFields').default(false),
    customFields: jsonb('customFields'),
    showBanner: boolean('showBanner').default(false),
    welcomeMessage: text('welcomeMessage'),
    isDefault: boolean('isDefault').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('LinkPreset_pId_key').on(table.pId),
    index('LinkPreset_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'LinkPreset_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const visitorGroup = pgTable(
  'VisitorGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    emails: text('emails').array(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('VisitorGroup_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'VisitorGroup_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const linkVisitorGroup = pgTable(
  'LinkVisitorGroup',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    linkId: text('linkId').notNull(),
    visitorGroupId: text('visitorGroupId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LinkVisitorGroup_linkId_idx').on(table.linkId),
    index('LinkVisitorGroup_visitorGroupId_idx').on(table.visitorGroupId),
    uniqueIndex('LinkVisitorGroup_linkId_visitorGroupId_key').on(
      table.linkId,
      table.visitorGroupId,
    ),
    foreignKey({
      name: 'LinkVisitorGroup_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LinkVisitorGroup_visitorGroupId_fkey',
      columns: [table.visitorGroupId],
      foreignColumns: [visitorGroup.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const customField = pgTable(
  'CustomField',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    type: customFieldTypeEnum('type').notNull(),
    identifier: text('identifier').notNull(),
    label: text('label').notNull(),
    placeholder: text('placeholder'),
    required: boolean('required').notNull().default(false),
    disabled: boolean('disabled').notNull().default(false),
    linkId: text('linkId').notNull(),
    orderIndex: integer('orderIndex').notNull().default(0),
  },
  (table) => [
    index('CustomField_linkId_idx').on(table.linkId),
    foreignKey({
      name: 'CustomField_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const customFieldResponse = pgTable(
  'CustomFieldResponse',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    data: jsonb('data').notNull(),
    viewId: text('viewId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('CustomFieldResponse_viewId_key').on(table.viewId),
    index('CustomFieldResponse_viewId_idx').on(table.viewId),
    foreignKey({
      name: 'CustomFieldResponse_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const notificationPreference = pgTable(
  'NotificationPreference',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    teamId: text('teamId').notNull(),
    type: text('type').notNull(),
    frequency: text('frequency').notNull().default('IMMEDIATE'),
    scope: text('scope').notNull().default('ALL'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('NotificationPreference_userId_teamId_idx').on(table.userId, table.teamId),
    uniqueIndex('NotificationPreference_userId_teamId_type_key').on(
      table.userId,
      table.teamId,
      table.type,
    ),
    foreignKey({
      name: 'NotificationPreference_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'NotificationPreference_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oAuthClient = pgTable(
  'OAuthClient',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('clientId').notNull(),
    clientName: text('clientName').notNull(),
    clientUri: text('clientUri'),
    logoUri: text('logoUri'),
    redirectUris: text('redirectUris').array(),
    grantTypes: text('grantTypes').array(),
    scopes: text('scopes').notNull(),
    tokenAuthMethod: text('tokenAuthMethod').notNull(),
    hashedSecret: text('hashedSecret'),
    registrationType: text('registrationType').notNull(),
    metadataUrl: text('metadataUrl'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('OAuthClient_clientId_key').on(table.clientId),
    index('OAuthClient_clientId_idx').on(table.clientId),
  ],
)

export const oAuthClientApproval = pgTable(
  'OAuthClientApproval',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    clientId: text('clientId').notNull(),
    approvedAt: timestamp('approvedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    revokedAt: timestamp('revokedAt', { precision: 3 }),
    clientMetadataHash: text('clientMetadataHash'),
  },
  (table) => [
    index('OAuthClientApproval_clientId_idx').on(table.clientId),
    uniqueIndex('OAuthClientApproval_userId_clientId_key').on(table.userId, table.clientId),
  ],
)

export const oAuthGrantTeam = pgTable(
  'OAuthGrantTeam',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    grantId: text('grantId').notNull(),
    teamId: text('teamId').notNull(),
    userId: text('userId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('OAuthGrantTeam_grantId_key').on(table.grantId),
    index('OAuthGrantTeam_grantId_idx').on(table.grantId),
  ],
)

export const oAuthRecord = pgTable(
  'OAuthRecord',
  {
    id: text('id').primaryKey(),
    kind: text('kind').notNull(),
    payload: jsonb('payload').notNull(),
    grantId: text('grantId'),
    userCode: text('userCode'),
    uid: text('uid'),
    expiresAt: timestamp('expiresAt', { precision: 3 }),
    consumedAt: timestamp('consumedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('OAuthRecord_kind_idx').on(table.kind),
    index('OAuthRecord_grantId_idx').on(table.grantId),
    index('OAuthRecord_userCode_idx').on(table.userCode),
    index('OAuthRecord_uid_idx').on(table.uid),
    index('OAuthRecord_expiresAt_idx').on(table.expiresAt),
  ],
)

export const documentRedactionJob = pgTable(
  'DocumentRedactionJob',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    documentId: text('documentId').notNull(),
    documentVersionId: text('documentVersionId').notNull(),
    teamId: text('teamId').notNull(),
    createdById: text('createdById').notNull(),
    status: text('status').notNull().default('PENDING'),
    customTerms: text('customTerms').array(),
    reasons: text('reasons').array(),
    resultVersionId: text('resultVersionId'),
    error: text('error'),
    detectRunId: text('detectRunId'),
    applyRunId: text('applyRunId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentRedactionJob_documentId_idx').on(table.documentId),
    index('DocumentRedactionJob_documentVersionId_idx').on(table.documentVersionId),
    index('DocumentRedactionJob_teamId_idx').on(table.teamId),
    index('DocumentRedactionJob_teamId_status_idx').on(table.teamId, table.status),
    foreignKey({
      name: 'DocumentRedactionJob_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentRedactionJob_documentVersionId_fkey',
      columns: [table.documentVersionId],
      foreignColumns: [documentVersion.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentRedactionJob_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'DocumentRedactionJob_createdById_fkey',
      columns: [table.createdById],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const documentRedaction = pgTable(
  'DocumentRedaction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    jobId: text('jobId').notNull(),
    pageNumber: integer('pageNumber').notNull(),
    x: doublePrecision('x').notNull(),
    y: doublePrecision('y').notNull(),
    width: doublePrecision('width').notNull(),
    height: doublePrecision('height').notNull(),
    textPreview: text('textPreview'),
    category: text('category'),
    confidence: text('confidence'),
    reason: text('reason'),
    source: text('source').notNull().default('AI'),
    status: text('status').notNull().default('PENDING'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('DocumentRedaction_jobId_idx').on(table.jobId),
    index('DocumentRedaction_jobId_pageNumber_idx').on(table.jobId, table.pageNumber),
    index('DocumentRedaction_jobId_status_idx').on(table.jobId, table.status),
    foreignKey({
      name: 'DocumentRedaction_jobId_fkey',
      columns: [table.jobId],
      foreignColumns: [documentRedactionJob.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const referralInvite = pgTable(
  'ReferralInvite',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    teamId: text('teamId'),
    scopeId: text('scopeId').notNull(),
    email: text('email').notNull(),
    referralLink: text('referralLink'),
    sentAt: timestamp('sentAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ReferralInvite_userId_idx').on(table.userId),
    index('ReferralInvite_teamId_idx').on(table.teamId),
    uniqueIndex('ReferralInvite_scopeId_email_key').on(table.scopeId, table.email),
    foreignKey({
      name: 'ReferralInvite_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ReferralInvite_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
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
    userId: text('userId').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerAccountId: text('providerAccountId').notNull(),
    refresh_token: text('refresh_token'),
    access_token: text('access_token'),
    expires_at: integer('expires_at'),
    token_type: text('token_type'),
    scope: text('scope'),
    id_token: text('id_token'),
    session_state: text('session_state'),
  },
  (table) => [
    uniqueIndex('Account_provider_providerAccountId_key').on(
      table.provider,
      table.providerAccountId,
    ),
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
    sessionToken: text('sessionToken').notNull(),
    userId: text('userId').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('Session_sessionToken_key').on(table.sessionToken),
    foreignKey({ name: 'Session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
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
    name: text('name'),
    email: text('email'),
    emailVerified: timestamp('emailVerified', { precision: 3 }),
    image: text('image'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    contactId: text('contactId'),
    plan: text('plan').notNull().default('free'),
    stripeId: text('stripeId'),
    subscriptionId: text('subscriptionId'),
    startsAt: timestamp('startsAt', { precision: 3 }),
    endsAt: timestamp('endsAt', { precision: 3 }),
    referralTrack: referralTrackEnum('referralTrack'),
    referralTrackSelectedAt: timestamp('referralTrackSelectedAt', { precision: 3 }),
    referralProfile: jsonb('referralProfile'),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    uniqueIndex('User_stripeId_key').on(table.stripeId),
    uniqueIndex('User_subscriptionId_key').on(table.subscriptionId),
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
    uniqueIndex('VerificationToken_token_key').on(table.token),
    uniqueIndex('VerificationToken_identifier_token_key').on(table.identifier, table.token),
  ],
)

export const domain = pgTable(
  'Domain',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    slug: text('slug').notNull(),
    userId: text('userId'),
    teamId: text('teamId').notNull(),
    verified: boolean('verified').notNull().default(false),
    isDefault: boolean('isDefault').notNull().default(false),
    redirectUrl: text('redirectUrl'),
    lastChecked: timestamp('lastChecked', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Domain_slug_key').on(table.slug),
    index('Domain_userId_idx').on(table.userId),
    index('Domain_teamId_idx').on(table.teamId),
    foreignKey({ name: 'Domain_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Domain_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const view = pgTable(
  'View',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    linkId: text('linkId').notNull(),
    documentId: text('documentId'),
    dataroomId: text('dataroomId'),
    dataroomViewId: text('dataroomViewId'),
    viewerEmail: text('viewerEmail'),
    viewerName: text('viewerName'),
    verified: boolean('verified').notNull().default(false),
    viewedAt: timestamp('viewedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    downloadedAt: timestamp('downloadedAt', { precision: 3 }),
    downloadType: downloadTypeEnum('downloadType'),
    downloadMetadata: jsonb('downloadMetadata'),
    viewType: viewTypeEnum('viewType').notNull().default('DOCUMENT_VIEW'),
    viewerId: text('viewerId'),
    groupId: text('groupId'),
    isArchived: boolean('isArchived').notNull().default(false),
    teamId: text('teamId'),
  },
  (table) => [
    index('View_linkId_idx').on(table.linkId),
    index('View_documentId_idx').on(table.documentId),
    index('View_dataroomId_idx').on(table.dataroomId),
    index('View_dataroomViewId_idx').on(table.dataroomViewId),
    index('View_viewerId_idx').on(table.viewerId),
    index('View_groupId_idx').on(table.groupId),
    index('View_teamId_idx').on(table.teamId),
    index('View_viewedAt_idx').on(table.viewedAt),
    index('View_viewerId_documentId_idx').on(table.viewerId, table.documentId),
    index('View_viewerEmail_idx').on(table.viewerEmail),
    index('View_documentId_isArchived_idx').on(table.documentId, table.isArchived),
    index('View_documentId_viewedAt_idx').on(table.documentId, table.viewedAt),
    foreignKey({ name: 'View_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'View_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'View_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'View_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'View_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'View_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const viewer = pgTable(
  'Viewer',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    email: text('email').notNull(),
    verified: boolean('verified').notNull().default(false),
    invitedAt: timestamp('invitedAt', { precision: 3 }),
    notificationPreferences: jsonb('notificationPreferences'),
    dataroomId: text('dataroomId'),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Viewer_teamId_idx').on(table.teamId),
    index('Viewer_dataroomId_idx').on(table.dataroomId),
    uniqueIndex('Viewer_teamId_email_key').on(table.teamId, table.email),
    foreignKey({
      name: 'Viewer_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({ name: 'Viewer_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const reaction = pgTable(
  'Reaction',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    viewId: text('viewId').notNull(),
    pageNumber: integer('pageNumber').notNull(),
    type: text('type').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('Reaction_viewId_idx').on(table.viewId),
    index('Reaction_viewId_type_idx').on(table.viewId, table.type),
    foreignKey({ name: 'Reaction_viewId_fkey', columns: [table.viewId], foreignColumns: [view.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const invitation = pgTable(
  'Invitation',
  {
    email: text('email').notNull(),
    expires: timestamp('expires', { precision: 3 }).notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    token: text('token').notNull(),
    role: roleEnum('role').notNull().default('MEMBER'),
    dataroomIds: text('dataroomIds').array().default([]),
  },
  (table) => [
    uniqueIndex('Invitation_token_key').on(table.token),
    uniqueIndex('Invitation_email_teamId_key').on(table.email, table.teamId),
    foreignKey({
      name: 'Invitation_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const sentEmail = pgTable(
  'SentEmail',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: emailTypeEnum('type').notNull(),
    recipient: text('recipient').notNull(),
    marketing: boolean('marketing').notNull().default(false),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    teamId: text('teamId').notNull(),
    domainSlug: text('domainSlug'),
  },
  (table) => [
    index('SentEmail_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'SentEmail_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chat = pgTable(
  'Chat',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    title: text('title'),
    teamId: text('teamId').notNull(),
    documentId: text('documentId'),
    dataroomId: text('dataroomId'),
    linkId: text('linkId'),
    viewId: text('viewId'),
    userId: text('userId'),
    viewerId: text('viewerId'),
    vectorStoreId: text('vectorStoreId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    lastMessageAt: timestamp('lastMessageAt', { precision: 3 }),
  },
  (table) => [
    index('Chat_teamId_idx').on(table.teamId),
    index('Chat_documentId_idx').on(table.documentId),
    index('Chat_dataroomId_idx').on(table.dataroomId),
    index('Chat_linkId_idx').on(table.linkId),
    index('Chat_userId_idx').on(table.userId),
    index('Chat_viewerId_idx').on(table.viewerId),
    index('Chat_viewId_idx').on(table.viewId),
    index('Chat_createdAt_idx').on(table.createdAt),
    foreignKey({ name: 'Chat_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Chat_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Chat_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Chat_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Chat_viewId_fkey', columns: [table.viewId], foreignColumns: [view.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Chat_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'Chat_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const chatMessage = pgTable(
  'ChatMessage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    chatId: text('chatId').notNull(),
    role: text('role').notNull(),
    content: text('content').notNull(),
    metadata: jsonb('metadata'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ChatMessage_chatId_idx').on(table.chatId),
    index('ChatMessage_chatId_createdAt_idx').on(table.chatId, table.createdAt),
    foreignKey({
      name: 'ChatMessage_chatId_fkey',
      columns: [table.chatId],
      foreignColumns: [chat.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feedback = pgTable(
  'Feedback',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    linkId: text('linkId').notNull(),
    data: jsonb('data').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Feedback_linkId_key').on(table.linkId),
    index('Feedback_linkId_idx').on(table.linkId),
    foreignKey({ name: 'Feedback_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const feedbackResponse = pgTable(
  'FeedbackResponse',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    feedbackId: text('feedbackId').notNull(),
    data: jsonb('data').notNull(),
    viewId: text('viewId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('FeedbackResponse_viewId_key').on(table.viewId),
    index('FeedbackResponse_feedbackId_idx').on(table.feedbackId),
    index('FeedbackResponse_viewId_idx').on(table.viewId),
    foreignKey({
      name: 'FeedbackResponse_feedbackId_fkey',
      columns: [table.feedbackId],
      foreignColumns: [feedback.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'FeedbackResponse_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const agreement = pgTable(
  'Agreement',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    content: text('content').notNull(),
    contentType: text('contentType').notNull().default('LINK'),
    signingProvider: text('signingProvider').notNull().default('LEGACY'),
    signingExternalId: text('signingExternalId'),
    signingEnvelopeId: text('signingEnvelopeId'),
    signingTemplateId: text('signingTemplateId'),
    requireName: boolean('requireName').notNull().default(true),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    deletedAt: timestamp('deletedAt', { precision: 3 }),
    deletedBy: text('deletedBy'),
  },
  (table) => [
    uniqueIndex('Agreement_signingExternalId_key').on(table.signingExternalId),
    index('Agreement_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'Agreement_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const agreementResponse = pgTable(
  'AgreementResponse',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    agreementId: text('agreementId').notNull(),
    viewId: text('viewId'),
    linkId: text('linkId'),
    signerEmail: text('signerEmail'),
    signerName: text('signerName'),
    signingStatus: text('signingStatus').notNull().default('PENDING'),
    signingExternalId: text('signingExternalId'),
    signingEnvelopeId: text('signingEnvelopeId'),
    signingDocumentId: integer('signingDocumentId'),
    signedAt: timestamp('signedAt', { precision: 3 }),
    completedAt: timestamp('completedAt', { precision: 3 }),
    signedFileKey: text('signedFileKey'),
    signedFileName: text('signedFileName'),
    signedFileStorageType: documentStorageTypeEnum('signedFileStorageType'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('AgreementResponse_viewId_key').on(table.viewId),
    uniqueIndex('AgreementResponse_signingExternalId_key').on(table.signingExternalId),
    index('AgreementResponse_agreementId_idx').on(table.agreementId),
    index('AgreementResponse_viewId_idx').on(table.viewId),
    index('AgreementResponse_linkId_idx').on(table.linkId),
    index('AgreementResponse_signerEmail_idx').on(table.signerEmail),
    foreignKey({
      name: 'AgreementResponse_agreementId_fkey',
      columns: [table.agreementId],
      foreignColumns: [agreement.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'AgreementResponse_viewId_fkey',
      columns: [table.viewId],
      foreignColumns: [view.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const incomingWebhook = pgTable(
  'IncomingWebhook',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    externalId: text('externalId').notNull(),
    name: text('name').notNull(),
    secret: text('secret'),
    source: text('source'),
    actions: text('actions'),
    consecutiveFailures: integer('consecutiveFailures').notNull().default(0),
    lastFailedAt: timestamp('lastFailedAt', { precision: 3 }),
    disabledAt: timestamp('disabledAt', { precision: 3 }),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('IncomingWebhook_externalId_key').on(table.externalId),
    index('IncomingWebhook_teamId_idx').on(table.teamId),
    foreignKey({
      name: 'IncomingWebhook_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const restrictedToken = pgTable(
  'RestrictedToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    hashedKey: text('hashedKey').notNull(),
    partialKey: text('partialKey').notNull(),
    scopes: text('scopes'),
    mode: text('mode').notNull().default('live'),
    source: text('source').notNull().default('dashboard'),
    subjectType: text('subjectType').notNull().default('user'),
    clientId: text('clientId'),
    expires: timestamp('expires', { precision: 3 }),
    lastUsed: timestamp('lastUsed', { precision: 3 }),
    rateLimit: integer('rateLimit').notNull().default(60),
    userId: text('userId').notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('RestrictedToken_hashedKey_key').on(table.hashedKey),
    index('RestrictedToken_userId_idx').on(table.userId),
    index('RestrictedToken_teamId_idx').on(table.teamId),
    index('RestrictedToken_hashedKey_idx').on(table.hashedKey),
    index('RestrictedToken_clientId_idx').on(table.clientId),
    foreignKey({
      name: 'RestrictedToken_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RestrictedToken_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'RestrictedToken_clientId_fkey',
      columns: [table.clientId],
      foreignColumns: [oAuthClient.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const webhook = pgTable(
  'Webhook',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    pId: text('pId').notNull(),
    name: text('name').notNull(),
    url: text('url').notNull(),
    secret: text('secret').notNull(),
    triggers: jsonb('triggers').notNull(),
    teamId: text('teamId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Webhook_pId_key').on(table.pId),
    index('Webhook_teamId_idx').on(table.teamId),
    foreignKey({ name: 'Webhook_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const yearInReview = pgTable(
  'YearInReview',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    teamId: text('teamId').notNull(),
    status: text('status').notNull().default('pending'),
    attempts: integer('attempts').notNull().default(0),
    lastAttempted: timestamp('lastAttempted', { precision: 3 }),
    error: text('error'),
    stats: jsonb('stats').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('YearInReview_status_attempts_idx').on(table.status, table.attempts),
    index('YearInReview_teamId_idx').on(table.teamId),
  ],
)

export const tag = pgTable(
  'Tag',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    color: text('color').notNull(),
    description: text('description'),
    teamId: text('teamId').notNull(),
    createdBy: text('createdBy'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('Tag_teamId_idx').on(table.teamId),
    index('Tag_name_idx').on(table.name),
    index('Tag_id_idx').on(table.id),
    uniqueIndex('Tag_teamId_name_key').on(table.teamId, table.name),
    foreignKey({ name: 'Tag_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const tagItem = pgTable(
  'TagItem',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tagId: text('tagId').notNull(),
    itemType: tagTypeEnum('itemType').notNull(),
    linkId: text('linkId'),
    documentId: text('documentId'),
    dataroomId: text('dataroomId'),
    taggedBy: text('taggedBy'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('TagItem_tagId_linkId_idx').on(table.tagId, table.linkId),
    index('TagItem_tagId_documentId_idx').on(table.tagId, table.documentId),
    index('TagItem_tagId_dataroomId_idx').on(table.tagId, table.dataroomId),
    foreignKey({ name: 'TagItem_tagId_fkey', columns: [table.tagId], foreignColumns: [tag.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'TagItem_linkId_fkey', columns: [table.linkId], foreignColumns: [link.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TagItem_documentId_fkey',
      columns: [table.documentId],
      foreignColumns: [document.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'TagItem_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const viewerInvitation = pgTable(
  'ViewerInvitation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    viewerId: text('viewerId').notNull(),
    linkId: text('linkId').notNull(),
    groupId: text('groupId'),
    invitedBy: text('invitedBy').notNull(),
    customMessage: text('customMessage'),
    sentAt: timestamp('sentAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    status: invitationStatusEnum('status').notNull().default('SENT'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('ViewerInvitation_viewerId_idx').on(table.viewerId),
    index('ViewerInvitation_linkId_idx').on(table.linkId),
    index('ViewerInvitation_groupId_idx').on(table.groupId),
    foreignKey({
      name: 'ViewerInvitation_viewerId_fkey',
      columns: [table.viewerId],
      foreignColumns: [viewer.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ViewerInvitation_linkId_fkey',
      columns: [table.linkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'ViewerInvitation_groupId_fkey',
      columns: [table.groupId],
      foreignColumns: [viewerGroup.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const team = pgTable(
  'Team',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    slug: text('slug'),
    defaultBrandId: text('defaultBrandId'),
    plan: text('plan').notNull().default('free'),
    stripeId: text('stripeId'),
    subscriptionId: text('subscriptionId'),
    startsAt: timestamp('startsAt', { precision: 3 }),
    endsAt: timestamp('endsAt', { precision: 3 }),
    pausedAt: timestamp('pausedAt', { precision: 3 }),
    pauseStartsAt: timestamp('pauseStartsAt', { precision: 3 }),
    pauseEndsAt: timestamp('pauseEndsAt', { precision: 3 }),
    cancelledAt: timestamp('cancelledAt', { precision: 3 }),
    trialStartedAt: timestamp('trialStartedAt', { precision: 3 }),
    trialEndsAt: timestamp('trialEndsAt', { precision: 3 }),
    trialReminderRunId: text('trialReminderRunId'),
    trialExpiredRunId: text('trialExpiredRunId'),
    limits: jsonb('limits'),
    enableExcelAdvancedMode: boolean('enableExcelAdvancedMode').notNull().default(false),
    replicateDataroomFolders: boolean('replicateDataroomFolders').notNull().default(true),
    timezone: text('timezone').notNull().default('Etc/UTC'),
    ssoEnabled: boolean('ssoEnabled').notNull().default(false),
    ssoEmailDomain: text('ssoEmailDomain'),
    ssoEnforcedAt: timestamp('ssoEnforcedAt', { precision: 3 }),
    surveyData: jsonb('surveyData'),
    agentsEnabled: boolean('agentsEnabled').notNull().default(false),
    vectorStoreId: text('vectorStoreId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    ignoredDomains: text('ignoredDomains').array(),
    globalBlockList: text('globalBlockList').array(),
    referralTrack: referralTrackEnum('referralTrack'),
    referralTrackSelectedAt: timestamp('referralTrackSelectedAt', { precision: 3 }),
    referralProfile: jsonb('referralProfile'),
  },
  (table): PgTableExtraConfigValue[] => [
    uniqueIndex('Team_slug_key').on(table.slug),
    uniqueIndex('Team_stripeId_key').on(table.stripeId),
    uniqueIndex('Team_subscriptionId_key').on(table.subscriptionId),
    uniqueIndex('Team_ssoEmailDomain_key').on(table.ssoEmailDomain),
    index('Team_defaultBrandId_idx').on(table.defaultBrandId),
    foreignKey({
      name: 'Team_defaultBrandId_fkey',
      columns: [table.defaultBrandId],
      foreignColumns: [brand.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const userDataroom = pgTable(
  'UserDataroom',
  {
    userId: text('userId').notNull(),
    teamId: text('teamId').notNull(),
    dataroomId: text('dataroomId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'UserDataroom_pkey', columns: [table.userId, table.dataroomId] }),
    index('UserDataroom_userId_idx').on(table.userId),
    index('UserDataroom_teamId_idx').on(table.teamId),
    index('UserDataroom_dataroomId_idx').on(table.dataroomId),
    foreignKey({
      name: 'UserDataroom_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserDataroom_teamId_fkey',
      columns: [table.teamId],
      foreignColumns: [team.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'UserDataroom_dataroomId_fkey',
      columns: [table.dataroomId],
      foreignColumns: [dataroom.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userTeam = pgTable(
  'UserTeam',
  {
    role: roleEnum('role').notNull().default('MEMBER'),
    status: text('status').notNull().default('ACTIVE'),
    userId: text('userId').notNull(),
    teamId: text('teamId').notNull(),
    blockedAt: timestamp('blockedAt', { precision: 3 }),
    notificationPreferences: jsonb('notificationPreferences'),
  },
  (table) => [
    primaryKey({ name: 'UserTeam_pkey', columns: [table.userId, table.teamId] }),
    index('UserTeam_userId_idx').on(table.userId),
    index('UserTeam_teamId_idx').on(table.teamId),
    foreignKey({ name: 'UserTeam_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'UserTeam_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflow = pgTable(
  'Workflow',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    entryLinkId: text('entryLinkId').notNull(),
    teamId: text('teamId').notNull(),
    isActive: boolean('isActive').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('Workflow_entryLinkId_key').on(table.entryLinkId),
    index('Workflow_entryLinkId_idx').on(table.entryLinkId),
    index('Workflow_teamId_idx').on(table.teamId),
    index('Workflow_isActive_idx').on(table.isActive),
    foreignKey({
      name: 'Workflow_entryLinkId_fkey',
      columns: [table.entryLinkId],
      foreignColumns: [link.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({ name: 'Workflow_teamId_fkey', columns: [table.teamId], foreignColumns: [team.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowStep = pgTable(
  'WorkflowStep',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    workflowId: text('workflowId').notNull(),
    name: text('name').notNull(),
    stepOrder: integer('stepOrder').notNull(),
    stepType: workflowStepTypeEnum('stepType').notNull().default('ROUTER'),
    conditions: jsonb('conditions').notNull(),
    actions: jsonb('actions').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('WorkflowStep_workflowId_idx').on(table.workflowId),
    uniqueIndex('WorkflowStep_workflowId_stepOrder_key').on(table.workflowId, table.stepOrder),
    foreignKey({
      name: 'WorkflowStep_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowExecution = pgTable(
  'WorkflowExecution',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    workflowId: text('workflowId').notNull(),
    visitorEmail: text('visitorEmail'),
    visitorIp: text('visitorIp'),
    status: executionStatusEnum('status').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    result: jsonb('result'),
    metadata: jsonb('metadata'),
  },
  (table) => [
    index('WorkflowExecution_workflowId_startedAt_idx').on(table.workflowId, table.startedAt),
    index('WorkflowExecution_visitorEmail_idx').on(table.visitorEmail),
    index('WorkflowExecution_status_idx').on(table.status),
    foreignKey({
      name: 'WorkflowExecution_workflowId_fkey',
      columns: [table.workflowId],
      foreignColumns: [workflow.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const workflowStepLog = pgTable(
  'WorkflowStepLog',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    executionId: text('executionId').notNull(),
    workflowStepId: text('workflowStepId').notNull(),
    conditionsMatched: boolean('conditionsMatched').notNull(),
    conditionResults: jsonb('conditionResults'),
    actionsExecuted: jsonb('actionsExecuted'),
    executedAt: timestamp('executedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    duration: integer('duration'),
    error: text('error'),
  },
  (table) => [
    index('WorkflowStepLog_executionId_idx').on(table.executionId),
    index('WorkflowStepLog_workflowStepId_idx').on(table.workflowStepId),
    foreignKey({
      name: 'WorkflowStepLog_executionId_fkey',
      columns: [table.executionId],
      foreignColumns: [workflowExecution.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'WorkflowStepLog_workflowStepId_fkey',
      columns: [table.workflowStepId],
      foreignColumns: [workflowStep.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    documentAnnotation,
    annotationImage,
    brand,
    conversation,
    dataroomQuestion,
    dataroomQuestionAssignment,
    conversationParticipant,
    message,
    conversationView,
    dataroomFaqItem,
    dataroom,
    taskList,
    task,
    taskAssignment,
    taskActivity,
    dataroomDocument,
    dataroomFolder,
    dataroomBrand,
    viewerGroup,
    viewerGroupMembership,
    viewerGroupAccessControls,
    permissionGroup,
    permissionGroupAccessControls,
    document,
    documentVersion,
    documentPage,
    folder,
    documentUpload,
    pendingDocumentUpload,
    integration,
    installedIntegration,
    jacksonIndex,
    jacksonStore,
    jacksonTtl,
    link,
    linkPreset,
    visitorGroup,
    linkVisitorGroup,
    customField,
    customFieldResponse,
    notificationPreference,
    oAuthClient,
    oAuthClientApproval,
    oAuthGrantTeam,
    oAuthRecord,
    documentRedactionJob,
    documentRedaction,
    referralInvite,
    account,
    session,
    user,
    verificationToken,
    domain,
    view,
    viewer,
    reaction,
    invitation,
    sentEmail,
    chat,
    chatMessage,
    feedback,
    feedbackResponse,
    agreement,
    agreementResponse,
    incomingWebhook,
    restrictedToken,
    webhook,
    yearInReview,
    tag,
    tagItem,
    viewerInvitation,
    team,
    userDataroom,
    userTeam,
    workflow,
    workflowStep,
    workflowExecution,
    workflowStepLog,
  },
  (r) => ({
    documentAnnotation: {
      document: r.one.document({
        from: r.documentAnnotation.documentId,
        to: r.document.id,
        optional: false,
      }),
      team: r.one.team({ from: r.documentAnnotation.teamId, to: r.team.id, optional: false }),
      createdBy: r.one.user({
        from: r.documentAnnotation.createdById,
        to: r.user.id,
        optional: false,
      }),
      images: r.many.annotationImage(),
    },
    annotationImage: {
      annotation: r.one.documentAnnotation({
        from: r.annotationImage.annotationId,
        to: r.documentAnnotation.id,
        optional: false,
      }),
    },
    brand: {
      team: r.one.team({
        from: r.brand.teamId,
        to: r.team.id,
        optional: false,
        alias: 'TeamBrands',
      }),
      defaultForTeam: r.many.team({ alias: 'TeamDefaultBrand' }),
      links: r.many.link(),
      datarooms: r.many.dataroom(),
    },
    conversation: {
      dataroom: r.one.dataroom({
        from: r.conversation.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      dataroomDocument: r.one.dataroomDocument({
        from: r.conversation.dataroomDocumentId,
        to: r.dataroomDocument.id,
      }),
      link: r.one.link({ from: r.conversation.linkId, to: r.link.id }),
      viewerGroup: r.one.viewerGroup({ from: r.conversation.viewerGroupId, to: r.viewerGroup.id }),
      question: r.one.dataroomQuestion({
        from: r.conversation.questionId,
        to: r.dataroomQuestion.id,
      }),
      recipientViewer: r.one.viewer({ from: r.conversation.recipientViewerId, to: r.viewer.id }),
      initialView: r.one.view({ from: r.conversation.initialViewId, to: r.view.id }),
      team: r.one.team({ from: r.conversation.teamId, to: r.team.id, optional: false }),
      views: r.many.conversationView(),
      participants: r.many.conversationParticipant(),
      messages: r.many.message(),
      faqItems: r.many.dataroomFaqItem(),
    },
    dataroomQuestion: {
      dataroom: r.one.dataroom({
        from: r.dataroomQuestion.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      team: r.one.team({ from: r.dataroomQuestion.teamId, to: r.team.id, optional: false }),
      createdByUser: r.one.user({ from: r.dataroomQuestion.createdByUserId, to: r.user.id }),
      dataroomDocument: r.one.dataroomDocument({
        from: r.dataroomQuestion.dataroomDocumentId,
        to: r.dataroomDocument.id,
      }),
      assignments: r.many.dataroomQuestionAssignment(),
      conversations: r.many.conversation(),
    },
    dataroomQuestionAssignment: {
      question: r.one.dataroomQuestion({
        from: r.dataroomQuestionAssignment.questionId,
        to: r.dataroomQuestion.id,
        optional: false,
      }),
      link: r.one.link({ from: r.dataroomQuestionAssignment.linkId, to: r.link.id }),
      group: r.one.viewerGroup({
        from: r.dataroomQuestionAssignment.groupId,
        to: r.viewerGroup.id,
      }),
      viewer: r.one.viewer({ from: r.dataroomQuestionAssignment.viewerId, to: r.viewer.id }),
    },
    conversationParticipant: {
      conversation: r.one.conversation({
        from: r.conversationParticipant.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
      viewer: r.one.viewer({ from: r.conversationParticipant.viewerId, to: r.viewer.id }),
      user: r.one.user({ from: r.conversationParticipant.userId, to: r.user.id }),
    },
    message: {
      conversation: r.one.conversation({
        from: r.message.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
      user: r.one.user({ from: r.message.userId, to: r.user.id }),
      viewer: r.one.viewer({ from: r.message.viewerId, to: r.viewer.id }),
      view: r.one.view({ from: r.message.viewId, to: r.view.id }),
      faqAsQuestion: r.many.dataroomFaqItem({ alias: 'FAQQuestionMessage' }),
      faqAsAnswer: r.many.dataroomFaqItem({ alias: 'FAQAnswerMessage' }),
    },
    conversationView: {
      conversation: r.one.conversation({
        from: r.conversationView.conversationId,
        to: r.conversation.id,
        optional: false,
      }),
      view: r.one.view({ from: r.conversationView.viewId, to: r.view.id, optional: false }),
    },
    dataroomFaqItem: {
      dataroom: r.one.dataroom({
        from: r.dataroomFaqItem.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      link: r.one.link({ from: r.dataroomFaqItem.linkId, to: r.link.id }),
      dataroomDocument: r.one.dataroomDocument({
        from: r.dataroomFaqItem.dataroomDocumentId,
        to: r.dataroomDocument.id,
      }),
      sourceConversation: r.one.conversation({
        from: r.dataroomFaqItem.sourceConversationId,
        to: r.conversation.id,
      }),
      questionMessage: r.one.message({
        from: r.dataroomFaqItem.questionMessageId,
        to: r.message.id,
        alias: 'FAQQuestionMessage',
      }),
      answerMessage: r.one.message({
        from: r.dataroomFaqItem.answerMessageId,
        to: r.message.id,
        alias: 'FAQAnswerMessage',
      }),
      team: r.one.team({ from: r.dataroomFaqItem.teamId, to: r.team.id, optional: false }),
      publishedByUser: r.one.user({
        from: r.dataroomFaqItem.publishedByUserId,
        to: r.user.id,
        optional: false,
      }),
    },
    dataroom: {
      team: r.one.team({ from: r.dataroom.teamId, to: r.team.id, optional: false }),
      documents: r.many.dataroomDocument(),
      folders: r.many.dataroomFolder(),
      links: r.many.link(),
      views: r.many.view(),
      viewers: r.many.viewer(),
      viewerGroups: r.many.viewerGroup(),
      brand: r.one.dataroomBrand({ from: r.dataroom.id, to: r.dataroomBrand.dataroomId }),
      baseBrand: r.one.brand({ from: r.dataroom.brandId, to: r.brand.id }),
      userDatarooms: r.many.userDataroom(),
      permissionGroups: r.many.permissionGroup(),
      conversations: r.many.conversation(),
      questions: r.many.dataroomQuestion(),
      faqItems: r.many.dataroomFaqItem(),
      chats: r.many.chat(),
      uploadedDocuments: r.many.documentUpload(),
      tags: r.many.tagItem(),
      taskLists: r.many.taskList(),
      tasks: r.many.task(),
      frozenByUser: r.one.user({ from: r.dataroom.frozenBy, to: r.user.id }),
    },
    taskList: {
      dataroom: r.one.dataroom({ from: r.taskList.dataroomId, to: r.dataroom.id, optional: false }),
      team: r.one.team({ from: r.taskList.teamId, to: r.team.id, optional: false }),
      tasks: r.many.task(),
    },
    task: {
      taskList: r.one.taskList({ from: r.task.taskListId, to: r.taskList.id, optional: false }),
      dataroom: r.one.dataroom({ from: r.task.dataroomId, to: r.dataroom.id, optional: false }),
      team: r.one.team({ from: r.task.teamId, to: r.team.id, optional: false }),
      uploadFolder: r.one.dataroomFolder({ from: r.task.uploadFolderId, to: r.dataroomFolder.id }),
      createdByUser: r.one.user({ from: r.task.createdByUserId, to: r.user.id }),
      assignments: r.many.taskAssignment(),
      activities: r.many.taskActivity(),
      uploads: r.many.documentUpload(),
    },
    taskAssignment: {
      task: r.one.task({ from: r.taskAssignment.taskId, to: r.task.id, optional: false }),
      viewer: r.one.viewer({ from: r.taskAssignment.viewerId, to: r.viewer.id }),
      group: r.one.viewerGroup({ from: r.taskAssignment.groupId, to: r.viewerGroup.id }),
      link: r.one.link({ from: r.taskAssignment.linkId, to: r.link.id }),
    },
    taskActivity: {
      task: r.one.task({ from: r.taskActivity.taskId, to: r.task.id, optional: false }),
      user: r.one.user({ from: r.taskActivity.userId, to: r.user.id }),
      viewer: r.one.viewer({ from: r.taskActivity.viewerId, to: r.viewer.id }),
      view: r.one.view({ from: r.taskActivity.viewId, to: r.view.id }),
    },
    dataroomDocument: {
      dataroom: r.one.dataroom({
        from: r.dataroomDocument.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      document: r.one.document({
        from: r.dataroomDocument.documentId,
        to: r.document.id,
        optional: false,
      }),
      folder: r.one.dataroomFolder({ from: r.dataroomDocument.folderId, to: r.dataroomFolder.id }),
      conversations: r.many.conversation(),
      questions: r.many.dataroomQuestion(),
      dataroomFAQs: r.many.dataroomFaqItem(),
      uploadedDocuments: r.many.documentUpload(),
    },
    dataroomFolder: {
      documents: r.many.dataroomDocument(),
      childFolders: r.many.dataroomFolder({ alias: 'SubFolders' }),
      parentFolder: r.one.dataroomFolder({
        from: r.dataroomFolder.parentId,
        to: r.dataroomFolder.id,
        alias: 'SubFolders',
      }),
      uploadTasks: r.many.task(),
      dataroom: r.one.dataroom({
        from: r.dataroomFolder.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
    },
    dataroomBrand: {
      dataroom: r.one.dataroom({
        from: r.dataroomBrand.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
    },
    viewerGroup: {
      members: r.many.viewerGroupMembership(),
      links: r.many.link(),
      accessControls: r.many.viewerGroupAccessControls(),
      dataroom: r.one.dataroom({
        from: r.viewerGroup.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      team: r.one.team({ from: r.viewerGroup.teamId, to: r.team.id, optional: false }),
      views: r.many.view(),
      conversations: r.many.conversation(),
      invitations: r.many.viewerInvitation(),
      questionAssignments: r.many.dataroomQuestionAssignment(),
      taskAssignments: r.many.taskAssignment(),
    },
    viewerGroupMembership: {
      viewer: r.one.viewer({
        from: r.viewerGroupMembership.viewerId,
        to: r.viewer.id,
        optional: false,
      }),
      group: r.one.viewerGroup({
        from: r.viewerGroupMembership.groupId,
        to: r.viewerGroup.id,
        optional: false,
      }),
    },
    viewerGroupAccessControls: {
      group: r.one.viewerGroup({
        from: r.viewerGroupAccessControls.groupId,
        to: r.viewerGroup.id,
        optional: false,
      }),
    },
    permissionGroup: {
      links: r.many.link(),
      accessControls: r.many.permissionGroupAccessControls(),
      dataroom: r.one.dataroom({
        from: r.permissionGroup.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
      team: r.one.team({ from: r.permissionGroup.teamId, to: r.team.id, optional: false }),
    },
    permissionGroupAccessControls: {
      group: r.one.permissionGroup({
        from: r.permissionGroupAccessControls.groupId,
        to: r.permissionGroup.id,
        optional: false,
      }),
    },
    document: {
      owner: r.one.user({ from: r.document.ownerId, to: r.user.id }),
      team: r.one.team({ from: r.document.teamId, to: r.team.id, optional: false }),
      links: r.many.link(),
      views: r.many.view(),
      versions: r.many.documentVersion(),
      chats: r.many.chat(),
      folder: r.one.folder({ from: r.document.folderId, to: r.folder.id }),
      datarooms: r.many.dataroomDocument(),
      tags: r.many.tagItem(),
      annotations: r.many.documentAnnotation(),
      redactionJobs: r.many.documentRedactionJob(),
      uploadedDocument: r.many.documentUpload(),
    },
    documentVersion: {
      document: r.one.document({
        from: r.documentVersion.documentId,
        to: r.document.id,
        optional: false,
      }),
      pages: r.many.documentPage(),
      redactionJobs: r.many.documentRedactionJob(),
    },
    documentPage: {
      version: r.one.documentVersion({
        from: r.documentPage.versionId,
        to: r.documentVersion.id,
        optional: false,
      }),
    },
    folder: {
      documents: r.many.document(),
      childFolders: r.many.folder({ alias: 'SubFolders' }),
      parentFolder: r.one.folder({ from: r.folder.parentId, to: r.folder.id, alias: 'SubFolders' }),
      team: r.one.team({ from: r.folder.teamId, to: r.team.id, optional: false }),
    },
    documentUpload: {
      document: r.one.document({
        from: r.documentUpload.documentId,
        to: r.document.id,
        optional: false,
      }),
      team: r.one.team({ from: r.documentUpload.teamId, to: r.team.id, optional: false }),
      viewer: r.one.viewer({ from: r.documentUpload.viewerId, to: r.viewer.id }),
      view: r.one.view({ from: r.documentUpload.viewId, to: r.view.id }),
      link: r.one.link({ from: r.documentUpload.linkId, to: r.link.id, optional: false }),
      dataroom: r.one.dataroom({ from: r.documentUpload.dataroomId, to: r.dataroom.id }),
      dataroomDocument: r.one.dataroomDocument({
        from: r.documentUpload.dataroomDocumentId,
        to: r.dataroomDocument.id,
      }),
      task: r.one.task({ from: r.documentUpload.taskId, to: r.task.id }),
    },
    integration: { installedIntegrations: r.many.installedIntegration() },
    installedIntegration: {
      integration: r.one.integration({
        from: r.installedIntegration.integrationId,
        to: r.integration.id,
        optional: false,
      }),
      user: r.one.user({ from: r.installedIntegration.userId, to: r.user.id }),
      team: r.one.team({ from: r.installedIntegration.teamId, to: r.team.id, optional: false }),
    },
    link: {
      document: r.one.document({ from: r.link.documentId, to: r.document.id }),
      dataroom: r.one.dataroom({ from: r.link.dataroomId, to: r.dataroom.id }),
      views: r.many.view(),
      domain: r.one.domain({ from: r.link.domainId, to: r.domain.id }),
      feedback: r.one.feedback({ from: r.link.id, to: r.feedback.linkId }),
      agreement: r.one.agreement({ from: r.link.agreementId, to: r.agreement.id }),
      group: r.one.viewerGroup({ from: r.link.groupId, to: r.viewerGroup.id }),
      permissionGroup: r.one.permissionGroup({
        from: r.link.permissionGroupId,
        to: r.permissionGroup.id,
      }),
      conversations: r.many.conversation(),
      questionAssignments: r.many.dataroomQuestionAssignment(),
      dataroomFaqItems: r.many.dataroomFaqItem(),
      chats: r.many.chat(),
      uploadedDocuments: r.many.documentUpload(),
      team: r.one.team({ from: r.link.teamId, to: r.team.id }),
      brand: r.one.brand({ from: r.link.brandId, to: r.brand.id }),
      owner: r.one.user({ from: r.link.ownerId, to: r.user.id }),
      customFields: r.many.customField(),
      tags: r.many.tagItem(),
      viewerInvitations: r.many.viewerInvitation(),
      visitorGroups: r.many.linkVisitorGroup(),
      taskAssignments: r.many.taskAssignment(),
      workflow: r.one.workflow({ from: r.link.id, to: r.workflow.entryLinkId }),
    },
    linkPreset: { team: r.one.team({ from: r.linkPreset.teamId, to: r.team.id, optional: false }) },
    visitorGroup: {
      team: r.one.team({ from: r.visitorGroup.teamId, to: r.team.id, optional: false }),
      links: r.many.linkVisitorGroup(),
    },
    linkVisitorGroup: {
      link: r.one.link({ from: r.linkVisitorGroup.linkId, to: r.link.id, optional: false }),
      visitorGroup: r.one.visitorGroup({
        from: r.linkVisitorGroup.visitorGroupId,
        to: r.visitorGroup.id,
        optional: false,
      }),
    },
    customField: {
      link: r.one.link({ from: r.customField.linkId, to: r.link.id, optional: false }),
    },
    customFieldResponse: {
      view: r.one.view({ from: r.customFieldResponse.viewId, to: r.view.id, optional: false }),
    },
    notificationPreference: {
      user: r.one.user({ from: r.notificationPreference.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.notificationPreference.teamId, to: r.team.id, optional: false }),
    },
    oAuthClient: { tokens: r.many.restrictedToken() },
    documentRedactionJob: {
      document: r.one.document({
        from: r.documentRedactionJob.documentId,
        to: r.document.id,
        optional: false,
      }),
      documentVersion: r.one.documentVersion({
        from: r.documentRedactionJob.documentVersionId,
        to: r.documentVersion.id,
        optional: false,
      }),
      team: r.one.team({ from: r.documentRedactionJob.teamId, to: r.team.id, optional: false }),
      createdBy: r.one.user({
        from: r.documentRedactionJob.createdById,
        to: r.user.id,
        optional: false,
      }),
      redactions: r.many.documentRedaction(),
    },
    documentRedaction: {
      job: r.one.documentRedactionJob({
        from: r.documentRedaction.jobId,
        to: r.documentRedactionJob.id,
        optional: false,
      }),
    },
    referralInvite: {
      user: r.one.user({ from: r.referralInvite.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.referralInvite.teamId, to: r.team.id }),
    },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    user: {
      accounts: r.many.account(),
      sessions: r.many.session(),
      documents: r.many.document(),
      teams: r.many.userTeam(),
      userDatarooms: r.many.userDataroom(),
      domains: r.many.domain(),
      chats: r.many.chat(),
      restrictedTokens: r.many.restrictedToken(),
      participatedConversations: r.many.conversationParticipant(),
      messages: r.many.message(),
      createdDataroomQuestions: r.many.dataroomQuestion(),
      publishedFaqItems: r.many.dataroomFaqItem(),
      createdAnnotations: r.many.documentAnnotation(),
      createdRedactionJobs: r.many.documentRedactionJob(),
      installedIntegrations: r.many.installedIntegration(),
      links: r.many.link(),
      frozenDatarooms: r.many.dataroom(),
      createdTasks: r.many.task(),
      taskActivities: r.many.taskActivity(),
      notificationPreferences: r.many.notificationPreference(),
      referralInvites: r.many.referralInvite(),
    },
    domain: {
      user: r.one.user({ from: r.domain.userId, to: r.user.id }),
      Team: r.one.team({ from: r.domain.teamId, to: r.team.id, optional: false }),
      links: r.many.link(),
    },
    view: {
      link: r.one.link({ from: r.view.linkId, to: r.link.id, optional: false }),
      document: r.one.document({ from: r.view.documentId, to: r.document.id }),
      dataroom: r.one.dataroom({ from: r.view.dataroomId, to: r.dataroom.id }),
      reactions: r.many.reaction(),
      viewer: r.one.viewer({ from: r.view.viewerId, to: r.viewer.id }),
      group: r.one.viewerGroup({ from: r.view.groupId, to: r.viewerGroup.id }),
      feedbackResponse: r.one.feedbackResponse({ from: r.view.id, to: r.feedbackResponse.viewId }),
      agreementResponse: r.one.agreementResponse({
        from: r.view.id,
        to: r.agreementResponse.viewId,
      }),
      customFieldResponse: r.one.customFieldResponse({
        from: r.view.id,
        to: r.customFieldResponse.viewId,
      }),
      conversationViews: r.many.conversationView(),
      messages: r.many.message(),
      initialConversations: r.many.conversation(),
      uploadedDocuments: r.many.documentUpload(),
      taskActivities: r.many.taskActivity(),
      chats: r.many.chat(),
      team: r.one.team({ from: r.view.teamId, to: r.team.id }),
    },
    viewer: {
      dataroom: r.one.dataroom({ from: r.viewer.dataroomId, to: r.dataroom.id }),
      team: r.one.team({ from: r.viewer.teamId, to: r.team.id, optional: false }),
      views: r.many.view(),
      groups: r.many.viewerGroupMembership(),
      invitations: r.many.viewerInvitation(),
      participatedConversations: r.many.conversationParticipant(),
      messages: r.many.message(),
      recipientConversations: r.many.conversation(),
      questionAssignments: r.many.dataroomQuestionAssignment(),
      uploadedDocuments: r.many.documentUpload(),
      taskAssignments: r.many.taskAssignment(),
      taskActivities: r.many.taskActivity(),
      chats: r.many.chat(),
    },
    reaction: { view: r.one.view({ from: r.reaction.viewId, to: r.view.id, optional: false }) },
    invitation: { team: r.one.team({ from: r.invitation.teamId, to: r.team.id, optional: false }) },
    sentEmail: { team: r.one.team({ from: r.sentEmail.teamId, to: r.team.id, optional: false }) },
    chat: {
      team: r.one.team({ from: r.chat.teamId, to: r.team.id, optional: false }),
      document: r.one.document({ from: r.chat.documentId, to: r.document.id }),
      dataroom: r.one.dataroom({ from: r.chat.dataroomId, to: r.dataroom.id }),
      link: r.one.link({ from: r.chat.linkId, to: r.link.id }),
      view: r.one.view({ from: r.chat.viewId, to: r.view.id }),
      user: r.one.user({ from: r.chat.userId, to: r.user.id }),
      viewer: r.one.viewer({ from: r.chat.viewerId, to: r.viewer.id }),
      messages: r.many.chatMessage(),
    },
    chatMessage: {
      chat: r.one.chat({ from: r.chatMessage.chatId, to: r.chat.id, optional: false }),
    },
    feedback: {
      link: r.one.link({ from: r.feedback.linkId, to: r.link.id, optional: false }),
      responses: r.many.feedbackResponse(),
    },
    feedbackResponse: {
      feedback: r.one.feedback({
        from: r.feedbackResponse.feedbackId,
        to: r.feedback.id,
        optional: false,
      }),
      view: r.one.view({ from: r.feedbackResponse.viewId, to: r.view.id, optional: false }),
    },
    agreement: {
      links: r.many.link(),
      responses: r.many.agreementResponse(),
      team: r.one.team({ from: r.agreement.teamId, to: r.team.id, optional: false }),
    },
    agreementResponse: {
      agreement: r.one.agreement({
        from: r.agreementResponse.agreementId,
        to: r.agreement.id,
        optional: false,
      }),
      view: r.one.view({ from: r.agreementResponse.viewId, to: r.view.id }),
    },
    incomingWebhook: {
      team: r.one.team({ from: r.incomingWebhook.teamId, to: r.team.id, optional: false }),
    },
    restrictedToken: {
      user: r.one.user({ from: r.restrictedToken.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.restrictedToken.teamId, to: r.team.id, optional: false }),
      oauthClient: r.one.oAuthClient({ from: r.restrictedToken.clientId, to: r.oAuthClient.id }),
    },
    webhook: { team: r.one.team({ from: r.webhook.teamId, to: r.team.id, optional: false }) },
    tag: {
      team: r.one.team({ from: r.tag.teamId, to: r.team.id, optional: false }),
      items: r.many.tagItem(),
    },
    tagItem: {
      tag: r.one.tag({ from: r.tagItem.tagId, to: r.tag.id, optional: false }),
      link: r.one.link({ from: r.tagItem.linkId, to: r.link.id }),
      document: r.one.document({ from: r.tagItem.documentId, to: r.document.id }),
      dataroom: r.one.dataroom({ from: r.tagItem.dataroomId, to: r.dataroom.id }),
    },
    viewerInvitation: {
      viewer: r.one.viewer({ from: r.viewerInvitation.viewerId, to: r.viewer.id, optional: false }),
      link: r.one.link({ from: r.viewerInvitation.linkId, to: r.link.id, optional: false }),
      group: r.one.viewerGroup({ from: r.viewerInvitation.groupId, to: r.viewerGroup.id }),
    },
    team: {
      users: r.many.userTeam(),
      userDatarooms: r.many.userDataroom(),
      documents: r.many.document(),
      folders: r.many.folder(),
      domains: r.many.domain(),
      invitations: r.many.invitation(),
      sentEmails: r.many.sentEmail(),
      brands: r.many.brand({ alias: 'TeamBrands' }),
      defaultBrand: r.one.brand({
        from: r.team.defaultBrandId,
        to: r.brand.id,
        alias: 'TeamDefaultBrand',
      }),
      datarooms: r.many.dataroom(),
      agreements: r.many.agreement(),
      viewerGroups: r.many.viewerGroup(),
      visitorGroups: r.many.visitorGroup(),
      viewers: r.many.viewer(),
      permissionGroups: r.many.permissionGroup(),
      linkPresets: r.many.linkPreset(),
      incomingWebhooks: r.many.incomingWebhook(),
      restrictedTokens: r.many.restrictedToken(),
      webhooks: r.many.webhook(),
      conversations: r.many.conversation(),
      dataroomQuestions: r.many.dataroomQuestion(),
      dataroomFaqItems: r.many.dataroomFaqItem(),
      uploadedDocuments: r.many.documentUpload(),
      Tag: r.many.tag(),
      annotations: r.many.documentAnnotation(),
      redactionJobs: r.many.documentRedactionJob(),
      installedIntegrations: r.many.installedIntegration(),
      links: r.many.link(),
      views: r.many.view(),
      workflows: r.many.workflow(),
      taskLists: r.many.taskList(),
      tasks: r.many.task(),
      chats: r.many.chat(),
      notificationPreferences: r.many.notificationPreference(),
      referralInvites: r.many.referralInvite(),
    },
    userDataroom: {
      user: r.one.user({ from: r.userDataroom.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.userDataroom.teamId, to: r.team.id, optional: false }),
      dataroom: r.one.dataroom({
        from: r.userDataroom.dataroomId,
        to: r.dataroom.id,
        optional: false,
      }),
    },
    userTeam: {
      user: r.one.user({ from: r.userTeam.userId, to: r.user.id, optional: false }),
      team: r.one.team({ from: r.userTeam.teamId, to: r.team.id, optional: false }),
    },
    workflow: {
      entryLink: r.one.link({ from: r.workflow.entryLinkId, to: r.link.id, optional: false }),
      team: r.one.team({ from: r.workflow.teamId, to: r.team.id, optional: false }),
      steps: r.many.workflowStep(),
      executions: r.many.workflowExecution(),
    },
    workflowStep: {
      workflow: r.one.workflow({
        from: r.workflowStep.workflowId,
        to: r.workflow.id,
        optional: false,
      }),
      executionLogs: r.many.workflowStepLog(),
    },
    workflowExecution: {
      workflow: r.one.workflow({
        from: r.workflowExecution.workflowId,
        to: r.workflow.id,
        optional: false,
      }),
      stepLogs: r.many.workflowStepLog(),
    },
    workflowStepLog: {
      execution: r.one.workflowExecution({
        from: r.workflowStepLog.executionId,
        to: r.workflowExecution.id,
        optional: false,
      }),
      step: r.one.workflowStep({
        from: r.workflowStepLog.workflowStepId,
        to: r.workflowStep.id,
        optional: false,
      }),
    },
  }),
)

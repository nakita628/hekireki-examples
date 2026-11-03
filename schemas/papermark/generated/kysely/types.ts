import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ConversationVisibility =
  | 'PRIVATE'
  | 'PUBLIC_LINK'
  | 'PUBLIC_GROUP'
  | 'PUBLIC_DOCUMENT'
  | 'PUBLIC_DATAROOM'

export type ParticipantRole = 'OWNER' | 'PARTICIPANT'

export type FaqVisibility = 'PUBLIC_DATAROOM' | 'PUBLIC_LINK' | 'PUBLIC_DOCUMENT'

export type FaqStatus = 'DRAFT' | 'PUBLISHED' | 'ARCHIVED'

export type ItemType = 'DATAROOM_DOCUMENT' | 'DATAROOM_FOLDER'

export type DefaultPermissionStrategy =
  | 'INHERIT_FROM_PARENT'
  | 'ASK_EVERY_TIME'
  | 'HIDDEN_BY_DEFAULT'

export type RootItemAccess = 'VIEW_ONLY' | 'VIEW_AND_DOWNLOAD' | 'HIDDEN'

export type DocumentStorageType = 'S3_PATH' | 'VERCEL_BLOB'

export type LinkType = 'DOCUMENT_LINK' | 'DATAROOM_LINK' | 'WORKFLOW_LINK'

export type LinkAudienceType = 'GENERAL' | 'GROUP' | 'TEAM'

export type CustomFieldType =
  | 'SHORT_TEXT'
  | 'LONG_TEXT'
  | 'NUMBER'
  | 'PHONE_NUMBER'
  | 'URL'
  | 'CHECKBOX'
  | 'SELECT'
  | 'MULTI_SELECT'

export type ReferralTrack = 'AFFILIATE' | 'PARTNER_DISCOUNT'

export type ViewType = 'DOCUMENT_VIEW' | 'DATAROOM_VIEW'

export type DownloadType = 'SINGLE' | 'BULK' | 'FOLDER'

export type EmailType =
  | 'FIRST_DAY_DOMAIN_REMINDER_EMAIL'
  | 'FIRST_DOMAIN_INVALID_EMAIL'
  | 'SECOND_DOMAIN_INVALID_EMAIL'
  | 'FIRST_TRIAL_END_REMINDER_EMAIL'
  | 'FINAL_TRIAL_END_REMINDER_EMAIL'

export type TagType = 'LINK_TAG' | 'DOCUMENT_TAG' | 'DATAROOM_TAG'

export type InvitationStatus = 'SENT' | 'FAILED' | 'BOUNCED'

export type Role = 'ADMIN' | 'MANAGER' | 'MEMBER' | 'DATAROOM_MEMBER'

export type WorkflowStepType = 'ROUTER'

export type ExecutionStatus = 'PENDING' | 'IN_PROGRESS' | 'COMPLETED' | 'FAILED' | 'BLOCKED'

export interface DocumentAnnotation {
  id: string
  title: string
  content: unknown
  pages: number[]
  documentId: string
  teamId: string
  createdById: string
  isVisible: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface AnnotationImage {
  id: string
  filename: string
  url: string
  size: number | null
  mimeType: string | null
  annotationId: string
  createdAt: Generated<Timestamp>
}

export interface Brand {
  id: string
  logo: string | null
  hideLogo: Generated<boolean>
  banner: string | null
  brandColor: string | null
  accentColor: string | null
  accentButtonColor: string | null
  applyAccentColorToDataroomView: Generated<boolean>
  welcomeMessage: string | null
  ctaLabel: string | null
  ctaUrl: string | null
  privacyPolicyUrl: string | null
  cardLayout: Generated<string>
  showFolderTree: Generated<boolean>
  viewerLayoutPreset: Generated<string>
  viewerHeaderStyle: Generated<string>
  hideFolderIconsInMain: Generated<boolean>
  customLinkPreviewEnabled: Generated<boolean>
  linkPreviewTitle: string | null
  linkPreviewDescription: string | null
  linkPreviewImage: string | null
  linkPreviewFavicon: string | null
  defaultLanguage: Generated<string>
  name: Generated<string>
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Conversation {
  id: string
  title: string | null
  isEnabled: Generated<boolean>
  visibilityMode: Generated<ConversationVisibility>
  dataroomId: string
  dataroomDocumentId: string | null
  documentVersionNumber: number | null
  documentPageNumber: number | null
  linkId: string | null
  viewerGroupId: string | null
  questionId: string | null
  origin: Generated<string>
  category: string | null
  priority: string | null
  recipientViewerId: string | null
  initialViewId: string | null
  teamId: string
  lastMessageAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DataroomQuestion {
  id: string
  content: string
  category: string | null
  priority: string | null
  status: Generated<string>
  orderIndex: number | null
  importBatchId: string | null
  dataroomId: string
  teamId: string
  createdByUserId: string | null
  dataroomDocumentId: string | null
  documentPageNumber: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DataroomQuestionAssignment {
  id: string
  questionId: string
  linkId: string | null
  groupId: string | null
  viewerId: string | null
  email: string | null
  createdAt: Generated<Timestamp>
}

export interface ConversationParticipant {
  id: string
  conversationId: string
  role: Generated<ParticipantRole>
  viewerId: string | null
  userId: string | null
  receiveNotifications: Generated<boolean>
  createdAt: Generated<Timestamp>
}

export interface Message {
  id: string
  content: string
  conversationId: string
  userId: string | null
  viewerId: string | null
  viewId: string | null
  isRead: Generated<boolean>
  isInternalNote: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ConversationView {
  id: string
  conversationId: string
  viewId: string
  createdAt: Generated<Timestamp>
}

export interface DataroomFaqItem {
  id: string
  title: string | null
  editedQuestion: string
  originalQuestion: string | null
  answer: string
  description: string | null
  dataroomId: string
  linkId: string | null
  dataroomDocumentId: string | null
  sourceConversationId: string | null
  questionMessageId: string | null
  answerMessageId: string | null
  teamId: string
  publishedByUserId: string
  visibilityMode: Generated<FaqVisibility>
  status: Generated<FaqStatus>
  isAnonymized: Generated<boolean>
  viewCount: Generated<number>
  tags: Generated<string[]>
  documentPageNumber: number | null
  documentVersionNumber: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Dataroom {
  id: string
  pId: string
  name: string
  internalName: string | null
  description: string | null
  teamId: string
  brandId: string | null
  conversationsEnabled: Generated<boolean>
  agentsEnabled: Generated<boolean>
  vectorStoreId: string | null
  requestListEnabled: Generated<boolean>
  enableChangeNotifications: Generated<boolean>
  enableVisitorUploadChangeNotifications: Generated<boolean>
  defaultPermissionStrategy: Generated<DefaultPermissionStrategy>
  defaultShowBanner: Generated<boolean>
  defaultGroupPermissionStrategy: Generated<DefaultPermissionStrategy>
  defaultRootItemAccess: Generated<RootItemAccess>
  defaultGroupRootItemAccess: Generated<RootItemAccess>
  allowBulkDownload: Generated<boolean>
  showLastUpdated: Generated<boolean>
  introductionEnabled: Generated<boolean>
  introductionContent: unknown
  isFrozen: Generated<boolean>
  frozenAt: Timestamp | null
  frozenBy: string | null
  freezeArchiveUrl: string | null
  freezeArchiveHash: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskList {
  id: string
  name: string
  orderIndex: number | null
  dataroomId: string
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Task {
  id: string
  title: string
  description: string | null
  type: Generated<string>
  status: Generated<string>
  category: string | null
  dueDate: Timestamp | null
  orderIndex: number | null
  taskListId: string
  dataroomId: string
  teamId: string
  uploadFolderId: string | null
  createdByUserId: string | null
  lastActivityNotifiedAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskAssignment {
  id: string
  taskId: string
  viewerId: string | null
  groupId: string | null
  linkId: string | null
  email: string | null
  notifiedAt: Timestamp | null
  lastReminderAt: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TaskActivity {
  id: string
  taskId: string
  type: string
  comment: string | null
  fromStatus: string | null
  toStatus: string | null
  userId: string | null
  viewerId: string | null
  viewId: string | null
  createdAt: Generated<Timestamp>
}

export interface DataroomDocument {
  id: string
  dataroomId: string
  documentId: string
  folderId: string | null
  orderIndex: number | null
  hierarchicalIndex: string | null
  vectorStoreFileId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DataroomFolder {
  id: string
  name: string
  path: string
  parentId: string | null
  icon: string | null
  color: string | null
  dataroomId: string
  orderIndex: number | null
  hierarchicalIndex: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DataroomBrand {
  id: string
  logo: string | null
  hideLogo: boolean | null
  banner: string | null
  brandColor: string | null
  accentColor: string | null
  accentButtonColor: string | null
  applyAccentColorToDataroomView: Generated<boolean>
  welcomeMessage: string | null
  cardLayout: Generated<string>
  showFolderTree: Generated<boolean>
  viewerLayoutPreset: Generated<string>
  viewerHeaderStyle: Generated<string>
  hideFolderIconsInMain: Generated<boolean>
  ctaLabel: string | null
  ctaUrl: string | null
  customLinkPreviewEnabled: Generated<boolean>
  linkPreviewTitle: string | null
  linkPreviewDescription: string | null
  linkPreviewImage: string | null
  linkPreviewFavicon: string | null
  defaultLanguage: Generated<string>
  dataroomId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ViewerGroup {
  id: string
  name: string
  domains: string[]
  allowAll: Generated<boolean>
  dataroomId: string
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ViewerGroupMembership {
  id: string
  viewerId: string
  groupId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ViewerGroupAccessControls {
  id: string
  groupId: string
  itemId: string
  itemType: ItemType
  canView: Generated<boolean>
  canDownload: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PermissionGroup {
  id: string
  name: string
  description: string | null
  dataroomId: string
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PermissionGroupAccessControls {
  id: string
  groupId: string
  itemId: string
  itemType: ItemType
  canView: Generated<boolean>
  canDownload: Generated<boolean>
  canDownloadOriginal: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Document {
  id: string
  name: string
  description: string | null
  file: string
  originalFile: string | null
  type: string | null
  contentType: string | null
  storageType: Generated<DocumentStorageType>
  numPages: number | null
  teamId: string
  ownerId: string | null
  assistantEnabled: Generated<boolean>
  advancedExcelEnabled: Generated<boolean>
  agentsEnabled: Generated<boolean>
  downloadOnly: Generated<boolean>
  hiddenInAllDocuments: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  folderId: string | null
  isExternalUpload: Generated<boolean>
}

export interface DocumentVersion {
  id: string
  versionNumber: number
  documentId: string
  file: string
  originalFile: string | null
  type: string | null
  contentType: string | null
  fileSize: bigint | null
  storageType: Generated<DocumentStorageType>
  numPages: number | null
  isPrimary: Generated<boolean>
  isVertical: Generated<boolean>
  fileId: string | null
  vectorStoreFileId: string | null
  hasPages: Generated<boolean>
  length: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DocumentPage {
  id: string
  versionId: string
  pageNumber: number
  embeddedLinks: string[]
  pageLinks: unknown
  metadata: unknown
  file: string
  storageType: Generated<DocumentStorageType>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Folder {
  id: string
  name: string
  path: string
  parentId: string | null
  hiddenInAllDocuments: Generated<boolean>
  icon: string | null
  color: string | null
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DocumentUpload {
  id: string
  documentId: string
  teamId: string
  viewerId: string | null
  viewId: string | null
  linkId: string
  dataroomId: string | null
  dataroomDocumentId: string | null
  taskId: string | null
  originalFilename: string | null
  fileSize: bigint | null
  numPages: number | null
  mimeType: string | null
  uploadedAt: Generated<Timestamp>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PendingDocumentUpload {
  id: string
  uploadId: string
  teamId: string
  tokenId: string
  key: string
  contentType: string
  storageType: Generated<DocumentStorageType>
  expiresAt: Timestamp
  consumedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface Integration {
  id: string
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
  category: string | null
  comingSoon: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface InstalledIntegration {
  id: string
  credentials: unknown
  configuration: unknown
  enabled: Generated<boolean>
  integrationId: string
  userId: string | null
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
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

export interface Link {
  id: string
  documentId: string | null
  dataroomId: string | null
  linkType: Generated<LinkType>
  url: string | null
  name: string | null
  slug: string | null
  expiresAt: Timestamp | null
  password: string | null
  allowList: string[]
  denyList: string[]
  emailProtected: Generated<boolean>
  emailAuthenticated: Generated<boolean>
  allowDownload: Generated<boolean | null>
  isArchived: Generated<boolean>
  deletedAt: Timestamp | null
  domainId: string | null
  domainSlug: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  enableNotification: Generated<boolean | null>
  enableFeedback: Generated<boolean | null>
  enableQuestion: Generated<boolean | null>
  enableScreenshotProtection: Generated<boolean | null>
  enableConfidentialView: Generated<boolean | null>
  enableAgreement: Generated<boolean | null>
  agreementId: string | null
  showBanner: Generated<boolean | null>
  enableWatermark: Generated<boolean | null>
  watermarkConfig: unknown
  audienceType: Generated<LinkAudienceType>
  groupId: string | null
  permissionGroupId: string | null
  metaTitle: string | null
  metaDescription: string | null
  metaImage: string | null
  metaFavicon: string | null
  enableCustomMetatag: Generated<boolean | null>
  welcomeMessage: string | null
  enableConversation: Generated<boolean>
  enableAIAgents: Generated<boolean | null>
  enableUpload: Generated<boolean | null>
  isFileRequestOnly: Generated<boolean | null>
  uploadFolderId: string | null
  uploadFolderIds: Generated<string[]>
  enableIndexFile: Generated<boolean | null>
  teamId: string | null
  brandId: string | null
  ownerId: string | null
}

export interface LinkPreset {
  id: string
  name: string
  teamId: string
  pId: string | null
  enableCustomMetaTag: Generated<boolean | null>
  metaTitle: string | null
  metaDescription: string | null
  metaImage: string | null
  metaFavicon: string | null
  enableNotification: Generated<boolean | null>
  emailProtected: Generated<boolean | null>
  emailAuthenticated: Generated<boolean | null>
  allowDownload: Generated<boolean | null>
  enableAllowList: Generated<boolean | null>
  allowList: string[]
  enableDenyList: Generated<boolean | null>
  denyList: string[]
  expiresIn: number | null
  enableScreenshotProtection: Generated<boolean | null>
  enableConfidentialView: Generated<boolean | null>
  expiresAt: Timestamp | null
  enablePassword: Generated<boolean | null>
  password: string | null
  enableWatermark: Generated<boolean | null>
  watermarkConfig: unknown
  enableAgreement: Generated<boolean | null>
  agreementId: string | null
  enableCustomFields: Generated<boolean | null>
  customFields: unknown
  showBanner: Generated<boolean | null>
  welcomeMessage: string | null
  isDefault: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface VisitorGroup {
  id: string
  name: string
  emails: string[]
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface LinkVisitorGroup {
  id: string
  linkId: string
  visitorGroupId: string
  createdAt: Generated<Timestamp>
}

export interface CustomField {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: CustomFieldType
  identifier: string
  label: string
  placeholder: string | null
  required: Generated<boolean>
  disabled: Generated<boolean>
  linkId: string
  orderIndex: Generated<number>
}

export interface CustomFieldResponse {
  id: string
  data: unknown
  viewId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface NotificationPreference {
  id: string
  userId: string
  teamId: string
  type: string
  frequency: Generated<string>
  scope: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OAuthClient {
  id: string
  clientId: string
  clientName: string
  clientUri: string | null
  logoUri: string | null
  redirectUris: string[]
  grantTypes: string[]
  scopes: string
  tokenAuthMethod: string
  hashedSecret: string | null
  registrationType: string
  metadataUrl: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface OAuthClientApproval {
  id: string
  userId: string
  clientId: string
  approvedAt: Generated<Timestamp>
  revokedAt: Timestamp | null
  clientMetadataHash: string | null
}

export interface OAuthGrantTeam {
  id: string
  grantId: string
  teamId: string
  userId: string
  createdAt: Generated<Timestamp>
}

export interface OAuthRecord {
  id: string
  kind: string
  payload: unknown
  grantId: string | null
  userCode: string | null
  uid: string | null
  expiresAt: Timestamp | null
  consumedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface DocumentRedactionJob {
  id: string
  documentId: string
  documentVersionId: string
  teamId: string
  createdById: string
  status: Generated<string>
  customTerms: string[]
  reasons: string[]
  resultVersionId: string | null
  error: string | null
  detectRunId: string | null
  applyRunId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface DocumentRedaction {
  id: string
  jobId: string
  pageNumber: number
  x: number
  y: number
  width: number
  height: number
  textPreview: string | null
  category: string | null
  confidence: string | null
  reason: string | null
  source: Generated<string>
  status: Generated<string>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ReferralInvite {
  id: string
  userId: string
  teamId: string | null
  scopeId: string
  email: string
  referralLink: string | null
  sentAt: Generated<Timestamp>
}

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
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
}

export interface User {
  id: string
  name: string | null
  email: string | null
  emailVerified: Timestamp | null
  image: string | null
  createdAt: Generated<Timestamp>
  contactId: string | null
  plan: Generated<string>
  stripeId: string | null
  subscriptionId: string | null
  startsAt: Timestamp | null
  endsAt: Timestamp | null
  referralTrack: ReferralTrack | null
  referralTrackSelectedAt: Timestamp | null
  referralProfile: unknown
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface Domain {
  id: string
  slug: string
  userId: string | null
  teamId: string
  verified: Generated<boolean>
  isDefault: Generated<boolean>
  redirectUrl: string | null
  lastChecked: Generated<Timestamp>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface View {
  id: string
  linkId: string
  documentId: string | null
  dataroomId: string | null
  dataroomViewId: string | null
  viewerEmail: string | null
  viewerName: string | null
  verified: Generated<boolean>
  viewedAt: Generated<Timestamp>
  downloadedAt: Timestamp | null
  downloadType: DownloadType | null
  downloadMetadata: unknown
  viewType: Generated<ViewType>
  viewerId: string | null
  groupId: string | null
  isArchived: Generated<boolean>
  teamId: string | null
}

export interface Viewer {
  id: string
  email: string
  verified: Generated<boolean>
  invitedAt: Timestamp | null
  notificationPreferences: unknown
  dataroomId: string | null
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Reaction {
  id: string
  viewId: string
  pageNumber: number
  type: string
  createdAt: Generated<Timestamp>
}

export interface Invitation {
  email: string
  expires: Timestamp
  teamId: string
  createdAt: Generated<Timestamp>
  token: string
  role: Generated<Role>
  dataroomIds: Generated<string[]>
}

export interface SentEmail {
  id: string
  type: EmailType
  recipient: string
  marketing: Generated<boolean>
  createdAt: Generated<Timestamp>
  teamId: string
  domainSlug: string | null
}

export interface Chat {
  id: string
  title: string | null
  teamId: string
  documentId: string | null
  dataroomId: string | null
  linkId: string | null
  viewId: string | null
  userId: string | null
  viewerId: string | null
  vectorStoreId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  lastMessageAt: Timestamp | null
}

export interface ChatMessage {
  id: string
  chatId: string
  role: string
  content: string
  metadata: unknown
  createdAt: Generated<Timestamp>
}

export interface Feedback {
  id: string
  linkId: string
  data: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface FeedbackResponse {
  id: string
  feedbackId: string
  data: unknown
  viewId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Agreement {
  id: string
  name: string
  content: string
  contentType: Generated<string>
  signingProvider: Generated<string>
  signingExternalId: string | null
  signingEnvelopeId: string | null
  signingTemplateId: string | null
  requireName: Generated<boolean>
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  deletedAt: Timestamp | null
  deletedBy: string | null
}

export interface AgreementResponse {
  id: string
  agreementId: string
  viewId: string | null
  linkId: string | null
  signerEmail: string | null
  signerName: string | null
  signingStatus: Generated<string>
  signingExternalId: string | null
  signingEnvelopeId: string | null
  signingDocumentId: number | null
  signedAt: Timestamp | null
  completedAt: Timestamp | null
  signedFileKey: string | null
  signedFileName: string | null
  signedFileStorageType: DocumentStorageType | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface IncomingWebhook {
  id: string
  externalId: string
  name: string
  secret: string | null
  source: string | null
  actions: string | null
  consecutiveFailures: Generated<number>
  lastFailedAt: Timestamp | null
  disabledAt: Timestamp | null
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface RestrictedToken {
  id: string
  name: string
  hashedKey: string
  partialKey: string
  scopes: string | null
  mode: Generated<string>
  source: Generated<string>
  subjectType: Generated<string>
  clientId: string | null
  expires: Timestamp | null
  lastUsed: Timestamp | null
  rateLimit: Generated<number>
  userId: string
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Webhook {
  id: string
  pId: string
  name: string
  url: string
  secret: string
  triggers: unknown
  teamId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface YearInReview {
  id: string
  teamId: string
  status: Generated<string>
  attempts: Generated<number>
  lastAttempted: Timestamp | null
  error: string | null
  stats: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Tag {
  id: string
  name: string
  color: string
  description: string | null
  teamId: string
  createdBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TagItem {
  id: string
  tagId: string
  itemType: TagType
  linkId: string | null
  documentId: string | null
  dataroomId: string | null
  taggedBy: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ViewerInvitation {
  id: string
  viewerId: string
  linkId: string
  groupId: string | null
  invitedBy: string
  customMessage: string | null
  sentAt: Generated<Timestamp>
  status: Generated<InvitationStatus>
  createdAt: Generated<Timestamp>
}

export interface Team {
  id: string
  name: string
  slug: string | null
  defaultBrandId: string | null
  plan: Generated<string>
  stripeId: string | null
  subscriptionId: string | null
  startsAt: Timestamp | null
  endsAt: Timestamp | null
  pausedAt: Timestamp | null
  pauseStartsAt: Timestamp | null
  pauseEndsAt: Timestamp | null
  cancelledAt: Timestamp | null
  trialStartedAt: Timestamp | null
  trialEndsAt: Timestamp | null
  trialReminderRunId: string | null
  trialExpiredRunId: string | null
  limits: unknown
  enableExcelAdvancedMode: Generated<boolean>
  replicateDataroomFolders: Generated<boolean>
  timezone: Generated<string>
  ssoEnabled: Generated<boolean>
  ssoEmailDomain: string | null
  ssoEnforcedAt: Timestamp | null
  surveyData: unknown
  agentsEnabled: Generated<boolean>
  vectorStoreId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  ignoredDomains: string[]
  globalBlockList: string[]
  referralTrack: ReferralTrack | null
  referralTrackSelectedAt: Timestamp | null
  referralProfile: unknown
}

export interface UserDataroom {
  userId: string
  teamId: string
  dataroomId: string
  createdAt: Generated<Timestamp>
}

export interface UserTeam {
  role: Generated<Role>
  status: Generated<string>
  userId: string
  teamId: string
  blockedAt: Timestamp | null
  notificationPreferences: unknown
}

export interface Workflow {
  id: string
  name: string
  description: string | null
  entryLinkId: string
  teamId: string
  isActive: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowStep {
  id: string
  workflowId: string
  name: string
  stepOrder: number
  stepType: Generated<WorkflowStepType>
  conditions: unknown
  actions: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkflowExecution {
  id: string
  workflowId: string
  visitorEmail: string | null
  visitorIp: string | null
  status: ExecutionStatus
  startedAt: Generated<Timestamp>
  completedAt: Timestamp | null
  result: unknown
  metadata: unknown
}

export interface WorkflowStepLog {
  id: string
  executionId: string
  workflowStepId: string
  conditionsMatched: boolean
  conditionResults: unknown
  actionsExecuted: unknown
  executedAt: Generated<Timestamp>
  duration: number | null
  error: string | null
}

export interface DB {
  DocumentAnnotation: DocumentAnnotation
  AnnotationImage: AnnotationImage
  Brand: Brand
  Conversation: Conversation
  DataroomQuestion: DataroomQuestion
  DataroomQuestionAssignment: DataroomQuestionAssignment
  ConversationParticipant: ConversationParticipant
  Message: Message
  ConversationView: ConversationView
  DataroomFaqItem: DataroomFaqItem
  Dataroom: Dataroom
  TaskList: TaskList
  Task: Task
  TaskAssignment: TaskAssignment
  TaskActivity: TaskActivity
  DataroomDocument: DataroomDocument
  DataroomFolder: DataroomFolder
  DataroomBrand: DataroomBrand
  ViewerGroup: ViewerGroup
  ViewerGroupMembership: ViewerGroupMembership
  ViewerGroupAccessControls: ViewerGroupAccessControls
  PermissionGroup: PermissionGroup
  PermissionGroupAccessControls: PermissionGroupAccessControls
  Document: Document
  DocumentVersion: DocumentVersion
  DocumentPage: DocumentPage
  Folder: Folder
  DocumentUpload: DocumentUpload
  PendingDocumentUpload: PendingDocumentUpload
  Integration: Integration
  InstalledIntegration: InstalledIntegration
  jackson_index: jackson_index
  jackson_store: jackson_store
  jackson_ttl: jackson_ttl
  Link: Link
  LinkPreset: LinkPreset
  VisitorGroup: VisitorGroup
  LinkVisitorGroup: LinkVisitorGroup
  CustomField: CustomField
  CustomFieldResponse: CustomFieldResponse
  NotificationPreference: NotificationPreference
  OAuthClient: OAuthClient
  OAuthClientApproval: OAuthClientApproval
  OAuthGrantTeam: OAuthGrantTeam
  OAuthRecord: OAuthRecord
  DocumentRedactionJob: DocumentRedactionJob
  DocumentRedaction: DocumentRedaction
  ReferralInvite: ReferralInvite
  Account: Account
  Session: Session
  User: User
  VerificationToken: VerificationToken
  Domain: Domain
  View: View
  Viewer: Viewer
  Reaction: Reaction
  Invitation: Invitation
  SentEmail: SentEmail
  Chat: Chat
  ChatMessage: ChatMessage
  Feedback: Feedback
  FeedbackResponse: FeedbackResponse
  Agreement: Agreement
  AgreementResponse: AgreementResponse
  IncomingWebhook: IncomingWebhook
  RestrictedToken: RestrictedToken
  Webhook: Webhook
  YearInReview: YearInReview
  Tag: Tag
  TagItem: TagItem
  ViewerInvitation: ViewerInvitation
  Team: Team
  UserDataroom: UserDataroom
  UserTeam: UserTeam
  Workflow: Workflow
  WorkflowStep: WorkflowStep
  WorkflowExecution: WorkflowExecution
  WorkflowStepLog: WorkflowStepLog
}
